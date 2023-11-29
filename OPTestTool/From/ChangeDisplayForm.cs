namespace OPTestTool
{
    public partial class ChangeDisplayForm : Form
    {
        public ChangeDisplayForm()
        {
            InitializeComponent();
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
