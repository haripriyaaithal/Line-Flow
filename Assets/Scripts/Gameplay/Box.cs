using LineFlow.Common;
using UnityEngine;

namespace LineFlow.Gameplay
{
    /// <summary>
    /// This class will be used to store the state of individual boxes in the grid.
    /// </summary>
    public class Box : MonoBehaviour
    {
        [SerializeField] private ushort _rowNumber;
        [SerializeField] private ushort _columnNumber;

        private SpriteRenderer _spriteRenderer;
        private Node _node;
        private ushort _lineId;
        public bool IsNode { get; set; }
        public Node NodeUI { get; set; }

        public ushort RowNumber => _rowNumber;
        public ushort ColumnNumber => _columnNumber;

        public ushort LineId
        {
            get => _lineId;
            set
            {
                _lineId = value;
                _spriteRenderer.color = Constants.GetColor(_lineId);
            }
        }

        public Vector2 CenterPoint => _spriteRenderer.bounds.center;

        #region Untiy methods

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDestroy()
        {
            _spriteRenderer = null;
            NodeUI = null;
        }

        #endregion

        public void ResetBox()
        {
            IsNode = false;
            NodeUI = null;
            LineId = 0;
        }
        
    }
}