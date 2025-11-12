using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class Items {

    // List of all items
    private static List<Item> allItems = new List<Item>();

    // Getter/Setter
    public static List<Item> GetItemList() => allItems;
    public static void AddToItemList(Item item) { allItems.Add(item); }

    // COMMON
    public static Item GASOLINE = new Item("Gasoline", Tier.COMMON, Textures.itemGasoline, new Text("+10% chance to ignite enemy on hit"));
    public static Item MAGNIFYING_GLASS = new Item("Magnifying Glass", Tier.COMMON, Textures.itemMagnifyingGlass, new Text("+10% chance to deal double damage on hit"));
    public static Item BROKEN_PLATE = new Item("Broken Plate", Tier.COMMON, Textures.itemBrokenPlate, new Text("+50 Max Health"));
    public static Item ION_BOOSTER = new Item("Ion Booster", Tier.COMMON, Textures.itemIonBooster, new Text("Gain attack speed"));
    public static Item VAMPIRIC_NANITES = new Item("Vampiric Nanites", Tier.COMMON, Textures.itemVampiricNanites, new Text("Heal +1 HP on hit"));
    public static Item PROXIMITY_MINES = new Item("Proximity Mines", Tier.COMMON, Textures.itemProximityMines, new Text("Chance to leave a mine on enemy kill"));
    
    // RARE
    public static Item MISSILE_LAUNCHER = new Item("Missile Launcher", Tier.RARE, Textures.itemMissileLauncher, new Text("5% Chance to shoot a missile on hit"));
    public static Item ROCKET_MAGAZINE = new Item("Rocket Magazine", Tier.RARE, Textures.itemRocketMagazine, new Text("Bullets gain damage the longer they fly"));
}