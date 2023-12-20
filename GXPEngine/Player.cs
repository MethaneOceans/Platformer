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
        // Doing this in the constructor proves to just not work because it's dependent on values that are yet to be defined at runtime
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
                canJump = true;
            }
        }
        currentCollision = null;
        
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
        playerCamera.SetXY(playerCamera.x + (playerCamera.x - x) / -10f, playerCamera.y + (playerCamera.y - y) / -10f);
    }
}