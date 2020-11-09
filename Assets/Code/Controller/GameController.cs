using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace mzmeevskiy
{
    public sealed class GameController : MonoBehaviour
    {
        private ListExecuteObject _interactiveObjects;
        private DisplayEndGame _displayEndGame;
        private DisplayBonuses _displayBonuses;
        private SoundController _soundController;
        private CameraController _cameraController;
        private InputController _inputController;

        private Button _restartButton;
        private int _countBonuses;

        private void Awake()
        {
            Time.timeScale = 1.0f;

            _countBonuses = 0;

            var reference = new Reference();
            _interactiveObjects = new ListExecuteObject();

            _inputController = new InputController(reference.PlayerBall, reference.CameraRig);
            _interactiveObjects.AddExecuteObject(_inputController);

            _soundController = new SoundController(reference.PlayerBall.GetComponent<AudioSource>());
            _displayEndGame = new DisplayEndGame(reference.Canvas.transform.Find("GameFinishedText").GetComponent<Text>());
            _displayBonuses = new DisplayBonuses(reference.Canvas.transform.Find("BonusesCountText").GetComponent<Text>());
            
            _cameraController = new CameraController(reference.PlayerBall.transform, reference.CameraRig);
            _interactiveObjects.AddExecuteObject(_cameraController);

            _restartButton = reference.RestartButton;
            _restartButton.onClick.AddListener(Restart);
            _restartButton.gameObject.SetActive(false);

            foreach (var o in _interactiveObjects)
            {
                if (o is GoodBonus goodBonus)
                {
                    goodBonus.OnPointChange += AddBonus;
                    goodBonus.OnPointChange += _soundController.PlayBonusPickupSound;
                    continue;
                }
                if (o is BadBonus badBonus)
                {
                    badBonus.OnCaughtPlayerChange += CaughtPlayer;
                    badBonus.OnCaughtPlayerChange += _displayEndGame.GameOver;
                }
                if (o is Finish finish)
                {
                    
                }
            }
        }

        private void CaughtPlayer(string value, string color)
        {
            Time.timeScale = 0.0f;
            _restartButton.gameObject.SetActive(true);
            Dispose();
        }

        private void AddBonus(int value)
        {
            _countBonuses += value;
            _displayBonuses.Display(_countBonuses);
        }

        private void FixedUpdate()
        {
            for(int i = 0; i < _interactiveObjects.Length; i++)
            {
                var interactiveObject = _interactiveObjects[i];
                if (interactiveObject == null)
                {
                    continue;
                }

                interactiveObject.Execute();
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }

        public void Dispose()
        {
            foreach(var o in _interactiveObjects)
            {
                if (o is BadBonus badBonus)
                {
                    badBonus.OnCaughtPlayerChange -= CaughtPlayer;
                    badBonus.OnCaughtPlayerChange -= _displayEndGame.GameOver;
                }

                if (o is GoodBonus goodBonus)
                {
                    goodBonus.OnPointChange -= AddBonus;
                    goodBonus.OnPointChange -= _soundController.PlayBonusPickupSound;
                }
            }
        }
    }
}