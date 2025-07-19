using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace alevel_asteroids;

public class Player : Entity
{

    int speed = 100;
    public Player(Texture2D setTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
    }

    public override void Update(GameTime gameTime)
    {
        
        int teleport_border = 20;
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            position.Y -= speed * delta;
        } else if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            position.Y += speed * delta;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            position.X -= speed * delta;
        } else if (Keyboard.GetState().IsKeyDown(Keys.D))
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