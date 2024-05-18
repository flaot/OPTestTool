using OPTestTool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptTestTools.View.BindWindow
{
    public partial class FrmBindWindowMouse : Form
    {
        private List<string> rbMouse = new List<string>();
        private List<string> cbMouse = new List<string>();

        public FrmBindWindowMouse()
        {
            InitializeComponent();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            if (rbMouse.Count > 0 & cbMouse.Count == 0)
            {
                StaticData.BindWindow.Mouse = rbMouse[0];
            }

            if (rbMouse.Count == 0 & cbMouse.Count > 0)
            {
                StaticData.BindWindow.Mouse = string.Join("|", cbMouse);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void RadioButtonBus(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb.Checked)
            {
                //添加己方，移除它方
                FrmControl.CheckBoxUnChecked(groupBox1);
                cbMouse.Clear();
                rbMouse.Clear();
                rbMouse.Add(rb.Text);
            }
            else
            {
                cbMouse.Clear();
            }
        }

        private void CheckBoxBus(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb.Checked)
            {
                CheckBoxFalse();
                cbMouse.Add(cb.Text);
            }
            else
            {
                cbMouse.Remove(cb.Text);
            }
        }

        private void btnSelectDxMouse_Click(object sender, EventArgs e)
        {
            //CheckBoxFalse();

            cbBus.Checked = true;
            cb2.Checked = true;
            cb5.Checked = true;
            cb6.Checked = true;
            cb7.Checked = true;
            cb9.Checked = true;
            cb10.Checked = true;
        }

        private void CheckBoxFalse()
        {
            rbMouse.Clear();
            FrmControl.RadioButtonUnchecked(groupBox1);
        }

        private void FrmBindWindowMouse_Load(object sender, EventArgs e)
        {
            FrmControl.ControlCheckedByText(groupBox1, StaticData.BindWindow.Mouse);
            ;
        }
    }
}