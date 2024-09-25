/// <summary>
/// Represents a pool member in the Attribute System.
/// </summary>
/// <remarks>
/// The PoolMember class is responsible for managing the attributes of a pool member.
/// It implements the IPoolMember interface, which defines the methods for adding and removing pool attributes,
/// as well as getting the calculated attribute value.
/// </remarks>
/// <example>
/// The following example demonstrates how to use the PoolMember class:
/// <code>
/// // Create an instance of the AttributeManager class
/// AttributeManager attributeManager = new AttributeManager();
///
/// // Create a list of base attributes
/// List<AttributeInstance> baseAttributes = new List<AttributeInstance>();
///
/// // Create a new PoolMember instance
/// PoolMember poolMember = new PoolMember(attributeManager);
/// poolMember.BaseAttributes = baseAttributes;
///
/// // Add pool attributes to the pool member
/// HashSet<AttributeInstance> poolAttributes = new HashSet<AttributeInstance>();
/// poolMember.AddPoolAttributes(poolAttributes);
///
/// // Remove pool attributes from the pool member
/// poolMember.RemovePoolAttributes(poolAttributes);
///
/// // Get the calculated attribute value
/// float calculatedValue = poolMember.GetCalculatedAttributeValue("mainAttributeName");
/// </code>
/// </example>
/// <seealso cref="IPoolMember"/>
/// <seealso cref="AttributeManager"/>
/// <seealso cref="AttributeInstance"/>

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable][HideLabel]
    public class PoolMember : IPoolMember
    {
        [SerializeField][TableList(AlwaysExpanded = true, DrawScrollView = false)][LabelText("PoolMember - Base Attributes")]
        private List<BaseAttributeInstance> inherientbaseAttributes;
        [SerializeField][TableList(AlwaysExpanded = true, DrawScrollView = false)][LabelText("PoolMember - Enhance Attributes")]
        private List<EnhanceAttributeInstance> inherientEnhanceAttributes;

        public List<BaseAttributeInstance> InherientBaseAttributes { get {return inherientbaseAttributes; } private set {inherientbaseAttributes = value;} }
        public List<EnhanceAttributeInstance> InherientEnhanceAttributes { get {return inherientEnhanceAttributes; } private set {inherientEnhanceAttributes = value;} }
        public AttributeManager AttributeManager => attributeManager;
        private AttributeManager attributeManager;

        public void InitializeAttributeManager() {
            if (attributeManager == null) {
                attributeManager = new AttributeManager();
                attributeManager.SetBaseAttributes(inherientbaseAttributes, inherientEnhanceAttributes);
            }
        }

        public PoolMember() {}

        public PoolMember(AttributeManager attributeManager) {
            this.attributeManager = attributeManager ?? throw new ArgumentNullException(nameof(attributeManager));
            this.attributeManager.SetBaseAttributes(inherientbaseAttributes, inherientEnhanceAttributes);
        }

        public float GetCalculatedAttributeValue(string mainAttributeName) 
        {
            InitializeAttributeManager();

            if (mainAttributeName == null) {
                throw new ArgumentNullException(nameof(mainAttributeName));
            }
            return attributeManager.GetCalculatedBaseAttributeValue(mainAttributeName);
        }

        public void AddAttributes(HashSet<EnhanceAttributeInstance> poolAttributes)
        {
            InitializeAttributeManager();

            if (poolAttributes == null) {
                throw new ArgumentNullException(nameof(poolAttributes));
            }
            attributeManager.AddAttributes(poolAttributes);
        }

        public void RemoveAttributes(HashSet<EnhanceAttributeInstance> poolAttributes)
        {
            InitializeAttributeManager();

            if (poolAttributes == null) {
                throw new ArgumentNullException(nameof(poolAttributes));
            }
            attributeManager.RemoveAttributes(poolAttributes);
        }
    }
}