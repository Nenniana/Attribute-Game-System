using System;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    public class RoundDownStrategy : IRoundingStrategy
    {
        public float Round(float value)
        {
            return Mathf.Floor(value);
        }
    }
}