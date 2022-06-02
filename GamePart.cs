using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// 表示游戏的图形单位
    /// </summary>
    class GamePart
    {
        private Point Position;

        /// <summary>
        /// 获取点的位置
        /// </summary>
        /// <returns>点的位置</returns>
        public Point GetPosition()
        {
            return Position;
        }

        /// <summary>
        /// 通过添加点的当前位置来移动游戏部分
        /// </summary>
        /// <param name="point">要添加的点</param>
        public void AddPosition(Point point)
        {
   
            Position.X += point.X;
            Position.Y += point.Y;
        }

        /// <summary>
        /// 设置部件的位置
        /// </summary>
        /// <param name="point">点位置的设定</param>
        public void SetPosition(Point point)
        {
    
            Position = point;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="X">X的坐标</param>
        /// <param name="Y">Y的坐标</param>
        public GamePart(int X,int Y)
        {
            Position = new Point(X,Y);
        }
    }
}
