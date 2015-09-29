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


    //http://www.3366.com/flash/106550.shtml

    public partial class MainWindow : Window
    {
        private Tile[] tiles = new Tile[16];
        enum MoveDict { Up, Down, Left, Right };

        private int _score = 0;

        private int[] prev = new int[17];

        private int[] records = new int[10];

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
             

            
            
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = this.FindName("b" + i.ToString()) as Tile;
            }

            startGame();
        }
        /// <summary>
        /// 
        /// </summary>
        private void startGame()
        {
            _score = 0;
            tScore.Text = _score.ToString();
            Random r = new Random();

            int c1 = r.Next(1, 5);
            int c2 = c1;

            do
            {
                c2 = r.Next(0, 16);
            } while (c2 == c1);

            for (int i = 0; i < 16; i++)
            {
                tiles[i].Value = 0;
            }



            switch (c1)
            {
                case 1:
                    tiles[0].Value = r.Next(0, 100) < 90 ? 2 : 4;
                    break;
                case 2:
                    tiles[3].Value = r.Next(0, 100) < 90 ? 2 : 4;
                    break;
                case 3:
                    tiles[12].Value = r.Next(0, 100) < 90 ? 2 : 4;
                    break;
                case 4:
                    tiles[15].Value = r.Next(0, 100) < 90 ? 2 : 4;
                    break;
                default:
                    break;
            }
            tiles[c2].Value = r.Next(0, 100) < 90 ? 2 : 4;
            savePrevValue();
        }


        private void savePrevValue()
        {
            for (int i = 0; i < 16; i++)
            {
                prev[i] = tiles[i].Value;
            }
            prev[16] = _score;
        }

        private void loadPrevValue()
        {
            for (int i = 0; i < 16; i++)
            {
                tiles[i].Value = prev[i];
            }
            _score = prev[16];
            tScore.Text = _score.ToString();
        }

        private void oneMove(MoveDict dict)
        {
            savePrevValue();
            //bool needGetNext1 = false;
            //bool needGetNext2 = false;
            //bool needGetNext3 = false;
            //bool needGetNext4 = false;

            switch (dict)
            {
                case MoveDict.Up:
                    _score += moveLine(b12, b8, b4, b0) + moveLine(b13, b9, b5, b1) + moveLine(b14, b10, b6, b2) + moveLine(b15, b11, b7, b3);
                    break;
                case MoveDict.Down:
                    _score += moveLine(b0, b4, b8, b12) + moveLine(b1, b5, b9, b13) + moveLine(b2, b6, b10, b14) + moveLine(b3, b7, b11, b15);
                    break;
                case MoveDict.Right:
                    _score += moveLine(b0, b1, b2, b3) + moveLine(b4, b5, b6, b7) + moveLine(b8, b9, b10, b11) + moveLine(b12, b13, b14, b15);
                    break;
                case MoveDict.Left:
                    _score += moveLine(b3, b2, b1, b0) + moveLine(b7, b6, b5, b4) + moveLine(b11, b10, b9, b8) + moveLine(b15, b14, b13, b12);
                    break;
                default:
                    break;
            }

            tScore.Text = _score.ToString();
            //int n = cubes.Count(isZero);
            int n = tiles.Count(cube => { return cube.Value == 0; });//计算为0的cube数
            //if (needGetNext1 || needGetNext2 || needGetNext3 || needGetNext4 || n < 5)
            if (n > 0)
            {
                getNextCube();
            }
            else
            {
                if (!canMove())
                {
                    setRecord();
                    MessageBox.Show("Game Over!", "Info");
                }
            }
        }

        private int moveLine(Tile t1, Tile t2, Tile t3, Tile t4)
        {
            //canGetNext = false;
            int score = 0;
            if (t1.Value == 0 && t2.Value == 0 && t3.Value == 0 && t4.Value == 0)//该行全为0直接返回
                return 0;
            //if ((c4.Value == 0 && (c1.Value != 0 || c2.Value != 0 || c3.Value != 0)) ||
            //(c3.Value == 0 && (c1.Value != 0 || c2.Value != 0)) ||
            //(c2.Value == 0 && c1.Value != 0))
            //    canGetNext = true;
            Tile[] t = { t1, t2, t3, t4 };

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
            ArrayList iTiles = new ArrayList();
            foreach (Tile tile in t)
            {
                if (tile.Value != 0)
                {
                    iTiles.Add(tile.Value);
                }
            }

            for (int i = 0; i < t.Length; i++)
            {
                if (iTiles.Count - 1 - i >= 0)
                {
                    t[t.Length - 1 - i].Value = (int)iTiles[iTiles.Count - 1 - i];//将非0的cube放到队尾
                }
                else
                {
                    t[t.Length - 1 - i].Value = 0;
                }
                //c[i].Value = iCube[iCube.Count()-1-i];
            }

            if (t[2].Value != 0)//如果倒数第二个为0，说明该行只有一个数直接返回
            {
                if (t[3].Value == t[2].Value)
                {
                    score += 2 * t[3].Value;
                    t[3].Value = 2 * t[3].Value;
                    //canGetNext = true;
                    if (t[1].Value == t[0].Value)
                    {
                        score += 2 * t[1].Value;
                        t[2].Value = 2 * t[1].Value;
                        //if (c[1].Value > 0) canGetNext = true;//???
                        t[1].Value = 0;
                        t[0].Value = 0;
                    }
                    else
                    {
                        t[2].Value = t[1].Value;
                        t[1].Value = t[0].Value;
                        t[0].Value = 0;
                    }
                }
                else
                {
                    if (t[2].Value == t[1].Value)
                    {
                        score += 2 * t[2].Value;
                        t[2].Value = 2 * t[2].Value;
                        //if (c[1].Value > 0) canGetNext = true;
                        t[1].Value = t[0].Value;
                        t[0].Value = 0;
                    }
                    else
                    {
                        if (t[1].Value == t[0].Value)
                        {
                            score += 2 * t[1].Value;
                            t[1].Value = 2 * t[1].Value;
                            //if (c[0].Value > 0) canGetNext = true;
                            t[0].Value = 0;
                        }
                    }
                }
            }
            return score;
        }

        private void getNextCube()
        {
            ArrayList iTiles = new ArrayList();

            for (int i = 0; i < 16; i++)
            {
                if (tiles[i].Value == 0)
                {
                    iTiles.Add(i);
                }
            }
            if (iTiles.Count > 0)
            {
                Random r = new Random();
                tiles[(int)iTiles[r.Next(1, iTiles.Count) - 1]].Value = (r.Next(1, 100) < 90 ? 2 : 4);
            }
        }
        private bool canMove()
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (tiles[i].Value == 0)
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
                startGame();
            }
        }

        private void butBack_Click(object sender, RoutedEventArgs e)
        {
            loadPrevValue();
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
                    oneMove(MoveDict.Up);
                    break;
                case Key.Down:
                    oneMove(MoveDict.Down);
                    break;
                case Key.Left:
                    oneMove(MoveDict.Left);
                    break;
                case Key.Right:
                    oneMove(MoveDict.Right);
                    break;
                default:
                    break;
            }
        }



        private void save_Click(object sender, RoutedEventArgs e)
        {
            string[] save = new string[17];
            for (int i = 0; i < 16; i++)
            {
                save[i] = tiles[i].Value.ToString();
            }
            save[16] = _score.ToString();
            File.WriteAllLines(@".\sava.dat", save, Encoding.UTF8);
            MessageBox.Show("存储成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void setRecord()
        {
            loadRecord();
            List<int> list = records.ToList();
            if (_score > list.Min())
            {
                list.Remove(list.Min());
                list.Add(_score);
                list.Sort();
                list.Reverse();
                records = list.ToArray();
                saveRecord();
            }
        }
        private void saveRecord()
        {
            StreamWriter stream = new StreamWriter(@"./record.dat", false, Encoding.UTF8);
            using (stream)
            {
                foreach (int r in records)
                {
                    stream.WriteLine(r.ToString());
                }
            }
        }
        private void loadRecord()
        {

            if (File.Exists(@"./record.dat"))
            {
                StreamReader stream = new StreamReader(@"./record.dat", Encoding.UTF8);
                using (stream)
                {
                    for (int i = 0; i < records.Length; i++)
                    {
                        records[i] = int.Parse(stream.ReadLine());
                    }
                }

            }
        }
        private void record_Click(object sender, RoutedEventArgs e)
        {
            loadRecord();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < records.Length; i++)
            {
                sb.Append("第" + (i + 1).ToString() + "名:" + records[i] + "\n");
            }
            MessageBox.Show(sb.ToString(), "纪录", MessageBoxButton.OK);
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(@".\sava.dat"))
            {
                string[] save = File.ReadAllLines(@".\sava.dat", Encoding.UTF8);

                for (int i = 0; i < 16; i++)
                {
                    tiles[i].Value = int.Parse(save[i]);
                }
                _score = int.Parse(save[16]);
                tScore.Text = save[16];
            }


        }
    }



}

