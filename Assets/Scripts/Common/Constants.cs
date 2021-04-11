using UnityEngine;

namespace LineFlow.Common
{
    /// <summary>
    /// This class will contains all the constants variables
    /// </summary>
    public class Constants
    {
        public static readonly string LEVEL_JSON_FILE_NAME = "LevelData";
        public static readonly int MAX_FLOW_NUMBER = 5;
        public static readonly int MAX_GRID_NUMBER = 25;
        public static Color GetColor(ushort id)
        {
            switch (id)
            {
                case 1:
                    return Color.red;
                case 2:
                    return Color.green;
                case 3:
                    return Color.blue;
                case 4:
                    return Color.yellow;
                case 5:
                    return Color.cyan;
            }

            return Color.white;
        }
    }
}