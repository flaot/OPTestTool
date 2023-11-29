namespace OPTestTool
{
    partial class ReadIntForm
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
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            button1 = new Button();
            button2 = new Button();
            radioButton5 = new RadioButton();
            radioButton6 = new RadioButton();
            radioButton7 = new RadioButton();
            SuspendLayout();
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(202, 22);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(81, 21);
            radioButton4.TabIndex = 2;
            radioButton4.TabStop = true;
            radioButton4.Tag = "+8";
            radioButton4.Text = "8位有符号";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(108, 22);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(88, 21);
            radioButton3.TabIndex = 1;
            radioButton3.TabStop = true;
            radioButton3.Tag = "+16";
            radioButton3.Text = "16位有符号";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(289, 22);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(52, 21);
            radioButton2.TabIndex = 3;
            radioButton2.TabStop = true;
            radioButton2.Tag = "+64";
            radioButton2.Text = "64位";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(14, 22);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(88, 21);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Tag = "+32";
            radioButton1.Text = "32位有符号";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.DialogResult = DialogResult.OK;
            button1.Location = new Point(85, 101);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 10;
            button1.Text = "确定";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(176, 101);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 11;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(14, 49);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(88, 21);
            radioButton5.TabIndex = 4;
            radioButton5.TabStop = true;
            radioButton5.Tag = "-32";
            radioButton5.Text = "32位无符号";
            radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(107, 49);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(88, 21);
            radioButton6.TabIndex = 5;
            radioButton6.TabStop = true;
            radioButton6.Tag = "-16";
            radioButton6.Text = "16位无符号";
            radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton7
            // 
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(201, 49);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(81, 21);
            radioButton7.TabIndex = 6;
            radioButton7.TabStop = true;
            radioButton7.Tag = "-8";
            radioButton7.Text = "8位无符号";
            radioButton7.UseVisualStyleBackColor = true;
            // 
            // ReadDataForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(349, 144);
            Controls.Add(radioButton7);
            Controls.Add(button2);
            Controls.Add(radioButton6);
            Controls.Add(button1);
            Controls.Add(radioButton5);
            Controls.Add(radioButton4);
            Controls.Add(radioButton3);
            Controls.Add(radioButton1);
            Controls.Add(radioButton2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ReadDataForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "请选择";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Button button1;
        private Button button2;
        private RadioButton radioButton5;
        private RadioButton radioButton6;
        private RadioButton radioButton7;
    }
}