using System.Diagnostics;
using System.Text;
using WordDictTool.Properties;

namespace WordDictTool
{
    public partial class MainForm : Form
    {
        private OpSoft _opSoft = new OpSoft();
        private const int DICT_FILE = 0;
        private WordDict _wordDict = new WordDict();
        private WordDict _tempDict = new WordDict();
        private WordDict _showDict;

        private const int COLOR_MAX = 20; //颜色信息条目上限
        private const string TEMP_FILE = "temp.bmp";
        private const string DEFAULT_DICT_FILE = "op.dict";
        private string TempFile => Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), TEMP_FILE);
        private string DictFile => Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), DEFAULT_DICT_FILE);
        public MainForm()
        {
            InitializeComponent();
            _wordDict.OnChange += Dict_OnChange;
            _tempDict.OnChange += Dict_OnChange;
            _showDict = _wordDict;
        
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            _opSoft.SetShowErrorMsg(0);
            var setting = Properties.Settings.Default;
            //初始化字库路径
            {
                string path = setting.Path;
                if (string.IsNullOrWhiteSpace(path))
                    path = DictFile;
                Txt_DictFile.Text = path;
            }

            //自动保存字库
            {
                var autoSaveDict = setting.AutoSaveDict;
                ToolStripMenuItem_AutoSaveDict.Checked = autoSaveDict;
                ToolStripMenuItem_AutoSaveDict.CheckedChanged += ToolStripMenuItem_AutoSaveDict_CheckedChanged;
                if (autoSaveDict)
                    _wordDict.OnChange += Dict_OnChangeToSave;
            }

            //初始化颜色信息
            {
                string colors = setting.Colors;
                List<KeyValuePair<string, string>> colorList = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(colors))
                {
                    string[] colorDfArray = colors.Split('|');
                    for (int i = 0; i < colorDfArray.Length; i++)
                    {
                        string[] colordf = colorDfArray[i].Split('-');
                        colorList.Add(new KeyValuePair<string, string>(colordf[0], colordf[1]));
                    }
                }
                DataGridView_Color.RowCount = COLOR_MAX;
                for (int row = 0; row < DataGridView_Color.RowCount; row++)
                {
                    KeyValuePair<string, string> colordf = new KeyValuePair<string, string>("000000", "000000");
                    if (row < colorList.Count)
                        colordf = colorList[row];
                    var rowObj = DataGridView_Color.Rows[row];
                    var rowColor = FuncHelper.HexToColor(colordf.Key);
                    rowObj.Tag = rowColor;
                    rowObj.Cells[Head_Name.Pos].Value = (row + 1).ToString();
                    var buttonCell = (DataGridViewButtonCell)rowObj.Cells[Head_Name.Color];
                    buttonCell.FlatStyle = FlatStyle.Flat;
                    buttonCell.Style.BackColor = rowColor;
                    buttonCell.Style.SelectionBackColor = rowColor;
                    rowObj.Cells[Head_Name.RGB].Value = colordf.Key;
                    rowObj.Cells[Head_Name.OffColor].Value = colordf.Value;
                    rowObj.Cells[Head_Name.Check].Value = false;
                }
                DataGridView_Color.CellValueChanged += DataGridView_Color_CellValueChanged;
                DataGridView_Color.CellContentClick += DataGridView_Color_CellContentClick;
                DataGridView_Color.CurrentCellDirtyStateChanged += DataGridView_Color_CurrentCellDirtyStateChanged;
                CheckBox_Bk.Checked = setting.IsBK;
                Txt_FindSim.Text = setting.Sim.ToString();
                RefreshColor();
            }

            //打开上次的临时文件
            if (File.Exists(TempFile))
                LoadImage(TempFile);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _wordDictTool = null;
            var setting = Properties.Settings.Default;
            List<KeyValuePair<string, string>> colorList = new List<KeyValuePair<string, string>>();
            for (int row = 0; row < DataGridView_Color.RowCount; row++)
            {
                var rowObj = DataGridView_Color.Rows[row];
                var key = rowObj.Cells[Head_Name.RGB].Value.ToString();
                var value = rowObj.Cells[Head_Name.OffColor].Value.ToString();
                colorList.Add(new KeyValuePair<string, string>(key, value));
            }
            setting.Colors = string.Join("|", colorList.ConvertAll(item => $"{item.Key}-{item.Value}"));
            setting.IsBK = CheckBox_Bk.Checked;
            setting.Sim = float.Parse(Txt_FindSim.Text);
            setting.Path = Txt_DictFile.Text;
            setting.AutoSaveDict = ToolStripMenuItem_AutoSaveDict.Checked;
            setting.Save();
        }

        #region MainMenuItem
        //文件
        private void ToolStripMenuItem_LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择bmp文件";
            dialog.Filter = "图像文件(*.bmp)|*.bmp";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            string file = dialog.FileName;
            LoadImage(file);
        }
        private void ToolStripMenuItem_SaveImage_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
                return;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "保存为bmp文件";
            dialog.Filter = "图像文件(*.bmp)|*.bmp";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            pictureBox1.Image.Save(dialog.FileName);
        }
        private void ToolStripMenuItem_Screenshot_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = ScreenshotForm.ShowDialogGetBitmap();
            if (bitmap != null)
            {
                var path = TempFile;
                bitmap.Save(path);
                LoadImage(path);
            }
        }
        private void ToolStripMenuItem_SaveGrayImage_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
                return;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "保存二值化图为bmp文件";
            dialog.Filter = "图像文件(*.bmp)|*.bmp";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            pictureBox2.Image.Save(dialog.FileName);
        }
        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //工具
        private void ToolStripMenuItem_ExtractWhole_Click(object sender, EventArgs e)
        {
            _showDict = _tempDict;
            _tempDict.Clear();
            var dictInfo = _opSoft.FetchWord(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height, TextBox_Color.Text, "");
            var findWord = _wordDict.FindByFeature(dictInfo);
            if (findWord != null)
                _tempDict.Add(findWord.wordCode);
            else
                _tempDict.Add(dictInfo);
            RefreshListBox();
        }
        private void ToolStripMenuItem_Extract_Click(object sender, EventArgs e)
        {
            _showDict = _tempDict;
            _tempDict.Clear();
            string result = _opSoft.GetWordsNoDict(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height, TextBox_Color.Text);
            var count = _opSoft.GetWordResultCount(result);
            for (int i = 0; i < count; i++)
            {
                var dictInfo = _opSoft.GetWordResultStr(result, i);
                var findWord = _wordDict.FindByFeature(dictInfo);
                if (findWord != null)
                    _tempDict.Add(findWord.wordCode);
                else
                    _tempDict.Add(dictInfo);
            }
            RefreshListBox();
        }
        private void ToolStripMenuItem_ResetColorConfig_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < DataGridView_Color.RowCount; row++)
            {
                KeyValuePair<string, string> colordf = new KeyValuePair<string, string>("000000", "000000");
                var rowObj = DataGridView_Color.Rows[row];
                rowObj.Cells[Head_Name.RGB].Value = colordf.Key;
                rowObj.Cells[Head_Name.OffColor].Value = colordf.Value;
                rowObj.Cells[Head_Name.Check].Value = false;
            }
        }

        //字库
        private void ToolStripMenuItem_OpenDict_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择字库文件";
            dialog.Filter = "字库文件(*.dict)|*.dict";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            string file = dialog.FileName;
            Directory.CreateDirectory(Path.GetDirectoryName(file));
            if (!File.Exists(file))
                File.Create(file).Close();
            Txt_DictFile.Text = file;
        }
        private void ToolStripMenuItem_EditDict_Click(object sender, EventArgs e)
        {
            _showDict = _wordDict;
            _tempDict.Clear();
            RefreshListBox();
        }
        private void ToolStripMenuItem_SaveDict_Click(object sender, EventArgs e)
        {
            _opSoft.ClearDict(DICT_FILE);
            foreach (var item in _wordDict.wrods)
                _opSoft.AddDict(DICT_FILE, item.wordCode);
            _opSoft.SaveDict(DICT_FILE, Txt_DictFile.Text);
        }
        private void ToolStripMenuItem_AutoSaveDict_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_AutoSaveDict.Checked = !ToolStripMenuItem_AutoSaveDict.Checked;
        }
        private void ToolStripMenuItem_AutoSaveDict_CheckedChanged(object sender, EventArgs e)
        {
            if (ToolStripMenuItem_AutoSaveDict.Checked)
                _wordDict.OnChange += Dict_OnChangeToSave;
            else
                _wordDict.OnChange -= Dict_OnChangeToSave;
        }
        private void ToolStripMenuItem_PreviewDict_Click(object sender, EventArgs e)
        {
            float sim = float.Parse(Txt_FindSim.Text);
            string ocrText = _opSoft.Ocr(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height, TextBox_Color.Text, sim);
            TextBox_Ocr.Text = ocrText;
        }

        //帮助
        private void ToolStripMenuItem_Guide_Click(object sender, EventArgs e)
        {
            MessageBox.Show("截图模式：小键盘方向键可控制鼠标每次移动1像素，加Shift可每次移动10像素\n\n" +
                "取色模式：鼠标左键确定取当前鼠标所在像素颜色\n" +
                "   Alt+1 为颜色配置1取色\n" +
                "   Alt+2 为颜色配置2取色\n" +
                "   ...\n" +
                "   Alt+9 为颜色配置9取色\n\n" +
                "其余快捷键可看菜单栏子项");
        }
        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("仅用于编辑\"OP\"所使用字库\n\n                                  --flaot");
        }
        private void MenuItem_JumpLink_Click(object sender, EventArgs e)
        {
            var linkLabel = (ToolStripMenuItem)sender;
            var url = (string)linkLabel.Tag;
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        #endregion

        #region 原图
        private void Btn_LoadImage_Click(object sender, EventArgs _) => ToolStripMenuItem_LoadImage.PerformClick();
        private void Btn_SaveImage_Click(object sender, EventArgs e) => ToolStripMenuItem_SaveImage.PerformClick();
        private void Btn_Screenshot_Click(object sender, EventArgs e) => ToolStripMenuItem_Screenshot.PerformClick();
        private void LoadImage(string file)
        {
            _opSoft.LoadPic(file);
            _opSoft.SetDisplayInput($"pic:{file}");
            _opSoft.GetPicSize(file, out var width, out var height);
            pictureBox1.Image = _opSoft.GetScreenDataBmp(0, 0, width, height);
            pictureBox2.Image = GrayImageBin.GrayImage(pictureBox1.Image, TextBox_Color.Text);
        }
        #endregion

        #region 二值图
        private void Btn_SaveBinImage_Click(object sender, EventArgs e) => ToolStripMenuItem_SaveGrayImage.PerformClick();
        private void Btn_ExtractWhole_Click(object sender, EventArgs e) => ToolStripMenuItem_ExtractWhole.PerformClick();
        private void Btn_Extract_Click(object sender, EventArgs e) => ToolStripMenuItem_Extract.PerformClick();
        #endregion

        #region 颜色信息
        private void DataGridView_Color_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;
            if (DataGridView_Color.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                ColorDialog colorDialog = new ColorDialog();
                colorDialog.Color = (Color)DataGridView_Color.Rows[e.RowIndex].Tag;
                if (colorDialog.ShowDialog() != DialogResult.OK)
                    return;
                Color newColor = colorDialog.Color;
                var rowObj = DataGridView_Color.Rows[e.RowIndex];
                rowObj.Tag = newColor;
                var buttonCell = (DataGridViewButtonCell)rowObj.Cells[Head_Name.Color];
                buttonCell.Style.BackColor = newColor;
                buttonCell.Style.SelectionBackColor = newColor;
                rowObj.Cells[Head_Name.RGB].Value = FuncHelper.ColorToHex(newColor);
                RefreshColor();
            }
        }
        private void DataGridView_Color_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView_Color.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void DataGridView_Color_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;
            var rowObj = DataGridView_Color.Rows[e.RowIndex];
            string columnName = DataGridView_Color.Columns[e.ColumnIndex].Name;
            var cell = rowObj.Cells[e.ColumnIndex];
            if (cell is DataGridViewCheckBoxCell)
                RefreshColor();
            if (columnName == Head_Name.OffColor)
                Utils.DataGridViewTextBoxCellColorHex_TextChanged(cell as DataGridViewTextBoxCell);
            if (columnName == Head_Name.RGB)
            {
                var color = (Color)rowObj.Tag;
                if (FuncHelper.ColorToHex(color) == cell.Value.ToString())
                    return;
                Utils.DataGridViewTextBoxCellColorHex_TextChanged(cell as DataGridViewTextBoxCell);
                var newColor = FuncHelper.HexToColor(cell.Value.ToString());
                var buttonCell = (DataGridViewButtonCell)rowObj.Cells[Head_Name.Color];
                rowObj.Tag = newColor;
                buttonCell.Style.BackColor = newColor;
                buttonCell.Style.SelectionBackColor = newColor;
                DataGridView_Color.Refresh();
                RefreshColor();
            }
        }
        private void CheckBox_Bk_CheckedChanged(object sender, EventArgs e)
        {
            RefreshColor();
        }
        private void RefreshColor()
        {
            StringBuilder sb = new StringBuilder();
            if (CheckBox_Bk.Checked)
                sb.Append('@');
            foreach (DataGridViewRow selRow in DataGridView_Color.Rows)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)selRow.Cells[Head_Name.Check];
                bool isChecked = Convert.ToBoolean(checkCell.Value);
                if (!isChecked)
                    continue;
                string colorStr = selRow.Cells[Head_Name.RGB].Value.ToString() + '-' + selRow.Cells[Head_Name.OffColor].Value.ToString();
                if (sb.Length > 1)
                    sb.Append('|');
                sb.Append(colorStr);
            }
            string newColor = sb.ToString();
            if (string.Equals(TextBox_Color.Text, newColor))
                return;
            TextBox_Color.Text = newColor;
            if (pictureBox1.Image != null)
                pictureBox2.Image = GrayImageBin.GrayImage(pictureBox1.Image, TextBox_Color.Text);
        }
        #endregion

        #region 字库
        private void Btn_CreateOrNewDict_Click(object sender, EventArgs e) => ToolStripMenuItem_OpenDict.PerformClick();
        private void Btn_EditDict_Click(object sender, EventArgs e) => ToolStripMenuItem_EditDict.PerformClick();
        private void ListBox_Dict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBox_Dict.SelectedIndex < 0)
                return;

            string selectedItem = (string)ListBox_Dict.SelectedItem;
            var word = _showDict.Find(selectedItem);
            Grid_ShowWord.RefreshData(word);

        }
        private void TextBox_DefWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) //回车键
                return;
            string inputChar = TextBox_DefWord.Text;
            string selectedItem = (string)ListBox_Dict.SelectedItem;
            _wordDict.ReplaceChar(selectedItem, inputChar);
            _showDict.ReplaceChar(selectedItem, inputChar);
            RefreshListBox();
        }
        private void TextBox_FindWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) //回车键
                return;
            string findWord = TextBox_FindWord.Text;
            for (int i = 0; i < _showDict.wrods.Count; i++)
            {
                if (string.Equals(_showDict.wrods[i].name, findWord))
                {
                    ListBox_Dict.SelectedIndex = i;
                    return;
                }
            }
        }
        private void Txt_DictFile_TextChanged(object sender, EventArgs e)
        {
            string file = Txt_DictFile.Text;
            _opSoft.SetDict(DICT_FILE, file);
            _opSoft.UseDict(DICT_FILE);

            //初始化字库信息
            _showDict = _wordDict;
            _wordDict.PauseEvent();
            _wordDict.Clear();
            for (int i = 0; i < _opSoft.GetDictCount(DICT_FILE); i++)
            {
                var dictInfo = _opSoft.GetDict(DICT_FILE, i);
                _wordDict.Add(dictInfo);
            }
            _wordDict.ResumeEvent();

            //界面显示
            RefreshListBox();
            Dict_OnChange(_wordDict);
        }
        private void Dict_OnChange(WordDict obj)
        {
            Txt_DictTip.Text = string.Format((string)Txt_DictTip.Tag, _tempDict.wrods.Count, _wordDict.wrods.Count);
        }
        private void Dict_OnChangeToSave(WordDict dict) => ToolStripMenuItem_SaveDict.PerformClick();
        private void RefreshListBox()
        {
            ListBox_Dict.Items.Clear();
            for (int i = 0; i < _showDict.wrods.Count; i++)
                ListBox_Dict.Items.Add(_showDict.wrods[i].ToString());
        }
        #endregion

        #region Ocr测试
        private void Btn_Ocr_Click(object sender, EventArgs e) => ToolStripMenuItem_PreviewDict.PerformClick();
        #endregion

        #region 控件-输入限制
        private void TextBoxFloatBar_TextChanged(object sender, EventArgs e) => Utils.TextBoxFloatBar_TextChanged(sender, e);
        private void TextBoxFloatBar_KeyPress(object sender, KeyPressEventArgs e) => Utils.TextBoxFloatBar_KeyPress(sender, e);
        #endregion

        private class Head_Name
        {
            public const string Pos = "vPos";
            public const string Color = "vColor";
            public const string RGB = "vRGB";
            public const string OffColor = "vOffColor";
            public const string Check = "vCheck";
        }

        private static MainForm _wordDictTool;
        public static void ShowPanel()
        {
            if (_wordDictTool == null)
                _wordDictTool = new WordDictTool.MainForm();
            if (_wordDictTool.Visible)
                _wordDictTool.Focus();
            else
                _wordDictTool.Show();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Alt + 1
            if (e.Alt && e.KeyCode == Keys.D1)
                SetColor(0);
            // Alt + 2
            if (e.Alt && e.KeyCode == Keys.D2)
                SetColor(1);
            // Alt + 3
            if (e.Alt && e.KeyCode == Keys.D3)
                SetColor(2);
            // Alt + 4
            if (e.Alt && e.KeyCode == Keys.D4)
                SetColor(3);
            // Alt + 5
            if (e.Alt && e.KeyCode == Keys.D5)
                SetColor(4);
            // Alt + 6
            if (e.Alt && e.KeyCode == Keys.D6)
                SetColor(6);
            // Alt + 7
            if (e.Alt && e.KeyCode == Keys.D7)
                SetColor(7);
            // Alt + 8
            if (e.Alt && e.KeyCode == Keys.D8)
                SetColor(8);
            // Alt + 9
            if (e.Alt && e.KeyCode == Keys.D9)
                SetColor(9);
            // Alt + 0
            if (e.Alt && e.KeyCode == Keys.D0)
            {
                Point mousePos = Cursor.Position;
                Rectangle screenBounds = Screen.GetWorkingArea(mousePos);
                Debug.WriteLine("");
                Debug.WriteLine("mousePos:" + mousePos);
                Debug.WriteLine("screenBounds:" + screenBounds);
                Debug.WriteLine("Location:" + this.Location);
                Debug.WriteLine("PointToClient:" + this.PointToClient(mousePos));
                Debug.WriteLine("PointToScreen:" + this.PointToScreen(this.Location));
            }
        }
        private void SetColor(int index)
        {
            if (index >= COLOR_MAX)
                return;
            var color = ScreenshotForm.ShowPanelGetColor();
            if (color.IsEmpty)
                return;
            var rowObj = DataGridView_Color.Rows[index];
            rowObj.Cells[Head_Name.RGB].Value = FuncHelper.ColorToHex(color);
        }
    }
}
