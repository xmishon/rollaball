using mzmeevskiy;
using UnityEngine;

namespace mazmeevskiy
{
    public sealed class GameController : MonoBehaviour
    {
        private InteractiveObject[] _interactiveObjects;

        private void Awake()
        {
            _interactiveObjects = FindObjectsOfType<InteractiveObject>();
        }

        private void Update()
        {
            for(int i = 0; i < _interactiveObjects.Length; i++)
            {
                var interactiveOngect = _interactiveObjects[i];
                if (interactiveOngect == null)
                    continue;

                if(interactiveOngect is IFly fly)
                {
                    fly.Fly();
                }

                if(interactiveOngect is IFlicker flicker)
                {
                    flicker.Flicker();
                }

                if(interactiveOngect is IRotation rotation)
                {
                    rotation.Rotate();
                }
            }
        }
    }
}