namespace alevel_asteroids;

public class StatusEffect
{
    public float timeLeft = 0;
    public float maxTimeLeft = 0;
    public LivingEntity owner;

    public StatusEffect(LivingEntity setOwner, float setMaxTime, float setTimeLeft)
    {
        timeLeft = setMaxTime;
        maxTimeLeft = setMaxTime;
        owner = setOwner;
    }

    public virtual void Tick(LivingEntity entity)
    {
        // supposed to be overriden.
    }
}