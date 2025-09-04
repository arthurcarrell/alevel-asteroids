using System;
using System.Collections.Generic;

public class Chance
{
    private static Random random = new Random();
    public static bool Percentage(float percentage, float proc_chance=1) {
        return random.Next(1, 100 + 1) > 100 - (percentage * proc_chance * 100);
    }

    public static T Item<T>(List<T> items) {
        return items[random.Next(0, items.Count)];
    }
}