
namespace ScriptTestTools.View.Setting
{
    partial class FrmSettingDmGuard
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
            cbType = new ComboBox();
            txtType = new TextBox();
            lblPID = new Label();
            txtPID = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 30);
            label1.Name = "label1";
            label1.Size = new Size(35, 12);
            label1.TabIndex = 0;
            label1.Text = "模式:";
            // 
            // cbSelect
            // 
            cbSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelect.FlatStyle = FlatStyle.System;
            cbSelect.FormattingEnabled = true;
            cbSelect.Location = new Point(93, 27);
            cbSelect.Name = "cbSelect";
            cbSelect.Size = new Size(49, 20);
            cbSelect.TabIndex = 1;
            // 
            // btnSure
            // 
            btnSure.Location = new Point(82, 95);
            btnSure.Name = "btnSure";
            btnSure.Size = new Size(75, 23);
            btnSure.TabIndex = 2;
            btnSure.Text = "确定";
            btnSure.UseVisualStyleBackColor = true;
            btnSure.Click += btnSure_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(174, 95);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // cbType
            // 
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.FlatStyle = FlatStyle.System;
            cbType.FormattingEnabled = true;
            cbType.Location = new Point(160, 27);
            cbType.Name = "cbType";
            cbType.Size = new Size(106, 20);
            cbType.TabIndex = 3;
            cbType.SelectedIndexChanged += cbType_SelectedIndexChanged;
            // 
            // txtType
            // 
            txtType.Location = new Point(51, 65);
            txtType.Name = "txtType";
            txtType.Size = new Size(225, 21);
            txtType.TabIndex = 4;
            txtType.Visible = false;
            // 
            // lblPID
            // 
            lblPID.AutoSize = true;
            lblPID.Location = new Point(107, 69);
            lblPID.Name = "lblPID";
            lblPID.Size = new Size(29, 12);
            lblPID.TabIndex = 5;
            lblPID.Text = "PID:";
            lblPID.Visible = false;
            // 
            // txtPID
            // 
            txtPID.Location = new Point(140, 65);
            txtPID.Name = "txtPID";
            txtPID.Size = new Size(80, 21);
            txtPID.TabIndex = 6;
            txtPID.Text = "0";
            txtPID.Visible = false;
            // 
            // FrmSettingDmGuard
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(336, 147);
            Controls.Add(txtPID);
            Controls.Add(lblPID);
            Controls.Add(txtType);
            Controls.Add(cbType);
            Controls.Add(btnCancel);
            Controls.Add(btnSure);
            Controls.Add(cbSelect);
            Controls.Add(label1);
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSettingDmGuard";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSelect;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label lblPID;
        private System.Windows.Forms.TextBox txtPID;
    }
}