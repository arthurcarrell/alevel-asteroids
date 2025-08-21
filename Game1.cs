using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public Texture2D pixel;
    Texture2D ship;
    Texture2D galaticSpaceFont;
    Texture2D bullet;

    public const int WINDOW_WIDTH = 640;
    public const int WINDOW_HEIGHT = 480;

    // how far outside of the game border does this need to be before it loops around
    // entities should be completely out of the screen so it looks like it seamlessly loops.
    public const int TELEPORT_BORDER = 50;

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
        ship = Content.Load<Texture2D>("ship");
        bullet = Content.Load<Texture2D>("bullet");
        galaticSpaceFont = Content.Load<Texture2D>("galatic_space_font");

        // TODO: use this.Content to load your game content here



        for (int i = 0; i < 1; i++)
        {
            Meteor meteor = new Meteor(galaticSpaceFont, pixel, cross, bullet, new Vector2(200, 200), 4);

            Random rnd = new Random();
            // spawn point
            meteor.position.Y = rnd.Next(0, Game1.WINDOW_HEIGHT);

            if (rnd.Next(0, 2) == 1)
            {
                meteor.position.X = 0 - TELEPORT_BORDER;
            }
            else
            {
                meteor.position.X = WINDOW_WIDTH + TELEPORT_BORDER;
            }

            EntityManager.entities.Add(meteor);
        }

        Player player = new Player(galaticSpaceFont, ship, cross, bullet, new Vector2(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2));
        EntityManager.entities.Add(player);


        // UI
        // FPS Counter
        HUD hud = new HUD(galaticSpaceFont, new Vector2(0, 0));
        EntityManager.entities.Add(hud);
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
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // set samplerState to pointclamp so no pixels are blurred
        DrawEntities(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}