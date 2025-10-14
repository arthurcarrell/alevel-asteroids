using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class ItemPickup : ColliderEntity
{

    List<Vector2> points = new List<Vector2>() { new Vector2(-8, -8), new Vector2(8, -8), new Vector2(-8, 8), new Vector2(8, 8), new Vector2(0, 0) };

    float trueRotation;
    public ItemPickup(Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(Textures.box, setPosition, setRotation, setScale)
    {
        trueRotation = setRotation;
    }

    protected override void Init()
    {
        SetPoints(points);
        shouldCollide = true;
        base.Init();
    }

    public override void Update(GameTime gameTime)
    {
        ColliderEntity collision = GetFirstCollider();
        isColliding = collision != null;

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        rotation += delta * EntityManager.movespeedMult;
        position += Vec2Forward(trueRotation, 50 * delta * EntityManager.movespeedMult);

        if (collision is Player player) {
            EntityManager.entities.Add(new PickupMenu(new Vector2(0,0)));
            Kill();
        }

        
        int teleport_border = Game1.TELEPORT_BORDER;


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

        base.Update(gameTime);
    }

    


}
