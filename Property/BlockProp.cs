using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Property
{
    public class BlockProp
    {
        /// <summary>
        /// 数值
        /// </summary>
        private int iValue;
        /// <summary>
        /// 数值
        /// </summary>
        public int IValue
        {
            get { return iValue; }
            set { iValue = value; }
        }

        /// <summary>
        /// 背景色
        /// </summary>
        private Color backColor;
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        /// <summary>
        /// 背景画刷
        /// </summary>
        private Brush backBrush;
        /// <summary>
        /// 背景画刷
        /// </summary>
        public Brush BackBrush
        {
            get { return backBrush; }
            set { backBrush = value; }
        }

        private Style
    }
}
