
namespace ScriptTestTools.View.MemoryAsm
{
    partial class FrmMemoryAsmReadString
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
            label1 = new Label();
            btnSure = new Button();
            btnCancel = new Button();
            label2 = new Label();
            txtLen = new TextBox();
            cbSelect = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(54, 25);
            label1.Name = "label1";
            label1.Size = new Size(35, 12);
            label1.TabIndex = 0;
            label1.Text = "编码:";
            // 
            // btnSure
            // 
            btnSure.Location = new Point(56, 90);
            btnSure.Name = "btnSure";
            btnSure.Size = new Size(75, 23);
            btnSure.TabIndex = 2;
            btnSure.Text = "确定";
            btnSure.UseVisualStyleBackColor = true;
            btnSure.Click += btnSure_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(146, 90);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(54, 56);
            label2.Name = "label2";
            label2.Size = new Size(35, 12);
            label2.TabIndex = 0;
            label2.Text = "长度:";
            // 
            // txtLen
            // 
            txtLen.Location = new Point(95, 52);
            txtLen.Name = "txtLen";
            txtLen.Size = new Size(73, 21);
            txtLen.TabIndex = 4;
            // 
            // cbSelect
            // 
            cbSelect.FormattingEnabled = true;
            cbSelect.Location = new Point(95, 22);
            cbSelect.Name = "cbSelect";
            cbSelect.Size = new Size(126, 20);
            cbSelect.TabIndex = 5;
            // 
            // FrmMemoryAsmReadString
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(271, 136);
            Controls.Add(cbSelect);
            Controls.Add(txtLen);
            Controls.Add(btnCancel);
            Controls.Add(btnSure);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMemoryAsmReadString";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FrmMemoryAsmReadString";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLen;
        private System.Windows.Forms.ComboBox cbSelect;
    }
}