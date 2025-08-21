using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;


public class LivingEntity : ColliderEntity
{
    protected int health = 0;
    protected int maxHealth = 0;
    protected int damage = 0;
    protected int critChance = 0;
    protected int experienceValue = 0;
    protected LivingEntity lastDamager;
    public LivingEntity(Texture2D setTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
    }

    // Getters
    public int GetMaxHealth() => maxHealth;
    public int GetHealth() => health;
    public int GetDamage() => damage;
    public int GetCritChance() => critChance;

    // Setters
    public void SetMaxHealth(int value) => maxHealth = value;
    public void SetHealth(int value) => health = value;
    public void SetDamage(int value) => damage = value;
    public void SetCritChance(int value) => critChance = value;

    public Damage CalculateDamage(LivingEntity source = null)
    {
        Damage endDamage = new Damage();

        // regular base
        endDamage.amount = damage;
        endDamage.type = DamageType.NORMAL;
        if (endDamage.source == null)
        {
            endDamage.source = this;
        }

        // calculate crit
        Random random = new Random();
        if (random.Next(1, 100 + 1) <= critChance)
        {
            endDamage.amount *= 2;
            endDamage.type = DamageType.CRIT;
        }

        return endDamage;
    }

    public void DoDamage(Damage damage, bool keepQuiet = false)
    {
        // run custom function
        damage = OnDamage(damage);

        health -= damage.amount;

        if (damage.source != null)
        {
            lastDamager = damage.source;
        }

        if (!keepQuiet)
        {
            EntityManager.entities.Add(new DamageIndicator(damage, position));
        }

        // check for death
        if (health <= 0)
        {
            OnDeath(damage);
        }
    }


    // overridable functions, doubt theyll be used for anything but I want the option.
    public virtual Damage OnDamage(Damage damage)
    {
        return damage;
    }
    public virtual void OnDeath(Damage damage)
    {
        if (damage.source is Player player)
        {
            player.AddExperience(experienceValue);
        }
        Kill();
    }
}