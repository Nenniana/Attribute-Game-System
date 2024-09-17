using System;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    public class RoundNearestStrategy : IRoundingStrategy
    {
        public float Round(float value)
        {
            return Mathf.Round(value);
        }
    }
}