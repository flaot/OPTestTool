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
    public partial class FrmMemoryAsmGetModuleBaseAddr : Form
    {
        public FrmMemoryAsmGetModuleBaseAddr()
        {
            InitializeComponent();
            this.txtModule.Text = StaticData.MemoryAsm.GetModuleBaseAddrModule;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.MemoryAsm.GetModuleBaseAddrModule = this.txtModule.Text;
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
