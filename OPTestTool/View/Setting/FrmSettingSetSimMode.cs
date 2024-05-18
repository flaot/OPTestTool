using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptTestTools.View.Setting
{
    public partial class FrmSettingSetSimMode : Form
    {
        public FrmSettingSetSimMode()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("普通模式 0");
            this.cbSelect.Items.Add("硬件模式 1");
            this.cbSelect.Items.Add("硬件模式 2(ps2)");
            this.cbSelect.Items.Add("硬件模式 3");
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            //StaticData.Set.SetSimMode = this.cbSelect.SelectedIndex;
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