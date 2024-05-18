namespace OPTestTool
{
    public partial class ChangeDisplayForm : Form
    {
        public ChangeDisplayForm()
        {
            InitializeComponent();
        }
        public void SetSelect(string selectMode)
        {
            if (string.IsNullOrEmpty(selectMode))
                return;
            foreach (var item in groupBox1.Controls)
            {
                var btn = item as RadioButton;
                if (btn == null)
                    continue;
                if (btn.Text != selectMode)
                    continue;
                btn.Checked = true;
                break;
            }
        }
        public string GetSelect()
        {
            foreach (var item in groupBox1.Controls)
            {
                var btn = item as RadioButton;
                if (btn == null)
                    continue;
                if (btn.Checked)
                    return btn.Text;
            }
            return string.Empty;
        }
    }
}
