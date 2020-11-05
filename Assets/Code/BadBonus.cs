using System;
using UnityEngine;
using static UnityEngine.Random;

namespace mzmeevskiy
{
    public class BadBonus : InteractiveObject, IFly, IFlicker
    {
        public event Action<string, Color> OnCaughtPlayerChange = delegate (string str, Color color) { };
        private float _speedRotation;
        private float _lengthFly;

        public event Action GoodBonusCaught;

        private void Awake()
        {
            _speedRotation = Range(1.0f, 5.0f);
            _lengthFly = Range(2.0f, 3.0f);
        }

        protected override void Interaction()
        {
            OnCaughtPlayerChange?.Invoke(gameObject.name, _color);
        }

        public override void Execute()
        {
            if (!IsInteractable)
            {
                return;
            }
            Fly();
            Rotate();
        }

        public void Fly()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time, _lengthFly), transform.localPosition.z);
        }

        public void Rotate()
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * _speedRotation), Space.World);
        }
    }
}