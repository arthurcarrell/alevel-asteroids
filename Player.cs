using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace alevel_asteroids;

public class Player : LivingEntity
{

    // === STATS ===
    private int speed = 100;
    private int experience = 0;
    private int level = 1;
    private int requiredExperience = 100;

    // === COOLDOWNS ===
    private float invincibilityFrames = 0;
    private float shootCooldown = 0;


    private int modifications;

    // === TEXTURES ===
    List<Vector2> points = new List<Vector2>() { new Vector2(-8, -8), new Vector2(8, -8), new Vector2(-8, 8), new Vector2(8, 8), new Vector2(0, 0) };
    public Player(Texture2D setTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {

        // set stats
        maxHealth = 200;
        health = maxHealth;
        damage = 20;

        items.Add(Item.TARGETTING_SCOPE);
        items.Add(Item.TARGETTING_SCOPE);
        items.Add(Item.TARGETTING_SCOPE);
        items.Add(Item.TARGETTING_SCOPE);
    }

    // getters
    public int GetModifications() => modifications;
    public int GetLevel() => level;
    public float GetExpPercent() => (float)experience / (float)requiredExperience;

    protected override void Init()
    {
        SetPoints(points);
        shouldCollide = true;
        base.Init();

        // give the entitymanager us, so that its easier to access everywhere else.
        EntityManager.player = this;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);
        // debug utility, should be hidden when done - shows points on the hitbox
        RenderPoints(spriteBatch, isColliding);
    }

    public override void Update(GameTime gameTime)
    {
        // Delta
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        // Collider stuff
        ColliderEntity collision = GetFirstCollider();
        isColliding = collision != null;
        TickStatusEffects(gameTime);

        if (isColliding && invincibilityFrames <= 0)
        {
            if (collision is LivingEntity livingEntity) {
                Damage damage = new Damage();
                damage.amount = livingEntity.GetDamage();
                damage.source = livingEntity;
                damage.type = DamageType.NORMAL;
                damage.procChance = 1;

                DoDamage(damage, true);
                invincibilityFrames += 1000;
            }
        }

        if (invincibilityFrames > 0)
        {
            invincibilityFrames -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }


        int teleport_border = 20;

        // Shooting
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && shootCooldown <= 0)
        {
            EntityManager.entities.Add(new Bullet(Textures.bullet, position + Vec2Forward(rotation, 20), this, rotation));
            shootCooldown = 500;
        }

        if (shootCooldown > 0)
        {
            shootCooldown -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        // Movement

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            position.Y -= speed * delta;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            position.Y += speed * delta;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            position.X -= speed * delta;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            position.X += speed * delta;
        }

        MouseState mouse = Mouse.GetState();
        Vector2 mousePos = new Vector2(mouse.X, mouse.Y);

        Vector2 direction = mousePos - position;
        rotation = (float)Math.Atan2(direction.Y, direction.X);

        // LOOPS

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

    public void AddExperience(int amount)
    {
        experience += amount;
        DoLevelUpCheck();
    }

    private void DoLevelUpCheck()
    {
        while (experience > requiredExperience)
        {
            experience -= requiredExperience;
            requiredExperience = (int)(requiredExperience * 1.5);
            level++;
            damage = (int)(damage * 1.2);
            maxHealth = (int)(maxHealth * 1.2);
        }
    }
}