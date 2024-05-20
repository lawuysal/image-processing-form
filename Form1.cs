using System.Diagnostics.Metrics;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace image_processing_form
{
    public partial class Form1 : Form
    {

        // Boolean state variables
        bool isAltKeyPressed = false;
        bool isCropMode = false;
        bool isZoomMode = false;
        bool isBrightnessMode = false;
        bool isContrastMode = false;
        bool isBlackWhiteMode = false;
        bool isMakeGrayMode = false;
        bool isBinarizeMode = false;
        bool isShifterMode = false;
        bool isHistogramVisible = false;

        Bitmap image = new Bitmap("image_2.jpg");

        // PictureBox �zerinde k�rpma yap�lacak alan� temsil eden dikd�rtgenin boyutu ve konumu
        Point pDown = Point.Empty;
        Rectangle rect = Rectangle.Empty;

        // Daha sonra ekleyece�im kaydet butonuna bas�lmadan g�rselin 
        // kaydedilmemesi i�in bir de�i�ken
        Image imageBeforeMode = null;

        public Form1()
        {
            InitializeComponent();

            histGraph.Plot.Axes.SetLimits(0, 256, 0, 10);
            histGraph.Plot.Axes.AutoScaleExpand();
            histGraph.Visible = false;

            imageBeforeMode = image;

            /// -S�LME- Loglar�n d�zg�n �al��mas� i�in gerkeli!
            Logger logger = new Logger();

            /// -S�LME- Zoom �zelli�inin ak�c�l��� i�in gerekli!
            this.DoubleBuffered = true;

            /// -S�LME- Undo i�lemi i�in gerekli!
            ImgProcess.processedImages.Add(image);
            ImgProcess.proceesedNames.Add("Original");
            undoBtn.Enabled = false;
            CreateHistoryTiles();

            // Control Butonlar�
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
            // �imdilik Gereksiz
            if (isAltKeyPressed && e.Delta < 0)
            {

            }
            else if (isAltKeyPressed && e.Delta > 0)
            {

            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            // �imdilik Gereksiz
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

                ImgProcess.CalculteHistogram(histGraph, ref image);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox.Image = image;
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Alt key is pressed
            if (e.KeyCode == Keys.Menu) // Keys.Menu is the Alt key
            {
                isAltKeyPressed = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // Check if Alt key is released
            if (e.KeyCode == Keys.Menu) // Keys.Menu is the Alt key
            {
                isAltKeyPressed = false;
            }
        }



        // B�y�k ihtimalle toplam 1 ya da 2 tane trackBar olacak. 
        // Her i�lem i�in ayr� trackBar olmas�na gerek yok.
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
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (isShifterMode)
            {
                if (trackBar1.Value != 0)
                {
                    ImgProcess.Shifter(pictureBox, ref image, trackBar1.Value, trackBar2.Value);
                }
            }
        }

        // Buras� biraz kar���k. Pek ellememek laz�m.
        // O�uzhana dan��.
        private void zoomBtn_Click(object sender, EventArgs e)
        {
            if (isZoomMode)
            {
                afterZoomMode();
                hideControlButtons();
            }
            else
            {
                // After fonksiyonlar�
                afterBrightnessMode();
                afterContrastMode();
                afterBlackWhiteMode();
                afterShifterMode();

                Logger.Log("'Zoom' fonksiyonu aktif.");
                trackBar1.Minimum = 1;
                trackBar1.Maximum = 100;
                trackBar1.SmallChange = 10;
                trackBar1.LargeChange = 10;
                trackBar1.UseWaitCursor = false;

                isBrightnessMode = false;
                isContrastMode = false;
                isCropMode = false;
                isZoomMode = true;
                isCropMode = false;
                isCropMode = false;
                isMakeGrayMode = false;
                isBinarizeMode = false;
                isCropMode = false;
                isShifterMode = false;

                imageBeforeMode = pictureBox.Image;

                // B�y�k g�rsellerde a��r� zoom yap�nca program uzun s�re donuyor. 
                // Bunun i�in y�kl� g�rselin boyutuna g�re trackBar'�n maximum de�eri belirlenmeli.
                // A�a��daki oran baya g�zel verimli �al���yor.
                trackBar1.Maximum = Convert.ToInt32((pictureBox.Image.Width <= pictureBox.Image.Height) ? 400000 / pictureBox.Image.Height : 400000 / pictureBox.Image.Width);
                trackBar1.Value = Convert.ToInt32((trackBar1.Maximum + 1) / 2);
                pictureBox.Image = ZoomPicture(image, new Size(trackBar1.Value, trackBar1.Value));
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                trackBar1.Visible = true;

                hideControlButtons();
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
                // After fonksiyonlar�
                afterContrastMode();
                afterZoomMode();
                afterBlackWhiteMode();
                afterBinarizeMode();
                afterBrightnessMode();
                afterMakeGrayMode();
                afterShifterMode();

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Shift' fonksiyonu aktif.");

                // State de�i�imleri i�in bir fonksiyon yaz�labilir.
                // Ama �imdilik b�yle ��nk� yoruldum. Sabah oldu saat 5:35
                isBrightnessMode = false;
                isContrastMode = false;
                isCropMode = false;
                isZoomMode = false;
                isCropMode = false;
                isCropMode = false;
                isMakeGrayMode = false;
                isBinarizeMode = false;
                isCropMode = false;
                isShifterMode = true;


                trackBar1.Minimum = 0;
                trackBar1.Maximum = 2000;
                trackBar2.Minimum = 0;
                trackBar2.Maximum = 2000;


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

        private void makeGrayBtn_Click(object sender, EventArgs e)
        {
            // �imdi test yap�yoruz
            // if i�indeki ifadeyi d�i�tirmeyi unutmu�uzk
            // �imdi aynen �al��t�
            // siz de yeni fonksiyonlar� videodaki gibi ekleyin ki program patlamas�n puahahahahah
            if (isMakeGrayMode)
            {

                afterMakeGrayMode();
                hideControlButtons();
            }
            else
            {
                // burada ise di�er i�lemler kapat�lacak
                // After fonksiyonlar�
                afterContrastMode();
                afterZoomMode();
                afterBlackWhiteMode();
                afterBrightnessMode();
                afterBinarizeMode();
                afterShifterMode();

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'MakeGray' fonksiyonu aktif.");

                // State de�i�imleri i�in bir fonksiyon yaz�labilir.
                // Ama �imdilik b�yle ��nk� yoruldum. Sabah oldu saat 5:35
                // �uan saat 3:01
                isBrightnessMode = false;
                isContrastMode = false;
                isCropMode = false;
                isZoomMode = false;
                isCropMode = false;
                isCropMode = false;
                isMakeGrayMode = true;
                isBinarizeMode = false;
                isCropMode = false;
                isShifterMode = false;



                trackBar1.Visible = false;


                imageBeforeMode = pictureBox.Image;
                ImgProcess.MakeGray(pictureBox, ref image);
                // �imdi ise apply butonuna bakal�m
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
                // After fonksiyonlar�
                afterContrastMode();
                afterZoomMode();
                afterBlackWhiteMode();
                afterShifterMode();

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Brightness' fonksiyonu aktif.");

                // State de�i�imleri i�in bir fonksiyon yaz�labilir.
                // Ama �imdilik b�yle ��nk� yoruldum. Sabah oldu saat 5:35
                isBrightnessMode = true;
                isContrastMode = false;
                isCropMode = false;
                isZoomMode = false;
                isCropMode = false;
                isCropMode = false;
                isMakeGrayMode = false;
                isBinarizeMode = false;
                isCropMode = false;
                isShifterMode = false;


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
            //�imdi binarize fonksiyonunu yap�yordum
            // farkettim ki bir hata var
            // trackbar � d�zenlememi�im
            // �zellik eklerken trackbar � d�zenlemeyi unutmay�n !!!
            if (isBinarizeMode)
            {
                afterBinarizeMode();
                hideControlButtons();

            }
            else
            {
                // After fonksiyonlar�
                afterBrightnessMode();
                afterZoomMode();
                afterBlackWhiteMode();
                afterMakeGrayMode();
                afterContrastMode();
                afterShifterMode();

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Binarize' fonksiyonu aktif.");

                // State de�i�imleri
                isContrastMode = false;
                isCropMode = false;
                isZoomMode = false;
                isBrightnessMode = false;
                isCropMode = false;
                isCropMode = false;
                isMakeGrayMode = false;
                isBinarizeMode = true;
                isCropMode = false;
                isShifterMode = false;

                // Max ve min de�erleri de�i�tirilebilir :).

                trackBar1.Minimum = 1;
                trackBar1.Maximum = 255;

                trackBar1.Value = 100; // Mutlaka max ve minden sonra gelmeli ve ikisinin arasonda olmal�
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
                // After fonksiyonlar�
                afterBrightnessMode();
                afterZoomMode();
                afterBlackWhiteMode();
                afterShifterMode();

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Contrast' fonksiyonu aktif.");

                // State de�i�imleri
                isContrastMode = true;
                isCropMode = false;
                isZoomMode = false;
                isBrightnessMode = false;
                isCropMode = false;
                isCropMode = false;
                isMakeGrayMode = false;
                isBinarizeMode = false;
                isCropMode = false;
                isShifterMode = false;

                // Max ve min de�erleri de�i�tirilebilir :).

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

        private void blackWhiteBtn_Click(object sender, EventArgs e)
        {
            //isBlackWhiteMode = true;
            //ImgProcess.BlackWhite(pictureBox, ref image);
            //Logger.Log("'BlackWhite' fonksiyonu �al��t�.");
            //isBlackWhiteMode = false;

            if (isBlackWhiteMode)
            {
                afterBlackWhiteMode();
                hideControlButtons();
            }
            else
            {
                afterBrightnessMode();
                afterContrastMode();
                afterZoomMode();
                afterShifterMode();

                pictureBox.Image = imageBeforeMode;
                trackBar1.Visible = false;

                Logger.Log("'BlackWhite' fonksiyonu aktif.");

                // State de�i�imleri
                isBlackWhiteMode = true;
                isCropMode = false;
                isZoomMode = false;
                isBrightnessMode = false;
                isContrastMode = false;
                isMakeGrayMode = false;
                isBinarizeMode = false;
                isShifterMode = false;

                imageBeforeMode = pictureBox.Image;
                ImgProcess.BlackWhite(pictureBox, ref image);

                showControlButtons();
            }

        }

        private void multiplyBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyalar�|*.jpg;*.jpeg;*.png;*.gif;*.bmp|T�m Dosyalar|*.*";

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
                    MessageBox.Show("Resim y�klenirken bir hata olu�tu: " + ex.Message);
                    return;
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyalar�|*.jpg;*.jpeg;*.png;*.gif;*.bmp|T�m Dosyalar|*.*";

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
                    MessageBox.Show("Resim y�klenirken bir hata olu�tu: " + ex.Message);
                    return;
                }
            }
        }


        private void applyButton_Click(object sender, EventArgs e)
        {
            // History �zelli�i i�in gerekli
            if (isBrightnessMode)
            {
                ImgProcess.proceesedNames.Add("Brightness");
            }
            else if (isContrastMode)
            {
                ImgProcess.proceesedNames.Add("Contrast");
            }
            else if (isBlackWhiteMode)
            {
                ImgProcess.proceesedNames.Add("Black - White");
            }
            else if (isMakeGrayMode)
            {
                ImgProcess.proceesedNames.Add("Make Gray");
            }
            else if (isBinarizeMode)
            {
                ImgProcess.proceesedNames.Add("Binarize");
            }
            else if (isShifterMode)
            {
                ImgProcess.proceesedNames.Add("Shift");
            }


            ImgProcess.processedImages.Add((Bitmap)pictureBox.Image);

            // Program genelinde kullan�lan de�i�kenlere i�lemin uygulanmas�
            image = (Bitmap)pictureBox.Image;
            imageBeforeMode = image;

            // Stateleri s�f�rlama
            isBlackWhiteMode = false;
            isCropMode = false;
            isZoomMode = false;
            isBrightnessMode = false;
            isContrastMode = false;
            isMakeGrayMode = false;
            isBinarizeMode = false;
            isShifterMode = false;

            // After fonksiyonlar�n� �a��r ki her �ey kapanm�� gibi olsun.
            afterBlackWhiteMode();
            afterBrightnessMode();
            afterContrastMode();
            afterZoomMode();
            afterMakeGrayMode();
            afterBinarizeMode();
            afterShifterMode();

            if (ImgProcess.processedImages.Count > 1)
            {
                undoBtn.Enabled = true;
            }

            MessageBox.Show("Applied.");

            CreateHistoryTiles();

            ImgProcess.CalculteHistogram(histGraph, ref image);

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

            MessageBox.Show("Undo.");

            ImgProcess.CalculteHistogram(histGraph, ref image);

            Control lastControl = historyPanel.Controls[historyPanel.Controls.Count - 1];
            historyPanel.Controls.Remove(lastControl);
            lastControl.Dispose();
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
                ImgProcess.CalculteHistogram(histGraph, ref image);
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



        // - After fonksiyonlar� -
        // Tuhaf hatalar�n �n�ne ge�mek i�in yaz�ld�.
        // E�er bir i�lem yap�l�rken ba�ka bir i�lem yap�l�rsa tuhaf hatalar olu�uyor.
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

        private void afterMakeGrayMode()
        {
            Logger.Log("'MakeGray' fonksiyonu inaktif.");
            isMakeGrayMode = false;
            pictureBox.Image = imageBeforeMode;
        }

        private void afterContrastMode()
        {
            Logger.Log("'Contrast' fonksiyonu inaktif.");
            isContrastMode = false;
            trackBar1.Visible = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void afterBinarizeMode()
        {
            Logger.Log("'Binarize' fonksiyonu inaktif.");
            isBinarizeMode = false;
            trackBar1.Visible = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void afterBlackWhiteMode()
        {
            Logger.Log("'BlackWhite' fonksiyonu inaktif.");
            isBlackWhiteMode = false;
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


        // - Utility fonksiyonlar� -
        // Silersen uygulama �al��maz.
        // Puahahahhhaaa
        // K�rpma ve zoom fonksiyonlar� i�in gerekli
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
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
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
                pnl.Size = new Size(260, 100);
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

                pnl.Controls.Add(pb);
                pnl.Controls.Add(lbl);

                historyPanel.Controls.Add(pnl);

            }

        }

        private void openFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyalar�|*.jpg;*.jpeg;*.png;*.gif;*.bmp|T�m Dosyalar|*.*";

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

                    ImgProcess.CalculteHistogram(histGraph, ref image);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Resim y�klenirken bir hata olu�tu: " + ex.Message);
                }
            }
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            Bitmap selectedImage = (Bitmap)pictureBox.Image;
            if (selectedImage != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "JPEG Dosyas�|*.jpg";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        selectedImage.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                        MessageBox.Show("Resim ba�ar�yla kaydedildi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Resim kaydedilirken bir hata olu�tu: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("�nce bir resim se�melisiniz.");
            }

        }

        
    }
}