using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace alevel_asteroids;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // textures
    Texture2D cross;
    Texture2D pixel;

    public const int WINDOW_WIDTH = 640;
    public const int WINDOW_HEIGHT = 480;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // change the window size to the set one.
        _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
        _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        pixel = Content.Load<Texture2D>("pixel");
        cross = Content.Load<Texture2D>("cross");
        // TODO: use this.Content to load your game content here
        for (int i = 0; i < 100; i++)
        {
            Meteor meteor = new Meteor(pixel, cross, new Vector2(200,200));
            EntityManager.entities.Add(meteor);
        }
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