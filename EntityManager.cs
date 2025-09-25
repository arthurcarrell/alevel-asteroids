using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public abstract class EntityManager
{

    /* Every single entity created needs to be added to this list, this list is responsible
    for making the entity render, and it calls each entities Update() and Render() function. */
    public static List<Entity> entities = new List<Entity>();
    public static Player player; // this is so the player can be accessed easily everywhere.
    public static int livingEntityCount; // this is the count of living entities so that the game doesnt spawn so many that it lags out
    
}