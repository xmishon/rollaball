using UnityEngine;
using Random = UnityEngine.Random;

namespace mzmeevskiy
{
    public abstract class InteractiveObject : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; } = true;

        #region UnityMethods

        private void OnTriggerEnter(Collider other)
        {
            if(!IsInteractable || !other.CompareTag("Player"))
            {
                return;
            }
            Interaction();
            Destroy(gameObject);
        }

        #endregion

        protected abstract void Interaction();

        private void Start()
        {
            DoAction();
        }

        public void DoAction()
        {
            if(TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Random.ColorHSV();
            }
        }
    }
}
