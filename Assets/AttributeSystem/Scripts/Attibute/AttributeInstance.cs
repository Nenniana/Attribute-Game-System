/// <summary>
/// Represents an instance of an attribute.
/// </summary>
/// <remarks>
/// An attribute instance contains the unique identifier, the attribute definition, and the attribute value.
/// </remarks>
/// <example>
/// <code>
/// AttributeInstance instance = new AttributeInstance();
/// instance.definition = new AttributeDefinition("Health", AttributeType.Float);
/// instance.value = 100f;
/// </code>
/// </example>
/// <seealso cref="AttributeDefinition"/>
/// <seealso cref="AttributeType"/>
/// <seealso cref="AttributePool"/>
/// <seealso cref="AttributeManager"/>

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    public abstract class AttributeInstance
    {
        internal string Id;

        [SerializeField][HideLabel][PropertyOrder(0)][TableColumnWidth(-300)]
        protected float value;

        [HideLabel][PropertyOrder(1)]
        public abstract AttributeDefinition definition {get;}

        public AttributeInstance() {
            CalculateNewID();
        }

        public AttributeInstance(float value)
        {
            CalculateNewID();
            this.value = value;
        }

        public void CalculateNewID() {
            Id = Guid.NewGuid().ToString();
        }

        public abstract float GetValue();
    }
}