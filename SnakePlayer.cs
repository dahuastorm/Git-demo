using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    /// <summary>
    /// 使用枚举设定蛇移动的方向
    /// </summary>
    public enum Direction
    {
        left,
        right,
        up,
        down,
        none
    }

    /// <summary>
    /// 类，其中包含播放器的控制器逻辑
    /// </summary>
    class SnakePlayer
    {
        private List<BodyPart> m_SnakeParts = new List<BodyPart>(); // 蛇当前部分的集合
        private const int m_CircleRadius = 20; // 决定身体部位大小
        private Direction m_MoveDirection = Direction.none; // 头部方向
        private int m_PendingSegments; // 排队等待加入蛇的身体部位的数量
        private Snake GameForm = null; // 存储GUI表单

        /// <summary>
        /// SnakePlayer的构造函数
        /// </summary>
        /// <param name="Form">游戏的GUI设计</param>
        public SnakePlayer(Snake Form)
        {
            // 贪吃蛇初始位置
            m_SnakeParts.Add(new BodyPart(100, 200, Direction.right));
            m_SnakeParts.Add(new BodyPart(80, 200, Direction.right));
            m_SnakeParts.Add(new BodyPart(60, 200, Direction.right));

            //设定向右移动作为初始移动方向
            m_MoveDirection = Direction.right;

            //目前没有排队等待添加的身体部位
            m_PendingSegments = 0;
            GameForm = Form;
        }

        /// <summary>
        /// 添加蛇身部分 
        /// </summary>
        /// <param name="Number">要添加的蛇身部分的数量</param>
        public void AddBodySegments(int Number)
        {
            //当 m_PendingSegments Increments 被处理，并且在下一次调用MovePlayer（）时添加了部件 时增量
            m_PendingSegments += Number;
        }

        /// <summary>
        /// 移动蛇并处理任何待创建的身体部分。调用每帧。
        /// </summary>
        public void MovePlayer()
        {
            // 添加任何待处理的身体部分。注意，这一次只处理身体的一个部分;
            // 如果m_PendingSegments > 1, 它需要多个帧才能完全处理。
            if (m_PendingSegments > 0)
            {
                Point LastPos = m_SnakeParts.Last().GetPosition(); // 将身体部位添加到尾部
                m_SnakeParts.Add(new BodyPart(LastPos.X, LastPos.Y));
                m_PendingSegments--;
            }

            m_SnakeParts[0].m_Dir = m_MoveDirection; // 设置头部方向


            //移动蛇身的每一部分
            for (int i = m_SnakeParts.Count - 1; i >= 0; i--)
            {
                //根据方向翻转身体部分
                switch (m_SnakeParts[i].m_Dir)
                {
                    case Direction.left:
                        m_SnakeParts[i].AddPosition(new Point(-20, 0));
                        break;
                    case Direction.right:
                        m_SnakeParts[i].AddPosition(new Point(20, 0));
                        break;
                    case Direction.down:
                        m_SnakeParts[i].AddPosition(new Point(0, 20));
                        break;
                    case Direction.up:
                        m_SnakeParts[i].AddPosition(new Point(0, -20));
                        break;
                    default:
                        break;
                }

                // 设置下一部分的方向为上一部分的方向
                // 像贪吃蛇一样运动
                if (i > 0)
                    m_SnakeParts[i].m_Dir = m_SnakeParts[i - 1].m_Dir;
            }
            if (IsSelfIntersecting()) // 检查自残
                OnHitSelf(); // 如果是，就触发游戏结束画面
        }

        /// <summary>
        /// 确定蛇是否与其自身相交
        /// </summary>
        /// <returns>蛇是否与自身相交</returns>
        public bool IsSelfIntersecting()
        {
            // 检查每条蛇的身体部位与其他蛇身体部位
            for (int i = 0; i < m_SnakeParts.Count; i++)
            {
                for (int j = 0; j < m_SnakeParts.Count; j++)
                {
                    if (i == j) //不想检查自己的身体部位
                        continue;
                    BodyPart part1 = m_SnakeParts[i];
                    BodyPart part2 = m_SnakeParts[j];

                    // 碰撞检查逻辑
                    if ((new Rectangle(part1.GetPosition().X, part1.GetPosition().Y, m_CircleRadius, m_CircleRadius)).IntersectsWith(
                        new Rectangle(part2.GetPosition().X, part2.GetPosition().Y, m_CircleRadius, m_CircleRadius)))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 设置蛇头的方向 
        /// </summary>
        /// <param name="Dir">设置头的方向</param>
        public void SetDirection(Direction Dir)
        {
            // 禁止180度转弯
            if (m_MoveDirection == Direction.left && Dir == Direction.right)
                return;

            if (m_MoveDirection == Direction.right && Dir == Direction.left)
                return;

            if (m_MoveDirection == Direction.up && Dir == Direction.down)
                return;

            if (m_MoveDirection == Direction.down && Dir == Direction.up)
                return;

            // 如果方向改变是合法的，则设置方向
            m_MoveDirection = Dir;
        }

        /// <summary>
        /// 确定蛇任何身体部分是否与给定矩形相交
        /// </summary>
        /// <param name="rect">The rectangle to check intsections with</param>
        /// <returns>是否有交叉点</returns>
        public bool IsIntersectingRect(Rectangle rect)
        {
            foreach (BodyPart Part in m_SnakeParts) // 检查蛇的每一个身体部分 
            {
                Point PartPos = Part.GetPosition();

                // 检查蛇身与矩形交叉的部分 
                if (rect.IntersectsWith(new Rectangle(PartPos.X, PartPos.Y, m_CircleRadius, m_CircleRadius)))
                    return true;
            }
            return false;
        }

        /// <summary>
        ///在玩家撞墙的情况下显示游戏结束屏幕
        /// </summary>
        /// <param name="WhichWall">玩家击中墙壁的方向</param>
        public void OnHitWall(Direction WhichWall)
        {
            GameForm.ToggleTimer(); // 在游戏结束屏幕上看不到计时器
            //MessageBox.Show("Hit Wall- GAME OVER"); //显示游戏结束消息
            GameForm.ResetGame();
        }

        /// <summary>
        ///调用每帧渲染蛇身部分
        /// </summary>
        /// <param name="g">要在其上渲染的图形对象</param>
        public void Draw(Graphics g)
        {
            Random _rand = new Random();
            SolidBrush SnakeColor = new SolidBrush(Color.FromArgb(_rand.Next(100, 256), 0, 0));
            List<Rectangle> Rects = GetRects(); // 获取蛇的身体部位，以矩形表示
            foreach (Rectangle Part in Rects) // 绘制每个蛇身部位
            {
                g.FillEllipse(SnakeColor, Part); // 将蛇的部分绘制为椭圆
            }
        }

        /// <summary>
        ///当蛇产生自残时，显示游戏结束屏幕
        /// </summary>
        public void OnHitSelf()
        {
            GameForm.ToggleTimer(); // 在 game-over 屏幕显示时，结束计时器
            //MessageBox.Show("Hit SELF- GAME OVER"); // 显示game-over提示
            GameForm.ResetGame();
        }

        /// <summary>
        ///得到蛇身部分，用矩形表示 
        /// </summary>
        /// <returns>用矩形表示的蛇身体部分 </returns>
        public List<Rectangle> GetRects()
        {
            List<Rectangle> Rects = new List<Rectangle>();
            foreach (BodyPart Part in m_SnakeParts) //返回身体的各个部分  
            {
                Point PartPos = Part.GetPosition();

                //每次迭代，在代表身体部位的正在进行的列表中添加一个矩形
                Rects.Add(new Rectangle(PartPos.X, PartPos.Y, m_CircleRadius, m_CircleRadius));
            }
            return Rects;
        }
    }
}
