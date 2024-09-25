using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;


namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "HealingStrategy_MaxHealth", menuName = "CalculationStrategies/HealingStrategy_MaxHealth")]
    public class HealingStrategy_MaxHealth : CalculationStrategy
    {
        public override float Calculate(IEnumerable<float> values, float runningTotal, ref Dictionary<AttributeCalculationType, float> previousCalculations) {
            float calculation = values.Sum();
            
            if (previousCalculations.TryGetValue(AttributeCalculationType.Negative, out float damage)) {
                calculation = Mathf.Min(calculation, damage);
            }
            
            previousCalculations.Add(AttributeCalculationType, calculation);
            
            return runningTotal += calculation;
        }
        
        public override AttributeCalculationType AttributeCalculationType { get => attributeCalculationType; }

        [SerializeField][EnumToggleButtons]
        private AttributeCalculationType attributeCalculationType = AttributeCalculationType.Positive;
    }
}
