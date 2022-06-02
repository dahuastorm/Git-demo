using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Snake
{
    /// <summary>
    /// 表示一个蛇的身体部分，继承自GamePart
    /// </summary>
    class BodyPart : GamePart
    {
        public Direction m_Dir
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="X">身体部分的X坐标</param>
        /// <param name="Y">身体部分的Y坐标</param>
        /// <param name="Dir">身体部分移动的方向</param>
        public BodyPart(int X,int Y,Direction Dir) : base(X,Y)
        {
            m_Dir = Dir;
        }
        /// <summary>
        ///默认将方向设置为none的对象构造函数
        /// </summary>
        /// <param name="X">身体部分的X坐标</param>
        /// <param name="Y">身体部分的Y坐标</param>
        public BodyPart(int X, int Y):base(X,Y)
        {
            m_Dir = Direction.none;
        }
    }
}
