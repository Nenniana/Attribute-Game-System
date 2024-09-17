using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace AttributeSystem
{
    public class AttributeSystemMainWindow : OdinMenuEditorWindow
    {
        private CreateAttributesWindow createAttributesWindow;

        [MenuItem("Attribute/Main")]
        private static void OpenWindow()
        {
            GetWindow<AttributeSystemMainWindow>().Show();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            createAttributesWindow = AssetDatabase.LoadAssetAtPath<CreateAttributesWindow>("Assets/Attribute System/ScriptableObjects/Windows/CreateAttributeWindow.asset");

            OdinMenuTree tree = new OdinMenuTree(true, GetTreeConfig()) {
                { "Create Attributes",                             createAttributesWindow,                                                   EditorIcons.PenAdd }
            };

            tree.AddAllAssetsAtPath("Enhance Attributes", "Assets/Attribute System/ScriptableObjects/Attributes/Enhance", typeof(EnhanceAttributeDefinition), true).AddThumbnailIcons().SortMenuItemsByName();
            tree.AddAllAssetsAtPath("Main Attributes", "Assets/Attribute System/ScriptableObjects/Attributes/Main", typeof(BaseAttributeDefinition), true).AddThumbnailIcons().SortMenuItemsByName();

            return tree;
        }

        // Setup Odin Menu Tree with Search and caching expandend states
        private OdinMenuTreeDrawingConfig GetTreeConfig() {
            OdinMenuTreeDrawingConfig config = new OdinMenuTreeDrawingConfig
            {
                DrawSearchToolbar = true,
                DefaultMenuStyle = OdinMenuStyle.TreeViewStyle,
                UseCachedExpandedStates = true
            };
            return config;
        }
    }
}