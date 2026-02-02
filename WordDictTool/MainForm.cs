using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordDictTool
{
    public partial class MainForm : Form
    {
        private OpSoft op = new OpSoft();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            op.SetShowErrorMsg(0);
        }

        private void Btn_CreateOrNewDict_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择字库文件";
            dialog.Filter = "字库文件(*.txt)|*.txt";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            string file = dialog.FileName;
            Directory.CreateDirectory(Path.GetDirectoryName(file));
            if (!File.Exists(file))
                File.Create(file).Close();
            Txt_DictFile.Text = file;
            op.SetMemDict(0, File.ReadAllText(file), file.Length);
            op.UseDict(0);
            Txt_DictTip.Text = string.Format((string)Txt_DictTip.Tag, op.GetDictCount(0), op.GetDictCount(0));
            string[] lines = File.ReadAllLines(file);
            ListBox_Dict.Items.Clear();
            foreach (var line in lines)
            {
                ListBox_Dict.Items.Add(line);
            }
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
            pictureBox1.Image = Image.FromFile(file);
            //op.//设置内存图片
            op.FetchWord(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height, "0x000000", "");
        }
    }
}
