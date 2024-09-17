using AttributeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestTower : MonoBehaviour, IDamagable, IKillable
{
    [SerializeField]
    public PoolMember PoolMember;
    public AttributeManager AttributeManager => PoolMember.AttributeManager;
    public bool IsDead { get => isDead; set => isDead = value; }
    private bool isDead = false;

    private void Start() {
        (this as IKillable).Initialize();
    }

    void IKillable.OnDeath(float health)
    {
        if (health > 0)
            return;
            
        IsDead = true;

        if (IsDead) {
            Debug.Log("Tower died.");
        }
    }
}