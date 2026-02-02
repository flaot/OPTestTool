namespace WordDictTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ListBox_Dict = new ListBox();
            groupBox1 = new GroupBox();
            button4 = new Button();
            Btn_EditImage = new Button();
            Btn_SaveImage = new Button();
            Btn_LoadImage = new Button();
            pictureBox1 = new PictureBox();
            groupBox2 = new GroupBox();
            pictureBox2 = new PictureBox();
            Btn_CreateOrNewDict = new Button();
            Btn_EditDict = new Button();
            label1 = new Label();
            Txt_DictFile = new TextBox();
            Txt_DictTip = new Label();
            CheckBox_AutoSaveDict = new CheckBox();
            Btn_DelAllChar = new Button();
            Btn_FindChar = new Button();
            Btn_Sort = new Button();
            textBox2 = new TextBox();
            groupBox3 = new GroupBox();
            dataGridView1 = new DataGridView();
            Pos = new DataGridViewTextBoxColumn();
            Color = new DataGridViewImageColumn();
            RGB = new DataGridViewTextBoxColumn();
            HSV = new DataGridViewTextBoxColumn();
            Grayscale = new DataGridViewTextBoxColumn();
            OffColor = new DataGridViewTextBoxColumn();
            Check = new DataGridViewCheckBoxColumn();
            groupBox4 = new GroupBox();
            pictureBox3 = new PictureBox();
            groupBox5 = new GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // ListBox_Dict
            // 
            ListBox_Dict.FormattingEnabled = true;
            ListBox_Dict.ItemHeight = 17;
            ListBox_Dict.Location = new Point(582, 141);
            ListBox_Dict.Name = "ListBox_Dict";
            ListBox_Dict.Size = new Size(256, 361);
            ListBox_Dict.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(Btn_EditImage);
            groupBox1.Controls.Add(Btn_SaveImage);
            groupBox1.Controls.Add(Btn_LoadImage);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(293, 140);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "原图";
            // 
            // button4
            // 
            button4.Location = new Point(207, 80);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 5;
            button4.Text = "抓图";
            button4.UseVisualStyleBackColor = true;
            // 
            // Btn_EditImage
            // 
            Btn_EditImage.Location = new Point(207, 108);
            Btn_EditImage.Name = "Btn_EditImage";
            Btn_EditImage.Size = new Size(75, 23);
            Btn_EditImage.TabIndex = 4;
            Btn_EditImage.Text = "编辑";
            Btn_EditImage.UseVisualStyleBackColor = true;
            // 
            // Btn_SaveImage
            // 
            Btn_SaveImage.Location = new Point(207, 51);
            Btn_SaveImage.Name = "Btn_SaveImage";
            Btn_SaveImage.Size = new Size(75, 23);
            Btn_SaveImage.TabIndex = 3;
            Btn_SaveImage.Text = "保存";
            Btn_SaveImage.UseVisualStyleBackColor = true;
            // 
            // Btn_LoadImage
            // 
            Btn_LoadImage.Location = new Point(207, 21);
            Btn_LoadImage.Name = "Btn_LoadImage";
            Btn_LoadImage.Size = new Size(75, 23);
            Btn_LoadImage.TabIndex = 2;
            Btn_LoadImage.Text = "加载";
            Btn_LoadImage.UseVisualStyleBackColor = true;
            Btn_LoadImage.Click += Btn_LoadImage_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ActiveCaptionText;
            pictureBox1.Location = new Point(6, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(177, 103);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(pictureBox2);
            groupBox2.Location = new Point(311, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 140);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "二值化";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.ActiveCaptionText;
            pictureBox2.Location = new Point(6, 22);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(177, 103);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // Btn_CreateOrNewDict
            // 
            Btn_CreateOrNewDict.Location = new Point(582, 15);
            Btn_CreateOrNewDict.Name = "Btn_CreateOrNewDict";
            Btn_CreateOrNewDict.Size = new Size(104, 37);
            Btn_CreateOrNewDict.TabIndex = 2;
            Btn_CreateOrNewDict.Text = "打开或新键字库";
            Btn_CreateOrNewDict.UseVisualStyleBackColor = true;
            Btn_CreateOrNewDict.Click += Btn_CreateOrNewDict_Click;
            // 
            // Btn_EditDict
            // 
            Btn_EditDict.Location = new Point(727, 15);
            Btn_EditDict.Name = "Btn_EditDict";
            Btn_EditDict.Size = new Size(104, 37);
            Btn_EditDict.TabIndex = 7;
            Btn_EditDict.Text = "编辑字库";
            Btn_EditDict.UseVisualStyleBackColor = true;
            Btn_EditDict.Click += Btn_EditDict_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(582, 63);
            label1.Name = "label1";
            label1.Size = new Size(59, 17);
            label1.TabIndex = 8;
            label1.Text = "字库文件:";
            // 
            // Txt_DictFile
            // 
            Txt_DictFile.Location = new Point(647, 60);
            Txt_DictFile.Name = "Txt_DictFile";
            Txt_DictFile.Size = new Size(184, 23);
            Txt_DictFile.TabIndex = 9;
            // 
            // Txt_DictTip
            // 
            Txt_DictTip.AutoSize = true;
            Txt_DictTip.Location = new Point(582, 91);
            Txt_DictTip.Name = "Txt_DictTip";
            Txt_DictTip.Size = new Size(191, 17);
            Txt_DictTip.TabIndex = 10;
            Txt_DictTip.Tag = "识别到的图形数量:{0},字库数量:{1}";
            Txt_DictTip.Text = "识别到的图形数量:{0},字库数量:{1}";
            // 
            // CheckBox_AutoSaveDict
            // 
            CheckBox_AutoSaveDict.AutoSize = true;
            CheckBox_AutoSaveDict.Location = new Point(739, 112);
            CheckBox_AutoSaveDict.Name = "CheckBox_AutoSaveDict";
            CheckBox_AutoSaveDict.Size = new Size(99, 21);
            CheckBox_AutoSaveDict.TabIndex = 11;
            CheckBox_AutoSaveDict.Text = "自动保存字库";
            CheckBox_AutoSaveDict.UseVisualStyleBackColor = true;
            // 
            // Btn_DelAllChar
            // 
            Btn_DelAllChar.Location = new Point(746, 543);
            Btn_DelAllChar.Name = "Btn_DelAllChar";
            Btn_DelAllChar.Size = new Size(92, 31);
            Btn_DelAllChar.TabIndex = 12;
            Btn_DelAllChar.Text = "批量删除";
            Btn_DelAllChar.UseVisualStyleBackColor = true;
            // 
            // Btn_FindChar
            // 
            Btn_FindChar.Location = new Point(739, 510);
            Btn_FindChar.Name = "Btn_FindChar";
            Btn_FindChar.Size = new Size(104, 31);
            Btn_FindChar.TabIndex = 13;
            Btn_FindChar.Text = "查找";
            Btn_FindChar.UseVisualStyleBackColor = true;
            // 
            // Btn_Sort
            // 
            Btn_Sort.Location = new Point(594, 543);
            Btn_Sort.Name = "Btn_Sort";
            Btn_Sort.Size = new Size(92, 31);
            Btn_Sort.TabIndex = 14;
            Btn_Sort.Text = "排序";
            Btn_Sort.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(582, 514);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(142, 23);
            textBox2.TabIndex = 15;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dataGridView1);
            groupBox3.Location = new Point(7, 158);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(333, 383);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "颜色信息";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.BackgroundColor = SystemColors.ButtonFace;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Pos, Color, RGB, HSV, Grayscale, OffColor, Check });
            dataGridView1.Location = new Point(6, 25);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(321, 352);
            dataGridView1.TabIndex = 0;
            // 
            // Pos
            // 
            Pos.HeaderText = "坐标";
            Pos.Name = "Pos";
            Pos.Width = 55;
            // 
            // Color
            // 
            Color.HeaderText = "";
            Color.Name = "Color";
            Color.Width = 20;
            // 
            // RGB
            // 
            RGB.HeaderText = "RGB";
            RGB.Name = "RGB";
            RGB.Width = 55;
            // 
            // HSV
            // 
            HSV.HeaderText = "HSV";
            HSV.Name = "HSV";
            HSV.Width = 55;
            // 
            // Grayscale
            // 
            Grayscale.HeaderText = "灰度";
            Grayscale.Name = "Grayscale";
            Grayscale.Width = 55;
            // 
            // OffColor
            // 
            OffColor.HeaderText = "偏色";
            OffColor.Name = "OffColor";
            OffColor.Width = 55;
            // 
            // Check
            // 
            Check.HeaderText = "";
            Check.Name = "Check";
            Check.Width = 20;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(pictureBox3);
            groupBox4.Location = new Point(361, 161);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(215, 249);
            groupBox4.TabIndex = 17;
            groupBox4.TabStop = false;
            groupBox4.Text = "点阵";
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(6, 22);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(192, 221);
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // groupBox5
            // 
            groupBox5.Location = new Point(361, 416);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(215, 143);
            groupBox5.TabIndex = 18;
            groupBox5.TabStop = false;
            groupBox5.Text = "Ocr测试";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 586);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(textBox2);
            Controls.Add(Btn_Sort);
            Controls.Add(Btn_FindChar);
            Controls.Add(Btn_DelAllChar);
            Controls.Add(CheckBox_AutoSaveDict);
            Controls.Add(Txt_DictTip);
            Controls.Add(Txt_DictFile);
            Controls.Add(label1);
            Controls.Add(Btn_EditDict);
            Controls.Add(Btn_CreateOrNewDict);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(ListBox_Dict);
            Name = "MainForm";
            Text = "字库制作工具";
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox ListBox_Dict;
        private GroupBox groupBox1;
        private Button button4;
        private Button Btn_EditImage;
        private Button Btn_SaveImage;
        private Button Btn_LoadImage;
        private PictureBox pictureBox1;
        private GroupBox groupBox2;
        private PictureBox pictureBox2;
        private Button Btn_CreateOrNewDict;
        private Button Btn_EditDict;
        private Label label1;
        private TextBox Txt_DictFile;
        private Label Txt_DictTip;
        private CheckBox CheckBox_AutoSaveDict;
        private Button Btn_DelAllChar;
        private Button Btn_FindChar;
        private Button Btn_Sort;
        private TextBox textBox2;
        private GroupBox groupBox3;
        private DataGridView dataGridView1;
        private GroupBox groupBox4;
        private PictureBox pictureBox3;
        private GroupBox groupBox5;
        private DataGridViewTextBoxColumn Pos;
        private DataGridViewImageColumn Color;
        private DataGridViewTextBoxColumn RGB;
        private DataGridViewTextBoxColumn HSV;
        private DataGridViewTextBoxColumn Grayscale;
        private DataGridViewTextBoxColumn OffColor;
        private DataGridViewCheckBoxColumn Check;
    }
}