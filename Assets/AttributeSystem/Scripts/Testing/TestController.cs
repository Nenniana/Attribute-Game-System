using AttributeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestController : MonoBehaviour 
{
    [SerializeField]
    AttributePool attributePool = new AttributePool();

    [SerializeField]
    private TestTower testTower;

    
    private void Awake() {
        attributePool.AddMember(testTower.PoolMember);
    } 

    [Button]
    private void Request(AttributeRef attributeName) {
        testTower.PoolMember.GetCalculatedAttributeValue(attributeName);
    }

    [Button]
    private void Damage(float amount) {
        (testTower as IDamagable).Damage(amount);
    }
}