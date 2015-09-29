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
using System.Collections;
using System.IO;

namespace My2048
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    //http://www.3366.com/flash/106550.shtml 在线版2048

    public partial class MainWindow : Window
    {
        private Tile[] _tiles = new Tile[16];
        enum MoveDict { Up, Down, Left, Right };

        private int _score = 0;

        private int[] _prev = new int[17];//存储上一步

        private int[] _records = new int[10];//存储记录

        /// <summary>
        ///初始化窗口，绑定方块控件 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //tiles[0] = b0;
            //tiles[1] = b1;
            //tiles[2] = b2;
            //tiles[3] = b3;
            //tiles[4] = b4;
            //tiles[5] = b5;
            //tiles[6] = b6;
            //tiles[7] = b7;
            //tiles[8] = b8;
            //tiles[9] = b9;
            //tiles[10] = b10;
            //tiles[11] = b11;
            //tiles[12] = b12;
            //tiles[13] = b13;
            //tiles[14] = b14;
            //tiles[15] = b15;

            for (int i = 0; i < _tiles.Length; i++)
            {
                _tiles[i] = this.FindName("b" + i.ToString()) as Tile;
            }

            StartGame();
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        private void StartGame()
        {
            _score = 0;
            tScore.Text = _score.ToString();
            Random r = new Random();

            int c1 = r.Next(0, 16);
            int c2;

            do
            {
                c2 = r.Next(0, 16);
            } while (c2 == c1);

            for (int i = 0; i < 16; i++)
            {
                _tiles[i].Value = 0;//清空
            }

            //随机设置两个方块
            _tiles[c1].Value = r.Next(0, 100) < 90 ? 2 : 4;
            _tiles[c2].Value = r.Next(0, 100) < 90 ? 2 : 4;

            SavePrevValue();
        }

        /// <summary>
        /// 存储上一步
        /// </summary>
        private void SavePrevValue()
        {
            for (int i = 0; i < 16; i++)
            {
                _prev[i] = _tiles[i].Value;
            }
            _prev[16] = _score;
        }

        /// <summary>
        /// 加载上一步
        /// </summary>
        private void LoadPrevValue()
        {
            for (int i = 0; i < 16; i++)
            {
                _tiles[i].Value = _prev[i];
            }
            _score = _prev[16];
            tScore.Text = _score.ToString();
        }
        /// <summary>
        /// 移动一步
        /// </summary>
        /// <param name="dict">移动方向</param>
        private void OneMove(MoveDict dict)
        {
            SavePrevValue();
            //bool needGetNext1 = false;
            //bool needGetNext2 = false;
            //bool needGetNext3 = false;
            //bool needGetNext4 = false;

            switch (dict)
            {
                case MoveDict.Up:
                    _score += MoveLine(b12, b8, b4, b0) + MoveLine(b13, b9, b5, b1) + MoveLine(b14, b10, b6, b2) + MoveLine(b15, b11, b7, b3);
                    break;
                case MoveDict.Down:
                    _score += MoveLine(b0, b4, b8, b12) + MoveLine(b1, b5, b9, b13) + MoveLine(b2, b6, b10, b14) + MoveLine(b3, b7, b11, b15);
                    break;
                case MoveDict.Right:
                    _score += MoveLine(b0, b1, b2, b3) + MoveLine(b4, b5, b6, b7) + MoveLine(b8, b9, b10, b11) + MoveLine(b12, b13, b14, b15);
                    break;
                case MoveDict.Left:
                    _score += MoveLine(b3, b2, b1, b0) + MoveLine(b7, b6, b5, b4) + MoveLine(b11, b10, b9, b8) + MoveLine(b15, b14, b13, b12);
                    break;
                default:
                    break;
            }

            tScore.Text = _score.ToString();
            //int n = cubes.Count(isZero);
            int n = _tiles.Count(cube => { return cube.Value == 0; });//计算为0的方块数

            //还有空方块则新建一个方块
            if (n > 0)
            {
                GetNextCube();
            }
            else
            {
                //判断是否还能移动
                if (!CanMove())
                {
                    SetRecord();
                    MessageBox.Show("Game Over!", "Info");
                }
            }
        }
        /// <summary>
        /// 移动一行(列）
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t3"></param>
        /// <param name="t4"></param>
        /// <returns></returns>
        private int MoveLine(Tile t1, Tile t2, Tile t3, Tile t4)
        {
            //canGetNext = false;
            //int score = 0;
            //if (t1.Value == 0 && t2.Value == 0 && t3.Value == 0 && t4.Value == 0)//该行全为0直接返回
            //    return 0;
            //if ((c4.Value == 0 && (c1.Value != 0 || c2.Value != 0 || c3.Value != 0)) ||
            //(c3.Value == 0 && (c1.Value != 0 || c2.Value != 0)) ||
            //(c2.Value == 0 && c1.Value != 0))
            //    canGetNext = true;
            int score = 0;
            Tile[] row = { t1, t2, t3, t4 };
            if (row.Count(t => t.Value == 0) == row.Length)//该行全为0直接返回
                return 0;
            //ArrayList iCubes = new ArrayList();
            //for (int i = 0; i < cubes.Length; i++)
            //    if (cubes[i].IValue != 0)
            //    {
            //        iCubes.Add(cubes[i].IValue);
            //    }
            //for (int i = 0; i < cubes.Length - iCubes.Count; i++)
            //    cubes[i].IValue = 0;
            //for (int i = 0; i < iCubes.Count; i++)
            //    cubes[i + cubes.Length - iCubes.Count].IValue = (int)iCubes[i];

            //选出非0的cube
            //for (int i = 0; i < iCube.Length; i++)
            //{
            //    c[c.Length - 1 - i].Value = iCube[iCube.Length - 1 - i].Value;//将非0的cube放到队尾
            //    //c[i].Value = iCube[iCube.Count()-1-i];
            //}
            ArrayList tilesNotZero = new ArrayList();//存放当前行非0块
            foreach (Tile tile in row)
            {
                if (tile.Value != 0)
                {
                    tilesNotZero.Add(tile.Value);
                }
            }

            for (int i = 0; i < row.Length; i++)
            {
                if (tilesNotZero.Count - 1 - i >= 0)
                {
                    row[row.Length - 1 - i].Value = (int)tilesNotZero[tilesNotZero.Count - 1 - i];//将非0的块放到队尾
                }
                else
                {
                    row[row.Length - 1 - i].Value = 0;//其余补0
                }
                //c[i].Value = iCube[iCube.Count()-1-i];
            }

            if (row[2].Value != 0)//如果倒数第二个为0，说明该行只有一个数直接返回
            {
                if (row[3].Value == row[2].Value)
                {
                    score += 2 * row[3].Value;
                    row[3].Value = 2 * row[3].Value;
                    //canGetNext = true;
                    if (row[1].Value == row[0].Value)
                    {
                        score += 2 * row[1].Value;
                        row[2].Value = 2 * row[1].Value;
                        //if (c[1].Value > 0) canGetNext = true;//???
                        row[1].Value = 0;
                        row[0].Value = 0;
                    }
                    else
                    {
                        row[2].Value = row[1].Value;
                        row[1].Value = row[0].Value;
                        row[0].Value = 0;
                    }
                }
                else
                {
                    if (row[2].Value == row[1].Value)
                    {
                        score += 2 * row[2].Value;
                        row[2].Value = 2 * row[2].Value;
                        //if (c[1].Value > 0) canGetNext = true;
                        row[1].Value = row[0].Value;
                        row[0].Value = 0;
                    }
                    else
                    {
                        if (row[1].Value == row[0].Value)
                        {
                            score += 2 * row[1].Value;
                            row[1].Value = 2 * row[1].Value;
                            //if (c[0].Value > 0) canGetNext = true;
                            row[0].Value = 0;
                        }
                    }
                }
            }
            return score;
        }


        /// <summary>
        /// 获取一个新的色块
        /// </summary>
        private void GetNextCube()
        {
            ArrayList iTiles = new ArrayList();//存放数组为0的块的位置

            for (int i = 0; i < 16; i++)
            {
                if (_tiles[i].Value == 0)
                {
                    iTiles.Add(i);
                }
            }
           

            if (iTiles.Count > 0)
            {
                Random r = new Random();
                _tiles[(int)iTiles[r.Next(1, iTiles.Count) - 1]].Value = (r.Next(1, 100) < 90 ? 2 : 4);
            }
        }


        /// <summary>
        /// 判断能否继续移动
        /// </summary>
        /// <returns></returns>
        private bool CanMove()
        {
            for (int i = 0; i < _tiles.Length; i++)
            {
                if (_tiles[i].Value == 0)
                {
                    return true;
                }
            }
            if (b0.Value == b1.Value || b1.Value == b2.Value || b2.Value == b3.Value ||
                b4.Value == b5.Value || b5.Value == b6.Value || b6.Value == b7.Value ||
                b8.Value == b9.Value || b9.Value == b10.Value || b10.Value == b11.Value ||
                b12.Value == b13.Value || b13.Value == b14.Value || b14.Value == b15.Value)
                return true;
            if (b0.Value == b4.Value || b4.Value == b8.Value || b8.Value == b12.Value ||
                b1.Value == b5.Value || b5.Value == b9.Value || b9.Value == b13.Value ||
                b2.Value == b6.Value || b6.Value == b10.Value || b10.Value == b14.Value ||
                b3.Value == b7.Value || b7.Value == b11.Value || b11.Value == b15.Value)
                return true;
            return false;
        }


        //private bool isZero(Cube c)
        //{
        //    return c.Value == 0;
        //}
        //private bool notZero(Cube c)
        //{
        //    return !isZero(c);
        //}


        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("重开一局吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                StartGame();
            }
        }



        
        private void butBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPrevValue();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("确定退出吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {

                case Key.Up:
                    OneMove(MoveDict.Up);
                    break;
                case Key.Down:
                    OneMove(MoveDict.Down);
                    break;
                case Key.Left:
                    OneMove(MoveDict.Left);
                    break;
                case Key.Right:
                    OneMove(MoveDict.Right);
                    break;
                default:
                    break;
            }
        }



        private void save_Click(object sender, RoutedEventArgs e)
        {
            string[] saves = new string[17];
            for (int i = 0; i < 16; i++)
            {
                saves[i] = _tiles[i].Value.ToString();
            }
            saves[16] = _score.ToString();
            File.WriteAllLines(@".\sava.dat", saves, Encoding.UTF8);
            MessageBox.Show("存储成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        
        /// <summary>
        /// 当前分数与纪录比较
        /// </summary>
        private void SetRecord()
        {
            LoadRecord();
            List<int> list = _records.ToList();
            if (_score > list.Min())
            {
                list.Remove(list.Min());
                list.Add(_score);
                list.Sort();
                list.Reverse();
                _records = list.ToArray();
                SaveRecord();
            }
        }

        /// <summary>
        /// 保存纪录
        /// </summary>
        private void SaveRecord()
        {
            StreamWriter stream = new StreamWriter(@"./record.dat", false, Encoding.UTF8);
            using (stream)
            {
                foreach (int r in _records)
                {
                    stream.WriteLine(r.ToString());
                }
            }
        }

        /// <summary>
        /// 加载纪录
        /// </summary>
        private void LoadRecord()
        {

            if (File.Exists(@"./record.dat"))
            {
                StreamReader stream = new StreamReader(@"./record.dat", Encoding.UTF8);
                using (stream)
                {
                    for (int i = 0; i < _records.Length; i++)
                    {
                        _records[i] = int.Parse(stream.ReadLine());
                    }
                }

            }
        }
        
        private void record_Click(object sender, RoutedEventArgs e)
        {
            LoadRecord();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _records.Length; i++)
            {
                sb.Append("第" + (i + 1).ToString() + "名:" + _records[i] + "\n");
            }
            MessageBox.Show(sb.ToString(), "纪录", MessageBoxButton.OK);
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(@".\sava.dat"))
            {
                string[] saves = File.ReadAllLines(@".\sava.dat", Encoding.UTF8);

                for (int i = 0; i < 16; i++)
                {
                    _tiles[i].Value = int.Parse(saves[i]);
                }
                _score = int.Parse(saves[16]);
                tScore.Text = saves[16];
            }


        }
    }



}

