using UnityEngine;

namespace LineFlow.UI
{
    /// <summary>
    /// This class is responsible for Game over UI and handle button clicks
    /// </summary>
    public class GameOverUIPanel : PanelBase
    {
        public void OnClickPlayAgain()
        {
            ClosePanel();
            UIManager.Instance.GetPanelFromType<LevelSelectionUIPanel>()?.OpenPanel();
        }

        public void OnClickExit()
        {
            Application.Quit();
        }
    }
}