using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class Meteor : LivingEntity
{

    private List<Vector2> points = new List<Vector2>();
    private int meteorSize;
    private int speed;

    public Meteor(Texture2D setTexture, Vector2 setPosition, int setSize = 1, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {

        // get difficulty modifier
        float difficultyModifier = EntityManager.director.GetDifficultyModifier();
        // set stats
        meteorSize = setSize;
        maxHealth = (int)((30 * meteorSize) + ((20 * meteorSize) * difficultyModifier));
        health = maxHealth;
        experienceValue = 10 * meteorSize;
        damage = (int)(5 + (5 * difficultyModifier));

        Console.WriteLine($"Instantiated: Max HP: {maxHealth}, Size: {meteorSize}, Damage : {damage}");
        int renderSize = 20 + (meteorSize * 20);

        CreatePoints(Math.Max(5,meteorSize*2), renderSize/12, renderSize);
    }

    private void CreatePoints(int amount, int minDistance, int maxDistance)
    {
        Random random = new Random();
        int rotationOffset = 360/amount - 2;
        int curRotation = 0;
        for (int i = 0; i < amount; i++)
        {
            // generate offset
            int distance = random.Next(minDistance, maxDistance);

            curRotation += rotationOffset;
            Vector2 offset = new Vector2();

            offset.X = (float)Math.Cos(curRotation) * distance;
            offset.Y = (float)Math.Sin(curRotation) * distance;

            points.Add(offset);
        }
    }

    private void RenderLines(SpriteBatch spriteBatch)
    {
        // assign the previous point to be the position of the current point.
        Vector2 prevPoint = points[0];
        Vector2 pointA;
        Vector2 pointB;

        foreach (Vector2 point in points)
        {
            // get the absolute coordinates of each point (points are stored as relative positions)
            pointA = position + prevPoint;
            pointB = position + point;

            // join up the points with lines. Because im drawing lines I need to use the LineDrawer custom utility.
            LineDrawer.DrawLine(spriteBatch, texture, pointA, pointB);

            // set the current point to be the previous point
            prevPoint = point;
        }

        // the final point needs to be connected to the first point
        pointA = position + prevPoint;
        pointB = position + points[0];
        LineDrawer.DrawLine(spriteBatch, texture, pointA, pointB);
    }

    protected override void Init()
    {
        Random random = new Random();

        // feed points to the hitbox and enable it
        SetPoints(points);
        shouldCollide = true;

        Random rnd = new Random();
        // random stats
        speed = rnd.Next(30, 150);
        rotation = 6.283185f * (float)rnd.NextDouble();
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        // draw lines
        RenderLines(spriteBatch);
        
        // debug utility, should be hidden when done - shows points on the hitbox
        //RenderPoints(spriteBatch, isColliding);
    }

    public override void OnDeath(Damage damage)
    {
        if (meteorSize > 1)
        {
            EntityManager.entities.Add(new Meteor(texture, position, meteorSize / 2));
            EntityManager.entities.Add(new Meteor(texture, position, meteorSize / 2));
        }
        base.OnDeath(damage);
    }

    public override void Update(GameTime gameTime)
    {

        // collision stuff
        isColliding = GetFirstCollider() != null;
        TickStatusEffects(gameTime);

        int teleport_border = Game1.TELEPORT_BORDER;

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        position += Vec2Forward(rotation, 50 * delta * EntityManager.movespeedMult);

        // LOOPS

        // right -> left
        if (position.X > Game1.WINDOW_WIDTH + teleport_border)
        {
            position.X = -teleport_border;
        }

        // left -> right
        if (position.X < -teleport_border)
        {
            position.X = Game1.WINDOW_WIDTH + teleport_border;
        }

        // top -> bottom
        if (position.Y > Game1.WINDOW_HEIGHT + teleport_border)
        {
            position.Y = -teleport_border;
        }

        // bottom -> top
        if (position.Y < -teleport_border)
        {
            position.Y = Game1.WINDOW_HEIGHT + teleport_border;
        }
    }
}
