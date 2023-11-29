/*
Copyright (c) 2014, Joe Esposito <joe@joeyespo.com>

Permission to use, copy, modify, and/or distribute this software for any
purpose with or without fee is hereby granted, provided that the above
copyright notice and this permission notice appear in all copies.

THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
 */

// Win32.cs
// By Joe Esposito

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WindowFinder
{
    public delegate bool WndEnumProc(IntPtr hWnd, int lParam);

    /// <summary>
    /// Win32 API.
    /// </summary>
    public static class Win32
    {
        /// <summary>
        /// Gets the text of the specified window.
        /// </summary>
        public static string GetWindowText(IntPtr hWnd)
        {
            int textLength = GetWindowTextLength(hWnd);
            if (SystemInformation.DbcsEnabled)
                textLength = (textLength * 2) + 1;
            textLength++;

            StringBuilder text = new StringBuilder(textLength);
            Marshal.ThrowExceptionForHR(GetWindowText(hWnd, text, text.Capacity));
            return text.ToString();
        }

        /// <summary>
        /// Gets the class name of the specified window.
        /// </summary>
        public static string GetClassName(IntPtr hWnd)
        {
            StringBuilder text = new StringBuilder(256);
            int hr = GetClassName(hWnd, text, 256);
            Marshal.ThrowExceptionForHR(hr);
            return text.ToString().Trim();
        }

        [Flags]
        enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000,
            ReadControl = 0x00020000,
            PROCESS_QUERY_LIMITED_INFORMATION = 0x1000
        }

        /// <summary>
        /// Retrieves the window from the client point.
        /// </summary>
        internal static IntPtr WindowFromPoint(IntPtr hClientWnd, int xPoint, int yPoint)
        {
            POINT pt;
            pt.x = xPoint;
            pt.y = yPoint;
            return WindowFromPoint(pt);
        }

        public static int GetWindowPID(IntPtr hWnd)
        {
            int tid = GetWindowThreadProcessId(hWnd, out var pid);
            if (tid == 0)
                return 0;
            return pid;
        }

        /// <summary>
        /// Highlights the specified window.
        /// </summary>
        public static bool HighlightWindow(IntPtr hWnd)
        {
            // Get the screen coordinates of the rectangle of the window.
            RECT rt = new RECT();         // Rectangle area of the window.
            GetWindowRect(hWnd, ref rt);
            //Debug.WriteLine(string.Format("{0} {1},{2},{3},{4}", (int)hWnd, rt.left, rt.top, rt.Width, rt.Height));
            Rectangle rct = Rectangle.Empty;
            if (rt.Width > 0 && rt.Height > 0)
            {
                rct = new Rectangle(rt.left, rt.top, rt.Width, rt.Height);
            }

            using (var rgn = CreateRectangleRegion(rct, 2))
            {
                var hDc = GetDC(IntPtr.Zero);
                SelectClipRgn(hDc, rgn.GetHrgn(Graphics.FromHdc(hDc)));

                var myBox = new RECT();
                GetClipBox(hDc, ref myBox);

                var brHandler = HighlightBrush;
                var oldHandle = SelectObject(hDc, brHandler);

                PatBlt(hDc,
                    myBox.left,
                    myBox.top,
                    myBox.right - myBox.left,
                    myBox.bottom - myBox.top,
                    (uint)RasterOperations.PATINVERT);

                SelectObject(hDc, oldHandle);
                SelectClipRgn(hDc, IntPtr.Zero);
                ReleaseDC(IntPtr.Zero, hDc);
            }
            return true;
        }

        private static IntPtr _HighlightBrush = IntPtr.Zero;

        /// <summary>
        /// Gets the brush that iwll be used for highlighting
        /// </summary>
        internal static IntPtr HighlightBrush
        {
            get
            {
                if (_HighlightBrush == IntPtr.Zero)
                {
                    _HighlightBrush = CreateHighlightBrushBrush();
                }

                return _HighlightBrush;
            }
        }
        /// <summary>
        /// Create brush which will be used for drawing frames
        /// </summary>
        /// <returns></returns>
        static IntPtr CreateHighlightBrushBrush()
        {
            Bitmap bitmap = new Bitmap(8, 8, PixelFormat.Format32bppArgb);
            byte[] lights = new byte[] { 255, 255, 255, 255 };
            byte[] darks = new byte[] { 255, 0, 0, 0 };

            bool useLights = true;

            BitmapData data = null;
            try
            {
                data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                IntPtr scan = data.Scan0;
                unsafe
                {
                    byte* p = (byte*)(void*)scan;

                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            if (useLights)
                            {
                                p[0] = lights[0]; p[1] = lights[1]; p[2] = lights[2]; p[3] = lights[3];
                                p += 4;
                            }
                            else
                            {
                                p[0] = darks[0]; p[1] = darks[1]; p[2] = darks[2]; p[3] = darks[3];
                                p += 4;
                            }

                            useLights = !useLights;
                        }
                    }
                }

                bitmap.UnlockBits(data);
            }
            catch
            {
                if (data != null)
                    bitmap.UnlockBits(data);
            }

            IntPtr hBitmap = bitmap.GetHbitmap();

            LOGBRUSH brush = new LOGBRUSH();
            brush.lbStyle = (uint)BrushStyles.BS_HATCHED;
            brush.lbHatch = (uint)hBitmap;

            return CreateBrushIndirect(ref brush);
        }
        /// <summary>
        /// Creates a border region for the given rectangle area
        /// </summary>
        /// <param name="rect">The area</param>
        /// <param name="thickness">Thickness of the border</param>
        /// <returns></returns>
        internal static Region CreateRectangleRegion(Rectangle rect, int thickness)
        {
            var region = new Region(rect);

            if ((thickness <= 0) || (rect.Width <= 2 * thickness)
                || (rect.Height <= 2 * thickness))
            {
                return region;
            }

            rect.X += thickness;
            rect.Y += thickness;
            rect.Width -= 2 * thickness;
            rect.Height -= 2 * thickness;

            region.Xor(rect);

            return region;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct POINTAPI
        {
            public int x;
            public int y;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        // Type definitions for Windows' basic types.
        public const int ANYSIZE_ARRAY = unchecked((int)(1));
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public int Width => right - left;
            public int Height => bottom - top;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct LOGBRUSH
        {
            public uint lbStyle;
            public uint lbColor;
            public uint lbHatch;
        }
        public enum BrushStyles
        {
            BS_SOLID = 0,
            BS_NULL = 1,
            BS_HOLLOW = 1,
            BS_HATCHED = 2,
            BS_PATTERN = 3,
            BS_INDEXED = 4,
            BS_DIBPATTERN = 5,
            BS_DIBPATTERNPT = 6,
            BS_PATTERN8X8 = 7,
            BS_DIBPATTERN8X8 = 8,
            BS_MONOPATTERN = 9
        }
        public enum RasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062
        }
        public enum WindOpt : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
        }

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateBrushIndirect(ref LOGBRUSH brush);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDc, IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern int SelectClipRgn(IntPtr hDc, IntPtr hRgn);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern int GetClipBox(IntPtr hDc, ref RECT rectBox);
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PatBlt(IntPtr hDc, int x, int y, int width, int height, uint flags);

        [DllImport("user32", EntryPoint = "IsWindow", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int IsWindow(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "IsWindowUnicode", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        internal static extern int IsWindowUnicode(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "SetCapture", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        internal static extern int SetCapture(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "ClientToScreen", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int ClientToScreen(IntPtr hWnd, ref POINTAPI lpPoint);

        [DllImport("user32", EntryPoint = "MapWindowPoints", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int MapWindowPoints(IntPtr hwndFrom, IntPtr hwndTo, ref RECT lprt, int cPoints);

        [DllImport("user32", EntryPoint = "MapWindowPoints", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int MapWindowPoints(IntPtr hwndFrom, IntPtr hwndTo, ref POINTAPI lppt, int cPoints);

        [DllImport("user32", EntryPoint = "ChildWindowFromPoint", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int ChildWindowFromPoint(IntPtr hWnd, int xPoint, int yPoint);

        [DllImport("user32", EntryPoint = "GetParent", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern Int32 GetWindowText(IntPtr hWnd, StringBuilder text, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder className, int length);

        [DllImport("user32", EntryPoint = "GetWindowDC", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetWindowDC(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "GetWindowRect", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("gdi32", EntryPoint = "CreateRectRgnIndirect", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int CreateRectRgnIndirect(ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT pt);

        [DllImport("user32", EntryPoint = "GetWindowRgn", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport("gdi32", EntryPoint = "SetROP2", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int SetROP2(IntPtr hdc, int nDrawMode);

        [DllImport("gdi32", EntryPoint = "FrameRgn", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int FrameRgn(IntPtr hdc, IntPtr hRgn, IntPtr hBrush, int nWidth, int nHeight);

        [DllImport("gdi32", EntryPoint = "GetStockObject", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetStockObject(int nIndex);

        [DllImport("user32", EntryPoint = "GetWindowThreadProcessId", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("gdi32", EntryPoint = "DeleteObject", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int DeleteObject(IntPtr hObject);

        [DllImport("user32", EntryPoint = "ReleaseDC", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);
        [DllImport("user32")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32")]
        public static extern IntPtr GetWindow(IntPtr hWnd, WindOpt nCom);
        [DllImport("User32")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int ToUnicode(uint virtualKeyCode, uint scanCode, byte[] keyboardState,
        [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder receivingBuffer, int bufferSize, uint flags);

        [DllImport("user32.dll")]
        public static extern int GetCursorPos(ref Point lpPoint);
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
    }
}
