using UnityEditor;
using UnityEngine;

namespace mzmeevskiy
{
    public class PlayerBall : PlayerBase
    {
        private Rigidbody _rigidbody;

        private float _sqrSpeed = 0;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _sqrSpeed = Speed * Speed;
        }

        public override void Move(Vector3 moveDirection)
        {
            if (_rigidbody.velocity.sqrMagnitude < _sqrSpeed)
            {
                _rigidbody.AddForce(moveDirection * Sensitivity, ForceMode.Force);
            }
        }

        public override void Jump()
        {
            _rigidbody.AddForce(Vector3.up * JumpStrength, ForceMode.VelocityChange);
        }        
    }
}