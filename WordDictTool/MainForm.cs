using System.Text;
using System.Windows.Forms;

namespace WordDictTool
{
    public partial class MainForm : Form
    {
        private OpSoft _opSoft = new OpSoft();
        private bool _hasDictList;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _opSoft.SetShowErrorMsg(0);
            DataGridView_Color.RowCount = 10;
            for (int row = 0; row < DataGridView_Color.RowCount; row++)
            {
                var rowObj = DataGridView_Color.Rows[row];
                rowObj.Cells[Head_Name.Pos].Value = string.Empty;
                rowObj.Cells[Head_Name.Color].Value = new Bitmap(80, 80);
                rowObj.Cells[Head_Name.RGB].Value = "000000";
                rowObj.Cells[Head_Name.HSV].Value = "0.0.0";
                rowObj.Cells[Head_Name.Grayscale].Value = "0";
                rowObj.Cells[Head_Name.OffColor].Value = "000000";
                rowObj.Cells[Head_Name.Check].Value = false;
            }
            DataGridView_Color.CellValueChanged += DataGridView_Color_CellValueChanged;
            DataGridView_Color.CurrentCellDirtyStateChanged += DataGridView_Color_CurrentCellDirtyStateChanged;

#if DEBUG
            string file = $"C:\\无标题.bmp";
            LoadImage(file);
#endif
        }

        private void DataGridView_Color_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView_Color.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DataGridView_Color_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;
            if (DataGridView_Color.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                RefreshColor();
        }

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
            _opSoft.SetMemDict(0, File.ReadAllText(file), file.Length);
            _opSoft.UseDict(0);
            string[] lines = File.ReadAllLines(file);
            _hasDictList = true;
            ListBox_Dict.Items.Clear();
            foreach (var line in lines)
            {
                ListBox_Dict.Items.Add(line);
            }
            RefreshData();
        }

        private void Btn_EditDict_Click(object sender, EventArgs e)
        {

        }

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
        private void Btn_Extract_Click(object sender, EventArgs e)
        {
            _hasDictList = false;
            ListBox_Dict.Items.Clear();
            if (CheckBox_Whole.Checked)
            {
                var dictInfo = _opSoft.FetchWord(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height, TextBox_Color.Text, "");
                ListBox_Dict.Items.Add(dictInfo);
            }
            else
            {
                string result = _opSoft.GetWordsNoDict(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height, TextBox_Color.Text);
                var count = _opSoft.GetWordResultCount(result);
                for (int i = 0; i < count; i++)
                {
                    var dictInfo = _opSoft.GetWordResultStr(result, i);
                    ListBox_Dict.Items.Add(dictInfo);
                }
            }
            RefreshData();
        }
        private void RefreshData()
        {
            Txt_DictTip.Text = string.Format((string)Txt_DictTip.Tag, _hasDictList ? ListBox_Dict.Items.Count : 0, _opSoft.GetDictCount(0));
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

        private void ListBox_Dict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBox_Dict.SelectedIndex < 0)
                return;

            string selectedItem = (string)ListBox_Dict.SelectedItem;
            Grid_ShowWord.RefreshData(selectedItem);

        }
        private void LoadImage(string file)
        {
            _opSoft.LoadPic(file);
            _opSoft.SetDisplayInput($"pic:{file}");
            _opSoft.GetPicSize(file, out var width, out var height);
            pictureBox1.Image = _opSoft.GetScreenDataBmp(0, 0, width, height);
            pictureBox2.Image = GrayImageBin.GrayImage(pictureBox1.Image, TextBox_Color.Text);
        }

        private void CheckBox_Bk_CheckedChanged(object sender, EventArgs e)
        {
            RefreshColor();
        }

        private class Head_Name
        {
            public const string Pos = "Pos";
            public const string Color = "Color";
            public const string RGB = "RGB";
            public const string HSV = "HSV";
            public const string Grayscale = "Grayscale";
            public const string OffColor = "OffColor";
            public const string Check = "Check";
        }
    }
}
