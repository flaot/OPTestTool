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
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowFinder.Properties;

namespace WindowFinder
{
    [DefaultEvent("WindowHandleChanged")]
    [ToolboxBitmap(typeof(ResourceFinder), "WindowFinder.Resources.WindowFinder.bmp")]
    [Designer(typeof(WindowFinderDesigner))]
    public sealed partial class WindowFinder : UserControl
    {
        private Timer _timer;
        HighlightOverlayForm overlay = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowFinder"/> class.
        /// </summary>
        public WindowFinder()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            SetStyle(ControlStyles.FixedWidth, true);
            SetStyle(ControlStyles.FixedHeight, true);
            SetStyle(ControlStyles.StandardClick, false);
            SetStyle(ControlStyles.StandardDoubleClick, false);
            SetStyle(ControlStyles.Selectable, false);

            picTarget.Size = new Size(31, 28);
            Size = picTarget.Size;

            _timer = new Timer();
            _timer.Interval = 300;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        #region Public Properties

        /// <summary>
        /// Called when the WindowHandle property is changed.
        /// </summary>
        public event EventHandler WindowHandleChanged;

        /// <summary>
        /// Handle of the window found.
        /// </summary>
        [Browsable(false)]
        public IntPtr WindowHandle => windowHandle;

        /// <summary>
        /// Handle text of the window found.
        /// </summary>
        [Browsable(false)]
        public string WindowHandleText => windowHandleText;

        /// <summary>
        /// Class of the window found.
        /// </summary>
        [Browsable(false)]
        public string WindowClass => windowClass;

        /// <summary>
        /// Text of the window found.
        /// </summary>
        [Browsable(false)]
        public string WindowText => windowText;

        /// <summary>
        /// Whether or not the found window is unicode.
        /// </summary>
        [Browsable(false)]
        public bool IsWindowUnicode => isWindowUnicode;

        /// <summary>
        /// Whether or not the found window is unicode, via text.
        /// </summary>
        [Browsable(false)]
        public string WindowCharset => windowCharset;

        /// <summary>
        /// 窗口所在的进程ID
        /// </summary>
        [Browsable(false)]
        public int WindowPID => windowPID;

        #endregion

        #region Event Handler Methods

        /// <summary>
        /// Handles the Load event of the WindowFinder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WindowFinder_Load(object sender, System.EventArgs e)
        {
            this.Size = picTarget.Size;
            try
            {
                using (MemoryStream ms = new MemoryStream(Resources.curFindTarget))
                    cursorTarget = new Cursor(ms);
                bitmapFind = Resources.bmpFind;
                bitmapFinda = Resources.bmpFinda;
            }
            catch (Exception x)
            {
                // Show error
                MessageBox.Show(this, "Failed to load resources.\n\n" + x.ToString(), "WindowFinder");

                // Attempt to use backup cursor
                if (cursorTarget == null)
                    cursorTarget = Cursors.Cross;
            }


            // Set default values
            picTarget.Image = bitmapFind;
        }

        /// <summary>
        /// Handles the MouseDown event of the picTarget control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void picTarget_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Set capture image and cursor
            picTarget.Image = bitmapFinda;
            picTarget.Cursor = cursorTarget;

            // Set capture
            Win32.SetCapture(picTarget.Handle);

            // Begin targeting
            isTargeting = true;
            targetWindow = IntPtr.Zero;

            // Show info   TODO: Put into function for mousemove & mousedown
            SetWindowHandle(picTarget.Handle);
        }

        /// <summary>
        /// Handles the MouseMove event of the picTarget control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void picTarget_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Make sure targeting before highlighting windows
            if (!isTargeting)
                return;

            // Get screen coords from client coords and window handle
            IntPtr hWnd = Win32.WindowFromPoint(picTarget.Handle, Control.MousePosition.X, Control.MousePosition.Y);

            // Check for valid highlight
            if (targetWindow == hWnd)
                return;

            // Show info
            SetWindowHandle(hWnd);

            targetWindow = hWnd;
            _timer.Enabled = true;
        }

        /// <summary>
        /// Handles the MouseUp event of the picTarget control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void picTarget_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            IntPtr hWnd;

            // End targeting
            isTargeting = false;

            // Unhighlight window
            if (overlay != null)
            { 
                overlay.SetTarget(targetWindow);
                overlay.DelayDestry(300);
                overlay = null;
            }
            targetWindow = IntPtr.Zero;

            // Reset capture image and cursor
            picTarget.Cursor = Cursors.Default;
            picTarget.Image = bitmapFind;

            // Get screen coords from client coords and window handle
            hWnd = Win32.WindowFromPoint(picTarget.Handle, Control.MousePosition.X, Control.MousePosition.Y);

            if (targetWindow != hWnd)
                SetWindowHandle(hWnd);

            // Release capture
            Win32.SetCapture(IntPtr.Zero);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (targetWindow != IntPtr.Zero)
            {
                // 创建并显示高亮覆盖窗口
                if (overlay == null)
                {
                    overlay = new HighlightOverlayForm();
                    overlay.SetTarget(targetWindow);
                    overlay.Show();
                }
                else
                {
                    overlay.SetTarget(targetWindow);
                }
            }
            else
            {
                _timer.Enabled = false;
            }
        }
        protected override void OnLeave(EventArgs e)
        {
            if (_timer != null)
                _timer.Dispose();
            base.OnLeave(e);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the window handle if handle is a valid window.
        /// </summary>
        /// <param name="handle">The handle to set to.</param>
        public void SetWindowHandle(IntPtr handle)
        {
            if (Win32.IsWindow(handle) == 0)
            {
                // Clear window information
                windowHandle = IntPtr.Zero;
                windowHandleText = string.Empty;
                windowClass = string.Empty;
                windowText = string.Empty;
                isWindowUnicode = false;
                windowCharset = string.Empty;
                windowPID = 0;
            }
            else
            {
                // Set window information
                windowHandle = handle;
                windowHandleText = Convert.ToString(handle.ToInt32(), 16).ToUpper().PadLeft(8, '0');
                windowClass = Win32.GetClassName(handle);
                windowText = Win32.GetWindowText(handle);
                isWindowUnicode = Win32.IsWindowUnicode(handle) != 0;
                windowCharset = ((isWindowUnicode) ? ("Unicode") : ("Ansi"));
                windowPID = Win32.GetWindowPID(handle);
            }
            if (WindowHandleChanged != null)
                WindowHandleChanged(this, EventArgs.Empty);
        }
        #endregion

        private bool isTargeting = false;
        private Cursor cursorTarget = null;
        private Bitmap bitmapFind = null;
        private Bitmap bitmapFinda = null;
        private IntPtr targetWindow = IntPtr.Zero;
        private IntPtr windowHandle = IntPtr.Zero;
        private string windowHandleText = string.Empty;
        private string windowClass = string.Empty;
        private string windowText = string.Empty;
        private bool isWindowUnicode = false;
        private string windowCharset = string.Empty;
        private int windowPID = 0;
    }
}
