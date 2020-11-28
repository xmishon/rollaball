using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace mzmeevskiy
{
    public sealed class GameController : MonoBehaviour
    {
        public event Action<int> TotalPointChanged = delegate (int i) { };

        private ListExecuteObject _executableObjects;
        private DisplayEndGame _displayEndGame;
        private DisplayBonuses _displayBonuses;
        private SoundController _soundController;
        private CameraController _cameraController;
        private InputController _inputController;
        private SaveDataRepository _saveDataRepository;

        private Button _restartButton;
        private int _countBonuses;
        private Reference _reference;
        private bool _sceneReloaded;

        private void Awake()
        {
            _reference = new Reference();

            _saveDataRepository = new SaveDataRepository(_reference);
            _saveDataRepository.Load("newGameData.txt");

            var go = _reference.GetNewGoodBonus();
            InteractiveObject io = go.GetComponent<InteractiveObject>();
            io.IsInteractable = false;

            Initialize();
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
            TotalPointChanged.Invoke(_countBonuses);
        }

        private void FixedUpdate()
        {
            _sceneReloaded = false;
            for(int i = 0; i < _executableObjects.Length; i++)
            {
                if (_sceneReloaded)
                {
                    break;
                }
                var interactiveObject = _executableObjects[i];
                if (interactiveObject.Equals(null))
                {
                    continue;
                }
                interactiveObject.Execute();
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
            Debug.Log("Scene reloaded");
        }

        public void Dispose()
        {
            foreach(var o in _executableObjects)
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
            _inputController.OnLoadCall -= Dispose;
            _inputController.OnLoadCall -= Load;
            Debug.Log("Dispose");
        }

        public void Load()
        {
            _saveDataRepository.Load();
            Initialize();
        }

        public void Initialize()
        {
            Time.timeScale = 1.0f;

            _executableObjects = new ListExecuteObject();
            _inputController = new InputController(_reference.PlayerBall, _reference.CameraRig);

            _executableObjects.AddExecuteObject(_inputController);
            
            _inputController.OnLoadCall += MarkSceneReloaded;
            _inputController.OnLoadCall += Dispose;
            _inputController.OnLoadCall += Load;
            _inputController.OnSaveCall += _saveDataRepository.Save;

            _soundController = new SoundController(_reference.PlayerBall.GetComponent<AudioSource>());
            _displayEndGame = new DisplayEndGame(_reference.Canvas.transform.Find("GameFinishedText").GetComponent<Text>());
            _displayBonuses = new DisplayBonuses(_reference.Canvas.transform.Find("BonusesCountText").GetComponent<Text>());

            _cameraController = new CameraController(_reference.PlayerBall.transform, _reference.CameraRig);
            _executableObjects.AddExecuteObject(_cameraController);

            _restartButton = _reference.RestartButton;
            _restartButton.onClick.AddListener(Restart);
            _restartButton.gameObject.SetActive(false);

            foreach (var o in _executableObjects)
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
            }
            Debug.Log("Initialize");
        }

        public void MarkSceneReloaded()
        {
            _sceneReloaded = true;
        }
    }
}