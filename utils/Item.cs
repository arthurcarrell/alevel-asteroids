using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public class Item {

    private readonly string name;
    private readonly Tier tier;
    private readonly Texture2D texture;
    private readonly Text description;
    public Item(string getName, Tier getTier, Texture2D getTexture, Text getDescription) {
        name = getName;
        tier = getTier;
        texture = getTexture;
        description = getDescription;

        Items.AddToItemList(this);
    }

    // Getters
    public string GetName() => name;
    public Tier GetTier() => tier;
    public Texture2D GetTexture() => texture;
    public Text GetDescription() => description;
}