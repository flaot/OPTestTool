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
    public partial class FrmSettingDmGuard : Form
    {
        public FrmSettingDmGuard()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("停止");
            this.cbSelect.Items.Add("启动");
            this.cbSelect.SelectedIndex = 0;

            this.cbType.Items.Add("np");
            this.cbType.Items.Add("memory");
            this.cbType.Items.Add("memory2");
            this.cbType.Items.Add("memory3");
            this.cbType.Items.Add("memory4");
            this.cbType.Items.Add("display2");
            this.cbType.Items.Add("block");
            this.cbType.Items.Add("f1");
            this.cbType.Items.Add("b2");
            this.cbType.Items.Add("b3");
            this.cbType.Items.Add("自定义");
            this.cbType.SelectedIndex = 0;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.Set.DmGuardEnable = this.cbSelect.SelectedIndex;
            StaticData.Set.DmGuardType = this.cbType.Text;
            if ((cbSelect.Text == "f1") | (cbSelect.Text == "b2") | (cbSelect.Text == "b3"))
            {
                StaticData.Set.DmGuardType = this.cbType.Text+" "+this.txtPID.Text.Trim();
            }
            else if (cbSelect.Text == "自定义")
            {
                StaticData.Set.DmGuardType = this.txtType.Text.Trim();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblPID.Visible = false;
            this.txtPID.Visible = false;
            this.txtType.Visible = false;
            if ((cbType.Text == "f1")|(cbType.Text=="b2")|(cbType.Text=="b3"))
            {
                this.lblPID.Visible = true;
                this.txtPID.Visible = true;
            }
            else if (this.cbType.Text == "自定义")
            {
                this.txtType.Visible = true;
            }
        }
    }
}
