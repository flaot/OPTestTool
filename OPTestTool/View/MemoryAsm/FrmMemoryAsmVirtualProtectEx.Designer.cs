
namespace ScriptTestTools.View.MemoryAsm
{
    partial class FrmMemoryAsmVirtualProtectEx
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
            txtSize = new TextBox();
            txtAddr = new TextBox();
            cbSelect = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            label4 = new Label();
            txtOldProtect = new TextBox();
            SuspendLayout();
            // 
            // btnSure
            // 
            btnSure.Location = new Point(53, 156);
            btnSure.Name = "btnSure";
            btnSure.Size = new Size(75, 23);
            btnSure.TabIndex = 2;
            btnSure.Text = "确定";
            btnSure.UseVisualStyleBackColor = true;
            btnSure.Click += btnSure_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(143, 156);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtSize
            // 
            txtSize.Location = new Point(98, 46);
            txtSize.Name = "txtSize";
            txtSize.Size = new Size(139, 21);
            txtSize.TabIndex = 8;
            // 
            // txtAddr
            // 
            txtAddr.Location = new Point(98, 12);
            txtAddr.Name = "txtAddr";
            txtAddr.Size = new Size(139, 21);
            txtAddr.TabIndex = 9;
            // 
            // cbSelect
            // 
            cbSelect.FormattingEnabled = true;
            cbSelect.Location = new Point(98, 82);
            cbSelect.Name = "cbSelect";
            cbSelect.Size = new Size(139, 20);
            cbSelect.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 16);
            label3.Name = "label3";
            label3.Size = new Size(83, 12);
            label3.TabIndex = 4;
            label3.Text = "地址(16进制):";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(62, 50);
            label2.Name = "label2";
            label2.Size = new Size(35, 12);
            label2.TabIndex = 5;
            label2.Text = "大小:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(62, 86);
            label1.Name = "label1";
            label1.Size = new Size(35, 12);
            label1.TabIndex = 6;
            label1.Text = "类型:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 116);
            label4.Name = "label4";
            label4.Size = new Size(47, 12);
            label4.TabIndex = 5;
            label4.Text = "旧属性:";
            // 
            // txtOldProtect
            // 
            txtOldProtect.Location = new Point(98, 113);
            txtOldProtect.Name = "txtOldProtect";
            txtOldProtect.Size = new Size(139, 21);
            txtOldProtect.TabIndex = 8;
            // 
            // FrmMemoryAsmVirtualProtectEx
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(271, 203);
            Controls.Add(txtOldProtect);
            Controls.Add(txtSize);
            Controls.Add(txtAddr);
            Controls.Add(cbSelect);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnSure);
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMemoryAsmVirtualProtectEx";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FrmMemoryAsmVirtualProtectEx";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.ComboBox cbSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOldProtect;
    }
}