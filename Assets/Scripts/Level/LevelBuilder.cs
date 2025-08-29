
using UnityEngine;
using System.Collections.Generic;

namespace Level
{
    public static class LevelBuilder
    {
        public static void BuildFromJson(string jsonText)
        {
            try
            {
                var levelData = JsonLite.Parse(jsonText);
                BuildLevel(levelData);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to parse level JSON: " + e.Message);
                BuildDefaultLevel();
            }
        }
        
        private static void BuildLevel(Dictionary<string, object> levelData)
        {
            if (!levelData.ContainsKey("width") || !levelData.ContainsKey("height") || !levelData.ContainsKey("cells"))
            {
                BuildDefaultLevel();
                return;
            }
            
            int width = (int)(double)levelData["width"];
            int height = (int)(double)levelData["height"];
            var cells = levelData["cells"] as List<object>;
            
            for (int i = 0; i < cells.Count && i < width * height; i++)
            {
                var cell = cells[i] as Dictionary<string, object>;
                if (cell == null || !cell.ContainsKey("type")) continue;
                
                string type = cell["type"] as string;
                int x = i % width;
                int z = i / width;
                
                BuildCellAtPosition(type, x, z);
            }
        }
        
        private static void BuildCellAtPosition(string type, int x, int z)
        {
            Vector3 position = new Vector3(x, 0, z);
            
            switch (type)
            {
                case "wall":
                    CreateWall(position);
                    break;
                case "floor":
                    CreateFloor(position);
                    break;
                case "player":
                    CreateFloor(position);
                    // Player spawn is handled by PlayerFactory
                    break;
                case "enemy":
                    CreateFloor(position);
                    // Enemies are spawned by EnemyFactory
                    break;
                case "door":
                    CreateDoor(position);
                    break;
            }
        }
        
        private static void CreateWall(Vector3 position)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.position = position + Vector3.up * 0.5f;
            wall.name = "Wall";
            
            var renderer = wall.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.gray;
            }
        }
        
        private static void CreateFloor(Vector3 position)
        {
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floor.transform.position = position;
            floor.transform.localScale = new Vector3(0.1f, 1f, 0.1f);
            floor.name = "Floor";
            
            var renderer = floor.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.white;
            }
        }
        
        private static void CreateDoor(Vector3 position)
        {
            GameObject door = GameObject.CreatePrimitive(PrimitiveType.Cube);
            door.transform.position = position + Vector3.up * 0.5f;
            door.transform.localScale = new Vector3(1f, 2f, 0.1f);
            door.name = "Door";
            
            var renderer = door.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.brown;
            }
        }
        
        private static void BuildDefaultLevel()
        {
            // Create a simple 16x16 level if JSON fails
            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    if (x == 0 || z == 0 || x == 15 || z == 15)
                    {
                        CreateWall(new Vector3(x, 0, z));
                    }
                    else
                    {
                        CreateFloor(new Vector3(x, 0, z));
                    }
                }
            }
        }
    }
}
