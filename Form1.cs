using System.Diagnostics.Metrics;
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

        Bitmap image = new Bitmap("image_2.jpg");
        //Bitmap image2 = new Bitmap("image.jpg");
        // PictureBox üzerinde kýrpma yapýlacak alaný temsil eden dikdörtgenin boyutu ve konumu
        Point pDown = Point.Empty;
        Rectangle rect = Rectangle.Empty;

        // Daha sonra ekleyeceðim kaydet butonuna basýlmadan görselin 
        // kaydedilmemesi için bir deðiþken
        Image imageBeforeMode = null;

        public Form1()
        {
            InitializeComponent();

            imageBeforeMode = image;

            /// -SÝLME- Loglarýn düzgün çalýþmasý için gerkeli!
            Logger logger = new Logger();

            /// -SÝLME- Zoom özelliðinin akýcýlýðý için gerekli!
            this.DoubleBuffered = true;

            /// -SÝLME- Undo iþlemi için gerekli!
            ImgProcess.processedImages.Add(image);
            undoBtn.Enabled = false;


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
            //ImgProcess.Shifter(pictureBox, ref image, 2000, 1800);
            //ImgProcess.Brightness(pictureBox, ref image, 100);
            //ImgProcess.BlackWhite(pictureBox, ref image);
            //ImgProcess.Contrast(pictureBox, ref image, 50);
            //ImgProcess.MakeGray(pictureBox, ref image);
            //ImgProcess.Binarize(pictureBox, ref image, 100);
            //EqualImageDimensionsAndPreventStrecthing(image2, image);
            //EqualImageDimensions(ref image2, ref image);
            //MultiplyImages(image, image);
        }


        private void PictureBox_MouseWheel(object? sender, MouseEventArgs e)
        {
            // Check if Alt key is pressed and mouse wheel is scrolled down
            if (isAltKeyPressed && e.Delta < 0)
            {

            }
            else if (isAltKeyPressed && e.Delta > 0)
            {

            }
        }

        public void MultiplyImages(Bitmap image1, Bitmap image2)
        {
            Bitmap temp = (Bitmap)image1;
            Bitmap bmap = (Bitmap)temp.Clone();
            Bitmap temp2 = (Bitmap)image2;
            Bitmap bmap2 = (Bitmap)temp2.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R * bmap2.GetPixel(i, j).R / 255;
                    int cG = c.G * bmap2.GetPixel(i, j).G / 255;
                    int cB = c.B * bmap2.GetPixel(i, j).B / 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            pictureBox.Image = bmap;
            MultiplyImages(bmap, bmap);
        }

        public void AddImages(Bitmap image1, Bitmap image2)
        {
            Bitmap temp = (Bitmap)image1;
            Bitmap bmap = (Bitmap)temp.Clone();
            Bitmap temp2 = (Bitmap)image2;
            Bitmap bmap2 = (Bitmap)temp2.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R + bmap2.GetPixel(i, j).R;
                    int cG = c.G + bmap2.GetPixel(i, j).G;
                    int cB = c.B + bmap2.GetPixel(i, j).B;

                    if (cR > 255) cR = 255;
                    if (cG > 255) cG = 255;
                    if (cB > 255) cB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            pictureBox.Image = bmap;
        }


        public void EqualImageDimensions(ref Bitmap image1, ref Bitmap image2)
        {
            int width = image1.Width;
            int height = image1.Height;
            if (image2.Width < width)
                width = image2.Width;
            if (image2.Height < height)
                height = image2.Height;
            image1 = new Bitmap(image1, new Size(width, height));
            image2 = new Bitmap(image2, new Size(width, height));

            pictureBox.Image = image2;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Þimdilik Gereksiz
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (isCropMode)
            {
                pDown = e.Location;
                pictureBox.Refresh();
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
                    g.DrawRectangle(Pens.Orange, rect);
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox.Image = image;
        }



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
        }

        // Burasý biraz karýþýk. Pek ellememek lazým.
        // Oðuzhana danýþ.
        private void zoomBtn_Click(object sender, EventArgs e)
        {
            if (isZoomMode)
            {
                afterZoomMode();

            }
            else
            {
                // After fonksiyonlarý
                afterBrightnessMode();
                afterContrastMode();
                afterBlackWhiteMode();

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

                imageBeforeMode = pictureBox.Image;

                // Büyük görsellerde aþýrý zoom yapýnca program uzun süre donuyor. 
                // Bunun için yüklü görselin boyutuna göre trackBar'ýn maximum deðeri belirlenmeli.
                // Aþaðýdaki oran baya güzel verimli çalýþýyor.
                trackBar1.Maximum = Convert.ToInt32((pictureBox.Image.Width <= pictureBox.Image.Height) ? 400000 / pictureBox.Image.Height : 400000 / pictureBox.Image.Width);
                trackBar1.Value = Convert.ToInt32((trackBar1.Maximum + 1) / 2);
                pictureBox.Image = ZoomPicture(image, new Size(trackBar1.Value, trackBar1.Value));
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                trackBar1.Visible = true;
            }
        }
        private void brightnessBtn_Click(object sender, EventArgs e)
        {
            if (isBrightnessMode)
            {
                afterBrightnessMode();
            }
            else
            {
                // After fonksiyonlarý
                afterContrastMode();
                afterZoomMode();
                afterBlackWhiteMode();

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Brightness' fonksiyonu aktif.");

                // State deðiþimleri için bir fonksiyon yazýlabilir.
                // Ama þimdilik böyle çünkü yoruldum. Sabah oldu saat 5:35
                isBrightnessMode = true;
                isContrastMode = false;
                isCropMode = false;
                isZoomMode = false;
                isCropMode = false;
                isCropMode = false;
                isMakeGrayMode = false;
                isBinarizeMode = false;
                isCropMode = false;


                trackBar1.Minimum = -255;
                trackBar1.Maximum = 255;

                trackBar1.Value = 0; // Mutlaka max ve minden sonra gelmeli
                trackBar1.SmallChange = 10;
                trackBar1.LargeChange = 10;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;

                imageBeforeMode = pictureBox.Image;
            }
        }

        private void contrastButton_Click(object sender, EventArgs e)
        {
            if (isContrastMode)
            {
                afterContrastMode();
            }
            else
            {
                // After fonksiyonlarý
                afterBrightnessMode();
                afterZoomMode();
                afterBlackWhiteMode();

                pictureBox.Image = imageBeforeMode;
                Logger.Log("'Contrast' fonksiyonu aktif.");

                // State deðiþimleri
                isContrastMode = true;
                isCropMode = false;
                isZoomMode = false;
                isBrightnessMode = false;
                isCropMode = false;
                isCropMode = false;
                isMakeGrayMode = false;
                isBinarizeMode = false;
                isCropMode = false;

                // Max ve min deðerleri deðiþtirilebilir :).

                trackBar1.Minimum = -100;
                trackBar1.Maximum = 100;

                trackBar1.Value = 0; // Mutlaka max ve minden sonra gelmeli
                trackBar1.SmallChange = 10;
                trackBar1.LargeChange = 10;
                trackBar1.UseWaitCursor = false;
                trackBar1.Visible = true;

                imageBeforeMode = pictureBox.Image;
            }
        }

        private void blackWhiteBtn_Click(object sender, EventArgs e)
        {
            //isBlackWhiteMode = true;
            //ImgProcess.BlackWhite(pictureBox, ref image);
            //Logger.Log("'BlackWhite' fonksiyonu çalýþtý.");
            //isBlackWhiteMode = false;

            if (isBlackWhiteMode)
            {
                afterBlackWhiteMode();
            }
            else
            {
                afterBrightnessMode();
                afterContrastMode();
                afterZoomMode();

                pictureBox.Image = imageBeforeMode;
                trackBar1.Visible = false;

                Logger.Log("'BlackWhite' fonksiyonu aktif.");

                // State deðiþimleri
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
            }

        }


        private void applyButton_Click(object sender, EventArgs e)
        {
            // History özelliði için gerekli
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
                ImgProcess.proceesedNames.Add("Black & White");
            }
            ImgProcess.processedImages.Add((Bitmap)pictureBox.Image);

            // Program genelinde kullanýlan deðiþkenlere iþlemin uygulanmasý
            image = (Bitmap)pictureBox.Image;
            imageBeforeMode = image;

            // Stateleri sýfýrlama
            isBlackWhiteMode = true;
            isCropMode = false;
            isZoomMode = false;
            isBrightnessMode = false;
            isContrastMode = false;
            isMakeGrayMode = false;
            isBinarizeMode = false;
            isShifterMode = false;

            // After fonksiyonlarýný çaðýrma ki her þey kapanmýþ gibi olsun.
            afterBlackWhiteMode();
            afterBrightnessMode();
            afterContrastMode();
            afterZoomMode();

            if(ImgProcess.processedImages.Count > 1)
            {
                undoBtn.Enabled = true;
            }

            MessageBox.Show("Applied.");
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

        private void afterContrastMode()
        {
            Logger.Log("'Contrast' fonksiyonu inaktif.");
            isContrastMode = false;
            trackBar1.Visible = false;

            pictureBox.Image = imageBeforeMode;
        }

        private void afterBlackWhiteMode()
        {
            Logger.Log("'BlackWhite' fonksiyonu inaktif.");
            isBlackWhiteMode = false;
            pictureBox.Image = imageBeforeMode;
        }


        // - Utility fonksiyonlarý -
        // Silersen uygulama çalýþmaz.
        // Puahahahhhaaa
        // Kýrpma ve zoom fonksiyonlarý için gerekli
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

        
    }
}