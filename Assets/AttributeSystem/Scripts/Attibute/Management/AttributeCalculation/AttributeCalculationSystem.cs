/// <summary>
/// This code snippet represents a static class called AttributeCalculationSystem in the AttributeSystem namespace. 
/// It provides a method called CalculateAttribute that calculates the value of a main attribute based on a set of combined attributes. 
/// The method first finds the attribute set by name using the AttributeSetMaster.Instance. 
/// Then, it calculates the final value by iterating through the relevant instances in the combined attributes and applying the calculation strategies defined in the attribute set's calculation group. 
/// The final value is rounded using the rounding method defined in the main attribute. 
/// The calculated value is logged using UnityEngine.Debug.Log. 
/// </summary>
/// <param name="mainAttributeName">The name of the main attribute to calculate.</param>
/// <param name="combinedAttributes">The set of combined attributes to use in the calculation.</param>
/// <returns>The calculated value of the main attribute.</returns>
/// <remarks>
/// The CalculateAttribute method uses the AttributeSetMaster.Instance to find the attribute set by name. 
/// It then iterates through the relevant instances in the combined attributes and applies the calculation strategies defined in the attribute set's calculation group. 
/// The final value is rounded using the rounding method defined in the main attribute. 
/// The calculated value is logged using UnityEngine.Debug.Log.
/// </remarks>
/// <example>
/// The following example demonstrates how to use the CalculateAttribute method:
/// <code>
/// HashSet<AttributeInstance> combinedAttributes = new HashSet<AttributeInstance>();
/// // Add attribute instances to the combinedAttributes set
/// float calculatedValue = AttributeCalculationSystem.CalculateAttribute("MainAttribute", combinedAttributes);
/// Console.WriteLine($"Calculated value: {calculatedValue}");
/// </code>
/// </example>
/// <seealso cref="AttributeSetMaster"/>
/// <seealso cref="AttributeInstance"/>
/// <seealso cref="AttributeSet"/>
/// <seealso cref="AttributeCalculationType"/>
/// <seealso cref="AttributeDefinition"/>
/// <seealso cref="CalculationStrategy"/>
/// <seealso cref="UnityEngine.Debug.Log"/>
/// <value>The calculated value of the main attribute.</value>
/// <returns>The calculated value of the main attribute.</returns>
/// <param name="mainAttributeName">The name of the main attribute to calculate.</param>
/// <param name="combinedAttributes">The set of combined attributes to use in the calculation.</param>

using System;
using System.Collections.Generic;
using System.Linq;

namespace AttributeSystem
{
    public static class AttributeCalculationSystem
    {
        private static Dictionary<AttributeCalculationType, List<float>> InitializeValueDictionary(AttributeSet set)
        {
            var dictionary = new Dictionary<AttributeCalculationType, List<float>>();

            foreach (AttributeCalculationType type in set.calculationGroup.strategies.Select(strategy => strategy.AttributeCalculationType))
            {
                dictionary[type] = new List<float>();
            }

            return dictionary;
        }

        public static float CalculateAttribute(string mainAttributeName, HashSet<AttributeInstance> combinedAttributes)
        {
            AttributeSet set = AttributeSetMaster.FindAttributeSetByName(mainAttributeName);
            if (set == null)
            {
                return 0f;
            }

            return CalculateSetValue(combinedAttributes, set);
        }

        private static float CalculateSetValue(HashSet<AttributeInstance> attributes, AttributeSet set)
        {
            Dictionary<AttributeCalculationType, List<float>> valuesByType = InitializeValueDictionary(set);
            Dictionary<AttributeCalculationType, float> previousCalculations = new Dictionary<AttributeCalculationType, float>();
            IEnumerable<AttributeInstance> relevantInstances = FindRelevantInstances(attributes, set);

            OrderInstanceValues(valuesByType, relevantInstances);
            float finalValue = CalculateFinalSetValue(set, valuesByType, ref previousCalculations);
            float roundedValue = set.mainAttribute.ApplyRounding(finalValue);

            return roundedValue;
        }

        private static IEnumerable<AttributeInstance> FindRelevantInstances(HashSet<AttributeInstance> attributes, AttributeSet set)
        {
            return attributes.Where(instance => set.attributes.Contains(instance.definition) || set.mainAttribute == instance.definition);
        }

        private static float CalculateFinalSetValue(AttributeSet set, Dictionary<AttributeCalculationType, List<float>> valuesByType, ref Dictionary<AttributeCalculationType, float> previousCalculations)
        {
            float finalValue = 0f;
            foreach (var strategy in set.calculationGroup.strategies)
            {
                finalValue = strategy.Calculate(valuesByType[strategy.AttributeCalculationType], finalValue, ref previousCalculations);
            }

            return finalValue;
        }

        private static void OrderInstanceValues(Dictionary<AttributeCalculationType, List<float>> valuesByType, IEnumerable<AttributeInstance> relevantInstances)
        {
            foreach (var instance in relevantInstances)
            {
                valuesByType[instance.definition.calculationType].Add(instance.GetValue());
            }
        }
    }
}