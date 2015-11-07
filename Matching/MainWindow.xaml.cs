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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Count = 16; //方块数量
        private readonly Tile[] _tiles = new Tile[Count]; //所有方块
        private int _score = 0;
        public Tile TileSelect { set; get; }
        public Tile TileCilck { set; get; }
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < _tiles.Length; i++)
            {
                _tiles[i] = this.FindName("B" + i.ToString()) as Tile;
            }
            Queue<int> quene = new Queue<int>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    quene.Enqueue(j);
                }
            }
            Random rand = new Random();

            while (_tiles.Count(r => r.Value == 0) != 0)
            {
                int n = _tiles.Count(r => r.Value == 0);
                Tile t = _tiles[rand.Next(0, 16)];
                if (t.Value == 0)
                {
                    t.Value = quene.Dequeue();
                }
            }
        }

        public void SetTileSelect(Tile tile)
        {
            TileSelect = tile;
        }

        public void SetTileClick(Tile tile)
        {
            TileCilck = tile;
            tile.Print();
        }

        public void Process()
        {
            if (TileCilck == null && TileCilck == null)
            {
                return;
            }

            if (TileCilck.Value == TileSelect.Value)
            {
                var c = Color.FromArgb(255, 187, 173, 160);
                TileCilck.Color = c;
                TileSelect.Color = c;
                TileCilck.Value = -1;
                TileSelect.Value = -1;
                TileSelect.Print(c);
                TileCilck.Print(c);
            }
            else
            {
                Thread.Sleep(1000);
                var c = Color.FromArgb(255, 204, 192, 178);
                TileCilck.Color = c;
                TileSelect.Color = c;
                TileCilck.Print(c);
                TileSelect.Print(c);
            }
            TileSelect = null;
            TileCilck = null;
        }

        private void MainWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process();
        }

        private void MainWin_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            foreach (var v in _tiles)
            {
                v.Value = 0;
                var c = Color.FromArgb(255, 204, 192, 178);
                v.Print(c);
            }

            Queue<int> quene = new Queue<int>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    quene.Enqueue(j);
                }
            }
            Random rand = new Random();

            while (_tiles.Count(r => r.Value == 0) != 0)
            {
                int n = _tiles.Count(r => r.Value == 0);
                Tile t = _tiles[rand.Next(0, 16)];
                if (t.Value == 0)
                {
                    t.Value = quene.Dequeue();
                }
            }
        }
    }
}
