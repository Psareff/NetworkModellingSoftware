using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NetworkModellingSoftware
{
    internal class Node : Border
    {
        public Point _location;
        Rectangle rectangle;
        List<Connector> inputConnectors;
        List<Connector> outputConnectors;

        public Canvas canvas;

        public Node(Point location, Workspace workspace, int inputConnectorsCount, int outputConnectorsCount)
        {
            _location = location;
            this.Name = "Node";
            this.CornerRadius = new CornerRadius(7, 7, 7, 7);
            Canvas.SetZIndex(this, -1);

            canvas = new Canvas();

            inputConnectors = new List<Connector>();
            outputConnectors = new List<Connector>();

            rectangle = new Rectangle();
            rectangle.Width = 100;
            rectangle.RadiusX = 7;
            rectangle.RadiusY = 7;
            rectangle.Fill = ColorPalette.Gray;

            for (int i = 0; i < inputConnectorsCount; i++)
                inputConnectors.Add(new Connector(ConnectorMode.Input, this, workspace));
            for (int i = 0; i < outputConnectorsCount; i++)
                outputConnectors.Add(new Connector(ConnectorMode.Output, this, workspace));


            for (int i = 0; i < inputConnectorsCount; i++)
            {
                Canvas.SetLeft(inputConnectors[i], -inputConnectors[i].Width / 2);
                Canvas.SetTop(inputConnectors[i], i * 25 + 25);
                canvas.Children.Add(inputConnectors[i]);
            }

            for (int i = 0; i < outputConnectorsCount; i++)
            {
                Canvas.SetLeft(outputConnectors[i], rectangle.Width - outputConnectors[i].Width / 2);
                Canvas.SetTop(outputConnectors[i], i * 25 + 25);
                canvas.Children.Add(outputConnectors[i]);
            }
            if (inputConnectorsCount > outputConnectorsCount)
                rectangle.Height = inputConnectorsCount * 25 + 25;
            else rectangle.Height = outputConnectorsCount * 25 + 25;

            Border infoPanel = new Border();
            infoPanel.Width = rectangle.Width;
            infoPanel.Height = 20;
            infoPanel.CornerRadius = new CornerRadius(7, 7, 0, 0);
            infoPanel.Background = ColorPalette.CoralRed;

            canvas.Width = rectangle.Width;
            canvas.Height = rectangle.Height;
            Child = canvas;
            canvas.Children.Add(rectangle);
            canvas.Children.Add(infoPanel);

        }
        internal void Select()
        {
            this.BorderBrush = Brushes.LightGray;
            this.BorderThickness = new Thickness(1);
        }
        internal void Deselect() => this.BorderThickness = new Thickness(0);

        internal void NodeAbsolutePos(Point point)
        {
            _location = point;
            foreach (Connector c in inputConnectors)
                c.UpdateConnectorPosition(point);
            foreach (Connector c in outputConnectors)
                c.UpdateConnectorPosition(point);
        }
        //{
        //    get => new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
        //}
    }
}
