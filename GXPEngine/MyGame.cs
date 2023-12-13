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

	// Store layer IDs for debugging purposes
	private List<int> terrainLayers = new List<int>();
	private List<int> decorationLayers = new List<int>();
	private int playerLayer;
	private List<int> otherLayers = new List<int>();

	bool includeDecoration = true;
	bool includeOtherLayers = false;

	public MyGame() : base(wwidth, wheight, false, pPixelArt:true)     // Create a window
	{
		// ==========================================================================================================================================
		//		Initialize canvas
		// ==========================================================================================================================================
		
		// Create a canvas that connects to the context of MyGame
		EasyDraw canvas = new EasyDraw(wwidth, wheight);
		canvas.Clear(Color.DeepSkyBlue);
		canvas.scale = 3;
		// Add the canvas to the engine to display it:
		AddChild(canvas);



		// ==========================================================================================================================================
		//		Load level and initiate associated objects
		// ==========================================================================================================================================

		TiledLoader level0 = new TiledLoader(@"level0.xml", canvas);
		level0.autoInstance = true;
		level0.addColliders = true;

		Layer[] levelLayers = level0.map.Layers;

		// Seperate map layers in collections
		int id;
		for (int i = 0; i < levelLayers.Length; i++)
		{
			id = i;
			switch (levelLayers[i].Name)
			{
				case "terrain":
					terrainLayers.Add(id);
					break;

				case "decoration":
					decorationLayers.Add(id);
					break;

				case "player":
					playerLayer = id;
					break;

				default:
					otherLayers.Add(id);
					break;
			}
		}

		level0.LoadTileLayers(terrainLayers.ToArray());
		Console.WriteLine($"Loaded terrain layers (Amount: {terrainLayers.Count})");
		if (includeDecoration)
		{
			level0.addColliders = false;
			level0.LoadTileLayers(decorationLayers.ToArray());
			Console.WriteLine($"Loaded decoration layers (Amount: {decorationLayers.Count})");
		}
		if (includeOtherLayers)
		{
			Console.WriteLine($"Loaded miscellaneous layers (Amount: {otherLayers.Count})");
			level0.LoadTileLayers(otherLayers.ToArray());
		}

		

		level0.LoadObjectGroups(playerLayer);
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