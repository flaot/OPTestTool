using OPTestTool.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptTestTools.View.TestKeyboard
{
    public partial class FrmTestKeyboardSetKeypadDelay : Form
    {
        public FrmTestKeyboardSetKeypadDelay()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("normal");
            this.cbSelect.Items.Add("windows");
            this.cbSelect.Items.Add("dx");
            this.cbSelect.SelectedIndex = 0;
            if (StaticData.Keyboard.SetKeypadDelayType.Contains("windows"))
                this.cbSelect.SelectedIndex = 1;
            if (StaticData.Keyboard.SetKeypadDelayType.Contains("dx"))
                this.cbSelect.SelectedIndex = 2;
            this.txtDelay.Text = StaticData.Keyboard.SetKeypadDelayDelay.ToString();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.Keyboard.SetKeypadDelayType = this.cbSelect.Text;
            StaticData.Keyboard.SetKeypadDelayDelay = this.txtDelay.Text.Trim().ToInt32();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}