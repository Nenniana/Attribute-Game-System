using System.Collections.Generic;
using UnityEngine;

namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "TimedBuff", menuName = "AttributeSources/TimedBuff")]
    public class TimedBuff : Buff
    {
        [SerializeField]
        private float ticksBeforeDone;
    }
}