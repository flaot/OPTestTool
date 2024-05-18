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

namespace ScriptTestTools.View.TestMouse
{
    public partial class FrmTestMouseEnableRealMouse : Form
    {
        public FrmTestMouseEnableRealMouse()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("0 关闭");
            this.cbSelect.Items.Add("1 开启");
            this.cbSelect.SelectedIndex = StaticData.Mouse.EnableRealMouseEnable;

            this.txtDelay.Text = StaticData.Mouse.EnableRealMouseDelay.ToString();
            this.txtMouseStep.Text = StaticData.Mouse.EnableRealMouseMouseStep.ToString();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.Mouse.EnableRealMouseEnable = this.cbSelect.SelectedIndex;
            StaticData.Mouse.EnableRealMouseDelay = this.txtDelay.Text.ToInt32();
            StaticData.Mouse.EnableRealMouseMouseStep = this.txtMouseStep.Text.ToInt32();

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