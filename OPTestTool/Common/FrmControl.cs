using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPTestTool.Common
{
    public static class FrmControl
    {
        public static void CheckBoxUnChecked(Control fatherControl)
        {
            Control.ControlCollection sonControls = fatherControl.Controls;
            foreach (Control control in sonControls)
            {
                if (control is CheckBox)
                {
                    var cbc = control as CheckBox;
                    cbc.Checked = false;
                }
            }
        }

        public static void RadioButtonUnchecked(Control fatherControl)
        {
            Control.ControlCollection sonControls = fatherControl.Controls;
            foreach (Control control in sonControls)
            {
                if (control is RadioButton)
                {
                    var cbc = control as RadioButton;
                    cbc.Checked = false;
                }
            }
        }

        /// <summary>
        /// 匹配指定标题并且选中当前控件
        /// </summary>
        /// <param name="parentControls">父控件</param>
        /// <param name="text">标题名</param>
        public static void ControlCheckedByText(Control parentControls, string text)
        {
            List<string> listText = new List<string>();
            if (text.Contains("|"))
            {
                listText = text.Split('|').ToList();
            }
            foreach (Control control in parentControls.Controls)
            {
                if (control is RadioButton)
                {
                    var radioButton = control as RadioButton;
                    if (listText.Count > 0)
                    {
                        listText.ForEach(f =>
                        {
                            if (radioButton.Text == f)
                            {
                                radioButton.Checked = true;
                            }
                        });
                    }
                    else
                    {
                        if (radioButton.Text == text)
                            radioButton.Checked = true;
                    }
                }
                if (control is CheckBox)
                {
                    var aa = parentControls.Controls.Count;
                    var checkBox = control as CheckBox;
                    if (listText.Count > 0)
                    {
                        listText.ForEach(f =>
                        {
                            if (checkBox.Text == f)
                            {
                                checkBox.Checked = true;
                            }
                        });
                    }
                    else
                    {
                        if (checkBox.Text == text)
                            checkBox.Checked = true;
                    }
                }
            }
        }
    }
}