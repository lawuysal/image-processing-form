namespace image_processing_form
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            panel1 = new Panel();
            histogramBtn = new Button();
            binarizeBtn = new Button();
            makeGrayBtn = new Button();
            blackWhiteBtn = new Button();
            contrastButton = new Button();
            brightnessBtn = new Button();
            zoomBtn = new Button();
            trackBar1 = new TrackBar();
            applyButton = new Button();
            panel2 = new Panel();
            exportBtn = new Button();
            openFileBtn = new Button();
            label2 = new Label();
            historyPanel = new Panel();
            panel4 = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            undoBtn = new Button();
            panel3 = new Panel();
            histGraph = new ScottPlot.WinForms.FormsPlot();
            cropBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            panel2.SuspendLayout();
            historyPanel.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BackColor = Color.FromArgb(40, 40, 40);
            pictureBox.Location = new Point(3, 3);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(921, 662);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(83, 83, 83);
            panel1.Controls.Add(cropBtn);
            panel1.Controls.Add(histogramBtn);
            panel1.Controls.Add(binarizeBtn);
            panel1.Controls.Add(makeGrayBtn);
            panel1.Controls.Add(blackWhiteBtn);
            panel1.Controls.Add(contrastButton);
            panel1.Controls.Add(brightnessBtn);
            panel1.Controls.Add(zoomBtn);
            panel1.Controls.Add(trackBar1);
            panel1.Location = new Point(43, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1265, 125);
            panel1.TabIndex = 3;
            // 
            // histogramBtn
            // 
            histogramBtn.Location = new Point(35, 74);
            histogramBtn.Name = "histogramBtn";
            histogramBtn.Size = new Size(94, 29);
            histogramBtn.TabIndex = 9;
            histogramBtn.Text = "Histogram";
            histogramBtn.UseVisualStyleBackColor = true;
            histogramBtn.Click += histogramBtn_Click;
            // 
            // binarizeBtn
            // 
            binarizeBtn.Location = new Point(435, 74);
            binarizeBtn.Name = "binarizeBtn";
            binarizeBtn.Size = new Size(94, 29);
            binarizeBtn.TabIndex = 8;
            binarizeBtn.Text = "Binarize";
            binarizeBtn.UseVisualStyleBackColor = true;
            binarizeBtn.Click += binarizeBtn_Click;
            // 
            // makeGrayBtn
            // 
            makeGrayBtn.Location = new Point(557, 22);
            makeGrayBtn.Name = "makeGrayBtn";
            makeGrayBtn.Size = new Size(94, 29);
            makeGrayBtn.TabIndex = 7;
            makeGrayBtn.Text = "MakeGray";
            makeGrayBtn.UseVisualStyleBackColor = true;
            makeGrayBtn.Click += makeGrayBtn_Click;
            // 
            // blackWhiteBtn
            // 
            blackWhiteBtn.Location = new Point(557, 74);
            blackWhiteBtn.Name = "blackWhiteBtn";
            blackWhiteBtn.Size = new Size(94, 29);
            blackWhiteBtn.TabIndex = 6;
            blackWhiteBtn.Text = "BlackWhite";
            blackWhiteBtn.UseVisualStyleBackColor = true;
            blackWhiteBtn.Click += blackWhiteBtn_Click;
            // 
            // contrastButton
            // 
            contrastButton.Location = new Point(335, 22);
            contrastButton.Name = "contrastButton";
            contrastButton.Size = new Size(94, 29);
            contrastButton.TabIndex = 5;
            contrastButton.Text = "Contrast";
            contrastButton.UseVisualStyleBackColor = true;
            contrastButton.Click += contrastButton_Click;
            // 
            // brightnessBtn
            // 
            brightnessBtn.Location = new Point(335, 74);
            brightnessBtn.Name = "brightnessBtn";
            brightnessBtn.Size = new Size(94, 29);
            brightnessBtn.TabIndex = 4;
            brightnessBtn.Text = "Brightness";
            brightnessBtn.UseVisualStyleBackColor = true;
            brightnessBtn.Click += brightnessBtn_Click;
            // 
            // zoomBtn
            // 
            zoomBtn.Location = new Point(35, 22);
            zoomBtn.Name = "zoomBtn";
            zoomBtn.Size = new Size(94, 29);
            zoomBtn.TabIndex = 3;
            zoomBtn.Text = "Zoom";
            zoomBtn.UseVisualStyleBackColor = true;
            zoomBtn.Click += zoomBtn_Click;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(956, 47);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(275, 56);
            trackBar1.TabIndex = 2;
            trackBar1.Visible = false;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // applyButton
            // 
            applyButton.Location = new Point(174, 18);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(94, 29);
            applyButton.TabIndex = 0;
            applyButton.Text = "Apply";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(83, 83, 83);
            panel2.Controls.Add(exportBtn);
            panel2.Controls.Add(openFileBtn);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(historyPanel);
            panel2.Controls.Add(undoBtn);
            panel2.Controls.Add(applyButton);
            panel2.Location = new Point(976, 143);
            panel2.Name = "panel2";
            panel2.Size = new Size(332, 668);
            panel2.TabIndex = 4;
            // 
            // exportBtn
            // 
            exportBtn.Location = new Point(174, 102);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(94, 29);
            exportBtn.TabIndex = 5;
            exportBtn.Text = "Export";
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += exportBtn_Click;
            // 
            // openFileBtn
            // 
            openFileBtn.Location = new Point(39, 102);
            openFileBtn.Name = "openFileBtn";
            openFileBtn.Size = new Size(94, 29);
            openFileBtn.TabIndex = 4;
            openFileBtn.Text = "Import";
            openFileBtn.UseVisualStyleBackColor = true;
            openFileBtn.Click += openFileBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(29, 177);
            label2.Name = "label2";
            label2.Size = new Size(59, 20);
            label2.TabIndex = 3;
            label2.Text = "History:";
            // 
            // historyPanel
            // 
            historyPanel.AutoScroll = true;
            historyPanel.AutoScrollMargin = new Size(10, 10);
            historyPanel.BackColor = Color.FromArgb(66, 66, 66);
            historyPanel.Controls.Add(panel4);
            historyPanel.Location = new Point(26, 200);
            historyPanel.Name = "historyPanel";
            historyPanel.Size = new Size(283, 451);
            historyPanel.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(83, 83, 83);
            panel4.Controls.Add(pictureBox1);
            panel4.Controls.Add(label1);
            panel4.Location = new Point(12, 12);
            panel4.Name = "panel4";
            panel4.Size = new Size(260, 100);
            panel4.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.DimGray;
            pictureBox1.Location = new Point(15, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(80, 80);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(120, 40);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // undoBtn
            // 
            undoBtn.Location = new Point(39, 18);
            undoBtn.Name = "undoBtn";
            undoBtn.Size = new Size(94, 29);
            undoBtn.TabIndex = 1;
            undoBtn.Text = "Undo";
            undoBtn.UseVisualStyleBackColor = true;
            undoBtn.Click += undoBtn_Click;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.BackColor = Color.FromArgb(40, 40, 40);
            panel3.Controls.Add(histGraph);
            panel3.Controls.Add(pictureBox);
            panel3.Location = new Point(43, 143);
            panel3.Name = "panel3";
            panel3.Size = new Size(927, 668);
            panel3.TabIndex = 5;
            // 
            // histGraph
            // 
            histGraph.DisplayScale = 1.25F;
            histGraph.Location = new Point(0, 343);
            histGraph.Name = "histGraph";
            histGraph.Size = new Size(429, 322);
            histGraph.TabIndex = 9;
            // 
            // cropBtn
            // 
            cropBtn.Location = new Point(150, 22);
            cropBtn.Name = "cropBtn";
            cropBtn.Size = new Size(94, 29);
            cropBtn.TabIndex = 10;
            cropBtn.Text = "Crop";
            cropBtn.UseVisualStyleBackColor = true;
            cropBtn.Click += cropBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(66, 66, 66);
            ClientSize = new Size(1352, 838);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            KeyPreview = true;
            Name = "Form1";
            Text = "Form1";
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            historyPanel.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private TrackBar trackBar1;
        private Button zoomBtn;
        private Button brightnessBtn;
        private Button contrastButton;
        private Button blackWhiteBtn;
        private Button applyButton;
        private Button undoBtn;
        private Panel historyPanel;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Panel panel4;
        private Button openFileBtn;
        private Button exportBtn;
        private Button makeGrayBtn;
        private Button binarizeBtn;
        private ScottPlot.WinForms.FormsPlot histGraph;
        private Button histogramBtn;
        private Button cropBtn;
    }
}