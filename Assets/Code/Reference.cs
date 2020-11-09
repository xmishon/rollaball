using UnityEngine;
using UnityEngine.UI;

namespace mzmeevskiy
{
    public class Reference 
    {
        private PlayerBall _playerBall;
        private GameObject _cameraRig;
        private GameObject _bonus;
        private GameObject _endGame;
        private Canvas _canvas;
        private Button _restartButton;

        public Button RestartButton
        {
            get
            {
                if(_restartButton == null)
                {
                    var gameObject = Resources.Load<Button>("UI/RestartButton");
                    _restartButton = Object.Instantiate(gameObject, Canvas.transform);
                }

                return _restartButton;
            }
        }

        public Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                {
                    var gameObject = Resources.Load<Canvas>("UI/Canvas");
                    _canvas = Object.Instantiate(gameObject);
                }

                return _canvas;
            }
        }

        public GameObject CameraRig
        {
            get
            {
                if (_cameraRig == null)
                {
                    var gameObject = Resources.Load<GameObject>("CameraRig");
                    _cameraRig = Object.Instantiate(gameObject);
                }

                return _cameraRig;
            }
        }

        public PlayerBall PlayerBall
        {
            get
            {
                if (_playerBall == null)
                {
                    var gameObject = Resources.Load<PlayerBall>("PlayerBall");
                    _playerBall = Object.Instantiate(gameObject);
                }

                return _playerBall;
            }
        }

        public GameObject GoodBonus
        {
            get
            {
                if (_bonus == null)
                {
                    var gameObject = Resources.Load<GameObject>("Bonus");
                    _bonus = Object.Instantiate(gameObject);
                }

                return _bonus;
            }
        }

        public GameObject EndGame
        {
            get
            {
                if (_endGame == null)
                {
                    var gameObject = Resources.Load<GameObject>("BadBonus");
                    _endGame = Object.Instantiate(gameObject);
                }

                return _endGame;
            }
        }
    }
}