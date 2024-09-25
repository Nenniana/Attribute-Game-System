using System.Collections.Generic;
using UnityEngine;

namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "AttributeSet", menuName = "Attributes/AttributeSet")]
    public class AttributeSet : ScriptableObject
    {
        public BaseAttributeDefinition mainAttribute;
        public List<AttributeDefinition> attributes;
        public CalculationStrategyGroup calculationGroup;
    }
}