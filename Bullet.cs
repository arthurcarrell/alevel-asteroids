using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class Bullet : ColliderEntity
{
    private LivingEntity owner;
    List<Vector2> points = new List<Vector2>() { new Vector2(-2,-2), new Vector2(2,-2), new Vector2(-2,2), new Vector2(2,2)};

    public Bullet(Texture2D setTexture, Vector2 setPosition, LivingEntity setOwner, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
        owner = setOwner;
    }

    protected void OnCollision(GameTime gameTime, ColliderEntity collision)
    {
        if (collision is LivingEntity livingEntity)
        {
            // create the damage structure
            Damage damage = new Damage();
            damage.amount = owner.GetDamage();
            damage.source = owner;
            damage.type = DamageType.NORMAL;
            damage.procChance = 1f;

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

        // move forward
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        position += Vec2Forward(rotation, 500 * delta);


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