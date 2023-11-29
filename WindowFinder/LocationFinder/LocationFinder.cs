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
    /// <summary>
    /// 取鼠标位置控件
    /// </summary>
    [DefaultEvent("MouseChangePos")]
    [ToolboxBitmap(typeof(ResourceLocationFinder), "WindowFinder.Resources.WindowFinder.bmp")]
    [Designer(typeof(LocationFinderDesigner))]
    public sealed partial class LocationFinder : UserControl
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowFinder"/> class.
        /// </summary>
        public LocationFinder()
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
        }

        #region Public Properties

        /// <summary>
        /// 鼠标坐标发生更改
        /// </summary>
        public event EventHandler MouseChangePos;

        /// <summary>
        /// 当前鼠标在屏幕上的位置
        /// </summary>
        [Browsable(false)]
        public Point MousePos => mousePos == null ? Point.Empty : mousePos.Value;

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
                using (MemoryStream ms = new MemoryStream(Resources.curLocationFindTarget))
                    cursorTarget = new Cursor(ms);
                bitmapFind = Resources.bmpLocationFind;
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
            isTargeting = true;
            // Show info   TODO: Put into function for mousemove & mousedown
            SetMousePos();
        }

        /// <summary>
        /// Handles the MouseMove event of the picTarget control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void picTarget_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!isTargeting)
                return;
            SetMousePos();
        }

        /// <summary>
        /// Handles the MouseUp event of the picTarget control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void picTarget_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isTargeting = false;
            // Reset capture image and cursor
            picTarget.Cursor = Cursors.Default;
            picTarget.Image = bitmapFind;

            SetMousePos();
        }
        #endregion

        private void SetMousePos()
        {
            if (mousePos.HasValue && mousePos == Control.MousePosition)
                return;
            mousePos = Control.MousePosition;
            if (MouseChangePos != null)
                MouseChangePos(this, EventArgs.Empty);
        }

        private bool isTargeting = false;
        private Cursor cursorTarget = null;
        private Bitmap bitmapFind = null;
        private Bitmap bitmapFinda = null;
        private Point? mousePos;
    }
}
