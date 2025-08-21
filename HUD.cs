using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

class HUD : Entity
{

    private float fps = 0;
    private Texture2D spriteFont = Textures.galaticSpaceFont;
    public HUD(Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(null, setPosition, setRotation, setScale)
    {
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        // get the player
        Player player = EntityManager.player;

        int health = player.GetHealth();
        int maxHealth = player.GetMaxHealth();
        int healthPercent = (int)((float)health / (float)maxHealth * 100);
        int modifications = player.GetModifications();
        int level = player.GetLevel();

        // calculate colour of health
        Color healthColor;
        if (healthPercent >= 70) { healthColor = Color.Lime; }
        else if (healthPercent >= 50) { healthColor = Color.Yellow; }
        else if (healthPercent >= 30) { healthColor = Color.Orange; }
        else if (healthPercent > 0) { healthColor = Color.Red; }
        else { healthColor = Color.Gray; }

        string expBar = "";
        for (int i = 0; i < Math.Round(player.GetExpPercent() * 10); i++)
        {
            expBar += '█';
        }

        Text text = new Text("all systems nominal\n", Color.Lime);
        text.Add($"core: {health}/{maxHealth} ({healthPercent}%)\n", healthColor);
        text.Add($"modifications: {modifications}\n", Color.Lime);
        text.Add($"level: {level}\n", Color.Lime);
        text.Add($"exp: {expBar}\n", Color.Lime);
        TextDrawer.DrawText(spriteBatch, text, new Vector2(0, 410), spriteFont, 1.2f);
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (deltaTime > 0)
        {
            fps = 1 / deltaTime;
        }
    }
}