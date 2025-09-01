namespace alevel_asteroids;

public class Item
{
    public string name;
    public Item(string setName)
    {
        name = setName;
    }
}

// Items

public class Items
{
    public static Item TargettingScope = new Item("TargettingScope");
    public static Item Gasoline = new Item("Gasoline");
}