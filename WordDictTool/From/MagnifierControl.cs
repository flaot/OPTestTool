namespace WordDictTool
{
    public partial class MagnifierControl : UserControl
    {
        private const int GridSize = 100;
        private readonly int CellSize;
        public MagnifierControl()
        {
            InitializeComponent();
            CellSize = Panel_Image.Width / GridSize;
            UpdateMagnifierPosition();
        }
        private void UpdateMagnifierContent()
        {
            try
            {
                Point mousePos = Cursor.Position;
                Point formPos = this.Location;

                // 创建放大镜图像
                Bitmap magnifierBitmap = new Bitmap(Panel_Image.Width, Panel_Image.Height);
                using (Graphics g = Graphics.FromImage(magnifierBitmap))
                {
                    // 获取鼠标位置周围的像素信息
                    using (Bitmap screenBitmap = new Bitmap(Panel_Image.Width, Panel_Image.Height))
                    {
                        using (Graphics screenGraphics = Graphics.FromImage(screenBitmap))
                        {
                            // 截取屏幕区域
                            screenGraphics.CopyFromScreen(
                                mousePos.X - Panel_Image.Width / 2,
                                mousePos.Y - Panel_Image.Height / 2,
                                0, 0,
                                new Size(Panel_Image.Width, Panel_Image.Height),
                                CopyPixelOperation.SourceCopy
                            );
                        }

                        // 绘制格栅效果
                        g.Clear(Color.White);

                        // 绘制格栅线
                        using (Pen gridPen = new Pen(Color.LightGray, 1))
                        {
                            for (int i = 0; i <= GridSize; i++)
                            {
                                int x = i * CellSize;
                                int y = i * CellSize;
                                g.DrawLine(gridPen, x, 0, x, Panel_Image.Width);
                                g.DrawLine(gridPen, 0, y, Panel_Image.Height, y);
                            }
                        }

                        // 绘制像素格栅
                        for (int y = 0; y < GridSize; y++)
                        {
                            for (int x = 0; x < GridSize; x++)
                            {
                                int screenX = x * CellSize;
                                int screenY = y * CellSize;

                                if (screenX < screenBitmap.Width && screenY < screenBitmap.Height)
                                {
                                    Color pixelColor = screenBitmap.GetPixel(screenX, screenY);
                                    using (SolidBrush brush = new SolidBrush(pixelColor))
                                    {
                                        g.FillRectangle(brush, x * CellSize, y * CellSize, CellSize, CellSize);
                                    }
                                }
                            }
                        }
                    }
                }

                // 更新面板内容
                Panel_Image.BackgroundImage = magnifierBitmap;
            }
            catch (Exception ex)
            {
                // 忽略异常，保持窗口正常运行
            }
        }
        private void UpdateMagnifierPosition()
        {
            Point mousePos = Cursor.Position;
            Point newLocation = new Point(mousePos.X + 10, mousePos.Y + 10); // 默认在鼠标右下角

            // 获取鼠标所在的屏幕
            Screen currentScreen = Screen.FromPoint(mousePos);
            Rectangle screenBounds = currentScreen.WorkingArea;

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

            this.Location = newLocation;
        }

        public void UpdatePanel()
        {
            UpdateMagnifierContent();
            UpdateMagnifierPosition();
        }
    }
}
