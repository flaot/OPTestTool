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
        
        public enum WindOpt : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
        }
    
     
        [DllImport("user32", EntryPoint = "IsWindow", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int IsWindow(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "IsWindowUnicode", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        internal static extern int IsWindowUnicode(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "SetCapture", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        internal static extern int SetCapture(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "GetParent", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern Int32 GetWindowText(IntPtr hWnd, StringBuilder text, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder className, int length);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT pt);

        [DllImport("user32", EntryPoint = "GetWindowThreadProcessId", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32")]
        public static extern IntPtr GetWindow(IntPtr hWnd, WindOpt nCom);
        [DllImport("User32")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int ToUnicode(uint virtualKeyCode, uint scanCode, byte[] keyboardState,
        [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder receivingBuffer, int bufferSize, uint flags);
      
    }
}
