
namespace OPTestTool
{
    public partial class WriteIntForm : Form
    {
        public WriteIntForm()
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
        public int Length => int.Parse(Txt_Number.Text);

        private void LabelInt_MouseDown(object sender, MouseEventArgs e) => Utils.LabelInt_MouseDown(sender, e);
        private void LabelInt_MouseMove(object sender, MouseEventArgs e) => Utils.LabelInt_MouseMove(sender, e);
        private void LabelInt_MouseUp(object sender, MouseEventArgs e) => Utils.LabelInt_MouseUp(sender, e);
        private void TextBoxInt_TextChanged(object sender, EventArgs e) => Utils.TextBoxInt_TextChanged(sender, e);
        private void TextBoxInt_KeyPress(object sender, KeyPressEventArgs e) => Utils.TextBoxInt_KeyPress(sender, e);

    }
}
