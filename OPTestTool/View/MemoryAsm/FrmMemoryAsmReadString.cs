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

namespace ScriptTestTools.View.MemoryAsm
{
    public partial class FrmMemoryAsmReadString : Form
    {
        public FrmMemoryAsmReadString()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("GBK字符串");
            this.cbSelect.Items.Add("Unicode字符串");
            this.cbSelect.Items.Add("UTF8字符串");

            this.cbSelect.SelectedIndex = StaticData.MemoryAsm.ReadStringType;
            this.txtLen.Text = StaticData.MemoryAsm.ReadStringLen.ToString();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.MemoryAsm.ReadStringLen = this.txtLen.Text.Trim().ToInt32();
            StaticData.MemoryAsm.ReadStringType = this.cbSelect.SelectedIndex;
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