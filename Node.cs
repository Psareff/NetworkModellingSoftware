using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NetworkModellingSoftware
{
    internal class Node : Border
    {
        public Point _location;

        public Node (Point location, int inputConnectorsCount, int outputConnectorsCount)
        {
            _location = location;
            this.Name = "Node";
            this.CornerRadius = new CornerRadius(7, 7, 7, 7);

        }
        internal void Select()
        {
            this.BorderBrush = Brushes.LightGray;
            this.BorderThickness = new Thickness(1);
        }
        internal void Deselect() => this.BorderThickness = new Thickness(0);
    }
}
