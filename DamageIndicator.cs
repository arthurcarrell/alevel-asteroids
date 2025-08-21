using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

class DamageIndicator : Entity
{

    private float damageTime;
    private Damage damage;
    private Texture2D spriteSheet;
    public DamageIndicator(Damage setDamage, Texture2D setSpriteSheet, Vector2 setPosition, int setDamageTime = 1000, float setRotation = 0, float setScale = 1) : base(null, setPosition, setRotation, setScale)
    {
        damageTime = setDamageTime;
        damage = setDamage;
        spriteSheet = setSpriteSheet;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        Color damageColor = damage.type switch
        {
            DamageType.CRIT => Color.Red,
            _ => Color.Yellow
        };

        TextDrawer.DrawText(spriteBatch, new Text($"{damage.amount}", damageColor), position, spriteSheet);
    }

    public override void Update(GameTime gameTime)
    {
        damageTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (damageTime <= 0)
        {
            Kill();
        }

        position.Y -= 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;

    }


}