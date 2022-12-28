using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModellingSoftware
{
    internal class ItemsArray
    {
        public List <Node> _nodes;
        public List <Connector> _connectors; 
        public List <Connection> _connections;

        public ItemsArray()
        {
            _nodes = new List<Node>();
            _connectors = new List<Connector>();
            _connections = new List<Connection>();
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
        public List<Connection> connections
        {
            get => _connections;
            set => _connections = value;
        }
    }
}
