
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
    public static Texture2D menuOption;
    public static Texture2D missile;
    public static Texture2D proximityMine;
    public static Texture2D explosion;

    // ITEMS
    public static Texture2D itemBrokenPlate;
    public static Texture2D itemGasoline;
    public static Texture2D itemMagnifyingGlass;
    public static Texture2D itemMissileLauncher;
    public static Texture2D itemRocketMagazine;
    public static Texture2D itemIonBooster;
    public static Texture2D itemVampiricNanites;
    public static Texture2D itemProximityMines;
    public static void Load(Game game)
    {
        pixel = game.Content.Load<Texture2D>("pixel");
        cross = game.Content.Load<Texture2D>("cross");
        ship = game.Content.Load<Texture2D>("ship");
        bullet = game.Content.Load<Texture2D>("bullet");
        galaticSpaceFont = game.Content.Load<Texture2D>("galatic_space_font");
        box = game.Content.Load<Texture2D>("box");
        menuOption = game.Content.Load<Texture2D>("menu_option");
        missile = game.Content.Load<Texture2D>("missile");
        proximityMine = game.Content.Load<Texture2D>("proximity_mine");
        explosion = game.Content.Load<Texture2D>("explosion");

        // Items
        itemBrokenPlate = game.Content.Load<Texture2D>("broken_plate");
        itemGasoline = game.Content.Load<Texture2D>("gasoline");
        itemMagnifyingGlass = game.Content.Load<Texture2D>("magnifying_glass");
        itemMissileLauncher = game.Content.Load<Texture2D>("missile_launcher");
        itemRocketMagazine = game.Content.Load<Texture2D>("rocket_magazine");
        itemIonBooster = game.Content.Load<Texture2D>("ion_booster");
        itemVampiricNanites = game.Content.Load<Texture2D>("vampiric_nanites");
        itemProximityMines = game.Content.Load<Texture2D>("proximity_mines");
    }
}