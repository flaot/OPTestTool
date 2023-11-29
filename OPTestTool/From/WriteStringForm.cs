
namespace OPTestTool
{
    public partial class WriteStringForm : Form
    {
        public WriteStringForm()
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
        public string Context => Txt_Value.Text;

    }
}
