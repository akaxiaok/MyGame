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
using Game1024.UControl;
using System.Collections;

namespace Game1024
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Blocks[] Blockes = new Blocks[16];

        /// <summary>
        /// 上下左右移动
        /// </summary>
        enum MoveDict { Up, Down, Left, Right };
        /// <summary>
        /// 分数
        /// </summary>
        private int i_Score = 0;
        /// <summary>
        /// 上1步方格值和总分数
        /// </summary>
        private int[] prev = new int[17];

        /// <summary>
        /// 鼠标起始位置
        /// </summary>
        private int x1, y1;

        public MainWindow()
        {
            InitializeComponent();
            Blockes[0] = b1;
            Blockes[1] = b2;
            Blockes[2] = b3;
            Blockes[3] = b4;
            Blockes[4] = b5;
            Blockes[5] = b6;
            Blockes[6] = b7;
            Blockes[7] = b8;
            Blockes[8] = b9;
            Blockes[9] = b10;
            Blockes[10] = b11;
            Blockes[11] = b12;
            Blockes[12] = b13;
            Blockes[13] = b14;
            Blockes[14] = b15;
            Blockes[15] = b16;
            StartGame();
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        private void StartGame()
        {
            //初始化分数
            i_Score = 0;
            t_Score.Text = i_Score.ToString();

            Random r = new Random();
            int c1 = r.Next(1, 5);
            int c2 = c1;

            do
                c2 = r.Next(0, 16);
            while (c2 == c1);


            for ( int i = 0; i < 16; i++)
                Blockes[i].IValue = 0;

            int n1 = r.Next(1, 100);
            int n2 = r.Next(1, 100);


            switch (c1)
            {
                case 1:
                    Blockes[0].IValue = (n1 < 90 ? 2 : 4);
                    break;
                case 2:
                    Blockes[3].IValue = (n1 < 90 ? 2 : 4);
                    break;
                case 3:
                    Blockes[12].IValue = (n1 < 90 ? 2 : 4);
                    break;
                case 4:
                    Blockes[15].IValue = (n1 < 90 ? 2 : 4);
                    break;
            }
            Blockes[c2].IValue = (n2 < 90 ? 2 : 4);
            SavePrevVaule();
        }

        /// <summary>
        /// 保存上一步值
        /// </summary>
        private void SavePrevVaule()
        {
            //记录每一个值
            for (int i = 0; i < 16; i++)
                prev[i] = Blockes[i].IValue;
            //记录总分
            prev[16] = i_Score;
        }

        /// <summary>
        /// 加载上一步值
        /// </summary>
        private void LoadPrevValue()
        {
            for (int i = 0; i < 16; i++)
                Blockes[i].IValue = prev[i];
            i_Score = prev[16];
            t_Score.Text = i_Score.ToString();
        }

        /// <summary>
        /// 一次移动操作
        /// </summary>
        /// <param name="dict">移动方向</param>
        private void OneMove(MoveDict dict)
        {
            SavePrevVaule();
            bool needGetNext1 = false, needGetNext2 = false, needGetNext3 = false, needGetNext4 = false;
            switch (dict)
            {
                case MoveDict.Up:
                    i_Score += MoveLine(b13, b9, b5, b1, out needGetNext1) + MoveLine(b14, b10, b6, b2, out needGetNext2) + MoveLine(b15, b11, b7, b3, out needGetNext3) + MoveLine(b16, b12, b8, b4, out needGetNext4);
                    break;
                case MoveDict.Down:
                    i_Score += MoveLine(b1, b5, b9, b13, out needGetNext1) + MoveLine(b2, b6, b10, b14, out needGetNext2) + MoveLine(b3, b7, b11, b15, out needGetNext3) + MoveLine(b4, b8, b12, b16, out needGetNext4);
                    break;
                case MoveDict.Right:
                    i_Score += MoveLine(b1, b2, b3, b4, out needGetNext1) + MoveLine(b5, b6, b7, b8, out needGetNext2) + MoveLine(b9, b10, b11, b12, out needGetNext3) + MoveLine(b13, b14, b15, b16, out needGetNext4);
                    break;
                case MoveDict.Left:
                    i_Score += MoveLine(b4, b3, b2, b1, out needGetNext1) + MoveLine(b8, b7, b6, b5, out needGetNext2) + MoveLine(b12, b11, b10, b9, out needGetNext3) + MoveLine(b16, b15, b14, b13, out needGetNext4);
                    break;
            }
            t_Score.Text = i_Score.ToString();  //显示总分
            int n = 0;//值不为0的方格个数
            for (int i = 0; i < Blockes.Length; i++)
                if (Blockes[i].IValue != 0) n++;
            if (needGetNext1 || needGetNext2 || needGetNext3 || needGetNext4 || n < 5)
            {
                getNextCube();//取下一个方块
            }
            else
            {
                if (!CanMove())
                {
                    MessageBox.Show("Game Over!", "Info");
                }
            }
        }

        /// <summary>
        /// 移动一行操作
        /// </summary>
        /// <param name="block1"></param>
        /// <param name="block2"></param>
        /// <param name="block3"></param>
        /// <param name="block4"></param>
        /// <param name="canGetNext">是否能获取下一个</param>
        /// <returns></returns>
        private int MoveLine(Blocks block1, Blocks block2, Blocks block3, Blocks block4, out bool canGetNext)
        {
            canGetNext = false;
            int score = 0;
            if (block1.IValue == 0 && block2.IValue == 0 && block3.IValue == 0 && block4.IValue == 0)
                return 0;
            if ((block4.IValue == 0 && (block1.IValue != 0 || block2.IValue != 0 || block3.IValue != 0)) ||
                (block3.IValue == 0 && (block1.IValue != 0 || block2.IValue != 0)) ||
                (block2.IValue == 0 && block1.IValue != 0)
                )
                canGetNext = true;
            Blocks[] Blockss = { block1, block2, block3, block4 }; 
            
            ArrayList iBlockes = new ArrayList();//存储不为0的方块
            for (int i = 0; i < Blockss.Length; i++)
                if (Blockss[i].IValue != 0)
                {
                    iBlockes.Add(Blockss[i].IValue);
                }
            for (int i = 0; i < Blockss.Length - iBlockes.Count; i++)
                Blockss[i].IValue = 0;
            for (int i = 0; i < iBlockes.Count; i++)
                Blockss[i + Blockss.Length - iBlockes.Count].IValue = (int)iBlockes[i];
            //如果等于0，说明该行只有一个非0的数，则不需要再移动和累加分值了。
            if (Blockss[2].IValue != 0)
            {
                if (Blockss[3].IValue == Blockss[2].IValue)
                {
                    score += 2 * Blockss[3].IValue;
                    Blockss[3].IValue = 2 * Blockss[3].IValue;
                    canGetNext = true;
                    if (Blockss[1].IValue == Blockss[0].IValue)
                    {
                        score += 2 * Blockss[1].IValue;
                        Blockss[2].IValue = 2 * Blockss[1].IValue;
                        if (Blockss[1].IValue > 0) canGetNext = true;
                        Blockss[1].IValue = 0;
                        Blockss[0].IValue = 0;
                    }
                    else
                    {
                        Blockss[2].IValue = Blockss[1].IValue;
                        Blockss[1].IValue = Blockss[0].IValue; ;
                        Blockss[0].IValue = 0;
                    }
                }
                else
                {
                    if (Blockss[2].IValue == Blockss[1].IValue)
                    {
                        score += 2 * Blockss[2].IValue;
                        Blockss[2].IValue = 2 * Blockss[2].IValue;
                        if (Blockss[1].IValue > 0) canGetNext = true;
                        Blockss[1].IValue = Blockss[0].IValue; ;
                        Blockss[0].IValue = 0;
                    }
                    else
                    {
                        if (Blockss[1].IValue == Blockss[0].IValue)
                        {
                            score += 2 * Blockss[1].IValue;
                            Blockss[1].IValue = 2 * Blockss[1].IValue;
                            if (Blockss[0].IValue > 0) canGetNext = true;
                            Blockss[0].IValue = 0;
                        }
                    }
                }
            }
            return score;
        }

        /// <summary>
        /// 获取下一个方块
        /// </summary>
        private void getNextCube()
        {
            ArrayList iBlockes = new ArrayList();   //去除0之后的方块
            //找出全部为0的方块
            for (int i = 0; i < 16; i++)
            {
                if (Blockes[i].IValue == 0)
                {
                    iBlockes.Add(i);
                }
            }
            if (iBlockes.Count > 0)
            {
                //在所有为0的位置上随机增加一个数
                System.Random r = new Random();
                Blockes[(int)iBlockes[r.Next(1, iBlockes.Count) - 1]].IValue = (r.Next(1, 100) < 90 ? 2 : 4); ;
            }
        }

        /// <summary>
        /// 全部方格已填满，是否还能移动
        /// </summary>
        /// <returns></returns>
        private bool CanMove()
        {
            for (int i = 0; i < Blockes.Length; i++)
            {
                if (Blockes[i].IValue == 0)
                    return true;
            }
            //是否能左右移动
            if (b1.IValue == b2.IValue || b2.IValue == b3.IValue || b3.IValue == b4.IValue ||
                b5.IValue == b6.IValue || b6.IValue == b7.IValue || b7.IValue == b8.IValue ||
                b9.IValue == b10.IValue || b10.IValue == b11.IValue || b11.IValue == b12.IValue ||
                b13.IValue == b14.IValue || b14.IValue == b15.IValue || b15.IValue == b16.IValue
                )
                return true;
            //是否能上下移动
            if (b1.IValue == b5.IValue || b5.IValue == b9.IValue || b9.IValue == b13.IValue ||
                b2.IValue == b6.IValue || b6.IValue == b10.IValue || b10.IValue == b14.IValue ||
                b3.IValue == b7.IValue || b7.IValue == b11.IValue || b11.IValue == b15.IValue ||
                b4.IValue == b8.IValue || b8.IValue == b12.IValue || b12.IValue == b16.IValue
                )
                return true;
            return false;
        }


        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                case Key.NumPad8:
                case Key.W:
                    OneMove(MoveDict.Up);
                    break;

                case Key.Down:
                case Key.NumPad2:
                case Key.S:
                    OneMove(MoveDict.Down);
                    break;

                case Key.Left:
                case Key.NumPad4:
                case Key.A:
                    OneMove(MoveDict.Left);
                    break;

                case Key.Right:
                case Key.NumPad6:
                case Key.D:
                    OneMove(MoveDict.Right);
                    break;

                case Key.Escape:
                    this.Close();
                    break;
            }
        }

        #region 窗体关闭时的事件
        /// <summary>
        /// 窗体关闭时的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //x1 = Convert.ToInt32(((System.Windows.Controls.Border)(e.OriginalSource)).CornerRadius.TopLeft);
            //x1 = e.X;   
            //e.LeftButton
            //y1 = e.Y;            
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //int i = Convert.ToInt32(((System.Windows.Controls.Border)(e.OriginalSource)).CornerRadius.TopLeft) - x1;
            //MessageBox.Show(i.ToString());
            //OneMove(getDirection(e.X - x1, e.Y - y1));
        }

        /// <summary>
        /// 获得鼠标移动方向
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        //private MoveDict getDirection(int x, int y)
        //{
        //    MoveDict d = MoveDict.Left;
        //    if (x >= 0 && y >= 0)
        //    {
        //        if (x > y)
        //            d = MoveDict.Right;
        //        else
        //            d = MoveDict.Down;
        //    }
        //    if (x >= 0 && y < 0)
        //    {
        //        if (x > Math.Abs(y))
        //            d = MoveDict.Right;
        //        else
        //            d = MoveDict.Up; ;
        //    }
        //    if (x < 0 && y >= 0)
        //    {
        //        if (Math.Abs(x) > y)
        //            d = MoveDict.Left;
        //        else
        //            d = MoveDict.Down;
        //    }
        //    if (x < 0 && y < 0)
        //    {
        //        if (Math.Abs(x) > Math.Abs(y))
        //            d = MoveDict.Left;
        //        else
        //            d = MoveDict.Up; ;
        //    }
        //    return d;
        //}


        /// <summary>
        /// 开始游戏(重新开始)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        /// <summary>
        /// 回退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            LoadPrevValue();
        }
    }
}
