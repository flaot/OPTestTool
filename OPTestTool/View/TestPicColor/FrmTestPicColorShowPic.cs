using OPTestTool;
using ScriptTestTools.StaticData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptTestTools.View.TestPicColor
{
    public partial class FrmTestPicColorShowPic : Form
    {
        private string picPathName = AppDomain.CurrentDomain.BaseDirectory + "capture_file.bmp";
        public Bitmap bp = null;
        private OpSoft op;

        public FrmTestPicColorShowPic(OpSoft _op)
        {
            InitializeComponent();
            op = _op;
            SetWindowSize();
        }

        // 创建一个计数器变量
        private int counter = 0;

        private bool isCtrl = false;

        private void SetWindowSize()
        {
            //bp = new Bitmap(picPath);
            this.AutoSize = true;
            pbShowPic.Image = null;
            //pbShowPic.Image = bp;
            this.Show();
        }

        public void UpdateImage()
        {
            try
            {
                if (bp != null)
                    bp.Dispose();
                op.Capture(PicColor.x1, PicColor.y1, PicColor.x2, PicColor.y2, picPathName);
                Logger.Log(string.Format(">>>Capture\n{0},{1},{2},{3},\"{4}\"", PicColor.x1, PicColor.y1, PicColor.x2, PicColor.y2, picPathName));
                bp = new Bitmap(picPathName);
                //pbShowPic.Image = null;
                pbShowPic.Image = bp;
                this.AutoSize = true;
                //this.Update();
                this.WindowState = FormWindowState.Normal;
                this.Focus();
                counter = 0;
            }
            catch (Exception ex)
            {
                ex.Message.ToLog();
            }
        }

        private void FrmTestPicColorShowPic_FormClosed(object sender, FormClosedEventArgs e)
        {
            pbShowPic.Image = null;
            StaticData.PicColor.frmTestPicColorShowPic = null;
            bp.Dispose();
        }

        private void pbShowPic_MouseClick(object sender, MouseEventArgs e)
        {
            //按下ctrl+鼠标左键(标记点位)

            //ctrl+l 清空标记
        }

        private void pbShowPic_Paint(object sender, PaintEventArgs e)
        {
        }

        private void FrmTestPicColorShowPic_Load(object sender, EventArgs e)
        {
            Paint();
        }

        private void FrmTestPicColorShowPic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                isCtrl = true;
        }

        private void FrmTestPicColorShowPic_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                isCtrl = false;
            if (e.KeyCode == Keys.F1)
                MessageBox.Show("1.鼠标左键按住拖动可以选范围\r\n2.鼠标右击可以标注序列\r\n3.按住ctrl+鼠标右击清空标注");
            if (e.KeyCode == Keys.F5)
            {
                UpdateImage();
            }
        }

        private void Paint()
        {
            // 创建一个矩形
            Rectangle rectangle = Rectangle.Empty;
            // 标记鼠标是否按下的变量
            bool isMouseDown = false;

            // 创建一个Font对象用于绘制文本
            Font font = new Font("Arial", 20);
            // 监听鼠标按下事件
            pbShowPic.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    rectangle.Location = e.Location;
                    isMouseDown = true;
                }

                if (e.Button == MouseButtons.Right)
                {
                    //按住ctrl说明要清空
                    if (isCtrl)
                    {
                        counter = 0;
                        pbShowPic.Invalidate();
                        return;
                    }

                    // 递增计数器
                    counter++;
                    $"坐标{counter}:==>{e.Location.X},{e.Location.Y}".ToLog();
                    // 在PictureBox上绘制文本
                    Graphics graphics = pbShowPic.CreateGraphics();
                    // 获取文本的大小
                    SizeF textSize = graphics.MeasureString(counter.ToString(), font);

                    // 创建一个与文本大小相同的矩形，并设置背景颜色
                    RectangleF backgroundRect = new RectangleF(e.Location, textSize);
                    graphics.FillRectangle(Brushes.Yellow, backgroundRect);

                    // 绘制文本
                    graphics.DrawString(counter.ToString(), font, Brushes.Black, e.Location);
                }
            };

            // 监听鼠标弹起事件
            pbShowPic.MouseUp += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    rectangle.Width = e.Location.X - rectangle.X;
                    rectangle.Height = e.Location.Y - rectangle.Y;
                    isMouseDown = false;
                    // 在PictureBox上绘制矩形
                    string pointRec = $"{rectangle.X},{rectangle.Y},{e.Location.X},{e.Location.Y}";
                    PicColor.FastSetFindPicAndFindPicExValue(pointRec);
                    $"矩形坐标==>{pointRec}".ToLog();
                    pbShowPic.Invalidate();
                }
            };
            // 监听鼠标移动事件
            pbShowPic.MouseMove += (sender, e) =>
            {
                if (isMouseDown)
                {
                    rectangle.Width = e.Location.X - rectangle.X;
                    rectangle.Height = e.Location.Y - rectangle.Y;

                    // 实时更新矩形的位置和大小，并重绘PictureBox
                    pbShowPic.Invalidate();
                }
            };

            // 在PictureBox的Paint事件中绘制矩形
            pbShowPic.Paint += (sender, e) =>
            {
                // 创建一个Graphics对象
                Graphics graphics = e.Graphics;

                // 绘制矩形
                graphics.DrawRectangle(Pens.Red, rectangle);
            };

            // 监听键盘按下事件
            this.KeyPress += (sender, e) =>
            {
                // 检查按下的键是否为Ctrl键
                if (e.KeyChar == (char)Keys.ControlKey & e.KeyChar == (char)Keys.L)
                {
                    // 清空计数器
                    counter = 0;

                    // 清空PictureBox的绘图内容
                    pbShowPic.Invalidate();
                }
            };
        }
    }
}