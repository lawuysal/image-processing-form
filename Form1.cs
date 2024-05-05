using System.Windows.Forms;

namespace image_processing_form
{
    public partial class Form1 : Form
    {
        Bitmap image = new Bitmap("image_2.jpg");
        Bitmap image2 = new Bitmap("image.jpg");
        // PictureBox üzerinde kýrpma yapýlacak alaný temsil eden dikdörtgenin boyutu ve konumu
        Point pDown = Point.Empty;
        Rectangle rect = Rectangle.Empty;
        public Form1()
        {
            InitializeComponent();


            int width = image.Width;
            int height = image.Height;


            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = image;
            //Otelei();
            Brightness(100);
            //BlackWhite();
            //Contrast(50);
            //MakeGray();
            //Binarize(100);
            //EqualImageDimensionsAndPreventStrecthing(image2, image);
            //EqualImageDimensions(ref image2, ref image);
            // MultiplyImages(image, image);
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

        //Çalýþmýyor
        public void EqualImageDimensionsAndPreventStrecthing(Bitmap image1, Bitmap image2)
        {
            // Calculate scaling factors for both images
            float scaleX = (float)image2.Width / image1.Width;
            float scaleY = (float)image2.Height / image1.Height;

            // Choose the smaller scaling factor to maintain aspect ratio
            float scale = Math.Min(scaleX, scaleY);

            // Calculate new dimensions for the images
            int newWidth = (int)(image1.Width * scale);
            int newHeight = (int)(image1.Height * scale);

            // Resize both images using the calculated dimensions
            Bitmap resizedImage1 = new Bitmap(image1, newWidth, newHeight);
            Bitmap resizedImage2 = new Bitmap(image2, newWidth, newHeight);

            // Display the second image in the PictureBox
            pictureBox.Image = resizedImage2;

            // Dispose of the original images
            image1.Dispose();
            image2.Dispose();
        }

        //Çalýþmýyor
        public void EqualImageDimensionsWithoutStretching(Bitmap image1, Bitmap image2)
        {
            // Calculate scaling factors for both images
            float scaleX = (float)image2.Width / image1.Width;
            float scaleY = (float)image2.Height / image1.Height;

            // Choose the smaller scaling factor to maintain aspect ratio
            float scale = Math.Min(scaleX, scaleY);

            // Calculate new dimensions for the images
            int newWidth = (int)(image1.Width * scale);
            int newHeight = (int)(image1.Height * scale);

            // Resize both images using the calculated dimensions
            Bitmap resizedImage1 = new Bitmap(image1, newWidth, newHeight);
            Bitmap resizedImage2 = new Bitmap(image2, newWidth, newHeight);

            // Display the second image in the PictureBox
            pictureBox.Image = resizedImage2;

            // Dispose of the original images
            image1.Dispose();
            image2.Dispose();
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

        public void Binarize(int threshold)
        {
            Bitmap temp = (Bitmap)image;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int r = c.R;
                    int g = c.G;
                    int b = c.B;
                    int avg = (r + g + b) / 3;
                    if (avg > threshold)
                        bmap.SetPixel(i, j, Color.White);
                    else
                        bmap.SetPixel(i, j, Color.Black);
                }
            }
            pictureBox.Image = bmap;
        }

        public void MakeGray()
        {
            Bitmap temp = (Bitmap)image;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);
                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            pictureBox.Image = bmap;
        }

        public void Contrast(double value)
        {
            Bitmap temp = (Bitmap)image;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (value < -100) value = -100;
            if (value > 100) value = 100;
            value = (100.0 + value) / 100.0;
            value *= value;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    double pR = c.R / 255.0;
                    pR -= 0.5;
                    pR *= value;
                    pR += 0.5;
                    pR *= 255;
                    if (pR < 0) pR = 0;
                    if (pR > 255) pR = 255;

                    double pG = c.G / 255.0;
                    pG -= 0.5;
                    pG *= value;
                    pG += 0.5;
                    pG *= 255;
                    if (pG < 0) pG = 0;
                    if (pG > 255) pG = 255;

                    double pB = c.B / 255.0;
                    pB -= 0.5;
                    pB *= value;
                    pB += 0.5;
                    pB *= 255;
                    if (pB < 0) pB = 0;
                    if (pB > 255) pB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                }
            }
            pictureBox.Image = bmap;
        }

        public void BlackWhite()
        {
            Bitmap temp = (Bitmap)image;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R;
                    int cG = c.G;
                    int cB = c.B;
                    int avg = (cR + cG + cB) / 3;
                    bmap.SetPixel(i, j, Color.FromArgb(avg, avg, avg));
                }
            }
            pictureBox.Image = bmap;
        }

        public void Brightness(int value)
        {
            Bitmap temp = (Bitmap)image;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (value < -255) value = -255;
            if (value > 255) value = 255;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R + value;
                    int cG = c.G + value;
                    int cB = c.B + value;

                    if (cR < 0) cR = 1;
                    if (cR > 255) cR = 255;

                    if (cG < 0) cG = 1;
                    if (cG > 255) cG = 255;

                    if (cB < 0) cB = 1;
                    if (cB > 255) cB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            pictureBox.Image = bmap;
        }

        public void Otelei()
        {
            Bitmap originalBitmap = image;

            int offsetX = 2000;
            int offsetY = 1800;


            Bitmap offsetBitmap = new Bitmap(originalBitmap.Width + offsetX, originalBitmap.Height + offsetY);


            using (Graphics g = Graphics.FromImage(offsetBitmap))
            {
                g.Clear(Color.Black);
            }


            for (int y = 0; y < originalBitmap.Height; y++)
            {
                for (int x = 0; x < originalBitmap.Width; x++)
                {
                    Color pixelColor = originalBitmap.GetPixel(x, y);
                    offsetBitmap.SetPixel(x + offsetX, y + offsetY, pixelColor);
                }
            }


            pictureBox.Image = offsetBitmap;

        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Dikdörtgeni çiz
            //e.Graphics.DrawRectangle(Pens.Red, cropRectangle);
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            pDown = e.Location;
            pictureBox.Refresh();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!e.Button.HasFlag(MouseButtons.Left)) return;

            rect = new Rectangle(pDown, new Size(e.X - pDown.X, e.Y - pDown.Y));
            using (Graphics g = pictureBox.CreateGraphics())
            {
                pictureBox.Refresh();
                g.DrawRectangle(Pens.Orange, rect);
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox.Image = image;
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
    }
}