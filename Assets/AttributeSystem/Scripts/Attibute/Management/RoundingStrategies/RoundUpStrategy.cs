using System;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    public class RoundUpStrategy : IRoundingStrategy
    {
        public float Round(float value)
        {
            return Mathf.Ceil(value);
        }
    }
}