using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

/* 
                THE DIRECTOR

The director is reponsible for the flow of the game.
It is in charge of spawning enemies and causing the difficulty to increase.
*/
public class Director : Entity
{

    private float credits = 0;
    private float spawnWait = 0;
    private int maxCredits = 5_000;

    private const int ENTITY_CAP = 70;

    private float difficultyModifier = 1f;


    // Settings. These can be changed to make the game easier/harder.
    // totalDifficultyIncreaseTime is the amount of milliseconds it takes for the game difficulty to increase.
    // difficultyIncreaseAmount is the amount in which the difficulty increases by.
    private float difficultyIncreaseTime = 0f;
    private float totalDifficultyIncreaseTime = 10_000f; // 5 seconds
    private float difficultyIncreaseAmount = 0.15f;
    public Director(Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(null, setPosition, setRotation, setScale)
    {
        shouldRender = false;
        EntityManager.director = this;
    }

    public float GetDifficultyModifier() => difficultyModifier;

    // Gains and spends points to summon enemies.

    public override void Render(SpriteBatch spriteBatch)
    {
        // dont render anything
    }

    // update difficulty
    private void DoDifficultyUpdate(float milliseconds) {

        // add to difficultyIncreaseTime
        difficultyIncreaseTime += milliseconds;

        // Check if difficultyIncreaseTime is higher or equal to totalDifficultyIncreaseTime, if so then reset and add to modifier.
        if (difficultyIncreaseTime >= totalDifficultyIncreaseTime) {
            difficultyIncreaseTime = 0;
            difficultyModifier += difficultyIncreaseAmount;
            Console.WriteLine($"Difficulty increased: {difficultyModifier}");
        }
    }

    private void DoSpawningUpdate(float milliseconds) {
        credits += (milliseconds / 10) * difficultyModifier;
        spawnWait -= milliseconds;

        if (credits > maxCredits) {
            credits = maxCredits;
        }

        //Console.WriteLine(credits);

        if (spawnWait <= 0) {
            int amountToAttemptSpawn = Chance.Range(1,5);
            spawnWait = Chance.Range(100,10000);
            Console.WriteLine($"attempting to spawn {amountToAttemptSpawn} enemies:");
            for (int i=0; i < amountToAttemptSpawn; i++) {
                bool result = AttemptSpawn();
                if (result == true) {
                    Console.WriteLine("Successfully spawned enemy");
                } else {
                    if (EntityManager.livingEntityCount <= ENTITY_CAP) {
                        Console.WriteLine("Failed enemy spawn: not enough credits");
                    } else {
                        Console.WriteLine("Failed enemy spawn: too many LivingEntities");
                    }
                    
                }
            }
        }
    }

    private bool AttemptSpawn() {
        // create a meteor

        int meteorCost = 250;
        if (credits >= meteorCost && EntityManager.livingEntityCount <= ENTITY_CAP) {

            // go for a random size
            int attemptSize = Chance.Range(1,4);
            int spawnSize = (int)Math.Min(Math.Floor(credits/meteorCost), attemptSize);


            Meteor meteor = new Meteor(Textures.pixel, new Vector2(0,0), spawnSize);

            meteor.position.Y = Chance.Range(0, Game1.WINDOW_HEIGHT);

            if (Chance.Range(0, 1) == 1)
            {
                meteor.position.X = 0 - Game1.TELEPORT_BORDER;
            }
            else
            {
                meteor.position.X = Game1.WINDOW_WIDTH + Game1.TELEPORT_BORDER;
            }

            EntityManager.entities.Add(meteor);

            credits -= spawnSize * meteorCost;
            Console.WriteLine($"LivingEntity count: {EntityManager.livingEntityCount}");
            return true;
        }

        return false;
    }

    public override void Update(GameTime gameTime)
    {
        // get milliseconds
        float milliseconds = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        // run updates
        DoDifficultyUpdate(milliseconds);
        DoSpawningUpdate(milliseconds);
    }

}