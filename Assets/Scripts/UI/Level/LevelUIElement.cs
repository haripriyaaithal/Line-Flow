using LineFlow.Common;
using TMPro;
using UnityEngine;

namespace LineFlow.UI
{
    /// <summary>
    /// This class will handle user click and displaying of level number.
    /// </summary>
    public class LevelUIElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNumberText;

        private LevelDetails _levelDetails;

        public void UpdateLevelData(LevelDetails levelDetails)
        {
            if (levelDetails == null) { return; }

            _levelDetails = levelDetails;
            _levelNumberText.text = levelDetails.LevelNumber.ToString();
        }

        public void OnClickLevel()
        {
            Gameplay.Grid.Instance.Initialise(_levelDetails);
            UIManager.Instance.GetPanelFromType<LevelSelectionUIPanel>()?.ClosePanel();
            UIManager.Instance.GetPanelFromType<GameplayUIPanel>()?.OpenPanel();
        }
    }
}