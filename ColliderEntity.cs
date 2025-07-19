using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

/* I want accurate collisions for each object. Because each meteor has a random size and points, it would be bad
 if you died to the air or were able to enter an entity. Instead, each entity's hitbox will be defined by a series of points that
 connect together. */
public class ColliderEntity : Entity
{
    protected List<Vector2> hitboxPoints = new List<Vector2>(); // points of the hitbox (relative to the objects position.)
    protected bool shouldCollide = false; // will it actually check for collisions? Should be false until the points are given.

    // textures
    Texture2D crossTexture;
    public ColliderEntity(Texture2D setTexture, Texture2D setCrossTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
        crossTexture = setCrossTexture;
    }

    // setters
    public void SetPoints(List<Vector2> setPoints) => hitboxPoints = setPoints;

    protected void RenderPoints(SpriteBatch spriteBatch)
    {
        foreach (Vector2 point in hitboxPoints)
        {
            spriteBatch.Draw(crossTexture, position + point, null, Color.Red, rotation, new Vector2(texture.Height / 2, texture.Width / 2), scale, SpriteEffects.None, layer);
        }
    }

}