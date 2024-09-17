using System;
using AttributeSystem;
using Player;
using UnityEngine;

public abstract class BasePlayer : PoolMember, IPlayer
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string id;
    [SerializeField]
    private IScoreHolderStrategy scoreStrategy;
    [SerializeField]
    private bool isDead = false;

    public string Name { get => name; private set => name = value; }
    public string Id { get => id; private set => id = value; }
    public IScoreHolderStrategy ScoreStrategy { get => scoreStrategy; private set => scoreStrategy = value; }
    public bool IsDead { get => isDead; private set => isDead = value; }

    void IKillable.OnDeath(float health)
    {
        if (health > 0)
            return;
            
        IsDead = true;

        if (IsDead) {
            Debug.Log("Player died.");
        }
    }

    public BasePlayer (string name, IScoreHolderStrategy scoreStrategy) 
    {
        Name = name;
        ScoreStrategy = scoreStrategy;
        CalculateNewID();
        (this as IKillable).Initialize();
    }

    public void CalculateNewID() {
        Id = Guid.NewGuid().ToString();
    }
}