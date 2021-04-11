using System.Collections.Generic;
using LineFlow.Common;
using UnityEngine;

namespace LineFlow.UI
{
    /// <summary>
    /// This class is responsible for the UI update and interactions of level selection panel.
    /// </summary>
    public class LevelSelectionUIPanel : PanelBase
    {
        [SerializeField] private LevelUIElement _levelUIElement;
        [SerializeField] private RectTransform _gridLayoutRoot;

        private List<LevelUIElement> _levelUIList;

        #region Unity methods

        private void Awake()
        {
            _levelUIList = new List<LevelUIElement>();
            UpdateUI();
        }

        private void OnDestroy()
        {
            _levelUIList?.Clear();
            _levelUIList = null;
        }

        #endregion

        private void UpdateUI()
        {
            var v_levelData = LevelDataLoader.Instance.GetData();
            v_levelData.LevelList.ForEach(level =>
            {
                var v_levelInstance = Instantiate(_levelUIElement, _gridLayoutRoot);
                v_levelInstance.UpdateLevelData(level);
                _levelUIList.Add(v_levelInstance);
            });
        }
    }
}