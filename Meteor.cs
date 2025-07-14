using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class Meteor : Entity
{

    private List<Vector2> points = new List<Vector2>();
    private Texture2D crossTexture;
    public Meteor(Texture2D setTexture, Texture2D setCrossTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
        crossTexture = setCrossTexture;
    }

    private void CreatePoints(int amount, int minDistance, int maxDistance) {
        Random random = new Random();
        int rotationOffset = 30;
        int curRotation = 0;
        for (int i=0; i < amount; i++) {
            // generate offset
            int distance = random.Next(minDistance, maxDistance);

            curRotation += rotationOffset;
            Vector2 offset = new Vector2();

            offset.X = (float)Math.Cos(curRotation) * distance;
            offset.Y = (float)Math.Sin(curRotation) * distance;

            points.Add(offset);
        }
    }

    private void RenderPoints(SpriteBatch spriteBatch) {
        foreach (Vector2 point in points) {
            spriteBatch.Draw(crossTexture, position + point, null, Color.White, rotation, new Vector2(texture.Height / 2, texture.Width / 2), scale, SpriteEffects.None, layer);
        }
    }

    protected override void Init()
    {
        System.Console.WriteLine("init");
        CreatePoints(5, 5, 50);
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        RenderPoints(spriteBatch);
    }
}
