
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public static class Textures
{
    public static Texture2D cross;
    public static Texture2D pixel;
    public static Texture2D ship;
    public static Texture2D galaticSpaceFont;
    public static Texture2D bullet;
    public static Texture2D box;
    public static void Load(Game game)
    {
        pixel = game.Content.Load<Texture2D>("pixel");
        cross = game.Content.Load<Texture2D>("cross");
        ship = game.Content.Load<Texture2D>("ship");
        bullet = game.Content.Load<Texture2D>("bullet");
        galaticSpaceFont = game.Content.Load<Texture2D>("galatic_space_font");
        box = game.Content.Load<Texture2D>("box");
    }
}