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
    public partial class FrmMemoryAsmReadInt : Form
    {
        public FrmMemoryAsmReadInt()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("32位有符号");
            this.cbSelect.Items.Add("16位有符号");
            this.cbSelect.Items.Add("8位有符号");
            this.cbSelect.Items.Add("64位");
            this.cbSelect.Items.Add("32位无符号");
            this.cbSelect.Items.Add("16位无符号");
            this.cbSelect.Items.Add("8位无符号");
            this.cbSelect.SelectedIndex = StaticData.MemoryAsm.ReadIntType;

        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.MemoryAsm.ReadIntType = this.cbSelect.SelectedIndex;
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
