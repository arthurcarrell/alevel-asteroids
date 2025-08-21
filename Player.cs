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

    // === COOLDOWNS ===
    private float invincibilityFrames = 0;
    private float shootCooldown = 0;
    

    private int modifications;

    // === TEXTURES ===
    private Texture2D bulletTexture;
    private Texture2D crossTexture;
    List<Vector2> points = new List<Vector2>() { new Vector2(-8,-8), new Vector2(8,-8), new Vector2(-8,8), new Vector2(8,8), new Vector2(0,0)};
    public Player(Texture2D setTexture, Texture2D setCrossTexture, Texture2D setBulletTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setCrossTexture, setPosition, setRotation, setScale)
    {
        bulletTexture = setBulletTexture;
        crossTexture = setCrossTexture;

        // set stats
        maxHealth = 200;
        health = maxHealth;
        damage = 20;
    }

    // getters
    public int GetModifications() => modifications;

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
        float milisecondDelta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        // Collider stuff
        isColliding = GetFirstCollider() != null;

        if (isColliding && invincibilityFrames <= 0)
        {
            health -= 10;
            invincibilityFrames += 1000;
        }

        if (invincibilityFrames > 0)
        {
            invincibilityFrames -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }


        int teleport_border = 20;

        // Shooting
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && shootCooldown <= 0)
        {
            shootCooldown = 500;
            EntityManager.entities.Add(new Bullet(bulletTexture, crossTexture, position + Vec2Forward(rotation, 20), damage, rotation));
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
}