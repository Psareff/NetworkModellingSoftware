using Accessibility;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        private static bool _isSelected;
        private Connection _connection;
        private static Connection _tempConnection;
        private Workspace _workspace;
        public Point ConnectorAbsolutePos;

        public Connector(ConnectorMode connectorMode, Node node, Workspace workspace, int currentPosition)
        {
            ellipse = new Ellipse();
            ellipse.Width = 14;
            ellipse.Height = 14;
            ellipse.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#DA0037");

            ellipse.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#171717");
            ellipse.StrokeThickness = 2;

            _workspace = workspace;

            Child = ellipse;
            Canvas.SetZIndex(this, 1);
            switch (connectorMode)
            {
                case ConnectorMode.Input:
                    Canvas.SetLeft(this, -this.Width / 2 );
                    Canvas.SetTop(this, currentPosition * 25 + 25);
                    break;
                case ConnectorMode.Output:
                    Canvas.SetLeft(this, node.rectangle.Width - this.Width / 2);
                    Canvas.SetTop(this, currentPosition * 25 + 25);
                    break;


            }

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

        public void Activate()
        {
            ellipse.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FF00");
        }

        public void Deactivate()
        {
            ellipse.Fill = ColorPalette.CoralRed;
        }

        public void Clicked()
        {
            if (!_isSelected && this.connectorMode == ConnectorMode.Output)
            {
                Activate();
                _isSelected = true;
                _connection = _tempConnection = new Connection(_workspace);
                _workspace.Children.Add(_connection);
                _connection.startConnector = this;
            }
            else if (_isSelected && this.connectorMode == ConnectorMode.Input)
            {
                Activate();
                _isSelected = false;
                _connection = _tempConnection;
                _connection.endConnector = this;
                _connection.EndConnection();
            }

            else if (_isSelected && this.connectorMode == ConnectorMode.Output && _connection != null)
            {
                Activate();
                _isSelected = false;
            }
        }

        public void UpdateConnectorPosition(Point point)
        {
            ConnectorAbsolutePos = new Point(point.X + Canvas.GetLeft(this) + 7, point.Y + Canvas.GetTop(this) + 7);
            if (_connection != null && _connection.endConnector != null)
            {
                _connection.myPathFigure.StartPoint = _connection.startConnector.ConnectorAbsolutePos;
                _connection.myLineSegment.Point3 = _connection.endConnector.ConnectorAbsolutePos;
                _connection.myLineSegment.Point1 = new Point(_connection.myPathFigure.StartPoint.X + 100, _connection.myPathFigure.StartPoint.Y);
                _connection.myLineSegment.Point2 = new Point(_connection.myLineSegment.Point3.X - 100, _connection.myLineSegment.Point3.Y);

            }
            else if (_connection != null && _connection.endConnector == null)
            {
                ellipse.Fill = ColorPalette.CoralRed;
                _connection = null;
            }
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            base.OnPreviewMouseLeftButtonDown(e);
            Clicked();
        }

        public void DeleteVisualConnection()
        {
            if (_connection != null)
            {
                _connection.SelfDelete();
                Trace.WriteLine("Good");
            }
        }
    }
}
