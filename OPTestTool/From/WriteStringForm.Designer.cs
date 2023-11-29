namespace OPTestTool
{
    partial class WriteStringForm
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
            radioButton3 = new RadioButton();
            radioButton1 = new RadioButton();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            Txt_Value = new TextBox();
            SuspendLayout();
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(186, 26);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(74, 21);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Tag = "utf-16";
            radioButton3.Text = "Unicode";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(117, 26);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(51, 21);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Tag = "gbk";
            radioButton1.Text = "GBK";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.DialogResult = DialogResult.OK;
            button1.Location = new Point(77, 101);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 10;
            button1.Text = "确定";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(168, 101);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 11;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(66, 28);
            label1.Name = "label1";
            label1.Size = new Size(35, 17);
            label1.TabIndex = 0;
            label1.Text = "编码:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 64);
            label2.Name = "label2";
            label2.Size = new Size(71, 17);
            label2.TabIndex = 3;
            label2.Text = "写入的内容:";
            // 
            // Txt_Length
            // 
            Txt_Value.Location = new Point(117, 61);
            Txt_Value.Name = "Txt_Length";
            Txt_Value.Size = new Size(178, 23);
            Txt_Value.TabIndex = 4;
            // 
            // WriteStringForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(307, 144);
            Controls.Add(Txt_Value);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(radioButton3);
            Controls.Add(radioButton1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WriteStringForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "请选择";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RadioButton radioButton3;
        private RadioButton radioButton1;
        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private TextBox Txt_Value;
    }
}