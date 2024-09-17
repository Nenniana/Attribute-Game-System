using System;
using AttributeSystem;
using UnityEngine;

public class TestAttributeKeyList : MonoBehaviour
{
    public AttributeName attributeName;

    AttributeManager AttributeManager { get; }

    private void AddToAttribute(int value) {
        attributeName = AttributeKeys.CardDrawAdditive;
        AttributeManager.AddAttributeByName(attributeName, value);
    }
}