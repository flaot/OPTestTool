
namespace ScriptTestTools.View.BindWindow
{
    partial class FrmBindWindowMouse
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
            cb12 = new CheckBox();
            cb13 = new CheckBox();
            cb11 = new CheckBox();
            cb10 = new CheckBox();
            cb9 = new CheckBox();
            cb8 = new CheckBox();
            cb7 = new CheckBox();
            cb6 = new CheckBox();
            cb5 = new CheckBox();
            cb4 = new CheckBox();
            cb2 = new CheckBox();
            cb3 = new CheckBox();
            cbBus = new CheckBox();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            rbBus = new RadioButton();
            btnSure = new Button();
            btnCancel = new Button();
            btnSelectDxMouse = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cb12);
            groupBox1.Controls.Add(cb13);
            groupBox1.Controls.Add(cb11);
            groupBox1.Controls.Add(cb10);
            groupBox1.Controls.Add(cb9);
            groupBox1.Controls.Add(cb8);
            groupBox1.Controls.Add(cb7);
            groupBox1.Controls.Add(cb6);
            groupBox1.Controls.Add(cb5);
            groupBox1.Controls.Add(cb4);
            groupBox1.Controls.Add(cb2);
            groupBox1.Controls.Add(cb3);
            groupBox1.Controls.Add(cbBus);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(rbBus);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(446, 204);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "鼠标(必选)";
            // 
            // cb12
            // 
            cb12.AutoSize = true;
            cb12.Location = new Point(231, 152);
            cb12.Name = "cb12";
            cb12.Size = new Size(168, 16);
            cb12.TabIndex = 2;
            cb12.Text = "dx.mouse.input.lock.api2";
            cb12.UseVisualStyleBackColor = true;
            cb12.CheckedChanged += CheckBoxBus;
            // 
            // cb13
            // 
            cb13.AutoSize = true;
            cb13.Location = new Point(18, 174);
            cb13.Name = "cb13";
            cb13.Size = new Size(168, 16);
            cb13.TabIndex = 2;
            cb13.Text = "dx.mouse.input.lock.api3";
            cb13.UseVisualStyleBackColor = true;
            cb13.CheckedChanged += CheckBoxBus;
            // 
            // cb11
            // 
            cb11.AutoSize = true;
            cb11.Location = new Point(18, 152);
            cb11.Name = "cb11";
            cb11.Size = new Size(132, 16);
            cb11.TabIndex = 2;
            cb11.Text = "dx.mouse.raw.input";
            cb11.UseVisualStyleBackColor = true;
            cb11.CheckedChanged += CheckBoxBus;
            // 
            // cb10
            // 
            cb10.AutoSize = true;
            cb10.Location = new Point(231, 130);
            cb10.Name = "cb10";
            cb10.Size = new Size(114, 16);
            cb10.TabIndex = 2;
            cb10.Text = "dx.mouse.cursor";
            cb10.UseVisualStyleBackColor = true;
            cb10.CheckedChanged += CheckBoxBus;
            // 
            // cb9
            // 
            cb9.AutoSize = true;
            cb9.Location = new Point(18, 130);
            cb9.Name = "cb9";
            cb9.Size = new Size(96, 16);
            cb9.TabIndex = 2;
            cb9.Text = "dx.mouse.api";
            cb9.UseVisualStyleBackColor = true;
            cb9.CheckedChanged += CheckBoxBus;
            // 
            // cb8
            // 
            cb8.AutoSize = true;
            cb8.Location = new Point(231, 108);
            cb8.Name = "cb8";
            cb8.Size = new Size(156, 16);
            cb8.TabIndex = 2;
            cb8.Text = "dx.mouse.state.message";
            cb8.UseVisualStyleBackColor = true;
            cb8.CheckedChanged += CheckBoxBus;
            // 
            // cb7
            // 
            cb7.AutoSize = true;
            cb7.Location = new Point(18, 108);
            cb7.Name = "cb7";
            cb7.Size = new Size(132, 16);
            cb7.TabIndex = 2;
            cb7.Text = "dx.mouse.state.api";
            cb7.UseVisualStyleBackColor = true;
            cb7.CheckedChanged += CheckBoxBus;
            // 
            // cb6
            // 
            cb6.AutoSize = true;
            cb6.Location = new Point(231, 86);
            cb6.Name = "cb6";
            cb6.Size = new Size(162, 16);
            cb6.TabIndex = 2;
            cb6.Text = "dx.mouse.input.lock.api";
            cb6.UseVisualStyleBackColor = true;
            cb6.CheckedChanged += CheckBoxBus;
            // 
            // cb5
            // 
            cb5.AutoSize = true;
            cb5.Location = new Point(18, 86);
            cb5.Name = "cb5";
            cb5.Size = new Size(156, 16);
            cb5.TabIndex = 2;
            cb5.Text = "dx.mouse.clip.lock.api";
            cb5.UseVisualStyleBackColor = true;
            cb5.CheckedChanged += CheckBoxBus;
            // 
            // cb4
            // 
            cb4.AutoSize = true;
            cb4.Location = new Point(231, 64);
            cb4.Name = "cb4";
            cb4.Size = new Size(192, 16);
            cb4.TabIndex = 2;
            cb4.Text = "dx.mouse.focus.input.message";
            cb4.UseVisualStyleBackColor = true;
            cb4.CheckedChanged += CheckBoxBus;
            // 
            // cb2
            // 
            cb2.AutoSize = true;
            cb2.Location = new Point(231, 42);
            cb2.Name = "cb2";
            cb2.Size = new Size(204, 16);
            cb2.TabIndex = 2;
            cb2.Text = "dx.mouse.position.lock.message";
            cb2.UseVisualStyleBackColor = true;
            cb2.CheckedChanged += CheckBoxBus;
            // 
            // cb3
            // 
            cb3.AutoSize = true;
            cb3.Location = new Point(18, 64);
            cb3.Name = "cb3";
            cb3.Size = new Size(168, 16);
            cb3.TabIndex = 2;
            cb3.Text = "dx.mouse.focus.input.api";
            cb3.UseVisualStyleBackColor = true;
            cb3.CheckedChanged += CheckBoxBus;
            // 
            // cbBus
            // 
            cbBus.AutoSize = true;
            cbBus.Location = new Point(18, 42);
            cbBus.Name = "cbBus";
            cbBus.Size = new Size(180, 16);
            cbBus.TabIndex = 2;
            cbBus.Text = "dx.mouse.position.lock.api";
            cbBus.UseVisualStyleBackColor = true;
            cbBus.CheckedChanged += CheckBoxBus;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(201, 20);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(71, 16);
            radioButton3.TabIndex = 1;
            radioButton3.TabStop = true;
            radioButton3.Text = "windows3";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += RadioButtonBus;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(105, 20);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(65, 16);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "windows";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += RadioButtonBus;
            // 
            // rbBus
            // 
            rbBus.AutoSize = true;
            rbBus.Checked = true;
            rbBus.Location = new Point(18, 20);
            rbBus.Name = "rbBus";
            rbBus.Size = new Size(59, 16);
            rbBus.TabIndex = 0;
            rbBus.TabStop = true;
            rbBus.Text = "normal";
            rbBus.UseVisualStyleBackColor = true;
            rbBus.CheckedChanged += RadioButtonBus;
            // 
            // btnSure
            // 
            btnSure.Location = new Point(142, 265);
            btnSure.Name = "btnSure";
            btnSure.Size = new Size(75, 23);
            btnSure.TabIndex = 1;
            btnSure.Text = "确定";
            btnSure.UseVisualStyleBackColor = true;
            btnSure.Click += btnSure_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(250, 265);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSelectDxMouse
            // 
            btnSelectDxMouse.Location = new Point(12, 225);
            btnSelectDxMouse.Name = "btnSelectDxMouse";
            btnSelectDxMouse.Size = new Size(134, 23);
            btnSelectDxMouse.TabIndex = 1;
            btnSelectDxMouse.Text = "勾选常用的dx鼠标属性";
            btnSelectDxMouse.UseVisualStyleBackColor = true;
            btnSelectDxMouse.Click += btnSelectDxMouse_Click;
            // 
            // FrmBindWindowMouse
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 300);
            Controls.Add(btnSelectDxMouse);
            Controls.Add(btnCancel);
            Controls.Add(btnSure);
            Controls.Add(groupBox1);
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmBindWindowMouse";
            StartPosition = FormStartPosition.CenterParent;
            Load += FrmBindWindowMouse_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb12;
        private System.Windows.Forms.CheckBox cb13;
        private System.Windows.Forms.CheckBox cb11;
        private System.Windows.Forms.CheckBox cb10;
        private System.Windows.Forms.CheckBox cb9;
        private System.Windows.Forms.CheckBox cb8;
        private System.Windows.Forms.CheckBox cb7;
        private System.Windows.Forms.CheckBox cb6;
        private System.Windows.Forms.CheckBox cb5;
        private System.Windows.Forms.CheckBox cb4;
        private System.Windows.Forms.CheckBox cb2;
        private System.Windows.Forms.CheckBox cb3;
        private System.Windows.Forms.CheckBox cbBus;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton rbBus;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelectDxMouse;
    }
}