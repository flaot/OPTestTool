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
    public partial class FrmMemoryAsmVirtualAllocEx : Form
    {
        public FrmMemoryAsmVirtualAllocEx()
        {
            InitializeComponent();
            this.txtAddr.Text = StaticData.MemoryAsm.VirtualAllocExAddr.ToString();
            this.txtSize.Text = StaticData.MemoryAsm.VirtualAllocExSize.ToString();
            this.cbSelect.Items.Add("可读写执行");
            this.cbSelect.Items.Add("可读执行,不可写");
            this.cbSelect.Items.Add("可读写,不可执行");
            this.cbSelect.SelectedIndex = StaticData.MemoryAsm.VirtualAllocExType;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.MemoryAsm.VirtualAllocExType = this.cbSelect.SelectedIndex;
            StaticData.MemoryAsm.VirtualAllocExSize = this.txtSize.Text.ToInt32();
            StaticData.MemoryAsm.VirtualAllocExAddr = this.txtAddr.ToInt32();
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