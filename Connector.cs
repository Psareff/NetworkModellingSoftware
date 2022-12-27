using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NetworkModellingSoftware
{
    public enum ConnectorMode {Input, Output}

    internal class Connector : Border
    {
       // private Ellipse ellipse;
        private ConnectorMode connectorMode;
        private Node node;
        Ellipse ellipse;

        public Connector(ConnectorMode connectorMode, Node node)
        {
            ellipse = new Ellipse();
            ellipse.Width = 14;
            ellipse.Height= 14;
            ellipse.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#DA0037"); ;

            ellipse.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#171717");
            ellipse.StrokeThickness = 2;

            Child = ellipse;
            Canvas.SetZIndex(this, 1);
            this.connectorMode = connectorMode;
            this.node = node;
        }

        public double Width
        {
            get => ellipse.Width;
        }

        public double Height
        {
            get => ellipse.Height;
        }
    }
}
