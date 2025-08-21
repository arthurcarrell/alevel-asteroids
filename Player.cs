using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace alevel_asteroids;

public class Player : ColliderEntity
{

    private int speed = 100;


    private int maxHealth = 200;
    private int health = 200;

    // hit cooldown
    private float invincibilityFrames = 0;
    

    private int modifications;
    List<Vector2> points = new List<Vector2>() { new Vector2(-8,-8), new Vector2(8,-8), new Vector2(-8,8), new Vector2(8,8)};
    public Player(Texture2D setTexture, Texture2D setCrossTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setCrossTexture, setPosition, setRotation, setScale)
    {
    }

    // getters
    public int GetHealth() => health;
    public int GetMaxHealth() => maxHealth;
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
        // collider stuff
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
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

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