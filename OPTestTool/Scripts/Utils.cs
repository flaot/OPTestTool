using System.Text;
using System.Text.RegularExpressions;
using WindowFinder;

namespace OPTestTool
{
    public class Utils
    {
        #region 输入验证

        //-------  允许输入:整数、退格键(8)、全选(1)、复制(3)、粘贴(22)、负数符号
        public static void TextBoxInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //回车键
            {
            }
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 &&
                e.KeyChar != 1 && e.KeyChar != 3 && e.KeyChar != 22 && e.KeyChar != '-' && e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }
        public static void TextBoxInt_TextChanged(object sender, EventArgs e)
        {
            // 对粘贴值进行验证
            var textbox = (TextBox)sender;
            var reg = new Regex("^-?(0|[1-9][0-9]*)$");
            var str = textbox.Text.Trim();
            var sb = new StringBuilder();
            if (!reg.IsMatch(str))
            {
                bool existSign = false;
                for (int i = 0; i < str.Length; i++)
                {
                    if ((!existSign && str[i] == '-'))
                    {
                        existSign = true;
                        sb.Insert(0, str[i]);
                    }
                    else if ((existSign && str[i] == '+'))
                    {
                        existSign = false;
                        sb.Remove(0, 1);
                    }
                    else if (str[i] >= '0' && str[i] <= '9')
                    {
                        sb.Append(str[i].ToString());
                    }
                }
                if (sb.Length <= 0)
                    textbox.Text = "0";
                else
                    textbox.Text = int.Parse(sb.ToString()).ToString();
                //定义输入焦点在最后一个字符
                textbox.SelectionStart = textbox.Text.Length;
            }
        }
        public static void TextBoxInt_Leave(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
        }

        //public static void TextBoxInt2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == 13) //回车键
        //    {
        //        PauseTextBoxInt(sender);
        //        return;
        //    }
        //    if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 &&
        //        e.KeyChar != 1 && e.KeyChar != 3 && e.KeyChar != 22 && 
        //        e.KeyChar != '+' && e.KeyChar != '-' && e.KeyChar != '*' && e.KeyChar != '/')
        //    {
        //        e.Handled = true;
        //    }
        //}
        //public static void TextBoxInt2_TextChanged(object sender, EventArgs e){ }
        //public static void TextBoxInt2_Leave(object sender, EventArgs e) => PauseTextBoxInt(sender);
        //private static void PauseTextBoxInt(object sender)
        //{
        //    var textbox = (TextBox)sender;
        //    var reg = new Regex("^-?(0|[1-9][0-9]*)$");
        //    string expression = textbox.Text.Trim();
        //    object result = new DataTable().Compute(expression, "").ToString();
        //    string str = result.ToString();
        //    var sb = new StringBuilder();
        //    if (!reg.IsMatch(str))
        //    {
        //        bool existSign = false;
        //        for (int i = 0; i < str.Length; i++)
        //        {
        //            if (str[i] == '.')
        //                break;
        //            if ((!existSign && str[i] == '-'))
        //            {
        //                existSign = true;
        //                sb.Insert(0, str[i]);
        //            }
        //            else if ((existSign && str[i] == '+'))
        //            {
        //                existSign = false;
        //                sb.Remove(0, 1);
        //            }
        //            else if (str[i] >= '0' && str[i] <= '9')
        //            {
        //                sb.Append(str[i].ToString());
        //            }
        //        }
        //        if (sb.Length <= 0)
        //            str = "0";
        //        else
        //            str = int.Parse(sb.ToString()).ToString();
        //    }
        //    textbox.Text = str;
        //    textbox.SelectionStart = textbox.Text.Length;
        //}

        //-------  允许输入:正整数、退格键(8)、全选(1)、复制(3)、粘贴(22)
        public static void TextBoxUInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 &&
                e.KeyChar != 1 && e.KeyChar != 3 && e.KeyChar != 22)
            {
                e.Handled = true;
            }
        }
        public static void TextBoxUInt_TextChanged(object sender, EventArgs e)
        {
            // 对粘贴值进行验证
            var textbox = (TextBox)sender;
            var reg = new Regex("^(0|[1-9][0-9]*)$");
            var str = textbox.Text.Trim();
            var sb = new StringBuilder();
            if (!reg.IsMatch(str) || string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] >= '0' && str[i] <= '9')
                    {
                        sb.Append(str[i].ToString());
                    }
                }
                if (sb.Length <= 0)
                    textbox.Text = "0";
                else
                    textbox.Text = int.Parse(sb.ToString()).ToString();
                //定义输入焦点在最后一个字符
                textbox.SelectionStart = textbox.Text.Length;
            }
        }

        //-------  允许输入:0-1.0、退格键(8)、全选(1)、复制(3)、粘贴(22)
        public static void TextBoxFloatBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 &&
                e.KeyChar != 1 && e.KeyChar != 3 && e.KeyChar != 22 && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        public static void TextBoxFloatBar_TextChanged(object sender, EventArgs e)
        {
            // 对粘贴值进行验证
            var textbox = (TextBox)sender;
            var reg = new Regex("^(0|1|0\\.[0-9]+)$");
            var str = textbox.Text.Trim();
            var sb = new StringBuilder();
            if (!reg.IsMatch(str) || string.IsNullOrEmpty(str))
            {
                bool existSign = false;
                for (int i = 0; i < str.Length; i++)
                {
                    if ((!existSign && (i == 0 && str[i] == '0')))
                    {
                        existSign = true;
                        sb.Insert(0, str[i]);
                    }
                    else if (str[i] >= '0' && str[i] <= '9')
                    {
                        sb.Append(str[i].ToString());
                    }
                }
                if (!existSign)
                    sb.Insert(0, "0.");
                else
                    sb.Insert(1, '.');
                if (sb.Length <= 0)
                    textbox.Text = "0";
                else
                    textbox.Text = float.Parse(sb.ToString()).ToString();
                //定义输入焦点在最后一个字符
                textbox.SelectionStart = textbox.Text.Length;
            }
        }

        private class MouseDataInt
        {
            /// <summary> 目标文本控件 </summary>
            public TextBox target;
            /// <summary> 文本控件的值 </summary>
            public int controlValue;
            /// <summary> 上一次的鼠标位置 </summary>
            public Point mousePos;
            /// <summary> 已按住鼠标 </summary>
            public bool mouseDown;
            /// <summary> 按住时鼠标所在显示器 </summary>
            public Screen screen;
        }
        public static void LabelInt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            var label = (Label)sender;
            label.Focus();
            var mouseData = label.Tag as MouseDataInt;
            if (mouseData == null)
            {
                string targetControlName = label.Tag as string;
                Control[] controls = label.Parent.Controls.Find(targetControlName, false);
                Control control = controls[0];
                var textBox = control as TextBox;
                mouseData = new MouseDataInt();
                mouseData.target = textBox;
                label.Tag = mouseData;
            }
            mouseData.screen = Screen.FromPoint(Control.MousePosition);
            mouseData.mouseDown = true;
            mouseData.mousePos = Control.MousePosition;
            mouseData.controlValue = int.Parse(mouseData.target.Text);
        }
        public static void LabelInt_MouseMove(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;
            var mouseData = label.Tag as MouseDataInt;
            if (mouseData == null) return;
            if (!mouseData.mouseDown) return;
            var offsetVal = Control.MousePosition.X - mouseData.mousePos.X;
            mouseData.controlValue += offsetVal;
            mouseData.target.Text = mouseData.controlValue.ToString();
            if (Control.MousePosition.X <= mouseData.screen.WorkingArea.X)
                Win32.SetCursorPos(mouseData.screen.WorkingArea.Right - 1, Control.MousePosition.Y);
            else if (Control.MousePosition.X >= mouseData.screen.WorkingArea.Right - 1)
                Win32.SetCursorPos(mouseData.screen.WorkingArea.X, Control.MousePosition.Y);
            mouseData.mousePos = Control.MousePosition;
        }
        public static void LabelInt_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            var label = (Label)sender;
            var mouseData = label.Tag as MouseDataInt;
            if (mouseData == null) return;
            mouseData.mouseDown = false;
        }

        private class MouseDataUInt
        {
            /// <summary> 目标文本控件 </summary>
            public TextBox target;
            /// <summary> 文本控件的值 </summary>
            public uint controlValue;
            /// <summary> 上一次的鼠标位置 </summary>
            public Point mousePos;
            /// <summary> 已按住鼠标 </summary>
            public bool mouseDown;
            /// <summary> 按住时鼠标所在显示器 </summary>
            public Screen screen;
        }
        public static void LabelUInt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            var label = (Label)sender;
            label.Focus();
            var mouseData = label.Tag as MouseDataUInt;
            if (mouseData == null)
            {
                string targetControlName = label.Tag as string;
                Control[] controls = label.Parent.Controls.Find(targetControlName, false);
                Control control = controls[0];
                var textBox = control as TextBox;
                mouseData = new MouseDataUInt();
                mouseData.target = textBox;
                label.Tag = mouseData;
            }
            mouseData.screen = Screen.FromPoint(Control.MousePosition);
            mouseData.mouseDown = true;
            mouseData.mousePos = Control.MousePosition;
            mouseData.controlValue = uint.Parse(mouseData.target.Text);
        }
        public static void LabelUInt_MouseMove(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;
            var mouseData = label.Tag as MouseDataUInt;
            if (mouseData == null) return;
            if (!mouseData.mouseDown) return;
            var offsetVal = Control.MousePosition.X - mouseData.mousePos.X;
            long value = mouseData.controlValue + offsetVal;
            mouseData.controlValue = (uint)(value < 0 ? 0 : value);
            mouseData.target.Text = mouseData.controlValue.ToString();
            if (Control.MousePosition.X <= mouseData.screen.WorkingArea.X)
                Win32.SetCursorPos(mouseData.screen.WorkingArea.Right - 1, Control.MousePosition.Y);
            else if (Control.MousePosition.X >= mouseData.screen.WorkingArea.Right - 1)
                Win32.SetCursorPos(mouseData.screen.WorkingArea.X, Control.MousePosition.Y);
            mouseData.mousePos = Control.MousePosition;
        }
        public static void LabelUInt_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            var label = (Label)sender;
            var mouseData = label.Tag as MouseDataUInt;
            if (mouseData == null) return;
            mouseData.mouseDown = false;
        }

        public static void TextBoxFileFolder_DragDrop(object sender, DragEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Array file = (Array)e.Data.GetData(DataFormats.FileDrop);//将拖来的数据转化为数组存储
            foreach (object I in file)
            {
                string str = I.ToString();
                FileInfo info = new FileInfo(str);
                if ((info.Attributes & FileAttributes.Directory) != 0)
                {
                    textBox.Text = str;
                    return;
                }
                if (File.Exists(str))
                {
                    textBox.Text = Path.GetDirectoryName(str);
                    return;
                }
            }
        }
        public static void TextBoxFileFolder_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))    //判断拖来的是否是文件
                e.Effect = DragDropEffects.Link;                //是则将拖动源中的数据连接到控件
            else
                e.Effect = DragDropEffects.None;
        }
        public static void TextBoxFilePath_DragDrop(object sender, DragEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Array file = (Array)e.Data.GetData(DataFormats.FileDrop);//将拖来的数据转化为数组存储
            foreach (object I in file)
            {
                string str = I.ToString();
                FileInfo info = new FileInfo(str);
                if ((info.Attributes & FileAttributes.Directory) != 0)
                    continue;
                if (File.Exists(str))
                {
                    textBox.Text = str;
                    return;
                }
            }
        }
        public static void TextBoxFilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))    //判断拖来的是否是文件
                e.Effect = DragDropEffects.Link;                //是则将拖动源中的数据连接到控件
            else
                e.Effect = DragDropEffects.None;
        }
        #endregion


        public static string GetCharsFromKeys(Keys keys, bool shift, bool altGr)
        {
            var buf = new StringBuilder(256);
            var keyboardState = new byte[256];
            if (shift)
                keyboardState[(int)Keys.ShiftKey] = 0xff;
            if (altGr)
            {
                keyboardState[(int)Keys.ControlKey] = 0xff;
                keyboardState[(int)Keys.Menu] = 0xff;
            }
            Win32.ToUnicode((uint)keys, 0, keyboardState, buf, 256, 0);
            return buf.ToString();
        }
    }
}
