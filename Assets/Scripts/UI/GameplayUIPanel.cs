using System.Collections.Generic;
using LineFlow.Common;
using LineFlow.Gameplay;
using LineFlow.UI;
using TMPro;
using UnityEngine;

namespace LineFlow
{
    /// <summary>
    /// This class is responsible for Gameplay UI and handle button clicks
    /// </summary>
    public class GameplayUIPanel : PanelBase
    {
        [SerializeField] private TextMeshProUGUI _flowText;
        [SerializeField] private TextMeshProUGUI _pipeText;

        public override void OpenPanel()
        {
            _flowText.text = $"0/{Constants.MAX_FLOW_NUMBER}";
            _pipeText.text = "0%";
            base.OpenPanel();
        }

        private void UpdateFlowText(int flow)
        {
            _flowText.text = $"{flow}/{Constants.MAX_FLOW_NUMBER}";
        }

        private void UpdatePipePercent(float percent)
        {
            _pipeText.text = $"{percent}%";
        }

        #region Unity methods

        private void OnEnable()
        {
            EventManager.Instance.onUpdateUI += OnUpdateUI;
        }

        private void OnDisable()
        {
            EventManager.Instance.onUpdateUI -= OnUpdateUI;
        }

        #endregion

        #region UI Callbacks

        public void OnClickChangeLevel()
        {
            ClosePanel();
            UIManager.Instance.GetPanelFromType<LevelSelectionUIPanel>().OpenPanel();
        }

        public void OnClickRestartLevel()
        {
            EventManager.Instance.TriggerRestartLevel();
        }

        #endregion

        private void OnUpdateUI(Dictionary<ushort, List<Box>> lineRendererDictionary)
        {
            var v_flowCount = 0;
            var v_boxFillCount = 0;
            foreach (var lineRendererData in lineRendererDictionary)
            {
                var v_nodeCount = 0;
                lineRendererData.Value.ForEach(box =>
                {
                    if (box.IsNode) { v_nodeCount++; }

                    if (box.LineId != 0) { v_boxFillCount++; }
                });
                if (v_nodeCount == 2) { v_flowCount++; }
            }

            if (v_flowCount == Constants.MAX_FLOW_NUMBER && v_boxFillCount == Constants.MAX_GRID_NUMBER)
            {
                EventManager.Instance.TriggerGameOver();
                UIManager.Instance.GetPanelFromType<GameplayUIPanel>()?.ClosePanel();
                UIManager.Instance.GetPanelFromType<GameOverUIPanel>()?.OpenPanel();
            }
            UpdateFlowText(v_flowCount);
            UpdatePipePercent((v_boxFillCount/(float)Constants.MAX_GRID_NUMBER) * 100);
        }
    }
}