using System.Collections.Generic;
using UnityEngine;

namespace mzmeevskiy
{
    public sealed class SaveController
    {
        private List<GameObject> _objectsToSave;

        public SaveController()
        {
            _objectsToSave = new List<GameObject>();
        }

        public void Add(GameObject gameObject)
        {
            _objectsToSave.Add(gameObject);
        }

        public List<GameObject> GetObjectsToSave()
        {
            return _objectsToSave;
        }
    }
}