using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class ItemPickup : ColliderEntity
{
    public ItemPickup(Texture2D setTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
    }
}