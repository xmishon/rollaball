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
        private SaveDataRepository _saveDataRepository;

        private Button _restartButton;
        private int _countBonuses;
        private Reference _reference;

        private void Awake()
        {
            Time.timeScale = 1.0f;
            LoadNewGame();
        }

        private void LoadNewGame()
        {

            _countBonuses = 0;

            _reference = new Reference();
            _interactiveObjects = new ListExecuteObject();

            _inputController = new InputController(_reference.PlayerBall, _reference.CameraRig);
            _interactiveObjects.AddExecuteObject(_inputController);

            _soundController = new SoundController(_reference.PlayerBall.GetComponent<AudioSource>());
            _displayEndGame = new DisplayEndGame(_reference.Canvas.transform.Find("GameFinishedText").GetComponent<Text>());
            _displayBonuses = new DisplayBonuses(_reference.Canvas.transform.Find("BonusesCountText").GetComponent<Text>());

            _cameraController = new CameraController(_reference.PlayerBall.transform, _reference.CameraRig);
            _interactiveObjects.AddExecuteObject(_cameraController);

            _restartButton = _reference.RestartButton;
            _restartButton.onClick.AddListener(Restart);
            _restartButton.gameObject.SetActive(false);

            _saveDataRepository = new SaveDataRepository();
            _saveDataRepository.SetPlayerBase(_reference.PlayerBall);
            _saveDataRepository.SetCameraRig(_reference.CameraRig);
            _inputController.OnSaveCall += _saveDataRepository.Save;

            foreach (var o in _interactiveObjects)
            {
                if (o is GoodBonus goodBonus)
                {
                    goodBonus.OnPointChange += AddBonus;
                    goodBonus.OnPointChange += _soundController.PlayBonusPickupSound;
                    _saveDataRepository.AddObjectToSave(goodBonus);
                    continue;
                }
                if (o is BadBonus badBonus)
                {
                    badBonus.OnCaughtPlayerChange += CaughtPlayer;
                    badBonus.OnCaughtPlayerChange += _displayEndGame.GameOver;
                    _saveDataRepository.AddObjectToSave(badBonus);
                }
                if (o is Finish finish)
                {

                }
            }
        }

        private void LoadSavedGame(SavedData savedData)
        {
            ClearScene();

        }

        private void ClearScene()
        {
            var interactiveObjects = Object.FindObjectsOfType<InteractiveObject>();
            for (var i = 0; i < interactiveObjects.Length; i++)
            {
                if (interactiveObjects[i] is IExecute interactiveObject)
                {
                    Destroy(interactiveObjects[i]);
                }
            }
            Destroy(_reference.PlayerBall);
            Destroy(_reference.CameraRig);

            _interactiveObjects = null;
            _displayEndGame = null;
            _displayBonuses = null;
            _soundController = null;
            _cameraController = null;
            _inputController = null;

            _countBonuses = 0;
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