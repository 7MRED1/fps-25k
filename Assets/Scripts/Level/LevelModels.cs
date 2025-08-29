
using System.Collections.Generic;

namespace Level
{
    [System.Serializable]
    public class LevelData
    {
        public int width;
        public int height;
        public List<CellData> cells;
    }
    
    [System.Serializable]
    public class CellData
    {
        public string type;
        public Dictionary<string, object> properties;
        
        public CellData()
        {
            properties = new Dictionary<string, object>();
        }
    }
    
    public static class CellTypes
    {
        public const string Wall = "wall";
        public const string Floor = "floor";
        public const string Player = "player";
        public const string Enemy = "enemy";
        public const string Door = "door";
        public const string Item = "item";
    }
}
