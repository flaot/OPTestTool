using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.StaticData
{
    /// <summary>
    /// 绑定参数静态类
    /// </summary>
    public static class BindWindow
    {

        public static int SetAero = 0;

        public static string Display = "normal";

        public static string Mouse = "normal";

        public static string Keypad = "normal";

        public static string Public = "";//可空

        public static int Mode = 0;

        public static bool MoveWindow = false;

        public static bool BindWindowSuccessTips = false;

        public static bool BeforeBindActive = false;//绑定前激活=true，否则是绑定后激活

        public static string PlugObj = "dm";
        public static int Hwnd = 0;
    }
}
