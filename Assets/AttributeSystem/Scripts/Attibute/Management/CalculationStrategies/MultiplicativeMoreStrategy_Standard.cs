using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "MultiplicativeMoreStrategy", menuName = "CalculationStrategies/MultiplicativeMore")]
    public class MultiplicativeMoreStrategy_Standard : CalculationStrategy
    {
        public override float Calculate(IEnumerable<float> values, float runningTotal, ref Dictionary<AttributeCalculationType, float> previousCalculations) {
            float calculation = values.Aggregate(1f, (acc, val) => acc * (1 + val / 100f));
            previousCalculations.Add(AttributeCalculationType, calculation);
            return runningTotal *= calculation;
        }

        public override AttributeCalculationType AttributeCalculationType { get => attributeCalculationType; }

        [SerializeField][EnumToggleButtons]
        private AttributeCalculationType attributeCalculationType = AttributeCalculationType.MultiplicativeMore;
    }
}