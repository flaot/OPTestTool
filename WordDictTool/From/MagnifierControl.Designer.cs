namespace WordDictTool
{
    partial class MagnifierControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            coordinateLabel = new Label();
            splitContainer1 = new SplitContainer();
            pictureBox = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // coordinateLabel
            // 
            coordinateLabel.AutoSize = true;
            coordinateLabel.Location = new Point(8, 5);
            coordinateLabel.Name = "coordinateLabel";
            coordinateLabel.Size = new Size(86, 51);
            coordinateLabel.TabIndex = 1;
            coordinateLabel.Text = "坐标：(0, 0)\r\n色值：000000\r\n按C复制色号";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pictureBox);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(coordinateLabel);
            splitContainer1.Size = new Size(128, 188);
            splitContainer1.SplitterDistance = 128;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.TabIndex = 3;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.ActiveCaptionText;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(128, 128);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // timer1
            // 
            timer1.Interval = 20;
            timer1.Tick += Timer_Tick;
            // 
            // MagnifierControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(splitContainer1);
            Name = "MagnifierControl";
            Size = new Size(128, 188);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label coordinateLabel;
        private SplitContainer splitContainer1;
        private PictureBox pictureBox;
        private System.Windows.Forms.Timer timer1;
    }
}
