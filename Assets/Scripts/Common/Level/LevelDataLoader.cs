using UnityEngine;

namespace LineFlow.Common
{
    /// <summary>
    /// This class is used to read the data from the JSON file.
    /// Contains methods to get the data in the form of an object.
    /// This uses singleton pattern & proxy pattern.
    /// </summary>
    public class LevelDataLoader
    {
        private LevelData _levelData;

        private static LevelDataLoader _instance;

        private LevelDataLoader() { }

        ~LevelDataLoader()
        {
            _levelData = null;
            _instance = null;
        }
        
        public static LevelDataLoader Instance => _instance ?? (_instance = new LevelDataLoader());

        public LevelData GetData()
        {
            return _levelData ?? (_levelData = LoadDataFromJsonFile());
        }

        private LevelData LoadDataFromJsonFile()
        {
            var v_jsonTextAsset = Resources.Load<TextAsset>(Constants.LEVEL_JSON_FILE_NAME);
            if (v_jsonTextAsset == null)
            {
                Debug.LogErrorFormat("Couldn't read {0} JSON file from resources.", Constants.LEVEL_JSON_FILE_NAME);
                return null;
            }

            if (string.IsNullOrEmpty(v_jsonTextAsset.text))
            {
                Debug.LogErrorFormat("Level data JSON string is null or empty");
                return null;
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<LevelData>(v_jsonTextAsset.text);
        }
    }
}