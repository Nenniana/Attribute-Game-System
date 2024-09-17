/// <summary>
/// The AttributeManager class is responsible for managing attributes in the AttributeSystem.
/// It keeps track of the base attributes, pool attributes, calculated attributes, and dirty attributes.
/// The AttributeManager allows for adding and removing pool attributes, calculating attribute values, and marking attributes as dirty.
/// </summary>
/// <remarks>
/// The AttributeManager class has the following key features:
/// - SetBaseAttributes: Sets the base attributes for the AttributeManager.
/// - AddPoolAttributes: Adds pool attributes to the AttributeManager.
/// - RemovePoolAttributes: Removes pool attributes from the AttributeManager.
/// - GetCalculatedAttributeValue: Gets calculated value of a specific attribute.
/// - MarkAttributeAsDirty: Marks an attribute as dirty, indicating that it needs to be recalculated.
/// </remarks>
/// <example>
/// The following example demonstrates how to use the AttributeManager class:
/// <code>
/// // Create an instance of the AttributeManager
/// AttributeManager attributeManager = new AttributeManager();
///
/// // Set the base attributes
/// attributeManager.SetBaseAttributes(baseAttributes);
///
/// // Add pool attributes
/// attributeManager.AddPoolAttributes(poolAttributes);
///
/// // Get the value of a specific attribute
/// float attributeValue = attributeManager.GetCalculatedAttributeValue("attributeName");
///
/// // Mark an attribute as dirty
/// attributeManager.MarkAttributeAsDirty("attributeName");
/// </code>
/// </example>
/// <seealso cref="AttributeInstance"/>
/// <seealso cref="AttributeCalculationSystem"/>
/// <seealso cref="AttributeSetMaster"/>

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AttributeSystem
{
    public class AttributeManager
    {
        private HashSet<AttributeInstance> inherientAttributes;
        private HashSet<AttributeInstance> combinedAttributes;
        private Dictionary<string, EnhanceAttributeInstance> poolAttributes = new Dictionary<string, EnhanceAttributeInstance>();
        private Dictionary<string, BaseAttributeInstance> baseAttributes = new Dictionary<string, BaseAttributeInstance>();

        internal void SetBaseAttributes(List<BaseAttributeInstance> inherientBaseAttributes, List<EnhanceAttributeInstance> inherientEnhanceAttributes)
        {
            if (inherientBaseAttributes == null && inherientEnhanceAttributes == null) 
                return;
            
            if (inherientBaseAttributes != null && inherientEnhanceAttributes == null) {
                inherientAttributes = inherientBaseAttributes.ToHashSet<AttributeInstance>();
                return;
            }

            if (inherientEnhanceAttributes != null && inherientBaseAttributes == null) {
                inherientAttributes = inherientEnhanceAttributes.ToHashSet<AttributeInstance>();
                return;
            }

            inherientAttributes = inherientBaseAttributes.ToHashSet<AttributeInstance>();
            inherientAttributes.UnionWith(inherientEnhanceAttributes.ToHashSet<AttributeInstance>());
        }

        public void AddAttributes(HashSet<EnhanceAttributeInstance> updatedPoolAttributes)
        {
            foreach (var updatedAttribute in updatedPoolAttributes)
            {
                if (poolAttributes.ContainsKey(updatedAttribute.Id))
                {
                    poolAttributes[updatedAttribute.Id].AddStack();
                }
                else
                {
                    poolAttributes[updatedAttribute.Id] = updatedAttribute;
                }
                UnityEngine.Debug.Log($"{updatedAttribute.definition.name} with {updatedAttribute.GetValue()} was added to the AttributeManager.");
            }

            MarkAttributesAsDirty(updatedPoolAttributes);
        }

        public void AddAttributeByName(string attributeName, float value)
        {
            var attributeDefinition = AttributeDefinitionHelper.GetEnhanceAttributeDefinitionByName(attributeName);
            if (attributeDefinition != null)
            {
                AddAttributes(new HashSet<EnhanceAttributeInstance> { new EnhanceAttributeInstance(attributeDefinition, value) });
            }
        }

        public void RemoveAttributes(HashSet<EnhanceAttributeInstance> updatedPoolAttributes)
        {
            foreach (var updatedAttribute in updatedPoolAttributes)
            {
                if (!poolAttributes[updatedAttribute.Id].RemoveStack())
                {
                    poolAttributes.Remove(updatedAttribute.Id);
                    // UnityEngine.Debug.Log($"{updatedAttribute.definition.name} with {updatedAttribute.value} was removed to the AttributeManager.");
                }
            }
            MarkAttributesAsDirty(updatedPoolAttributes);
        }

        private void MarkAttributesAsDirty(HashSet<EnhanceAttributeInstance> updatedPoolAttributes)
        {
            foreach (BaseAttributeDefinition mainAttribute in AttributeSetMaster.FindMainAttributeNamesForRelevantSets(updatedPoolAttributes))
            {
                if (mainAttribute.IsAllowedDirty)
                    MarkAttributeAsDirty(mainAttribute.name);
                else
                    CalculateAttributeValue(mainAttribute.name);
            }
        }

        public float GetCalculatedBaseAttributeValue(string mainAttributeName)
        {
            if (baseAttributes.TryGetValue(mainAttributeName, out BaseAttributeInstance cachedMainAttributeInstances) && !cachedMainAttributeInstances.isDirty)
            {
                UnityEngine.Debug.Log($"Cached value for {mainAttributeName} is {cachedMainAttributeInstances.GetValue()}.");
                return cachedMainAttributeInstances.GetValue();
            }

            return CalculateAttributeValue(mainAttributeName).GetValue();
        }

        public BaseAttributeInstance GetBaseAttributeInstance(string mainAttributeName)
        {
            if (baseAttributes.TryGetValue(mainAttributeName, out BaseAttributeInstance cachedMainAttributeInstances) && !cachedMainAttributeInstances.isDirty)
            {
                return cachedMainAttributeInstances;
            }
            
            return CalculateAttributeValue(mainAttributeName);
        }

        private BaseAttributeInstance CalculateAttributeValue(string mainAttributeName) {
            combinedAttributes = new HashSet<AttributeInstance>(inherientAttributes);
            combinedAttributes.UnionWith(poolAttributes.Values);

            float setValue = AttributeCalculationSystem.CalculateAttribute(mainAttributeName, combinedAttributes);
            BaseAttributeInstance baseAttributeInstance = AddToBaseAttributes(mainAttributeName, setValue, false);

            UnityEngine.Debug.Log($"Calculated value for {mainAttributeName} is {setValue}.");

            return baseAttributeInstance;
        }

        private BaseAttributeInstance AddToBaseAttributes(string mainAttributeName, float value, bool dirty) {
            if (baseAttributes.TryGetValue(mainAttributeName, out BaseAttributeInstance cachedMainAttributeInstances)) {
                cachedMainAttributeInstances.SetValue(value);
                cachedMainAttributeInstances.isDirty = dirty;
                return cachedMainAttributeInstances;
            } else {
                BaseAttributeInstance newBaseAttributeInstance = new BaseAttributeInstance(value, dirty);
                baseAttributes.Add(mainAttributeName, newBaseAttributeInstance);
                return newBaseAttributeInstance;
            }
        }

        public void MarkAttributeAsDirty(string attributeName)
        {
            AddToBaseAttributes(attributeName, 0, true);
        }
    }
}