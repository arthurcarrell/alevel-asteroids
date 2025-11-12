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

    public const int WINDOW_WIDTH = 1280;
    public const int WINDOW_HEIGHT = 920;

    // how far outside of the game border does this need to be before it loops around
    // entities should be completely out of the screen so it looks like it seamlessly loops.
    public const int TELEPORT_BORDER = 50;
    private int drawCount;
    private HUD hud;
    private HUDDebug hudDebug;

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
        // LOAD TEXTURES
        Textures.Load(this);

        Player player = new Player(Textures.ship, new Vector2(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2));
        EntityManager.entities.Add(player);

        hud = new HUD(new Vector2(0, 0)); // Hud is an entity, but it doesnt exist with the EntityManager system due to needing to be executed uniquely

        // Director
        Director director = new Director(new Vector2(0,0));
        EntityManager.entities.Add(director);

        hudDebug = new HUDDebug(null, new Vector2(0,0)); // HudDebug is an entity, but it doesnt exist with the EntityManager system due to needing to be executed uniquely
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        UpdateEntities(gameTime);

        base.Update(gameTime);
    }

    protected void DrawEntities(SpriteBatch spriteBatch, GameTime gameTime) {
        List<Entity> tempEntities = new List<Entity>(EntityManager.entities); // prevents modification during foreach loop which leads to a crash
        Console.Clear();
        drawCount++;
        Console.WriteLine($"Draw: {drawCount}");
        foreach (Entity entity in tempEntities) {
            Console.WriteLine(entity);
            entity.Render(spriteBatch);
        }
        hud.Render(spriteBatch);
        hudDebug.Render(spriteBatch, gameTime);
    }

    protected void UpdateEntities(GameTime gameTime) {
        List<Entity> tempEntities = new List<Entity>(EntityManager.entities); // prevents modification during foreach loop which leads to a crash
        foreach (Entity entity in tempEntities) {
            entity.Update(gameTime);
        }
        hud.Update(gameTime);
        hudDebug.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // set samplerState to pointclamp so no pixels are blurred
        DrawEntities(_spriteBatch, gameTime);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}