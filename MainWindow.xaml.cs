using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkModellingSoftware
{
    public static class ColorPalette
    {
        public static SolidColorBrush CoralRed = (SolidColorBrush)new BrushConverter().ConvertFrom("#DA0037");
        public static SolidColorBrush DarkGray = (SolidColorBrush)new BrushConverter().ConvertFrom("#171717");
        public static SolidColorBrush Gray = (SolidColorBrush)new BrushConverter().ConvertFrom("#444444");
        public static SolidColorBrush LightGray = (SolidColorBrush)new BrushConverter().ConvertFrom("#EDEDED");

    }

    public partial class MainWindow : Window
    {
        ItemsArray _nodeArray;
        public MainWindow()
        {
            InitializeComponent();
            _nodeArray = new ItemsArray();
            workspace.Children.Add(new Server(new Point(0,0), workspace, 5, 7));

        }
    }
}
