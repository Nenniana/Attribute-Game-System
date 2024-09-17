using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace AttributeSystem
{
    [Serializable]
    public class BaseAttributeInstance : AttributeInstance
    {
        [HideInInspector]
        public UnityEvent<float, float, float> ValueChangedToFromDiff = new UnityEvent<float, float, float>();
        [HideInInspector]
        public UnityEvent<float, float> ValueChangedToFrom = new UnityEvent<float, float>();
        [HideInInspector]
        public UnityEvent<float> ValueChangedTo = new UnityEvent<float>();
        [HideInInspector]
        internal bool isDirty = false;
        
        [SerializeField][HideLabel][PropertyOrder(1)]
        private BaseAttributeDefinition baseAttributeDefinition;
        public override AttributeDefinition definition { get => baseAttributeDefinition; }

        public BaseAttributeInstance(float value, bool isDirty) : base () {
            this.value = value;
            this.isDirty = isDirty;
        }

        public override float GetValue()
        {
            return value;
        }

        public void SetValue(float value) {
            ValueChangedToFromDiff?.Invoke(value, base.value, base.value - value);
            ValueChangedToFrom?.Invoke(value, base.value);
            ValueChangedTo?.Invoke(value);
            base.value = value;
        }
    }
}