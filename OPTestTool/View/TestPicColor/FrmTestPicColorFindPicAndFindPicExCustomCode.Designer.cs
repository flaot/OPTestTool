namespace ScriptTestTools.View.TestPicColor
{
    partial class FrmTestPicColorFindPicAndFindPicExCustomCode
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
            btnSure = new Button();
            btnCancel = new Button();
            cbIf = new CheckBox();
            cbElse = new CheckBox();
            txtElseCustomCode = new RichTextBox();
            txtIFCustomCode = new RichTextBox();
            SuspendLayout();
            // 
            // btnSure
            // 
            btnSure.Location = new Point(156, 187);
            btnSure.Name = "btnSure";
            btnSure.Size = new Size(75, 23);
            btnSure.TabIndex = 2;
            btnSure.Text = "确定";
            btnSure.UseVisualStyleBackColor = true;
            btnSure.Click += btnSure_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(237, 187);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // cbIf
            // 
            cbIf.AutoSize = true;
            cbIf.Location = new Point(21, 11);
            cbIf.Name = "cbIf";
            cbIf.Size = new Size(210, 16);
            cbIf.TabIndex = 3;
            cbIf.Text = "if(FindPic/FindPicEx是否找到图)";
            cbIf.UseVisualStyleBackColor = true;
            cbIf.CheckedChanged += cbIf_CheckedChanged;
            // 
            // cbElse
            // 
            cbElse.AutoSize = true;
            cbElse.Location = new Point(21, 89);
            cbElse.Name = "cbElse";
            cbElse.Size = new Size(48, 16);
            cbElse.TabIndex = 3;
            cbElse.Text = "else";
            cbElse.UseVisualStyleBackColor = true;
            cbElse.CheckedChanged += cbElse_CheckedChanged;
            // 
            // txtElseCustomCode
            // 
            txtElseCustomCode.Location = new Point(42, 110);
            txtElseCustomCode.Name = "txtElseCustomCode";
            txtElseCustomCode.Size = new Size(414, 60);
            txtElseCustomCode.TabIndex = 5;
            txtElseCustomCode.Text = "//没找到图的代码\nreturn false;";
            // 
            // txtIFCustomCode
            // 
            txtIFCustomCode.Location = new Point(42, 29);
            txtIFCustomCode.Name = "txtIFCustomCode";
            txtIFCustomCode.Size = new Size(414, 57);
            txtIFCustomCode.TabIndex = 5;
            txtIFCustomCode.Text = "//找到图需要写的代码\nreturn true;";
            // 
            // FrmTestPicColorFindPicAndFindPicExCustomCode
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(468, 222);
            Controls.Add(txtIFCustomCode);
            Controls.Add(txtElseCustomCode);
            Controls.Add(cbElse);
            Controls.Add(cbIf);
            Controls.Add(btnCancel);
            Controls.Add(btnSure);
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTestPicColorFindPicAndFindPicExCustomCode";
            StartPosition = FormStartPosition.CenterParent;
            Load += FrmTestPicColorFindPicAndFindPicExCustomCode_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbIf;
        private System.Windows.Forms.CheckBox cbElse;
        private System.Windows.Forms.RichTextBox txtElseCustomCode;
        private System.Windows.Forms.RichTextBox txtIFCustomCode;
    }
}