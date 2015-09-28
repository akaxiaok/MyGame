using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace White
{
    /// <summary>
    /// Tile.xaml 的交互逻辑
    /// </summary>
    public partial class Tile : UserControl
    {
        public Color Color { get; private set; }

        public Tile()
        {
            InitializeComponent();
        }

        public void SetColor(Color color)
        {
            MainArea.Background = new SolidColorBrush(color);
            Color = color;
        }

         

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
