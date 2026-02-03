using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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
            string file = $"C:\\无标题.bmp";
            LoadImage(file);
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
            pictureBox2.Image = GrayImage(pictureBox1.Image);
        }
        private Image GrayImage(Image orgImage)
        {
            Bitmap bitmap = new Bitmap(orgImage);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                int bytesPerPixel = Image.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
                int height = bitmap.Height;
                int width = bitmap.Width;
                int stride = bitmapData.Stride;
                var scan = (byte*)bitmapData.Scan0;
                int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;
                byte[] rgbValues = new byte[bytes];
                Marshal.Copy(new IntPtr(scan), rgbValues, 0, bytes);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int i = y * width * bytesPerPixel + x * bytesPerPixel;
                        fixed (byte* pData = &rgbValues[i])
                        {
                            byte a = rgbValues[i]; // Alpha (透明度)
                            byte r = rgbValues[i + 1]; // Red (红色)
                            byte g = rgbValues[i + 2]; // Green (绿色)
                            byte b = rgbValues[i + 3]; // Blue (蓝色)
                            *(int *)pData = (g * 299 + r * 587 + a * 114 + 500) / 1000;
                        }
                    }
                }
                //TODO
                Marshal.Copy(rgbValues, 0, new IntPtr(scan), bytes);
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }
    }
}
