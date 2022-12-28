using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NetworkModellingSoftware
{
    public enum NetworkNodeType { PROVIDER, CLIENT, HUB, BRIDGE, ROUTER, SWITCH, SERVER }

    public interface IPrototype<T>
    {
        public T CreateDeepCopy(T element);
    }

    [Serializable]
    internal class Node : Border, IPrototype<Node>
    {
        #region LogicalVariables
        public Point _location;
        public Rectangle rectangle;
        List<Connector> inputConnectors;
        List<Connector> outputConnectors;
        Workspace _workspace;
        Border _infoPanel;
        #endregion

        #region InformationalVariables
        NodeInfo _nodeInfo;
            TextBlock _typeAndName, _manufacter, _model, _bandwidth;
        #endregion

        public Canvas _nodeContents;

        public Node(Point location, Workspace workspace, NodeInfo nodeInfo)
        {
            #region
            _location = location;
            this.Name = "Node";
            this.CornerRadius = new CornerRadius(7, 7, 7, 7);
            Canvas.SetZIndex(this, -1);

            _nodeContents = new Canvas();
            _nodeInfo = nodeInfo;

            inputConnectors = new List<Connector>();
            outputConnectors = new List<Connector>();

            rectangle = new Rectangle();
            rectangle.Width = 150;
            rectangle.RadiusX = 7;
            rectangle.RadiusY = 7;
            rectangle.Fill = ColorPalette.Gray;

            _workspace = workspace;

            for (int i = 0; i < _nodeInfo.inputDocksQty; i++)
            {
                inputConnectors.Add(new Connector(ConnectorMode.Input, this, workspace, i));
                _nodeContents.Children.Add(inputConnectors[i]);

            }
            for (int i = 0; i < _nodeInfo.outputDocksQty; i++)
            {
                outputConnectors.Add(new Connector(ConnectorMode.Output, this, workspace, i));
                _nodeContents.Children.Add(outputConnectors[i]);

            }

            if (_nodeInfo.inputDocksQty > _nodeInfo.outputDocksQty)
                rectangle.Height = _nodeInfo.inputDocksQty * 25 + 25;
            else rectangle.Height = _nodeInfo.outputDocksQty * 25 + 25;
            rectangle.MinHeight = 100;
            this.Height = rectangle.Height;
            this.MinHeight = rectangle.MinHeight;

            _infoPanel = new Border();
            _infoPanel.Width = rectangle.Width;
            _infoPanel.Height = 20;
            _infoPanel.CornerRadius = new CornerRadius(7, 7, 0, 0);
            _infoPanel.Background = ColorPalette.CoralRed;

            _nodeContents.Width = rectangle.Width;
            _nodeContents.Height = rectangle.Height;
            Child = _nodeContents;
            _nodeContents.Children.Add(rectangle);
            _nodeContents.Children.Add(_infoPanel);
            #endregion

            _typeAndName = new TextBlock();
            _typeAndName.Text = _nodeInfo.type + " : " + _nodeInfo.name;
            _typeAndName.Foreground = ColorPalette.LightGray;
            _typeAndName.FontWeight = FontWeights.Bold;
            Canvas.SetTop(_typeAndName, 0);
            Canvas.SetLeft(_typeAndName, 7);
            _nodeContents.Children.Add(_typeAndName);

            _manufacter = new TextBlock();
            _manufacter.Text = "Manufacter: " + _nodeInfo.manufacter;
            _manufacter.Foreground = ColorPalette.LightGray;
            Canvas.SetTop(_manufacter, 25);
            Canvas.SetLeft(_manufacter, 7);
            _nodeContents.Children.Add(_manufacter);

            _model = new TextBlock();
            _model.Text = "Model: " + _nodeInfo.model;
            _model.Foreground = ColorPalette.LightGray;
            Canvas.SetTop(_model, 50);
            Canvas.SetLeft(_model, 7);
            _nodeContents.Children.Add(_model);

            _bandwidth = new TextBlock();
            _bandwidth.Text = "Bandwidth: " + _nodeInfo.bandwidth + "MB/s";
            _bandwidth.Foreground = ColorPalette.LightGray;
            Canvas.SetTop(_bandwidth, 75);
            Canvas.SetLeft(_bandwidth, 7);
            _nodeContents.Children.Add(_bandwidth);
        }
        internal void Select()
        {
            _infoPanel.BorderBrush = Brushes.LightGray;
            _infoPanel.BorderThickness = new Thickness(1,1,1,0);
            rectangle.Stroke = Brushes.LightGray;
            rectangle.StrokeThickness = 1;
        }
        internal void Deselect()
        {
            _infoPanel.BorderThickness = new Thickness(0);
            rectangle.StrokeThickness = 0;

        }

        public Node CreateDeepCopy(Node node)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, node);
                stream.Position = 0;
                return (Node)formatter.Deserialize(stream);
            }
        }

        internal void NodeAbsolutePos(Point point)
        {
            _location = point;
            foreach (Connector c in inputConnectors)
                c.UpdateConnectorPosition(point);
            foreach (Connector c in outputConnectors)
                c.UpdateConnectorPosition(point);
        }

        public void RemoveVisualConnections()
        {
            foreach (Connector c in inputConnectors)
                c.DeleteVisualConnection();
            foreach (Connector c in outputConnectors)
                c.DeleteVisualConnection();
        }
 
      
    }
}
