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
    public partial class FrmTestMouseGetCursorShapeEx : Form
    {
        public FrmTestMouseGetCursorShapeEx()
        {
            InitializeComponent();
            this.cbSelect.Items.Add("0");
            this.cbSelect.Items.Add("1");
            this.cbSelect.SelectedIndex = StaticData.Mouse.GetCursorShaperEx;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            StaticData.Mouse.GetCursorShaperEx = this.cbSelect.SelectedIndex;
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
