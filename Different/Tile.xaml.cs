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

        public Tile()
        {
            InitializeComponent();
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
            var win = GetParentObject<MainWindow>(this, "MainWin");//通过name找父控件
            win.OneClick(this);//主窗口MaiWin处理一次点击事件
        }

        /// <summary>
        /// 通过name找父控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">源控件</param>
        /// <param name="name">父控件的name</param>
        /// <returns></returns>
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
