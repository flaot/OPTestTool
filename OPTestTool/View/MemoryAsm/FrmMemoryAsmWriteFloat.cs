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
    public partial class FrmMemoryAsmWriteFloat : Form
    {
        public FrmMemoryAsmWriteFloat()
        {
            InitializeComponent();
            this.txtWriteContent.Text = StaticData.MemoryAsm.WriteFloatV.ToString();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.MemoryAsm.WriteFloatV = float.Parse(this.txtWriteContent.Text.Trim());
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