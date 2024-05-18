
namespace ScriptTestTools.View.BindWindow
{
    partial class FrmBindWindowKeypad
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
            groupBox1 = new GroupBox();
            cb4 = new CheckBox();
            cb3 = new CheckBox();
            cb2 = new CheckBox();
            cb1 = new CheckBox();
            rbWindows = new RadioButton();
            rbNormal = new RadioButton();
            btnSelectDxKeypad = new Button();
            btnSure = new Button();
            btnCancel = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cb4);
            groupBox1.Controls.Add(cb3);
            groupBox1.Controls.Add(cb2);
            groupBox1.Controls.Add(cb1);
            groupBox1.Controls.Add(rbWindows);
            groupBox1.Controls.Add(rbNormal);
            groupBox1.Location = new Point(12, 11);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(328, 132);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "键盘(必选)";
            // 
            // cb4
            // 
            cb4.AutoSize = true;
            cb4.Location = new Point(186, 87);
            cb4.Name = "cb4";
            cb4.Size = new Size(138, 16);
            cb4.TabIndex = 5;
            cb4.Text = "dx.keypad.raw.input";
            cb4.UseVisualStyleBackColor = true;
            cb4.CheckedChanged += CheckBoxBus;
            // 
            // cb3
            // 
            cb3.AutoSize = true;
            cb3.Location = new Point(17, 87);
            cb3.Name = "cb3";
            cb3.Size = new Size(102, 16);
            cb3.TabIndex = 4;
            cb3.Text = "dx.keypad.api";
            cb3.UseVisualStyleBackColor = true;
            cb3.CheckedChanged += CheckBoxBus;
            // 
            // cb2
            // 
            cb2.AutoSize = true;
            cb2.Location = new Point(186, 55);
            cb2.Name = "cb2";
            cb2.Size = new Size(138, 16);
            cb2.TabIndex = 3;
            cb2.Text = "dx.keypad.state.api";
            cb2.UseVisualStyleBackColor = true;
            cb2.CheckedChanged += CheckBoxBus;
            // 
            // cb1
            // 
            cb1.AutoSize = true;
            cb1.Location = new Point(17, 55);
            cb1.Name = "cb1";
            cb1.Size = new Size(168, 16);
            cb1.TabIndex = 2;
            cb1.Text = "dx.keypad.input.lock.api";
            cb1.UseVisualStyleBackColor = true;
            cb1.CheckedChanged += CheckBoxBus;
            // 
            // rbWindows
            // 
            rbWindows.AutoSize = true;
            rbWindows.Location = new Point(163, 22);
            rbWindows.Name = "rbWindows";
            rbWindows.Size = new Size(65, 16);
            rbWindows.TabIndex = 1;
            rbWindows.TabStop = true;
            rbWindows.Text = "windows";
            rbWindows.UseVisualStyleBackColor = true;
            rbWindows.CheckedChanged += RadioButtionBus;
            // 
            // rbNormal
            // 
            rbNormal.AutoSize = true;
            rbNormal.Location = new Point(17, 23);
            rbNormal.Name = "rbNormal";
            rbNormal.Size = new Size(59, 16);
            rbNormal.TabIndex = 0;
            rbNormal.TabStop = true;
            rbNormal.Text = "normal";
            rbNormal.UseVisualStyleBackColor = true;
            rbNormal.CheckedChanged += RadioButtionBus;
            // 
            // btnSelectDxKeypad
            // 
            btnSelectDxKeypad.Location = new Point(12, 149);
            btnSelectDxKeypad.Name = "btnSelectDxKeypad";
            btnSelectDxKeypad.Size = new Size(147, 23);
            btnSelectDxKeypad.TabIndex = 1;
            btnSelectDxKeypad.Text = "勾选常用的dx键盘属性";
            btnSelectDxKeypad.UseVisualStyleBackColor = true;
            btnSelectDxKeypad.Click += btnSelectDxKeypad_Click;
            // 
            // btnSure
            // 
            btnSure.Location = new Point(93, 184);
            btnSure.Name = "btnSure";
            btnSure.Size = new Size(75, 23);
            btnSure.TabIndex = 0;
            btnSure.Text = "确认";
            btnSure.UseVisualStyleBackColor = true;
            btnSure.Click += btnSure_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(184, 184);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmBindWindowKeypad
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(352, 219);
            Controls.Add(btnCancel);
            Controls.Add(btnSure);
            Controls.Add(btnSelectDxKeypad);
            Controls.Add(groupBox1);
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmBindWindowKeypad";
            StartPosition = FormStartPosition.CenterParent;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb4;
        private System.Windows.Forms.CheckBox cb3;
        private System.Windows.Forms.CheckBox cb2;
        private System.Windows.Forms.CheckBox cb1;
        private System.Windows.Forms.RadioButton rbWindows;
        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.Button btnSelectDxKeypad;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
    }
}