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
    public partial class FrmSettingLockInput : Form
    {
        public FrmSettingLockInput()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("解除锁定 0");
            this.cbSelect.Items.Add("锁定键鼠 1");
            this.cbSelect.Items.Add("只锁定鼠标 2");
            this.cbSelect.Items.Add("只锁定键盘 3");
            this.cbSelect.Items.Add("锁定键盘 4(同时缩短一些特殊输入,比如回车等)");
            this.cbSelect.Items.Add("只锁定键盘 5(同时缩短一些特殊输入,比如回车等)");
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.Set.LockInput = this.cbSelect.SelectedIndex;
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
