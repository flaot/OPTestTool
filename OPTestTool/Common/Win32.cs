using OPTestTool.Extension;
using OPTestTool.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OPTestTool.Common
{
    public static class Win32
    {
        /// <summary>
        /// 该函数返回桌面窗口的句柄。桌面窗口覆盖整个屏幕。桌面窗口是一个要在其上绘制所有的图标和其他窗口的区域。
        /// 【说明】获得代表整个屏幕的一个窗口（桌面窗口）句柄.
        /// </summary>
        /// <returns>返回值：函数返回桌面窗口的句柄。</returns>
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetDesktopWindow();

        /// <summary>
        /// 获取根窗口句柄
        /// </summary>
        /// <returns></returns>
        public static int GetDesktopWindows()
        {
            return Convert.ToInt32(GetDesktopWindow().ToString());
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern long FlashWindow(IntPtr hwnd, bool bInvert);

        /// <summary>
        /// 窗口闪烁
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns></returns>
        public static long FlashWindow(int hwnd)
        {
            return FlashWindow(new IntPtr(hwnd), true);
        }

        /// <summary>
        /// 闪烁窗口
        /// </summary>
        /// <param name="pwfi">窗口闪烁信息结构</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        /// <summary>
        /// 闪烁类型
        /// </summary>
        public enum flashType : uint
        {
            FLASHW_STOP = 0, //停止闪烁
            FALSHW_CAPTION = 1, //只闪烁标题
            FLASHW_TRAY = 2, //只闪烁任务栏
            FLASHW_ALL = 3, //标题和任务栏同时闪烁
            FLASHW_PARAM1 = 4,
            FLASHW_PARAM2 = 12,
            FLASHW_TIMER = FLASHW_TRAY | FLASHW_PARAM1, //无条件闪烁任务栏直到发送停止标志或者窗口被激活，如果未激活，停止时高亮
            FLASHW_TIMERNOFG = FLASHW_TRAY | FLASHW_PARAM2 //未激活时闪烁任务栏直到发送停止标志或者窗体被激活，停止后高亮
        }

        /// <summary>
        /// 包含系统应在指定时间内闪烁窗口次数和闪烁状态的信息
        /// </summary>
        public struct FLASHWINFO
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// 要闪烁或停止的窗口句柄
            /// </summary>
            public IntPtr hwnd;

            /// <summary>
            /// 闪烁的类型
            /// </summary>
            public uint dwFlags;

            /// <summary>
            /// 闪烁窗口的次数
            /// </summary>
            public uint uCount;

            /// <summary>
            /// 窗口闪烁的频度，毫秒为单位；若该值为0，则为默认图标的闪烁频度
            /// </summary>
            public uint dwTimeout;
        }

        /// <summary>
        /// 闪烁窗口
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="type">闪烁类型</param>
        /// <returns></returns>
        public static bool FlashWindowEx(IntPtr hWnd, flashType type = flashType.FLASHW_ALL)
        {
            FLASHWINFO fInfo = new FLASHWINFO();
            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;//要闪烁的窗口的句柄，该窗口可以是打开的或最小化的
            fInfo.dwFlags = (uint)type;//闪烁的类型
            fInfo.uCount = UInt32.MaxValue;//闪烁窗口的次数
            fInfo.dwTimeout = 0; //窗口闪烁的频度，毫秒为单位；若该值为0，则为默认图标的闪烁频度
            return FlashWindowEx(ref fInfo);
        }

        #region 提取ICO图片

        [System.Runtime.InteropServices.DllImport("shell32.dll")]
        private static extern int ExtractIconEx(string lpszFile, int niconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons);

        private static IntPtr[] largeIcons, smallIcons;  //存放大/小图标的指针数组

        public static void GetIcon(string path)
        {
            //第一步：获取程序中的图标数

            //int IconCount = ExtractIconEx("", -1, null, null, 0);

            //第二步：创建存放大/小图标的空间

            largeIcons = new IntPtr[2000];

            smallIcons = new IntPtr[2000];

            //第三步：抽取所有的大小图标保存到largeIcons和smallIcons中

            int IconCount = ExtractIconEx(path, 1, largeIcons, smallIcons, 2000);

            //第四步：显示抽取的图标(推荐使用imageList和listview搭配显示）
            ImageList imageList = new ImageList();
            for (int i = 0; i < IconCount; i++)
            {
                imageList.Images.Add(Icon.FromHandle(largeIcons[i]));
            }
        }

        /// <summary>
        /// 获取可见窗口的ICO图片
        /// </summary>
        /// <param name="hwndp">单独获取某个窗口ICO图</param>
        /// <returns></returns>
        public static Tuple<ImageList, Dictionary<int, int>> GetProcessIco(OpSoft mk, int hwndp = 0)
        {
            Random random = new Random();
            string appPath = AppDomain.CurrentDomain.BaseDirectory;

            //key=窗口句柄 ，value是索引
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            ImageList imageList = new ImageList();

            Bitmap exefalse = new Bitmap(Resources.exefalse);
            Bitmap exetrue = new Bitmap(Resources.exetrue);

            imageList.Images.Add(exefalse);
            imageList.Images.Add(exetrue);

            var hwndList = mk.EnumWindow(0, "", "", 4 + 8 + 16)
                .Split(',')
                .ToList()
                .ToList();

            //是否只获取指定窗口
            if (hwndp != 0)
            {
                hwndList.Clear();
                hwndList.Add(hwndp.ToString());
            }

            hwndList.ForEach(hf =>
            {   //Icon.ExtractAssociatedIcon 获取指定路径的ICO图片
                string path = mk.GetWindowProcessPath(hf.ToInt32()).FilteQuotes();
                try
                {
                    //length>0 说明有路径；Contains(".") 说明里面有.exe 文件；!path.Contains("ApplicationFrameHost.exe") 改文件是非法exe
                    if (path.Length > 0 & path.Contains(".") & !path.Contains("ApplicationFrameHost.exe"))
                    {
                        Image image = Icon.ExtractAssociatedIcon(path).ToBitmap();
                        imageList.Images.Add(image);
                        keyValuePairs.Add(hf.ToInt32(), imageList.Images.Count - 1);
                    }
                }
                catch
                {
                }
            });
            return new Tuple<ImageList, Dictionary<int, int>>(imageList, keyValuePairs);
        }

        #endregion 提取ICO图片
    }
}