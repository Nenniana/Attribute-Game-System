using AttributeSystem;

public interface IKillable {
    AttributeManager AttributeManager { get; }
    bool IsDead {get;}
    void OnDeath(float health);
    void Initialize() {
        AttributeManager.GetBaseAttributeInstance(AttributeKeys.HealthBase).ValueChangedTo.AddListener(OnDeath);
    }
}