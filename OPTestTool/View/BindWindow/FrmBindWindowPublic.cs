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
    public partial class FrmBindWindowPublic : Form
    {
        private List<string> checkBoxPublic = new List<string>();

        //private string customProperties = "";
        public FrmBindWindowPublic()
        {
            InitializeComponent();
            FrmControl.ControlCheckedByText(groupBox1, StaticData.BindWindow.Public);
        }

        private void btnAutoSelect_Click(object sender, EventArgs e)
        {
            cbdxp1.Checked = true;
            cbdxp2.Checked = true;
        }

        private void CheckBoxBus(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;

            if (checkBox.Checked)
            {
                checkBoxPublic.Add(checkBox.Text);
            }
            else
            {
                checkBoxPublic.Remove(checkBox.Text);
            }
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            if (this.txtCustomProperties.Text.Length > 0)
                checkBoxPublic.Add(this.txtCustomProperties.Text.Trim());
            StaticData.BindWindow.Public = string.Join("|", checkBoxPublic);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbBefore_CheckedChanged(object sender, EventArgs e)
        {
            StaticData.BindWindow.BeforeBindActive = true;
        }

        private void rbLast_CheckedChanged(object sender, EventArgs e)
        {
            StaticData.BindWindow.BeforeBindActive = false;
        }
    }
}