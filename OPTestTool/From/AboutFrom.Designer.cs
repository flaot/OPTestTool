namespace OPTestTool
{
    partial class AboutFrom
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
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(355, 185);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "OPExort 是个生成调用op功能的胶水代码生成器\n\n注意:生成的代码不可直接与OP的dll配合使用 \n代码非调用com组件的代码 \n而是类似于调用dll中的win32API的实现 \n使用的dll需要往原始OP代码中注入C函数以供调用 \n也可以使用测试工具附带dll\n";
            // 
            // AboutFrom
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 209);
            Controls.Add(richTextBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutFrom";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "关于";
            TopMost = true;
            Load += AboutFrom_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
    }
}