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

namespace My1024
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    //http://www.3366.com/flash/106550.shtml

    public partial class MainWindow : Window
    {
        private Cube[] cubes = new Cube[16];
        enum MoveDict { Up, Down, Left, Right };

        private int _score = 0;

        private int[] prev = new int[17];

        public MainWindow()
        {
            InitializeComponent();
            cubes[0] = b0;
            cubes[1] = b1;
            cubes[2] = b2;
            cubes[3] = b3;
            cubes[4] = b4;
            cubes[5] = b5;
            cubes[6] = b6;
            cubes[7] = b7;
            cubes[8] = b8;
            cubes[9] = b9;
            cubes[10] = b10;
            cubes[11] = b11;
            cubes[12] = b12;
            cubes[13] = b13;
            cubes[14] = b14;
            cubes[15] = b15;
            startGame();
        }

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
                cubes[i].Value = 0;
            }



            switch (c1)
            {
                case 1:
                    cubes[0].Value = r.Next(0, 100) < 90 ? 2 : 4;
                    break;
                case 2:
                    cubes[3].Value = r.Next(0, 100) < 90 ? 2 : 4;
                    break;
                case 3:
                    cubes[12].Value = r.Next(0, 100) < 90 ? 2 : 4;
                    break;
                case 4:
                    cubes[15].Value = r.Next(0, 100) < 90 ? 2 : 4;
                    break;
                default:
                    break;
            }
            cubes[c2].Value = r.Next(0, 100) < 90 ? 2 : 4;
            savePrevValue();
        }


        private void savePrevValue()
        {
            for (int i = 0; i < 16; i++)
            {
                prev[i] = cubes[i].Value;
            }
            prev[16] = _score;
        }

        private void loadPrevValue()
        {
            for (int i = 0; i < 16; i++)
            {
                cubes[i].Value = prev[i];
            }
            _score = prev[16];
            tScore.Text = _score.ToString();
        }

        private void oneMove(MoveDict dict)
        {
            savePrevValue();
            bool needGetNext1 = false;
            bool needGetNext2 = false;
            bool needGetNext3 = false;
            bool needGetNext4 = false;

            switch (dict)
            {
                case MoveDict.Up:
                    _score += moveLine(b12, b8, b4, b0, out needGetNext1) + moveLine(b13, b9, b5, b1, out needGetNext2) + moveLine(b14, b10, b6, b2, out needGetNext3) + moveLine(b15, b11, b7, b3, out needGetNext4);
                    break;
                case MoveDict.Down:
                    _score += moveLine(b0, b4, b8, b12, out needGetNext1) + moveLine(b1, b5, b9, b13, out needGetNext2) + moveLine(b2, b6, b10, b14, out needGetNext3) + moveLine(b3, b7, b11, b15, out needGetNext4);
                    break;
                case MoveDict.Right:
                    _score += moveLine(b0, b1, b2, b3, out needGetNext1) + moveLine(b4, b5, b6, b7, out needGetNext2) + moveLine(b8, b9, b10, b11, out needGetNext3) + moveLine(b12, b13, b14, b15, out needGetNext4);
                    break;
                case MoveDict.Left:
                    _score += moveLine(b3, b2, b1, b0, out needGetNext1) + moveLine(b7, b6, b5, b4, out needGetNext2) + moveLine(b11, b10, b9, b8, out needGetNext3) + moveLine(b15, b14, b13, b12, out needGetNext4);
                    break;
                default:
                    break;
            }

            tScore.Text = _score.ToString();
            //int n = cubes.Count(isZero);
            int n = cubes.Count(cube => { return cube.Value == 0; });//计算为0的cube数
            //if (needGetNext1 || needGetNext2 || needGetNext3 || needGetNext4 || n < 5)
            if (n > 0)
            {
                getNextCube();
            }
            else
            {
                if (!canMove())
                {
                    MessageBox.Show("Game Over!", "Info");
                }
            }
        }

        private int moveLine(Cube c1, Cube c2, Cube c3, Cube c4, out bool canGetNext)
        {
            canGetNext = false;
            int score = 0;
            if (c1.Value == 0 && c2.Value == 0 && c3.Value == 0 && c4.Value == 0)//该行全为0直接返回
                return 0;
            //if ((c4.Value == 0 && (c1.Value != 0 || c2.Value != 0 || c3.Value != 0)) ||
            //(c3.Value == 0 && (c1.Value != 0 || c2.Value != 0)) ||
            //(c2.Value == 0 && c1.Value != 0))
            //    canGetNext = true;
            Cube[] c = { c1, c2, c3, c4 };

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
            ArrayList iCube = new ArrayList();
            foreach (Cube cube in c)
            {
                if (cube.Value != 0)
                {
                    iCube.Add(cube.Value);
                }
            }

            for (int i = 0; i < c.Length; i++)
            {
                if (iCube.Count - 1 - i >= 0)
                {
                    c[c.Length - 1 - i].Value = (int)iCube[iCube.Count - 1 - i];//将非0的cube放到队尾
                }
                else
                {
                    c[c.Length - 1 - i].Value = 0;
                }
                //c[i].Value = iCube[iCube.Count()-1-i];
            }

            if (c[2].Value != 0)//如果倒数第二个为0，说明该行只有一个数直接返回
            {
                if (c[3].Value == c[2].Value)
                {
                    score += 2 * c[3].Value;
                    c[3].Value = 2 * c[3].Value;
                    //canGetNext = true;
                    if (c[1].Value == c[0].Value)
                    {
                        score += 2 * c[1].Value;
                        c[2].Value = 2 * c[1].Value;
                        //if (c[1].Value > 0) canGetNext = true;//???
                        c[1].Value = 0;
                        c[0].Value = 0;
                    }
                    else
                    {
                        c[2].Value = c[1].Value;
                        c[1].Value = c[0].Value;
                        c[0].Value = 0;
                    }
                }
                else
                {
                    if (c[2].Value == c[1].Value)
                    {
                        score += 2 * c[2].Value;
                        c[2].Value = 2 * c[2].Value;
                        //if (c[1].Value > 0) canGetNext = true;
                        c[1].Value = c[0].Value;
                        c[0].Value = 0;
                    }
                    else
                    {
                        if (c[1].Value == c[0].Value)
                        {
                            score += 2 * c[1].Value;
                            c[1].Value = 2 * c[1].Value;
                            //if (c[0].Value > 0) canGetNext = true;
                            c[0].Value = 0;
                        }
                    }
                }
            }
            return score;
        }

        private void getNextCube()
        {
            ArrayList iCubes = new ArrayList();

            for (int i = 0; i < 16; i++)
            {
                if (cubes[i].Value == 0)
                {
                    iCubes.Add(i);
                }
            }
            if (iCubes.Count > 0)
            {
                Random r = new Random();
                cubes[(int)iCubes[r.Next(1, iCubes.Count) - 1]].Value = (r.Next(1, 100) < 90 ? 2 : 4);
            }
        }
        private bool canMove()
        {
            for (int i = 0; i < cubes.Length; i++)
            {
                if (cubes[i].Value == 0)
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
    }



}

