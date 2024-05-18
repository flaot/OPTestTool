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
    public partial class FrmSettingDownCpu : Form
    {
        public FrmSettingDownCpu()
        {
            InitializeComponent();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            string values = this.txtValue.Text.Trim();
            StaticData.Set.DownCpuType = this.cbType.SelectedIndex;
            StaticData.Set.DownCpuRate = int.Parse(values);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmSettingDownCpu_Load(object sender, EventArgs e)
        {
            this.cbType.Items.Add("0");
            this.cbType.Items.Add("1");
            this.cbType.SelectedIndex = 0;
        }
    }
}
