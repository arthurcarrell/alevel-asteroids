namespace alevel_asteroids;

public class Attachment
{
    protected string name;
    protected int durability;
    protected LivingEntity owner;
    public Attachment()
    {

    }

    public string GetName() => name;

    public virtual void OnDamage(Damage damage) { }
    public virtual void OnHit(Damage damage) { }
    public virtual void OnShoot() {}
}