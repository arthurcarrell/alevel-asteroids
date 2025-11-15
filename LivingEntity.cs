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
        EntityManager.livingEntityCount++;
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

    public void Heal(int amount, bool display = true)
    {
        if (display)
        {
            Damage damage = new Damage();
            damage.amount = amount;
            damage.type = DamageType.HEAL;
            damage.source = this;
            damage.procChance = 0;
            EntityManager.entities.Add(new DamageIndicator(damage, position));
        }


        if (health + amount > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += amount;
        }
    }

    public Damage ProcessDamage(Damage damage)
    {
        // calculate crit
        List<Item> attackerItems = damage.source.GetItems();
        int magnifyingGlassCount = attackerItems.FindAll(item => item == Items.MAGNIFYING_GLASS).Count;
        int gasolineCount = attackerItems.FindAll(item => item == Items.GASOLINE).Count;
        int missileLauncherCount = attackerItems.FindAll(item => item == Items.MISSILE_LAUNCHER).Count;
        int vampiricNanitesCount = attackerItems.FindAll(item => item == Items.VAMPIRIC_NANITES).Count;

        if (Chance.Percentage(magnifyingGlassCount * 0.1f, damage.procChance))
        {
            damage.amount *= 2;
            damage.type = DamageType.CRIT;
        }

        if (Chance.Percentage(gasolineCount * 0.1f, damage.procChance))
        {
            statusEffects.Add(new FireStatusEffect(damage, 5000f));
        }

        if (Chance.Percentage(0.05f, damage.procChance) && missileLauncherCount > 0)
        {
            // spawn a missile
            EntityManager.entities.Add(new Missile(damage.source.position + Vec2Forward(damage.source.rotation, 40), damage, missileLauncherCount, this));
        }

        if (vampiricNanitesCount > 0 && damage.source != this && damage.procChance > 0)
        {
            damage.source.Heal(vampiricNanitesCount);
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

        // get items
        int proximityMineCount = damage.source.GetItems().FindAll(item => item == Items.PROXIMITY_MINES).Count;

        // proximity mine
        if (Chance.Percentage(0.1f * proximityMineCount, damage.procChance))
        {
            // create a mine
            Damage mineDamage = new Damage();
            mineDamage.amount = damage.amount * 2;
            mineDamage.procChance = damage.procChance / 2;
            mineDamage.source = damage.source;
            EntityManager.entities.Add(new ProximityMine(position, mineDamage));
        }


        Kill();
    }

    public override void Kill()
    {
        EntityManager.livingEntityCount--;
        if (Chance.Percentage(0.1f))
        {
            EntityManager.entities.Add(new ItemPickup(position));
        }
        base.Kill();
    }
}
