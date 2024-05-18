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
    public partial class FrmSettingEnableFakeActive : Form
    {
        public FrmSettingEnableFakeActive()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("0 关闭");
            this.cbSelect.Items.Add("1 启动");
            this.cbSelect.SelectedIndex = 0;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.Set.EnableFakeActive = this.cbSelect.SelectedIndex;
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
