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
    public partial class FrmMemoryAsmVirtualProtectEx : Form
    {
        public FrmMemoryAsmVirtualProtectEx()
        {
            InitializeComponent();
            this.txtAddr.Text = StaticData.MemoryAsm.VirtualProtectExAddr.ToString();
            this.txtSize.Text = StaticData.MemoryAsm.VirtualProtectExSize.ToString();
            this.txtOldProtect.Text = StaticData.MemoryAsm.VirtualProtectExOldProtect.ToString();
            this.cbSelect.Items.Add("可读写执行");
            this.cbSelect.Items.Add("protect指定读写");
            this.cbSelect.SelectedIndex = StaticData.MemoryAsm.VirtualProtectExType;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.MemoryAsm.VirtualProtectExAddr = this.txtAddr.Text.Trim().ToInt32();
            StaticData.MemoryAsm.VirtualProtectExSize = this.txtSize.Text.Trim().ToInt32();
            StaticData.MemoryAsm.VirtualProtectExOldProtect = this.txtOldProtect.Text.Trim().ToInt32();
            StaticData.MemoryAsm.VirtualProtectExType = this.cbSelect.SelectedIndex;
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