using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace alevel_asteroids;

public class PickupMenu : Entity
{

    private bool showMenu = true;
    private Item[] items = new Item[3];
    private Vector2 boxPosition1 = new Vector2(Game1.WINDOW_WIDTH/3f, Game1.WINDOW_HEIGHT/2f);
    private Vector2 boxPosition2 = new Vector2(Game1.WINDOW_WIDTH/2f, Game1.WINDOW_HEIGHT/2f);
    private Vector2 boxPosition3 = new Vector2(Game1.WINDOW_WIDTH/1.5f , Game1.WINDOW_HEIGHT/2f);


    public PickupMenu(Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(null, setPosition, setRotation, setScale)
    {
        shouldRender = false;

        // select three random items for this pickup menu to have.
        for (int i=0; i < 3; i++) {
            items[i] = Chance.Item<Item>(Items.GetItemList()); // pick a random item and select it
            Console.WriteLine(items[i].GetName());
        }
        EntityManager.movespeedMult = 0;
    }

    private Color TierToColor(Tier tier) {
        switch(tier) {
            case Tier.COMMON:
                return Color.White;
            case Tier.RARE:
                return Color.Lime;
            case Tier.LEGENDARY:
                return Color.Red;
            default:
                return Color.HotPink; // if I see hotpink in the game it means something has gone very wrong.
        }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        // draw the boxes
        Texture2D boxTexture = Textures.menuOption;
        spriteBatch.Draw(boxTexture, boxPosition1, null, TierToColor(items[0].GetTier()), rotation, new Vector2(boxTexture.Width/2f, boxTexture.Height/2f), 2, SpriteEffects.None, layer);
        spriteBatch.Draw(boxTexture, boxPosition2, null, TierToColor(items[1].GetTier()), rotation, new Vector2(boxTexture.Width/2f, boxTexture.Height/2f), 2, SpriteEffects.None, layer);
        spriteBatch.Draw(boxTexture, boxPosition3, null, TierToColor(items[2].GetTier()), rotation, new Vector2(boxTexture.Width/2f, boxTexture.Height/2f), 2, SpriteEffects.None, layer);

        // draw the items
        int scale = 5;
        spriteBatch.Draw(items[0].GetTexture(), boxPosition1, null, Color.White, rotation, new Vector2(8,8), scale, SpriteEffects.None, layer);
        spriteBatch.Draw(items[1].GetTexture(), boxPosition2, null, Color.White, rotation, new Vector2(8,8), scale, SpriteEffects.None, layer);
        spriteBatch.Draw(items[2].GetTexture(), boxPosition3, null, Color.White, rotation, new Vector2(8,8), scale, SpriteEffects.None, layer);

        Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

        Rectangle rec1 = new Rectangle((int)boxPosition1.X-64, (int)boxPosition2.Y-64, 64, 64);
        Rectangle rec2 = new Rectangle((int)boxPosition1.X-64, (int)boxPosition2.Y-64, 64, 64);
        Rectangle rec3 = new Rectangle((int)boxPosition1.X-64, (int)boxPosition2.Y-64, 64, 64);

        if (rec1.Contains(mousePos)) {
            Console.WriteLine("hovering");
            Text text = new Text(items[0].GetName().ToLower(), TierToColor(items[0].GetTier()));
            TextDrawer.DrawText(spriteBatch, text, new Vector2(Game1.WINDOW_WIDTH/2, Game1.WINDOW_HEIGHT/2-80), Textures.galaticSpaceFont, 1);
        }
    }


    public override void Update(GameTime gameTime)
    {
        Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

        Rectangle rec1 = new Rectangle((int)boxPosition1.X, (int)boxPosition2.Y, 64, 64);
        Rectangle rec2 = new Rectangle((int)boxPosition1.X, (int)boxPosition2.Y, 64, 64);
        Rectangle rec3 = new Rectangle((int)boxPosition1.X, (int)boxPosition2.Y, 64, 64);

        if (Keyboard.GetState().IsKeyDown(Keys.D1)) {
            EntityManager.player.AddItem(items[0]);
            Kill();
        } else if (Keyboard.GetState().IsKeyDown(Keys.D2)){
            EntityManager.player.AddItem(items[1]);
            Kill();
        } else if (Keyboard.GetState().IsKeyDown(Keys.D3)){
            EntityManager.player.AddItem(items[2]);
            Kill();
        }

        base.Update(gameTime);
    }


    public override void Kill()
    {
        EntityManager.movespeedMult = 1;
        base.Kill();
    }
}