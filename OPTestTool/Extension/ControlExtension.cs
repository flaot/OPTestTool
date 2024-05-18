using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPTestTool.Extension
{
    public static class ControlExtension
    {
        public static IEnumerable<TextBox> GetAllTextBoxes(this TabControl tabControl)
        {
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    if (control is TextBox)
                    {
                        yield return (TextBox)control;
                    }

                    foreach (Control childControl in control.Controls)
                    {
                        if (childControl is TextBox)
                        {
                            yield return (TextBox)childControl;
                        }
                    }
                }
            }
        }
    }
}