using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace mzmeevskiy
{
    public sealed class SaveDataRepository
    {
        private readonly IData<SavedData> _repository;

        private List<InteractiveObject> _interactiveObjects;
        private PlayerBase _playerBase;
        private GameObject _cameraRig;

        private SavedData _savedData;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.txt";
        private readonly string _path;

        public SaveDataRepository()
        {
            _repository = new JsonData<SavedData>();
            _path = Path.Combine(Application.dataPath, _folderName);
            _savedData = new SavedData();
            _interactiveObjects = new List<InteractiveObject>();
        }

        public void Save()
        {
            _savedData.savedDataItmes = new List<SavedDataItem>();
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }

            foreach (var o in _interactiveObjects)
            {
                var gameObject = new SavedDataItem
                {
                    Position = o.transform.position,
                    Name = o.name,
                    IsInteractable = o.IsInteractable
                };
                if (o is GoodBonus goodBonus)
                {
                    gameObject.GameObjectType = GameObjectTypes.GoodBonus;
                }
                if (o is BadBonus badBonus)
                {
                    gameObject.GameObjectType = GameObjectTypes.BadBonus;
                }
                _savedData.savedDataItmes.Add(gameObject);
            }

            if (_playerBase != null)
            {
                var playerGameObject = new SavedDataItem
                {
                    Position = _playerBase.transform.position,
                    Name = _playerBase.gameObject.name,
                    IsInteractable = true,
                    GameObjectType = GameObjectTypes.Player
                };
                _savedData.savedDataItmes.Add(playerGameObject);
            }

            if (_cameraRig != null)
            {
                var cameraRigGameObject = new SavedDataItem
                {
                    Position = _cameraRig.transform.position,
                    Name = _cameraRig.gameObject.name,
                    IsInteractable = true,
                    GameObjectType = GameObjectTypes.CameraRig
                };
                _savedData.savedDataItmes.Add(cameraRigGameObject);
            }

            _repository.Save(_savedData, Path.Combine(_path, _fileName));
        }

        public void Load(SavedData savedData)
        {
            var file = Path.Combine(_path, _fileName);
            if (!File.Exists(file)) return;
            var data = _repository.Load(file);
            savedData = data;

            Debug.Log(savedData);
        }

        public void AddObjectToSave(InteractiveObject gameObject)
        {
            _interactiveObjects.Add(gameObject);
        }

        public void SetPlayerBase(PlayerBase playerBase)
        {
            _playerBase = playerBase;
        }

        public void SetCameraRig(GameObject cameraRig)
        {
            _cameraRig = cameraRig;
        }
    }
}