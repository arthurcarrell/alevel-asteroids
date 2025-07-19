using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class Meteor : ColliderEntity
{

    private List<Vector2> points = new List<Vector2>();

    private int speed;
    
    public Meteor(Texture2D setTexture, Texture2D setCrossTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setCrossTexture, setPosition, setRotation, setScale)
    {
    }

    private void CreatePoints(int amount, int minDistance, int maxDistance)
    {
        Random random = new Random();
        int rotationOffset = 30;
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
        CreatePoints(5, 5, 50);

        // feed points to the hitbox and enable it
        SetPoints(points);
        shouldCollide = true;

        Random rnd = new Random();
        int teleport_border = Game1.TELEPORT_BORDER;

        // random stats
        speed = rnd.Next(30, 150);
        rotation = 6.283185f * (float)rnd.NextDouble();

        // spawn point
        position.Y = rnd.Next(0, Game1.WINDOW_HEIGHT);

        if (rnd.Next(0, 2) == 1)
        {
            position.X = 0 - teleport_border;
        }
        else
        {
            position.X = Game1.WINDOW_WIDTH + teleport_border;
        }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        // draw lines
        RenderLines(spriteBatch);
        
        // debug utility, should be hidden when done - shows points on the hitbox
        RenderPoints(spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        int teleport_border = Game1.TELEPORT_BORDER;

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        position -= Vec2Forward(rotation, 50 * delta);

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

        // left -> right
        if (position.Y < -teleport_border)
        {
            position.Y = Game1.WINDOW_HEIGHT + teleport_border;
        }
    }
}
