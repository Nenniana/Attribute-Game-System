using System;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    public class SecondsTickable : ITickable
    {
        [SerializeField]
        private float timeRemaining;
        public bool Tick(float deltaTime)
        {
            timeRemaining -= deltaTime;
            if (timeRemaining <= 0) {
                return true;
            }
            return false;
        }
    }
}