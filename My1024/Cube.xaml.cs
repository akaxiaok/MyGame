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
    public partial class Cube : UserControl
    {
        public Cube()
        {
            InitializeComponent();
        }

        private int _value=0;

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
        private void CubePaint()
        {
            switch (_value)
            {
                case 2:
                    setColor(238, 228, 218);
                    txbNum.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case 4:
                    setColor(236, 224, 200);
                    txbNum.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case 8:
                    setColor(242, 177, 121);
                    break;
                case 16:
                    setColor(245, 149, 99);
                    break;
                case 32:
                    setColor(243, 124, 94);
                    break;
                case 64:
                    setColor(246, 93, 59);
                    break;
                case 128:
                case 256:
                case 512:
                case 1024:
                case 2048:
                case 4096:
                    setColor(237, 204, 97);
                    break;
                default:
                    setColor(204, 192, 178);
                    break;
            }
        }
        private void setColor(byte r, byte g, byte b)
        {
            MainArea.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
            txbNum.Foreground = new SolidColorBrush(Colors.White);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
