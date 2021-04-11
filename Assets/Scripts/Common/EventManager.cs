using System.Collections.Generic;
using LineFlow.Gameplay;

namespace LineFlow.Common
{
    /// <summary>
    /// This classes is responsible for triggering events
    /// This uses singleton pattern & observer pattern.
    /// </summary>
    public class EventManager
    {
        private EventManager() { }

        ~EventManager()
        {
            _instance = null;
        }

        private static EventManager _instance;

        public static EventManager Instance => _instance ?? (_instance = new EventManager());

        #region Restart Level

        public delegate void RestartLevel();

        public event RestartLevel onRestartLevel;

        public void TriggerRestartLevel()
        {
            onRestartLevel?.Invoke();
        }

        #endregion

        #region Game Over

        public delegate void GameOver();

        public event GameOver onGameOver;

        public void TriggerGameOver()
        {
            onGameOver?.Invoke();
        }

        #endregion
        
        #region Update UI

        public delegate void UpdateUI(Dictionary<ushort, List<Box>> lineRendererDictionary);

        public event UpdateUI onUpdateUI;

        public void TriggerUpdateUI(Dictionary<ushort, List<Box>> lineRendererDictionary)
        {
            onUpdateUI?.Invoke(lineRendererDictionary);
        }

        #endregion
    }
}