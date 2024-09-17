
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities.Editor;

namespace AttributeSystem {
    public static class AttributeDefinitionHelper {
        private static Dictionary<string, BaseAttributeDefinition> baseAttributeDefinitionsByName;
        private static Dictionary<string, EnhanceAttributeDefinition> enhanceAttributeDefinitionsByName;

        private static void BuildEnhanceAttributeDefinitionCache()
        {
            if (enhanceAttributeDefinitionsByName != null)
                return;

            List<EnhanceAttributeDefinition> allAttributeDefinitions = AssetUtilities.GetAllAssetsOfType<EnhanceAttributeDefinition>().ToList();
            enhanceAttributeDefinitionsByName = new Dictionary<string, EnhanceAttributeDefinition>();

            foreach (var attributeDefinition in allAttributeDefinitions)
            {
                if (attributeDefinition != null)
                {
                    if (!enhanceAttributeDefinitionsByName.ContainsKey(attributeDefinition.name))
                    {
                        enhanceAttributeDefinitionsByName[attributeDefinition.name] = attributeDefinition;
                    }
                }
            }
        }

        public static EnhanceAttributeDefinition GetEnhanceAttributeDefinitionByName(string name)
        {
            BuildEnhanceAttributeDefinitionCache();

            return enhanceAttributeDefinitionsByName.TryGetValue(name, out var attributeDefinition) ? attributeDefinition : null;
        }

        private static void BuildBaseAttributeDefinitionCache()
        {
            if (baseAttributeDefinitionsByName != null)
                return;

            List<BaseAttributeDefinition> allAttributeDefinitions = AssetUtilities.GetAllAssetsOfType<BaseAttributeDefinition>().ToList();
            baseAttributeDefinitionsByName = new Dictionary<string, BaseAttributeDefinition>();

            foreach (var attributeDefinition in allAttributeDefinitions)
            {
                if (attributeDefinition != null)
                {
                    if (!baseAttributeDefinitionsByName.ContainsKey(attributeDefinition.name))
                    {
                        baseAttributeDefinitionsByName[attributeDefinition.name] = attributeDefinition;
                    }
                }
            }
        }

        public static BaseAttributeDefinition GetBaseAttributeDefinitionByName(string name)
        {
            BuildBaseAttributeDefinitionCache();

            return baseAttributeDefinitionsByName.TryGetValue(name, out var attributeDefinition) ? attributeDefinition : null;
        }
    }
}