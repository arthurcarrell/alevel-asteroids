using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class HUDDebug : Entity
{
    float timeSinceLastFrame = 0;
    float fpsUpdateTime = 0;
    int frameRate = 0;
    public HUDDebug(Texture2D setTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
        shouldRender = false;
    }

    public void Render(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // get info
        int entityCount = EntityManager.entities.Count;
        float renderTimeLastFrame = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        // create colors if something is bad
        Color colorRender = Color.LightGreen;
        if (entityCount >= 100 | renderTimeLastFrame >= 100) {
            colorRender = Color.Orange;
        }

        Color colorUpdate = Color.LightGreen;
        if (timeSinceLastFrame >= 100 | frameRate <= 51) {
            colorRender = Color.Orange;
        }

        // create debug text
        Text text = new Text($"entities: {entityCount}\nrender time: {Math.Ceiling(renderTimeLastFrame)}ms ({Math.Round(1.0/gameTime.ElapsedGameTime.TotalSeconds)}fps)\n", colorRender);
        text.Add($"update time: {Math.Ceiling(timeSinceLastFrame)}ms ({frameRate}fps)", colorUpdate);
        TextDrawer.DrawText(spriteBatch, text, new Vector2(5, 10), Textures.galaticSpaceFont);
    }

    public override void Update(GameTime gameTime)
    {
        timeSinceLastFrame = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        frameRate = (int)Math.Round(1.0/gameTime.ElapsedGameTime.TotalSeconds);
        base.Update(gameTime);
    }
}