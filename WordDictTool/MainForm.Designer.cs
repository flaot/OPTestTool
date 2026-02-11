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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
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
            TextBox_FindWord = new TextBox();
            DataGridView_Color = new DataGridView();
            vPos = new DataGridViewTextBoxColumn();
            vColor = new DataGridViewButtonColumn();
            vRGB = new DataGridViewTextBoxColumn();
            vOffColor = new DataGridViewTextBoxColumn();
            vCheck = new DataGridViewCheckBoxColumn();
            Grid_ShowWord = new GridControl();
            groupBox5 = new GroupBox();
            Btn_Ocr = new Button();
            Txt_FindSim = new TextBox();
            TextBox_Ocr = new TextBox();
            label2 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox6 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            TextBox_DefWord = new TextBox();
            groupBox9 = new GroupBox();
            TextBox_Color = new TextBox();
            CheckBox_Bk = new CheckBox();
            panel1 = new Panel();
            groupBox3 = new GroupBox();
            Btn_SaveBinImage = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DataGridView_Color).BeginInit();
            groupBox5.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox9.SuspendLayout();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // ListBox_Dict
            // 
            ListBox_Dict.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ListBox_Dict.FormattingEnabled = true;
            ListBox_Dict.ItemHeight = 17;
            ListBox_Dict.Location = new Point(6, 96);
            ListBox_Dict.Name = "ListBox_Dict";
            ListBox_Dict.Size = new Size(212, 412);
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
            groupBox1.Size = new Size(354, 140);
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
            Btn_SaveImage.Click += Btn_SaveImage_Click;
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
            groupBox2.Controls.Add(Btn_SaveBinImage);
            groupBox2.Controls.Add(CheckBox_Whole);
            groupBox2.Controls.Add(Btn_Extract);
            groupBox2.Controls.Add(pictureBox2);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(363, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(502, 140);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "二值化";
            // 
            // CheckBox_Whole
            // 
            CheckBox_Whole.AutoSize = true;
            CheckBox_Whole.Location = new Point(273, 77);
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
            Btn_CreateOrNewDict.Location = new Point(10, 19);
            Btn_CreateOrNewDict.Name = "Btn_CreateOrNewDict";
            Btn_CreateOrNewDict.Size = new Size(100, 30);
            Btn_CreateOrNewDict.TabIndex = 2;
            Btn_CreateOrNewDict.Text = "打开字库";
            Btn_CreateOrNewDict.UseVisualStyleBackColor = true;
            Btn_CreateOrNewDict.Click += Btn_CreateOrNewDict_Click;
            // 
            // Btn_EditDict
            // 
            Btn_EditDict.Location = new Point(116, 19);
            Btn_EditDict.Name = "Btn_EditDict";
            Btn_EditDict.Size = new Size(100, 30);
            Btn_EditDict.TabIndex = 7;
            Btn_EditDict.Text = "编辑字库";
            Btn_EditDict.UseVisualStyleBackColor = true;
            Btn_EditDict.Click += Btn_EditDict_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 55);
            label1.Name = "label1";
            label1.Size = new Size(59, 17);
            label1.TabIndex = 8;
            label1.Text = "字库文件:";
            // 
            // Txt_DictFile
            // 
            Txt_DictFile.Location = new Point(76, 52);
            Txt_DictFile.Name = "Txt_DictFile";
            Txt_DictFile.ReadOnly = true;
            Txt_DictFile.Size = new Size(139, 23);
            Txt_DictFile.TabIndex = 9;
            Txt_DictFile.TextChanged += Txt_DictFile_TextChanged;
            // 
            // Txt_DictTip
            // 
            Txt_DictTip.AutoSize = true;
            Txt_DictTip.Font = new Font("Microsoft YaHei UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Txt_DictTip.ForeColor = SystemColors.ControlDarkDark;
            Txt_DictTip.Location = new Point(11, 76);
            Txt_DictTip.Name = "Txt_DictTip";
            Txt_DictTip.Size = new Size(160, 16);
            Txt_DictTip.TabIndex = 10;
            Txt_DictTip.Tag = "识别到的图形数量:{0}, 字库数量:{1}";
            Txt_DictTip.Text = "识别到的图形数量:{0}, 字库数量:{1}";
            // 
            // TextBox_FindWord
            // 
            TextBox_FindWord.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TextBox_FindWord.Location = new Point(116, 545);
            TextBox_FindWord.Name = "TextBox_FindWord";
            TextBox_FindWord.Size = new Size(99, 23);
            TextBox_FindWord.TabIndex = 15;
            TextBox_FindWord.KeyPress += TextBox_FindWord_KeyPress;
            // 
            // DataGridView_Color
            // 
            DataGridView_Color.AllowUserToAddRows = false;
            DataGridView_Color.AllowUserToDeleteRows = false;
            DataGridView_Color.AllowUserToResizeColumns = false;
            DataGridView_Color.AllowUserToResizeRows = false;
            DataGridView_Color.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataGridView_Color.BackgroundColor = SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            DataGridView_Color.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridView_Color.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridView_Color.Columns.AddRange(new DataGridViewColumn[] { vPos, vColor, vRGB, vOffColor, vCheck });
            DataGridView_Color.Location = new Point(6, 20);
            DataGridView_Color.Margin = new Padding(6);
            DataGridView_Color.Name = "DataGridView_Color";
            DataGridView_Color.RowHeadersVisible = false;
            DataGridView_Color.Size = new Size(347, 373);
            DataGridView_Color.TabIndex = 0;
            // 
            // vPos
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            vPos.DefaultCellStyle = dataGridViewCellStyle2;
            vPos.HeaderText = "";
            vPos.Name = "vPos";
            vPos.ReadOnly = true;
            vPos.Width = 30;
            // 
            // vColor
            // 
            vColor.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            vColor.HeaderText = "颜色";
            vColor.Name = "vColor";
            // 
            // vRGB
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            vRGB.DefaultCellStyle = dataGridViewCellStyle3;
            vRGB.HeaderText = "RGB";
            vRGB.Name = "vRGB";
            vRGB.Width = 60;
            // 
            // vOffColor
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            vOffColor.DefaultCellStyle = dataGridViewCellStyle4;
            vOffColor.HeaderText = "偏色";
            vOffColor.Name = "vOffColor";
            vOffColor.Width = 60;
            // 
            // vCheck
            // 
            vCheck.HeaderText = "";
            vCheck.Name = "vCheck";
            vCheck.Width = 20;
            // 
            // Grid_ShowWord
            // 
            Grid_ShowWord.BorderStyle = BorderStyle.FixedSingle;
            Grid_ShowWord.Dock = DockStyle.Fill;
            Grid_ShowWord.Location = new Point(3, 19);
            Grid_ShowWord.Name = "Grid_ShowWord";
            Grid_ShowWord.Size = new Size(490, 302);
            Grid_ShowWord.TabIndex = 19;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(Btn_Ocr);
            groupBox5.Controls.Add(Txt_FindSim);
            groupBox5.Controls.Add(TextBox_Ocr);
            groupBox5.Controls.Add(label2);
            groupBox5.Location = new Point(6, 334);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(6);
            groupBox5.Size = new Size(496, 100);
            groupBox5.TabIndex = 18;
            groupBox5.TabStop = false;
            groupBox5.Text = "Ocr测试";
            // 
            // Btn_Ocr
            // 
            Btn_Ocr.Location = new Point(412, 65);
            Btn_Ocr.Name = "Btn_Ocr";
            Btn_Ocr.Size = new Size(75, 23);
            Btn_Ocr.TabIndex = 35;
            Btn_Ocr.Text = "Ocr";
            Btn_Ocr.UseVisualStyleBackColor = true;
            Btn_Ocr.Click += Btn_Ocr_Click;
            // 
            // Txt_FindSim
            // 
            Txt_FindSim.Location = new Point(433, 25);
            Txt_FindSim.Name = "Txt_FindSim";
            Txt_FindSim.Size = new Size(54, 23);
            Txt_FindSim.TabIndex = 34;
            Txt_FindSim.Text = "0.8";
            Txt_FindSim.TextChanged += TextBoxFloatBar_TextChanged;
            Txt_FindSim.KeyPress += TextBoxFloatBar_KeyPress;
            // 
            // TextBox_Ocr
            // 
            TextBox_Ocr.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TextBox_Ocr.Location = new Point(9, 22);
            TextBox_Ocr.Multiline = true;
            TextBox_Ocr.Name = "TextBox_Ocr";
            TextBox_Ocr.Size = new Size(390, 66);
            TextBox_Ocr.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(405, 28);
            label2.Name = "label2";
            label2.Size = new Size(28, 17);
            label2.TabIndex = 0;
            label2.Text = "sim";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 360F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
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
            groupBox6.Controls.Add(label4);
            groupBox6.Controls.Add(label3);
            groupBox6.Controls.Add(TextBox_DefWord);
            groupBox6.Controls.Add(Btn_CreateOrNewDict);
            groupBox6.Controls.Add(TextBox_FindWord);
            groupBox6.Controls.Add(ListBox_Dict);
            groupBox6.Controls.Add(Btn_EditDict);
            groupBox6.Controls.Add(label1);
            groupBox6.Controls.Add(Txt_DictFile);
            groupBox6.Controls.Add(Txt_DictTip);
            groupBox6.Dock = DockStyle.Fill;
            groupBox6.Location = new Point(871, 3);
            groupBox6.Name = "groupBox6";
            tableLayoutPanel1.SetRowSpan(groupBox6, 2);
            groupBox6.Size = new Size(224, 580);
            groupBox6.TabIndex = 0;
            groupBox6.TabStop = false;
            groupBox6.Text = "字库";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(10, 548);
            label4.Name = "label4";
            label4.Size = new Size(100, 17);
            label4.TabIndex = 18;
            label4.Text = "查找文字(回车)：";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(10, 517);
            label3.Name = "label3";
            label3.Size = new Size(160, 17);
            label3.TabIndex = 17;
            label3.Text = "定义文字(回车添加到字库)：";
            // 
            // TextBox_DefWord
            // 
            TextBox_DefWord.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TextBox_DefWord.Location = new Point(176, 514);
            TextBox_DefWord.Name = "TextBox_DefWord";
            TextBox_DefWord.Size = new Size(41, 23);
            TextBox_DefWord.TabIndex = 16;
            TextBox_DefWord.KeyPress += TextBox_DefWord_KeyPress;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(TextBox_Color);
            groupBox9.Controls.Add(CheckBox_Bk);
            groupBox9.Controls.Add(DataGridView_Color);
            groupBox9.Dock = DockStyle.Fill;
            groupBox9.Location = new Point(3, 149);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(354, 434);
            groupBox9.TabIndex = 3;
            groupBox9.TabStop = false;
            groupBox9.Text = "颜色信息";
            // 
            // TextBox_Color
            // 
            TextBox_Color.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TextBox_Color.Location = new Point(63, 405);
            TextBox_Color.Name = "TextBox_Color";
            TextBox_Color.ReadOnly = true;
            TextBox_Color.Size = new Size(290, 23);
            TextBox_Color.TabIndex = 16;
            TextBox_Color.Text = "@";
            // 
            // CheckBox_Bk
            // 
            CheckBox_Bk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CheckBox_Bk.AutoSize = true;
            CheckBox_Bk.Checked = true;
            CheckBox_Bk.CheckState = CheckState.Checked;
            CheckBox_Bk.Location = new Point(6, 407);
            CheckBox_Bk.Name = "CheckBox_Bk";
            CheckBox_Bk.Size = new Size(51, 21);
            CheckBox_Bk.TabIndex = 8;
            CheckBox_Bk.Text = "背景";
            CheckBox_Bk.UseVisualStyleBackColor = true;
            CheckBox_Bk.CheckedChanged += CheckBox_Bk_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox5);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(360, 146);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(508, 440);
            panel1.TabIndex = 4;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(Grid_ShowWord);
            groupBox3.Location = new Point(6, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(496, 324);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "点阵";
            // 
            // Btn_SaveBinImage
            // 
            Btn_SaveBinImage.Location = new Point(273, 43);
            Btn_SaveBinImage.Name = "Btn_SaveBinImage";
            Btn_SaveBinImage.Size = new Size(75, 23);
            Btn_SaveBinImage.TabIndex = 6;
            Btn_SaveBinImage.Text = "保存";
            Btn_SaveBinImage.UseVisualStyleBackColor = true;
            Btn_SaveBinImage.Click += Btn_SaveBinImage_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1098, 586);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "字库制作工具";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)DataGridView_Color).EndInit();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
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
        private TextBox TextBox_FindWord;
        private DataGridView DataGridView_Color;
        private GroupBox groupBox5;
        private GridControl Grid_ShowWord;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox6;
        private GroupBox groupBox9;
        private Panel panel1;
        private GroupBox groupBox3;
        private CheckBox CheckBox_Whole;
        private Button Btn_Extract;
        private TextBox TextBox_Color;
        private CheckBox CheckBox_Bk;
        private TextBox TextBox_Ocr;
        private Label label2;
        private TextBox TextBox_DefWord;
        private Label label3;
        private DataGridViewTextBoxColumn vPos;
        private DataGridViewButtonColumn vColor;
        private DataGridViewTextBoxColumn vRGB;
        private DataGridViewTextBoxColumn vOffColor;
        private DataGridViewCheckBoxColumn vCheck;
        private TextBox Txt_FindSim;
        private Label label4;
        private Button Btn_Ocr;
        private Button Btn_SaveBinImage;
    }
}