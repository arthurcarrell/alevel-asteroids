using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace alevel_asteroids;

// this is not an entity, instead this is a utility class that only contains static methods. 
// utility classes must be abstract, and their methods must be static.
public abstract class LineDrawer
{
    public static void DrawLine(SpriteBatch spriteBatch, Texture2D texture, Vector2 pointA, Vector2 pointB, int lineWidth=1)
    {
        // calculate angle and width
        double angle = Math.Atan2(pointB.Y - pointA.Y, pointB.X - pointA.X);
        double length = Vector2.Distance(pointA, pointB);

        // draw
        spriteBatch.Draw(texture, pointA, null, Color.White, (float)angle, Vector2.Zero, new Vector2((float)length, (float)lineWidth), SpriteEffects.None, 0);
    }
}