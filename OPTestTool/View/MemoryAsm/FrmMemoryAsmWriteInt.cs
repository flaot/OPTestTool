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
    public partial class FrmMemoryAsmWriteInt : Form
    {
        public FrmMemoryAsmWriteInt()
        {
            InitializeComponent();
            this.txtWriteContent.Text = StaticData.MemoryAsm.WriteIntV.ToString();
            this.cbSelect.Items.Add("32位");
            this.cbSelect.Items.Add("16位");
            this.cbSelect.Items.Add("8位");
            this.cbSelect.Items.Add("64位");
            this.cbSelect.SelectedIndex = StaticData.MemoryAsm.WriteIntType;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.MemoryAsm.WriteIntType = this.cbSelect.SelectedIndex;
            StaticData.MemoryAsm.WriteIntV = this.txtWriteContent.Text.Trim().ToInt32();
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