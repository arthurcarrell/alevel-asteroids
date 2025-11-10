using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

class DamageIndicator : Entity
{

    private float damageTime;
    private Damage damage;
    private float damageRatio;
    public DamageIndicator(Damage setDamage, Vector2 setPosition, int setDamageTime = 1000, float setRotation = 0, float setScale = 1) : base(null, setPosition, setRotation, setScale)
    {
        damageTime = setDamageTime;
        damage = setDamage;

        /* get the damage ratio (how much damage is this above that entities base damage)
        if an entity has 20 damage and deals 20 damage then thats a ratio of 1
        if an entity has 20 damage and deals 200 damage then thats a ratio of 10
        for the sake of the text looking nice the lowest this number can go is 1 and the highest is 5
        */
        damageRatio = Math.Min(5, Math.Max(1, 1 + damage.amount / damage.source.GetDamage()/4));
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        Color damageColor = damage.type switch
        {
            DamageType.CRIT => Color.Red,
            DamageType.FIRE => Color.Orange,
            DamageType.HEAL => Color.Lime,
            _ => Color.Yellow
        };

        TextDrawer.DrawText(spriteBatch, new Text($"{damage.amount}", damageColor), position, Textures.galaticSpaceFont, damageRatio);
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