
namespace ScriptTestTools.View.Setting
{
    partial class FrmSettingDownCpu
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
            txtValue = new TextBox();
            label2 = new Label();
            cbType = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 56);
            label1.Name = "label1";
            label1.Size = new Size(59, 12);
            label1.TabIndex = 0;
            label1.Text = "取值范围:";
            // 
            // btnSure
            // 
            btnSure.Location = new Point(71, 92);
            btnSure.Name = "btnSure";
            btnSure.Size = new Size(75, 23);
            btnSure.TabIndex = 2;
            btnSure.Text = "确定";
            btnSure.UseVisualStyleBackColor = true;
            btnSure.Click += btnSure_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(164, 92);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtValue
            // 
            txtValue.Location = new Point(142, 51);
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(53, 21);
            txtValue.TabIndex = 3;
            txtValue.Text = "0";
            txtValue.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(101, 23);
            label2.Name = "label2";
            label2.Size = new Size(35, 12);
            label2.TabIndex = 0;
            label2.Text = "类型:";
            // 
            // cbType
            // 
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.FormattingEnabled = true;
            cbType.Location = new Point(142, 20);
            cbType.Name = "cbType";
            cbType.Size = new Size(53, 20);
            cbType.TabIndex = 4;
            // 
            // FrmSettingDownCpu
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(298, 144);
            Controls.Add(cbType);
            Controls.Add(txtValue);
            Controls.Add(btnCancel);
            Controls.Add(btnSure);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSettingDownCpu";
            StartPosition = FormStartPosition.CenterParent;
            Load += FrmSettingDownCpu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbType;
    }
}