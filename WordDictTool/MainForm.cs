using System.IO;
using System.Text;

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
            _wordDict.OnChange += Dict_OnChangeToSave;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            _opSoft.SetShowErrorMsg(0);
            //初始化字库路径
            {
                string path = Properties.Settings.Default.Path;
                if (string.IsNullOrWhiteSpace(path))
                    path = DictFile;
                Txt_DictFile.Text = path;
            }

            //初始化颜色信息
            {
                string colors = Properties.Settings.Default.Colors;
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
                    var rowColor = HexToColor(colordf.Key);
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
                CheckBox_Bk.Checked = Properties.Settings.Default.IsBK;
                Txt_FindSim.Text = Properties.Settings.Default.Sim.ToString();
                RefreshColor();
            }

            //打开上次的临时文件
            if (File.Exists(TempFile))
                LoadImage(TempFile);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _wordDictTool = null;
            List<KeyValuePair<string, string>> colorList = new List<KeyValuePair<string, string>>();
            for (int row = 0; row < DataGridView_Color.RowCount; row++)
            {
                var rowObj = DataGridView_Color.Rows[row];
                var key = rowObj.Cells[Head_Name.RGB].Value.ToString();
                var value = rowObj.Cells[Head_Name.OffColor].Value.ToString();
                colorList.Add(new KeyValuePair<string, string>(key, value));
            }
            Properties.Settings.Default.Colors = string.Join("|", colorList.ConvertAll(item => $"{item.Key}-{item.Value}"));
            Properties.Settings.Default.IsBK = CheckBox_Bk.Checked;
            Properties.Settings.Default.Sim = float.Parse(Txt_FindSim.Text);
            Properties.Settings.Default.Path = Txt_DictFile.Text;
            Properties.Settings.Default.Save();
        }

        #region 原图
        private void Btn_LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择bmp文件";
            dialog.Filter = "图像文件(*.bmp)|*.bmp";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            string file = dialog.FileName;
            LoadImage(file);
        }
        private void Btn_SaveImage_Click(object sender, EventArgs e)
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
        private void Btn_Screenshot_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = ScreenshotForm.ShowPanel();
            if (bitmap != null)
            {
                var path = TempFile;
                bitmap.Save(path);
                LoadImage(path);
            }
        }
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
        private void Btn_SaveBinImage_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
                return;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "保存为bmp文件";
            dialog.Filter = "图像文件(*.bmp)|*.bmp";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            pictureBox2.Image.Save(dialog.FileName);
        }
        private void Btn_ExtractWhole_Click(object sender, EventArgs e)
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
        private void Btn_Extract_Click(object sender, EventArgs e)
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
                rowObj.Cells[Head_Name.RGB].Value = ColorToHex(newColor);
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
                if (ColorToHex(color) == cell.Value.ToString())
                    return;
                Utils.DataGridViewTextBoxCellColorHex_TextChanged(cell as DataGridViewTextBoxCell);
                var newColor = HexToColor(cell.Value.ToString());
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
        private void Btn_CreateOrNewDict_Click(object sender, EventArgs e)
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
        private void Btn_EditDict_Click(object sender, EventArgs e)
        {
            _showDict = _wordDict;
            _tempDict.Clear();
            RefreshListBox();
        }
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
        private void Dict_OnChangeToSave(WordDict dict)
        {
            _opSoft.ClearDict(DICT_FILE);
            foreach (var item in dict.wrods)
                _opSoft.AddDict(DICT_FILE, item.wordCode);
            _opSoft.SaveDict(DICT_FILE, Txt_DictFile.Text);
        }
        private void RefreshListBox()
        {
            ListBox_Dict.Items.Clear();
            for (int i = 0; i < _showDict.wrods.Count; i++)
                ListBox_Dict.Items.Add(_showDict.wrods[i].ToString());
        }
        #endregion

        #region Ocr测试
        private void Btn_Ocr_Click(object sender, EventArgs e)
        {
            float sim = float.Parse(Txt_FindSim.Text);
            string ocrText = _opSoft.Ocr(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height, TextBox_Color.Text, sim);
            TextBox_Ocr.Text = ocrText;
        }
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
        private static string ColorToHex(Color color)
        {
            return string.Format("{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }
        private static Color HexToColor(string hex)
        {
            return Color.FromArgb(0xFF, Convert.ToInt32(hex.Substring(0, 2), 16),
                                   Convert.ToInt32(hex.Substring(2, 2), 16),
                                   Convert.ToInt32(hex.Substring(4, 2), 16));
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
    }
}
