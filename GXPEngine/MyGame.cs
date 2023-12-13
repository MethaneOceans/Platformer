using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using TiledMapParser;
using System.Collections.Generic;

public class MyGame : Game {
	// Support for scaling window not implemented
	// TODO: Block user from scaling the window | Reason: X/Y scale breaks after resize
	private static readonly int wwidth = 1280;
	private static readonly int wheight = 720;

    private List<Layer> terrainLayers = new List<Layer>();
    private List<Layer> decorationLayers = new List<Layer>();
    private Layer playerLayer;
    private List<Layer> otherLayers = new List<Layer>();

    public MyGame() : base(wwidth, wheight, false, pPixelArt:true)     // Create a window
	{
        // ==========================================================================================================================================
		// 
		//		Initialize canvas
		// 
        // ==========================================================================================================================================
        
		// Create a canvas that connects to the context of MyGame
        EasyDraw canvas = new EasyDraw(wwidth, wheight);
		canvas.Clear(Color.DeepSkyBlue);
		// Add the canvas to the engine to display it:
		AddChild(canvas);



		// ==========================================================================================================================================
		//
		//		Load level and initiate associated objects
		//
		// ==========================================================================================================================================

		TiledLoader level0 = new TiledLoader(@"..\..\..\TiledFiles\level0.xml");
		level0.autoInstance = true;
		level0.addColliders = true;

		Layer[] levelLayers = level0.map.Layers;

		

		for (int i = 0; i < levelLayers.Length; i++)
		{
			switch (levelLayers[i].GetStringProperty("Class", null))
			{
				case "terrain":
					terrainLayers.Add(levelLayers[i]);
					break;

				case "decoration":
					decorationLayers.Add(levelLayers[i]);
					break;

				case "player":
					playerLayer = levelLayers[i];
					break;

				default:
					otherLayers.Add(levelLayers[i]);
					break;
			}
		}
    }

    // For every game object, Update is called every frame, by the engine:
    void Update() {
		// Empty
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}