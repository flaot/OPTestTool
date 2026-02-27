namespace WindowFinder
{
    partial class HighlightOverlayForm
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
            components = new System.ComponentModel.Container();
            animationTimer = new System.Windows.Forms.Timer(components);
            destryTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // animationTimer
            // 
            animationTimer.Interval = 20;
            animationTimer.Tick += AnimationTimer_Tick;
            // 
            // destryTimer
            // 
            destryTimer.Tick += DestryTimer_Tick;
            // 
            // HighlightOverlayForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "HighlightOverlayForm";
            ShowInTaskbar = false;
            Text = "HighlightOverlayForm";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Timer destryTimer;
    }
}