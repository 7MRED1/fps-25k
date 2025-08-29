
using UnityEngine;
using System.IO;

public static class ConfigIO
{
    public static string ReadLevelJsonPath()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Configs", "level1.json");
        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        else
        {
            return DefaultLevelJson();
        }
    }

    public static string ReadWeaponsJsonPath()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Configs", "weapons.json");
        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        else
        {
            return DefaultWeaponsJson();
        }
    }

    public static void EnsureDefaultConfigs()
    {
        string configsDir = Path.Combine(Application.streamingAssetsPath, "Configs");
        if (!Directory.Exists(configsDir))
        {
            Directory.CreateDirectory(configsDir);
        }

        string levelPath = Path.Combine(configsDir, "level1.json");
        if (!File.Exists(levelPath))
        {
            File.WriteAllText(levelPath, DefaultLevelJson());
        }

        string weaponsPath = Path.Combine(configsDir, "weapons.json");
        if (!File.Exists(weaponsPath))
        {
            File.WriteAllText(weaponsPath, DefaultWeaponsJson());
        }
    }

    private static string DefaultLevelJson()
    {
        int width = 16, height = 16;
        var sb = new System.Text.StringBuilder();
        sb.Append(@"{ ""width"": ").Append(width).Append(@", ""height"": ").Append(height).Append(@", ""cells"": [ ");
        int idx = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                string type = (x == 0 || y == 0 || x == width - 1 || y == height - 1) ? "wall" : "floor";
                if (x == 2 && y == 2) type = "player";
                if ((x == 5 && y == 3) || (x == 8 && y == 7)) type = "door";
                if ((x == 4 && y == 5) || (x == 7 && y == 6) || (x == 9 && y == 3)) type = "enemy";
                sb.Append(@"{ ""type"": """).Append(type).Append(@""" }");
                idx++;
                if (idx < width * height) sb.Append(", ");
            }
        }
        return sb.ToString();
    }

    private static string DefaultWeaponsJson()
    {
        return @"{
  ""weapons"": [
    {
      ""id"": ""rifle"",
      ""displayName"": ""Rifle"",
      ""damage"": 20,
      ""fireRate"": 0.1,
      ""range"": 100,
      ""ammoCapacity"": 30
    },
    {
      ""id"": ""pistol"",
      ""displayName"": ""Pistol"",
      ""damage"": 15,
      ""fireRate"": 0.3,
      ""range"": 50,
      ""ammoCapacity"": 12
    }
  ]
}";
    }
}
