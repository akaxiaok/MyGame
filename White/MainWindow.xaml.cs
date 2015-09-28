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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int First = 4;
        private const int Second = 4;
        private readonly Tile[,] _tiles = new Tile[First, Second];
        enum Column { Zero, One, Two, Three };

        private int _score = 0;

        public MainWindow()
        {
            InitializeComponent();
            _tiles[0, 0] = T0;
            _tiles[0, 1] = T1;
            _tiles[0, 2] = T2;
            _tiles[0, 3] = T3;
            _tiles[1, 0] = T4;
            _tiles[1, 1] = T5;
            _tiles[1, 2] = T6;
            _tiles[1, 3] = T7;
            _tiles[2, 0] = T8;
            _tiles[2, 1] = T9;
            _tiles[2, 2] = T10;
            _tiles[2, 3] = T11;
            _tiles[3, 0] = T12;
            _tiles[3, 1] = T13;
            _tiles[3, 2] = T14;
            _tiles[3, 3] = T15;
            StartGame();
            int i = 0;
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        private void StartGame()
        {
            Reset();

            Random r = new Random();

            for (int i = 0; i < First; i++)
            {
                _tiles[i, r.Next(0, Second)].SetColor(Colors.Black);
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void Reset()
        {
            _score = 0;
            foreach (var v in _tiles)
            {
                v.SetColor(Colors.White);
            }
        }
        /// <summary>
        /// 处理一次操作
        /// </summary>
        /// <param name="which">哪一格</param>
        private void OneMove(Column which)
        {
            bool isWhite = _tiles[First - 1, (int)which].Color.Equals(Colors.White);
            if (isWhite)
            {
                EndGame();
            }
            else
            {
                GetOnePoint();
                NextLine();
            }
        }

        /// <summary>
        /// 结束·游戏·
        /// </summary>
        private void EndGame()
        {
            MessageBox.Show("Game Over!", "Info");
            _score = 0;
            TScore.Text = _score.ToString();

            StartGame();
        }
        /// <summary>
        /// 加一分
        /// </summary>
        private void GetOnePoint()
        {
            _score++;
            TScore.Text = _score.ToString();
        }
        /// <summary>
        /// 下移一行
        /// </summary>
        private void NextLine()
        {
            for (int i = 0; i < First - 1; i++)
            {
                for (int j = 0; j < Second; j++)
                {
                    _tiles[First - 1 - i, j].SetColor(_tiles[First - 2 - i, j].Color);
                }

            }
            for (int i = 0; i < Second; i++)
            {
                _tiles[0, i].SetColor(Colors.White);
            }

            _tiles[0, new Random().Next(0, Second)].SetColor(Colors.Black);

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("确定退出吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {

                case Key.A:
                case Key.H:
                    OneMove(Column.Zero);
                    break;
                case Key.S:
                case Key.J:
                    OneMove(Column.One);
                    break;
                case Key.D:
                case Key.K:
                    OneMove(Column.Two);
                    break;
                case Key.F:
                case Key.L:
                    OneMove(Column.Three);
                    break;
                default:
                    break;
            }
        }
    }
}
