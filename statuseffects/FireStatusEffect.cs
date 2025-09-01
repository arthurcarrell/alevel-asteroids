namespace alevel_asteroids;

public class FireStatusEffect : StatusEffect
{
    public FireStatusEffect(float setTimeLeft, LivingEntity owner) : base(owner, setTimeLeft, 3.0f) { }

    public override void Tick(LivingEntity entity)
    {
        Damage damage = new Damage();
        damage.amount = owner.GetDamage() / 10;
        damage.source = owner;
        damage.type = DamageType.FIRE;
        damage.procChance = 0f;
        
        entity.DoDamage(damage);
    }
}