using System;
using AttributeSystem;

public interface IHealable {
    AttributeManager AttributeManager { get; }
    virtual void Heal(float healing) {
        if (AttributeManager == null)
            return;
            
        if (healing < 0)
            throw new ArgumentException("Healing cannot be a negative number");

        AttributeManager.AddAttributeByName(AttributeKeys.HealthPositive, healing);
    }
}