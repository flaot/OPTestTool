namespace WordDictTool
{
    partial class ScreenshotForm
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
            sizeLabel = new Label();
            magnifier = new MagnifierControl();
            SuspendLayout();
            // 
            // sizeLabel
            // 
            sizeLabel.AutoSize = true;
            sizeLabel.BackColor = Color.White;
            sizeLabel.BorderStyle = BorderStyle.FixedSingle;
            sizeLabel.Location = new Point(149, 155);
            sizeLabel.Name = "sizeLabel";
            sizeLabel.Padding = new Padding(5);
            sizeLabel.Size = new Size(76, 29);
            sizeLabel.TabIndex = 1;
            sizeLabel.Text = "100 x 100";
            // 
            // magnifierControl1
            // 
            magnifier.BorderStyle = BorderStyle.FixedSingle;
            magnifier.Location = new Point(302, 232);
            magnifier.Magnification = 8;
            magnifier.Name = "magnifierControl1";
            magnifier.Size = new Size(128, 188);
            magnifier.TabIndex = 2;
            // 
            // ScreenshotForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            ControlBox = false;
            Controls.Add(magnifier);
            Controls.Add(sizeLabel);
            Cursor = Cursors.Cross;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ScreenshotForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "ScreenshotForm";
            TopMost = true;
            FormClosing += ScreenshotForm_FormClosing;
            Load += ScreenshotForm_Load;
            KeyDown += ScreenshotForm_KeyDown;
            MouseDoubleClick += ScreenshotForm_MouseDoubleClick;
            MouseDown += ScreenshotForm_MouseDown;
            MouseLeave += ScreenshotForm_MouseLeave;
            MouseMove += ScreenshotForm_MouseMove;
            MouseUp += ScreenshotForm_MouseUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label sizeLabel;
        private MagnifierControl magnifier;
    }
}