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
        private const int Count = 16;//方块数量
        private string Dirt = @".\dictionary.txt";//字典位置
        private readonly Tile[] _tiles = new Tile[Count];//所有方块
        private int _score = 0;
        Queue<Similar> _dictionary = new Queue<Similar>();//字典，每项存一组形近字

        private Similar SimNow = new Similar();//当前文字

        //enum Column { Zero, One, Two, Three };

        // 一组形近字
        struct Similar
        {
            public string First;
            public string Second;
        }

        /// <summary>
        /// 窗口初始化
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < _tiles.Length; i++)
            {
                _tiles[i] = this.FindName("T" + i.ToString()) as Tile;
            }
        }

        /// <summary>
        /// 重置方块和得分
        /// </summary>
        private void Reset()
        {
            foreach (var v in _tiles)
            {
                v.SetText("");
            }
            _score = 0;
            TScore.Text = _score.ToString();
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        private void StartGame()
        {
            Reset();
            GetDictionary();
            NewWords();
        }
        /// <summary>
        /// 新的一关
        /// </summary>
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
        /// <summary>
        /// 游戏字典是否为空
        /// </summary>
        /// <returns></returns>
        private bool IsDictionaryEmpty()
        {
            return _dictionary.Count <= 0;
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 得分
        /// </summary>
        private void GetOnePoint()
        {
            _score++;
            TScore.Text = _score.ToString();
        }
        /// <summary>
        /// 读取字典
        /// </summary>
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
        /// <summary>
        /// 处理点击
        /// </summary>
        /// <param name="tile">被点击的方块</param>
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
        /// <summary>
        /// 结束游戏
        /// </summary>
        private void EndGame()
        {
            MessageBox.Show("游戏结束！", "", MessageBoxButton.OK, MessageBoxImage.Information);
            Reset();
        }
    }
}
