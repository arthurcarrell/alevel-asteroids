using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace alevel_asteroids;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Texture2D pixel = Content.Load<Texture2D>("pixel");
        Texture2D cross = Content.Load<Texture2D>("cross");
        // TODO: use this.Content to load your game content here
        Meteor meteorTest = new Meteor(pixel, cross, new Vector2(200,200));
        meteorTest.scale = 1;
        EntityManager.entities.Add(meteorTest);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        UpdateEntities(gameTime);

        base.Update(gameTime);
    }

    protected void DrawEntities(SpriteBatch spriteBatch) {
        List<Entity> tempEntities = new List<Entity>(EntityManager.entities); // prevents modification during foreach loop which leads to a crash
        foreach (Entity entity in tempEntities) {
            entity.Render(spriteBatch);
        }
    }

    protected void UpdateEntities(GameTime gameTime) {
        List<Entity> tempEntities = new List<Entity>(EntityManager.entities); // prevents modification during foreach loop which leads to a crash
        foreach (Entity entity in tempEntities) {
            entity.Update(gameTime);
        }
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        DrawEntities(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}