using System.Collections.Generic;

namespace AttributeSystem
{
    public interface ICalculationStrategy
    {
        float Calculate(IEnumerable<float> values, float runningTotal, ref Dictionary<AttributeCalculationType, float> previousCalculations);
    }
}