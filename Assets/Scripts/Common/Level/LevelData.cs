using System.Collections.Generic;
using Newtonsoft.Json;

namespace LineFlow.Common
{
    /// <summary>
    /// Level data read from JSON file will be deserialised to this class.
    /// </summary>
    public class LevelData
    {
        [JsonProperty("level_details")] private List<LevelDetails> _levelList;

        public List<LevelDetails> LevelList => _levelList;
    }


    public class LevelDetails
    {
        [JsonProperty("level_number")] private ushort _levelNumber;
        [JsonProperty("nodes")] private List<NodePoints> _nodesList;

        public ushort LevelNumber => _levelNumber;
        public List<NodePoints> NodesList => _nodesList;
    }


    public class NodePoints
    {
        [JsonProperty("node_id")] private ushort _nodeId;
        [JsonProperty("point1")] private List<ushort> _point1;
        [JsonProperty("point2")] private List<ushort> _point2;

        public ushort NodeId => _nodeId;
        public List<ushort> Point1 => _point1;
        public List<ushort> Point2 => _point2;
    }
}