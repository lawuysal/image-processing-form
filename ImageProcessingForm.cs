using System.Diagnostics.Metrics;
using System.Drawing.Imaging;
using System.Windows.Forms;
using clr = System.Drawing.Color;
using static image_processing_form.ImgProcess;
using static OpenTK.Graphics.OpenGL.GL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace image_processing_form
{

    public enum Modes
    {
        cropMode,
        zoomMode,
        brightnessMode,
        contrastMode,
        makeGrayMode,
        binarizeMode,
        shifterMode,
        histogramVisible,
        rotatorMode,
        morphologicalMode,
        applyFilterMode,
        convMode,
        edgeDetectMode,
        saltAndPepperMode,
        meanFilterMode,
        medianFilterMode,
        dilateMode,
        erodeMode,
        openMode,
        closeMode,
        adaptiveThresholdMode,
        RGBMode,
        stretchMode,
        resizeMode,
        noMode
    }

    public partial class ImageProcessingForm : Form
    {

        Label labelTrackBar1 = new Label();
        Label labelTrackBar2 = new Label();


        bool isAltKeyPressed = false;
        bool isCropMode = false;
        bool isZoomMode = false;
        bool isBrightnessMode = false;
        bool isContrastMode = false;
        bool isMakeGrayMode = false;
        bool isBinarizeMode = false;
        bool isShifterMode = false;
        bool isHistogramVisible = false;
        bool isRotatorMode = false;
        bool isMorphologicalMode = false;
        bool isApplyFilterMode = false;
        bool isConvMode = false;
        bool isEdgeDetectMode = false;
        bool isSaltAndPepperMode = false;
        bool isMeanFilterMode = false;
        bool isMedianFilterMode = false;
        bool isDilateMode = false;
        bool isErodeMode = false;
        bool isOpeningMode = false;
        bool isClosingMode = false;
        bool isAdaptiveThresholdMode = false;
        bool isRGBMode = false;
        bool isStretchMode = false;
        bool isResizeMode = false;

        Bitmap image = new Bitmap("bahceli_degil.jpg");


        // PictureBox üzerinde kýrpma yapýlacak alaný temsil eden dikdörtgenin boyutu ve konumu
        Point pDown = Point.Empty;
        Rectangle rect = Rectangle.Empty;

        // Daha sonra ekleyeceðim kaydet butonuna basýlmadan görselin 
        // kaydedilmemesi için bir deðiþken
        Image imageBeforeMode = null;


        public ImageProcessingForm()
        {


            InitializeComponent();

            ImgProcess.stateKeyValuePairs.Add("cropMode", (value) => isCropMode = value);
            ImgProcess.stateKeyValuePairs.Add("zoomMode", (value) => isZoomMode = value);
            ImgProcess.stateKeyValuePairs.Add("brightnessMode", (value) => isBrightnessMode = value);
            ImgProcess.stateKeyValuePairs.Add("contrastMode", (value) => isContrastMode = value);
            ImgProcess.stateKeyValuePairs.Add("makeGrayMode", (value) => isMakeGrayMode = value);
            ImgProcess.stateKeyValuePairs.Add("binarizeMode", (value) => isBinarizeMode = value);
            ImgProcess.stateKeyValuePairs.Add("shifterMode", (value) => isShifterMode = value);
            ImgProcess.stateKeyValuePairs.Add("histogramVisible", (value) => isHistogramVisible = value);
            ImgProcess.stateKeyValuePairs.Add("rotatorMode", (value) => isRotatorMode = value);
            ImgProcess.stateKeyValuePairs.Add("morphologicalMode", (value) => isMorphologicalMode = value);
            ImgProcess.stateKeyValuePairs.Add("applyFilterMode", (value) => isApplyFilterMode = value);
            ImgProcess.stateKeyValuePairs.Add("convMode", (value) => isConvMode = value);
            ImgProcess.stateKeyValuePairs.Add("edgeDetectMode", (value) => isEdgeDetectMode = value);
            ImgProcess.stateKeyValuePairs.Add("saltAndPepperMode", (value) => isSaltAndPepperMode = value);
            ImgProcess.stateKeyValuePairs.Add("meanFilterMode", (value) => isMeanFilterMode = value);
            ImgProcess.stateKeyValuePairs.Add("medianFilterMode", (value) => isMedianFilterMode = value);
            ImgProcess.stateKeyValuePairs.Add("RGBMode", (value) => isRGBMode = value);
            ImgProcess.stateKeyValuePairs.Add("dilateMode", (value) => isDilateMode = value);
            ImgProcess.stateKeyValuePairs.Add("erodeMode", (value) => isErodeMode = value);
            ImgProcess.stateKeyValuePairs.Add("openMode", (value) => isOpeningMode = value);
            ImgProcess.stateKeyValuePairs.Add("closeMode", (value) => isClosingMode = value);
            ImgProcess.stateKeyValuePairs.Add("adaptiveThresholdMode", (value) => isAdaptiveThresholdMode = value);
            ImgProcess.stateKeyValuePairs.Add("stretchMode", (value) => isStretchMode = value);
            ImgProcess.stateKeyValuePairs.Add("resizeMode", (value) => isResizeMode = value);

            ImgProcess.stateAfterFunctions.Add("zoomMode", afterZoomMode);
            ImgProcess.stateAfterFunctions.Add("brightnessMode", afterBrightnessMode);
            ImgProcess.stateAfterFunctions.Add("contrastMode", afterContrastMode);
            ImgProcess.stateAfterFunctions.Add("makeGrayMode", afterMakeGrayMode);
            ImgProcess.stateAfterFunctions.Add("binarizeMode", afterBinarizeMode);
            ImgProcess.stateAfterFunctions.Add("shifterMode", afterShifterMode);
            ImgProcess.stateAfterFunctions.Add("rotatorMode", afterRotatorMode);
            ImgProcess.stateAfterFunctions.Add("morphologicalMode", afterMorphologicalMode);
            ImgProcess.stateAfterFunctions.Add("applyFilterMode", afterApplyFilterMode);
            ImgProcess.stateAfterFunctions.Add("convMode", afterApplyConvMode);
            ImgProcess.stateAfterFunctions.Add("edgeDetectMode", afterEdgeDetectMode);
            ImgProcess.stateAfterFunctions.Add("saltAndPepperMode", afterSnPMode);
            ImgProcess.stateAfterFunctions.Add("meanFilterMode", afterMeanFilterMode);
            ImgProcess.stateAfterFunctions.Add("medianFilterMode", afterMedianFilterMode);
            ImgProcess.stateAfterFunctions.Add("dilateMode", afterDilateMode);
            ImgProcess.stateAfterFunctions.Add("adaptiveThresholdMode", afterAthresholdMode);
            ImgProcess.stateAfterFunctions.Add("openMode", afterOpenMode);
            ImgProcess.stateAfterFunctions.Add("closeMode", afterCloseMode);
            ImgProcess.stateAfterFunctions.Add("RGBMode", afterRGBMode);
            ImgProcess.stateAfterFunctions.Add("stretchMode", afterStretchMode);
            ImgProcess.stateAfterFunctions.Add("resizeMode", afterResizeMode);


            colorSpaceComboBox.Items.AddRange(new string[] { "CMYK", "HSI", "YCC", "YES" });
            colorSpaceComboBox.SelectedIndex = 0;


            histGraph.Plot.Axes.SetLimits(0, 256, 0, 10);
            histGraph.Plot.Axes.AutoScaleExpand();

            histGraph.Visible = false;
            interpolateX.Visible = false;
            interpolateY.Visible = false;
            resizeBtn.Visible = false;

            imageBeforeMode = image;

            /// -SÝLME- Loglarýn düzgün çalýþmasý için gerkeli!
            Logger logger = new Logger();

            /// -SÝLME- Zoom özelliðinin akýcýlýðý için gerekli!
            this.DoubleBuffered = true;

            /// -SÝLME- Undo iþlemi için gerekli!
            ImgProcess.processedImages.Add(image);
            ImgProcess.proceesedNames.Add("Original");
            undoBtn.Enabled = false;
            CreateHistoryTiles();
            ImgProcess.CalculateHistogram(histGraph, ref image);


            applyButton.Visible = false;

            trackBar1.Minimum = 1;
            trackBar1.Maximum = 600;
            trackBar1.SmallChange = 10;
            trackBar1.LargeChange = 10;
            trackBar1.UseWaitCursor = false;
            trackBar1.Visible = false;


            pictureBox.MouseWheel += PictureBox_MouseWheel;

            int width = image.Width;
            int height = image.Height;


            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = image;
        }


        private void PictureBox_MouseWheel(object? sender, MouseEventArgs e)
        {
            // Þimdilik Gereksiz
            if (isAltKeyPressed && e.Delta < 0)
            {

            }
            else if (isAltKeyPressed && e.Delta > 0)
            {

            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (isCropMode)
            {
                pDown = e.Location;
                pictureBox.Refresh();
                imageBeforeMode = pictureBox.Image;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCropMode)
            {
                if (!e.Button.HasFlag(MouseButtons.Left)) return;

                rect = new Rectangle(pDown, new Size(e.X - pDown.X, e.Y - pDown.Y));
                using (Graphics g = pictureBox.CreateGraphics())
                {
                    pictureBox.Refresh();
                    g.DrawRectangle(Pens.Red, rect);
                }
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {

            if (isCropMode)
            {
                Rectangle iR = ImageArea(pictureBox);
                rect = new Rectangle(pDown.X - iR.X, pDown.Y - iR.Y,
                                     e.X - pDown.X, e.Y - pDown.Y);
                Rectangle rectSrc = Scaled(rect, pictureBox, true);
                Rectangle rectDest = new Rectangle(Point.Empty, rectSrc.Size);

                Bitmap bmp = new Bitmap(rectDest.Width, rectDest.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(pictureBox.Image, rectDest, rectSrc, GraphicsUnit.Pixel);
                }
                pictureBox.Image = bmp;
                image = bmp;
                imageBeforeMode = pictureBox.Image;

                isCropMode = false;
                ImgProcess.processedImages.Add((Bitmap)pictureBox.Image);
                ImgProcess.proceesedNames.Add("Crop");
                CreateHistoryTiles();

                if (ImgProcess.processedImages.Count > 1)
                {
                    undoBtn.Enabled = true;
                }

                ImgProcess.CalculateHistogram(histGraph, ref image);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox.Image = image;
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Menu)
            {
                isAltKeyPressed = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Menu)
            {
                isAltKeyPressed = false;
            }
        }


        // Büyük ihtimalle toplam 1 ya da 2 tane trackBar olacak. 
        // Her iþlem için ayrý trackBar olmasýna gerek yok.
        // Hepsi buradan halledilebilir.
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (isZoomMode)
            {
                pictureBox.Location = new Point(3, 3);
                if (trackBar1.Value != 0)
                {
                    pictureBox.Image = ZoomPicture(image, new Size(trackBar1.Value, trackBar1.Value));
                }
            }
            else if (isBrightnessMode)
            {
                ImgProcess.Brightness(pictureBox, ref image, trackBar1.Value);
            }
            else if (isContrastMode)
            {
                ImgProcess.Contrast(pictureBox, ref image, trackBar1.Value);
            }
            else if (isBinarizeMode)
            {
                ImgProcess.Binarize(pictureBox, ref image, trackBar1.Value);
            }
            else if (isShifterMode)
            {
                ImgProcess.Shifter(pictureBox, ref image, trackBar1.Value, trackBar2.Value);
            }
            else if (isRotatorMode)
            {
                ImgProcess.Rotator(pictureBox, ref image, trackBar1.Value);
            }
            else if (isApplyFilterMode)
            {
                ImgProcess.ApplyBlurringFilter(pictureBox, image, trackBar1.Value);
            }
            else if (isConvMode)
            {
                ImgProcess.ConvoluteImage(pictureBox, image, trackBar1.Value, trackBar2.Value);
            }
            else if (isSaltAndPepperMode)
            {
                double value = (double)(trackBar1.Value) / 50;
                ImgProcess.AddSaltAndPepperNoise(pictureBox, image, value);
            }
            else if (isMeanFilterMode)
            {
                ImgProcess.MeanFilter(pictureBox, image, trackBar1.Value);
            }
            else if (isMedianFilterMode)
            {
                if (trackBar1.Value != 1 && trackBar1.Value != 3 && trackBar1.Value != 5 && trackBar1.Value != 7)
                {
                    int nearestAllowedValue = (trackBar1.Value < 4) ? 1 : 3;
                    nearestAllowedValue = (trackBar1.Value < 6) ? 5 : 7;
                    trackBar1.Value = nearestAllowedValue;
                }
                ImgProcess.MedianFilter(pictureBox, image, trackBar1.Value);
            }
            else if (isDilateMode)
            {
                pictureBox.Image = ImgProcess.Dilate(image, trackBar1.Value);
            }
            else if (isErodeMode)
            {
                pictureBox.Image = ImgProcess.Erode(image, trackBar1.Value);
            }
            else if (isOpeningMode)
            {
                ImgProcess.Opening(pictureBox, image, trackBar1.Value);
            }
            else if (isClosingMode)
            {
                ImgProcess.Closing(pictureBox, image, trackBar1.Value);
            }
            else if (isAdaptiveThresholdMode)
            {
                double value1 = (double)(trackBar1.Value) / 50;
                double value2 = (double)(trackBar2.Value) / 50;
                ImgProcess.AdaptiveThresholding(pictureBox, image, value1, value2);
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (isShifterMode)
            {
                if (trackBar1.Value != null)
                {
                    ImgProcess.Shifter(pictureBox, ref image, trackBar1.Value, trackBar2.Value);
                }
            }
            else if (isConvMode)
            {
                ImgProcess.ConvoluteImage(pictureBox, image, trackBar1.Value, trackBar2.Value);
            }
            else if (isAdaptiveThresholdMode)
            {
                double value1 = (double)(trackBar1.Value) / 50;
                double value2 = (double)(trackBar2.Value) / 50;
                ImgProcess.AdaptiveThresholding(pictureBox, image, value1, value2);
            }
        }

        // Burasý biraz karýþýk. Pek ellememek lazým.
        // Oðuzhana danýþ.
        private void zoomBtn_Click(object sender, EventArgs e)
        {
            if (isZoomMode)
            {
                afterZoomMode();
                hideControlButtons();
            }
            else
            {
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.zoomMode);

                Logger.Log("'Zoom' fonksiyonu aktif.");
                trackBar1.Minimum = 10;
                trackBar1.Maximum = 100;
                trackBar1.SmallChange = 10;
                trackBar1.LargeChange = 10;
                trackBar1.UseWaitCursor = false;

                ImgProcess.ChangeMode(Modes.zoomMode);

                imageBeforeMode = pictureBox.Image;

                // Büyük görsellerde aþýrý zoom yapýnca program uzun süre donuyor. 
                // Bunun için yüklü görselin boyutuna göre trackBar'ýn maximum deðeri belirlenmeli.
                // Aþaðýdaki oran baya güzel verimli çalýþýyor.
                trackBar1.Maximum = Convert.ToInt32((pictureBox.Image.Width <= pictureBox.Image.Height) ? 400000 / pictureBox.Image.Height : 400000 / pictureBox.Image.Width);
                trackBar1.Value = Convert.ToInt32((trackBar1.Maximum + 1) / 2);
                pictureBox.Image = ZoomPicture(image, new Size(trackBar1.Value, trackBar1.Value));
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                trackBar1.Visible = true;

                hideControlButtons();
            }
        }

        private void RGBButton_Click(object sender, EventArgs e)
        {
            if (isRGBMode)
            {
                afterRGBMode();
                hideControlButtons();

            }
            else
            {

                ImgProcess.CallAfters(Modes.RGBMode);

                pictureBox.Image = imageBeforeMode;

                // Bunun yeri burasý said baba pro

                imageBeforeMode = pictureBox.Image;

                Logger.Log("'RGB' fonksiyonu aktif.");

                ImgProcess.ChangeMode(Modes.RGBMode);

                ImgProcess.ConvertToSelectedColorSpace(colorSpaceComboBox, pictureBox);

                showControlButtons();
            }
        }



        private void shiftButton_Click(object sender, EventArgs e)
        {
            if (isShifterMode)
            {
                afterShifterMode();
                hideControlButtons();
            }
            else
            {
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.shifterMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Shift' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                ImgProcess.ChangeMode(Modes.shifterMode);


                trackBar1.Minimum = -pictureBox.Image.Size.Width;
                trackBar1.Maximum = pictureBox.Image.Size.Width;
                trackBar2.Minimum = -pictureBox.Image.Size.Height;
                trackBar2.Maximum = pictureBox.Image.Size.Height;


                trackBar1.Value = 0; // Mutlaka max ve minden sonra gelmeli
                trackBar2.Value = 0;
                trackBar1.SmallChange = 10;
                trackBar2.SmallChange = 10;
                trackBar1.LargeChange = 10;
                trackBar2.LargeChange = 10;
                trackBar1.UseWaitCursor = false;
                trackBar2.UseWaitCursor = false;
                trackBar1.Visible = true;
                trackBar2.Visible = true;

                imageBeforeMode = pictureBox.Image;

                showControlButtons();
            }
        }

        private void rotateButton_Click(object sender, EventArgs e)
        {
            if (isRotatorMode)
            {

                afterRotatorMode();
                hideControlButtons();
            }
            else
            {

                ImgProcess.CallAfters(Modes.rotatorMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Rotate' fonksiyonu inaktif.");

                ImgProcess.ChangeMode(Modes.rotatorMode);


                trackBar1.Minimum = -360;
                trackBar1.Maximum = 360;
                trackBar1.Value = 0;
                trackBar1.SmallChange = 1;
                trackBar1.LargeChange = 10;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;

                imageBeforeMode = pictureBox.Image;

                showControlButtons();
            }
        }


        private void makeGrayBtn_Click(object sender, EventArgs e)
        {
            // þimdi test yapýyoruz
            // if içindeki ifadeyi dðiþtirmeyi unutmuþuzk
            // þimdi aynen çalýþtý
            // siz de yeni fonksiyonlarý videodaki gibi ekleyin ki program patlamasýn puahahahahah
            if (isMakeGrayMode)
            {

                afterMakeGrayMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.makeGrayMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'MakeGray' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.makeGrayMode);

                trackBar1.Visible = false;

                imageBeforeMode = pictureBox.Image;
                ImgProcess.MakeGray(pictureBox, ref image);
                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }

        }

        private void btnStretch_Click(object sender, EventArgs e)
        {
            if (isStretchMode)
            {
                afterStretchMode();
                hideControlButtons();

            }
            else
            {
                pictureBox.Image = imageBeforeMode;
                ImgProcess.CallAfters(Modes.stretchMode);

                Logger.Log("'Stretch' fonksiyonu aktif.");
                ImgProcess.ChangeMode(Modes.stretchMode);
                imageBeforeMode = pictureBox.Image;
                Bitmap stretchedImage = StretchHistogram(image);

                image = stretchedImage;
                pictureBox.Image = image;


                showControlButtons();

            }
        }


        private void brightnessBtn_Click(object sender, EventArgs e)
        {
            if (isBrightnessMode)
            {
                afterBrightnessMode();
                hideControlButtons();
            }
            else
            {
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.brightnessMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Brightness' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                ImgProcess.ChangeMode(Modes.brightnessMode);


                trackBar1.Minimum = -255;
                trackBar1.Maximum = 255;

                trackBar1.Value = 0; // Mutlaka max ve minden sonra gelmeli
                trackBar1.SmallChange = 10;
                trackBar1.LargeChange = 10;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;

                imageBeforeMode = pictureBox.Image;

                showControlButtons();
            }
        }

        private void binarizeBtn_Click(object sender, EventArgs e)
        {
            //þimdi binarize fonksiyonunu yapýyordum
            // farkettim ki bir hata var
            // trackbar ý düzenlememiþim
            // özellik eklerken trackbar ý düzenlemeyi unutmayýn !!!
            if (isBinarizeMode)
            {
                afterBinarizeMode();
                hideControlButtons();

            }
            else
            {
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.binarizeMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Binarize' fonksiyonu aktif.");

                // State deðiþimleri
                ImgProcess.ChangeMode(Modes.binarizeMode);

                // Max ve min deðerleri deðiþtirilebilir :).

                trackBar1.Minimum = 1;
                trackBar1.Maximum = 255;

                trackBar1.Value = 100; // Mutlaka max ve minden sonra gelmeli ve ikisinin arasonda olmalý
                trackBar1.SmallChange = 10;
                trackBar1.LargeChange = 10;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;

                imageBeforeMode = pictureBox.Image;

                showControlButtons();
            }
        }

        private void contrastButton_Click(object sender, EventArgs e)
        {
            if (isContrastMode)
            {
                afterContrastMode();
                hideControlButtons();

            }
            else
            {
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.contrastMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Contrast' fonksiyonu aktif.");

                // State deðiþimleri
                ImgProcess.ChangeMode(Modes.contrastMode);

                // Max ve min deðerleri deðiþtirilebilir :).

                trackBar1.Minimum = -100;
                trackBar1.Maximum = 100;

                trackBar1.Value = 0; // Mutlaka max ve minden sonra gelmeli
                trackBar1.SmallChange = 10;
                trackBar1.LargeChange = 10;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;

                imageBeforeMode = pictureBox.Image;

                showControlButtons();
            }
        }

        private void multiplyBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyalarý|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Tüm Dosyalar|*.*";

            Bitmap selectedImage;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    selectedImage = new Bitmap(openFileDialog.FileName);
                    ImgProcess.MultiplyImages(pictureBox, ref image, ref selectedImage);
                    image = (Bitmap)pictureBox.Image;
                    imageBeforeMode = image;
                    ImgProcess.processedImages.Add((Bitmap)pictureBox.Image);
                    ImgProcess.proceesedNames.Add("Multiply");
                    CreateHistoryTiles();
                    if (ImgProcess.processedImages.Count > 1)
                    {
                        undoBtn.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Resim yüklenirken bir hata oluþtu: " + ex.Message);
                    return;
                }
            }
        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            if (isApplyFilterMode)
            {

                afterApplyFilterMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.applyFilterMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'AddFilter' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.applyFilterMode);

                trackBar1.Minimum = 0;
                trackBar1.Maximum = 5;

                trackBar1.Value = 0; // Mutlaka max ve minden sonra gelmeli
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;


                imageBeforeMode = pictureBox.Image;
                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void convoluteBtn_Click(object sender, EventArgs e)
        {
            if (isConvMode)
            {

                afterApplyConvMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.convMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Convolute' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.convMode);



                labelTrackBar1.Text = "Size";
                labelTrackBar1.ForeColor = Color.White;
                labelTrackBar1.Location = new Point(trackBar1.Location.X + trackBar1.Width - 5, trackBar1.Location.Y);



                labelTrackBar2.Text = "Sig";
                labelTrackBar2.ForeColor = Color.White;
                labelTrackBar2.Location = new Point(trackBar2.Location.X + trackBar2.Width - 5, trackBar2.Location.Y);

                panel1.Controls.Add(labelTrackBar1);
                panel1.Controls.Add(labelTrackBar2);


                trackBar1.Minimum = 0;
                trackBar1.Maximum = 5;

                trackBar2.Minimum = 1;
                trackBar2.Maximum = 5;

                trackBar1.Value = 0; // Mutlaka max ve minden sonra gelmeli
                trackBar2.Value = 1;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;
                trackBar2.Visible = true;


                imageBeforeMode = pictureBox.Image;
                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void edgeDetectBtn_Click(object sender, EventArgs e)
        {
            if (isEdgeDetectMode)
            {

                afterEdgeDetectMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.edgeDetectMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'EdgeDetection' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.edgeDetectMode);



                imageBeforeMode = pictureBox.Image;
                ImgProcess.EdgeDetection(pictureBox, image);


                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void saltAndPepperBtn_Click(object sender, EventArgs e)
        {
            if (isSaltAndPepperMode)
            {

                afterSnPMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.saltAndPepperMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'AddSaltAndPepper' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.saltAndPepperMode);

                trackBar1.Minimum = 0;
                trackBar1.Maximum = 10;

                trackBar1.Value = 0; // Mutlaka max ve minden sonra gelmeli
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;
                imageBeforeMode = pictureBox.Image;


                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void meanFilterBtn_Click(object sender, EventArgs e)
        {
            if (isMeanFilterMode)
            {

                afterMeanFilterMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.meanFilterMode);


                pictureBox.Image = imageBeforeMode;
                Logger.Log("'MeanFilter' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.meanFilterMode);

                trackBar1.Minimum = 0;
                trackBar1.Maximum = 8;

                trackBar1.LargeChange = 2;
                trackBar1.SmallChange = 2;


                trackBar1.Value = 0; // Mutlaka max ve minden sonra gelmeli
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;
                imageBeforeMode = pictureBox.Image;


                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void medFilterBtn_Click(object sender, EventArgs e)
        {
            if (isMedianFilterMode)
            {

                afterMedianFilterMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.medianFilterMode);


                pictureBox.Image = imageBeforeMode;
                Logger.Log("'MedianFilter' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.medianFilterMode);

                trackBar1.Minimum = 1;
                trackBar1.Maximum = 7;
                trackBar1.LargeChange = 2;
                trackBar1.SmallChange = 1;
                trackBar1.TickFrequency = 1;

                trackBar1.Value = 1;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;
                imageBeforeMode = pictureBox.Image;


                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }


        private void dilateBtn_Click(object sender, EventArgs e)
        {
            if (isDilateMode)
            {

                afterDilateMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.dilateMode);


                pictureBox.Image = imageBeforeMode;
                Logger.Log("'DÝlate' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.dilateMode);


                trackBar1.Minimum = 0;
                trackBar1.Maximum = 5;

                trackBar1.Value = 0;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;

                imageBeforeMode = pictureBox.Image;

                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void erodeBtn_Click(object sender, EventArgs e)
        {
            if (isErodeMode)
            {

                afterErodeMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.erodeMode);


                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Erode' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.erodeMode);


                trackBar1.Minimum = 0;
                trackBar1.Maximum = 5;

                trackBar1.Value = 0;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;

                imageBeforeMode = pictureBox.Image;

                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            if (isOpeningMode)
            {

                afterOpenMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.dilateMode);


                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Open' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.openMode);

                trackBar1.Minimum = 0;
                trackBar1.Maximum = 5;

                trackBar1.Value = 0;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;

                imageBeforeMode = pictureBox.Image;

                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (isClosingMode)
            {

                afterCloseMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.closeMode);


                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Close' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.closeMode);


                trackBar1.Minimum = 0;
                trackBar1.Maximum = 5;

                trackBar1.Value = 0;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;

                imageBeforeMode = pictureBox.Image;

                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void adaptiveThresholdBtn_Click(object sender, EventArgs e)
        {
            if (isAdaptiveThresholdMode)
            {

                afterAthresholdMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.adaptiveThresholdMode);


                pictureBox.Image = imageBeforeMode;
                Logger.Log("'AdaptiveThreshold' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.adaptiveThresholdMode);


                trackBar1.Minimum = 0;
                trackBar1.Maximum = 100;

                trackBar2.Minimum = 0;
                trackBar2.Maximum = 100;

                trackBar1.Value = 0;
                trackBar2.Value = 0;
                trackBar1.UseWaitCursor = false;
                trackBar2.UseWaitCursor = false;
                trackBar1.Visible = true;
                trackBar2.Visible = true;

                imageBeforeMode = pictureBox.Image;

                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyalarý|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Tüm Dosyalar|*.*";

            Bitmap selectedImage;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    selectedImage = new Bitmap(openFileDialog.FileName);
                    ImgProcess.AddImages(pictureBox, ref image, ref selectedImage);
                    image = (Bitmap)pictureBox.Image;
                    imageBeforeMode = image;
                    ImgProcess.processedImages.Add((Bitmap)pictureBox.Image);
                    ImgProcess.proceesedNames.Add("Add");
                    CreateHistoryTiles();
                    if (ImgProcess.processedImages.Count > 1)
                    {
                        undoBtn.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Resim yüklenirken bir hata oluþtu: " + ex.Message);
                    return;
                }
            }
        }


        private void applyButton_Click(object sender, EventArgs e)
        {
            bool histogramFix = !isHistogramVisible;
            // History özelliði için gerekli
            if (isBrightnessMode)
            {
                ImgProcess.proceesedNames.Add("Brightness");
            }
            else if (isContrastMode)
            {
                ImgProcess.proceesedNames.Add("Contrast");
            }
            else if (isMakeGrayMode)
            {
                ImgProcess.proceesedNames.Add("MakeGray");
            }
            else if (isBinarizeMode)
            {
                ImgProcess.proceesedNames.Add("Binarize");
            }
            else if (isShifterMode)
            {
                ImgProcess.proceesedNames.Add("Shift");
            }
            else if (isRotatorMode)
            {
                ImgProcess.proceesedNames.Add("Rotate");
            }
            else if (isMorphologicalMode)
            {
                ImgProcess.proceesedNames.Add("Morph");
            }
            else if (isApplyFilterMode)
            {
                ImgProcess.proceesedNames.Add("Filter");
            }
            else if (isConvMode)
            {
                ImgProcess.proceesedNames.Add("Convolute");
            }
            else if (isEdgeDetectMode)
            {
                ImgProcess.proceesedNames.Add("EdgeDetect");
            }
            else if (isSaltAndPepperMode)
            {
                ImgProcess.proceesedNames.Add("SnP");
            }
            else if (isMeanFilterMode)
            {
                ImgProcess.proceesedNames.Add("MeanFilter");
            }
            else if (isMedianFilterMode)
            {
                ImgProcess.proceesedNames.Add("MedianFilter");
            }
            else if (isDilateMode)
            {
                ImgProcess.proceesedNames.Add("Dilate");
            }
            else if (isErodeMode)
            {
                ImgProcess.proceesedNames.Add("Erode");
            }
            else if (isOpeningMode)
            {
                ImgProcess.proceesedNames.Add("Opening");
            }
            else if (isClosingMode)
            {
                ImgProcess.proceesedNames.Add("Closing");
            }
            else if (isAdaptiveThresholdMode)
            {
                ImgProcess.proceesedNames.Add("Threshold");
            }
            else if (isRGBMode)
            {
                ImgProcess.proceesedNames.Add("RGB");
            }
            else if (isStretchMode)
            {
                ImgProcess.proceesedNames.Add("Stretch");
            }
            else if (isResizeMode)
            {
                ImgProcess.proceesedNames.Add("Resize");
            }


            ImgProcess.processedImages.Add((Bitmap)pictureBox.Image);

            // Program genelinde kullanýlan deðiþkenlere iþlemin uygulanmasý
            image = (Bitmap)pictureBox.Image;
            imageBeforeMode = image;

            // Stateleri sýfýrlama
            ImgProcess.ChangeMode(Modes.noMode);

            // After fonksiyonlarýný çaðýr ki her þey kapanmýþ gibi olsun.
            ImgProcess.CallAfters(Modes.noMode);

            if (ImgProcess.processedImages.Count > 1)
            {
                undoBtn.Enabled = true;
            }

            MessageBox.Show("Applied.");

            CreateHistoryTiles();

            ImgProcess.CalculateHistogram(histGraph, ref image);

            if (histogramFix)
            {
                isHistogramVisible = true;
            }
            else
            {
                isHistogramVisible = false;
            }

            applyButton.Visible = false;
        }

        private void undoBtn_Click(object sender, EventArgs e)
        {
            ImgProcess.proceesedNames.RemoveAt(ImgProcess.proceesedNames.Count - 1);
            ImgProcess.processedImages.RemoveAt(ImgProcess.processedImages.Count - 1);

            image = ImgProcess.processedImages[ImgProcess.processedImages.Count - 1];
            pictureBox.Image = image;
            imageBeforeMode = image;

            if (ImgProcess.processedImages.Count == 1)
            {
                undoBtn.Enabled = false;
            }


            ImgProcess.CalculateHistogram(histGraph, ref image);

            Control lastControl = historyPanel.Controls[historyPanel.Controls.Count - 1];
            historyPanel.Controls.Remove(lastControl);
            lastControl.Dispose();

            pictureBox.Location = new Point(3, 3);
            pictureBox.Size = new Size(921, 662);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            MessageBox.Show("Undo.");

        }

        private void histogramBtn_Click(object sender, EventArgs e)
        {
            if (isHistogramVisible)
            {
                histGraph.Visible = false;
                isHistogramVisible = false;
            }
            else
            {
                histGraph.Visible = true;
                isHistogramVisible = true;
            }
        }

        private void cropBtn_Click(object sender, EventArgs e)
        {
            if (isCropMode)
            {
                isCropMode = false;
                pictureBox.Refresh();
            }
            else
            {
                isCropMode = true;
            }
        }



        // - After fonksiyonlarý -
        // Tuhaf hatalarýn önüne geçmek için yazýldý.
        // Eðer bir iþlem yapýlýrken baþka bir iþlem yapýlýrsa tuhaf hatalar oluþuyor.
        private void afterZoomMode()
        {
            Logger.Log("'Zoom' fonksiyonu inaktif.");
            pictureBox.Image = imageBeforeMode;

            pictureBox.Location = new Point(3, 3);
            pictureBox.Size = new Size(921, 662);
            isZoomMode = false;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            trackBar1.Visible = false;
        }

        private void afterBrightnessMode()
        {
            Logger.Log("'Brightness' fonksiyonu inaktif.");
            isBrightnessMode = false;
            trackBar1.Visible = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void afterShifterMode()
        {
            Logger.Log("'Shifter' fonksiyonu inaktif.");
            isShifterMode = false;
            trackBar1.Visible = false;
            trackBar2.Visible = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void afterRotatorMode()
        {
            Logger.Log("'Rotate' fonksiyonu inaktif.");
            isRotatorMode = false;
            trackBar1.Visible = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void afterMorphologicalMode()
        {
            Logger.Log("'Morph' fonksiyonu inaktif.");
            isMorphologicalMode = false;
            trackBar1.Visible = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void afterMakeGrayMode()
        {
            Logger.Log("'MakeGray' fonksiyonu inaktif.");
            isMakeGrayMode = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterResizeMode()
        {
            Logger.Log("'Resize' fonksiyonu inaktif.");
            isResizeMode = false;
            pictureBox.Image = imageBeforeMode;

            interpolateX.Visible = false;
            interpolateY.Visible = false;
            resizeBtn.Visible = false;
        }

        private void afterContrastMode()
        {
            Logger.Log("'Contrast' fonksiyonu inaktif.");
            isContrastMode = false;
            trackBar1.Visible = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void afterStretchMode()
        {
            Logger.Log("'Stretch' fonksiyonu inaktif.");
            isStretchMode = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void afterBinarizeMode()
        {
            Logger.Log("'Binarize' fonksiyonu inaktif.");
            isBinarizeMode = false;
            trackBar1.Visible = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void afterApplyFilterMode()
        {
            Logger.Log("'ApplyFilter' fonksiyonu inaktif.");
            isApplyFilterMode = false;
            trackBar1.Visible = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterApplyConvMode()
        {
            Logger.Log("'Convolute' fonksiyonu inaktif.");
            isConvMode = false;
            trackBar1.Visible = false;
            trackBar2.Visible = false;
            panel1.Controls.Remove(labelTrackBar1);
            panel1.Controls.Remove(labelTrackBar2);
            pictureBox.Image = imageBeforeMode;
        }

        private void afterEdgeDetectMode()
        {
            Logger.Log("'EdgeDetection' fonksiyonu inaktif.");
            isEdgeDetectMode = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterSnPMode()
        {
            Logger.Log("'AddSaltAndPepperNoise' fonksiyonu inaktif.");
            isSaltAndPepperMode = false;
            trackBar1.Visible = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterMeanFilterMode()
        {
            Logger.Log("'MeanFilter' fonksiyonu inaktif.");
            isMeanFilterMode = false;
            trackBar1.Visible = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterMedianFilterMode()
        {
            Logger.Log("'MedianFilter' fonksiyonu inaktif.");
            isMedianFilterMode = false;
            trackBar1.Visible = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterDilateMode()
        {
            Logger.Log("'Dilate' fonksiyonu inaktif.");
            isDilateMode = false;
            trackBar1.Visible = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterErodeMode()
        {
            Logger.Log("'Erode' fonksiyonu inaktif.");
            isErodeMode = false;
            trackBar1.Visible = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterOpenMode()
        {
            Logger.Log("'Open' fonksiyonu inaktif.");
            isOpeningMode = false;
            trackBar1.Visible = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterCloseMode()
        {
            Logger.Log("'Close' fonksiyonu inaktif.");
            isClosingMode = false;
            trackBar1.Visible = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterAthresholdMode()
        {
            Logger.Log("'AdaptiveThreshold' fonksiyonu inaktif.");
            isAdaptiveThresholdMode = false;
            trackBar1.Visible = false;
            trackBar2.Visible = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterRGBMode()
        {
            Logger.Log("'RGB' fonksiyonu inaktif.");
            isRGBMode = false;
            trackBar1.Visible = false;
            trackBar2.Visible = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void hideControlButtons()
        {
            applyButton.Visible = false;
        }

        private void showControlButtons()
        {
            applyButton.Visible = true;
        }


        // - Utility fonksiyonlarý -
        // Silersen uygulama çalýþmaz.
        // Puahahahhhaaa
        // Kýrpma ve zoom fonksiyonlarý için gerekli
        float GetFactor(PictureBox pBox)
        {
            if (pBox.Image == null) return 0;
            Size si = pBox.Image.Size;
            Size sp = pBox.ClientSize;
            float ri = 1f * si.Width / si.Height;
            float rp = 1f * sp.Width / sp.Height;
            float factor = 1f * pBox.Image.Width / pBox.ClientSize.Width;
            if (rp > ri) factor = 1f * pBox.Image.Height / pBox.ClientSize.Height;
            return factor;
        }

        Image ZoomPicture(Image img, Size size)
        {
            Bitmap bmp = new Bitmap(img, Convert.ToInt32(img.Width * size.Width / 100), Convert.ToInt32(img.Height * size.Height / 100));
            Graphics g = Graphics.FromImage(bmp);
            return bmp;
        }

        Rectangle ImageArea(PictureBox pbox)
        {
            Size si = pbox.Image.Size;
            Size sp = pbox.ClientSize;

            if (pbox.SizeMode == PictureBoxSizeMode.StretchImage)
                return pbox.ClientRectangle;
            if (pbox.SizeMode == PictureBoxSizeMode.Normal ||
                pbox.SizeMode == PictureBoxSizeMode.AutoSize)
                return new Rectangle(Point.Empty, si);
            if (pbox.SizeMode == PictureBoxSizeMode.CenterImage)
                return new Rectangle(new Point((sp.Width - si.Width) / 2,
                                    (sp.Height - si.Height) / 2), si);

            //  PictureBoxSizeMode.Zoom
            float ri = 1f * si.Width / si.Height;
            float rp = 1f * sp.Width / sp.Height;
            if (rp > ri)
            {
                int width = si.Width * sp.Height / si.Height;
                int left = (sp.Width - width) / 2;
                return new Rectangle(left, 0, width, sp.Height);
            }
            else
            {
                int height = si.Height * sp.Width / si.Width;
                int top = (sp.Height - height) / 2;
                return new Rectangle(0, top, sp.Width, height);
            }
        }

        Rectangle Scaled(Rectangle rect, PictureBox pbox, bool scale)
        {
            float factor = GetFactor(pbox);
            if (!scale) factor = 1f / factor;
            return Rectangle.Round(new RectangleF(rect.X * factor, rect.Y * factor,
                                       rect.Width * factor, rect.Height * factor));
        }

        void CreateHistoryTiles()
        {
            historyPanel.Controls.Clear();
            int count = ImgProcess.proceesedNames.Count;
            for (int i = 0; i < count; i++)
            {
                Panel pnl = new Panel();
                pnl.Size = new Size(330, 100);
                pnl.Location = new Point(12, 12 + i * 110);
                pnl.BackColor = Color.FromArgb(83, 83, 83);

                PictureBox pb = new PictureBox();
                pb.Image = ImgProcess.processedImages[i];
                pb.Size = new Size(80, 80);
                pb.Location = new Point(15, 10);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.BorderStyle = BorderStyle.FixedSingle;

                Label lbl = new Label();
                lbl.Text = ImgProcess.proceesedNames[i];
                lbl.Location = new Point(120, 40);
                lbl.ForeColor = Color.White;
                lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);

                pnl.Controls.Add(pb);
                pnl.Controls.Add(lbl);

                historyPanel.Controls.Add(pnl);

            }

        }

        private void openFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyalarý|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Tüm Dosyalar|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    Bitmap selectedImage = new Bitmap(openFileDialog.FileName);

                    image = selectedImage;
                    pictureBox.Image = selectedImage;
                    imageBeforeMode = selectedImage;
                    ImgProcess.processedImages.Clear();
                    ImgProcess.proceesedNames.Clear();
                    ImgProcess.processedImages.Add(selectedImage);
                    ImgProcess.proceesedNames.Add("Original");
                    CreateHistoryTiles();

                    ImgProcess.ChangeMode(Modes.noMode);
                    ImgProcess.CallAfters(Modes.noMode);
                    hideControlButtons();
                    ImgProcess.CalculateHistogram(histGraph, ref image);


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Resim yüklenirken bir hata oluþtu: " + ex.Message);
                }
            }
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            Bitmap selectedImage = (Bitmap)pictureBox.Image;
            if (selectedImage != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "JPEG Dosyasý|*.jpg";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        selectedImage.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                        MessageBox.Show("Resim baþarýyla kaydedildi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Resim kaydedilirken bir hata oluþtu: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Önce bir resim seçmelisiniz.");
            }

        }

        //Form moving code.
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void interpolationBtn_Click_1(object sender, EventArgs e)
        {
            // þimdi test yapýyoruz
            // if içindeki ifadeyi dðiþtirmeyi unutmuþuzk
            // þimdi aynen çalýþtý
            // siz de yeni fonksiyonlarý videodaki gibi ekleyin ki program patlamasýn puahahahahah
            if (isResizeMode)
            {

                afterResizeMode();
                hideControlButtons();
            }
            else
            {
                // burada ise diðer iþlemler kapatýlacak
                // After fonksiyonlarý
                ImgProcess.CallAfters(Modes.resizeMode);

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Resize' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                // þuan saat 3:01
                ImgProcess.ChangeMode(Modes.resizeMode);

                trackBar1.Visible = false;
                interpolateX.Visible = true;
                interpolateY.Visible = true;
                resizeBtn.Visible = true;

                imageBeforeMode = pictureBox.Image;

                // þimdi ise apply butonuna bakalým
                showControlButtons();
            }
        }

        private void resizeBtn_Click(object sender, EventArgs e)
        {
            imageBeforeMode = pictureBox.Image;
            int width = Convert.ToInt32(interpolateX.Value);
            int height = Convert.ToInt32(interpolateY.Value);
            image = ImgProcess.Resize(image, width, height);
            pictureBox.Image = image;
            MessageBox.Show("Resized.");
        }
    }
}