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
    public partial class FrmMemoryAsmWriteDouble : Form
    {
        public FrmMemoryAsmWriteDouble()
        {
            InitializeComponent();
            this.txtWriteContent.Text = StaticData.MemoryAsm.WriteDoubleV.ToString();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.MemoryAsm.WriteDoubleV = this.txtWriteContent.Text.Trim().ToInt32();
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