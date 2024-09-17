/* 
The code snippet is a part of a class called CreateAttributesWindow which is a subclass of ScriptableObject. 
It contains methods and fields related to creating and managing attribute definitions and sets.

-Inputs
options: a list of AttributeTypeSelectionStruct objects that represent the different attribute types to create.
attributeName: a string representing the name of the new attribute to create.
createAndAddToSet: a boolean indicating whether to create a new attribute set and add the created attributes to it.
calculationGroup: a CalculationStrategyGroup object representing the calculation group to assign to the new attribute set.

-Flow
1. The CreateAttributes method is called when the "Create Attribute(s)" button is clicked.
2. It iterates over each AttributeTypeSelectionStruct object in the options list.
3. If the CreateType property of the current AttributeTypeSelectionStruct object is set to Off, it skips to the next iteration.
4. If the Type property of the current AttributeTypeSelectionStruct object is Base, it creates a new attribute definition using the CreateAttribute method and assigns it to the mainAttributeDefinition variable.
5. Otherwise, it creates a new attribute definition using the CreateAttribute method and adds it to the createdAttributes list.
6. If there are any created attributes and the createAndAddToSet flag is set to true, it calls the CreateNewSet method to create a new attribute set and assigns the created attributes and main attribute definition to it.
7. It saves the assets and refreshes the asset database.

- Outputs
The code creates attribute definitions and optionally a new attribute set. 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "CreateAttributeWindow", menuName = "Attributes/Windows/CreateAttributeWindow", order = 1)]
    public class CreateAttributesWindow : ScriptableObject
    {
        private const string attributeBasePath = "Assets/Attribute System/ScriptableObjects/Attributes/";
        private const string setBasePath = "Assets/Attribute System/ScriptableObjects/AttributeSets/";

        [SerializeField]
        [TableList(AlwaysExpanded = true, DrawScrollView = false)]
        [LabelText("Choose which Attributes to create:")]
        private List<AttributeTypeSelectionStruct> options;

        [SerializeField]
        [LabelText("Name of new Attribute")]
        [OnInspectorInit("OnInspectorInit")]
        [PropertyOrder(-1)]
        private string attributeName;

        [SerializeField]
        [ShowIf("createAndAddToSet")]
        private CalculationStrategyGroup calculationGroup;

        [SerializeField]
        [HideInInspector]
        private bool createAndAddToSet = false;

        [ResponsiveButtonGroup("Set"), GUIColor("$setColorInvoking")]
        private void ToggleCreateNewSet()
        {
            createAndAddToSet = !createAndAddToSet;
            ToggleColor(ref setColorInvoking, createAndAddToSet);
        }

        [Button("Create Attribute(s)", ButtonSizes.Large)]
        [GUIColor("#26FFB9")]
        [EnableIf("CheckCreationCriteria")]
        [PropertyOrder(0)]
        private void CreateAttributes()
        {
            List<AttributeDefinition> createdAttributes = new List<AttributeDefinition>();
            AttributeDefinition mainAttributeDefinition = null;

            foreach (AttributeTypeSelectionStruct option in options)
            {
                if (option.CreateType == ToggleButtonEnum.Off)
                    continue;

                if (option.Type == AttributeCalculationType.Base)
                {
                    mainAttributeDefinition = CreateAttribute(attributeName, option.TypeName, option.Type);
                    continue;
                }

                AttributeDefinition newAttribute = CreateAttribute(attributeName, option.TypeName, option.Type);

                if (newAttribute != null)
                    createdAttributes.Add(newAttribute);
            }

            if (createdAttributes.Any() && createAndAddToSet)
            {
                CreateNewSet(createdAttributes, mainAttributeDefinition, attributeName);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void CreateNewSet(List<AttributeDefinition> createdAttributes, AttributeDefinition mainAttribute, string baseName)
        {
            string path = Path.Combine(setBasePath + baseName + "Set.asset");

            if (AssetDatabase.LoadAssetAtPath<AttributeSet>(path) != null)
            {
                Debug.LogWarning($"Set '{path}' already exists.");
                return;
            }

            AttributeSet newSet = CreateInstance<AttributeSet>();

            if (calculationGroup != null)
                newSet.calculationGroup = calculationGroup;

            if (mainAttribute != null)
                newSet.mainAttribute = mainAttribute as BaseAttributeDefinition;

            newSet.attributes = createdAttributes.ToList();

            AssetDatabase.CreateAsset(newSet, path);
        }

        private AttributeDefinition CreateAttribute(string baseName, string addedName, AttributeCalculationType type)
        {
            AttributeDefinition attribute;
            string path = attributeBasePath;

            if (type == AttributeCalculationType.Base)
            {
                path = Path.Combine(path, "Main/");
                attribute = CreateInstance<BaseAttributeDefinition>();
            }
            else
            {
                path = Path.Combine(path, "Enhance/");
                attribute = CreateInstance<EnhanceAttributeDefinition>();
            }

            path = Path.Combine(path, baseName + addedName + ".asset");

            if (AssetDatabase.LoadAssetAtPath<AttributeDefinition>(path) != null)
            {
                Debug.LogWarning($"Attribute '{path}' already exists.");
                return null;
            }

            attribute.calculationType = type;

            AssetDatabase.CreateAsset(attribute, path);

            return attribute;
        }

        private bool CheckCreationCriteria()
        {
            if (string.IsNullOrEmpty(attributeName))
                return false;

            if (attributeName.Any(char.IsWhiteSpace))
                return false;

            if (options.Any(option => option.CreateType == ToggleButtonEnum.On))
                return true;

            return false;
        }

        private void ToggleColor(ref Color colorToToggle, bool state)
        {
            if (state)
                colorToToggle = new Color(0, 0.8f, 0);
            else
                colorToToggle = new Color32(243, 109, 134, 255);
        }

        private void OnInspectorInit()
        {
            ToggleColor(ref setColorInvoking, createAndAddToSet);
            ConstructBasicOptions();
        }

        private void ConstructBasicOptions()
        {
            if (options == null)
                options = new List<AttributeTypeSelectionStruct>();

            foreach (AttributeCalculationType type in Enum.GetValues(typeof(AttributeCalculationType)))
            {
                if (options.Any(option => option.Type == type))
                    continue;

                options.Add(new AttributeTypeSelectionStruct(type, type.ToString()));
            }
        }

        #region Private Fields

        #pragma warning disable 0414
        private Color setColorInvoking = new Color(0, 0.8f, 0);
        #pragma warning restore 0414

        #endregion
    }
}