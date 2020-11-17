﻿using UnityEngine;
using UnityEngine.UIElements;

namespace mzmeevskiy
{
    public class InputController : IExecute
    {
        private readonly PlayerBase _playerBase;
        private Transform _cameraPosition;

        private readonly SaveDataRepository _saveDataRepository;
        private readonly KeyCode _savePlayer = KeyCode.C;
        private readonly KeyCode _loadPlayer = KeyCode.V;
        private InteractiveObject[] _interactiveObjects;

        public InputController(PlayerBase player, GameObject cameraRig, InteractiveObject interactiveObjects)
        {
            _playerBase = player;
            _cameraPosition = cameraRig.GetComponentInChildren<Camera>().transform;
            _interactiveObjects = interactiveObjects;

            _saveDataRepository = new SaveDataRepository();
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

            if (Input.GetKeyDown(_savePlayer))
            {
                _saveDataRepository.Save(_interactiveObjects);
            }

            if (Input.GetKeyDown(_loadPlayer))
            {
                _saveDataRepository.Load(_playerBase);
            }
        }
    }
}