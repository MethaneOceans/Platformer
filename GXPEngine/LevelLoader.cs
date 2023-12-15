﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class LevelLoader : GameObject
    {
        Level currentLevel;
        List<string> levels = new List<string>();
        bool paused = true;

        public LevelLoader() { }

        // List all levels in level folder so user can pick one
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

        // Let user enter the first level
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

        // Load level automatically
        public void LoadLevel(string path)
        {
            // Create level object
            currentLevel = new Level();
            TiledLoader loader = new TiledLoader(path, currentLevel);
            AddChild(currentLevel);

            // TODO: Filter layers to prevent decoration from getting hitboxes
            for (int i = 0; i < loader.map.Layers.Length; i++)
            {
                loader.addColliders = loader.map.Layers[i].GetBoolProperty("enableCollisions", false);
                loader.LoadTileLayers(new int[]{i});
            }
            
            loader.autoInstance = true;
            loader.addColliders = true;
            loader.LoadObjectGroups();

            Console.WriteLine("Level loaded");
        }

    }
}
