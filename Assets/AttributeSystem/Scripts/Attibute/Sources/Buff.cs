/// <summary>
/// This code snippet represents a class called Buff in the AttributeSystem namespace. 
/// It is a ScriptableObject that implements the IAttributeSource interface. 
/// The Buff class has a serialized field called attributes, which is a list of AttributeInstance objects. 
/// The SourceAttributes property returns the attributes list. 
/// </summary>

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "Buff", menuName = "AttributeSources/Buff")]
    public class Buff : ScriptableObject, IAttributeSource
    {
        [SerializeField][TableList(AlwaysExpanded = true, DrawScrollView = false)]
        private List<EnhanceAttributeInstance> attributes;
        public List<EnhanceAttributeInstance> SourceAttributes => attributes;
    }
}