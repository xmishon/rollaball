using System.Collections.Generic;
using UnityEngine;

namespace mzmeevskiy
{
    public sealed class SaveController
    {
        private List<GameObject> _saveData;

        public SaveController()
        {
            _saveData = new List<GameObject>();
        }

        public void Add(GameObject gameObject)
        {
            _saveData.Add(gameObject);
        }

        public List<GameObject> GetObjectsToSave()
        {
            return _saveData;
        }
    }
}