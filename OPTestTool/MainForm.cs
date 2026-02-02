using OPTestTool.Properties;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using WindowFinder;
using Timer = System.Windows.Forms.Timer;

namespace OPTestTool
{
    public partial class MainForm : Form
    {
        private Timer _timerTick;
        private readonly ConcurrentQueue<string> _logQueue = new ConcurrentQueue<string>();
        private static bool _showLogTime;
        private OpSoft opSoft = new OpSoft();
        private GetScreenDataBmpForm _getScreenDataBmpForm;
        private WordDictTool.MainForm _wordDictTool;

        public MainForm()
        {
            InitializeComponent();
            Logger.LogMessageReceived += Logger_LogMessageReceived;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            opSoft.SetShowErrorMsg(0);
            ComboBox_MouseAction.SelectedIndex = 0;
            ComboBox_CodeLang.SelectedIndex = 0;
            _timerTick = new Timer();
            _timerTick.Interval = 20;
            _timerTick.Tick += EventTimer_Tick;
            _timerTick.Start();
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timerTick.Stop();
        }

        #region 日志
        private void CheckBox_LogShowTime_CheckedChanged(object sender, EventArgs e)
        {
            lock (_logQueue)
            {
                _showLogTime = CheckBox_LogShowTime.Checked;
            }
        }
        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            TxtBox_Log.Clear();
        }
        private void Logger_LogMessageReceived(LogType logType, string context)
        {
            if (logType == LogType.Exception)
                return;
            lock (_logQueue)
            {
                if (_showLogTime)
                    context = string.Format("【{0:HH:mm:ss}】", DateTime.Now) + context;
            }
            _logQueue.Enqueue(logType.ToString() + "|" + context);
        }
        private void EventTimer_Tick(object sender, EventArgs e)
        {
            int num = 0;
            while (true)
            {
                if (!_logQueue.TryDequeue(out var result))
                    break;
                if (++num >= 10)
                    break;
                int splitIndex = result.IndexOf('|');
                LogType logType = (LogType)Enum.Parse(typeof(LogType), result.Substring(0, splitIndex));
                string context = result.Substring(splitIndex + 1);
                switch (logType)
                {
                    case LogType.Log:
                    default:
                        TxtBox_Log.SelectionColor = Color.Black;
                        break;
                    case LogType.Warning:
                        TxtBox_Log.SelectionColor = Color.Blue;
                        break;
                    case LogType.Error:
                        TxtBox_Log.SelectionColor = Color.Red;
                        break;
                }
                TxtBox_Log.AppendText(context);
                TxtBox_Log.AppendText(Environment.NewLine);
                if (TxtBox_Log.TextLength > 100000)
                {
                    TxtBox_Log.Clear();
                }
                if (CheckBox_LogAutoScroll.Checked)
                {
                    TxtBox_Log.Select(TxtBox_Log.TextLength, 0);
                    TxtBox_Log.ScrollToCaret();
                }
            }
            if (_getScreenDataBmpForm != null)
            {
                int x1 = int.Parse(Txt_CaptureX1.Text);
                int y1 = int.Parse(Txt_CaptureY1.Text);
                int x2 = int.Parse(Txt_CaptureX2.Text);
                int y2 = int.Parse(Txt_CaptureY2.Text);
                _getScreenDataBmpForm.Update(x1, y1, x2, y2);
            }
        }
        #endregion

        #region 综合设置
        private void Finder_Window_WindowHandleChanged(object sender, EventArgs e)
        {
            var finder = (WindowFinder.WindowFinder)sender;
            Txt_WindowClassName.Text = finder.WindowClass;
            Txt_WindowHwnd.Text = finder.WindowHandle.ToString();
            Txt_WindowTitle.Text = finder.WindowText;
            Txt_WindowPID.Text = finder.WindowPID.ToString();

            IntPtr newWnd = finder.WindowHandle;
            IntPtr rootWnd = newWnd;
            do
            {
                newWnd = Win32.GetParent(newWnd);
                if (newWnd == IntPtr.Zero) break;
                rootWnd = newWnd;
            } while (true);
            TreeView_Window.Nodes.Clear();
            var rootNode = new TreeNode(GetWindowInfo(rootWnd));
            rootNode.Tag = rootWnd;
            TreeView_Window.Nodes.Add(rootNode);
            ApplayIcon(rootNode);
            AddChildWindow(rootNode, finder.WindowHandle);
            rootNode.Expand();
            if (rootWnd == finder.WindowHandle)
                TreeView_Window.SelectedNode = rootNode;
        }
        private void TreeView_Window_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var hWnd = (IntPtr)e.Node.Tag;
            Win32.HighlightWindow(hWnd);
        }
        private void TreeView_Window_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var hWnd = (IntPtr)e.Node.Tag;
            Txt_WindowClassName.Text = Win32.GetClassName(hWnd);
            Txt_WindowHwnd.Text = hWnd.ToString();
            Txt_WindowTitle.Text = Win32.GetWindowText(hWnd);
            Txt_WindowPID.Text = Win32.GetWindowPID(hWnd).ToString();
        }
        private void Btn_AllWindow_Click(object sender, EventArgs e)
        {
            TreeView_Window.Nodes.Clear();
            IntPtr desktopWnd = Win32.GetDesktopWindow();
            var rootNode = new TreeNode(GetWindowInfo(desktopWnd));
            rootNode.Tag = desktopWnd;
            TreeView_Window.Nodes.Add(rootNode);
            ApplayIcon(rootNode);
            AddChildWindow(rootNode, IntPtr.Zero);
            rootNode.Expand();
        }
        private void AddChildWindow(TreeNode rootNode, IntPtr selectHwnd)
        {
            var rootWnd = (IntPtr)rootNode.Tag;
            IntPtr hwnd = Win32.GetWindow(rootWnd, Win32.WindOpt.GW_CHILD);
            while (hwnd != IntPtr.Zero)
            {
                if (CheckBox_ExcludeHide.Checked && !Win32.IsWindowVisible(hwnd))
                {
                    hwnd = Win32.GetWindow(hwnd, Win32.WindOpt.GW_HWNDNEXT);
                    continue;
                }
                var node = new TreeNode(GetWindowInfo(hwnd));
                node.Tag = hwnd;
                rootNode.Nodes.Add(node);
                if (selectHwnd == hwnd)
                    node.TreeView.SelectedNode = node;
                ApplayIcon(node);
                AddChildWindow(node, selectHwnd);
                hwnd = Win32.GetWindow(hwnd, Win32.WindOpt.GW_HWNDNEXT);
            }
        }
        private void ApplayIcon(TreeNode node)
        {
            var hwnd = (IntPtr)node.Tag;
            if (node.TreeView.ImageList == null)
            {
                node.TreeView.ImageList = new ImageList();
                node.TreeView.ImageList.Images.Add("Desktop", Resources.Desktop);
                node.TreeView.ImageList.Images.Add("Window", Resources.Window);
                node.TreeView.ImageList.Images.Add("HiddenWindow", Resources.HiddenWindow);
            }
            if (hwnd == Win32.GetDesktopWindow())
            {
                node.ImageKey = "Desktop";
            }
            else if (Win32.GetParent(hwnd) == IntPtr.Zero)
            {
                string exeFile = opSoft.GetWindowProcessPath((int)hwnd);
                Icon icon = Icon.ExtractAssociatedIcon(exeFile);
                if (!node.TreeView.ImageList.Images.Keys.Contains(exeFile))
                    node.TreeView.ImageList.Images.Add(exeFile, icon);
                node.ImageKey = exeFile;
            }
            else
            {
                node.ImageKey = Win32.IsWindowVisible(hwnd) ? "Window" : "HiddenWindow";
            }
            node.SelectedImageKey = node.ImageKey;
        }
        private string GetWindowInfo(IntPtr hWnd)
        {
            if (Win32.IsWindow(hWnd) == 0)
                return string.Empty;
            var title = Win32.GetWindowText(hWnd).Trim();
            var className = Win32.GetClassName(hWnd).Trim();
            return string.Format("[{0}][{1}][{2}]", hWnd, title, className);
        }
        private void Btn_SetPath_Click(object sender, EventArgs e)
        {
            SetPathForm form = new SetPathForm(opSoft.GetPath());
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string path = form.GetSelect();
                opSoft.SetPath(path);
            }
        }
        private void Btn_GetCommandLine_Click(object sender, EventArgs e)
        {
            Logger.Log(">>>GetCommandLine\n" + string.Join(" ", Environment.GetCommandLineArgs()));
        }
        #endregion

        #region 绑定参数
        private void Btn_ChangeDisplay_Click(object sender, EventArgs e)
        {
            ChangeDisplayForm form = new ChangeDisplayForm();
            form.SetSelect(Txt_BindDisplayMode.Text);
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Txt_BindDisplayMode.Text = form.GetSelect();
            }
        }
        private void Btn_ChangeMouse_Click(object sender, EventArgs e)
        {
            ChangeMouseForm form = new ChangeMouseForm();
            form.SetSelect(Txt_BindMouseMode.Text);
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Txt_BindMouseMode.Text = form.GetSelect();
            }
        }
        private void Btn_ChangeKeypad_Click(object sender, EventArgs e)
        {
            ChangeKeypadForm form = new ChangeKeypadForm();
            form.SetSelect(Txt_BindKeypadMode.Text);
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Txt_BindKeypadMode.Text = form.GetSelect();
            }
        }
        private void Btn_Bind_Click(object sender, EventArgs e)
        {
            if (opSoft.IsBind() != 0)
            {
                Logger.Log(">>>UnBindWindow");
                int reault = opSoft.UnBindWindow();
                Logger.Log("返回值：" + reault);
            }
            else
            {
                var nHwnd = string.IsNullOrEmpty(Txt_WindowHwnd.Text) ? 0 : int.Parse(Txt_WindowHwnd.Text);
                if (CheckBox_BindMoveWendow.Checked)
                    opSoft.MoveWindow(nHwnd, -10, 0); //OP发送的SetWindowPos(-10,0,W,H,28)  大漠的是SetWindowPos(-10,0,0,0,21)
                Logger.Log(string.Format(">>>BindWindow\n{0},\"{1}\",\"{2}\",\"{3}\",{4}", nHwnd, Txt_BindDisplayMode.Text, Txt_BindMouseMode.Text, Txt_BindKeypadMode.Text, Txt_BindMode.Text));
                int reault = opSoft.BindWindow(nHwnd, Txt_BindDisplayMode.Text, Txt_BindMouseMode.Text, Txt_BindKeypadMode.Text, int.Parse(Txt_BindMode.Text));
                Logger.Log("返回值：" + reault);
                if (CheckBox_BindMessage.Checked && reault == 1)
                {
                    MessageBox.Show("绑定成功");
                }
            }

            if (opSoft.IsBind() != 0)
            {
                Btn_Bind.Text = "解绑";
                Btn_SwitchBind.Enabled = true;
            }
            else
            {
                Btn_Bind.Text = "绑定";
                Btn_SwitchBind.Enabled = false;
            }
        }
        private void Btn_SwitchBind_Click(object sender, EventArgs e)
        {
            Btn_Bind_Click(sender, e);
        }
        private void Btn_GenBindCode_Click(object sender, EventArgs e)
        {
            string bindCode = string.Format("op_ret = op.BindWindow(hwnd,\"{1}\",\"{2}\",\"{3}\",{4})", Txt_WindowHwnd.Text, Txt_BindDisplayMode.Text, Txt_BindMouseMode.Text, Txt_BindKeypadMode.Text, Txt_BindMode.Text);
            Logger.Log(string.Format(">>>SetClipboard \"{0}\"", bindCode));
            opSoft.SetClipboard(bindCode);
            MessageBox.Show("代码已经复制到剪贴板,直接粘贴即可使用！");
        }
        #endregion

        #region 测试图色
        private void Btn_Capture_Click(object sender, EventArgs e)
        {
            int x1 = int.Parse(Txt_CaptureX1.Text);
            int y1 = int.Parse(Txt_CaptureY1.Text);
            int x2 = int.Parse(Txt_CaptureX2.Text);
            int y2 = int.Parse(Txt_CaptureY2.Text);
            string file = Path.Combine(opSoft.GetPath(), "capture_file.bmp");
            Logger.Log(string.Format(">>>Capture\n{0},{1},{2},{3},\"{4}\"", x1, y1, x2, y2, file));
            int reault = opSoft.Capture(x1, y1, x2, y2, file);
            Logger.Log("返回值：" + reault);
            if (reault == 1)
            {
                Process.Start("mspaint.exe", string.Format("\"{0}\"", file));
            }
        }
        private void Btn_GetColor_Click(object sender, EventArgs e)
        {
            int x = int.Parse(Txt_GetColorX.Text);
            int y = int.Parse(Txt_GetColorY.Text);
            Logger.Log(string.Format(">>>GetColor {0},{1}", x, y));
            string reault = opSoft.GetColor(x, y);
            Logger.Log("返回值：" + reault);
        }
        private void Finder_GetColor_MouseChangePos(object sender, EventArgs e)
        {
            var finder = (WindowFinder.LocationFinder)sender;
            int x = finder.MousePos.X, y = finder.MousePos.Y;
            if (opSoft.IsBind() == 1 && opSoft.GetBindWindow() != (int)Win32.GetDesktopWindow())
                opSoft.ScreenToClient(opSoft.GetBindWindow(), ref x, ref y);
            Txt_GetColorX.Text = x.ToString();
            Txt_GetColorY.Text = y.ToString();
        }
        private void Btn_FindPic_Click(object sender, EventArgs e)
        {
            int x1 = int.Parse(Txt_FindPicX1.Text);
            int y1 = int.Parse(Txt_FindPicY1.Text);
            int x2 = int.Parse(Txt_FindPicX2.Text);
            int y2 = int.Parse(Txt_FindPicY2.Text);
            float sim = float.Parse(Txt_FindPicSim.Text);
            int dir = int.Parse(Txt_FindPicDir.Text);
            Logger.Log(string.Format(">>>FindPic\n{0},{1},{2},{3},\"{4}\",\"{5}\",{6},{7},x,y", x1, y1, x2, y2, Txt_FindPicFile.Text, Txt_FindPicDelta.Text, sim, dir));
            int reault = opSoft.FindPic(x1, y1, x2, y2, Txt_FindPicFile.Text, Txt_FindPicDelta.Text, sim, dir, out var x, out var y);
            Logger.Log("返回值：" + reault + ",x=" + x + ",y=" + y);
        }
        private void CheckBox_GetScreenDataBmp_CheckedChanged(object sender, EventArgs e)
        {
            if (_getScreenDataBmpForm == null)
            {
                _getScreenDataBmpForm = new GetScreenDataBmpForm(opSoft);
                _getScreenDataBmpForm.FormClosed += ScreenDataBmpForm_FormClosed;
            }
            if (CheckBox_GetScreenDataBmp.Checked)
                _getScreenDataBmpForm.Show();
            else
                _getScreenDataBmpForm.Hide();
        }
        private void ScreenDataBmpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CheckBox_GetScreenDataBmp.Checked = false;
            _getScreenDataBmpForm = null;
        }
        private void Btn_FindPicEx_Click(object sender, EventArgs e)
        {
            int x1 = int.Parse(Txt_FindPicX1.Text);
            int y1 = int.Parse(Txt_FindPicY1.Text);
            int x2 = int.Parse(Txt_FindPicX2.Text);
            int y2 = int.Parse(Txt_FindPicY2.Text);
            float sim = float.Parse(Txt_FindPicSim.Text);
            int dir = int.Parse(Txt_FindPicDir.Text);
            Logger.Log(string.Format(">>>FindPicEx\n{0},{1},{2},{3},\"{4}\",\"{5}\",{6},{7}", x1, y1, x2, y2, Txt_FindPicFile.Text, Txt_FindPicDelta.Text, sim, dir));
            string reault = opSoft.FindPicEx(x1, y1, x2, y2, Txt_FindPicFile.Text, Txt_FindPicDelta.Text, sim, dir);
            Logger.Log("返回值：" + reault);
        }
        #endregion

        #region 测试鼠标
        private void Btn_SetMouseDelay_Click(object sender, EventArgs e)
        {
            SetMouseDelayForm form = new SetMouseDelayForm();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            string delay = form.GetDelay();
            string mode = form.GetMode();
            int nDelay = int.Parse(delay);
            Logger.Log(string.Format(">>>SetMouseDelay {0},{1}", mode, nDelay));
            int reault = opSoft.SetMouseDelay(mode, nDelay);
            Logger.Log("返回值：" + reault);
        }
        private void Btn_MoveTo_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            int x = int.Parse(Txt_MoveToX.Text);
            int y = int.Parse(Txt_MoveToY.Text);
            Logger.Log(string.Format(">>>MoveTo {0},{1}", x, y));
            int reault = opSoft.MoveTo(x, y);
            Logger.Log("返回值：" + reault);
            if (reault == 1 && CheckBox_MoveAndSend.Checked)
            {
                Logger.Log(string.Format(">>>{0}", ComboBox_MouseAction.Text));
                switch (ComboBox_MouseAction.Text)
                {
                    case "LeftClick": reault = opSoft.LeftClick(); break;
                    case "RightClick": reault = opSoft.RightClick(); break;
                    case "MiddleClick": reault = opSoft.MiddleClick(); break;
                    case "LeftDoubleClick": reault = opSoft.LeftDoubleClick(); break;
                    case "WheelUp": reault = opSoft.WheelUp(); break;
                    case "WheelDown": reault = opSoft.WheelDown(); break;
                    default: break;
                }
                Logger.Log("返回值：" + reault);
            }
        }
        private void Finder_MoveTo_MouseChangePos(object sender, EventArgs e)
        {
            var finder = (WindowFinder.LocationFinder)sender;
            int x = finder.MousePos.X, y = finder.MousePos.Y;
            if (opSoft.IsBind() == 1 && opSoft.GetBindWindow() != (int)Win32.GetDesktopWindow())
                opSoft.ScreenToClient(opSoft.GetBindWindow(), ref x, ref y);
            Txt_MoveToX.Text = x.ToString();
            Txt_MoveToY.Text = y.ToString();
        }
        private void Btn_MoveR_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            int x = int.Parse(Txt_MoveRX.Text);
            int y = int.Parse(Txt_MoveRY.Text);
            Logger.Log(string.Format(">>>MoveR {0},{1}", x, y));
            int reault = opSoft.MoveR(x, y);
            Logger.Log("返回值：" + reault);
            if (reault == 1 && CheckBox_MoveAndSend.Checked)
            {
                Logger.Log(string.Format(">>>{0}", ComboBox_MouseAction.Text));
                switch (ComboBox_MouseAction.Text)
                {
                    case "LeftClick": reault = opSoft.LeftClick(); break;
                    case "RightClick": reault = opSoft.RightClick(); break;
                    case "MiddleClick": reault = opSoft.MiddleClick(); break;
                    case "LeftDoubleClick": reault = opSoft.LeftDoubleClick(); break;
                    case "WheelUp": reault = opSoft.WheelUp(); break;
                    case "WheelDown": reault = opSoft.WheelDown(); break;
                    default: break;
                }
                Logger.Log("返回值：" + reault);
            }
        }
        private void Btn_LeftDown_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>LeftDown");
            int reault = opSoft.LeftDown();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_LeftUp_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>LeftUp");
            int reault = opSoft.LeftUp();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_LeftClick_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>LeftClick");
            int reault = opSoft.LeftClick();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_RightDown_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>RightDown");
            int reault = opSoft.RightDown();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_RightUp_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>RightUp");
            int reault = opSoft.RightUp();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_RightClick_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>RightClick");
            int reault = opSoft.RightClick();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_MiddleDown_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>MiddleDown");
            int reault = opSoft.MiddleDown();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_MiddleUp_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>MiddleUp");
            int reault = opSoft.MiddleUp();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_MiddleClick_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>MiddleClick");
            int reault = opSoft.MiddleClick();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_WheelDown_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>WheelDown");
            int reault = opSoft.WheelDown();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_WheelUp_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>WheelUp");
            int reault = opSoft.WheelUp();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_LeftDoubleClick_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>LeftDoubleClick");
            int reault = opSoft.LeftDoubleClick();
            Logger.Log("返回值：" + reault);
        }
        private void Btn_GetCursorPos_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Logger.Log(">>>GetCursorPos x,y");
            int reault = opSoft.GetCursorPos(out var x, out var y);
            Logger.Log("返回值：" + reault + ",x=" + x + ",y=" + y);
        }
        #endregion

        #region 测试键盘
        private void Btn_SetKeypadDelay_Click(object sender, EventArgs e)
        {
            SetKeypadDelayForm form = new SetKeypadDelayForm();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            string delay = form.GetDelay();
            string mode = form.GetMode();
            int nDelay = int.Parse(delay);
            Logger.Log(string.Format(">>>SetKeypadDelay {0},{1}", mode, nDelay));
            int reault = opSoft.SetKeypadDelay(mode, nDelay);
            Logger.Log("返回值：" + reault);
        }
        private void Btn_KeyDown_Click(object sender, EventArgs e)
        {
            if (CheckBox_KeyPreActive.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            int keyCode = 0;
            if (!string.IsNullOrEmpty(Txt_KeyDown.Text))
                keyCode = int.Parse(Txt_KeyDown.Text);
            Logger.Log(string.Format(">>>KeyDown {0}", keyCode));
            int reault = opSoft.KeyDown(keyCode);
            Logger.Log("返回值：" + reault);
        }
        private void Btn_KeyUp_Click(object sender, EventArgs e)
        {
            if (CheckBox_KeyPreActive.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            int keyCode = 0;
            if (!string.IsNullOrEmpty(Txt_KeyUp.Text))
                keyCode = int.Parse(Txt_KeyUp.Text);
            Logger.Log(string.Format(">>>KeyUp {0}", keyCode));
            int reault = opSoft.KeyUp(keyCode);
            Logger.Log("返回值：" + reault);
        }
        private void Btn_KeyPress_Click(object sender, EventArgs e)
        {
            if (CheckBox_KeyPreActive.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);

            int keyCode = 0;
            if (!string.IsNullOrEmpty(Txt_KeyPress.Text))
                keyCode = int.Parse(Txt_KeyPress.Text);
            Logger.Log(string.Format(">>>KeyPress {0}", keyCode));
            int reault = opSoft.KeyPress(keyCode);
            Logger.Log("返回值：" + reault);
        }

        private void Btn_KeyGroup_Click(object sender, EventArgs e)
        {
            if (CheckBox_KeyPreActive.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            Btn_KeyDown_Click(sender, e);
            Logger.Log(">>>Delay 1000");
            opSoft.Delay(1000);
            Btn_KeyPress_Click(sender, e);
            Logger.Log(">>>Delay 1000");
            opSoft.Delay(1000);
            Btn_KeyUp_Click(sender, e);
        }
        private void Btn_KeyPressStr_Click(object sender, EventArgs e)
        {
            if (CheckBox_KeyPreActive.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);

            int delay = int.Parse(Txt_KeyPressStrDelay.Text);
            Logger.Log(string.Format(">>>KeyPressStr {0},{1}", Txt_KeyPressStr.Text, delay));
            int reault = opSoft.KeyPressStr(Txt_KeyPressStr.Text, delay);
            Logger.Log("返回值：" + reault);
        }
        private void Txt_KeyDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void Txt_KeyDown_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
            Txt_KeyDown.Text = e.KeyValue.ToString();
            Txt_KeyDownTip.Text = Utils.GetCharsFromKeys(e.KeyCode, e.Shift, e.Alt);
        }
        private void Txt_KeyUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void Txt_KeyUp_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = false;
            Txt_KeyUp.Text = e.KeyValue.ToString();
            Txt_KeyUpTip.Text = Utils.GetCharsFromKeys(e.KeyCode, e.Shift, e.Alt);
        }
        private void Txt_KeyPress_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void Txt_KeyPress_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = false;
            Txt_KeyPress.Text = e.KeyValue.ToString();
            Txt_KeyPressTip.Text = Utils.GetCharsFromKeys(e.KeyCode, e.Shift, e.Alt);
        }
        #endregion

        #region 文本输入
        private void Btn_SendString_Click(object sender, EventArgs e)
        {
            if (CheckBox_SendPreActive.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);

            string sendText = Txt_SendText.Text;
            Logger.Log(string.Format(">>>SendString {0},{1}", opSoft.GetBindWindow(), sendText));
            int reault = opSoft.SendString(opSoft.GetBindWindow(), sendText);
            Logger.Log("返回值：" + reault);
        }
        private void Btn_SendStringIme_Click(object sender, EventArgs e)
        {
            if (CheckBox_SendPreActive.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);

            string sendText = Txt_SendText.Text;
            Logger.Log(string.Format(">>>SendStringIme {0},{1}", opSoft.GetBindWindow(), sendText));
            int reault = opSoft.SendStringIme(opSoft.GetBindWindow(), sendText);
            Logger.Log("返回值：" + reault);
        }
        private void Btn_SendPaste_Click(object sender, EventArgs e)
        {
            if (CheckBox_SendPreActive.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);

            Logger.Log(string.Format(">>>SendPaste {0}", opSoft.GetBindWindow()));
            int reault = opSoft.SendPaste(opSoft.GetBindWindow());
            Logger.Log("返回值：" + reault);
        }
        #endregion

        #region 内存汇编
        private void Btn_ReadInt_Click(object sender, EventArgs e)
        {
            ReadIntForm form = new ReadIntForm();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            switch (form.GetSelect())
            {
                case "+64": //64位有符号
                    break;
                case "+32": //32位有符号
                    break;
                case "-32": //32位无符号
                    break;
                case "+16": //16位有符号
                    break;
                case "-16": //16位无符号
                    break;
                case "+8": //8位有符号
                    break;
                case "-8": //8位无符号
                    break;
                default:
                    throw new KeyNotFoundException(form.GetSelect());
            }
        }
        private void Btn_ReadFloat_Click(object sender, EventArgs e)
        {
        }
        private void Btn_ReadDouble_Click(object sender, EventArgs e)
        {

        }
        private void Btn_ReadString_Click(object sender, EventArgs e)
        {
            ReadStringForm form = new ReadStringForm();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            string encodingStr = form.GetSelect();
            int length = form.Length;
            var encoding = Encoding.GetEncoding(encodingStr);
        }
        private void Btn_ReadData_Click(object sender, EventArgs e)
        {
            ReadDataForm form = new ReadDataForm();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            int length = form.Length;
            Logger.Log(string.Format(">>>ReadData {0},\"{1}\",{2}", opSoft.GetBindWindow(), Txt_ReadAddress.Text, length));
            string reault = opSoft.ReadData(opSoft.GetBindWindow(), Txt_ReadAddress.Text, length);
            Logger.Log("返回值：" + reault);
        }
        private void Btn_WriteInt_Click(object sender, EventArgs e)
        {
            WriteIntForm form = new WriteIntForm();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            switch (form.GetSelect())
            {
                case "+64": //64位有符号
                    break;
                case "+32": //32位有符号
                    break;
                case "+16": //16位有符号
                    break;
                case "+8": //8位有符号
                    break;
                default:
                    throw new KeyNotFoundException(form.GetSelect());
            }
        }
        private void Btn_WriteFloat_Click(object sender, EventArgs e)
        {
            WriteFloatForm form = new WriteFloatForm();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            float value = form.Context();
        }
        private void Btn_WriteDouble_Click(object sender, EventArgs e)
        {
            WriteDoubleForm form = new WriteDoubleForm();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            double value = form.Context();
        }
        private void Btn_WriteString_Click(object sender, EventArgs e)
        {
            WriteStringForm form = new WriteStringForm();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            string encodingStr = form.GetSelect();
            string context = form.Context;
            var encoding = Encoding.GetEncoding(encodingStr);
        }
        private void Btn_WriteData_Click(object sender, EventArgs e)
        {
            WriteDataForm form = new WriteDataForm();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            string context = form.Context;
            Logger.Log(string.Format(">>>WriteData {0},\"{1}\",{2},{3}", opSoft.GetBindWindow(), Txt_ReadAddress.Text, context, context.Length));
            int reault = opSoft.WriteData(opSoft.GetBindWindow(), Txt_ReadAddress.Text, context, context.Length);
            Logger.Log("返回值：" + reault);
        }
        #endregion

        #region OPExport
        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var linkLabel = (LinkLabel)sender;
            var url = (string)linkLabel.Tag;
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        private void Btn_GenerateCode_Click(object sender, EventArgs e)
        {
            string exeFile = Path.Combine(Application.StartupPath, "OpExport/OpExport.exe");
            if (!File.Exists(exeFile))
            {
                MessageBox.Show(string.Format("无法执行,未找到可执行文件\n{0}", exeFile));
                return;
            }
            string opFolder = Txt_OPFolder.Text;
            if (!CheckBox_UseOutProject.Checked)
            {
                string tempProject = Path.Combine(Path.GetTempPath(), "OPProject");
                string tempLibopFile = Path.Combine(tempProject, "libop/libop.h");
                string tempIdlFile = Path.Combine(tempProject, "libop/com/op.idl");
                if (!Directory.Exists(Path.GetDirectoryName(tempLibopFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(tempLibopFile));
                if (!Directory.Exists(Path.GetDirectoryName(tempIdlFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(tempIdlFile));
                if (!File.Exists(tempLibopFile) || File.ReadAllText(tempLibopFile) != Resources.libop)
                    File.WriteAllText(tempLibopFile, Resources.libop);
                if (!File.Exists(tempIdlFile) || File.ReadAllText(tempIdlFile) != Resources.op)
                    File.WriteAllText(tempIdlFile, Resources.op);
                opFolder = tempProject;
            }
            string libopFile = Path.Combine(opFolder, "libop/libop.h");
            string idlFile = Path.Combine(opFolder, "libop/com/op.idl");
            if (!File.Exists(libopFile))
            {
                MessageBox.Show("找不到文件：\n" + libopFile);
                return;
            }
            if (!File.Exists(idlFile))
            {
                MessageBox.Show("找不到文件：\n" + idlFile);
                return;
            }
            string args = string.Format("{0} -lang {1} -doc {2}", opFolder, ComboBox_CodeLang.Text, CheckBox_AddWifiDoc.Checked);
            Process process = new Process();
            process.StartInfo.FileName = exeFile;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
            if (CheckBox_OpenGenCodeFolder.Checked)
                Process.Start(new ProcessStartInfo(Path.GetDirectoryName(exeFile)) { UseShellExecute = true });

            //对外部工程进行更改
            if (CheckBox_UseOutProject.Checked && ComboBox_CodeLang.Text.Equals("op", StringComparison.OrdinalIgnoreCase))
            {
                string folder = Path.Combine(Path.GetDirectoryName(exeFile), "OP");
                string srcHeader = Path.Combine(folder, "libopExport.h");
                string srcSource = Path.Combine(folder, "libopExport.cpp");
                if (!File.Exists(srcHeader) || !File.Exists(srcSource))
                    return;
                string desHeader = Path.Combine(opFolder, "libop/libopExport.h");
                string desSource = Path.Combine(opFolder, "libop/libopExport.cpp");
                if (!File.Exists(desHeader) || File.ReadAllText(desHeader) != File.ReadAllText(srcHeader))
                    File.Copy(srcHeader, desHeader, true);
                if (!File.Exists(desSource) || File.ReadAllText(desSource) != File.ReadAllText(srcSource))
                    File.Copy(srcSource, desSource, true);
                string makeFile = Path.Combine(opFolder, "libop/CMakeLists.txt");
                if (!File.Exists(makeFile))
                    return;
                string[] makeLines = File.ReadAllLines(makeFile);
                List<string> newMakeLines = new List<string>(makeLines);
                makeLines = Array.ConvertAll(makeLines, item => item.Trim());
                if (Array.Exists(makeLines, "\"libopExport.cpp\"".Equals))
                    return;
                int index = Array.FindIndex(makeLines, "\"libop.cpp\"".Equals);
                if (index < 0)
                    return;
                newMakeLines.Insert(index + 1, "\"libopExport.cpp\"");
                File.WriteAllLines(makeFile, newMakeLines);
            }
        }
        private void CheckBox_UseOutProject_CheckedChanged(object sender, EventArgs e)
        {
            Panel_UseOutProject.Visible = CheckBox_UseOutProject.Checked;
        }
        #endregion


        #region 控件-输入限制
        private void Txt_Path_DragEnter(object sender, DragEventArgs e) => Utils.TextBoxFilePath_DragEnter(sender, e);
        private void Txt_Path_DragDrop(object sender, DragEventArgs e) => Utils.TextBoxFilePath_DragDrop(sender, e);
        private void Txt_Folder_DragEnter(object sender, DragEventArgs e) => Utils.TextBoxFileFolder_DragEnter(sender, e);
        private void Txt_Folder_DragDrop(object sender, DragEventArgs e) => Utils.TextBoxFileFolder_DragDrop(sender, e);

        private void LabelInt_MouseDown(object sender, MouseEventArgs e) => Utils.LabelInt_MouseDown(sender, e);
        private void LabelInt_MouseMove(object sender, MouseEventArgs e) => Utils.LabelInt_MouseMove(sender, e);
        private void LabelInt_MouseUp(object sender, MouseEventArgs e) => Utils.LabelInt_MouseUp(sender, e);
        private void TextBoxInt_TextChanged(object sender, EventArgs e) => Utils.TextBoxInt_TextChanged(sender, e);
        private void TextBoxInt_KeyPress(object sender, KeyPressEventArgs e) => Utils.TextBoxInt_KeyPress(sender, e);
        private void TextBoxInt_Leave(object sender, EventArgs e) => Utils.TextBoxInt_Leave(sender, e);

        private void LabelUInt_MouseDown(object sender, MouseEventArgs e) => Utils.LabelUInt_MouseDown(sender, e);
        private void LabelUInt_MouseMove(object sender, MouseEventArgs e) => Utils.LabelUInt_MouseMove(sender, e);
        private void LabelUInt_MouseUp(object sender, MouseEventArgs e) => Utils.LabelUInt_MouseUp(sender, e);
        private void TextBoxUInt_TextChanged(object sender, EventArgs e) => Utils.TextBoxUInt_TextChanged(sender, e);
        private void TextBoxUInt_KeyPress(object sender, KeyPressEventArgs e) => Utils.TextBoxUInt_KeyPress(sender, e);
        private void TextBoxFloatBar_TextChanged(object sender, EventArgs e) => Utils.TextBoxFloatBar_TextChanged(sender, e);
        private void TextBoxFloatBar_KeyPress(object sender, KeyPressEventArgs e) => Utils.TextBoxFloatBar_KeyPress(sender, e);
        #endregion

        #region 菜单栏
        private void MenuItem_WordDictTool_Click(object sender, EventArgs e)
        {
            if (_wordDictTool == null)
                _wordDictTool = new WordDictTool.MainForm();
            if (_wordDictTool.Visible)
                _wordDictTool.Focus();
            else
                _wordDictTool.Show();
        }
        private void MenuItem_JumpLink_Click(object sender, EventArgs e)
        {
            var linkLabel = (ToolStripMenuItem)sender;
            var url = (string)linkLabel.Tag;
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        private void MenuItem_About_Click(object sender, EventArgs e)
        {
            AboutFrom aboutFrom = new AboutFrom();
            aboutFrom.ShowDialog();
        }
        #endregion
    }
}