using Microsoft.Xna.Framework;

namespace alevel_asteroids;

public class StatusEffect
{
    public float timeLeft = 0;
    public float maxTimeLeft = 0;
    public Damage cause;

    public StatusEffect(Damage setCause, float setMaxTime)
    {
        timeLeft = setMaxTime;
        maxTimeLeft = setMaxTime;
        cause = setCause;
    }

    public virtual void Tick(LivingEntity entity, GameTime gameTime)
    {
        // supposed to be overriden.
    }
}