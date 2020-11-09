using System;
using UnityEngine;

namespace mzmeevskiy
{
    public class Finish : InteractiveObject
    {
        public event Action GameFinishedAction;

        public override void Execute()
        {
            
        }

        protected override void Interaction()
        {
            GameFinishedAction?.Invoke();
        }
    }
}