using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace alevel_asteroids;

public abstract class Entity
{
    /*
    This class isn't designed to be used by itself, instead, it's designed to be inherited and exists as a base for all objects in the game.
    This means that all rendering code is universally editable here, and basic functions are abstracted so they dont need to be thought about
    when making an object.
    */

    // RENDER STUFF
    protected Texture2D texture;
    public Boolean shouldRender = true;

    public float scale = 1;
    public float rotation = 0;
    public int layer = 1;

    // Coordinate Stuff
    public Vector2 position;

    
    /* MAIN FUNCTIONS */
    protected virtual void Init() {
        // Run on Entity creation
    }

    public virtual void Update(GameTime gameTime) {
        // Run each frame
    }
    
    protected Vector2 Vec2Forward(float direction, float distance)
    {
        Vector2 output;
        output.X = (float)Math.Cos(direction) * distance;
        output.Y = (float)Math.Sin(direction) * distance;

        return output;
    }


    /* GETTERS */
    public Texture2D GetTexture2D()
    {
        return texture;
    }

    // Render, this is run every frame and draws the texture, with the sprite drawn in the center, not in the corner.
    public virtual void Render(SpriteBatch spriteBatch) {
        if (shouldRender) {
            // render onto the screen
            spriteBatch.Draw(texture, position - new Vector2(texture.Height / 2, texture.Width / 2), null, Color.White, rotation, new Vector2(texture.Height / 2, texture.Width / 2), scale, SpriteEffects.None, layer);
        }
    }

    // constructor
    public Entity(Texture2D setTexture, Vector2 setPosition, float setRotation=0, float setScale=1) {
        texture = setTexture;
        position = setPosition;
        rotation = setRotation;
        scale = setScale;

        // Run init
        Init();
    }
}