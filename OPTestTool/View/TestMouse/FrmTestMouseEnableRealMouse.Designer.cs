
namespace ScriptTestTools.View.TestMouse
{
    partial class FrmTestMouseEnableRealMouse
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
            txtDelay = new TextBox();
            label3 = new Label();
            label2 = new Label();
            txtMouseStep = new TextBox();
            label4 = new Label();
            label5 = new Label();
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
            btnSure.Location = new Point(64, 104);
            btnSure.Name = "btnSure";
            btnSure.Size = new Size(75, 23);
            btnSure.TabIndex = 2;
            btnSure.Text = "确定";
            btnSure.UseVisualStyleBackColor = true;
            btnSure.Click += btnSure_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(154, 104);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtDelay
            // 
            txtDelay.Location = new Point(125, 48);
            txtDelay.Name = "txtDelay";
            txtDelay.Size = new Size(69, 21);
            txtDelay.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(201, 53);
            label3.Name = "label3";
            label3.Size = new Size(29, 12);
            label3.TabIndex = 5;
            label3.Text = "毫秒";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(87, 51);
            label2.Name = "label2";
            label2.Size = new Size(35, 12);
            label2.TabIndex = 6;
            label2.Text = "延时:";
            // 
            // txtMouseStep
            // 
            txtMouseStep.Location = new Point(125, 75);
            txtMouseStep.Name = "txtMouseStep";
            txtMouseStep.Size = new Size(69, 21);
            txtMouseStep.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(201, 80);
            label4.Name = "label4";
            label4.Size = new Size(29, 12);
            label4.TabIndex = 8;
            label4.Text = "像素";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(87, 78);
            label5.Name = "label5";
            label5.Size = new Size(35, 12);
            label5.TabIndex = 9;
            label5.Text = "步长:";
            // 
            // FrmTestMouseEnableRealMouse
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(271, 136);
            Controls.Add(txtMouseStep);
            Controls.Add(label4);
            Controls.Add(label5);
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
            Name = "FrmTestMouseEnableRealMouse";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSelect;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtDelay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMouseStep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}