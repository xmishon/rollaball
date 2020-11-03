using System;
using UnityEngine;

namespace mzmeevskiy
{
    public class Finish : InteractiveObject
    {
        public event Action GameFinishedAction;

        protected override void Interaction()
        {
            GameFinishedAction?.Invoke();
        }
    }
}