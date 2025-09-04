using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

class HUD : Entity
{

    private float fps = 0;
    private Texture2D spriteFont = Textures.galaticSpaceFont;
    private List<String> deathWarnMessages = new List<string>() {
                "you are going to die", "death is inevitable", "nothing lasts forever", "perhaps this is hell"
            };

    private float deathMessageSwapTime = 1;
    private float deathMessageAnim = -1f;
    private float deathMessageAnimCooldown = 0;
    private bool deathMessageShowRectangle = false;
    private bool deathMessageShowText = false;

    private string lowHealthSystemText = "";

    private string deathWarnMessage;
    public HUD(Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(null, setPosition, setRotation, setScale)
    {
        deathWarnMessage = Chance.Item<String>(deathWarnMessages);
        lowHealthSystemText = Chance.Item<String>(deathWarnMessages);
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

        string systemText = "systems non-functional";
        // calculate colour of health
        Color healthColor;
        if (healthPercent >= 70) { healthColor = Color.Lime; systemText = "all systems nominal"; }
        else if (healthPercent >= 50) { healthColor = Color.Yellow; systemText = "systems operational"; }
        else if (healthPercent >= 30) { healthColor = Color.Orange; systemText = "systems damaged"; }
        else if (healthPercent > 0) { healthColor = Color.Red; systemText = lowHealthSystemText; }
        else { healthColor = Color.Gray; }

        string expBar = "";
        for (int i = 0; i < Math.Round(player.GetExpPercent() * 10); i++)
        {
            expBar += '█';
        }
        

        Text text = new Text($"{systemText}\n", healthColor);
        text.Add($"core: {health}/{maxHealth} ({healthPercent}%)\n", healthColor);
        text.Add($"modifications: {modifications}\n", healthColor);
        text.Add($"level: {level}\n", healthColor);
        text.Add($"exp: {expBar}\n", healthColor);
        TextDrawer.DrawText(spriteBatch, text, new Vector2(0, 410), spriteFont, 1.2f);

        if (healthColor == Color.Red) {
            Text deathWarn = new Text(deathWarnMessage, Color.Black);

            if (deathMessageAnimCooldown <= 0) {
                deathMessageAnim = 2000;
                deathMessageSwapTime = 1500;
                deathWarnMessage = "low health remaining";
                deathMessageAnimCooldown = 100000;
            }
            if (deathMessageShowRectangle) {
                // draw the rectangle
                int paddingY = 40;
                int paddingX = 10;
                LineDrawer.DrawLine(spriteBatch, Textures.pixel, new Vector2(Game1.WINDOW_WIDTH/2 - paddingX - deathWarn.GetPixelLength(2)/2, Game1.WINDOW_HEIGHT/2 - paddingY/4), new Vector2(Game1.WINDOW_WIDTH/2 + paddingX + deathWarn.GetPixelLength(2)/2, Game1.WINDOW_HEIGHT/2 - paddingY/4), paddingY, Color.Red);
            }
            
            if (deathMessageShowText) {
                TextDrawer.DrawText(spriteBatch, deathWarn, new Vector2(Game1.WINDOW_WIDTH/2 - deathWarn.GetPixelLength(2)/2, Game1.WINDOW_HEIGHT/2), spriteFont, 2);
            }          
        }
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        deathMessageSwapTime -= deltaTime;

        if (deathMessageSwapTime <= 0) {
            deathMessageSwapTime = 50;
            deathWarnMessage = Chance.Item<String>(deathWarnMessages);
            
        }

        if (deathMessageAnim > 0) {
            deathMessageAnim -= deltaTime;
        }

        if (deathMessageAnimCooldown > 0) {
            deathMessageAnimCooldown -= deltaTime;
        }

        deathMessageShowRectangle = deathMessageAnim <= 1900 && deathMessageAnim >= 100;
        deathMessageShowText = deathMessageAnim <= 1800 && deathMessageAnim >= 200;
    }
}