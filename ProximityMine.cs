using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class ProximityMine : ColliderEntity
{

    public float armTime = 1000; // ms - counts down to zero and then activates
    public float explosionTime = 200;
    public bool hasExploded = false;
    public List<ColliderEntity> alreadyHit = new List<ColliderEntity>();

    List<Vector2> points = new List<Vector2>() { new Vector2(-8, -8), new Vector2(8, -8), new Vector2(-8, 8), new Vector2(8, 8) };
    public Damage explosionDamage;
    public ProximityMine(Vector2 setPosition, Damage setExplosionDamage, float setRotation = 0, float setScale = 1) : base(Textures.proximityMine, setPosition, setRotation, setScale)
    {
        explosionDamage = setExplosionDamage;
    }

    protected override void Init()
    {
        Random rnd = new Random();
        rotation = 6.283185f * (float)rnd.NextDouble();
        shouldCollide = false;
        SetPoints(points);
        base.Init();
    }

    public override void Update(GameTime gameTime)
    {
        // subtract armTime;
        if (armTime > 0) {
            armTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        } else {
            // now do collisions - This is quite expensive!

            // Get all colliders.
            List<ColliderEntity> collisions = GetAllColliders();
            isColliding = collisions.Count > 0;

            // If colliding with a LivingEntity and havent exploded:
            if (isColliding && !hasExploded && collisions.Any(entity => entity is LivingEntity)) {
                texture = Textures.explosion;
                points = new List<Vector2>() { new Vector2(-64, -64), new Vector2(64, -64), new Vector2(-64, 64), new Vector2(64, 64) };
                SetPoints(points);
                hasExploded = true;
            }

            // Do damage to each collision.
            foreach (ColliderEntity collision in collisions) {
                if (collision is LivingEntity livingEntity && !alreadyHit.Contains(collision) && livingEntity != explosionDamage.source) {
                    livingEntity.DoDamage(explosionDamage);
                    alreadyHit.Add(collision);
                }
            }

            // increase the size of the explosion
            if (hasExploded) {
                scale += 0.05f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (hasExploded) {
                explosionTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (explosionTime <= 0) {
                    Kill();
                }
            }
        }
        base.Update(gameTime);
    }
}
