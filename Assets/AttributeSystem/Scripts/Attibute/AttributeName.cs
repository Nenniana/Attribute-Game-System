using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem {
    [Serializable][InlineProperty]
    public class AttributeName {
        [SerializeField][ValueDropdown("@AttributeKeys.GetAllAttributeKeys()")][HideLabel]
        private string value = AttributeKeys.GetAllAttributeKeys()[0];

        public static implicit operator string(AttributeName attributeName) => attributeName.value;
        public static implicit operator AttributeName(string attributeNameValue) => new AttributeName(attributeNameValue);

        public AttributeName (string attributeNameValue) {
            value = attributeNameValue;
        }

        public AttributeName () {
            value = AttributeKeys.GetAllAttributeKeys()[0];
        }
    }
}