
namespace OPTestTool
{
    public partial class ReadStringForm : Form
    {
        public ReadStringForm()
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
        public int Length => int.Parse(Txt_Length.Text);

        private void LabelUInt_MouseDown(object sender, MouseEventArgs e) => Utils.LabelUInt_MouseDown(sender, e);
        private void LabelUInt_MouseMove(object sender, MouseEventArgs e) => Utils.LabelUInt_MouseMove(sender, e);
        private void LabelUInt_MouseUp(object sender, MouseEventArgs e) => Utils.LabelUInt_MouseUp(sender, e);
        private void TextBoxUInt_TextChanged(object sender, EventArgs e) => Utils.TextBoxUInt_TextChanged(sender, e);
        private void TextBoxUInt_KeyPress(object sender, KeyPressEventArgs e) => Utils.TextBoxUInt_KeyPress(sender, e);

    }
}
