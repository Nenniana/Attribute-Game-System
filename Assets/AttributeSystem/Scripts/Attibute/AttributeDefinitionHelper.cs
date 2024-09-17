
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities.Editor;

namespace AttributeSystem {
    public static class AttributeDefinitionHelper {
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
    }
}