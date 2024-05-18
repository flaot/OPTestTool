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
    public partial class FrmMemoryAsmWriteString : Form
    {
        public FrmMemoryAsmWriteString()
        {
            InitializeComponent();
            this.txtWriteContent.Text = StaticData.MemoryAsm.WriteStringV;

            this.cbSelect.Items.Add("Ascii字符串");
            this.cbSelect.Items.Add("Unicode字符串");
            this.cbSelect.Items.Add("UTF8字符串");
            this.cbSelect.SelectedIndex = StaticData.MemoryAsm.WriteStringType;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.MemoryAsm.WriteStringType = this.cbSelect.SelectedIndex;
            StaticData.MemoryAsm.WriteStringV = this.txtWriteContent.Text;
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
