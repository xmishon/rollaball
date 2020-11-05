using UnityEngine;

namespace mzmeevskiy
{
    public sealed class GameController : MonoBehaviour
    {
        private ListExecuteObject _interactiveObjects;
        private DisplayEndGame _displayEndGame;
        private DisplayBonuses _displayBonuses;

        private int _countBonuses;
        private SoundController _soundController;

        private void Awake()
        {
            _soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
            _displayEndGame = new DisplayEndGame();
            _displayBonuses = new DisplayBonuses();
            _interactiveObjects = new ListExecuteObject();
            foreach (InteractiveObject interactiveObject in _interactiveObjects)
            {
                if (interactiveObject is GoodBonus goodBonus)
                {
                    goodBonus.OnPointChange += AddBonus;
                    goodBonus.OnPointChange += _soundController.PlayBonusPickupSound;
                    continue;
                }
                if (interactiveObject is BadBonus badBonus)
                {
                    badBonus.OnCaughtPlayerChange += CaughtPlayer;
                    badBonus.OnCaughtPlayerChange += _displayEndGame.GameOver;
                }
                if (interactiveObject is Finish finish)
                {
                    
                }
            }
        }

        private void CaughtPlayer(string value, Color args)
        {
            Time.timeScale = 0.0f;
        }

        private void AddBonus(int value)
        {
            _countBonuses += value;
            _displayBonuses.Display(_countBonuses);
        }

        private void Update()
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
                }
            }
        }
    }
}