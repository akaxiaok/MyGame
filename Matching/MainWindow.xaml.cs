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
               for (int j=1;j<5;j++)
               {
                   quene.Enqueue(j);
               }
            }
            Random rand = new Random();
           
            while (_tiles.Count(r => r.Value == 0) != 0)
            {
                int n = _tiles.Count(r => r.Value == 0);
                Tile t = _tiles[rand.Next(0, 16)];
                if (t.Value==0)
                {
                    t.Value = quene.Dequeue();
                }
            };
            string log = "2";
        }
    }
}
