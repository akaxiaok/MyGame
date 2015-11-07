using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Matching
{
    /// <summary>
    /// Tiel.xaml 的交互逻辑
    /// </summary>
    public partial class Tile : UserControl
    {
        public Color Color { get; set; }
        public int Value { get; set; }
        private MainWindow win;
        public Tile()
        {
            InitializeComponent();
        }

        public void SetColor(Color color)
        {

            MainArea.Background = new SolidColorBrush(color);
            Color = color;

        }

        private void MainArea_MouseUp(object sender, MouseButtonEventArgs e)
        {

            //if (sel.Value == this.Value)
            //{

            //    sel.Color = Color.FromArgb(255, 187, 173, 160);
            //    this.Color = Color.FromArgb(255, 187, 173, 160);
            //    sel.Value = -1;
            //    this.Value = -1;
            //    Print(sel);
            //    Print(this);
            //}
            //else
            //{
            //    Thread.Sleep(1000);
            //    sel.Color = Color.FromArgb(255, 204, 192, 178);
            //    this.Color = Color.FromArgb(255, 204, 192, 178);
            //    Print(sel);
            //    Print(this);
            //}
            //SetTileSelect();
        }

        public void Print(Color col)
        {
            this.SetColor(col);
        }
        public void Print()
        {
            switch (Value)
            {
                case 1:
                    SetColor(Colors.BlueViolet);
                    break;
                case 2:
                    SetColor(Colors.Crimson);
                    break;
                case 3:
                    SetColor(Colors.DarkSlateGray);
                    break;
                case 4:
                    SetColor(Colors.MediumSeaGreen);
                    break;
                default:
                    break;
            }
        }

        public void SetTileSelect()
        {
            win.SetTileSelect(this);//主窗口MaiWin处理一次点击事件
        }

        public void SetTileClick()
        {
            win.SetTileClick(this);
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

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            win = GetParentObject<MainWindow>(this, "MainWin");//通过name找父控件
            var sel = win.TileSelect;
            if (this.Value == -1)
            {
                return;
            }
            if (this.Equals(win.TileSelect))
            {
                return;
            }
            if (sel == null || sel.Value <= 0)
            {
                SetTileSelect();
                Print();
            }
            else
            {
                Print();
                SetTileClick();
            }
        }


    }
}
