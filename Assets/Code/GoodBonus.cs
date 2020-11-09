using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace mzmeevskiy
{
    public class GoodBonus : InteractiveObject, IFly, IFlicker
    {
        public int Point = 1;
        public event Action<int> OnPointChange = delegate (int i) { };
        private Material _material;
        private float _lengthFly;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _lengthFly = Random.Range(1.0f, 2.0f);
        }

        protected override void Interaction()
        {
            OnPointChange?.Invoke(Point);
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
            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, Mathf.PingPong(Time.time, 1.0f));
        }
    }
}