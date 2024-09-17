/// <summary>
/// This class represents the master controller for attribute sets in the AttributeSystem namespace.
/// It manages the caching and retrieval of attribute sets based on attribute definitions and main attribute names.
/// </summary>
/// <remarks>
/// The AttributeSetMaster class is responsible for building and maintaining caches of attribute sets.
/// It provides methods for finding attribute sets containing a specific attribute, finding main attribute names for relevant sets,
/// and retrieving attribute sets by name.
/// </remarks>
/// <example>
/// The following example demonstrates how to use the AttributeSetMaster class:
/// <code>
/// // Get the instance of the AttributeSetMaster
/// AttributeSetMaster attributeSetMaster = AttributeSetMaster.Instance;
///
/// // Find attribute sets containing a specific attribute
/// AttributeDefinition attribute = new AttributeDefinition();
/// HashSet<AttributeSet> attributeSets = attributeSetMaster.FindAttributeSetsContaining(attribute);
///
/// // Find main attribute names for relevant attribute instances
/// HashSet<AttributeInstance> attributeInstances = new HashSet<AttributeInstance>();
/// HashSet<string> mainAttributeNames = attributeSetMaster.FindMainAttributeNamesForRelevantSets(attributeInstances);
///
/// // Retrieve an attribute set by name
/// string mainAttributeName = "MainAttribute";
/// AttributeSet attributeSet = attributeSetMaster.FindAttributeSetByName(mainAttributeName);
/// </code>
/// </example>

using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities.Editor;
using UnityEngine;
namespace AttributeSystem
{
    public static class AttributeSetMaster
    {
        private static Dictionary<AttributeDefinition, List<AttributeSet>> attributeSetCache = new Dictionary<AttributeDefinition, List<AttributeSet>>();
        private static Dictionary<string, AttributeSet> mainAttributeSetCache = new Dictionary<string, AttributeSet>();
        private static List<AttributeSet> allAttributeSets;

        private static void BuildMainAttributeSetCache()
        {
            if (allAttributeSets != null)
                return;

            allAttributeSets = AssetUtilities.GetAllAssetsOfType<AttributeSet>().ToList();

            foreach (var set in allAttributeSets)
            {
                if (set.mainAttribute != null)
                {
                    if (!mainAttributeSetCache.ContainsKey(set.mainAttribute.name))
                    {
                        mainAttributeSetCache[set.mainAttribute.name] = set;
                    }
                    else
                    {
                        Debug.LogError($"Multiple sets with the same main attribute name: '{set.mainAttribute.name}'");
                    }
                }
            }
        }

        internal static HashSet<AttributeSet> FindAttributeSetsContaining(AttributeDefinition attribute)
        {
            BuildMainAttributeSetCache();

            if (attributeSetCache.TryGetValue(attribute, out var cachedSets))
            {
                return new HashSet<AttributeSet>(cachedSets);
            }

            HashSet<AttributeSet> relevantSets = new HashSet<AttributeSet>();
            foreach (var set in allAttributeSets)
            {
                if (set.attributes.Contains(attribute))
                {
                    relevantSets.Add(set);
                }
            }

            attributeSetCache[attribute] = new List<AttributeSet>(relevantSets);
            return relevantSets;
        }

        internal static HashSet<BaseAttributeDefinition> FindMainAttributeNamesForRelevantSets(HashSet<EnhanceAttributeInstance> attributeInstances)
        {
            HashSet<BaseAttributeDefinition> mainAttributeNames = new HashSet<BaseAttributeDefinition>();
            List<AttributeSet> relevantSets = new List<AttributeSet>();

            if (attributeInstances == null)
            {
                throw new ArgumentNullException(nameof(attributeInstances));
            }

            foreach (AttributeInstance attributeInstance in attributeInstances)
            {
                relevantSets.AddRange(FindAttributeSetsContaining(attributeInstance.definition));
            }

            if (!relevantSets.Any())
            {
                return mainAttributeNames;
            }

            foreach (AttributeSet attributeSet in relevantSets)
            {
                mainAttributeNames.Add(attributeSet.mainAttribute);
            }

            return mainAttributeNames;
        }

        internal static AttributeSet FindAttributeSetByName(string mainAttributeName)
        {
            BuildMainAttributeSetCache();

            if (mainAttributeSetCache.TryGetValue(mainAttributeName, out var attributeSet))
            {
                return attributeSet;
            }

            Debug.LogError($"Attribute set with name: '{mainAttributeName}' was not found!");

            return null;
        }
    }
}