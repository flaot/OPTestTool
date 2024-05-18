using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptTestTools.View.TestPicColor
{
    public partial class FrmTestPicColorSetPicPwd : Form
    {
        public FrmTestPicColorSetPicPwd()
        {
            InitializeComponent();

            if (StaticData.PicColor.SetPicPwd.Length > 0)
                this.txtPwd.Text = StaticData.PicColor.SetPicPwd;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.PicColor.SetPicPwd = this.txtPwd.Text.Trim();
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
