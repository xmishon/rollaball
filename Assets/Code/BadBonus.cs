using System;
using UnityEngine;
using static UnityEngine.Random;

namespace mzmeevskiy
{
    public class BadBonus : InteractiveObject, IFly, IFlicker
    {
        public event Action<string, string> OnCaughtPlayerChange = delegate (string name, string color) { };
        private float _speedRotation;
        private float _lengthFly;

        private CustomColors.CustomColor customColor;

        private void Awake()
        {
            _speedRotation = Range(1.0f, 5.0f);
            _lengthFly = Range(1.0f, 2.0f);

            customColor = CustomColors.GetRandomColor();
            GetComponent<Renderer>().material.color = CustomColors.GetColor(customColor);
        }

        protected override void Interaction()
        {
            OnCaughtPlayerChange?.Invoke(gameObject.name, customColor.ToString());
        }

        public override void Execute()
        {
            if (!IsInteractable)
            {
                return;
            }
            Fly();
            Flicker();
        }

        public void Fly()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time, _lengthFly), transform.localPosition.z);
        }

        public void Flicker()
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * _speedRotation), Space.World);
        }
    }
}