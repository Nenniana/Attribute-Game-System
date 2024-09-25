using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "MainAttributeDefinition", menuName = "Attributes/MainAttributeDefinition")]
    public class BaseAttributeDefinition : AttributeDefinition
    {
        [SerializeReference]
        public IRoundingStrategy roundingStrategy = new RoundUpStrategy();

        [SerializeField]
        private bool isAllowedDirty = true;

        public bool IsAllowedDirty { get => isAllowedDirty; private set => isAllowedDirty = value; }

        public void SetRoundingStrategy(IRoundingStrategy newStrategy)
        {
            roundingStrategy = newStrategy; 
        }

        public float ApplyRounding(float value)
        {
            if (roundingStrategy != null)
            {
                return roundingStrategy.Round(value);
            }
            return value; 
        }
    }
}