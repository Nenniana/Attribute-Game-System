using System;
using AttributeSystem;

public interface IDamagable {
    AttributeManager AttributeManager { get; }
    void Damage(float damage) {
        if (AttributeManager == null)
            return;
            
        if (damage < 0)
            throw new ArgumentException("Damage cannot be a negative number");
            
        AttributeManager.AddAttributeByName(AttributeKeys.HealthNegative, damage);
    }
}