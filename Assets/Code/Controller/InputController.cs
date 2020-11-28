using UnityEngine;
using System;

namespace mzmeevskiy
{
    public class InputController : IExecute
    {
        private PlayerBase _playerBase;
        private Transform _cameraPosition;

        public event Action OnSaveCall = delegate { };
        public event Action OnLoadCall = delegate { };

        private readonly KeyCode _saveGame = KeyCode.C;
        private readonly KeyCode _loadGame = KeyCode.V;

        public InputController(PlayerBase player, GameObject cameraRig)
        {
            _playerBase = player;
            _cameraPosition = cameraRig.GetComponentInChildren<Camera>().transform;
        }

        public void SetPlayerBase(PlayerBase player)
        {
            _playerBase = player;
        }

        public void SetCameraPosition(GameObject cameraRig)
        {
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

            if (Input.GetKeyDown(_saveGame))
            {
                OnSaveCall.Invoke();
            }

            if (Input.GetKeyDown(_loadGame))
            {
                Debug.Log("LoadGame pressed");
                OnLoadCall.Invoke();
            }
        }
    }
}