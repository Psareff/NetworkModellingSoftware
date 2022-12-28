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
    internal class Connection : UIElement
    {
        public Line line;

        #region Bezier
        public PathFigure myPathFigure;
        public BezierSegment myLineSegment;
        PathSegmentCollection myPathSegmentCollection;
        PathFigureCollection myPathFigureCollection;
        PathGeometry myPathGeometry;
        Path myPath;
        #endregion
        Workspace _workspace;
        public Connector startConnector
        {
            get;set;
        }
        public Connector endConnector
        {
            get; set;
        }
        public Connection(Workspace workspace)
        {
            line = new Line();
            _workspace = workspace;
        }
        /*
        public void EndConnection()
        {

            line.StrokeThickness = 3;
            line.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#777777");

            line.X1 = startConnector.ConnectorAbsolutePos.X;
            line.Y1 = startConnector.ConnectorAbsolutePos.Y;
            line.X2 = endConnector.ConnectorAbsolutePos.X;
            line.Y2 = endConnector.ConnectorAbsolutePos.Y;

            Canvas.SetZIndex(line, -2);

            _workspace.Children.Add(line);
            
            Trace.WriteLine("Connection created. Start connector: " + startConnector.ConnectorAbsolutePos + " End connector: " + endConnector.ConnectorAbsolutePos);
        }
        */
        public void EndConnection()
        {

            myPathFigure = new PathFigure();
            myPathFigure.StartPoint = startConnector.ConnectorAbsolutePos;

            myLineSegment = new BezierSegment();

            myLineSegment.Point3 = endConnector.ConnectorAbsolutePos;
            myLineSegment.Point1 = new Point(myPathFigure.StartPoint.X + 100, myPathFigure.StartPoint.Y);
            myLineSegment.Point2 = new Point(myLineSegment.Point3.X - 100, myLineSegment.Point3.Y);


            myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myLineSegment);

            myPathFigure.Segments = myPathSegmentCollection;

            myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            myPath = new Path();
            myPath.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#444444");
            myPath.StrokeThickness = 4;
            myPath.Data = myPathGeometry;

            Canvas.SetZIndex(myPath, -2);

            _workspace.Children.Add(myPath);

            Trace.WriteLine("Connection created. Start connector: " + startConnector.ConnectorAbsolutePos + " End connector: " + endConnector.ConnectorAbsolutePos);
        }

    }
}
 