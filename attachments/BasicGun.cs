namespace alevel_asteroids;

public class BasicGun : Attachment
{
    public BasicGun() : base()
    {
        name = "basic gun";
        durability = 150;
    }

    public override void OnShoot()
    {
        //EntityManager.entities.Add(new Bullet(owner.GetSpriteSheet(), owner.GetBulletTexture(), owner.g, position + Vec2Forward(rotation, 20), this, rotation));
    }
}