using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.Model
{
    public class FrmTestScript
    {
        public string GetPath()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".cs"; // Default file extension
            dlg.Filter = "C# Files|*.cs?|All Files|*.*"; // Filter files by extension

            if (dlg.ShowDialog() == DialogResult.OK || dlg.ShowDialog() == DialogResult.Yes)
            {
                return dlg.FileName;
            }
            return "";
        }
    }
}