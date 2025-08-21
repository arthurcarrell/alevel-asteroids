using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

/* I want accurate collisions for each object. Because each meteor has a random size and points, it would be bad
 if you died to the air or were able to enter an entity. Instead, each entity's hitbox will be defined by a series of points that
 connect together. */
public class ColliderEntity : Entity
{
    protected List<Vector2> hitboxPoints = new List<Vector2>(); // points of the hitbox (relative to the objects position.)
    protected bool shouldCollide = false; // will it actually check for collisions? Should be false until the points are given.

    protected bool isColliding;

    // textures
    public ColliderEntity(Texture2D setTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
    }

    // setters
    public void SetPoints(List<Vector2> setPoints) => hitboxPoints = setPoints;

    // getters
    public List<Vector2> GetPoints() => hitboxPoints;
    public bool GetShouldCollide() => shouldCollide;

    protected void RenderPoints(SpriteBatch spriteBatch, bool isColliding = false)
    {
        // set colour
        Color pointColor = Color.Red;
        Texture2D crossTexture = Textures.cross;
        
        if (isColliding)
        {
            pointColor = Color.Green;
        }

        foreach (Vector2 point in hitboxPoints)
        {
            spriteBatch.Draw(crossTexture, position + point, null, pointColor, 0, new Vector2(crossTexture.Width / 2f, crossTexture.Height / 2f), scale, SpriteEffects.None, layer);
        }
    }


    // simple one liner, but its used a lot so its its own function
    // conveys if the two vectors are parallel, colinear or intersecting
    private float ComputeCrossProduct(Vector2 vectorA, Vector2 vectorB)
    {
        return vectorA.X * vectorB.Y - vectorA.Y * vectorB.X;
    }

    private bool DoesOverlapOnAxis(float aa, float ab, float ba, float bb)
    {
        float minA = Math.Min(aa, ab);
        float maxA = Math.Max(aa, ab);
        float minB = Math.Min(ba, bb);
        float maxB = Math.Max(ba, bb);

        return maxA >= minB && maxB >= minA;
    }

    private bool DoLinesIntersect(Vector2 aPointA, Vector2 aPointB, Vector2 bPointA, Vector2 bPointB)
    {
        Vector2 lineSegmentA = aPointB - aPointA;
        Vector2 lineSegmentB = bPointB - bPointA;

        float denominator = ComputeCrossProduct(lineSegmentA, lineSegmentB);
        float numeratorA = ComputeCrossProduct(bPointA - aPointA, lineSegmentA);
        float numeratorB = ComputeCrossProduct(bPointA - aPointA, lineSegmentB);

        if (denominator == 0)
        {
            if (numeratorA == 0 && numeratorB == 0)
            {
                // is colinear, so check if it is overlapping
                bool doesOverlap = DoesOverlapOnAxis(aPointA.X, aPointB.X, bPointA.X, bPointB.X) && DoesOverlapOnAxis(aPointA.Y, aPointB.Y, bPointA.Y, bPointB.Y);
                return doesOverlap;
            }

            // parallel
            return false;
        }

        float intersectPointA = ComputeCrossProduct(bPointA - aPointA, lineSegmentA) / denominator;
        float intersectPointB = ComputeCrossProduct(bPointA - aPointA, lineSegmentB) / denominator;

        return intersectPointA >= 0 && intersectPointA <= 1 && intersectPointB >= 0 && intersectPointB <= 1;
    }

    private bool CheckForCollision(List<Vector2> colliderAWorldPoints, List<Vector2> colliderBWorldPoints)
    {
        // check if the edges are intersecting
        for (int i = 0; i < colliderAWorldPoints.Count; i++)
        {
            Vector2 aPointA = colliderAWorldPoints[i];
            Vector2 aPointB = colliderAWorldPoints[(i + 1) % colliderAWorldPoints.Count]; // the modulus means that it will loop to the start

            for (int b = 0; b < colliderBWorldPoints.Count; b++)
            {
                Vector2 bPointA = colliderBWorldPoints[b];
                Vector2 bPointB = colliderBWorldPoints[(b + 1) % colliderBWorldPoints.Count];

                if (DoLinesIntersect(aPointA, aPointB, bPointA, bPointB))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private List<Vector2> TransformIntoWorldCoords(ColliderEntity baseEntity)
    {
        List<Vector2> basePoints = baseEntity.GetPoints();
        List<Vector2> worldPoints = new List<Vector2>();

        float cos = (float)Math.Cos(baseEntity.rotation);
        float sin = (float)Math.Sin(baseEntity.rotation);

        foreach (Vector2 point in basePoints)
        {
            //float rotatedX = point.X * cos - point.Y * sin;
            //float rotatedY = point.X * sin + point.Y * cos;

            Vector2 newCoordinate = point + baseEntity.position;

            worldPoints.Add(newCoordinate);
        }

        return worldPoints;
    }

    protected bool IsCollidingWith(ColliderEntity colliderEntity)
    {
        // get points in World Coordinates
        List<Vector2> colliderAWorldPoints = TransformIntoWorldCoords(this);
        List<Vector2> colliderBWorldPoints = TransformIntoWorldCoords(colliderEntity);

        return CheckForCollision(colliderAWorldPoints, colliderBWorldPoints);

    }


    // checks if this sprite is colliding with any collider entity that has collision enabled

#nullable enable
    protected ColliderEntity? GetFirstCollider()
    {
        List<Entity> tempEntities = new List<Entity>(EntityManager.entities); // prevents modification during foreach loop which leads to a crash
        foreach (Entity entity in tempEntities)
        {
            if (entity is ColliderEntity && entity != this)
            {
                ColliderEntity colliderEntity = (ColliderEntity)entity;
                if (colliderEntity.GetShouldCollide())
                {
                    bool answer = IsCollidingWith(colliderEntity);
                    if (answer) { return colliderEntity; }
                }
            }
        }

        return null;
    }


}