using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Text;
using System.IO;

namespace AttributeSystem {
    public class AttributeKeysAssetPostprocessor : AssetPostprocessor
    {
        private static readonly string AttributesPath = "Assets/AttributeSystem/ScriptableObjects/Attributes";
        private static readonly string OutputFile = "Assets/AttributeSystem/Scripts/Attribute/AutoGeneration/AttributeKeys.cs";

        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            var allAffectedAssets = importedAssets.Concat(deletedAssets).Concat(movedAssets).Concat(movedFromAssetPaths);

            bool scriptableObjectChanged = allAffectedAssets.Any(assetPath => IsScriptableObject(assetPath));
            bool isRelevantPath = allAffectedAssets.Any(assetPath => IsRelevantPath(assetPath));

            if (scriptableObjectChanged || isRelevantPath)
            {
                Debug.Log("ScriptableObject has changed. Updating AttributeKeys...");
                GenerateAttributeKeys();
            }
        }

        private static bool IsRelevantPath(string assetPath)
        {
            string normalizedPath = assetPath.Replace("\\", "/").Trim();
            return normalizedPath.StartsWith(AttributesPath, System.StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsScriptableObject(string assetPath)
        {
            var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            return asset is ScriptableObject;
        }

        private static void GenerateAttributeKeys()
        {
            StringBuilder builder = new StringBuilder();
            IOrderedEnumerable<string> attributes = AssetDatabase.FindAssets("t:ScriptableObject", new[] { AttributesPath })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(Path.GetFileNameWithoutExtension)
                .Distinct()
                .OrderBy(name => name);

            AddNameSpaceAndClassToBuilder(ref builder);
            AddConstAttributeStringsToBuilder(attributes, ref builder);
            AddGetAllAttributeKeysMethodToBuilder(attributes, ref builder);
            CloseNamespaceAndClassForBuilder(ref builder);

            File.WriteAllText(OutputFile, builder.ToString());
            AssetDatabase.Refresh();

            Debug.Log("AttributeKeys generated at " + OutputFile);
        }

        private static void AddNameSpaceAndClassToBuilder(ref StringBuilder builder) {
            builder.AppendLine("// !IMPORTANT: This file is auto-generated. Modifications are not saved.");
            builder.AppendLine("namespace AttributeSystem {");
            builder.AppendLine("    public static class AttributeKeys");
            builder.AppendLine("    {");
        }

        private static void AddConstAttributeStringsToBuilder(IOrderedEnumerable<string> attributes, ref StringBuilder builder) {
            foreach (var attr in attributes)
            {
                builder.AppendLine($"        public const string {attr} = \"{attr}\";");
            }
        }

        private static void AddGetAllAttributeKeysMethodToBuilder(IOrderedEnumerable<string> attributes, ref StringBuilder builder) {
            builder.AppendLine("        public static string[] GetAllAttributeKeys() {");
            builder.AppendLine("            return new string[] {");

            foreach (var attr in attributes)
            {
                builder.AppendLine($"                {attr},");
            }

            builder.AppendLine("            };");
            builder.AppendLine("        }");
        }

        private static void CloseNamespaceAndClassForBuilder(ref StringBuilder builder) {
            builder.AppendLine("    }");
            builder.AppendLine("}");
        }
    }
}