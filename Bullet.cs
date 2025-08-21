using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class Bullet : ColliderEntity
{

    private int speed;
    private int damage;
    List<Vector2> points = new List<Vector2>() { new Vector2(-2,-2), new Vector2(2,-2), new Vector2(-2,2), new Vector2(2,2)};

    public Bullet(Texture2D setTexture, Texture2D setCrossTexture, Vector2 setPosition, int setDamage, float setRotation = 0, float setScale = 1) : base(setTexture, setCrossTexture, setPosition, setRotation, setScale)
    {
        damage = setDamage;
    }

    protected void OnCollision(GameTime gameTime, ColliderEntity collision)
    {
        if (collision is LivingEntity livingEntity)
        {
            livingEntity.SetHealth(livingEntity.GetHealth() - damage);
            Kill();
        }
    }

    protected override void Init()
    {
        SetPoints(points);
        shouldCollide = true;
        base.Init();
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // get colliders
        ColliderEntity collision = GetFirstCollider();
        isColliding = collision != null;

        if (isColliding)
        {
            OnCollision(gameTime, collision);
        }

    }
}