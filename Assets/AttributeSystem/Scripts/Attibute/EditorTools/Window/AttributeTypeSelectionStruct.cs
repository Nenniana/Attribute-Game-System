using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    public struct AttributeTypeSelectionStruct
    {
        [SerializeField]
        AttributeCalculationType type;
        [SerializeField]
        string typeName;
        [SerializeField][EnumToggleButtons]
        ToggleButtonEnum createType;

        public AttributeCalculationType Type { get => type; set => type = value; }
        public ToggleButtonEnum CreateType { get => createType; set => createType = value; }
        public string TypeName { get => typeName; set => typeName = value; }

        public AttributeTypeSelectionStruct(AttributeCalculationType type, string typeName) {
            this.type = type;
            this.typeName = typeName;
            createType = ToggleButtonEnum.Off;
        }
    }

    public enum ToggleButtonEnum {
        On,
        Off
    }
}