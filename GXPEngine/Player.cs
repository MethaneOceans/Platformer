using GXPEngine;
using GXPEngine.Core;
using System.Collections.Generic;
using TiledMapParser;
using static GXPEngine.Mathf;
public class Player : Actor
{
    Camera playerCamera;

    private float vX = 0.0f;
    private float vY = 0.0f;

    private List<GameObject> terrainList;
    Collision currentCollision;

    private bool canJump = false;
    private bool initialized = false;

    public Player(string imgPath, int cols, int rows, TiledObject yourSoul) : base(imgPath, cols, rows, yourSoul)
    {

    }

    public void Update()
    {
        // Doing this in the constructor proves to just not work because it's dependent on values that are yet to be initialized
        // #GXPengine things!
        if (!initialized)
        {
            List<GameObject> list = this.parent.GetChildren();
            foreach (GameObject go in list)
            {
                if (go.name == "terrain")
                {
                    terrainList = go.GetChildren();
                }
            }

            playerCamera = new Camera(0, 0, 1920, 1080);
            playerCamera.scale = 1 / 3.0f;
            parent.AddChild(playerCamera); // Camera should not follow player directly

            initialized = true;
        }
        handleInput();

        vY += 0.15f;

        // Crap still causes getting stuck
        currentCollision = MoveUntilCollision(vX, 0);
        if (currentCollision != null)
        {
            vX = 0.0f;
        }
        currentCollision = null;
        currentCollision = MoveUntilCollision(0, vY);
        if (currentCollision != null)
        {
            vY = 0.0f;
            vX *= 0.9f;
            if (currentCollision.normal.y < 0)
            {
                // This movement change is a very sketchy fix to the player getting stuck on the floor sometimes
                // I want to fix it but I don't think I'll have the energy/brainpower for now to do that
                y += currentCollision.normal.y * 0.001f;
                canJump = true;
            }
        }
        currentCollision = null;

        // Move player for a total of vX and vY taking in consideration collisions
        // Move vY
        // If player collides it's with a ceiling or floor
        // vX *= 0.9f
        // vY = 0.0f
        // If normal < 0 the player is on the ground
        // canJump = true
        // currentCollision = null
        // Move vX
        // If player collides it's with a wall
        // vX = 0.0f
        // In some weird cases however the player collides with the floor causing the player to not move left or right
        // If normal.x < 0.1f && normal.x > -0.1f
        // Just move?




    }

    private void handleInput()
    {
        // TODO: Replace with command structure
        if (Input.GetKey(Key.A))
        {
            vX = Clamp(vX - 0.5f, -2.5f, 2.5f);
        }
        if (Input.GetKey(Key.D))
        {
            vX = Clamp(vX + 0.5f, -2.5f, 2.5f);
        }
        if (Input.GetKeyDown(Key.SPACE) && canJump)
        {
            vY = -3.0f;
            canJump = false;
        }

        // "Smooth" camera movement
        // ngl feels kinda nice to me
        playerCamera.SetXY(playerCamera.x + (playerCamera.x - x) / -10f, playerCamera.y + (playerCamera.y - y) / -10f); 
    }
}