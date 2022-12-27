using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModellingSoftware
{
    internal class ItemsArray
    {
        public List<Node> _nodes;
        public List <Connector> _connectors; 

        public ItemsArray()
        {
            _nodes = new List<Node>();
            _connectors = new List<Connector>();
        }
        public List<Node> nodes
        {
            get => _nodes;
            set => _nodes = value;
        }
        public List<Connector> connectors
        {
            get => _connectors;
            set => _connectors = value;
        }
    }
}
