using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem {
    [Serializable][InlineProperty]
    public class Attribute {
        [SerializeField][ValueDropdown("@AttributeKeys.GetAllAttributeKeys()")][HideLabel]
        private string value = AttributeKeys.GetAllAttributeKeys()[0];

        public static implicit operator string(Attribute attributeName) => attributeName.value;
        public static implicit operator Attribute(string attributeNameValue) => new Attribute(attributeNameValue);

        public Attribute (string attributeNameValue) {
            value = attributeNameValue;
        }

        public Attribute () {
            value = AttributeKeys.GetAllAttributeKeys()[0];
        }
    }
}