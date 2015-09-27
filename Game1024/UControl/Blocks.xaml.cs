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
using System.ComponentModel;

namespace Game1024.UControl
{
    /// <summary>
    /// Blocks.xaml 的交互逻辑
    /// </summary>
    public partial class Blocks : UserControl
    {
        public Blocks()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 值
        /// </summary>
        private int iValue;

        [Description("设置当前值")]
        [DefaultValue(typeof(int), "0")]
        public int IValue
        {
            set
            {
                iValue = value;
                txbNum.Text = iValue.ToString();
                if (iValue == 0)
                    txbNum.Text = "";
                Cube_Paint();
            }
            get { return iValue; }
        }

        private void Cube_Paint()
        {
            switch (iValue)
            {
                case 2:
                    MainArea.Background = new SolidColorBrush(Color.FromRgb(238, 228, 218));
                    txbNum.Foreground = new SolidColorBrush(Color.FromRgb(121, 111, 101));
                    break;
                case 4:
                    MainArea.Background = new SolidColorBrush(Color.FromRgb(236, 224, 200));
                    txbNum.Foreground = new SolidColorBrush(Color.FromRgb(121, 111, 101));
                    break;
                case 8:
                    MainArea.Background = new SolidColorBrush(Color.FromRgb(242, 177, 121));
                    txbNum.Foreground = new SolidColorBrush(Colors.White);
                    break;
                case 16:
                    MainArea.Background = new SolidColorBrush(Color.FromRgb(245, 149, 99));
                    txbNum.Foreground = new SolidColorBrush(Colors.White);
                    break;
                case 32:
                    MainArea.Background = new SolidColorBrush(Color.FromRgb(243, 124, 94));
                    txbNum.Foreground = new SolidColorBrush(Colors.White);
                    break;
                case 64:
                    MainArea.Background = new SolidColorBrush(Color.FromRgb(246, 93, 59));
                    txbNum.Foreground = new SolidColorBrush(Colors.White);
                    break;
                case 128:
                case 256:
                case 512:
                case 1024:
                case 2048:
                case 4096:
                    MainArea.Background = new SolidColorBrush(Color.FromRgb(237, 204, 97));
                    txbNum.Foreground = new SolidColorBrush(Colors.White);
                    break;
                default:
                    MainArea.Background = new SolidColorBrush(Color.FromRgb(204, 192, 178));
                    txbNum.Foreground = new SolidColorBrush(Color.FromRgb(121, 111, 101));
                    break;
            }
        }

    }
}
