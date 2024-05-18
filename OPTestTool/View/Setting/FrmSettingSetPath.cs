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
    public partial class FrmSettingSetPath : Form
    {
        public FrmSettingSetPath()
        {
            InitializeComponent();
            this.txtPath.Text = StaticData.Set.SetPath;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.Set.SetPath = this.txtPath.Text;
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
