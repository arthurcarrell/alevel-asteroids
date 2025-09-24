using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;


public class LivingEntity : ColliderEntity
{
    protected int health = 0;
    protected int maxHealth = 0;
    protected int damage = 0;
    protected int experienceValue = 0;
    protected LivingEntity lastDamager;

    protected List<Item> items = new List<Item>();
    protected List<StatusEffect> statusEffects = new List<StatusEffect>();
    public LivingEntity(Texture2D setTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
    }

    // Getters
    public int GetMaxHealth() => maxHealth;
    public int GetHealth() => health;
    public int GetDamage() => damage;
    public List<Item> GetItems() => items;
    public List<StatusEffect> GetStatusEffects() => statusEffects;

    // Setters
    public void SetMaxHealth(int value) => maxHealth = value;
    public void SetHealth(int value) => health = value;
    public void SetDamage(int value) => damage = value;

    public void TickStatusEffects(GameTime gameTime)
    {
        List<StatusEffect> runStatusEffects = new List<StatusEffect>(statusEffects);
        foreach (StatusEffect statusEffect in runStatusEffects)
        {
            statusEffect.timeLeft -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            statusEffect.Tick(this, gameTime);
            if (statusEffect.timeLeft <= 0)
            {
                statusEffects.Remove(statusEffect);
            }
        }
    }

    public Damage ProcessDamage(Damage damage)
    {
        // calculate crit
        Random random = new Random();

        int targetingScopeCount = damage.source.GetItems().FindAll(item => item == Item.TARGETTING_SCOPE).Count;
        int gasolineCount = damage.source.GetItems().FindAll(item => item == Item.GASOLINE).Count;

        if (Chance.Percentage(targetingScopeCount*0.1f, damage.procChance))
        {
            damage.amount *= 2;
            damage.type = DamageType.CRIT;
        }

        if (Chance.Percentage(gasolineCount*0.1f, damage.procChance))
        {
            statusEffects.Add(new FireStatusEffect(5000f, damage.source));
        }

        return damage;
    }

    public void DoDamage(Damage damage, bool keepQuiet = false)
    {
        // process damage
        damage = ProcessDamage(damage);
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