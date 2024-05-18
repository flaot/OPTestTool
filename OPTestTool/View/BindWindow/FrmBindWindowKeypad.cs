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
    public partial class FrmBindWindowKeypad : Form
    {
        private string radioValue = "";
        private List<string> checkValue = new List<string>();

        public FrmBindWindowKeypad()
        {
            InitializeComponent();
            FrmControl.ControlCheckedByText(groupBox1, StaticData.BindWindow.Keypad);
        }

        private void RadioButtionBus(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;

            if (radioButton.Checked)
            {
                //添加己方，移除它方
                FrmControl.CheckBoxUnChecked(groupBox1);
                checkValue.Clear();
                radioValue = radioButton.Text;
            }
            else
            {
            }
        }

        private void CheckBoxBus(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.Checked)
            {
                //添加己方，移除它方
                FrmControl.RadioButtonUnchecked(groupBox1);
                radioValue = "";
                checkValue.Add(checkBox.Text);
            }
            else
            {
                checkValue.Remove(checkBox.Text);
            }
        }

        private void btnSelectDxKeypad_Click(object sender, EventArgs e)
        {
            this.cb1.Checked = true;
            this.cb2.Checked = true;
            this.cb3.Checked = true;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            if (radioValue != "" & checkValue.Count == 0)
            {
                StaticData.BindWindow.Keypad = radioValue;
            }

            if (radioValue == "" & checkValue.Count > 0)
            {
                StaticData.BindWindow.Keypad = string.Join("|", checkValue);
            }
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