using ScriptTestTools.View.TestPicColor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.StaticData
{
    /// <summary>
    /// 测试图色静态类
    /// </summary>
    public static class PicColor
    {
        public static int x1 { get; set; }
        public static int y1 { get; set; }
        public static int x2 { get; set; }
        public static int y2 { get; set; }
        public static string SetPicPwd = "";

        public static int SpeedNormalGraphic = 0;

        public static int EnableGetColorByCapture = 0;

        public static bool CustomCodeIf = false;
        public static string CustomCodeIfContent = "";
        public static bool CustomCodeElse = false;
        public static string CustomCodeElseContent = "";

        public static event EventHandler FastSetFindPicAndFindPicExValueEvent;

        public static void FastSetFindPicAndFindPicExValue(string point)
        {
            FastSetFindPicAndFindPicExValueEvent.Invoke(point, null);
        }

        public static FrmTestPicColorShowPic frmTestPicColorShowPic = null;
    }
}