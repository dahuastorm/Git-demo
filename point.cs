using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// 一对 x-y 坐标表示一个位置点
    /// </summary>
    class Point
    {
        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }

        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="X">点的X坐标</param>
        /// <param name="Y">点的Y坐标</param>
        public Point(int X,int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
