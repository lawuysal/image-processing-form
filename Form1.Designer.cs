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
            button1 = new Button();
            panel1 = new Panel();
            zoomBtn = new Button();
            trackBar1 = new TrackBar();
            panel2 = new Panel();
            panel3 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
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
            // button1
            // 
            button1.Location = new Point(122, 19);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(83, 83, 83);
            panel1.Controls.Add(zoomBtn);
            panel1.Controls.Add(trackBar1);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(43, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1265, 125);
            panel1.TabIndex = 3;
            // 
            // zoomBtn
            // 
            zoomBtn.Location = new Point(122, 60);
            zoomBtn.Name = "zoomBtn";
            zoomBtn.Size = new Size(94, 29);
            zoomBtn.TabIndex = 3;
            zoomBtn.Text = "Zoom";
            zoomBtn.UseVisualStyleBackColor = true;
            zoomBtn.Click += zoomBtn_Click;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(435, 33);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(275, 56);
            trackBar1.TabIndex = 2;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(83, 83, 83);
            panel2.Location = new Point(976, 143);
            panel2.Name = "panel2";
            panel2.Size = new Size(332, 668);
            panel2.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.BackColor = Color.FromArgb(40, 40, 40);
            panel3.Controls.Add(pictureBox);
            panel3.Location = new Point(43, 143);
            panel3.Name = "panel3";
            panel3.Size = new Size(927, 668);
            panel3.TabIndex = 5;
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
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox;
        private Button button1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private TrackBar trackBar1;
        private Button zoomBtn;
    }
}