using UnityEngine;

namespace LineFlow.UI
{
    /// <summary>
    /// This class will be parent class of all UI panels.
    /// Contains methods to open and close panels, stacking functionality can be added in future,
    /// </summary>
    public abstract class PanelBase : MonoBehaviour
    {
        [SerializeField] private GameObject _panelRoot;
    
        public virtual void OpenPanel()
        {
            _panelRoot.SetActive(true);
        }

        public virtual void ClosePanel()
        {
            _panelRoot.SetActive(false);
        }
    }
}