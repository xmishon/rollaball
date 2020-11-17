using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace mzmeevskiy
{
    public sealed class SaveDataRepository
    {
        private readonly IData<SavedData> _repository;

        private List<GameObject> _objectsToSave;

        private SavedData _savedData;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.txt";
        private readonly string _path;

        public SaveDataRepository()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                _repository = new PlayerPrefsData();
            }
            else
            {
                _repository = new JsonData<SavedData>();
                _path = Path.Combine(Application.dataPath, _folderName);
            }
            _savedData = new SavedData();
            _objectsToSave = new List<GameObject>();
        }

        public void Save()
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            foreach (var o in _objectsToSave)
            {
                var gameObject = new SavedDataItem
                {
                    Position = o.transform.position,
                    Name = o.name
                };
                InteractiveObject interactiveObject = null;
                if (o.TryGetComponent<InteractiveObject>(out interactiveObject))
                {
                    gameObject.IsEnabled = interactiveObject.enabled;
                }
                else
                {
                    gameObject.IsEnabled = true;
                }

                if (interactiveObject != null)
                {
                    if(interactiveObject is GoodBonus goodBonus)
                    {
                        gameObject.GameObjectType = GameObjectTypes.GoodBonus;
                        continue;
                    }
                    if(interactiveObject is BadBonus badBonus)
                    {
                        gameObject.GameObjectType = GameObjectTypes.BadBonus;
                        continue;
                    }
                }

                PlayerBase playerBase; 
                if(o.TryGetComponent<PlayerBase>(out playerBase))
                {
                    gameObject.GameObjectType = GameObjectTypes.Player;
                    continue;
                }

                Camera camera = o.GetComponentInChildren<Camera>(); 
                if (camera != null)
                {
                    gameObject.GameObjectType = GameObjectTypes.CameraRig;
                    continue;
                }

                _savedData.savedDataItmes.Add(gameObject);
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

        public void AddObjectToSave(GameObject gameObject)
        {
            _objectsToSave.Add(gameObject);
        }
    }
}