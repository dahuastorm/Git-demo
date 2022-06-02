using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    /// <summary>
    /// 管理食物颗粒，包括产生新的食物粒子，破坏和碰撞检测
    /// </summary>
    class FoodManager
    {
        private Random r = new Random(); //用于生成此类中的随机变量
        private List<FoodPellet> m_FoodPellets = new List<FoodPellet>(); // 在游戏中收集所有食物颗粒
        private const int m_CircleRadius = 20; //确定食物颗粒大小
        private int m_GameWidth; //游戏窗口大小以像素为单位，以确保程序在屏幕内绘制
        private int m_GameHeight;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="GameWidth">游戏窗口的像素宽度</param>
        /// <param name="GameHeight">游戏窗口的像素高度</param>
        public FoodManager(int GameWidth,int GameHeight)
        {
            m_GameWidth = GameWidth;
            m_GameHeight = GameHeight;
        }

        /// <summary>
        /// 绘制食物粒子
        /// </summary>
        /// <param name="g">要绘制的画布对象（游戏屏幕）</param>
        public void Draw(Graphics g)
        {
            // 迭代所有食物颗粒并绘制它们
            Brush SnakeColor = Brushes.BlueViolet;
            foreach (FoodPellet Pellet in m_FoodPellets)
            {
                Point PartPos = Pellet.GetPosition();
                g.FillEllipse(SnakeColor, new Rectangle(PartPos.X+(m_CircleRadius / 4), PartPos.Y+ (m_CircleRadius / 4), m_CircleRadius/2, m_CircleRadius/2));
            }
        }

        /// <summary>
        /// 在游戏中添加一个食物颗粒
        /// </summary>
        public void AddRandomFood()
        {
            int X = r.Next(m_GameWidth - m_CircleRadius); // 随机的x / y的位置
            int Y = r.Next(m_GameHeight - m_CircleRadius);
            int ix = (X / m_CircleRadius); // 使用舍位来对齐网格
            int iy = Y / m_CircleRadius;
            X = ix * m_CircleRadius; //网格x / y的位置
            Y = iy * m_CircleRadius;
            m_FoodPellets.Add(new FoodPellet(X, Y)); // 保存食物颗粒对象
        }

        /// <summary>
        /// 重写添加食物的数量
        /// </summary>
        /// <param name="Amount">增加的食物量</param>
        public void AddRandomFood(int Amount)
        {
            for(int i=0;i<Amount;i++)
            {
                AddRandomFood();
            }
        }

        /// <summary>
        /// 确定给定的矩形是否与任何现有的食物颗粒相交
        /// </summary>
        /// <param name="rect">检查到的与食物颗粒碰撞的矩形</param>
        /// <param name="RemoveFood">是否移除与矩形相交的食物颗粒</param>
        /// <returns>是否有交叉点</returns>
        public bool IsIntersectingRect(Rectangle rect, bool RemoveFood)
        {
            foreach (FoodPellet Pellet in m_FoodPellets) // 检查每个食物颗粒
            {
                Point PartPos = Pellet.GetPosition();

                // 检查与食品颗粒交叉的矩形
                if (rect.IntersectsWith(new Rectangle(PartPos.X, PartPos.Y, m_CircleRadius, m_CircleRadius)))
                {
                    if (RemoveFood) // 如果RemoveFood参数为true，则删除食品颗粒
                        m_FoodPellets.Remove(Pellet);
                    return true;
                }
            }
            return false;
        }
    }
}
