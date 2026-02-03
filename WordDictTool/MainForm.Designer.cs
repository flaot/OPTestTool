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
            CheckBox_Whole = new CheckBox();
            Btn_Extract = new Button();
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
            dataGridView1 = new DataGridView();
            Pos = new DataGridViewTextBoxColumn();
            Color = new DataGridViewImageColumn();
            RGB = new DataGridViewTextBoxColumn();
            HSV = new DataGridViewTextBoxColumn();
            Grayscale = new DataGridViewTextBoxColumn();
            OffColor = new DataGridViewTextBoxColumn();
            Check = new DataGridViewCheckBoxColumn();
            Grid_ShowWord = new GridControl();
            groupBox5 = new GroupBox();
            textBox3 = new TextBox();
            numericUpDown1 = new NumericUpDown();
            label2 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox6 = new GroupBox();
            groupBox9 = new GroupBox();
            textBox1 = new TextBox();
            checkBox2 = new CheckBox();
            panel1 = new Panel();
            groupBox3 = new GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox9.SuspendLayout();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // ListBox_Dict
            // 
            ListBox_Dict.FormattingEnabled = true;
            ListBox_Dict.ItemHeight = 17;
            ListBox_Dict.Location = new Point(11, 147);
            ListBox_Dict.Name = "ListBox_Dict";
            ListBox_Dict.Size = new Size(341, 361);
            ListBox_Dict.TabIndex = 0;
            ListBox_Dict.SelectedIndexChanged += ListBox_Dict_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(Btn_EditImage);
            groupBox1.Controls.Add(Btn_SaveImage);
            groupBox1.Controls.Add(Btn_LoadImage);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(359, 140);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "原图";
            // 
            // button4
            // 
            button4.Location = new Point(278, 78);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 5;
            button4.Text = "抓图";
            button4.UseVisualStyleBackColor = true;
            // 
            // Btn_EditImage
            // 
            Btn_EditImage.Location = new Point(278, 106);
            Btn_EditImage.Name = "Btn_EditImage";
            Btn_EditImage.Size = new Size(75, 23);
            Btn_EditImage.TabIndex = 4;
            Btn_EditImage.Text = "编辑";
            Btn_EditImage.UseVisualStyleBackColor = true;
            // 
            // Btn_SaveImage
            // 
            Btn_SaveImage.Location = new Point(278, 49);
            Btn_SaveImage.Name = "Btn_SaveImage";
            Btn_SaveImage.Size = new Size(75, 23);
            Btn_SaveImage.TabIndex = 3;
            Btn_SaveImage.Text = "保存";
            Btn_SaveImage.UseVisualStyleBackColor = true;
            // 
            // Btn_LoadImage
            // 
            Btn_LoadImage.Location = new Point(278, 19);
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
            pictureBox1.Size = new Size(255, 103);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(CheckBox_Whole);
            groupBox2.Controls.Add(Btn_Extract);
            groupBox2.Controls.Add(pictureBox2);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(368, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(360, 140);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "二值化";
            // 
            // CheckBox_Whole
            // 
            CheckBox_Whole.AutoSize = true;
            CheckBox_Whole.Location = new Point(273, 72);
            CheckBox_Whole.Name = "CheckBox_Whole";
            CheckBox_Whole.Size = new Size(75, 21);
            CheckBox_Whole.TabIndex = 7;
            CheckBox_Whole.Text = "整体提取";
            CheckBox_Whole.UseVisualStyleBackColor = true;
            // 
            // Btn_Extract
            // 
            Btn_Extract.Location = new Point(273, 102);
            Btn_Extract.Name = "Btn_Extract";
            Btn_Extract.Size = new Size(75, 23);
            Btn_Extract.TabIndex = 6;
            Btn_Extract.Text = "提取点阵";
            Btn_Extract.UseVisualStyleBackColor = true;
            Btn_Extract.Click += Btn_Extract_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.ActiveCaptionText;
            pictureBox2.Location = new Point(6, 22);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(255, 103);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // Btn_CreateOrNewDict
            // 
            Btn_CreateOrNewDict.Location = new Point(11, 21);
            Btn_CreateOrNewDict.Name = "Btn_CreateOrNewDict";
            Btn_CreateOrNewDict.Size = new Size(104, 37);
            Btn_CreateOrNewDict.TabIndex = 2;
            Btn_CreateOrNewDict.Text = "打开或新键字库";
            Btn_CreateOrNewDict.UseVisualStyleBackColor = true;
            Btn_CreateOrNewDict.Click += Btn_CreateOrNewDict_Click;
            // 
            // Btn_EditDict
            // 
            Btn_EditDict.Location = new Point(248, 23);
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
            label1.Location = new Point(11, 69);
            label1.Name = "label1";
            label1.Size = new Size(59, 17);
            label1.TabIndex = 8;
            label1.Text = "字库文件:";
            // 
            // Txt_DictFile
            // 
            Txt_DictFile.Location = new Point(76, 66);
            Txt_DictFile.Name = "Txt_DictFile";
            Txt_DictFile.Size = new Size(276, 23);
            Txt_DictFile.TabIndex = 9;
            // 
            // Txt_DictTip
            // 
            Txt_DictTip.AutoSize = true;
            Txt_DictTip.Location = new Point(11, 97);
            Txt_DictTip.Name = "Txt_DictTip";
            Txt_DictTip.Size = new Size(191, 17);
            Txt_DictTip.TabIndex = 10;
            Txt_DictTip.Tag = "识别到的图形数量:{0},字库数量:{1}";
            Txt_DictTip.Text = "识别到的图形数量:{0},字库数量:{1}";
            // 
            // CheckBox_AutoSaveDict
            // 
            CheckBox_AutoSaveDict.AutoSize = true;
            CheckBox_AutoSaveDict.Location = new Point(248, 120);
            CheckBox_AutoSaveDict.Name = "CheckBox_AutoSaveDict";
            CheckBox_AutoSaveDict.Size = new Size(99, 21);
            CheckBox_AutoSaveDict.TabIndex = 11;
            CheckBox_AutoSaveDict.Text = "自动保存字库";
            CheckBox_AutoSaveDict.UseVisualStyleBackColor = true;
            // 
            // Btn_DelAllChar
            // 
            Btn_DelAllChar.Location = new Point(210, 547);
            Btn_DelAllChar.Name = "Btn_DelAllChar";
            Btn_DelAllChar.Size = new Size(92, 31);
            Btn_DelAllChar.TabIndex = 12;
            Btn_DelAllChar.Text = "批量删除";
            Btn_DelAllChar.UseVisualStyleBackColor = true;
            // 
            // Btn_FindChar
            // 
            Btn_FindChar.Location = new Point(248, 512);
            Btn_FindChar.Name = "Btn_FindChar";
            Btn_FindChar.Size = new Size(104, 31);
            Btn_FindChar.TabIndex = 13;
            Btn_FindChar.Text = "查找";
            Btn_FindChar.UseVisualStyleBackColor = true;
            // 
            // Btn_Sort
            // 
            Btn_Sort.Location = new Point(76, 548);
            Btn_Sort.Name = "Btn_Sort";
            Btn_Sort.Size = new Size(92, 31);
            Btn_Sort.TabIndex = 14;
            Btn_Sort.Text = "排序";
            Btn_Sort.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(11, 520);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(231, 23);
            textBox2.TabIndex = 15;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = SystemColors.ButtonFace;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Pos, Color, RGB, HSV, Grayscale, OffColor, Check });
            dataGridView1.Location = new Point(6, 20);
            dataGridView1.Margin = new Padding(6);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(347, 373);
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
            // Grid_ShowWord
            // 
            Grid_ShowWord.BorderStyle = BorderStyle.FixedSingle;
            Grid_ShowWord.Dock = DockStyle.Fill;
            Grid_ShowWord.Location = new Point(3, 19);
            Grid_ShowWord.Name = "Grid_ShowWord";
            Grid_ShowWord.Size = new Size(348, 302);
            Grid_ShowWord.TabIndex = 19;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(textBox3);
            groupBox5.Controls.Add(numericUpDown1);
            groupBox5.Controls.Add(label2);
            groupBox5.Location = new Point(6, 334);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(6);
            groupBox5.Size = new Size(354, 100);
            groupBox5.TabIndex = 18;
            groupBox5.TabStop = false;
            groupBox5.Text = "Ocr测试";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(97, 22);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(248, 66);
            textBox3.TabIndex = 2;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown1.Location = new Point(43, 49);
            numericUpDown1.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(48, 23);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.Value = new decimal(new int[] { 90, 0, 0, 131072 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 49);
            label2.Name = "label2";
            label2.Size = new Size(28, 17);
            label2.TabIndex = 0;
            label2.Text = "sim";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 365F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.9999962F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox6, 2, 0);
            tableLayoutPanel1.Controls.Add(groupBox9, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 1, 1);
            tableLayoutPanel1.Controls.Add(groupBox2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 146F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(1098, 586);
            tableLayoutPanel1.TabIndex = 19;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(Btn_CreateOrNewDict);
            groupBox6.Controls.Add(textBox2);
            groupBox6.Controls.Add(ListBox_Dict);
            groupBox6.Controls.Add(Btn_Sort);
            groupBox6.Controls.Add(Btn_EditDict);
            groupBox6.Controls.Add(Btn_FindChar);
            groupBox6.Controls.Add(label1);
            groupBox6.Controls.Add(Btn_DelAllChar);
            groupBox6.Controls.Add(Txt_DictFile);
            groupBox6.Controls.Add(CheckBox_AutoSaveDict);
            groupBox6.Controls.Add(Txt_DictTip);
            groupBox6.Dock = DockStyle.Fill;
            groupBox6.Location = new Point(734, 3);
            groupBox6.Name = "groupBox6";
            tableLayoutPanel1.SetRowSpan(groupBox6, 2);
            groupBox6.Size = new Size(361, 580);
            groupBox6.TabIndex = 0;
            groupBox6.TabStop = false;
            groupBox6.Text = "字库";
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(textBox1);
            groupBox9.Controls.Add(checkBox2);
            groupBox9.Controls.Add(dataGridView1);
            groupBox9.Dock = DockStyle.Fill;
            groupBox9.Location = new Point(3, 149);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(359, 434);
            groupBox9.TabIndex = 3;
            groupBox9.TabStop = false;
            groupBox9.Text = "颜色信息";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(63, 402);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(290, 23);
            textBox1.TabIndex = 16;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(6, 407);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(51, 21);
            checkBox2.TabIndex = 8;
            checkBox2.Text = "背景";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox5);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(365, 146);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(366, 440);
            panel1.TabIndex = 4;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(Grid_ShowWord);
            groupBox3.Location = new Point(6, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(354, 324);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "点阵";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1098, 586);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "字库制作工具";
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            panel1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
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
        private DataGridView dataGridView1;
        private GroupBox groupBox5;
        private DataGridViewTextBoxColumn Pos;
        private DataGridViewImageColumn Color;
        private DataGridViewTextBoxColumn RGB;
        private DataGridViewTextBoxColumn HSV;
        private DataGridViewTextBoxColumn Grayscale;
        private DataGridViewTextBoxColumn OffColor;
        private DataGridViewCheckBoxColumn Check;
        private GridControl Grid_ShowWord;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox6;
        private GroupBox groupBox9;
        private Panel panel1;
        private GroupBox groupBox3;
        private CheckBox CheckBox_Whole;
        private Button Btn_Extract;
        private TextBox textBox1;
        private CheckBox checkBox2;
        private TextBox textBox3;
        private NumericUpDown numericUpDown1;
        private Label label2;
    }
}