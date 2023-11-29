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
            opSoft.GetScreenDataBmp(x1, y1, x2, y2, out pBmp, out bmpSize);
            if (pBmp == IntPtr.Zero)
                return;
            Byte[] tbuf = new Byte[bmpSize];
            unsafe
            {
                byte* memBytePtr = (byte*)pBmp.ToPointer();
                using (UnmanagedMemoryStream ms = new UnmanagedMemoryStream(memBytePtr, (long)bmpSize, (long)bmpSize, FileAccess.Read))
                {
                    ms.Read(tbuf, 0, tbuf.Length);
                }
            }
            Image iamge = null;
            try
            {
                using (MemoryStream ms = new MemoryStream(tbuf))
                {
                    iamge = Image.FromStream(ms);
                }
            }
            catch (Exception)
            {
            }
            pictureBox1.Image = iamge;
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
