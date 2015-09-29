using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;

namespace Different
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

        public void SetText(string text)
        {
            TxbNum.Text = text;
        }

        public string GetText()
        {
            return TxbNum.Text;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MainArea_Click(object sender, RoutedEventArgs e)
        {
            var win = GetParentObject<MainWindow>(this, "MainWin");
            win.OneClick(this);
        }


        private T GetParentObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T && (((T)parent).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

    }
}
