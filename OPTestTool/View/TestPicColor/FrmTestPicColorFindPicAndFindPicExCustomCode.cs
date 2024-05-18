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
    public partial class FrmTestPicColorFindPicAndFindPicExCustomCode : Form
    {
        public FrmTestPicColorFindPicAndFindPicExCustomCode()
        {
            InitializeComponent();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.PicColor.CustomCodeIf = this.cbIf.Checked;
            StaticData.PicColor.CustomCodeElse= this.cbElse.Checked ;
            StaticData.PicColor.CustomCodeIfContent= this.txtIFCustomCode.Text ;
            StaticData.PicColor.CustomCodeElseContent= this.txtElseCustomCode.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cbIf_CheckedChanged(object sender, EventArgs e)
        {
            this.txtIFCustomCode.Enabled = false;
            if (cbIf.Checked)
            {
                this.txtIFCustomCode.Enabled = true;
            }
        }

        private void cbElse_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void FrmTestPicColorFindPicAndFindPicExCustomCode_Load(object sender, EventArgs e)
        {
            this.cbIf.Checked = StaticData.PicColor.CustomCodeIf;
            this.cbElse.Checked = StaticData.PicColor.CustomCodeElse;
            this.txtIFCustomCode.Text = StaticData.PicColor.CustomCodeIfContent;
            this.txtElseCustomCode.Text = StaticData.PicColor.CustomCodeElseContent;
        }
    }
}
