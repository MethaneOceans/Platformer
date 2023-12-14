using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using TiledMapParser;
using System.Collections.Generic;
using GXPEngine.OpenGL;
using System.Drawing.Text;

public class MyGame : Game {
	// Support for scaling window not implemented
	// TODO: Block user from scaling the window | Reason: X/Y scale breaks after resize
	// TODO: Make game fullscreen
	private static readonly int wwidth = 1280;
	private static readonly int wheight = 720;

	private LevelLoader levelLoader;

	public MyGame() : base(wwidth, wheight, false, pPixelArt:true)     // Create a window
	{
		// Sets clear color for background
		GL.ClearColor(Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B, 255);

		// Sprites are too small so scale up the game
		scale = 3;

		levelLoader = new LevelLoader();
		AddChild(levelLoader);
		levelLoader.ListLevels();
		levelLoader.LoadLevel();
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