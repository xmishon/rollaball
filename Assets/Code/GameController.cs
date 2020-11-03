using mzmeevskiy;
using UnityEngine;

namespace mazmeevskiy
{
    public sealed class GameController : MonoBehaviour
    {
        private InteractiveObject[] _interactiveObjects;
        private BonusController _bonusController;
        private Display _display;
        private SoundController _soundController;

        private void Awake()
        {
            _soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
            _display = new Display();
            _bonusController = new BonusController();
            _bonusController.BonusesChanged += _display.DisplayBonuses;
            _interactiveObjects = FindObjectsOfType<InteractiveObject>();
            foreach (InteractiveObject interactiveObject in _interactiveObjects)
            {
                if (interactiveObject is GoodBonus goodBonus)
                {
                    goodBonus.GoodBonusCaught += _bonusController.OnBonusCaught;
                    goodBonus.GoodBonusCaught += _soundController.PlayBonusPickupSound;
                    continue;
                }
                if (interactiveObject is Finish finish)
                {
                    finish.GameFinishedAction += _display.DisplayGameFinished;
                }
            }
        }

        private void Update()
        {
            for(int i = 0; i < _interactiveObjects.Length; i++)
            {
                var interactiveObject = _interactiveObjects[i];
                if (interactiveObject == null)
                    continue;

                if(interactiveObject is IFly fly)
                {
                    fly.Fly();
                }

                if(interactiveObject is IFlicker flicker)
                {
                    flicker.Flicker();
                }

                if(interactiveObject is IRotation rotation)
                {
                    rotation.Rotate();
                }
            }
        }
    }
}