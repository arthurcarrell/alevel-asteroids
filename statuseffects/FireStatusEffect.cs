using Microsoft.Xna.Framework;

namespace alevel_asteroids;

public class FireStatusEffect : StatusEffect
{

    float cooldown = 200;
    public FireStatusEffect(float setMaxTime, LivingEntity owner) : base(owner, setMaxTime) { }

    public override void Tick(LivingEntity entity, GameTime gameTime)
    {
        cooldown -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        if (cooldown <= 0)
        {
            cooldown = 200;

            Damage damage = new Damage();
            damage.amount = owner.GetDamage() / 10;
            damage.source = owner;
            damage.type = DamageType.FIRE;
            damage.procChance = 0f;

            entity.DoDamage(damage);
        }
    }
}