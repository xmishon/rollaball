using UnityEngine;

namespace mzmeevskiy
{
    public class CameraController : IExecute
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _offsetUp = 1.0f;
        [SerializeField] private float _cameraRotationSpeed = 1.0f;
        [SerializeField] private float _cameraMovingSpeed = 3.0f;
        [SerializeField] private float _distanceToTarget = 3.0f;

        private Vector3 _cameraTarget;
        private Rigidbody _rigidbody;
        private GameObject _cameraRig;
        Vector3 _directionToTarget;

        public CameraController(Transform target, GameObject cameraRig)
        {
            _cameraRig = cameraRig;
            _rigidbody = cameraRig.GetComponent<Rigidbody>();
            _target = target;
        }

        public void Execute()
        {
            _cameraTarget = _target.position + Vector3.up * _offsetUp;
            RotateToTarget();
            MoveToTarget();
        }

        private void RotateToTarget()
        {
            Vector3 directionToTarget = _cameraTarget - _cameraRig.transform.position;
            directionToTarget.Normalize();
            directionToTarget = Vector3.RotateTowards(_cameraRig.transform.forward, directionToTarget, _cameraRotationSpeed * Time.deltaTime, Time.deltaTime);
            _cameraRig.transform.rotation = Quaternion.LookRotation(directionToTarget);
        }

        private void MoveToTarget()
        {
            _directionToTarget = _cameraTarget - _cameraRig.transform.position;
            _directionToTarget.y = 0.0f;
            _directionToTarget.Normalize();
            if ((_cameraTarget - _cameraRig.transform.position).magnitude > _distanceToTarget)
            {
                _rigidbody.MovePosition(_cameraRig.transform.position + _directionToTarget * _cameraMovingSpeed * Time.deltaTime);
            }
        }
    }
}