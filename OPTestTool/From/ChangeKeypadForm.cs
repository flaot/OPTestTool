namespace OPTestTool
{
    public partial class ChangeKeypadForm : Form
    {
        public ChangeKeypadForm()
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
