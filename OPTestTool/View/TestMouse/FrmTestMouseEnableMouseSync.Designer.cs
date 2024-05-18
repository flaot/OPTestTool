
namespace ScriptTestTools.View.TestMouse
{
    partial class FrmTestMouseEnableMouseSync
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
            cbSelect = new ComboBox();
            btnSure = new Button();
            btnCancel = new Button();
            label2 = new Label();
            label3 = new Label();
            txtDelay = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(62, 25);
            label1.Name = "label1";
            label1.Size = new Size(59, 12);
            label1.TabIndex = 0;
            label1.Text = "是否开启:";
            // 
            // cbSelect
            // 
            cbSelect.FormattingEnabled = true;
            cbSelect.Location = new Point(125, 22);
            cbSelect.Name = "cbSelect";
            cbSelect.Size = new Size(69, 20);
            cbSelect.TabIndex = 1;
            // 
            // btnSure
            // 
            btnSure.Location = new Point(64, 90);
            btnSure.Name = "btnSure";
            btnSure.Size = new Size(75, 23);
            btnSure.TabIndex = 2;
            btnSure.Text = "确定";
            btnSure.UseVisualStyleBackColor = true;
            btnSure.Click += btnSure_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(154, 90);
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
            label2.Location = new Point(87, 56);
            label2.Name = "label2";
            label2.Size = new Size(35, 12);
            label2.TabIndex = 3;
            label2.Text = "延时:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(201, 58);
            label3.Name = "label3";
            label3.Size = new Size(29, 12);
            label3.TabIndex = 3;
            label3.Text = "毫秒";
            // 
            // txtDelay
            // 
            txtDelay.Location = new Point(125, 53);
            txtDelay.Name = "txtDelay";
            txtDelay.Size = new Size(69, 21);
            txtDelay.TabIndex = 4;
            // 
            // FrmTestMouseEnableMouseSync
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(271, 136);
            Controls.Add(txtDelay);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnCancel);
            Controls.Add(btnSure);
            Controls.Add(cbSelect);
            Controls.Add(label1);
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTestMouseEnableMouseSync";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSelect;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDelay;
    }
}