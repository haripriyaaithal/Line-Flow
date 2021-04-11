using System.Collections.Generic;
using LineFlow.Common;
using UnityEngine;

namespace LineFlow.Gameplay
{
    /// <summary>
    /// This class is used to hold together boxes and nodes
    /// Contains methods to update the UI, reset UI
    /// </summary>
    public class Grid : SingletonMonoBehaviour<Grid>
    {
        [SerializeField] private Transform _nodesParent;
        [SerializeField] private List<Box> _boxes;
        [SerializeField] private List<Node> _nodePoints;
        [SerializeField] private List<Color> _colors;

        private LevelDetails _levelDetails;
        
        public void Initialise(LevelDetails levelDetails)
        {
            ResetGrid();
            _levelDetails = levelDetails;
            if (levelDetails == null) { return; }

            if (levelDetails.NodesList.Count != 5)
            {
                Debug.LogErrorFormat("Incorrect node count: {0}", levelDetails.NodesList.Count);
                return;
            }


            var v_index = 0;
            levelDetails.NodesList.ForEach(node =>
            {
                if (node == null) { return; }

                if (node.Point1.Count != 2 || node.Point2.Count != 2)
                {
                    Debug.LogErrorFormat("node.Point1.Count != 2 -> {0} | node.Point2.Count != 2 -> {1}",
                        node.Point1.Count != 2, node.Point2.Count != 2);
                    return;
                }

                UpdateNodeUI(node.NodeId, node.Point1, v_index);
                UpdateNodeUI(node.NodeId, node.Point2, v_index);
                v_index++;
            });
        }

        private void UpdateNodeUI(ushort id, List<ushort> points, int index)
        {
            var v_row = points[0];
            var v_column = points[1];

            var v_nodeUI = _nodePoints.Find(nodeUI => !nodeUI.Initialised);
            if (v_nodeUI == null)
            {
                Debug.LogErrorFormat("Uninitialised node point not found.");
                return;
            }

            v_nodeUI.NodeId = id;
            v_nodeUI.UpdateSpriteColor(_colors[index]);
            var v_box = _boxes.Find(box => box.RowNumber == v_row && box.ColumnNumber == v_column);
            if (v_box == null)
            {
                Debug.LogErrorFormat("Box with row: {0} and column: {1} not found", v_row, v_column);
                return;
            }

            SetParentAndResetTransform(v_nodeUI, v_box);
            v_box.NodeUI = v_nodeUI;
            v_box.IsNode = true;
            v_nodeUI.RowId = v_row;
            v_nodeUI.ColumnId = v_column;
            v_nodeUI.Initialised = true;
            v_nodeUI.gameObject.SetActive(true);
        }

        private void SetParentAndResetTransform(Node v_nodeUI, Box v_box)
        {
            var v_nodeTransform = v_nodeUI.transform;
            v_nodeTransform.SetParent(v_box.transform);
            v_nodeTransform.localScale = Vector3.one;
            v_nodeTransform.localRotation = Quaternion.identity;
            v_nodeTransform.localPosition = Vector3.zero;
        }

        private void ResetGrid()
        {
            _boxes.ForEach(box => box.ResetBox());
            _nodePoints.ForEach(node => node.ResetNode());

            LineController.Instance.ResetLines();
        }

        #region Unity methods

        private void OnEnable()
        {
            EventManager.Instance.onRestartLevel += OnRestartLevel;
            EventManager.Instance.onGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            EventManager.Instance.onRestartLevel -= OnRestartLevel;
            EventManager.Instance.onGameOver -= OnGameOver;
        }

        private void OnDestroy()
        {
            _boxes.Clear();
            _boxes = null;
            _nodePoints.Clear();
            _nodePoints = null;
            _colors.Clear();
            _colors = null;
        }

        #endregion

        #region Event handlers

        private void OnRestartLevel()
        {
            if (_levelDetails == null) { return; }

            Initialise(_levelDetails);
        }

        private void OnGameOver()
        {
            _nodePoints.ForEach(node =>
            {
                node.gameObject.SetActive(false);
                node.transform.SetParent(_nodesParent);
            });
        }

        #endregion
    }
}