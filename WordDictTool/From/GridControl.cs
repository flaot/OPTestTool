namespace WordDictTool
{
    public partial class GridControl : UserControl
    {
        private const int PIXEL_W = 7;
        const int offset = 0;
        const int offsetx = 0;
        const int max_height = 255;

        WordData data = null;

        Point p = new Point(2, 2);
        public GridControl()
        {
            InitializeComponent();
            this.Size = new Size(PIXEL_W * 255, max_height * 255);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen pen = new Pen(Color.Gray))
            {
                for (int i = 0; i <= max_height; ++i)
                {
                    e.Graphics.DrawLine(pen, p.X + offsetx, p.Y + i * PIXEL_W + offset,
                        p.X + PIXEL_W * max_height + offsetx, p.Y + i * PIXEL_W + offset);
                }
                for (int j = 0; j <= max_height; ++j)
                {
                    e.Graphics.DrawLine(pen, p.X + j * PIXEL_W + offsetx, p.Y + offset,
                        p.X + j * PIXEL_W + offsetx, p.Y + max_height * PIXEL_W + offset);
                }
            }

            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
            {
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                {
                    if (data != null && data.w > 0)
                    {
                        int rows = data.h;
                        int cols = data.w;
                        int idx = 0;
                        for (int j = 0; j < cols; ++j)
                        {
                            for (int i = 0; i < rows; ++i)
                            {
                                SolidBrush brush;
                                if (((data.data[idx / 8] >> (idx & 7)) & 1u) != 0)
                                    brush = blackBrush;
                                else
                                    brush = whiteBrush;
                                idx++;
                                e.Graphics.FillRectangle(brush, p.X + j * PIXEL_W + 1 + offsetx, p.Y + i * PIXEL_W + 1 + offset,
                                    PIXEL_W - 1, PIXEL_W - 1);
                            }
                        }
                    }
                }
            }
           
        }

        public void RefreshData(string wordCode)
        {
            data = new WordData(wordCode, false);
            this.Refresh();
        }
    }
}
