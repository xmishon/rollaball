using UnityEngine;

namespace mzmeevskiy
{
    public abstract class PlayerBase : MonoBehaviour
    {
        public float Speed = 3.0f;
        public float Sensitivity = 6.0f;
        public float JumpStrength = 1.0f;

        public abstract void Move(Vector3 moveDirection);
        public abstract void Jump();
    }
}