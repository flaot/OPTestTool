using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowFinder
{
    public partial class HighlightOverlayForm : Form
    {
        private IntPtr targetHwnd;
        private int animationStep = 0;
        private const int borderWidth = 4;

        public HighlightOverlayForm()
        {
            InitializeComponent();

            // 允许透明
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.LimeGreen; // 透明色键
            TransparencyKey = BackColor;

            // 点击穿透
            int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE);
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
      
        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animationStep = (animationStep + 1) % 20;
            UpdatePosition();
            Invalidate(); // 触发重绘
        }
        public void SetTarget(IntPtr hwnd)
        {
            targetHwnd = hwnd;
            // 启动定时器动画
            if (!animationTimer.Enabled)
            {
                UpdatePosition();
                animationTimer.Start();
            }
        }
        public void DelayDestry(int time)
        {
            destryTimer.Interval = time;
            destryTimer.Start();
        }
        private void DestryTimer_Tick(object sender, EventArgs e)
        {
            destryTimer.Stop();
            Dispose();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 计算动画颜色，闪烁效果
            int alpha = (int)(128 + 127 * Math.Sin(animationStep * Math.PI / 10));
            Color borderColor = Color.FromArgb(alpha, Color.Red);

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                Rectangle rect = new Rectangle(borderWidth / 2, borderWidth / 2, Width - borderWidth, Height - borderWidth);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }
        private void UpdatePosition()
        {
            if (targetHwnd == IntPtr.Zero)
                return;

            if (!GetWindowRect(targetHwnd, out RECT rect))
                return;

            // 设置覆盖窗口位置和大小，扩大边框宽度
            int x = rect.left - borderWidth;
            int y = rect.top - borderWidth;
            int width = rect.Width + borderWidth * 2;
            int height = rect.Height + borderWidth * 2;

            SetBounds(x, y, width, height);
        }
        #region Win32 API

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left, top, right, bottom;
            public int Width => right - left;
            public int Height => bottom - top;
        }

        #endregion
    }
}
