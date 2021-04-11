using System.Collections.Generic;
using System.Linq;
using LineFlow.Common;
using UnityEngine;

namespace LineFlow.Gameplay
{
    /// <summary>
    /// This class is responsible for drawing line on the grid
    /// Has methods to start, draw, end lines, validate if lines can be drawn or not
    /// </summary>
    public class LineController : SingletonMonoBehaviour<LineController>
    {
        [SerializeField] private LineRenderer _lineRendererPrefab;

        private Dictionary<ushort, LineRenderer> _lineRendererDictionary = new Dictionary<ushort, LineRenderer>(5);

        private Dictionary<ushort, List<Box>> _lineRendererPointsDictionary =
            new Dictionary<ushort, List<Box>>(5);

        private ushort _currentNodeId;

        public void StartLine(Box box)
        {
            if (box == null || box.NodeUI == null) { return; }

            var v_nodeId = box.NodeUI.NodeId;
            if (!_lineRendererDictionary.ContainsKey(v_nodeId))
            {
                var v_lineRenderer = Instantiate(_lineRendererPrefab);
                v_lineRenderer.material.color = box.NodeUI.NodeColor;
                _lineRendererDictionary.Add(box.NodeUI.NodeId, v_lineRenderer);
                _lineRendererPointsDictionary[v_nodeId] = new List<Box>();
            }

            DeleteLine(v_nodeId);
            _lineRendererPointsDictionary[v_nodeId].Add(box);
            box.LineId = v_nodeId;
            _currentNodeId = v_nodeId;

            DrawLine();
        }

        public void UpdateLine(Box box)
        {
            if (box == null)
            {
                _currentNodeId = 0;
                return;
            }
            
            // If mouse is on some other node, don't draw line
            if (box.IsNode && box.NodeUI.NodeId != _currentNodeId)
            {
                _currentNodeId = 0;
                return;
            }

            if (!_lineRendererDictionary.ContainsKey(_currentNodeId) ||
                !_lineRendererPointsDictionary.ContainsKey(_currentNodeId))
            {
                _currentNodeId = 0;
                return;
            }

            var v_points = _lineRendererPointsDictionary[_currentNodeId];
            if (!CanAddBox(box)) { return; }

            if (!v_points.Contains(box)) { v_points.Add(box); }

            // If colliding with other lines, delete other line.
            if (box.LineId != 0 && box.LineId != _currentNodeId) { DeleteLine(box.LineId); }

            box.LineId = _currentNodeId;
            EventManager.Instance.TriggerUpdateUI(_lineRendererPointsDictionary);
            DrawLine();
        }

        public void EndLine()
        {
            _currentNodeId = 0;
            EventManager.Instance.TriggerUpdateUI(_lineRendererPointsDictionary);
        }

        private void DrawLine()
        {
            foreach (var lineRendererData in _lineRendererDictionary)
            {
                var v_lineRenderer = lineRendererData.Value;
                var v_points = _lineRendererPointsDictionary[lineRendererData.Key];
                v_lineRenderer.positionCount = v_points.Count;
                for (var v_index = 0; v_index < v_points.Count; v_index++)
                {
                    var v_centerPoint = new Vector3(v_points[v_index].CenterPoint.x, v_points[v_index].CenterPoint.y,
                        -1);
                    lineRendererData.Value.SetPosition(v_index, v_centerPoint);
                }
            }
        }

        private void DeleteLine(ushort id)
        {
            if (_lineRendererDictionary.ContainsKey(id)) { _lineRendererDictionary[id].positionCount = 0; }

            if (!_lineRendererPointsDictionary.ContainsKey(id)) { return; }

            _lineRendererPointsDictionary[id].ForEach(box => box.LineId = 0);
            _lineRendererPointsDictionary[id].Clear();
        }

        private bool CanAddBox(Box box)
        {
            var v_previousBox = _lineRendererPointsDictionary[_currentNodeId].LastOrDefault();
            if (v_previousBox == null) { return true; }

            // If 2 nodes are found in the points list, that means that flow is completed.
            var v_nodeCount = 0;
            _lineRendererPointsDictionary[_currentNodeId].ForEach(boxEntry =>
            {
                if (boxEntry.IsNode) { v_nodeCount++; }
            });

            if (v_nodeCount >= 2) { return false; }

            // Avoid diagonal movement of lines.
            var v_validBoxes = new (int, int)[]
            {
                (box.RowNumber - 1, box.ColumnNumber),
                (box.RowNumber + 1, box.ColumnNumber),
                (box.RowNumber, box.ColumnNumber - 1),
                (box.RowNumber, box.ColumnNumber + 1)
            };

            foreach (var (row, column) in v_validBoxes)
            {
                if (v_previousBox.RowNumber == row && v_previousBox.ColumnNumber == column) { return true; }
            }

            return false;
        }

        public void ResetLines()
        {
            foreach (var v_lineId in _lineRendererDictionary.Keys) { DeleteLine(v_lineId); }
        }


        private void OnDestroy()
        {
            _lineRendererDictionary.Clear();
            _lineRendererDictionary = null;
            _lineRendererPointsDictionary.Clear();
            _lineRendererPointsDictionary = null;
        }
    }
}