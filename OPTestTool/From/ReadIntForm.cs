
namespace OPTestTool
{
    public partial class ReadIntForm : Form
    {
        public ReadIntForm()
        {
            InitializeComponent();
        }
        public string GetSelect()
        {
            foreach (var item in Controls)
            {
                var btn = item as RadioButton;
                if (btn == null)
                    continue;
                if (btn.Checked)
                    return btn.Tag as string;
            }
            return string.Empty;
        }
    }
}
