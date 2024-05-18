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

namespace ScriptTestTools.View.ContentWrite
{
    public partial class FrmContentWriteSendStringIme2 : Form
    {
        public FrmContentWriteSendStringIme2()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("0 发送");
            this.cbSelect.Items.Add("1 发送");
            this.cbSelect.Items.Add("2 发送");
            this.cbSelect.Items.Add("200 安装输入法");
            this.cbSelect.Items.Add("300 安装输入法");
            switch (StaticData.ContentWrite.SendStringIme2Mode)
            {
                case 0: this.cbSelect.SelectedIndex = 0; break;
                case 1: this.cbSelect.SelectedIndex = 1; break;
                case 2: this.cbSelect.SelectedIndex = 2; break;
                case 200: this.cbSelect.SelectedIndex = 3; break;
                case 300: this.cbSelect.SelectedIndex = 4; break;
            }
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.ContentWrite.SendStringIme2Mode = this.cbSelect.Text.Split(' ')[0].ToInt32();
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