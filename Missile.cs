using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

// Clone of Bullet but with some changes such as targetting and more damage
public class Missile : ColliderEntity
{
    private Damage procDamage;
    private LivingEntity owner;
    private Entity target;
    private float missileCount;
    private float baseRotation;
    List<Vector2> points = new List<Vector2>() { new Vector2(-8, -8), new Vector2(8, -8), new Vector2(-8, 8), new Vector2(8, 8) };

    public Missile(Vector2 setPosition, Damage originalDamage, float amount, Entity setTarget, float setRotation = 0, float setScale = 1) : base(Textures.missile, setPosition, setRotation, setScale)
    {
        target = setTarget;
        procDamage = originalDamage;
        missileCount = amount;
        baseRotation = rotation;
    }

    protected void OnCollision(GameTime gameTime, ColliderEntity collision)
    {
        if (collision is LivingEntity livingEntity)
        {
            // create the damage structure
            Damage damage = new Damage();
            damage.amount = (int)Math.Round(procDamage.amount * (2 + missileCount));
            damage.source = procDamage.source;
            damage.type = DamageType.NORMAL;
            damage.procChance = procDamage.procChance * 0.75f;

            // pass it over
            livingEntity.DoDamage(damage);
        }
        Kill();
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);
        //RenderPoints(spriteBatch, isColliding);
    }

    protected override void Init()
    {
        SetPoints(points);
        shouldCollide = true;
        owner = procDamage.source;
        base.Init();
    }

    public double getAngleToFace(Entity entity)
    {
        Vector2 difference = entity.position - position;
        return Math.Atan2(difference.Y, difference.X);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (target != null)
        {
            rotation = (float)getAngleToFace(target);
        }
        else
        {
            rotation = baseRotation;
        }

        // check if target is alive
        if (!EntityManager.entities.Contains(target))
        {
            target = null;
        }

        // get colliders
        ColliderEntity collision = GetFirstCollider();
        isColliding = collision != null;

        if (isColliding && collision != owner)
        {
            OnCollision(gameTime, collision);
        }

        // move forward
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        position += Vec2Forward(rotation, 700 * delta * EntityManager.movespeedMult);


        // LOOPS
        int teleport_border = 20;
        // right -> left
        if (position.X > Game1.WINDOW_WIDTH + teleport_border)
        {
            position.X = -teleport_border;
        }

        // left -> right
        if (position.X < -teleport_border)
        {
            position.X = Game1.WINDOW_WIDTH + teleport_border;
        }

        // top -> bottom
        if (position.Y > Game1.WINDOW_HEIGHT + teleport_border)
        {
            position.Y = -teleport_border;
        }

        // left -> right
        if (position.Y < -teleport_border)
        {
            position.Y = Game1.WINDOW_HEIGHT + teleport_border;
        }

    }
}
