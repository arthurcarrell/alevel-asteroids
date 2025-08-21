using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;


public class LivingEntity : ColliderEntity
{
    protected int health;
    protected int maxHealth;
    protected int damage;
    public LivingEntity(Texture2D setTexture, Texture2D setCrossTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setCrossTexture, setPosition, setRotation, setScale)
    {
    }

    // Getters
    public int GetMaxHealth() => maxHealth;
    public int GetHealth() => health;
    public int GetDamage() => damage;

    // Setters
    public void SetMaxHealth(int value) => maxHealth = value;
    public void SetHealth(int value) => health = value;
    public void SetDamage(int value) => damage = value;
}