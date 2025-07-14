using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public static class EntityManager {

    /* Every single entity created needs to be added to this list, this list is responsible
    for making the entity render, and it calls each entities Update() and Render() function. */
    public static List<Entity> entities = new List<Entity>();
    
}