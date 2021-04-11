using LineFlow.UI;
using UnityEngine;

namespace LineFlow
{
    public class GameInitialiser : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60; 
            var v_levelSelectionPanel = UIManager.Instance.GetPanelFromType<LevelSelectionUIPanel>();
            if (v_levelSelectionPanel == null)
            {
                Debug.LogError("Level selection UI panel is not found");
                return;
            }
            v_levelSelectionPanel.OpenPanel();
        }
    }
}