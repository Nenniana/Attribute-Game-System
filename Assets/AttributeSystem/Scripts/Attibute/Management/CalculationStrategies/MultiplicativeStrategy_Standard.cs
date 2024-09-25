using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "MultiplicativeStrategy", menuName = "CalculationStrategies/Multiplicative")]
    public class MultiplicativeStrategy_Standard : CalculationStrategy
    {
        public override float Calculate(IEnumerable<float> values, float runningTotal, ref Dictionary<AttributeCalculationType, float> previousCalculations) {
            float calculation = 1 + (0.01f * values.Sum());
            previousCalculations.Add(AttributeCalculationType, calculation);
            return runningTotal *= calculation;
        }

        public override AttributeCalculationType AttributeCalculationType { get => attributeCalculationType; }

        [SerializeField][EnumToggleButtons]
        private AttributeCalculationType attributeCalculationType = AttributeCalculationType.Multiplicative;
    }
}