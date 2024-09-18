using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem {
    [Serializable][InlineProperty]
    public class AttributeRef {
        [SerializeField][ValueDropdown("@AttributeKeys.GetAllAttributeKeys()")][HideLabel]
        private string value = AttributeKeys.GetAllAttributeKeys()[0];

        public static implicit operator string(AttributeRef attributeName) => attributeName.value;
        public static implicit operator AttributeRef(string attributeNameValue) => new AttributeRef(attributeNameValue);

        public AttributeRef (string attributeNameValue) {
            value = attributeNameValue;
        }

        public AttributeRef () {
            value = AttributeKeys.GetAllAttributeKeys()[0];
        }
    }
}