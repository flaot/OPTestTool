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
#if DEBUG
            string file = $"C:\\无标题.bmp";
            LoadImage(file);
#endif
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
                var dictInfo = _opSoft.FetchWord(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height, "@", "");
                ListBox_Dict.Items.Add(dictInfo);
            }
            else
            {
                string result = _opSoft.GetWordsNoDict(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height, "@");
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
            pictureBox2.Image = GrayHelper.GrayImage(pictureBox1.Image, "@");
        }
    }
}
