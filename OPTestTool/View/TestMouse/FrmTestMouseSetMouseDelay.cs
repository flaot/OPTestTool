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

namespace ScriptTestTools.View.TestMouse
{
    public partial class FrmTestMouseSetMouseDelay : Form
    {
        //
        //FrmTestMouseEnableMouseMsg
        public FrmTestMouseSetMouseDelay()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("normal");
            this.cbSelect.Items.Add("windows");
            this.cbSelect.Items.Add("dx");
            this.cbSelect.SelectedIndex = 0;
            if (StaticData.Mouse.SetMouseDelayType == "windows")
                this.cbSelect.SelectedIndex = 1;
            if (StaticData.Mouse.SetMouseDelayType == "dx")
                this.cbSelect.SelectedIndex = 2;
            this.txtDelay.Text = StaticData.Mouse.SetMouseDelayDelay.ToString();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.Mouse.SetMouseDelayType = this.cbSelect.Text;
            StaticData.Mouse.SetMouseDelayDelay = this.txtDelay.Text.Trim().ToInt32();
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