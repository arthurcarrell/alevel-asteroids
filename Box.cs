using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class Box : Entity
{

    private int length;
    private int height;
    public Box(Texture2D setTexture, Vector2 setPosition, int setHeight, int setLength, float setRotation = 0) : base(setTexture, setPosition, setRotation, 1)
    {
        length = setLength;
        height = setHeight;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        if (shouldRender) {
            // render onto the screen
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Height / 2, texture.Width / 2), scale, SpriteEffects.None, layer);
        }
    }
}