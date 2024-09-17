using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem
{
    public abstract class AttributeDefinition : ScriptableObject
    {
        [EnumToggleButtons]
        public AttributeCalculationType calculationType;
    }
}