using System;

public class Chance
{
    private static Random random = new Random();
    public static bool Percentage(float percentage, float proc_chance=1) {
        return random.Next(1, 100 + 1) >= 100 - (percentage * proc_chance * 100);
    }
}