namespace OPTestTool
{
    public partial class SetPathForm : Form
    {
        private string curPath;
        public SetPathForm(string curPath)
        {
            this.curPath = curPath;
            InitializeComponent();
        }
        public string GetSelect()
        {
            return Txt_Path.Text;
        }
        private void SetPathForm_Load(object sender, EventArgs e)
        {
            Txt_Path.Text = curPath;
        }

        private void Txt_Path_DragEnter(object sender, DragEventArgs e) => Utils.TextBoxFileFolder_DragEnter(sender, e);
        private void Txt_Path_DragDrop(object sender, DragEventArgs e) => Utils.TextBoxFileFolder_DragDrop(sender, e);
    }
}
