namespace alevel_asteroids;

public enum DamageType
{
    NORMAL,
    CRIT
}

public struct Damage
{
    public int amount;
    public DamageType type;
    public LivingEntity source;
}