namespace WordDictTool
{
    public partial class ScreenshotForm : Form
    {
        private Bitmap background;
        private Bitmap result;
        private Point startPoint;
        private Point endPoint;
        private bool isDrawing = false;
        private bool isResizing = false;
        private int resizeHandleIndex = -1;
        private Rectangle rect;
        private Pen pen = new Pen(Color.Blue, 2);
        private SolidBrush handleBrush = new SolidBrush(Color.Red);
        private SolidBrush highlightBrush = new SolidBrush(Color.Orange);
        private const int HandleSize = 8;
        private Rectangle[] handles = new Rectangle[HandleSize];
        private Point lastMousePoint;
        private int hoveredHandleIndex = -1;
        private bool isHoveringOverRectangle = false;
        private const int MouseMoveStep = 1; // 鼠标移动步长
        private const int MouseShiftMoveStep = 10;
        public ScreenshotForm()
        {
            InitializeComponent();
        }

        private void ScreenshotForm_Load(object sender, EventArgs e)
        {
            magnifier.Visible = false;
            sizeLabel.Visible = false;
            Rectangle bounds = Screen.PrimaryScreen.Bounds; //主显示器
            //Rectangle bounds = SystemInformation.VirtualScreen; //所有显示器
            Size = bounds.Size;
            Location = bounds.Location;

            background = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(background))
            {
                g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            }
            BackgroundImage = background;
        }
        private void ScreenshotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pen?.Dispose();
            handleBrush?.Dispose();
            highlightBrush?.Dispose();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //绘制框选区
            if (!rect.IsEmpty)
            {
                e.Graphics.DrawRectangle(pen, rect);
                DrawHandles(e.Graphics);
            }
        }

        private void ScreenshotForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 双击选框关闭窗口
            if (!rect.IsEmpty && rect.Contains(e.Location))
            {
                result = new Bitmap(rect.Width, rect.Height);
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.DrawImage(background, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        private void ScreenshotForm_KeyDown(object sender, KeyEventArgs e)
        {
            // 按ESC键关闭窗口
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            //移动鼠标位置快捷键
            int moveStep = MouseMoveStep;
            if (e.Shift)
                moveStep = MouseShiftMoveStep;
            if (e.KeyCode == Keys.Left)
            {
                Cursor.Position = new Point(Cursor.Position.X - moveStep, Cursor.Position.Y);
            }
            else if (e.KeyCode == Keys.Right)
            {
                Cursor.Position = new Point(Cursor.Position.X + moveStep, Cursor.Position.Y);
            }
            else if (e.KeyCode == Keys.Up)
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - moveStep);
            }
            else if (e.KeyCode == Keys.Down)
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + moveStep);
            }
            // 小键盘方向键控制鼠标位置
            else if ((e.KeyCode >= Keys.NumPad1 && e.KeyCode <= Keys.NumPad9))
            {
                // 只处理小键盘方向键
                if (e.KeyCode == Keys.NumPad4) // 左
                {
                    Cursor.Position = new Point(Cursor.Position.X - moveStep, Cursor.Position.Y);
                }
                else if (e.KeyCode == Keys.NumPad6) // 右
                {
                    Cursor.Position = new Point(Cursor.Position.X + moveStep, Cursor.Position.Y);
                }
                else if (e.KeyCode == Keys.NumPad8) // 上
                {
                    Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - moveStep);
                }
                else if (e.KeyCode == Keys.NumPad2) // 下
                {
                    Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + moveStep);
                }
                else if (e.KeyCode == Keys.NumPad7) // 左上
                {
                    Cursor.Position = new Point(Cursor.Position.X - moveStep, Cursor.Position.Y - moveStep);
                }
                else if (e.KeyCode == Keys.NumPad9) // 右上
                {
                    Cursor.Position = new Point(Cursor.Position.X + moveStep, Cursor.Position.Y - moveStep);
                }
                else if (e.KeyCode == Keys.NumPad1) // 左下
                {
                    Cursor.Position = new Point(Cursor.Position.X - moveStep, Cursor.Position.Y + moveStep);
                }
                else if (e.KeyCode == Keys.NumPad3) // 右下
                {
                    Cursor.Position = new Point(Cursor.Position.X + moveStep, Cursor.Position.Y + moveStep);
                }
            }
        }
        private void ScreenshotForm_MouseDown(object sender, MouseEventArgs e)
        {
            lastMousePoint = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                // 检查是否点击了控制手柄
                if (IsInHandle(e.Location, out int handleIndex))
                {
                    isResizing = true;
                    resizeHandleIndex = handleIndex;
                    SetCursorForHandle(handleIndex);
                    return;
                }

                // 检查是否点击了矩形内部（移动矩形）
                if (!rect.IsEmpty && rect.Contains(e.Location))
                {
                    isResizing = true;
                    resizeHandleIndex = -2; // 特殊值表示移动整个矩形
                    this.Cursor = Cursors.SizeAll;
                    return;
                }

                // 开始绘制新矩形
                isDrawing = true;
                startPoint = e.Location;
                rect = new Rectangle(startPoint, new Size(0, 0));
            }
            else if (e.Button == MouseButtons.Right)
            {
                // 右键取消选框
                rect = Rectangle.Empty;
                handles = new Rectangle[8];
                this.Cursor = Cursors.Default;
                hoveredHandleIndex = -1;
                isHoveringOverRectangle = false;
                sizeLabel.Visible = false;
                this.Invalidate();
            }
        }
        private void ScreenshotForm_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentPoint = e.Location;
            Point delta = new Point(currentPoint.X - lastMousePoint.X, currentPoint.Y - lastMousePoint.Y);
            lastMousePoint = currentPoint;

            if (isResizing)
            {
                if (resizeHandleIndex >= 0) // 调整大小
                {
                    ResizeRectangle(resizeHandleIndex, currentPoint);
                }
                else if (resizeHandleIndex == -2) // 移动矩形
                {
                    rect.Location = new Point(rect.Location.X + delta.X, rect.Location.Y + delta.Y);
                }
                UpdateHandles();
                UpdateSizeLabel();
                this.Invalidate();
            }
            else if (isDrawing)
            {
                endPoint = e.Location;
                rect = new Rectangle(
                    Math.Min(startPoint.X, endPoint.X),
                    Math.Min(startPoint.Y, endPoint.Y),
                    Math.Abs(endPoint.X - startPoint.X),
                    Math.Abs(endPoint.Y - startPoint.Y)
                );
                UpdateHandles();
                UpdateSizeLabel();
                this.Invalidate();
            }
            else if (!isResizing && !isDrawing)
            {
                // 非操作状态下检测手柄悬停
                int previousHovered = hoveredHandleIndex;
                bool previousHoveringRect = isHoveringOverRectangle;

                IsInHandle(e.Location, out hoveredHandleIndex);
                isHoveringOverRectangle = !rect.IsEmpty && rect.Contains(e.Location);

                // 更新尺寸标签显示状态
                if (!rect.IsEmpty)
                {
                    if (hoveredHandleIndex >= 0 || isHoveringOverRectangle)
                    {
                        UpdateSizeLabel();
                        sizeLabel.Visible = true;
                    }
                    else
                    {
                        sizeLabel.Visible = false;
                    }
                }

                // 如果悬停的手柄发生变化，更新光标
                if (previousHovered != hoveredHandleIndex)
                {
                    if (hoveredHandleIndex >= 0)
                    {
                        SetCursorForHandle(hoveredHandleIndex);
                    }
                    else if (isHoveringOverRectangle)
                    {
                        this.Cursor = Cursors.SizeAll;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                    this.Invalidate(); // 重绘以显示悬停高亮效果
                }
                // 如果手柄未悬停但矩形悬停状态变化
                else if (hoveredHandleIndex < 0 && previousHoveringRect != isHoveringOverRectangle)
                {
                    if (isHoveringOverRectangle)
                    {
                        this.Cursor = Cursors.SizeAll;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        sizeLabel.Visible = false;
                    }
                    this.Invalidate();
                }
            }
            if (magnifier.Visible)
            { 
                magnifier.UpdatePanel();
                this.Refresh();
            }
        }
        private void ScreenshotForm_MouseLeave(object sender, EventArgs e)
        {
            if (!isResizing && !isDrawing)
            {
                this.Cursor = Cursors.Default;
                hoveredHandleIndex = -1;
                isHoveringOverRectangle = false;
                sizeLabel.Visible = false;
                this.Invalidate();
            }
        }
        private void ScreenshotForm_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            isResizing = false;
            resizeHandleIndex = -1;
            // 鼠标释放后重新检测悬停状态
            if (IsInHandle(e.Location, out int handleIndex))
            {
                SetCursorForHandle(handleIndex);
            }
            else if (!rect.IsEmpty && rect.Contains(e.Location))
            {
                this.Cursor = Cursors.SizeAll;
            }
            else
            {
                this.Cursor = Cursors.Default;
                sizeLabel.Visible = false;
            }
        }

        private void UpdateHandles()
        {
            if (rect.IsEmpty) return;

            handles[0] = new Rectangle(rect.Left - HandleSize / 2, rect.Top - HandleSize / 2, HandleSize, HandleSize); // TopLeft
            handles[1] = new Rectangle(rect.Left + rect.Width / 2 - HandleSize / 2, rect.Top - HandleSize / 2, HandleSize, HandleSize); // TopMiddle
            handles[2] = new Rectangle(rect.Right - HandleSize / 2, rect.Top - HandleSize / 2, HandleSize, HandleSize); // TopRight
            handles[3] = new Rectangle(rect.Right - HandleSize / 2, rect.Top + rect.Height / 2 - HandleSize / 2, HandleSize, HandleSize); // RightMiddle
            handles[4] = new Rectangle(rect.Right - HandleSize / 2, rect.Bottom - HandleSize / 2, HandleSize, HandleSize); // BottomRight
            handles[5] = new Rectangle(rect.Left + rect.Width / 2 - HandleSize / 2, rect.Bottom - HandleSize / 2, HandleSize, HandleSize); // BottomMiddle
            handles[6] = new Rectangle(rect.Left - HandleSize / 2, rect.Bottom - HandleSize / 2, HandleSize, HandleSize); // BottomLeft
            handles[7] = new Rectangle(rect.Left - HandleSize / 2, rect.Top + rect.Height / 2 - HandleSize / 2, HandleSize, HandleSize); // LeftMiddle
        }
        private void DrawHandles(Graphics g)
        {
            for (int i = 0; i < handles.Length; i++)
            {
                if (!handles[i].IsEmpty)
                {
                    // 如果是悬停的手柄，使用高亮颜色
                    if (i == hoveredHandleIndex)
                    {
                        g.FillRectangle(highlightBrush, handles[i]);
                    }
                    else
                    {
                        g.FillRectangle(handleBrush, handles[i]);
                    }
                }
            }
        }
        private bool IsInHandle(Point p, out int index)
        {
            for (int i = 0; i < handles.Length; i++)
            {
                if (!handles[i].IsEmpty && handles[i].Contains(p))
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }
        private void SetCursorForHandle(int handleIndex)
        {
            if (handleIndex < 0 || handleIndex >= 8)
            {
                if (!isResizing && !isDrawing)
                    this.Cursor = Cursors.Default;
                return;
            }

            switch (handleIndex)
            {
                case 0: // TopLeft
                    this.Cursor = Cursors.SizeNWSE;
                    break;
                case 1: // TopMiddle
                    this.Cursor = Cursors.SizeNS;
                    break;
                case 2: // TopRight
                    this.Cursor = Cursors.SizeNESW;
                    break;
                case 3: // RightMiddle
                    this.Cursor = Cursors.SizeWE;
                    break;
                case 4: // BottomRight
                    this.Cursor = Cursors.SizeNWSE;
                    break;
                case 5: // BottomMiddle
                    this.Cursor = Cursors.SizeNS;
                    break;
                case 6: // BottomLeft
                    this.Cursor = Cursors.SizeNESW;
                    break;
                case 7: // LeftMiddle
                    this.Cursor = Cursors.SizeWE;
                    break;
            }
        }
        private void ResizeRectangle(int handleIndex, Point currentPoint)
        {
            switch (handleIndex)
            {
                case 0: // TopLeft
                    rect = new Rectangle(
                        currentPoint.X,
                        currentPoint.Y,
                        rect.Right - currentPoint.X,
                        rect.Bottom - currentPoint.Y
                    );
                    break;
                case 1: // TopMiddle
                    rect = new Rectangle(
                        rect.Left,
                        currentPoint.Y,
                        rect.Width,
                        rect.Bottom - currentPoint.Y
                    );
                    break;
                case 2: // TopRight
                    rect = new Rectangle(
                        rect.Left,
                        currentPoint.Y,
                        currentPoint.X - rect.Left,
                        rect.Bottom - currentPoint.Y
                    );
                    break;
                case 3: // RightMiddle
                    rect = new Rectangle(
                        rect.Left,
                        rect.Top,
                        currentPoint.X - rect.Left,
                        rect.Height
                    );
                    break;
                case 4: // BottomRight
                    rect = new Rectangle(
                        rect.Left,
                        rect.Top,
                        currentPoint.X - rect.Left,
                        currentPoint.Y - rect.Top
                    );
                    break;
                case 5: // BottomMiddle
                    rect = new Rectangle(
                        rect.Left,
                        rect.Top,
                        rect.Width,
                        currentPoint.Y - rect.Top
                    );
                    break;
                case 6: // BottomLeft
                    rect = new Rectangle(
                        currentPoint.X,
                        rect.Top,
                        rect.Right - currentPoint.X,
                        currentPoint.Y - rect.Top
                    );
                    break;
                case 7: // LeftMiddle
                    rect = new Rectangle(
                        currentPoint.X,
                        rect.Top,
                        rect.Right - currentPoint.X,
                        rect.Height
                    );
                    break;
            }

            // 确保矩形尺寸为正
            if (rect.Width < 0)
            {
                rect = new Rectangle(rect.Right, rect.Top, -rect.Width, rect.Height);
            }
            if (rect.Height < 0)
            {
                rect = new Rectangle(rect.Left, rect.Bottom, rect.Width, -rect.Height);
            }
        }
        private void UpdateSizeLabel()
        {
            if (!rect.IsEmpty)
            {
                sizeLabel.Text = $"{rect.Width} x {rect.Height}";
                sizeLabel.Location = new Point(rect.Left, rect.Top);
                sizeLabel.Visible = true;
            }
        }

        public static Bitmap ShowPanel()
        {
            using (ScreenshotForm form = new ScreenshotForm())
            {
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.Cancel)
                    return null;
                else
                    return form.result;
            }
        }
    }
}