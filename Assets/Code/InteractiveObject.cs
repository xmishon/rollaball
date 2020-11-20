using UnityEngine;
using static UnityEngine.Random;

namespace mzmeevskiy
{
    public abstract class InteractiveObject : MonoBehaviour, IExecute
    {
        protected Color _color;
        private bool _isInteractable;

        public bool IsInteractable
        {
            get { return _isInteractable; }
            set
            {
                _isInteractable = value;
                GetComponent<Renderer>().enabled = _isInteractable;
                GetComponent<Collider>().enabled = _isInteractable;
            }
        }

        #region UnityMethods

        private void OnTriggerEnter(Collider other)
        {
            if(!IsInteractable || !other.CompareTag("Player"))
            {
                return;
            }
            Interaction();
            IsInteractable = false;
        }

        #endregion

        protected abstract void Interaction();

        public abstract void Execute();

        private void Start()
        {
            IsInteractable = true;
            _color = ColorHSV();
            if(TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = _color;
            }
        }
    }
}
