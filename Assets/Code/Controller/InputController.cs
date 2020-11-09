using UnityEngine;
using UnityEngine.UIElements;

namespace mzmeevskiy
{
    public class InputController : IExecute
    {
        private readonly PlayerBase _playerBase;
        private Transform _cameraPosition;

        public InputController(PlayerBase player, GameObject cameraRig)
        {
            _playerBase = player;
            _cameraPosition = cameraRig.GetComponentInChildren<Camera>().transform;
        }

        public void Execute()
        {
            Vector3 viewDirection = _playerBase.transform.position - _cameraPosition.position;
            viewDirection.y = 0.0f;

            Vector3 moveDirection = viewDirection * Input.GetAxis("Vertical") 
                + Quaternion.AngleAxis(90.0f, Vector3.up) * viewDirection * Input.GetAxis("Horizontal");
            moveDirection.Normalize();

            _playerBase.Move(moveDirection);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerBase.Jump();
            }
        }
    }
}