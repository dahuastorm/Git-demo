using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    /// <summary>
    ///管理游戏的键盘输入
    /// </summary>
    class Input
    {
        // 存储键盘键映射到按钮是否被按下
        private static Dictionary<Keys,bool> KeyTable = new Dictionary<Keys,bool>();

        /// <summary>
        ///获取某个键是否被按下
        /// </summary>
        /// <param name="key">检查键是否被握住</param>
        /// <returns>是否按下了键</returns>
        public static bool IsKeyDown(Keys key)
        {
            bool KeyState;
            if (KeyTable.TryGetValue(key,out KeyState))
            {
                return KeyState;
            }
            return false;
        }

        /// <summary>
        ///  设置要按或释放的键盘按钮
        /// </summary>
        /// <param name="key">键设置</param>
        /// <param name="IsDown">设置键的值，真正意义为按下</param>
        public static void SetKey(Keys key,bool IsDown)
        {
            KeyTable[key] = IsDown;
        }
    }
}
