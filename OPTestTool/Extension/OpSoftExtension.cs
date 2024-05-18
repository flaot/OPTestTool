using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OPTestTool.Extension
{
    public static class OpSoftExtension
    {
        /// <summary>
        /// 寻找指点图片后移动到自定义坐标上鼠标左键单击。
        /// </summary>
        /// <param name="mk">插件</param>
        /// <param name="picName">图片名</param>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="sim">识别精准度默认0.9</param>
        /// <returns>0=未找到、1=已找到且已经点击鼠标左键单击</returns>
        public static int FindPicMoveToCustomPointLeftClickEx(this OpSoft mk, string picName, int x, int y, double sim = 0.9)
        {
            int outx = 0;
            int outy = 0;
            var findRes = mk.FindPic(0, 0, 1920, 1080, picName, "000000", 0.9, 0, out outx, out outy);
            if (findRes != -1)
            {
                mk.MoveTo(x, y);
                mk.Delay(100);
                mk.LeftClick();
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 寻找指定图片后移动到返回的坐标上鼠标左键单击
        /// </summary>
        /// <param name="mk">插件</param>
        /// <param name="picName">图片名</param>
        /// <param name="sim">识别精度可空，默认0.9</param>
        /// <returns></returns>
        public static int FindPicMoveToPointLeftClickEx(this OpSoft mk, string picName, double sim = 0.9)
        {
            //登录_关闭按钮1
            int outx = 0;
            int outy = 0;
            if (mk.FindPic(0, 0, 1920, 1080, picName, "000000", sim, 0, out outx, out outy) != -1)
            {
                mk.MoveTo(outx, outy);
                mk.Delay(100);
                mk.LeftClick();
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// x,y坐标大于-1 鼠标移动到指定坐标左键单击
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="sleep">延迟时间</param>
        public static bool MoveToLeftClick(this OpSoft mk, int x, int y, Action<int> sleep, int time = 300)
        {
            if (x > -1 & y > -1)
            {
                mk.MoveTo(x, y);
                sleep(time);
                mk.LeftClick();
                return true;
            }
            return false;
        }

        /// <summary>
        /// x,y坐标大于-1 鼠标移动到指定坐标左键双击
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="sleep">延迟时间</param>
        public static bool MoveToLeftDoubleClick(this OpSoft mk, int x, int y, Action<int> sleep, int time = 300)
        {
            mk.MoveTo(x, y);
            sleep(time);
            mk.LeftClick();
            //mk.Delay(300);
            //mk.LeftClick();
            mk.Delay(100);
            mk.LeftClick();
            return true;
        }

        /// <summary>
        /// 移动到相对位置后进行鼠标左键单击
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="x">相对位置X</param>
        /// <param name="y">相对位置Y</param>
        /// <param name="sleep">移动后待定多久后进行鼠标左键单击</param>
        /// <param name="time">延迟时间</param>
        /// <returns></returns>
        public static void MoveRLeftClick(this OpSoft mk, int x, int y, Action<int> sleep, int time = 300)
        {
            mk.MoveR(x, y);
            sleep(time);
            mk.LeftClick();
        }

        /// <summary>
        /// 是否找到图片
        /// </summary>
        /// <param name="mk">插件</param>
        /// <param name="picName">图片名，可以多图片</param>
        /// <param name="sim">识别精准度</param>
        /// <returns>找到返回true；否则返回false</returns>
        public static bool IsFindPicEx(this OpSoft mk, string picName, double sim = 0.9)
        {
            return mk.FindPic(0, 0, 1920, 1080, picName, "000000", sim, 0, out int outx, out int outy).FindPicSuccess();
        }

        /// <summary>
        /// 是否找到图片
        /// </summary>
        /// <param name="mk">插件</param>
        /// <param name="x1">x1坐标</param>
        /// <param name="y1">y1坐标</param>
        /// <param name="x2">x2坐标</param>
        /// <param name="y2">y2坐标</param>
        /// <param name="picName">图片名，可以多图片</param>
        /// <param name="sim">识别精准度</param>
        /// <returns>找到返回true；否则返回false</returns>
        public static bool IsFindPicEx(this OpSoft mk, int x1, int y1, int x2, int y2, string picName, double sim = 0.9)
        {
            return mk.FindPic(x1, y1, x2, y2, picName, "000000", sim, 0, out int outx, out int outy).FindPicSuccess();
        }

        /// <summary>
        /// 延迟
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="time">1秒=1000</param>
        /// <returns>插件对象</returns>
        public static OpSoft SleepEx(this OpSoft mk, int time)
        {
            Thread.Sleep(time);
            return mk;
        }

        /// <summary>
        /// 把鼠标移动到目的点(x,y)
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns>插件对象</returns>
        public static OpSoft MoveToEx(this Tuple<OpSoft, bool, int, int> t)
        {
            t.Item1.MoveTo(t.Item3, t.Item4);
            return t.Item1;
        }

        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <returns>插件对象</returns>
        public static OpSoft ClickEx(this OpSoft mk)
        {
            mk.LeftClick();
            return mk;
        }

        /// <summary>
        /// 设置窗口置顶
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns>插件对象</returns>
        public static OpSoft SetWindowTopEx(this OpSoft mk, int hwnd)
        {
            mk.SetWindowState(hwnd, 8);
            return mk;
        }

        /// <summary>
        /// 强制结束窗口所有进程
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns>插件对象</returns>
        public static OpSoft CloseWindowEx(this OpSoft mk, int hwnd)
        {
            mk.SetWindowState(hwnd, 13);
            return mk;
        }

        /// <summary>
        /// 插件执行是否成功
        /// </summary>
        /// <param name="result">插件的大部分返回结果</param>
        /// <returns>如果是0=false，1=true;</returns>
        public static bool IsSuccessEx(this int result)
        {
            return result == 0 ? false : true;
        }

        /// <summary>
        /// 鼠标左键双击
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <returns>插件对象</returns>
        public static OpSoft DoubleClickEx(this OpSoft mk)
        {
            mk.LeftDoubleClick();
            return mk;
        }

        /// <summary>
        /// 查找多个图片,只返回第一个找到的X Y坐标
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="picName">图片名,可以是多个图片,比如"test.bmp|test2.bmp|test3.bmp"</param>
        /// <param name="sim">双精度浮点数:相似度,取值范围0.1-1.0</param>
        /// <returns>Item1=插件对象、Item2=寻找结果ture=找到，false=没找到、Item3=X坐标、Item4=Y坐标</returns>
        public static Tuple<OpSoft, bool, int, int> FindPicEx(this OpSoft mk, string picName, double sim = 0.9)
        {
            int outx = 0;
            int outy = 0;
            if (mk.FindPic(0, 0, 1920, 1080, picName, "000000", sim, 0, out outx, out outy) != -1)
            {
                return new Tuple<OpSoft, bool, int, int>(mk, true, outx, outy);
            }
            return new Tuple<OpSoft, bool, int, int>(mk, false, outx, outy);
        }

        /// <summary>
        /// 返回指定窗口的  句柄|标题|类名
        /// </summary>
        /// <typeparam name="T">int、string</typeparam>
        /// <param name="mk">插件对象</param>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns>句柄|标题|类名</returns>
        public static string GetWindowHwndTitleClassName<T>(this OpSoft mk, T hwnd)
        {
            int hwndc = hwnd.ToInt32();
            if (hwndc == 0)
                return "0";
            return $"{hwndc}|{mk.GetWindowTitle(hwndc)}|{mk.GetWindowClass(hwndc)}";
        }

        /// <summary>
        /// 指定窗口句柄是否可见
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns>可见返回：true；否则返回:false;</returns>
        public static bool IsWindowsVisible(this OpSoft mk, int hwnd)
        {
            return mk.GetWindowState(hwnd, 2).ToBool();
        }

        /// <summary>
        /// 寻找父窗口
        /// </summary>
        /// <param name="child">所有可见已经不可见窗口</param>
        /// <param name="parent">所有可见窗口</param>
        /// <returns>Key=子窗口 value=父窗口</returns>
        public static Dictionary<string, string> FindParent(this OpSoft mk, List<string> child, List<string> parent)
        {
            Dictionary<string, string> keyValue = new Dictionary<string, string>();
            try
            {
                parent.ForEach(f =>
                {
                    child.Remove(f);
                });

                child.ForEach(f =>
                {
                    var resHwnd = mk.GetWindow(int.Parse(f), 0).ToString();
                    keyValue.Add(f, resHwnd);
                });
            }
            catch (Exception ex)
            {
                //ex.ToString().ToLog();
            }
            return keyValue;
        }

        /// <summary>
        /// FindPic成功找到图片
        /// </summary>
        /// <param name="findRes">图片名</param>
        /// <returns>成功返回true；否则返回false</returns>
        public static bool FindPicSuccess(this int findRes)
        {
            return findRes == -1 ? false : true;
        }

        /// <summary>
        /// 强制关闭绑定的窗口句柄
        /// </summary>
        /// <param name="mk"></param>
        /// <returns></returns>
        public static bool CloseBindWindowHwnd(this OpSoft mk)
        {
            if (mk == null) return false;
            var hwnd = mk.GetBindWindow();
            if (hwnd > 0)
                return mk.SetWindowState(hwnd, 13).IsSuccessEx();
            return false;
        }

        /// <summary>
        /// 寻找到的图片名是否等于需要寻找的图片名
        /// </summary>
        /// <param name="findPicName">寻找到的图片名返回值</param>
        /// <param name="picName">需要寻找的图片名</param>
        /// <returns>相等返回true；否则返回false</returns>
        public static bool FindPicNameIs(this string findPicName, string picName)
        {
            if (findPicName == picName)
                return true;
            return false;
        }

        /// <summary>
        /// 模糊寻找图片名是否匹配需要寻找的图片名
        /// </summary>
        /// <param name="findPicName">寻找到的图片名返回值</param>
        /// <param name="picName">需要寻找的图片名</param>
        /// <returns>相等返回true；否则返回false</returns>
        public static bool FindPicNameContains(this string findPicName, string picName)
        {
            return findPicName.Contains(picName);
        }

        /// <summary>
        /// 将FindPicEx返回的坐标转换为List Point类型
        /// </summary>
        /// <param name="resDatas">FindPicEx返回值</param>
        /// <returns>如果找到一张或多张图片则返回图片的Point，否则返回null</returns>
        public static List<Point> FindPicExConvertToListPoint(this string resDatas)
        {
            List<Point> points = new List<Point>();
            try
            {
                //"0,100,20|2,30,40|3,30,40"
                if (string.IsNullOrEmpty(resDatas) == false)
                {
                    foreach (var item in resDatas.Split('|'))
                    {
                        points.Add(new Point
                        {
                            X = item.Split(',')[1].ToInt32(),
                            Y = item.Split(',')[2].ToInt32()
                        });
                    }

                    for (int i = 0; i < points.Count; i++)
                    {
                        for (int q = 0; q < points.Count; q++)
                        {
                            if (i == q)
                                continue;
                            if (Math.Abs(points[i].X - points[q].X) <= 4
                                & Math.Abs(points[i].Y - points[q].Y) <= 4)
                            {
                                points.RemoveAt(q);
                                //points.Remove(points[q]);
                                //points[1].X=points[i].X;
                            }
                        }
                    }
                    //剔除重复坐标
                    return points;
                }
                return null;
            }
            catch (Exception ex)
            {
                return points;
            }
        }

        /// <summary>
        /// 将FindPicEx返回的坐标转换为List Point类型
        /// </summary>
        /// <param name="resDatas">FindPicEx返回值</param>
        /// <returns>如果找到一张或多张图片则返回图片的Point，否则返回null</returns>
        public static List<Point> FindPicExConvertToListPoint(this string resDatas, int customX = 4, int customY = 4)
        {
            List<Point> points = new List<Point>();
#if DEBUG
#endif
            //"0,100,20|2,30,40|3,30,40"
            if (string.IsNullOrEmpty(resDatas) == false)
            {
                foreach (var item in resDatas.Split('|'))
                {
                    points.Add(new Point
                    {
                        X = item.Split(',')[1].ToInt32(),
                        Y = item.Split(',')[2].ToInt32()
                    });
                }

                for (int i = 0; i < points.Count; i++)
                {
                    for (int q = 0; q < points.Count; q++)
                    {
                        if (i == q)
                            continue;
                        if (Math.Abs(points[i].X - points[q].X) <= customX
                            & Math.Abs(points[i].Y - points[q].Y) <= customY)
                        {
                            points.Remove(points[q]);
                        }
                    }
                }
                //剔除重复坐标
                return points;
            }
            return null;
        }

        /// <summary>
        /// 将FindPicExS的返回值转换为ListTuple
        /// </summary>
        /// <param name="resDatas">FindPicExS返回值</param>
        /// <returns>图片名，坐标x，坐标y</returns>
        public static List<Tuple<string, int, int>> FindPicExSConvertToList(this string resDatas)
        {
            List<Tuple<string, int, int>> tuples = new List<Tuple<string, int, int>>();
            if (string.IsNullOrEmpty(resDatas) == false)
            {
                foreach (var item in resDatas.Split('|'))
                {
                    tuples.Add(new Tuple<string, int, int>
                        (
                        item.Split(',')[0].Replace(".bmp", ""),
                        item.Split(',')[1].ToInt32(),
                        item.Split(',')[2].ToInt32()
                        ));
                }
            }
            return tuples;
        }

        /// <summary>
        /// 获取鼠标在当前窗口的相对位置
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <returns>返回获取到的坐标值</returns>
        public static Tuple<int, int> GetMousePoint(this OpSoft mk)
        {
            int x, y;
            int rectX1, rectY1, rectX2, rectY2;
            mk.GetCursorPos(out x, out y);

            mk.GetClientRect(mk.GetMousePointWindow(), out rectX1, out rectY1, out rectX2, out rectY2);
            x = x - rectX1;
            y = y - rectY1;

            return new Tuple<int, int>(x, y);
        }

        /// <summary>
        /// 判断窗口是否最小化
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="hwnd">窗口句柄：窗口句柄为0则获取绑定的窗口句柄，否则使用输入的窗口句柄</param>
        /// <returns>最小化返回true；否则返回false</returns>
        public static bool WindowIsMinimize(this OpSoft mk, int hwnd = 0)
        {
            if (hwnd == 0)
            {
                hwnd = mk.GetBindWindow();
            }
            int baseHwnd = mk.GetWindow(hwnd, 0);
            return mk.GetWindowState(baseHwnd, 3) == 1;
        }

        /// <summary>
        /// 激活指定窗口（可让窗口获取焦点，也可让最小化窗口恢复正常）
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="hwnd">窗口句柄：窗口句柄为0则获取绑定的窗口句柄，否则使用输入的窗口句柄</param>
        /// <returns>激活成功返回true；否则返回false</returns>
        public static bool ActiveWindow(this OpSoft mk, int hwnd = 0)
        {
            if (hwnd == 0)
            {
                hwnd = mk.GetBindWindow();
            }
            return mk.SetWindowState(hwnd, 1) == 1;
        }

        /// <summary>
        /// 窗口大小是否为指定大小（用于判断窗口大小是否被改变）
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="width">窗口宽度</param>
        /// <param name="height">窗口高度</param>
        /// <param name="hwnd">窗口句柄：窗口句柄为0则获取绑定的窗口句柄，否则使用输入的窗口句柄</param>
        /// <returns>窗口为指定大小返回true；否则返回false</returns>
        public static bool WindowsSizeIsEqual(this OpSoft mk, int width, int height, int hwnd = 0)
        {
            if (hwnd == 0)
            {
                hwnd = mk.GetBindWindow();
            }
            mk.GetClientSize(hwnd, out int w, out int h); //[xy-1004,573 ]
            return width == w && height == h;
        }

        /// <summary>
        /// 通过子窗体设置模拟器窗口大小
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="width">宽（可空，默认1002）</param>
        /// <param name="height">高（可空，默认576）</param>
        /// <param name="hwnd">窗口（可空,默认绑定窗口）</param>
        /// <returns></returns>
        public static bool SetSimulatorSizeByChild(this OpSoft mk, int width = 1002, int height = 576, int hwnd = 0)
        {
            if (hwnd == 0)
            {
                hwnd = mk.GetBindWindow();
            }
            if (hwnd == 0)
                return false;
            hwnd = mk.GetBindWindow();
            var fristHwnd = mk.GetWindow(hwnd, 7);

            return mk.SetWindowSize(fristHwnd, width, height) == 1;
        }

        /// <summary>
        /// 通过子窗体设置模拟器窗口大小【自适应】
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="width">960-width后的值</param>
        /// <param name="height">540-height后的值</param>
        /// <param name="hwnd">窗口（可空,默认绑定窗口）</param>
        /// <returns></returns>
        public static bool SetSimulatorSizeByChildZiShiYing(this OpSoft mk, int width, int height, int hwnd = 0)
        {
            if (hwnd == 0)
            {
                hwnd = mk.GetBindWindow();
            }
            if (hwnd == 0)
                return false;
            hwnd = mk.GetBindWindow();
            var fristHwnd = mk.GetWindow(hwnd, 7);
            //获取父窗口大小
            mk.GetClientSize(fristHwnd, out int w, out int h);

            return mk.SetWindowSize(fristHwnd, w + width, h + height) == 1;
        }

        /// <summary>
        /// 移动窗口到屏幕中间
        /// </summary>
        /// <param name="mk"></param>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static bool MoveWindowToScreenCenter(this OpSoft mk, int hwnd)
        {
            if (hwnd == 0)
                return false;
            int simW = 0;
            int simH = 0;
            mk.GetClientSize(hwnd, out simW, out simH);
            int baseW = 0;
            int baseH = 0;
            mk.GetClientSize(mk.GetSpecialWindow(0), out baseW, out baseH);
            int moveX = baseW / 2 - simW / 2;
            int moveY = baseH / 2 - simH / 2;
            mk.MoveWindow(hwnd, moveX, moveY);
            return true;
        }

        /// <summary>
        /// 获取顶层窗口标题
        /// </summary>
        /// <param name="mk">插件对象</param>
        /// <param name="subHwnd">子句柄</param>
        /// <returns></returns>
        public static string GetTopWindowTitle(this OpSoft mk, int subHwnd)
        {
            int topHwnd = mk.GetWindow(subHwnd, 7);
            return mk.GetWindowTitle(topHwnd);
        }
    }
}