namespace image_processing_form
{
    partial class ImageProcessingForm
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
            resizeBtn = new Button();
            interpolateY = new NumericUpDown();
            interpolateX = new NumericUpDown();
            ExitButton = new Button();
            panel8 = new Panel();
            erodeBtn = new Button();
            histogramBtn = new Button();
            colorSpaceComboBox = new ComboBox();
            closeBtn = new Button();
            RGBBtn = new Button();
            openBtn = new Button();
            stretchBtn = new Button();
            dilateBtn = new Button();
            panel7 = new Panel();
            interpolationBtn = new Button();
            convoluteBtn = new Button();
            filterBtn = new Button();
            meanFilterBtn = new Button();
            saltAndPepperBtn = new Button();
            medFilterBtn = new Button();
            panel6 = new Panel();
            contrastBtn = new Button();
            adaptiveThresholdBtn = new Button();
            brightnessBtn = new Button();
            makeGrayBtn = new Button();
            binarizeBtn = new Button();
            edgeDetectBtn = new Button();
            panel5 = new Panel();
            zoomBtn = new Button();
            cropBtn = new Button();
            multiplyBtn = new Button();
            addBtn = new Button();
            shiftBtn = new Button();
            rotateBtn = new Button();
            trackbar2Label = new Label();
            trackbar1Label = new Label();
            trackBar2 = new TrackBar();
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
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)interpolateY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)interpolateX).BeginInit();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
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
            pictureBox.Margin = new Padding(4, 2, 4, 2);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(921, 662);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(83, 83, 83);
            panel1.Controls.Add(resizeBtn);
            panel1.Controls.Add(interpolateY);
            panel1.Controls.Add(interpolateX);
            panel1.Controls.Add(ExitButton);
            panel1.Controls.Add(panel8);
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(trackbar2Label);
            panel1.Controls.Add(trackbar1Label);
            panel1.Controls.Add(trackBar2);
            panel1.Controls.Add(trackBar1);
            panel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            panel1.Location = new Point(3, 10);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1632, 150);
            panel1.TabIndex = 3;
            // 
            // resizeBtn
            // 
            resizeBtn.BackColor = Color.FromArgb(40, 40, 40);
            resizeBtn.FlatStyle = FlatStyle.Flat;
            resizeBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            resizeBtn.ForeColor = SystemColors.Control;
            resizeBtn.Location = new Point(1415, 49);
            resizeBtn.Margin = new Padding(4, 2, 4, 2);
            resizeBtn.Name = "resizeBtn";
            resizeBtn.Size = new Size(100, 50);
            resizeBtn.TabIndex = 6;
            resizeBtn.Text = "Resize";
            resizeBtn.UseVisualStyleBackColor = false;
            resizeBtn.Click += resizeBtn_Click;
            // 
            // interpolateY
            // 
            interpolateY.Location = new Point(1234, 81);
            interpolateY.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            interpolateY.Name = "interpolateY";
            interpolateY.Size = new Size(150, 27);
            interpolateY.TabIndex = 35;
            // 
            // interpolateX
            // 
            interpolateX.Location = new Point(1234, 48);
            interpolateX.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            interpolateX.Name = "interpolateX";
            interpolateX.Size = new Size(150, 27);
            interpolateX.TabIndex = 34;
            // 
            // ExitButton
            // 
            ExitButton.BackColor = Color.FromArgb(40, 40, 40);
            ExitButton.FlatStyle = FlatStyle.Popup;
            ExitButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ExitButton.ForeColor = SystemColors.Control;
            ExitButton.Location = new Point(1596, 13);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(24, 25);
            ExitButton.TabIndex = 33;
            ExitButton.Text = "X";
            ExitButton.UseVisualStyleBackColor = false;
            ExitButton.Click += ExitButton_Click;
            // 
            // panel8
            // 
            panel8.BackColor = Color.FromArgb(40, 40, 40);
            panel8.BorderStyle = BorderStyle.Fixed3D;
            panel8.Controls.Add(erodeBtn);
            panel8.Controls.Add(histogramBtn);
            panel8.Controls.Add(colorSpaceComboBox);
            panel8.Controls.Add(closeBtn);
            panel8.Controls.Add(RGBBtn);
            panel8.Controls.Add(openBtn);
            panel8.Controls.Add(stretchBtn);
            panel8.Controls.Add(dilateBtn);
            panel8.Location = new Point(848, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(358, 150);
            panel8.TabIndex = 22;
            // 
            // erodeBtn
            // 
            erodeBtn.BackColor = Color.FromArgb(40, 40, 40);
            erodeBtn.FlatStyle = FlatStyle.Flat;
            erodeBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            erodeBtn.ForeColor = SystemColors.Control;
            erodeBtn.Location = new Point(13, 58);
            erodeBtn.Margin = new Padding(4, 2, 4, 2);
            erodeBtn.Name = "erodeBtn";
            erodeBtn.Size = new Size(124, 25);
            erodeBtn.TabIndex = 26;
            erodeBtn.Text = "Erode";
            erodeBtn.UseVisualStyleBackColor = false;
            erodeBtn.Click += erodeBtn_Click;
            // 
            // histogramBtn
            // 
            histogramBtn.BackColor = Color.FromArgb(40, 40, 40);
            histogramBtn.FlatStyle = FlatStyle.Flat;
            histogramBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            histogramBtn.ForeColor = SystemColors.Control;
            histogramBtn.Location = new Point(253, 78);
            histogramBtn.Margin = new Padding(4, 2, 4, 2);
            histogramBtn.Name = "histogramBtn";
            histogramBtn.Size = new Size(96, 46);
            histogramBtn.TabIndex = 9;
            histogramBtn.Text = "Histogram";
            histogramBtn.UseVisualStyleBackColor = false;
            histogramBtn.Click += histogramBtn_Click;
            // 
            // colorSpaceComboBox
            // 
            colorSpaceComboBox.BackColor = Color.FromArgb(40, 40, 40);
            colorSpaceComboBox.FlatStyle = FlatStyle.Flat;
            colorSpaceComboBox.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            colorSpaceComboBox.ForeColor = SystemColors.Control;
            colorSpaceComboBox.FormattingEnabled = true;
            colorSpaceComboBox.Location = new Point(156, 47);
            colorSpaceComboBox.Margin = new Padding(4, 2, 4, 2);
            colorSpaceComboBox.Name = "colorSpaceComboBox";
            colorSpaceComboBox.Size = new Size(190, 25);
            colorSpaceComboBox.TabIndex = 22;
            // 
            // closeBtn
            // 
            closeBtn.BackColor = Color.FromArgb(40, 40, 40);
            closeBtn.FlatStyle = FlatStyle.Flat;
            closeBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            closeBtn.ForeColor = SystemColors.Control;
            closeBtn.Location = new Point(12, 98);
            closeBtn.Margin = new Padding(4, 2, 4, 2);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(63, 25);
            closeBtn.TabIndex = 28;
            closeBtn.Text = "Close";
            closeBtn.UseVisualStyleBackColor = false;
            closeBtn.Click += closeBtn_Click;
            // 
            // RGBBtn
            // 
            RGBBtn.BackColor = Color.FromArgb(40, 40, 40);
            RGBBtn.FlatStyle = FlatStyle.Flat;
            RGBBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            RGBBtn.ForeColor = SystemColors.Control;
            RGBBtn.Location = new Point(156, 18);
            RGBBtn.Margin = new Padding(4, 2, 4, 2);
            RGBBtn.Name = "RGBBtn";
            RGBBtn.Size = new Size(190, 25);
            RGBBtn.TabIndex = 23;
            RGBBtn.Text = "Color Space";
            RGBBtn.UseVisualStyleBackColor = false;
            RGBBtn.Click += RGBButton_Click;
            // 
            // openBtn
            // 
            openBtn.BackColor = Color.FromArgb(40, 40, 40);
            openBtn.FlatStyle = FlatStyle.Flat;
            openBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            openBtn.ForeColor = SystemColors.Control;
            openBtn.Location = new Point(76, 98);
            openBtn.Margin = new Padding(4, 2, 4, 2);
            openBtn.Name = "openBtn";
            openBtn.Size = new Size(62, 26);
            openBtn.TabIndex = 27;
            openBtn.Text = "Open";
            openBtn.UseVisualStyleBackColor = false;
            openBtn.Click += openBtn_Click;
            // 
            // stretchBtn
            // 
            stretchBtn.BackColor = Color.FromArgb(40, 40, 40);
            stretchBtn.FlatStyle = FlatStyle.Flat;
            stretchBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            stretchBtn.ForeColor = SystemColors.Control;
            stretchBtn.Location = new Point(154, 79);
            stretchBtn.Margin = new Padding(4, 2, 4, 2);
            stretchBtn.Name = "stretchBtn";
            stretchBtn.Size = new Size(96, 44);
            stretchBtn.TabIndex = 24;
            stretchBtn.Text = "Stretch";
            stretchBtn.UseVisualStyleBackColor = false;
            stretchBtn.Click += btnStretch_Click;
            // 
            // dilateBtn
            // 
            dilateBtn.BackColor = Color.FromArgb(40, 40, 40);
            dilateBtn.FlatStyle = FlatStyle.Flat;
            dilateBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            dilateBtn.ForeColor = SystemColors.Control;
            dilateBtn.Location = new Point(13, 18);
            dilateBtn.Margin = new Padding(4, 2, 4, 2);
            dilateBtn.Name = "dilateBtn";
            dilateBtn.Size = new Size(124, 25);
            dilateBtn.TabIndex = 25;
            dilateBtn.Text = "Dilate";
            dilateBtn.UseVisualStyleBackColor = false;
            dilateBtn.Click += dilateBtn_Click;
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(40, 40, 40);
            panel7.BorderStyle = BorderStyle.Fixed3D;
            panel7.Controls.Add(interpolationBtn);
            panel7.Controls.Add(convoluteBtn);
            panel7.Controls.Add(filterBtn);
            panel7.Controls.Add(meanFilterBtn);
            panel7.Controls.Add(saltAndPepperBtn);
            panel7.Controls.Add(medFilterBtn);
            panel7.Location = new Point(566, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(280, 150);
            panel7.TabIndex = 32;
            // 
            // interpolationBtn
            // 
            interpolationBtn.BackColor = Color.FromArgb(40, 40, 40);
            interpolationBtn.FlatStyle = FlatStyle.Flat;
            interpolationBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            interpolationBtn.ForeColor = SystemColors.Control;
            interpolationBtn.Location = new Point(142, 98);
            interpolationBtn.Margin = new Padding(4, 2, 4, 2);
            interpolationBtn.Name = "interpolationBtn";
            interpolationBtn.Size = new Size(124, 25);
            interpolationBtn.TabIndex = 22;
            interpolationBtn.Text = "Interpolation";
            interpolationBtn.UseVisualStyleBackColor = false;
            interpolationBtn.Click += interpolationBtn_Click_1;
            // 
            // convoluteBtn
            // 
            convoluteBtn.BackColor = Color.FromArgb(40, 40, 40);
            convoluteBtn.FlatStyle = FlatStyle.Flat;
            convoluteBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            convoluteBtn.ForeColor = SystemColors.Control;
            convoluteBtn.Location = new Point(142, 58);
            convoluteBtn.Margin = new Padding(4, 2, 4, 2);
            convoluteBtn.Name = "convoluteBtn";
            convoluteBtn.Size = new Size(124, 25);
            convoluteBtn.TabIndex = 17;
            convoluteBtn.Text = "Convolute";
            convoluteBtn.UseVisualStyleBackColor = false;
            convoluteBtn.Click += convoluteBtn_Click;
            // 
            // filterBtn
            // 
            filterBtn.BackColor = Color.FromArgb(40, 40, 40);
            filterBtn.FlatStyle = FlatStyle.Flat;
            filterBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            filterBtn.ForeColor = SystemColors.Control;
            filterBtn.Location = new Point(10, 58);
            filterBtn.Margin = new Padding(4, 2, 4, 2);
            filterBtn.Name = "filterBtn";
            filterBtn.Size = new Size(124, 25);
            filterBtn.TabIndex = 16;
            filterBtn.Text = "Filter";
            filterBtn.UseVisualStyleBackColor = false;
            filterBtn.Click += filterBtn_Click;
            // 
            // meanFilterBtn
            // 
            meanFilterBtn.BackColor = Color.FromArgb(40, 40, 40);
            meanFilterBtn.FlatStyle = FlatStyle.Flat;
            meanFilterBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            meanFilterBtn.ForeColor = SystemColors.Control;
            meanFilterBtn.Location = new Point(10, 18);
            meanFilterBtn.Margin = new Padding(4, 2, 4, 2);
            meanFilterBtn.Name = "meanFilterBtn";
            meanFilterBtn.Size = new Size(124, 25);
            meanFilterBtn.TabIndex = 7;
            meanFilterBtn.Text = "MeanFilter";
            meanFilterBtn.UseVisualStyleBackColor = false;
            meanFilterBtn.Click += meanFilterBtn_Click;
            // 
            // saltAndPepperBtn
            // 
            saltAndPepperBtn.BackColor = Color.FromArgb(40, 40, 40);
            saltAndPepperBtn.FlatStyle = FlatStyle.Flat;
            saltAndPepperBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            saltAndPepperBtn.ForeColor = SystemColors.Control;
            saltAndPepperBtn.Location = new Point(10, 98);
            saltAndPepperBtn.Margin = new Padding(4, 2, 4, 2);
            saltAndPepperBtn.Name = "saltAndPepperBtn";
            saltAndPepperBtn.Size = new Size(124, 25);
            saltAndPepperBtn.TabIndex = 6;
            saltAndPepperBtn.Text = "Salt And Pepper Noise";
            saltAndPepperBtn.UseVisualStyleBackColor = false;
            saltAndPepperBtn.Click += saltAndPepperBtn_Click;
            // 
            // medFilterBtn
            // 
            medFilterBtn.BackColor = Color.FromArgb(40, 40, 40);
            medFilterBtn.FlatStyle = FlatStyle.Flat;
            medFilterBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            medFilterBtn.ForeColor = SystemColors.Control;
            medFilterBtn.Location = new Point(142, 18);
            medFilterBtn.Margin = new Padding(4, 2, 4, 2);
            medFilterBtn.Name = "medFilterBtn";
            medFilterBtn.Size = new Size(124, 25);
            medFilterBtn.TabIndex = 21;
            medFilterBtn.Text = "MedianFilter";
            medFilterBtn.UseVisualStyleBackColor = false;
            medFilterBtn.Click += medFilterBtn_Click;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(40, 40, 40);
            panel6.BorderStyle = BorderStyle.Fixed3D;
            panel6.Controls.Add(contrastBtn);
            panel6.Controls.Add(adaptiveThresholdBtn);
            panel6.Controls.Add(brightnessBtn);
            panel6.Controls.Add(makeGrayBtn);
            panel6.Controls.Add(binarizeBtn);
            panel6.Controls.Add(edgeDetectBtn);
            panel6.Location = new Point(286, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(280, 150);
            panel6.TabIndex = 31;
            // 
            // contrastBtn
            // 
            contrastBtn.BackColor = Color.FromArgb(40, 40, 40);
            contrastBtn.FlatStyle = FlatStyle.Flat;
            contrastBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            contrastBtn.ForeColor = SystemColors.Control;
            contrastBtn.Location = new Point(10, 19);
            contrastBtn.Margin = new Padding(4, 2, 4, 2);
            contrastBtn.Name = "contrastBtn";
            contrastBtn.Size = new Size(124, 25);
            contrastBtn.TabIndex = 5;
            contrastBtn.Text = "Contrast";
            contrastBtn.UseVisualStyleBackColor = false;
            contrastBtn.Click += contrastButton_Click;
            // 
            // adaptiveThresholdBtn
            // 
            adaptiveThresholdBtn.BackColor = Color.FromArgb(40, 40, 40);
            adaptiveThresholdBtn.FlatStyle = FlatStyle.Flat;
            adaptiveThresholdBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            adaptiveThresholdBtn.ForeColor = SystemColors.Control;
            adaptiveThresholdBtn.Location = new Point(10, 99);
            adaptiveThresholdBtn.Margin = new Padding(4, 2, 4, 2);
            adaptiveThresholdBtn.Name = "adaptiveThresholdBtn";
            adaptiveThresholdBtn.Size = new Size(124, 25);
            adaptiveThresholdBtn.TabIndex = 29;
            adaptiveThresholdBtn.Text = "A-Threshold";
            adaptiveThresholdBtn.UseVisualStyleBackColor = false;
            adaptiveThresholdBtn.Click += adaptiveThresholdBtn_Click;
            // 
            // brightnessBtn
            // 
            brightnessBtn.BackColor = Color.FromArgb(40, 40, 40);
            brightnessBtn.FlatStyle = FlatStyle.Flat;
            brightnessBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            brightnessBtn.ForeColor = SystemColors.Control;
            brightnessBtn.Location = new Point(10, 59);
            brightnessBtn.Margin = new Padding(4, 2, 4, 2);
            brightnessBtn.Name = "brightnessBtn";
            brightnessBtn.Size = new Size(124, 25);
            brightnessBtn.TabIndex = 4;
            brightnessBtn.Text = "Brightness";
            brightnessBtn.UseVisualStyleBackColor = false;
            brightnessBtn.Click += brightnessBtn_Click;
            // 
            // makeGrayBtn
            // 
            makeGrayBtn.BackColor = Color.FromArgb(40, 40, 40);
            makeGrayBtn.FlatStyle = FlatStyle.Flat;
            makeGrayBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            makeGrayBtn.ForeColor = SystemColors.Control;
            makeGrayBtn.Location = new Point(144, 19);
            makeGrayBtn.Margin = new Padding(4, 2, 4, 2);
            makeGrayBtn.Name = "makeGrayBtn";
            makeGrayBtn.Size = new Size(124, 25);
            makeGrayBtn.TabIndex = 7;
            makeGrayBtn.Text = "MakeGray";
            makeGrayBtn.UseVisualStyleBackColor = false;
            makeGrayBtn.Click += makeGrayBtn_Click;
            // 
            // binarizeBtn
            // 
            binarizeBtn.BackColor = Color.FromArgb(40, 40, 40);
            binarizeBtn.FlatStyle = FlatStyle.Flat;
            binarizeBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            binarizeBtn.ForeColor = SystemColors.Control;
            binarizeBtn.Location = new Point(144, 59);
            binarizeBtn.Margin = new Padding(4, 2, 4, 2);
            binarizeBtn.Name = "binarizeBtn";
            binarizeBtn.Size = new Size(124, 25);
            binarizeBtn.TabIndex = 8;
            binarizeBtn.Text = "Binarize";
            binarizeBtn.UseVisualStyleBackColor = false;
            binarizeBtn.Click += binarizeBtn_Click;
            // 
            // edgeDetectBtn
            // 
            edgeDetectBtn.BackColor = Color.FromArgb(40, 40, 40);
            edgeDetectBtn.FlatStyle = FlatStyle.Flat;
            edgeDetectBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            edgeDetectBtn.ForeColor = SystemColors.Control;
            edgeDetectBtn.Location = new Point(144, 99);
            edgeDetectBtn.Margin = new Padding(4, 2, 4, 2);
            edgeDetectBtn.Name = "edgeDetectBtn";
            edgeDetectBtn.Size = new Size(124, 25);
            edgeDetectBtn.TabIndex = 20;
            edgeDetectBtn.Text = "EdgeDetect";
            edgeDetectBtn.UseVisualStyleBackColor = false;
            edgeDetectBtn.Click += edgeDetectBtn_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(40, 40, 40);
            panel5.BorderStyle = BorderStyle.Fixed3D;
            panel5.Controls.Add(zoomBtn);
            panel5.Controls.Add(cropBtn);
            panel5.Controls.Add(multiplyBtn);
            panel5.Controls.Add(addBtn);
            panel5.Controls.Add(shiftBtn);
            panel5.Controls.Add(rotateBtn);
            panel5.Location = new Point(3, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(284, 149);
            panel5.TabIndex = 30;
            // 
            // zoomBtn
            // 
            zoomBtn.BackColor = Color.FromArgb(40, 40, 40);
            zoomBtn.FlatStyle = FlatStyle.Flat;
            zoomBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            zoomBtn.ForeColor = SystemColors.Control;
            zoomBtn.Location = new Point(12, 18);
            zoomBtn.Margin = new Padding(4, 2, 4, 2);
            zoomBtn.Name = "zoomBtn";
            zoomBtn.Size = new Size(124, 25);
            zoomBtn.TabIndex = 3;
            zoomBtn.Text = "Zoom";
            zoomBtn.UseVisualStyleBackColor = false;
            zoomBtn.Click += zoomBtn_Click;
            // 
            // cropBtn
            // 
            cropBtn.BackColor = Color.FromArgb(40, 40, 40);
            cropBtn.FlatStyle = FlatStyle.Flat;
            cropBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            cropBtn.ForeColor = SystemColors.Control;
            cropBtn.Location = new Point(144, 18);
            cropBtn.Margin = new Padding(4, 2, 4, 2);
            cropBtn.Name = "cropBtn";
            cropBtn.Size = new Size(124, 25);
            cropBtn.TabIndex = 10;
            cropBtn.Text = "Crop";
            cropBtn.UseVisualStyleBackColor = false;
            cropBtn.Click += cropBtn_Click;
            // 
            // multiplyBtn
            // 
            multiplyBtn.BackColor = Color.FromArgb(40, 40, 40);
            multiplyBtn.FlatStyle = FlatStyle.Flat;
            multiplyBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            multiplyBtn.ForeColor = SystemColors.Control;
            multiplyBtn.Location = new Point(144, 98);
            multiplyBtn.Margin = new Padding(4, 2, 4, 2);
            multiplyBtn.Name = "multiplyBtn";
            multiplyBtn.Size = new Size(124, 25);
            multiplyBtn.TabIndex = 11;
            multiplyBtn.Text = "Multiply";
            multiplyBtn.UseVisualStyleBackColor = false;
            multiplyBtn.Click += multiplyBtn_Click;
            // 
            // addBtn
            // 
            addBtn.BackColor = Color.FromArgb(40, 40, 40);
            addBtn.FlatStyle = FlatStyle.Flat;
            addBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            addBtn.ForeColor = SystemColors.Control;
            addBtn.Location = new Point(12, 98);
            addBtn.Margin = new Padding(4, 2, 4, 2);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(124, 25);
            addBtn.TabIndex = 12;
            addBtn.Text = "Add";
            addBtn.UseVisualStyleBackColor = false;
            addBtn.Click += addBtn_Click;
            // 
            // shiftBtn
            // 
            shiftBtn.BackColor = Color.FromArgb(40, 40, 40);
            shiftBtn.FlatStyle = FlatStyle.Flat;
            shiftBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            shiftBtn.ForeColor = SystemColors.Control;
            shiftBtn.Location = new Point(12, 58);
            shiftBtn.Margin = new Padding(4, 2, 4, 2);
            shiftBtn.Name = "shiftBtn";
            shiftBtn.Size = new Size(124, 25);
            shiftBtn.TabIndex = 13;
            shiftBtn.Text = "Shift";
            shiftBtn.UseVisualStyleBackColor = false;
            shiftBtn.Click += shiftButton_Click;
            // 
            // rotateBtn
            // 
            rotateBtn.BackColor = Color.FromArgb(40, 40, 40);
            rotateBtn.FlatStyle = FlatStyle.Flat;
            rotateBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            rotateBtn.ForeColor = SystemColors.Control;
            rotateBtn.Location = new Point(144, 58);
            rotateBtn.Margin = new Padding(4, 2, 4, 2);
            rotateBtn.Name = "rotateBtn";
            rotateBtn.Size = new Size(124, 25);
            rotateBtn.TabIndex = 15;
            rotateBtn.Text = "Rotate";
            rotateBtn.UseVisualStyleBackColor = false;
            rotateBtn.Click += rotateButton_Click;
            // 
            // trackbar2Label
            // 
            trackbar2Label.AutoSize = true;
            trackbar2Label.ForeColor = SystemColors.ButtonFace;
            trackbar2Label.Location = new Point(1547, 59);
            trackbar2Label.Margin = new Padding(4, 0, 4, 0);
            trackbar2Label.Name = "trackbar2Label";
            trackbar2Label.Size = new Size(0, 20);
            trackbar2Label.TabIndex = 19;
            // 
            // trackbar1Label
            // 
            trackbar1Label.AutoSize = true;
            trackbar1Label.ForeColor = SystemColors.ButtonFace;
            trackbar1Label.Location = new Point(1547, 18);
            trackbar1Label.Margin = new Padding(4, 0, 4, 0);
            trackbar1Label.Name = "trackbar1Label";
            trackbar1Label.Size = new Size(0, 20);
            trackbar1Label.TabIndex = 18;
            // 
            // trackBar2
            // 
            trackBar2.BackColor = Color.FromArgb(40, 40, 40);
            trackBar2.Cursor = Cursors.Hand;
            trackBar2.Location = new Point(1231, 81);
            trackBar2.Margin = new Padding(4, 2, 4, 2);
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(344, 56);
            trackBar2.TabIndex = 14;
            trackBar2.Visible = false;
            trackBar2.Scroll += trackBar2_Scroll;
            // 
            // trackBar1
            // 
            trackBar1.BackColor = Color.FromArgb(40, 40, 40);
            trackBar1.Cursor = Cursors.Hand;
            trackBar1.Location = new Point(1231, 21);
            trackBar1.Margin = new Padding(4, 2, 4, 2);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(344, 56);
            trackBar1.TabIndex = 2;
            trackBar1.Visible = false;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // applyButton
            // 
            applyButton.BackColor = Color.FromArgb(40, 40, 40);
            applyButton.FlatStyle = FlatStyle.Flat;
            applyButton.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            applyButton.ForeColor = SystemColors.Control;
            applyButton.Location = new Point(206, 12);
            applyButton.Margin = new Padding(4, 2, 4, 2);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(100, 50);
            applyButton.TabIndex = 0;
            applyButton.Text = "Apply";
            applyButton.UseVisualStyleBackColor = false;
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
            panel2.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            panel2.Location = new Point(1220, 166);
            panel2.Margin = new Padding(4, 2, 4, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(416, 671);
            panel2.TabIndex = 4;
            // 
            // exportBtn
            // 
            exportBtn.BackColor = Color.FromArgb(40, 40, 40);
            exportBtn.FlatStyle = FlatStyle.Flat;
            exportBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            exportBtn.ForeColor = SystemColors.Control;
            exportBtn.Location = new Point(206, 80);
            exportBtn.Margin = new Padding(4, 2, 4, 2);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(100, 50);
            exportBtn.TabIndex = 5;
            exportBtn.Text = "Export";
            exportBtn.UseVisualStyleBackColor = false;
            exportBtn.Click += exportBtn_Click;
            // 
            // openFileBtn
            // 
            openFileBtn.BackColor = Color.FromArgb(40, 40, 40);
            openFileBtn.FlatStyle = FlatStyle.Flat;
            openFileBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            openFileBtn.ForeColor = SystemColors.Control;
            openFileBtn.Location = new Point(88, 80);
            openFileBtn.Margin = new Padding(4, 2, 4, 2);
            openFileBtn.Name = "openFileBtn";
            openFileBtn.Size = new Size(100, 50);
            openFileBtn.TabIndex = 4;
            openFileBtn.Text = "Import";
            openFileBtn.UseVisualStyleBackColor = false;
            openFileBtn.Click += openFileBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(36, 142);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(65, 20);
            label2.TabIndex = 3;
            label2.Text = "History:";
            // 
            // historyPanel
            // 
            historyPanel.AutoScroll = true;
            historyPanel.AutoScrollMargin = new Size(10, 10);
            historyPanel.BackColor = Color.FromArgb(66, 66, 66);
            historyPanel.Controls.Add(panel4);
            historyPanel.Location = new Point(32, 160);
            historyPanel.Margin = new Padding(4, 2, 4, 2);
            historyPanel.Name = "historyPanel";
            historyPanel.Size = new Size(354, 489);
            historyPanel.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(83, 83, 83);
            panel4.Controls.Add(pictureBox1);
            panel4.Controls.Add(label1);
            panel4.Location = new Point(16, 10);
            panel4.Margin = new Padding(4, 2, 4, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(324, 80);
            panel4.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.DimGray;
            pictureBox1.Location = new Point(19, 8);
            pictureBox1.Margin = new Padding(4, 2, 4, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 64);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(150, 32);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // undoBtn
            // 
            undoBtn.BackColor = Color.FromArgb(40, 40, 40);
            undoBtn.FlatStyle = FlatStyle.Flat;
            undoBtn.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            undoBtn.ForeColor = SystemColors.Control;
            undoBtn.Location = new Point(88, 12);
            undoBtn.Margin = new Padding(4, 2, 4, 2);
            undoBtn.Name = "undoBtn";
            undoBtn.Size = new Size(100, 50);
            undoBtn.TabIndex = 1;
            undoBtn.Text = "Undo";
            undoBtn.UseVisualStyleBackColor = false;
            undoBtn.Click += undoBtn_Click;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.BackColor = Color.FromArgb(40, 40, 40);
            panel3.Controls.Add(histGraph);
            panel3.Controls.Add(pictureBox);
            panel3.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            panel3.Location = new Point(127, 164);
            panel3.Margin = new Padding(4, 2, 4, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(933, 670);
            panel3.TabIndex = 5;
            // 
            // histGraph
            // 
            histGraph.DisplayScale = 1.25F;
            histGraph.Location = new Point(0, 417);
            histGraph.Margin = new Padding(4, 2, 4, 2);
            histGraph.Name = "histGraph";
            histGraph.Size = new Size(539, 248);
            histGraph.TabIndex = 9;
            // 
            // ImageProcessingForm
            // 
            AutoScaleDimensions = new SizeF(10F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(66, 66, 66);
            ClientSize = new Size(1647, 844);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Mongolian Baiti", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Margin = new Padding(4, 2, 4, 2);
            Name = "ImageProcessingForm";
            Text = "Form1";
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)interpolateY).EndInit();
            ((System.ComponentModel.ISupportInitialize)interpolateX).EndInit();
            panel8.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
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
        private Button contrastBtn;
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
        private Button multiplyBtn;
        private Button addBtn;
        private Button shiftBtn;
        private TrackBar trackBar2;
        private Button rotateBtn;
        private Button filterBtn;
        private Button convoluteBtn;
        private Label trackbar1Label;
        private Label trackbar2Label;
        private Button edgeDetectBtn;
        private Button saltAndPepperBtn;
        private Button meanFilterBtn;
        private Button medFilterBtn;
        private ComboBox colorSpaceComboBox;
        private Button stretchBtn;
        private Button RGBBtn;
        private Button adaptiveThresholdBtn;
        private Button closeBtn;
        private Button openBtn;
        private Button erodeBtn;
        private Button dilateBtn;
        private Panel panel5;
        private Panel panel6;
        private Panel panel8;
        private Panel panel7;
        private Button ExitButton;
        private Button resizeBtn;
        private NumericUpDown interpolateY;
        private NumericUpDown interpolateX;
        private Button interpolationBtn;
    }
}