namespace OPTestTool
{
    public partial class GetScreenDataBmpForm : Form
    {
        private OpSoft opSoft;
        public bool IsShow;
        private System.IntPtr pBmp = IntPtr.Zero;
        private int bmpSize = 0;
        public GetScreenDataBmpForm(OpSoft opSoft)
        {
            this.opSoft = opSoft;
            InitializeComponent();
        }
        public void Update(int x1, int y1, int x2, int y2)
        {
            if (!IsShow)
                return;
            if (opSoft.IsBind() != 1)
            {
                Txt_Tip.Visible = true;
                return;
            }
            Txt_Tip.Visible = false;
            pictureBox1.Image = opSoft.GetScreenDataBmp(x1, y1, x2, y2);
        }
        public new void Show()
        {
            IsShow = true;
            base.Show();
        }
        public new void Hide()
        {
            IsShow = false;
            base.Hide();
        }
    }
}
