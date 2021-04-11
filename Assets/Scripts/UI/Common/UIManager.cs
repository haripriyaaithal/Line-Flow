using System.Collections.Generic;
using LineFlow.Common;
using UnityEngine;

namespace LineFlow.UI
{
    /// <summary>
    /// This class will hold reference for all the UI panels.
    /// Has method to search through all the panels which inherit from PanelBase class & is assigned to the list.
    /// </summary>
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [SerializeField] private List<PanelBase> _uiPanels;

        public T GetPanelFromType<T>() where T : PanelBase
        {
            return (T) _uiPanels.Find(panel => panel.GetType() == typeof(T));
        }
    }
}