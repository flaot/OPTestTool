namespace OPTestTool
{
    partial class GetScreenDataBmpForm
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
            pictureBox1 = new PictureBox();
            Txt_Tip = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(307, 144);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Txt_Tip
            // 
            Txt_Tip.ForeColor = Color.Red;
            Txt_Tip.Location = new Point(79, 56);
            Txt_Tip.Name = "Txt_Tip";
            Txt_Tip.Size = new Size(155, 23);
            Txt_Tip.TabIndex = 1;
            Txt_Tip.Text = "未绑定窗口";
            Txt_Tip.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GetScreenDataBmpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(307, 144);
            Controls.Add(Txt_Tip);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GetScreenDataBmpForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "GetScreenDataBmp";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label Txt_Tip;
    }
}