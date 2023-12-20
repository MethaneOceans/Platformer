using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TiledMapParser;

namespace GXPEngine
{
    public class LevelLoader : GameObject
    {
        Level currentLevel;
        
        // List that contains paths to all levels in Levels folder
        List<string> levels = new List<string>();
        bool paused = true;

        public LevelLoader() { }

        // List all levels in level folder
        public void ListLevels()
        {
            levels = Directory.EnumerateFiles(@"..\..\..\Levels", "*.xml").ToList();
            Console.WriteLine("Available levels");
            Console.WriteLine("===============================================");
            int i = 0;
            foreach (string level in levels)
            {
                Console.WriteLine(i + ": " + level);
                i++;
            }
            Console.WriteLine("===============================================");
        }

        // Let user pick a level
        public void LoadLevel()
        {
            Console.Write("Please enter the number of the level you want to play: ");
            try
            {
                int level = int.Parse(Console.ReadLine());
                LoadLevel(levels.ElementAt(level));

            }
            catch
            {
                Console.WriteLine("Input invalid. Please enter a valid number");
                LoadLevel();
            }
        }

        // Load level with set parameters
        public void LoadLevel(string path)
        {
            // Create level object and level objects to hold types of tiles
            currentLevel = new Level();
            Level terrainInLevel = new Level();
            terrainInLevel.name = "terrain";
            Level miscInLevel = new Level();
            currentLevel.AddChild(terrainInLevel);
            currentLevel.AddChild(miscInLevel);

            // Set up tiled loader
            TiledLoader loader = new TiledLoader(path, currentLevel);
            AddChild(currentLevel);

            // TODO: Make collections of objects with colliders and without
            // Loop over all layers of the map and add colliders for tile layers if necessary
            for (int i = 0; i < loader.map.Layers.Length; i++)
            {
                loader.addColliders = loader.map.Layers[i].GetBoolProperty("enableCollisions", false);
                if (loader.addColliders)
                {
                    loader.rootObject = terrainInLevel;
                }
                else
                {
                    loader.rootObject = miscInLevel;
                }
                loader.LoadTileLayers(new int[]{i});
            }

            loader.rootObject = currentLevel;
            loader.autoInstance = true;
            loader.addColliders = true;
            loader.LoadObjectGroups();

            Console.WriteLine("Level loaded");
        }

    }
}
