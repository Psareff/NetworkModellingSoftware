using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Diagnostics;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;

namespace NetworkModellingSoftware
{
    internal class Workspace : Canvas
    {
        private UIElement itemClicked;
        public UIElement ItemDragging { get; set; }

        private Point _startPoint;
        private Point _currentPoint;
        private bool _isMoving;



        public Workspace()
        {
        }

        public UIElement FindChild(DependencyObject dependencyObject)
        {
            while (dependencyObject != null)
            {
                UIElement element = dependencyObject as UIElement;
                if (element != null && base.Children.Contains(element))
                    break;
                if (element is Visual || element is Visual3D)
                    dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
                else
                    dependencyObject = LogicalTreeHelper.GetParent(dependencyObject);
            }
            return dependencyObject as UIElement;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            this._isMoving = false;
            this._startPoint = e.GetPosition(this);
            this.itemClicked = null;
            this.itemClicked = this.FindChild(e.Source as DependencyObject) as UIElement;

            if (ItemDragging != null)
                (ItemDragging as Node).Deselect();
            if (itemClicked != null && itemClicked is Node)
            {
                ItemDragging = itemClicked;
                Trace.WriteLine("Node Clicked");
                (ItemDragging as Node).Select();
                ItemDragging.CaptureMouse();
                this._isMoving = true;

            }

            else if (e.Source is Workspace) foreach (Node i in Children) i.Deselect();

        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            if (ItemDragging != null)
                ItemDragging.ReleaseMouseCapture();
            this._isMoving = false;
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (_isMoving == true && e.LeftButton == MouseButtonState.Pressed)
            {
                _currentPoint = e.GetPosition(this);
                if (itemClicked != null && itemClicked is Node)
                {
                    (itemClicked as Node)._location.X += _currentPoint.X - _startPoint.X;
                    (itemClicked as Node)._location.Y += _currentPoint.Y - _startPoint.Y;

                    Canvas.SetLeft(itemClicked, (itemClicked as Node)._location.X);
                    Canvas.SetTop(itemClicked, (itemClicked as Node)._location.Y);
                    _startPoint = _currentPoint;
                    (itemClicked as Node).NodeAbsolutePos(new Point(Canvas.GetLeft(itemClicked), Canvas.GetTop(itemClicked)));
                }

            }
        }
        public MemoryStream Render()
        {
            Rect rect = new Rect(this.RenderSize);

            RenderTargetBitmap rtb = new RenderTargetBitmap(1300, 875, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(this);
            //endcode as PNG
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            //save to memory stream
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            pngEncoder.Save(ms);
            ms.Close();
            return ms;
        }
    }

}

