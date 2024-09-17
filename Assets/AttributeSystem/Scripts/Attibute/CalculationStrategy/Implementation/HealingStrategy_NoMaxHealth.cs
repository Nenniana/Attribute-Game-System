using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;


namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "HealingStrategy_NoMaxHealth", menuName = "CalculationStrategies/HealingStrategy_NoMaxHealth")]
    public class HealingStrategy_NoMaxHealth : CalculationStrategy
    {
        public override float Calculate(IEnumerable<float> values, float runningTotal, ref Dictionary<AttributeCalculationType, float> previousCalculations) {
            float calculation = values.Sum();   
            previousCalculations.Add(AttributeCalculationType, calculation);
            
            return runningTotal += calculation;
        }
        
        public override AttributeCalculationType AttributeCalculationType { get => attributeCalculationType; }

        [SerializeField][EnumToggleButtons]
        private AttributeCalculationType attributeCalculationType = AttributeCalculationType.Positive;
    }
}
