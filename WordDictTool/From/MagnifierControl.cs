namespace WordDictTool
{
    public partial class MagnifierControl : UserControl
    {
        private Bitmap _background;
        private Point _startPoint;
        private Size captureSize; // 捕获区域大小
        private int _magnification = 8;
        private Size _lineSize = new Size(1, 1); //辅助线大小
        /// <summary>
        /// 放大倍数
        /// </summary>
        public int Magnification
        {
            get => _magnification;
            set
            {
                _magnification = value;
                captureSize = pictureBox.Size / _magnification;
            }
        }

        public MagnifierControl()
        {
            InitializeComponent();
            pictureBox.Paint += PictureBox_Paint;
            captureSize = pictureBox.Size / _magnification;
        }
        public void SetBackground(Point startPoint, Bitmap background)
        {
            _startPoint = startPoint;
            _background = background;
            timer1.Start();
        }
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            // 确保在图像上绘制
            if (pictureBox.Image == null)
                return;

            int centerX = pictureBox.Width / 2 + _magnification / 2;
            int centerY = pictureBox.Height / 2 + _magnification / 2;

            // 使用抗锯齿使线条更平滑
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 绘制十字线
            using (Pen crossPen = new Pen(Color.Red))
            {
                // 垂直红线
                crossPen.Width = _lineSize.Width;
                e.Graphics.DrawLine(crossPen, centerX, 0, centerX, pictureBox.Height);
                // 水平红线
                crossPen.Width = _lineSize.Height;
                e.Graphics.DrawLine(crossPen, 0, centerY, pictureBox.Width, centerY);
            }

            // 添加中心标记
            using (Brush centerBrush = new SolidBrush(Color.Yellow))
            {
                e.Graphics.FillEllipse(centerBrush, centerX - 2, centerY - 2, 4, 4);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 获取鼠标位置
            Point mousePos = Cursor.Position;

            // 鼠标在画布上的位置
            Point localMousePos = new Point(mousePos.X - _startPoint.X, mousePos.Y - _startPoint.Y);
            if (localMousePos.X < 0 || localMousePos.Y < 0 || localMousePos.X > _background.Width || localMousePos.Y > _background.Height)
                return;

            // 更新窗体位置（跟随鼠标）
            UpdateMagnifierPosition();

            // 捕获屏幕区域
            Rectangle captureRect = new Rectangle(
                localMousePos.X - captureSize.Width / 2,
                localMousePos.Y - captureSize.Height / 2,
                captureSize.Width,
                captureSize.Height
            );
            captureRect.X = Math.Clamp(captureRect.X, 0, _background.Width);
            captureRect.Y = Math.Clamp(captureRect.Y, 0, _background.Height);
            captureRect.Width = Math.Clamp(captureRect.Width + captureRect.X, 1, _background.Width) - captureRect.X;
            captureRect.Height = Math.Clamp(captureRect.Height + captureRect.Y, 1, _background.Height) - captureRect.Y;

            // 捕获屏幕图像
            using (Bitmap screenBmp = _background.Clone(captureRect, _background.PixelFormat))
            {
                // 创建放大后的图像
                Bitmap magnifiedBmp = FuncHelper.ResizeImageProportionally(screenBmp, _magnification);

                // 显示放大图像
                if (pictureBox.Image != null)
                    pictureBox.Image.Dispose();
                pictureBox.Image = magnifiedBmp;

                // 获取中心像素颜色
                Color centerColor = _background.GetPixel(localMousePos.X, localMousePos.Y);

                // 更新文本显示
                coordinateLabel.Text = $"坐标: ({mousePos.X}, {mousePos.Y})\n" +
                                       $"色值: {FuncHelper.ColorToHex(centerColor)}\n" +
                                       "按C复制色号";
            }
        }

        private void UpdateMagnifierPosition()
        {
            Point mousePos = Cursor.Position;
            Point newLocation = new Point(mousePos.X + 10, mousePos.Y + 10); // 默认在鼠标右下角

            // 获取鼠标所在的屏幕
            Rectangle screenBounds = Screen.GetWorkingArea(mousePos);

            // 检查是否超出右边界
            if (newLocation.X + Width > screenBounds.Right)
            {
                // 切换到鼠标左侧
                newLocation.X = mousePos.X - Width - 10;
            }

            // 检查是否超出下边界
            if (newLocation.Y + Height > screenBounds.Bottom)
            {
                // 切换到鼠标上方
                newLocation.Y = mousePos.Y - Height - 10;
            }

            // 确保不会超出左边界和上边界
            if (newLocation.X < screenBounds.Left)
            {
                newLocation.X = screenBounds.Left;
            }

            if (newLocation.Y < screenBounds.Top)
            {
                newLocation.Y = screenBounds.Top;
            }
            this.Location = Parent.PointToClient(newLocation);
        }
    }
}
