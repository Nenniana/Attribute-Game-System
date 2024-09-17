using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    public class EnhanceAttributeInstance : AttributeInstance
    {
        private int StackCount { get; set; } = 0;

        [SerializeField][HideLabel][PropertyOrder(1)]
        private EnhanceAttributeDefinition enhanceAttributeDefinition;
        public override AttributeDefinition definition { get => enhanceAttributeDefinition; }

        [SerializeField][PropertyOrder(-1)][TableColumnWidth(-300)]
        public int stackLimit = int.MaxValue;

        public EnhanceAttributeInstance() : base() {}

        public EnhanceAttributeInstance(EnhanceAttributeDefinition definition, float value) : base(value) { 
            enhanceAttributeDefinition = definition;
        }

        public EnhanceAttributeInstance(EnhanceAttributeDefinition definition, float value, int stackLimit) : base(value) {
            enhanceAttributeDefinition = definition;
            this.stackLimit = stackLimit; 
        }
        
        public void AddStack() {
            StackCount++;
        }

        public override float GetValue()
        {
            if (StackCount >= stackLimit) 
                return value * stackLimit;

            return value * (StackCount + 1);
        }

        public bool RemoveStack() {
            if (StackCount > 0) {
                StackCount--;
                return true;
            }
            return false;
        }
    }
}