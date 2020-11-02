using UnityEngine;

namespace mzmeevskiy
{
    public class GoodBonus : InteractiveObject, IFly, IFlicker
    {
        private Material _material;
        private float _lengthFly;
        private DisplayBonuses _displayBonuses;

        private static int _totalBonuses = 0;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _lengthFly = Random.Range(2.0f, 4.0f);
            _displayBonuses = new DisplayBonuses();
        }

        protected override void Interaction()
        {
            _totalBonuses += 1;
            _displayBonuses.Display(_totalBonuses);
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