using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// 代表一个食物颗粒,继承自GamePart
    /// </summary>
    class FoodPellet : GamePart
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="X">食品颗粒的X坐标</param>
        /// <param name="Y">食品颗粒的Y坐标</param>
        public FoodPellet(int X, int Y) : base(X,Y) {}
    }
}
