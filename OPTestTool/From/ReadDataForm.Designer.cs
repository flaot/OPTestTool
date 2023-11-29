namespace OPTestTool
{
    partial class ReadDataForm
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
            label2 = new Label();
            Txt_Length = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.DialogResult = DialogResult.OK;
            button1.Location = new Point(77, 92);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 10;
            button1.Text = "确定";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(168, 92);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 11;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Cursor = Cursors.SizeWE;
            label2.Location = new Point(93, 49);
            label2.Name = "label2";
            label2.Size = new Size(35, 17);
            label2.TabIndex = 3;
            label2.Tag = "Txt_Length";
            label2.Text = "长度:";
            label2.MouseDown += LabelUInt_MouseDown;
            label2.MouseMove += LabelUInt_MouseMove;
            label2.MouseUp += LabelUInt_MouseUp;
            // 
            // Txt_Length
            // 
            Txt_Length.Location = new Point(144, 46);
            Txt_Length.Name = "Txt_Length";
            Txt_Length.Size = new Size(75, 23);
            Txt_Length.TabIndex = 4;
            Txt_Length.Text = "0";
            Txt_Length.TextChanged += TextBoxUInt_TextChanged;
            Txt_Length.KeyPress += TextBoxUInt_KeyPress;
            // 
            // ReadDataForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(307, 144);
            Controls.Add(Txt_Length);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
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
        private Button button1;
        private Button button2;
        private Label label2;
        private TextBox Txt_Length;
    }
}