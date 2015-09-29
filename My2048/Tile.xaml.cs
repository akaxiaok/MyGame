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

namespace My2048
{
    /// <summary>
    /// Cube.xaml 的交互逻辑
    /// </summary>
    public partial class Tile : UserControl
    {
        public Tile()
        {
            InitializeComponent();
        }

        private int _value=0;

        /// <summary>
        /// 设置显示数字
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                txbNum.Text = _value.ToString();
                if (_value == 0)
                {
                    txbNum.Text = "";
                }
                CubePaint();
            }

        }
        /// <summary>
        ///根据数字设置颜色 
        /// </summary>
        private void CubePaint()
        {
            switch (_value)
            {
                case 2:
                    SetColor(238, 228, 218);
                    txbNum.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case 4:
                    SetColor(236, 224, 200);
                    txbNum.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case 8:
                    SetColor(242, 177, 121);
                    break;
                case 16:
                    SetColor(245, 149, 99);
                    break;
                case 32:
                    SetColor(243, 124, 94);
                    break;
                case 64:
                    SetColor(246, 93, 59);
                    break;
                case 128:
                case 256:
                case 512:
                case 1024:
                case 2048:
                case 4096:
                    SetColor(237, 204, 97);
                    break;
                default:
                    SetColor(204, 192, 178);
                    break;
            }
        }
        private void SetColor(byte r, byte g, byte b)
        {
            MainArea.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
            txbNum.Foreground = new SolidColorBrush(Colors.White);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
