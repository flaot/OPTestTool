
namespace ScriptTestTools.View.TestPicColor
{
    partial class FrmTestPicColorShowPic
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
            pbShowPic = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbShowPic).BeginInit();
            SuspendLayout();
            // 
            // pbShowPic
            // 
            pbShowPic.Dock = DockStyle.Fill;
            pbShowPic.Location = new Point(0, 0);
            pbShowPic.Name = "pbShowPic";
            pbShowPic.Size = new Size(800, 450);
            pbShowPic.SizeMode = PictureBoxSizeMode.AutoSize;
            pbShowPic.TabIndex = 0;
            pbShowPic.TabStop = false;
            pbShowPic.Paint += pbShowPic_Paint;
            pbShowPic.MouseClick += pbShowPic_MouseClick;
            // 
            // FrmTestPicColorShowPic
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(800, 450);
            Controls.Add(pbShowPic);
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTestPicColorShowPic";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " 按F1查看帮助/F5刷新";
            FormClosed += FrmTestPicColorShowPic_FormClosed;
            Load += FrmTestPicColorShowPic_Load;
            KeyDown += FrmTestPicColorShowPic_KeyDown;
            KeyUp += FrmTestPicColorShowPic_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pbShowPic).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pbShowPic;
    }
}