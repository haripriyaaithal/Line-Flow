using UnityEngine;

namespace LineFlow.Gameplay
{
    /// <summary>
    /// This class is used to represent start and end of the line in the grid.
    /// </summary>
    public class Node : MonoBehaviour
    {
        [SerializeField] private ushort _rowId;
        [SerializeField] private ushort _columnId;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private ushort _nodeId;

        public ushort NodeId
        {
            get => _nodeId;
            set => _nodeId = value;
        }
        
        public ushort RowId
        {
            get => _rowId;
            set => _rowId = value;
        }

        public ushort ColumnId
        {
            get => _columnId;
            set => _columnId = value;
        }

        public bool Initialised { get; set; }

        public Color NodeColor => _spriteRenderer.color;
        
        public void UpdateSpriteColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void ResetNode()
        {
            Initialised = false;
            UpdateSpriteColor(Color.white);
            NodeId = 0;
        }
    }
}