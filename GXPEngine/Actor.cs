using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TiledMapParser;
using static GXPEngine.Mathf;

public class Actor : AnimationSprite
{
    protected Collision collision;

    public Actor(String imgPath, int cols, int rows, TiledObject mySoul) : base(imgPath, cols, rows)
    {
        // mySoul is the object that this object will be based on :)

    }
}

public class Player : Actor
{
    private float vX = 0.0f;
    private float vY = 0.0f;

    bool canJump = false;

    private List<GameObject> theWorld;

    public Player(string imgPath, int cols, int rows, TiledObject yourSoul) : base(imgPath, cols, rows, yourSoul)
    {
        
    }

    public void Update()
    {
        // This is so ugly :(
        // TODO: Change this crap because the game should make these lists on initialization
        if (theWorld == null)
        {
            theWorld = this.parent.GetChildren();
            theWorld.Remove(this);
        }

        vY += 0.1f;

        if (Input.GetKeyDown(Key.SPACE) && canJump)
        {
            vY = -2.0f;
            canJump = false;
        }

        if (Input.GetKey(Key.D))
        {
            vX = Clamp(vX + 0.2f, -3.0f, 3.0f);
        }
        if (Input.GetKey(Key.A))
        {
            vX = Clamp(vX - 0.2f, -3.0f, 3.0f);
        }

        collision = MoveUntilCollision(vX, vY, theWorld);

        // If there is no collision then there's no need to do this
        if (collision != null)
        {
            // If player hit
            if (collision.normal.y < 0)
            {
                canJump = true;
            }
            if (collision.normal.y != 0.0f)
            {
                vY = 0.0f;
                collision.normal.y = 0;
                vX = 0.95f * vX;
                MoveUntilCollision(vX, vY, theWorld);
            }
            else if (collision.normal.x != 0.0f)
            {
                vX = 0.0f;
                collision.normal.x = 0;
                MoveUntilCollision(vX, vY, theWorld);
            }
            
            //canJump = true;
        }

        collision = null;
    }
}