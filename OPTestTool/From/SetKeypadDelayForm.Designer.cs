namespace OPTestTool
{
    partial class SetKeypadDelayForm
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
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            Txt_Delay = new TextBox();
            label2 = new Label();
            label3 = new Label();
            ComboBox_Mode = new ComboBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.DialogResult = DialogResult.OK;
            button1.Location = new Point(65, 92);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "确定";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(156, 92);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Cursor = Cursors.SizeWE;
            label1.Location = new Point(66, 43);
            label1.Name = "label1";
            label1.Size = new Size(35, 17);
            label1.TabIndex = 3;
            label1.Tag = "Txt_Delay";
            label1.Text = "延时:";
            label1.MouseDown += LabelUInt_MouseDown;
            label1.MouseMove += LabelUInt_MouseMove;
            label1.MouseUp += LabelUInt_MouseUp;
            // 
            // Txt_Delay
            // 
            Txt_Delay.Location = new Point(107, 40);
            Txt_Delay.Name = "Txt_Delay";
            Txt_Delay.Size = new Size(67, 23);
            Txt_Delay.TabIndex = 4;
            Txt_Delay.Text = "50";
            Txt_Delay.TextChanged += TextBoxUInt_TextChanged;
            Txt_Delay.KeyPress += TextBoxUInt_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(185, 43);
            label2.Name = "label2";
            label2.Size = new Size(32, 17);
            label2.TabIndex = 5;
            label2.Text = "毫秒";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 15);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 6;
            label3.Text = "键盘模式:";
            // 
            // ComboBox_Mode
            // 
            ComboBox_Mode.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox_Mode.FormattingEnabled = true;
            ComboBox_Mode.Items.AddRange(new object[] { "normal", "normal.hd", "windows" });
            ComboBox_Mode.Location = new Point(108, 10);
            ComboBox_Mode.Name = "ComboBox_Mode";
            ComboBox_Mode.Size = new Size(66, 25);
            ComboBox_Mode.TabIndex = 7;
            // 
            // SetKeypadDelayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(294, 139);
            Controls.Add(ComboBox_Mode);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(Txt_Delay);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SetKeypadDelayForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "请选择";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Button button2;
        private Label label1;
        private TextBox Txt_Delay;
        private Label label2;
        private Label label3;
        private ComboBox ComboBox_Mode;
    }
}