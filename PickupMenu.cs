using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class PickupMenu : Entity
{

    private bool showMenu = true;
    public PickupMenu(Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(null, setPosition, setRotation, setScale)
    {
        shouldRender = false;
    }

    public override void Render(SpriteBatch spriteBatch)
    {

        // draw the box

        // points
        Vector2 startPoint = new Vector2(Game1.WINDOW_WIDTH / 3 - 32, Game1.WINDOW_HEIGHT / 2 - 32);
        Vector2 endPoint = new Vector2(Game1.WINDOW_WIDTH / 3 + 32, Game1.WINDOW_HEIGHT / 2 + 32);
        LineDrawer.DrawLine(spriteBatch, Textures.pixel, new Vector2(startPoint.X, startPoint.Y), new Vector2(endPoint.X, startPoint.Y), 10);
        base.Render(spriteBatch);
    }

}