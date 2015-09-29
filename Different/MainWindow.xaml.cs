using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace Different
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Count = 16;
        private string Dirt = @".\dictionary.txt";
        private readonly Tile[] _tiles = new Tile[Count];
        enum Column { Zero, One, Two, Three };

        private int _score = 0;

        //string[,] _dictionary = new string[100, 2];
        Queue<Similar> _dictionary = new Queue<Similar>();


        private Similar SimNow = new Similar();

        struct Similar
        {
            public string First;
            public string Second;
        }
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < _tiles.Length; i++)
            {
                _tiles[i] = this.FindName("T" + i.ToString()) as Tile;
            }
        }


        private void Reset()
        {
            foreach (var v in _tiles)
            {
                v.SetText("");
            }
            _score = 0;
            TScore.Text = _score.ToString();
        }
        private void StartGame()
        {
            Reset();
            GetDictionary();
            NewWords();
        }

        private void NewWords()
        {
            if (!IsDictionaryEmpty())
            {
                SimNow = _dictionary.Dequeue();
                foreach (var v in _tiles)
                {
                    v.SetText(SimNow.First);
                }
                Random r = new Random();
                _tiles[r.Next(0, _tiles.Length)].SetText(SimNow.Second);
            }
        }

        private bool IsDictionaryEmpty()
        {
            return _dictionary.Count <= 0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("确定退出吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void GetOnePoint()
        {
            _score++;
            TScore.Text = _score.ToString();
        }

        private void GetDictionary()
        {
            StreamReader s;
            if (File.Exists(Dirt))
            {

                s = new StreamReader(Dirt, Encoding.UTF8);
                string[] words;

                while (s.Peek() > 0)
                {
                    words = s.ReadLine().Split(' ');
                    Similar sim = new Similar();
                    sim.First = words[0];
                    sim.Second = words[1];
                    _dictionary.Enqueue(sim);
                }

            }
            else
                MessageBox.Show("请检查字典文件", "错误", MessageBoxButton.OK, MessageBoxImage.Error);


        }

        public void OneClick(Tile tile)
        {
            if (tile.GetText() == SimNow.Second)
            {
                GetOnePoint();
                if (IsDictionaryEmpty())
                {
                    EndGame();
                }
                else
                    NewWords();
            }
        }

        private void EndGame()
        {
            MessageBox.Show("游戏结束！", "", MessageBoxButton.OK, MessageBoxImage.Information);
            Reset();
        }
    }
}
