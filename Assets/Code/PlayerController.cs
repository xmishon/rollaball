using UnityEngine;

namespace mzmeevskiy
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 3.0f;
        [SerializeField] private float _sensitivity = 3.0f;
        [SerializeField] private CameraController _cameraController;

        Rigidbody rb;

        private float _sqrSpeed;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (_cameraController == null)
                _cameraController = GameObject.Find("CameraRig").GetComponent<CameraController>();
            _sqrSpeed = _speed * _speed;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                //rb.velocity += new Vector3(rb.velocity. x, 3.0f, rb.velocity.z);
                rb.AddForce(Vector3.up * 4.0f, ForceMode.Impulse);
            }
        }

        private void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            var cameraDirection = _cameraController.GetCameraLookDirection();

            var speedVector = cameraDirection * moveVertical + Quaternion.AngleAxis(90.0f, Vector3.up) * cameraDirection * moveHorizontal;

            //var speedVector = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * _speed;

            speedVector.Normalize();
            speedVector *= _speed;

            speedVector.y = rb.velocity.y;

            if (rb.velocity.sqrMagnitude < _sqrSpeed)
                //rb.AddForce(speedVector * _sensitivity, ForceMode.Force);
                rb.AddForce(speedVector * _sensitivity);
        }
    }
}