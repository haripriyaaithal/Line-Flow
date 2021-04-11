using LineFlow.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LineFlow.Gameplay
{
    /// <summary>
    /// This class is responsible for the input in the game.
    /// Handles click, drag events.
    /// </summary>
    public class InputController : SingletonMonoBehaviour<InputController>, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Camera _camera;

        #region Unity methods
        
        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main;
        }

        private void OnDestroy()
        {
            _camera = null;
        }
        
        #endregion
        
        #region DragHandler implementation

        public void OnBeginDrag(PointerEventData eventData)
        {
            var v_box = GetSelectedBox();
            if (v_box == null || !v_box.IsNode) { return; }

            LineController.Instance.StartLine(v_box);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var v_box = GetSelectedBox();
            if (v_box == null) { return; }

            LineController.Instance.UpdateLine(v_box);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var v_box = GetSelectedBox();
            if (v_box == null) { return; }

            LineController.Instance.EndLine();
        }

        #endregion

        private Box GetSelectedBox()
        {
            var v_rayCastHit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            return v_rayCastHit.collider == null ? null : v_rayCastHit.collider.GetComponent<Box>();
        }
    }
}