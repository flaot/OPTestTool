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
    public partial class FrmMemoryAsmReadData : Form
    {
        public FrmMemoryAsmReadData()
        {
            InitializeComponent();
            this.txtLen.Text = StaticData.MemoryAsm.ReadDataLen.ToString();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.MemoryAsm.ReadDataLen = this.txtLen.Text.Trim().ToInt32();
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