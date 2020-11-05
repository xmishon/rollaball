using UnityEngine;

namespace mzmeevskiy
{
    public class Reference 
    {
        private PlayerBall _playerBall;
        private Camera _mainCamera;

        public PlayerBall PlayerBall
        {
            get
            {
                if (_playerBall == null)
                {
                    var gameObject = Resources.Load<PlayerBall>("Player");
                    _playerBall = Object.Instantiate(gameObject);
                }

                return _playerBall;
            }
        }
    }
}