using ICSharpCode.NRefactory.TypeSystem;
using OPTestTool.Extension;
using OPTestTool.Properties;
using OPTestTool.View.Navigate;
using ScriptTestTools.Entity;
using ScriptTestTools.Model;
using ScriptTestTools.StaticData;
using ScriptTestTools.View.Setting;
using ScriptTestTools.View.TestPicColor;
using System.Collections.Concurrent;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

//using WindowFinder;
using Timer = System.Windows.Forms.Timer;

namespace OPTestTool
{
    public partial class MainForm : Form
    {
        public bool IsBindWindow { get; set; }
        public int BindWindowHwnd { get; set; }

        private bool ResSuccess = false;//注册成功
        private int GHwnd = 0;                              //全局窗口句柄
        private FrmDraw frmDraw = null;
        private string appPath = AppDomain.CurrentDomain.BaseDirectory;

        private OpSoft opMouse = new OpSoft();
        private TreeNodeHelper treeNodeHelper = null;
        public GlobalSetting globalSetting = null;
        private FrmBindWindowHelper frmBindWindowHelper = null;
        private GlobalSettingService globalSettingService = null;
        private FrmTestKeypadService frmTestKeypadService = new FrmTestKeypadService();
        private FrmTestScript frmTestScript = new FrmTestScript();
        private FrmBindHistoryService frmBindHistoryService = null;
        private List<FrmBindHistory> frmBindHistories = new List<FrmBindHistory>();
        private Timer _timerTick;
        private readonly ConcurrentQueue<string> _logQueue = new ConcurrentQueue<string>();
        private static bool _showLogTime;
        private OpSoft opSoft = new OpSoft();
        private GetScreenDataBmpForm _getScreenDataBmpForm;

        public MainForm()
        {
            InitializeComponent();
            Logger.LogMessageReceived += Logger_LogMessageReceived;

            PicColor.FastSetFindPicAndFindPicExValueEvent += (e, h) =>
            {
                //处理快速填充参数
                this.txtTestPicColorFindPicPointPlus.Text = e.ToString();
            };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            StartupConfig();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShutdownConfig();
        }

        #region 启动配置&退出配置

        private void StartupConfig()
        {
            opSoft.SetShowErrorMsg(0);
            _timerTick = new Timer();
            _timerTick.Interval = 20;
            _timerTick.Tick += EventTimer_Tick;
            _timerTick.Start();

            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            globalSettingService = new GlobalSettingService(appPath + ConfigurationManager.AppSettings["GlobalSetting"].ToString());
            globalSetting = globalSettingService.ReadSetting(); //全局配置读取
            frmBindHistoryService = new FrmBindHistoryService(appPath + ConfigurationManager.AppSettings["BindHistory"].ToString());
            frmBindHistories = frmBindHistoryService.ReadSetting();//读取历史绑定记录
                                                                   //this.dgvBindHistory.Update(frmBindHistories);

            //综合设置
            Set.SetPath = globalSetting.SetPath;
            if (!string.IsNullOrWhiteSpace(Set.SetPath))
                opSoft.SetPath(Set.SetPath);
            else
                opSoft.SetPath(appPath + "Data");
            //this.cbSetClearHide.Checked = GlobalSetting.ClearHide;
            //this.cbLogShowTime.Checked = GlobalSetting.TimeInfo;
            //this.cbLogAutoRoll.Checked = GlobalSetting.AutoRoll;
            treeNodeHelper = new TreeNodeHelper(this.TreeView_Window, opSoft);
            //绑定设置
            //this.cbBindWindowMoveWindow.Checked = GlobalSetting.BindMoveWindow;
            //this.cbBindWindowSuccessTips.Checked = GlobalSetting.BindSuccessTips;
            //this.txtPlugObj.Text = GlobalSetting.PlugObj;
            //测试图色
            //this.cbTestPicColorEnableGetColorByCapture.Checked = GlobalSetting.EnableGetColorByCapture.ToBool();
            //if (GlobalSetting.EnablePicCacheOpenCache)
            //    this.rbTestPicColorFindPicPointOpenCache.Checked = true;
            //else
            //    this.rbTestPicColorFindPicPointCloseCache.Checked = true;

            //this.cbTestPicColorAutoGetLanPoint.Checked = globalSetting.AutoGetLanPoint;
            string[] ReadText = globalSetting.CustomCodeIfContent.Replace("\\n", "@").Split('@');
            globalSetting.CustomCodeIfContent = string.Join($"\n", ReadText);

            //测试鼠标
            //this.cbTestMouseAfterMoveTo.Checked = GlobalSetting.TestMouseAfterMoveOperation;
            //this.cbTestMouseBeforeActive.Checked = GlobalSetting.TestMouseBeforeOperationActive;
            //测试键盘
            //this.cbTestKeypadBeforeOperateActive.Checked = GlobalSetting.TestKeyboardBeforeOperationActive;
            //脚本测试
            //if (File.Exists(GlobalSetting.TestScriptScriptPath))
            //{
            //    this.txtTestScriptPath.Text = GlobalSetting.TestScriptScriptPath;
            //    editor.Text = File.ReadAllText(GlobalSetting.TestScriptScriptPath);
            //}
            ComboBox_MouseAction.SelectedIndex = 0;
            ComboBox_CodeLang.SelectedIndex = 0;
            AllTextBoxMouseDoubleClickSubScribe();
        }

        private void ShutdownConfig()
        {
            _timerTick.Stop();

            globalSettingService.WriteSetting(globalSetting);       //综合设置配置写出
            frmBindHistoryService.WriteSetting(frmBindHistories);   //历史绑定配置写出
            if (opSoft != null) opSoft.Dispose();
            if (opMouse != null) opMouse.Dispose();
        }

        #endregion 启动配置&退出配置

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
                LogType logType = (LogType)
                    Enum.Parse(typeof(LogType), result.Substring(0, splitIndex));
                string context = result.Substring(splitIndex + 1);
                switch (logType)
                {
                    case LogType.Log:
                    default:
                        //TxtBox_Log.SelectionColor = Color.Black;
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

        #endregion 日志

        #region 综合设置

        private void Finder_Window_WindowHandleChanged(object sender, EventArgs e)
        {
            //var finder = (WindowFinder.WindowFinder)sender;
            //Txt_WindowClassName.Text = finder.WindowClass;
            //Txt_WindowHwnd.Text = finder.WindowHandle.ToString();
            //Txt_WindowTitle.Text = finder.WindowText;
            //Txt_WindowPID.Text = finder.WindowPID.ToString();

            //IntPtr newWnd = finder.WindowHandle;
            //IntPtr rootWnd = newWnd;
            //do
            //{
            //    newWnd = Win32.GetParent(newWnd);
            //    if (newWnd == IntPtr.Zero)
            //        break;
            //    rootWnd = newWnd;
            //} while (true);
            //TreeView_Window.Nodes.Clear();
            //var rootNode = new TreeNode(GetWindowInfo(rootWnd));
            //rootNode.Tag = rootWnd;
            //TreeView_Window.Nodes.Add(rootNode);
            //ApplayIcon(rootNode);
            //AddChildWindow(rootNode, finder.WindowHandle);
            //rootNode.Expand();
            //if (rootWnd == finder.WindowHandle)
            //    TreeView_Window.SelectedNode = rootNode;
        }

        private void TreeView_Window_NodeMouseDoubleClick(
            object sender,
            TreeNodeMouseClickEventArgs e
        )
        {
            var hWnd = (IntPtr)e.Node.Tag;
            //Win32.HighlightWindow(hWnd);
        }

        private void TreeView_Window_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //var hWnd = (IntPtr)e.Node.Tag;
            //Txt_WindowClassName.Text = Win32.GetClassName(hWnd);
            //Txt_WindowHwnd.Text = hWnd.ToString();
            //Txt_WindowTitle.Text = Win32.GetWindowText(hWnd);
            //Txt_WindowPID.Text = Win32.GetWindowPID(hWnd).ToString();
            UpdateUISetTitleClassHwndPID(FrmSettingHelper.GetHwndWhereParsTreeViewText(e.Node.Text), false);
        }

        private void Btn_AllWindow_Click(object sender, EventArgs e)
        {
            treeNodeHelper.TreeViewAdd();
        }

        private void Btn_SetPath_Click(object sender, EventArgs e)
        {
            using (FrmSettingSetPath frmSettingSetPath = new FrmSettingSetPath())
            {
                if (frmSettingSetPath.ShowDialog() == DialogResult.OK)
                {
                    globalSetting.SetPath = Set.SetPath;
                    opSoft.SetPath(Set.SetPath);
                }
            }
        }

        private void Btn_GetCommandLine_Click(object sender, EventArgs e)
        {
            Logger.Log(">>>GetCommandLine\n" + string.Join(" ", Environment.GetCommandLineArgs()));
        }

        private void picBoxCursor_MouseDown(object sender, MouseEventArgs e)
        {
            picBoxCursor.Image = Resources.CursorNotAll;
            SetCursor(Resources.Cursor, new Point(0, 0));
            this.timerSetGetHwnd.Enabled = true;
            frmDraw = new FrmDraw();
        }

        private void picBoxCursor_MouseUp(object sender, MouseEventArgs e)
        {
            picBoxCursor.Image = Resources.CursorAll;
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.timerSetGetHwnd.Enabled = false;
            if (frmDraw != null)
                frmDraw.CloseWindow();
        }

        private void timerSetGetHwnd_Tick(object sender, EventArgs e)
        {
            int hwndTemp = opSoft.GetMousePointWindow();
            if (GHwnd != 0 & opSoft.GetWindowProcessId(this.Handle.ToInt32()) != opSoft.GetWindowProcessId(hwndTemp))
            {
                if (hwndTemp != GHwnd)
                {
                    GHwnd = hwndTemp;
                    UpdateUISetTitleClassHwndPID(GHwnd);
                    UpdateUISetFrmDraw(GHwnd);
                }
            }
            else
                GHwnd = hwndTemp;
        }

        #endregion 综合设置

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
            Debug.WriteLine(opSoft.IsBind());
            if (opSoft.IsBind() != 0)
            {
                Logger.Log(">>>UnBindWindow");
                int reault = opSoft.UnBindWindow();
                IsBindWindow = false;
                BindWindowHwnd = 0;
                Logger.Log("返回值：" + reault);
            }
            else
            {
                var nHwnd = string.IsNullOrEmpty(Txt_WindowHwnd.Text)
                    ? 0
                    : int.Parse(Txt_WindowHwnd.Text);
                if (CheckBox_BindMoveWendow.Checked)
                    opSoft.MoveWindow(nHwnd, -10, 0); //OP发送的SetWindowPos(-10,0,W,H,28)  大漠的是SetWindowPos(-10,0,0,0,21)
                Logger.Log(
                    string.Format(
                        ">>>BindWindow\n{0},\"{1}\",\"{2}\",\"{3}\",{4}",
                        nHwnd,
                        Txt_BindDisplayMode.Text,
                        Txt_BindMouseMode.Text,
                        Txt_BindKeypadMode.Text,
                        Txt_BindMode.Text
                    )
                );
                int reault = opSoft.BindWindow(
                    nHwnd,
                    Txt_BindDisplayMode.Text,
                    Txt_BindMouseMode.Text,
                    Txt_BindKeypadMode.Text,
                    int.Parse(Txt_BindMode.Text)
                );
                Logger.Log("返回值：" + reault);
                if (CheckBox_BindMessage.Checked && reault == 1)
                {
                    IsBindWindow = true;
                    BindWindowHwnd = nHwnd;
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
            string bindCode = string.Format(
                "op_ret = op.BindWindow(hwnd,\"{1}\",\"{2}\",\"{3}\",{4})",
                Txt_WindowHwnd.Text,
                Txt_BindDisplayMode.Text,
                Txt_BindMouseMode.Text,
                Txt_BindKeypadMode.Text,
                Txt_BindMode.Text
            );
            Logger.Log(string.Format(">>>SetClipboard \"{0}\"", bindCode));
            opSoft.SetClipboard(bindCode);
            MessageBox.Show("代码已经复制到剪贴板,直接粘贴即可使用！");
        }

        #endregion 绑定参数

        #region 测试图色

        private void Btn_Capture_Click(object sender, EventArgs e)
        {
            PicColor.x1 = int.Parse(Txt_CaptureX1.Text);
            PicColor.y1 = int.Parse(Txt_CaptureY1.Text);
            PicColor.x2 = int.Parse(Txt_CaptureX2.Text);
            PicColor.y2 = int.Parse(Txt_CaptureY2.Text);

            if (PicColor.frmTestPicColorShowPic == null)
            {
                PicColor.frmTestPicColorShowPic = new FrmTestPicColorShowPic(opSoft);
            }
            PicColor.frmTestPicColorShowPic.UpdateImage();
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
            //var finder = (WindowFinder.LocationFinder)sender;
            //int x = finder.MousePos.X,
            //    y = finder.MousePos.Y;
            //if (opSoft.IsBind() == 1 && opSoft.GetBindWindow() != (int)Win32.GetDesktopWindow())
            //    opSoft.ScreenToClient(opSoft.GetBindWindow(), ref x, ref y);
            //Txt_GetColorX.Text = x.ToString();
            //Txt_GetColorY.Text = y.ToString();
        }

        private void Btn_FindPic_Click(object sender, EventArgs e)
        {
            try
            {
                int x1 = GetFindRangeAndPicFile().Item1;
                int y1 = GetFindRangeAndPicFile().Item2;
                int x2 = GetFindRangeAndPicFile().Item3;
                int y2 = GetFindRangeAndPicFile().Item4;
                float sim = float.Parse(Txt_FindPicSim.Text);
                int dir = int.Parse(Txt_FindPicDir.Text);

                #region 自定义代码

                var elseCustomCode = globalSetting.CustomCodeElseContent;
                if (globalSetting.CustomCodeElse)
                    elseCustomCode = $"else\n{{\n{elseCustomCode}\n}}";
                if (globalSetting.CustomCodeIf)
                {
                    $"\nif({GetPlusMethod("FindPic", $@"{x1},{y1},{x2},{y2},""{GetFindRangeAndPicFile().Item5}"",""{Txt_FindPicDelta.Text}"",{sim},{dir},out int outX,out int outY").Replace(";", "")}!=-1)\n{{\n {globalSetting.CustomCodeIfContent}\n}}\n{elseCustomCode}".ToLog();
                }
                else
                {
                    GetPlusMethod("FindPic", $@"{x1},{y1},{x2},{y2},""{GetFindRangeAndPicFile().Item5}"",""{Txt_FindPicDelta.Text}"",{sim},{dir},out int outX,out int outY").ToLog();
                }

                #endregion 自定义代码

                //Logger.Log(
                //    string.Format(">>>FindPic\n{0},{1},{2},{3},\"{4}\",\"{5}\",{6},{7},x,y", x1, y1, x2, y2, GetFindRangeAndPicFile().Item5, Txt_FindPicDelta.Text, sim, dir)
                //);
                int reault = opSoft.FindPic(x1, y1, x2, y2, GetFindRangeAndPicFile().Item5, Txt_FindPicDelta.Text, sim, dir, out var x, out var y);
                Logger.Log("返回值：" + reault + ",x=" + x + ",y=" + y);
                ShowDrawTarget(GetFindRangeAndPicFile().Item5.Split('|')[reault], x, y);
            }
            catch (Exception ex)
            {
                ex.Message.ToLog();
                ex.StackTrace.ToLog();
            }
        }

        private void Btn_FindPicEx_Click(object sender, EventArgs e)
        {
            try
            {
                int x1 = GetFindRangeAndPicFile().Item1;
                int y1 = GetFindRangeAndPicFile().Item2;
                int x2 = GetFindRangeAndPicFile().Item3;
                int y2 = GetFindRangeAndPicFile().Item4;
                float sim = float.Parse(Txt_FindPicSim.Text);
                int dir = int.Parse(Txt_FindPicDir.Text);

                string reault = opSoft.FindPicEx(x1, y1, x2, y2, GetFindRangeAndPicFile().Item5, Txt_FindPicDelta.Text, sim, dir);
                Logger.Log("返回值：" + reault);

                var findSuccess = reault.Split('|').ToList();
                int count = findSuccess.Count;
                if (findSuccess.Count > 20)
                {
                    findSuccess = findSuccess.Take(20).ToList();
                }
                findSuccess.ForEach(fe =>
                {
                    var resIndexPoint = fe.Split(',');
                    var pname = GetFindRangeAndPicFile().Item5.Split('|')[resIndexPoint[0].ToInt32()];

                    $"找到图片序号:{resIndexPoint[0]},图片名:{pname},坐标:{resIndexPoint[1]},{resIndexPoint[2]}".ToLog();
                    ShowDrawTarget(pname, resIndexPoint[1].ToInt32(), resIndexPoint[2].ToInt32());
                });

                #region 自定义代码

                var elseCustomCode = globalSetting.CustomCodeElseContent;
                if (globalSetting.CustomCodeElse)
                    elseCustomCode = $"else\n{{\n{elseCustomCode}\n}}";
                if (globalSetting.CustomCodeIf)
                {
                    $"\nif({GetPlusMethod("FindPicEx", $@"{x1},{y1},{x2},{y2},""{GetFindRangeAndPicFile().Item5}"",""{Txt_FindPicDelta.Text}"",{sim},{dir},out int outX,out int outY").Replace(";", "")}!=-1)\n{{\n {globalSetting.CustomCodeIfContent}\n}}\n{elseCustomCode}".ToLog();
                }
                else
                {
                    GetPlusMethod("FindPicEx", $@"{x1},{y1},{x2},{y2},""{GetFindRangeAndPicFile().Item5}"",""{Txt_FindPicDelta.Text}"",{sim},{dir},out int outX,out int outY").ToLog();
                }

                #endregion 自定义代码
            }
            catch (Exception ex)
            {
                ex.Message.ToLog();
                ex.StackTrace.ToLog();
            }
        }

        private string GetPlusMethod(string methodName, string param = "")
        {
            return $"{globalSetting.PlugObj}.{methodName}({param});";
        }

        private Tuple<int, int, int, int, string> GetFindRangeAndPicFile()
        {
            //判断 txtTestPicColorFindPicPointPlus 是否有值,且是有效的值
            int margin = txtTestPicColorFindPicPointMargin.Text.Trim().ToInt32();
            int x1 = int.Parse(Txt_FindPicX1.Text);
            int y1 = int.Parse(Txt_FindPicY1.Text);
            int x2 = int.Parse(Txt_FindPicX2.Text);
            int y2 = int.Parse(Txt_FindPicY2.Text);
            string picFile = Txt_FindPicFile.Text.Trim();
            if (txtTestPicColorFindPicPointPlus.Text.Length > 0)
            {
                string points = txtTestPicColorFindPicPointPlus.Text;
                if (points.Contains("宽高"))
                    points = points.Replace(",宽", "|").Split('|')[0];
                var pointsArray = points.Split(',');
                if (pointsArray.Count() == 4)
                {
                    x1 = pointsArray[0].ToInt32();
                    y1 = pointsArray[1].ToInt32();
                    x2 = pointsArray[2].ToInt32();
                    y2 = pointsArray[3].ToInt32();
                }
                else
                {
                    txtTestPicColorFindPicPointPlus.Text = "";
                    "警告:快速填参数据不符".ToLog();
                }
            }
            //支持不带bmp后缀
            if (picFile.Contains('|') & !picFile.Contains(".bmp"))
            {
                List<string> files = new List<string>();
                foreach (var file in picFile.Split('|'))
                {
                    if (!picFile.Contains(".bmp"))
                        files.Add(file + ".bmp");
                }
                picFile = string.Join("|", files);
            }
            else
            {
                if (!picFile.Contains(".bmp"))
                {
                    picFile += ".bmp";
                }
            }

            //通配符
            if (picFile.Contains("?") | picFile.Contains("*"))
                picFile = opSoft.MatchPicName(picFile);
            x1 = x1 == 0 ? 0 : x1 - margin; y1 = y1 == 0 ? 0 : y1 - margin;
            x2 = x2 + margin; y2 = y2 + margin;
            ShowMargin(x1, y1, x2 - x1, y2 - y1);
            return Tuple.Create(x1, y1, x2, y2, picFile);
        }

        private void ShowDrawTarget(string picName, int x, int y)
        {
            Bitmap bitmap = GetBitmap(picName);
            if (bitmap == null)
                return;
            FrmFindTargetDraw frmFindTargetDraw = new FrmFindTargetDraw();
            frmFindTargetDraw.Show();
            opSoft.GetClientRect(BindWindow.Hwnd, out int xr1, out int yr1, out int xr2, out int yr2);
            var draw = new Thread(() =>
            {
                frmFindTargetDraw.SetWindowSize(bitmap.Width, bitmap.Height, Color.Red);
                opSoft.MoveWindow(frmFindTargetDraw.GetWindowHwnd(), xr1 + x - 3, yr1 + y - 3);
                Thread.Sleep(4000);
                frmFindTargetDraw.CloseWindow();
            });
            draw.IsBackground = true;
            draw.Start();
        }

        private Bitmap GetBitmap(string picName)
        {
            string getPath = null;
            getPath = picName;
            if (!picName.Contains(":"))
            {
                string path = opSoft.GetPath();
                if (!path.EndsWith("\\"))
                {
                    path += "\\";
                }
                getPath = path + picName;
            }
            if (!File.Exists(getPath))
                return null;
            return new Bitmap(getPath);
        }

        private void ShowMargin(int x, int y, int marSizeW, int marSizeH)
        {
            FrmFindTargetDraw frmFindTargetDraw = new FrmFindTargetDraw();
            frmFindTargetDraw.Show();
            opSoft.GetClientRect(BindWindow.Hwnd, out int xr1, out int yr1, out int xr2, out int yr2);
            var draw = new Thread(() =>
            {
                frmFindTargetDraw.SetWindowSize(marSizeW, marSizeH, Color.FromArgb(5, 82, 62));
                opSoft.MoveWindow(frmFindTargetDraw.GetWindowHwnd(), xr1 + x - 3, yr1 + y - 3);
                Thread.Sleep(4000);
                frmFindTargetDraw.CloseWindow();
            });
            draw.IsBackground = true;
            draw.Start();
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

        private void pbTestPicColorCursor_MouseDown(object sender, MouseEventArgs e)
        {
            pbTestPicColorCursor.Image = Resources.CursorNotAll;
            pbTestPicColorCursor.Cursor = System.Windows.Forms.Cursors.Cross;
            timerTestPicColorGetPoint.Enabled = true;
        }

        private void pbTestPicColorCursor_MouseUp(object sender, MouseEventArgs e)
        {
            pbTestPicColorCursor.Image = Resources.CursorAll1;
            pbTestPicColorCursor.Cursor = System.Windows.Forms.Cursors.Arrow;
            timerTestPicColorGetPoint.Enabled = false;
        }

        private void timerTestPicColorGetPoint_Tick(object sender, EventArgs e)
        {
            var point = GetMousePoint();
            this.Txt_GetColorX.Text = point.Item1;
            this.Txt_GetColorY.Text = point.Item2;
        }

        private void btnTestPicColorFindPicFindPicExCustomCode_Click(object sender, EventArgs e)
        {
            try
            {
                InitCustomCode();
                if (ShowDialogCreateWindow(new FrmTestPicColorFindPicAndFindPicExCustomCode()))
                {
                    globalSetting.CustomCodeIf = ScriptTestTools.StaticData.PicColor.CustomCodeIf;
                    globalSetting.CustomCodeIfContent = ScriptTestTools.StaticData.PicColor.CustomCodeIfContent;
                    globalSetting.CustomCodeElse = ScriptTestTools.StaticData.PicColor.CustomCodeElse;
                    globalSetting.CustomCodeElseContent = ScriptTestTools.StaticData.PicColor.CustomCodeElseContent;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ToLog();
            }
        }

        private void InitCustomCode()
        {
            ScriptTestTools.StaticData.PicColor.CustomCodeIf = globalSetting.CustomCodeIf;
            ScriptTestTools.StaticData.PicColor.CustomCodeIfContent = globalSetting.CustomCodeIfContent;
            ScriptTestTools.StaticData.PicColor.CustomCodeElse = globalSetting.CustomCodeElse;
            ScriptTestTools.StaticData.PicColor.CustomCodeElseContent = globalSetting.CustomCodeElseContent;
        }

        #endregion 测试图色

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
                    case "LeftClick":
                        reault = opSoft.LeftClick();
                        break;

                    case "RightClick":
                        reault = opSoft.RightClick();
                        break;

                    case "MiddleClick":
                        reault = opSoft.MiddleClick();
                        break;

                    case "LeftDoubleClick":
                        reault = opSoft.LeftDoubleClick();
                        break;

                    case "WheelUp":
                        reault = opSoft.WheelUp();
                        break;

                    case "WheelDown":
                        reault = opSoft.WheelDown();
                        break;

                    default:
                        break;
                }
                Logger.Log("返回值：" + reault);
            }
        }

        private void Finder_MoveTo_MouseChangePos(object sender, EventArgs e)
        {
            //var finder = (WindowFinder.LocationFinder)sender;
            //int x = finder.MousePos.X,
            //    y = finder.MousePos.Y;
            //if (opSoft.IsBind() == 1 && opSoft.GetBindWindow() != (int)Win32.GetDesktopWindow())
            //    opSoft.ScreenToClient(opSoft.GetBindWindow(), ref x, ref y);
            //Txt_MoveToX.Text = x.ToString();
            //Txt_MoveToY.Text = y.ToString();
        }

        private void Btn_MoveR_Click(object sender, EventArgs e)
        {
            if (CheckBox_MousePreActionWindow.Checked)
                opSoft.SetWindowState(opSoft.GetBindWindow(), 1);
            if (txtTestMouseMoveRSX.Text.Length > 0 & txtTestMouseMoveRSY.Text.Length > 0)
            {
                opSoft.MoveTo(txtTestMouseMoveRSX.Text.Trim().ToInt32(), txtTestMouseMoveRSY.Text.Trim().ToInt32());
                opSoft.Delay(300);
            }
            int x = int.Parse(Txt_MoveRX.Text);
            int y = int.Parse(Txt_MoveRY.Text);
            Logger.Log(string.Format(">>>MoveR {0},{1}", x, y));
            int reault = opSoft.MoveR(x, y);//BUG
            //int reault = opSoft.MoveTo(x + txtTestMouseMoveRSX.Text.Trim().ToInt32(), y + txtTestMouseMoveRSY.Text.Trim().ToInt32());
            Logger.Log("返回值：" + reault);
            if (reault == 1 && CheckBox_MoveAndSend.Checked)
            {
                Logger.Log(string.Format(">>>{0}", ComboBox_MouseAction.Text));
                switch (ComboBox_MouseAction.Text)
                {
                    case "LeftClick":
                        reault = opSoft.LeftClick();
                        break;

                    case "RightClick":
                        reault = opSoft.RightClick();
                        break;

                    case "MiddleClick":
                        reault = opSoft.MiddleClick();
                        break;

                    case "LeftDoubleClick":
                        reault = opSoft.LeftDoubleClick();
                        break;

                    case "WheelUp":
                        reault = opSoft.WheelUp();
                        break;

                    case "WheelDown":
                        reault = opSoft.WheelDown();
                        break;

                    default:
                        break;
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

        private void pbTestMouseCursor_MouseDown(object sender, MouseEventArgs e)
        {
            pbTestMouseCursor.Image = Resources.CursorNotAll;
            pbTestMouseCursor.Cursor = Cursors.Cross;
            timeTestMouseGetPoint.Enabled = true;
        }

        private void pbTestMouseCursor_MouseUp(object sender, MouseEventArgs e)
        {
            pbTestMouseCursor.Image = Resources.CursorAll1;
            pbTestMouseCursor.Cursor = Cursors.Arrow;
            timeTestMouseGetPoint.Enabled = false;
        }

        private void timeTestMouseGetPoint_Tick(object sender, EventArgs e)
        {
            var point = GetMousePoint();
            this.Txt_MoveToX.Text = point.Item1;
            this.Txt_MoveToY.Text = point.Item2;
            this.txtTestMouseMoveToXY.Text = point.Item1 + "," + point.Item2;
        }

        private void btnTestMouseAnchor_Click(object sender, EventArgs e)
        {
            this.txtTestMouseMoveRSX.Text = this.Txt_MoveToX.Text;
            this.txtTestMouseMoveRSY.Text = this.Txt_MoveToY.Text;
        }

        private void btnTestMouseAnchorCalculate_Click(object sender, EventArgs e)
        {
            var rsx = this.txtTestMouseMoveRSX.Text.ToInt32();
            var rsy = this.txtTestMouseMoveRSY.Text.ToInt32();
            var tox = this.Txt_MoveToX.Text.ToInt32();
            var toy = this.Txt_MoveToY.Text.ToInt32();
            string rrx;
            string rry;
            if ((rsx - tox).ToString().Contains("-"))
            {
                rrx = (rsx - tox).ToString().Replace("-", "");
            }
            else
            {
                rrx = $"-{rsx - tox}";
            }
            if ((rsy - toy).ToString().Contains("-"))
            {
                rry = (rsy - toy).ToString().Replace("-", "");
            }
            else
            {
                rry = $"-{rsy - toy}";
            }
            this.Txt_MoveRX.Text = rrx;
            this.Txt_MoveRY.Text = rry;
        }

        #endregion 测试鼠标

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

        #endregion 测试键盘

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

        #endregion 文本输入

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
        { }

        private void Btn_ReadDouble_Click(object sender, EventArgs e)
        { }

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
            Logger.Log(
                string.Format(
                    ">>>ReadData {0},\"{1}\",{2}",
                    opSoft.GetBindWindow(),
                    Txt_ReadAddress.Text,
                    length
                )
            );
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
            Logger.Log(
                string.Format(
                    ">>>WriteData {0},\"{1}\",{2},{3}",
                    opSoft.GetBindWindow(),
                    Txt_ReadAddress.Text,
                    context,
                    context.Length
                )
            );
            int reault = opSoft.WriteData(
                opSoft.GetBindWindow(),
                Txt_ReadAddress.Text,
                context,
                context.Length
            );
            Logger.Log("返回值：" + reault);
        }

        #endregion 内存汇编

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
                if (
                    !File.Exists(tempLibopFile)
                    || File.ReadAllText(tempLibopFile) != Resources.libop
                )
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
            string args = string.Format(
                "{0} -lang {1} -doc {2}",
                opFolder,
                ComboBox_CodeLang.Text,
                CheckBox_AddWifiDoc.Checked
            );
            Process process = new Process();
            process.StartInfo.FileName = exeFile;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
            if (CheckBox_OpenGenCodeFolder.Checked)
                Process.Start(
                    new ProcessStartInfo(Path.GetDirectoryName(exeFile)) { UseShellExecute = true }
                );

            //对外部工程进行更改
            if (
                CheckBox_UseOutProject.Checked
                && ComboBox_CodeLang.Text.Equals("op", StringComparison.OrdinalIgnoreCase)
            )
            {
                string folder = Path.Combine(Path.GetDirectoryName(exeFile), "OP");
                string srcHeader = Path.Combine(folder, "libopExport.h");
                string srcSource = Path.Combine(folder, "libopExport.cpp");
                if (!File.Exists(srcHeader) || !File.Exists(srcSource))
                    return;
                string desHeader = Path.Combine(opFolder, "libop/libopExport.h");
                string desSource = Path.Combine(opFolder, "libop/libopExport.cpp");
                if (
                    !File.Exists(desHeader)
                    || File.ReadAllText(desHeader) != File.ReadAllText(srcHeader)
                )
                    File.Copy(srcHeader, desHeader, true);
                if (
                    !File.Exists(desSource)
                    || File.ReadAllText(desSource) != File.ReadAllText(srcSource)
                )
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

        #endregion OPExport

        #region 工具栏

        private void ToolbarClick(object sender, EventArgs e)
        {
            try
            {
                ToolbarClickService(sender.ToString());
            }
            catch (Exception ex)
            {
                ex.Message.ToLog();
            }
        }

        private async void ToolbarClickService(string toolName)
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            switch (toolName)
            {
                case "当前程序目录":

                    Process.Start(new ProcessStartInfo(appPath) { UseShellExecute = true });
                    break;

                case "脚本存放目录":
                    Process.Start(new ProcessStartInfo(appPath + "Script\\") { UseShellExecute = true });
                    break;

                case "大漠综合工具":
                    Process.Start(appPath + "Navigate\\Tools\\大漠\\大漠综合工具.exe");
                    break;

                case "大漠偏色计算器":
                    Process.Start(appPath + "Navigate\\Tools\\大漠\\大漠偏色计算器.exe");
                    break;

                case "大漠插件接口说明":
                    var psi = new ProcessStartInfo(appPath + "Navigate\\Helper\\大漠插件接口说明.chm")
                    {
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                    break;

                case "Op插件接口说明":
                    Process.Start(appPath + "Navigate\\Helper\\op-wiki-离线文档0.45.exe");
                    break;

                case "当前SetPath目录":
                    Process.Start(new ProcessStartInfo(opSoft.GetPath()) { UseShellExecute = true });
                    break;

                case "乐玩编程助手":
                    Process.Start(appPath + "Navigate\\Tools\\乐玩\\乐玩助手.exe");
                    break;

                case "精易编程助手":
                    await NavigateOpenToolsExe(appPath + "Navigate\\Tools\\精易\\精易编程助手.exe");
                    break;

                case "关于":
                    using (FrmAbout frmAbout = new FrmAbout())
                    {
                        frmAbout.ShowDialog();
                    }
                    break;

                default:
                    break;
            }
        }

        private async Task NavigateOpenToolsExe(string path)
        {
            //注：exe名需要和zip名一直（dm.exe  dm.zip)
            //没找到dm.exe的情况就会去解压dm.zip这个包

            //判断exe是否存在
            if (File.Exists(path))
            {
                Process.Start(path);
                return;
            }
            //判断解压包是否存在
            var zipPath = path.Replace(".exe", ".zip");
            if (File.Exists(zipPath))
            {
                var fileDocument = Path.GetDirectoryName(zipPath);
                using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(zipPath))
                {
                    //踩雷：在解压的时候出现乱码问题，那就是手动压缩包的时候有问题
                    zip.ExtractAll(fileDocument);
                }
                for (int i = 0; i < 10; i++)
                {
                    "解压中,请等待...".ToLog();
                    await Task.Delay(1000);
                    if (File.Exists(path))
                    {
                        "解压完毕,欢迎使用".ToLog();
                        Process.Start(path);
                        break;
                    }
                }
            }
        }

        [DllImport("shell32.dll", SetLastError = true)]
        private static extern int ShellExecute(IntPtr hwnd, string lpVerb, string lpFile, string lpParameters, string lpDirectory, int nShowWindow);

        public static void OpenCHMFile(string filePath)
        {
            ShellExecute(IntPtr.Zero, "open", filePath, null, null, 0);
        }

        #endregion 工具栏

        #region 自定义方法

        /// <summary>
        /// 设置鼠标指针
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="hotPoint"></param>
        public void SetCursor(Bitmap cursor, Point hotPoint)
        {
            int hotX = hotPoint.X;
            int hotY = hotPoint.Y;
            Bitmap myNewCursor = new Bitmap(cursor.Width * 2 - hotX, cursor.Height * 2 - hotY);
            Graphics g = Graphics.FromImage(myNewCursor);
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.DrawImage(cursor, cursor.Width - hotX, cursor.Height - hotY, cursor.Width,
            cursor.Height);

            this.Cursor = new Cursor(myNewCursor.GetHicon());

            g.Dispose();
            myNewCursor.Dispose();
        }

        /// <summary>
        /// 更新UI-综合设置标题、类名、窗口句柄、PID
        /// </summary>
        /// <param name="hwndc">窗口句柄</param>
        private void UpdateUISetTitleClassHwndPID(int hwndc, bool treeViewUpdate = true)
        {
            this.Txt_WindowHwnd.Text = hwndc.ToString();
            this.Txt_WindowPID.Text = opSoft.GetWindowProcessId(hwndc).ToString();
            this.Txt_WindowTitle.Text = opSoft.GetWindowTitle(hwndc);//返回标题
            this.Txt_WindowClassName.Text = opSoft.GetWindowClass(hwndc);//返回Class
            if (treeViewUpdate)
                treeNodeHelper.TreeViewAdd(hwndc);
            //TreeViewUpdate(hwndc);
        }

        /// <summary>
        /// 更新FrmDraw的坐标位置、窗口大小
        /// </summary>
        /// <param name="hwndc">窗口句柄</param>
        private void UpdateUISetFrmDraw(int hwndc)
        {
            if (frmDraw != null)
            {
                int width, height, x1, y1, x2, y2;
                opSoft.GetClientSize(hwndc, out width, out height);
                opSoft.GetClientRect(hwndc, out x1, out y1, out x2, out y2);
                frmDraw.SetWindowSize(width, height);
                if (frmDraw.GetHwnd() != 0)
                {
                    opSoft.MoveWindow(frmDraw.GetHwnd(), x1, y1);
                }
            }
        }

        /// <summary>
        /// 显示对话框的方式创建窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="win"></param>
        /// <returns></returns>
        private bool ShowDialogCreateWindow<T>(T win)
        {
            var wf = win as Form;
            if (wf.ShowDialog() == DialogResult.OK)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取鼠标x y 位置
        /// </summary>
        /// <returns></returns>
        private Tuple<string, string> GetMousePoint()
        {
            int x, y;
            int rectX1, rectY1, rectX2, rectY2;
            opMouse.GetCursorPos(out x, out y);
            if (IsBindWindow)
            {
                opMouse.GetClientRect(BindWindowHwnd, out rectX1, out rectY1, out rectX2, out rectY2);
                x = x - rectX1;
                y = y - rectY1;
            }
            return new Tuple<string, string>(x.ToString(), y.ToString());
        }

        private void AllTextBoxMouseDoubleClickSubScribe()
        {
            foreach (TextBox textBox in tabControl1.GetAllTextBoxes())
            {
                if (textBox.Enabled)
                {
                    textBox.MouseDoubleClick += TextBoxMouseDoubleClickEvent;
                }
            }
        }

        private void TextBoxMouseDoubleClickEvent(object o, MouseEventArgs m)
        {
            if (o is TextBox)
            {
                TextBox t = (TextBox)o;
                if (t.Text.Length < 0)
                    return;
                opSoft.SetClipboard(t.Text);
                $">>>{t},已经复制到剪切板".ToLog();
            }
        }

        #endregion 自定义方法

        #region 控件-输入限制

        private void Txt_Path_DragEnter(object sender, DragEventArgs e) =>
            Utils.TextBoxFilePath_DragEnter(sender, e);

        private void Txt_Path_DragDrop(object sender, DragEventArgs e) =>
            Utils.TextBoxFilePath_DragDrop(sender, e);

        private void Txt_Folder_DragEnter(object sender, DragEventArgs e) =>
            Utils.TextBoxFileFolder_DragEnter(sender, e);

        private void Txt_Folder_DragDrop(object sender, DragEventArgs e) =>
            Utils.TextBoxFileFolder_DragDrop(sender, e);

        private void LabelInt_MouseDown(object sender, MouseEventArgs e) =>
            Utils.LabelInt_MouseDown(sender, e);

        private void LabelInt_MouseMove(object sender, MouseEventArgs e) =>
            Utils.LabelInt_MouseMove(sender, e);

        private void LabelInt_MouseUp(object sender, MouseEventArgs e) =>
            Utils.LabelInt_MouseUp(sender, e);

        private void TextBoxInt_TextChanged(object sender, EventArgs e) =>
            Utils.TextBoxInt_TextChanged(sender, e);

        private void TextBoxInt_KeyPress(object sender, KeyPressEventArgs e) =>
            Utils.TextBoxInt_KeyPress(sender, e);

        private void TextBoxInt_Leave(object sender, EventArgs e) =>
            Utils.TextBoxInt_Leave(sender, e);

        private void LabelUInt_MouseDown(object sender, MouseEventArgs e) =>
            Utils.LabelUInt_MouseDown(sender, e);

        private void LabelUInt_MouseMove(object sender, MouseEventArgs e) =>
            Utils.LabelUInt_MouseMove(sender, e);

        private void LabelUInt_MouseUp(object sender, MouseEventArgs e) =>
            Utils.LabelUInt_MouseUp(sender, e);

        private void TextBoxUInt_TextChanged(object sender, EventArgs e) =>
            Utils.TextBoxUInt_TextChanged(sender, e);

        private void TextBoxUInt_KeyPress(object sender, KeyPressEventArgs e) =>
            Utils.TextBoxUInt_KeyPress(sender, e);

        private void TextBoxFloatBar_TextChanged(object sender, EventArgs e) =>
            Utils.TextBoxFloatBar_TextChanged(sender, e);

        private void TextBoxFloatBar_KeyPress(object sender, KeyPressEventArgs e) =>
            Utils.TextBoxFloatBar_KeyPress(sender, e);

        #endregion 控件-输入限制
    }
}