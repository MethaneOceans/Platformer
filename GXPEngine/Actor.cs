using GXPEngine;
using GXPEngine.Core;
using System;
using TiledMapParser;

public class Actor : AnimationSprite
{
    protected Collision collision;

    public Actor(String imgPath, int cols, int rows, TiledObject mySoul) : base(imgPath, cols, rows)
    {
        // mySoul is the object that this object will be based on :)

    }
}
