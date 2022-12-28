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
using System.Diagnostics;

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

        }

        private void DeleteNode_Click(object sender, RoutedEventArgs e)
        {
            workspace.Children.Remove(workspace.ItemDragging);
            _nodeArray.nodes.Remove(workspace.ItemDragging as Node);
            (workspace.ItemDragging as Node).RemoveVisualConnections();
        }

        private void AddNode_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine((NetworkNodeType)Enum.Parse(typeof(NetworkNodeType), TypeChoiceHeader.Text));

            NodeInfo nodeInfo = new NodeInfo
                (
                (NetworkNodeType)Enum.Parse(typeof(NetworkNodeType), TypeChoiceHeader.Text),
                NodeNameSetter.Text,
                NodeManufacterSetter.Text,
                ModelSetter.Text,
                InputDocksSetter.Text,
                OutputDocksSetter.Text,
                BandwidthSetter.Text
                );
            workspace.Children.Add(new Node(new Point(0, 0), workspace, nodeInfo));
        }

        private void SetterTextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            TypeChoiceHeader.Text = (e.Source as MenuItem).Header.ToString();//(NetworkNodeType)Enum.Parse(typeof(NetworkNodeType),(e.Source as MenuItem).Header.ToString());
        }
    }
}
