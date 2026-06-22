namespace WordDictTool
{
    /// <summary>
    /// 全屏截图/取色窗体。支持框选区域、调整选框、双击或 Enter/Space 确认截图。
    /// </summary>
    public partial class ScreenshotForm : Form
    {
        private Bitmap background;       // 屏幕截图背景
        private Bitmap result;           // 框选确认后的截图结果
        private Color resultColor;       // 取色模式下的选中颜色
        private bool getColor;           // true=取色模式，false=截图模式
        private Point startPoint;        // 框选起始点
        private Point endPoint;          // 框选当前终点
        private bool isDrawing = false;  // 是否正在绘制新选框
        private bool isResizing = false; // 是否正在调整选框大小或移动
        private int resizeHandleIndex = -1; // 当前操作的手柄索引，-2 表示移动整个选框
        private Rectangle rect;          // 当前选框区域
        private Pen pen = new Pen(Color.Blue, 2);
        private SolidBrush handleBrush = new SolidBrush(Color.Red);
        private SolidBrush highlightBrush = new SolidBrush(Color.Orange);
        private const int HandleSize = 8; // 调整手柄边长
        private Rectangle[] handles = new Rectangle[HandleSize];
        private Point lastMousePoint;
        private int hoveredHandleIndex = -1;      // 当前悬停的手柄索引
        private bool isHoveringOverRectangle = false; // 鼠标是否悬停在选框内
        private const int MouseMoveStep = 1;      // 方向键移动鼠标步长
        private const int MouseShiftMoveStep = 10;  // Shift+方向键移动步长
        private const int SizeLabelMargin = 4;      // 尺寸标签与选框的间距
        private Point _startPoint; // 窗体左上角对应的屏幕坐标，用于坐标换算
        public ScreenshotForm()
        {
            InitializeComponent();
        }

        private void ScreenshotForm_Load(object sender, EventArgs e)
        {
            sizeLabel.Visible = false;
            // 覆盖全部虚拟屏幕（Debug 下仅主屏，便于调试）
            Rectangle bounds = SystemInformation.VirtualScreen; //所有显示器
#if DEBUG
            bounds = Screen.PrimaryScreen.Bounds; //主显示器
#endif
            Size = bounds.Size;
            Location = bounds.Location;

            background = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(background))
            {
                g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            }
            _startPoint = new Point(bounds.X, bounds.Y);
            magnifier.SetBackground(_startPoint, background);
        }
        private void ScreenshotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pen?.Dispose();
            handleBrush?.Dispose();
            highlightBrush?.Dispose();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(background, new Point(0, 0));

            //绘制框选区
            if (!rect.IsEmpty)
            {
                e.Graphics.DrawRectangle(pen, rect);
                DrawHandles(e.Graphics);
            }
        }

        private void ScreenshotForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 双击选框内部确认截图
            if (!rect.IsEmpty && rect.Contains(e.Location))
                ConfirmSelection();
        }

        /// <summary>
        /// 根据当前选框裁剪背景图并关闭窗体。
        /// </summary>
        private void ConfirmSelection()
        {
            if (rect.IsEmpty) return;

            result = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(background, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }
            DialogResult = DialogResult.OK;
            Close();
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
            // Enter/Space 确认当前选框截图
            if (!rect.IsEmpty && (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space))
            {
                ConfirmSelection();
                return;
            }
            //按C键复制颜色值到剪贴板
            if (e.KeyCode == Keys.C)
            {
                Point mousePos = Cursor.Position;
                Point localMousePos = new Point(mousePos.X - _startPoint.X, mousePos.Y - _startPoint.Y);
                Color color = background.GetPixel(localMousePos.X, localMousePos.Y);
                Clipboard.SetText($"{color.R:X2}{color.G:X2}{color.B:X2}");
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
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
                return;
            if (getColor)
            {
                // 取色模式：左键点击直接取色并关闭
                if (e.Button == MouseButtons.Left)
                {
                    resultColor = background.GetPixel(e.Location.X, e.Location.Y);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
            }
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

        /// <summary>
        /// 根据当前选框更新 8 个调整手柄的位置。
        /// </summary>
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
        /// <summary>
        /// 判断点是否落在某个调整手柄上。
        /// </summary>
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
        /// <summary>
        /// 更新选框尺寸标签的位置。标签始终放在选框外侧，避免遮挡小选区导致无法双击确认。
        /// </summary>
        private void UpdateSizeLabel()
        {
            if (rect.IsEmpty) return;

            sizeLabel.Text = $"{rect.Width} x {rect.Height}";
            Size labelSize = sizeLabel.PreferredSize;
            int x = rect.Left;
            // 优先放在选框上方
            int y = rect.Top - labelSize.Height - SizeLabelMargin;

            // 上方空间不足时改放下方
            if (y < 0)
                y = rect.Bottom + SizeLabelMargin;

            if (y + labelSize.Height > ClientSize.Height)
                y = Math.Max(0, rect.Top - labelSize.Height - SizeLabelMargin);

            // 横向溢出时依次尝试右侧、左侧
            if (x + labelSize.Width > ClientSize.Width)
                x = rect.Right + SizeLabelMargin;

            if (x + labelSize.Width > ClientSize.Width)
                x = Math.Max(0, rect.Left - labelSize.Width - SizeLabelMargin);

            // 限制在窗体可见范围内
            x = Math.Clamp(x, 0, Math.Max(0, ClientSize.Width - labelSize.Width));
            y = Math.Clamp(y, 0, Math.Max(0, ClientSize.Height - labelSize.Height));
            sizeLabel.Location = new Point(x, y);
            sizeLabel.Visible = true;
        }

        /// <summary>
        /// 打开截图对话框，返回框选区域的位图；取消时返回 null。
        /// </summary>
        public static Bitmap ShowDialogGetBitmap()
        {
            using (ScreenshotForm form = new ScreenshotForm())
            {
                form.getColor = false;
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.Cancel)
                    return null;
                else
                    return form.result;
            }
        }
        /// <summary>
        /// 打开取色对话框，点击左键取色；取消时返回 Color.Empty。
        /// </summary>
        public static Color ShowPanelGetColor()
        {
            using (ScreenshotForm form = new ScreenshotForm())
            {
                form.getColor = true;
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.Cancel)
                    return Color.Empty;
                else
                    return form.resultColor;
            }
        }
    }
}