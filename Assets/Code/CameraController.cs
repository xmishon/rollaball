using UnityEngine;

namespace mzmeevskiy
{
    [RequireComponent(typeof(Rigidbody))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _offsetUp = 1.0f;
        [SerializeField] private float _cameraRotationSpeed = 1.0f;
        [SerializeField] private float _cameraMovingSpeed = 1.0f;
        [SerializeField] private float _distanceToTarget = 3.0f;
        [SerializeField] private Rigidbody _playerRigidbody;

        private Vector3 _cameraTarget;
        private float _sqrDistanceToTarget;
        private Rigidbody _rigidbody;
        Vector3 _directionToTarget;

        public Transform GetCameraTransform() => transform;

        public Vector3 GetCameraLookDirection() => _directionToTarget;

        private void Start()
        {
            if (_target == null)
            {
                _target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            }
            _rigidbody = GetComponent<Rigidbody>();
            _sqrDistanceToTarget = _distanceToTarget * _distanceToTarget;
        }

        private void FixedUpdate()
        {
            _cameraTarget = _target.position + Vector3.up * _offsetUp;
            RotateToTarget();
            MoveToTarget();
        }

        private void RotateToTarget()
        {
            Vector3 directionToTarget = _cameraTarget - transform.position;
            directionToTarget.Normalize();
            directionToTarget = Vector3.RotateTowards(transform.forward, directionToTarget, _cameraRotationSpeed * Time.deltaTime, Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(directionToTarget);
        }

        private void MoveToTarget()
        {
            _directionToTarget = _cameraTarget - transform.position;
            _directionToTarget.y = 0.0f;
            _directionToTarget.Normalize();
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(90.0f, Vector3.up) * _directionToTarget * 5.0f, Color.green);

            if ((_cameraTarget - transform.position).magnitude > _distanceToTarget)
            {
                //transform.position = Vector3.MoveTowards(transform.position, transform.position + _directionToTarget, _cameraMovingSpeed * Time.deltaTime); //
                //transform.position = Vector3.Lerp(transform.position, _target.position + Vector3.up * _offsetUp, _playerRigidbody.velocity.magnitude * Time.deltaTime);
                //_rigidbody.AddForce((_target.position + Vector3.up * _offsetUp - transform.position) * 5, ForceMode.Force);
                _rigidbody.MovePosition(transform.position + _directionToTarget * _cameraMovingSpeed * Time.deltaTime);
            }
        }
    }
}