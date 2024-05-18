using OPTestTool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptTestTools.View.BindWindow
{
    public partial class FrmBindWindowDisplay : Form
    {
        private string display = "normal";

        public FrmBindWindowDisplay()
        {
            InitializeComponent();
            FrmControl.ControlCheckedByText(groupBox1, StaticData.BindWindow.Display);
        }

        private void rdbBus_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            display = rb.Text;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.BindWindow.Display = display;
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