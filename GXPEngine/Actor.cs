using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Actor : AnimationSprite
{
    

    public Actor(String imgPath, int cols, int rows, TiledObject mySoul) : base(imgPath, cols, rows)
    {
        // mySoul is the object that this object will be based on :)

    }
}

