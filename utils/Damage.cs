namespace alevel_asteroids;

public enum DamageType
{
    NORMAL,
    CRIT,
    FIRE,
    HEAL
}


/* 
Damage:

All damage dealt needs:
amount - The amount of damage dealt, this is so the program knows how much health to take away
type - This is mostly so the damage numbers are a certain color but they could be used to make things more resistant to other things.
source - This is so the game can run bonuses on whoever hurt the entity, so giving experience if its a player who killed is one example
procChance - the chance modifier that causes luck to be less or more likely, fire has a proc chance of 0, which means it cannot crit or set things on fire (preventing an infinite loop)

*/
public struct Damage
{
    public int amount;
    public DamageType type;
    public LivingEntity source;
    public float procChance;
}