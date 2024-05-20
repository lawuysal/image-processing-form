
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;
using System.Drawing;

namespace image_processing_form
{
    public class ImgProcess
    {

        public delegate void AfterFunctions(); 

        public static List<Bitmap> processedImages = new List<Bitmap>();
        public static List<string> proceesedNames = new List<string>();

        public static Dictionary<string, Action<bool>> stateKeyValuePairs = new Dictionary<string, Action<bool>>();
        
        public static Dictionary<string, AfterFunctions> stateAfterFunctions = new Dictionary<string, AfterFunctions>();
 
 

        public static void ChangeMode(Modes mode)
        {
            if (!(mode.ToString() == "noMode"))
            {
                foreach (var e in stateKeyValuePairs)
                {
                    if (e.Key == mode.ToString())
                    {
                        e.Value(true);
                    }
                    else
                    {
                        e.Value(false);
                    }

                }
            }
            else
            {

                foreach (var e in stateKeyValuePairs)
                {
                    e.Value(false);
                }
            }
            
        }

        public static void CallAfters(Modes mode)
        {
            if(!(mode.ToString() == "noMode"))
            {
                foreach (var e in stateAfterFunctions)
                {
                    if(!(e.Key == mode.ToString()))
                    {
                        e.Value.DynamicInvoke();
                    }
                    
                }
            }
            else
            {
                foreach (var e in stateAfterFunctions)
                {
                    e.Value.DynamicInvoke();
                }
            }
            
        }

        // Brightness fonksiyonu
        /// <summary>
        /// Görselin parlaklığını verilen değere göre ayarlar.
        /// </summary>
        /// <param name="pictureBox">Görselin bulunduğu konteyner.</param>
        /// <param name="image">Üzerinde çalışılan görsel.</param>
        /// <param name="value">Parlaklığın değişme değeri.</param>
        /// 
        public static Bitmap Resize(Bitmap original, int newWidth, int newHeight)
        {
            Bitmap resized = new Bitmap(newWidth, newHeight);

            float xRatio = (float)original.Width / newWidth;
            float yRatio = (float)original.Height / newHeight;

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    int px = (int)(x * xRatio);
                    int py = (int)(y * yRatio);

                    Color c1 = original.GetPixel(px, py);
                    Color c2 = (px + 1 < original.Width) ? original.GetPixel(px + 1, py) : c1;
                    Color c3 = (py + 1 < original.Height) ? original.GetPixel(px, py + 1) : c1;
                    Color c4 = (px + 1 < original.Width && py + 1 < original.Height) ? original.GetPixel(px + 1, py + 1) : c1;

                    float xDiff = (x * xRatio) - px;
                    float yDiff = (y * yRatio) - py;

                    Color interpolatedColor = InterpolateColors(c1, c2, c3, c4, xDiff, yDiff);
                    resized.SetPixel(x, y, interpolatedColor);
                }
            }

            return resized;
        }

        private static Color InterpolateColors(Color c1, Color c2, Color c3, Color c4, float xDiff, float yDiff)
        {
            float r1 = c1.R * (1 - xDiff) + c2.R * xDiff;
            float g1 = c1.G * (1 - xDiff) + c2.G * xDiff;
            float b1 = c1.B * (1 - xDiff) + c2.B * xDiff;

            float r2 = c3.R * (1 - xDiff) + c4.R * xDiff;
            float g2 = c3.G * (1 - xDiff) + c4.G * xDiff;
            float b2 = c3.B * (1 - xDiff) + c4.B * xDiff;

            int r = (int)(r1 * (1 - yDiff) + r2 * yDiff);
            int g = (int)(g1 * (1 - yDiff) + g2 * yDiff);
            int b = (int)(b1 * (1 - yDiff) + b2 * yDiff);

            return Color.FromArgb(r, g, b);
        }
        public static void Brightness(PictureBox pictureBox, ref Bitmap image, int value)
        {
            try
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

                // History özelliği için gerekli.
                //processedImages.Add(pictureBox.Image);
                //proceesedNames.Add("Brightness: " + value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("'Brightness' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'Brightness' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
        }

        //Contrast fonksiyonu
        /// <summary>
        /// Görselin kontrastını verilen değere göre ayarlar.
        /// </summary>
        /// <param name="pictureBox">Görselin bulunduğu konteyner.</param>
        /// <param name="image">Üzerinde çalışılan görsel.</param>
        /// <param name="value">Kontrastın değişme değeri.</param>
        public static void Contrast(PictureBox pictureBox, ref Bitmap image, double value)
        {
            try
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

                //// History özelliği için gerekli.
                //processedImages.Add(pictureBox.Image);
                //proceesedNames.Add("Contrast: " + value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("'Contrast' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'Contrast' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
        }

        //MakeGray fonksiyonu
        /// <summary>
        /// Görseli gri hale getirir.
        /// </summary>
        /// <param name="pictureBox">Görselin bulunduğu konteyner.</param>
        /// <param name="image">Üzerinde çalışılan görsel.</param>
        public static void MakeGray(PictureBox pictureBox, ref Bitmap image)
        {
            try
            { // Resmin nasıl işlenmesini istiyorsak try bloğunun içine yazılır.
                // Atama en son picture box a yapılır.
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
                pictureBox.Image = bmap; // burada
            }
            catch (Exception ex)
            { // catch bloğu kopyala yapıştır olabilir sıkıntı yok aq
                MessageBox.Show("'MakeGray' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'MakeGray' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
        }

        //Binarize fonksiyonu
        /// <summary>
        /// Görselin yalnızca bir değerden sonrasını beyaz geri kalanını siyah yapar.
        /// </summary>
        /// <param name="pictureBox">Görselin bulunduğu konteyner.</param>
        /// <param name="image">Üzerinde çalışılan görsel.</param>
        /// <param name="threshold">Sınır değeri.</param>
        public static void Binarize(PictureBox pictureBox, ref Bitmap image, int threshold)
        {
            try
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
                            bmap.SetPixel(i, j, Color.FromArgb(255,255,255));
                        else
                            bmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
                pictureBox.Image = bmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("'Binarize' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'Binarize' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
        }

        //Shifter fonksiyonu
        /// <summary>
        /// Görseli verilen değerlere göre kaydırır.
        /// </summary>
        /// <param name="pictureBox">Görselin bulunduğu konteyner.</param>
        /// <param name="image">Üzerinde çalışılan görsel.</param>
        /// <param name="offsetX">X ekseninde kaydırma miktarı.</param>
        /// /// <param name="offsetY">Y ekseninde kaydırma miktarı. </param>
        public static void Shifter(PictureBox pictureBox, ref Bitmap image, int offsetX, int offsetY)
        {
            try
            {
                Bitmap originalBitmap = image;
                Bitmap offsetBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

                // Shift the image
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    for (int x = 0; x < originalBitmap.Width; x++)
                    {
                        int newX = x + offsetX;
                        int newY = y + offsetY;

                        // Check bounds
                        if (newX >= 0 && newX < offsetBitmap.Width && newY >= 0 && newY < offsetBitmap.Height)
                        {
                            offsetBitmap.SetPixel(newX, newY, originalBitmap.GetPixel(x, y));
                        }
                    }
                }

                // Fill the shifted area with black
                if (offsetX > 0)
                {
                    for (int y = 0; y < originalBitmap.Height; y++)
                    {
                        for (int x = 0; x < offsetX; x++)
                        {
                            offsetBitmap.SetPixel(x, y, Color.Black);
                        }
                    }
                }
                else if (offsetX < 0)
                {
                    for (int y = 0; y < originalBitmap.Height; y++)
                    {
                        for (int x = originalBitmap.Width + offsetX; x < originalBitmap.Width; x++)
                        {
                            offsetBitmap.SetPixel(x, y, Color.Black);
                        }
                    }
                }

                if (offsetY > 0)
                {
                    for (int y = 0; y < offsetY; y++)
                    {
                        for (int x = 0; x < originalBitmap.Width; x++)
                        {
                            offsetBitmap.SetPixel(x, y, Color.Black);
                        }
                    }
                }
                else if (offsetY < 0)
                {
                    for (int y = originalBitmap.Height + offsetY; y < originalBitmap.Height; y++)
                    {
                        for (int x = 0; x < originalBitmap.Width; x++)
                        {
                            offsetBitmap.SetPixel(x, y, Color.Black);
                        }
                    }
                }

                pictureBox.Image = offsetBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("'Shifter' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'Shifter' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
        }

        //Histogram fonksiyonu
        /// <summary>
        /// Görselin histogramını hesaplar ve görselleştirir.
        /// </summary>
        /// <param name="histGraph">Histogramın çizileceği grafik.</param>
        /// <param name="image">Üzerinde çalışılan görsel.</param>
        public static void CalculateHistogram(FormsPlot histGraph, ref Bitmap image)
        {
            int[] histogram = new int[256];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    System.Drawing.Color pixelColor = image.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    histogram[grayValue]++;
                }
            }

            UpdateHistogramPlot(histGraph, histogram);
        }


        public static Bitmap StretchHistogram(Bitmap image)
        {
            int[] histogram = new int[256];
            int minGray = 255;
            int maxGray = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    System.Drawing.Color pixelColor = image.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    histogram[grayValue]++;
                    if (grayValue < minGray)
                        minGray = grayValue;
                    if (grayValue > maxGray)
                        maxGray = grayValue;
                }
            }

            Bitmap stretchedImage = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    System.Drawing.Color pixelColor = image.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    int stretchedGrayValue = (grayValue - minGray) * 255 / (maxGray - minGray);
                    stretchedImage.SetPixel(x, y, System.Drawing.Color.FromArgb(stretchedGrayValue, stretchedGrayValue, stretchedGrayValue));
                }
            }

            return stretchedImage;
        }


        public static void UpdateHistogramPlot(FormsPlot histGraph, int[] histogram)
        {
            histGraph.Plot.Clear();
            histGraph.Plot.Axes.SetLimits(0, 256, 0, histogram.Max());
            histGraph.Plot.Axes.AutoScaleExpand();

            for (int i = 0; i < histogram.Length; i++)
            {
                histGraph.Plot.Add.Bar(position: i, value: histogram[i]);
            }

            histGraph.Refresh();
        }




        //Multiply fonksiyonu
        /// <summary>
        /// İki görseli çarpar.
        /// </summary>
        /// <param name="pictureBox">Görselin bulunduğu konteyner.</param>
        /// <param name="image1">Üzerinde çalışılan görsel.</param>
        /// <param name="image2">Sonradan eklenen görsel.</param>
        public static void MultiplyImages(PictureBox pictureBox, ref Bitmap image1, ref Bitmap image2)
        {
            Bitmap[] images = ImgProcess.EqualImageDimensions(ref image1, ref image2);
            image1 = images[0];
            image2 = images[1];

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
        }

        //Add fonksiyonu
        /// <summary>
        /// İki görseli toplar.
        /// </summary>
        /// <param name="pictureBox">Görselin bulunduğu konteyner.</param>
        /// <param name="image1">Üzerinde çalışılan görsel.</param>
        /// <param name="image2">Sonradan eklenen görsel.</param>
        public static void AddImages(PictureBox pictureBox, ref Bitmap image1, ref Bitmap image2)
        {
            Bitmap[] images = ImgProcess.EqualImageDimensions(ref image1, ref image2);
            image1 = images[0];
            image2 = images[1];

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

        //Equal Image Dimensions fonksiyonu
        /// <summary>
        /// İki görselin boyutlarını büyük olana göre eşitler.
        /// </summary>
        /// <param name="image1">Üzerinde çalışılan görsel.</param>
        /// <param name="image2">Üzerinde çalışılan görsel.</param>
        /// <returns>Boyutları eşitlenmiş iki görsel.</returns>
        public static Bitmap[] EqualImageDimensions(ref Bitmap image1, ref Bitmap image2)
        {
            int width = image1.Width;
            int height = image1.Height;
            if (image2.Width > width)
                width = image2.Width;
            if (image2.Height > height)
                height = image2.Height;
            image1 = new Bitmap(image1, new Size(width, height));
            image2 = new Bitmap(image2, new Size(width, height));

            return new Bitmap[] { image1, image2 };
        }

        //Görsel döndürme
        public static void Rotator(PictureBox pictureBox, ref Bitmap image, float angle)
        {
            try
            {
                Bitmap originalBitmap = image;


                RectangleF rotatedRectangle = new RectangleF(PointF.Empty, originalBitmap.Size);
                GraphicsPath path = new GraphicsPath();
                path.AddRectangle(rotatedRectangle);
                Matrix matrix = new Matrix();
                matrix.Rotate(angle);
                path.Transform(matrix);
                RectangleF rotatedBounds = path.GetBounds();


                Bitmap rotatedBitmap = new Bitmap((int)rotatedBounds.Width, (int)rotatedBounds.Height);

                using (Graphics g = Graphics.FromImage(rotatedBitmap))
                {
                    g.TranslateTransform(rotatedBounds.Width / 2, rotatedBounds.Height / 2);
                    g.RotateTransform(angle); // Rotate the image
                    g.TranslateTransform(-originalBitmap.Width / 2, -originalBitmap.Height / 2);

                    g.DrawImage(originalBitmap, new Point(0, 0));

                    pictureBox.Image = rotatedBitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("'Rotator' function encountered an error: " + ex.Message);
                Logger.Log("'Rotator' function encountered an error: " + ex.Message);
            }
        }

        public static void ApplyBlurringFilter(PictureBox pBox, Bitmap imageToBeBlurred, int blurRadius)
        {
            Bitmap blurredImage = new Bitmap(imageToBeBlurred.Width, imageToBeBlurred.Height);

            BitmapData lockedImageMap = imageToBeBlurred.LockBits(
                new Rectangle(0, 0, imageToBeBlurred.Width, imageToBeBlurred.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            BitmapData lockedBlurredImageMap = blurredImage.LockBits(
                new Rectangle(0, 0, imageToBeBlurred.Width, imageToBeBlurred.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            double[] blurKernel = GenerateDiskKernel(blurRadius);


            try
            {

                unsafe
                {

                    byte* imageStartingPoint = (byte*)lockedImageMap.Scan0;
                    byte* blurredImageStartingPoint = (byte*)lockedBlurredImageMap.Scan0;

                    for (int y = 0; y < imageToBeBlurred.Height; y++)
                    {
                        for (int x = 0; x < imageToBeBlurred.Width; x++)
                        {
                            int totalRed = 0, totalGreen = 0, totalBlue = 0;
                            int pixelCount = 0;
                            for (int offsetX = -blurRadius; offsetX <= blurRadius; offsetX++)
                            {
                                for (int offsetY = -blurRadius; offsetY <= blurRadius; offsetY++)
                                {
                                    int newX = Math.Min(Math.Max(x + offsetX, 0), imageToBeBlurred.Width - 1);
                                    int newY = Math.Min(Math.Max(y + offsetY, 0), imageToBeBlurred.Height - 1);

                                    byte* curPixel = imageStartingPoint + lockedImageMap.Stride * newY + 3 * newX;
                                    totalRed += (int)(curPixel[2] * blurKernel[pixelCount]);
                                    totalGreen += (int)(curPixel[1] * blurKernel[pixelCount]);
                                    totalBlue += (int)(curPixel[0] * blurKernel[pixelCount]);
                                    pixelCount++;
                                }
                            }

                            byte* newPixel = blurredImageStartingPoint + lockedBlurredImageMap.Stride * y + 3 * x;
                            newPixel[0] = (byte)(totalBlue);
                            newPixel[1] = (byte)(totalGreen);
                            newPixel[2] = (byte)(totalRed);

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("'ApplyBlurringFilter' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'ApplyBlurringFilter' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }

            imageToBeBlurred.UnlockBits(lockedImageMap);
            blurredImage.UnlockBits(lockedBlurredImageMap);

            pBox.Image = blurredImage;

        }

        private static double[] GenerateDiskKernel(int radius)
 {
             int size = radius * 2 + 1;
             double[,] kernel = new double[size, size];
             double sum = 0.0;
             int center = radius;

             for (int y = 0; y < size; y++)
             {
                 for (int x = 0; x < size; x++)
                 {
                     double distance = Math.Sqrt((x - center) * (x - center) + (y - center) * (y - center));
                     if (distance <= radius)
                     {
                         kernel[y, x] = 1.0;
                         sum += 1.0;
                     }
                     else
                     {
                         kernel[y, x] = 0.0;
                     }
                 }
             }

     
             for (int y = 0; y < size; y++)
             {
                 for (int x = 0; x < size; x++)
                 {
                     kernel[y, x] /= sum;
                 }
             }

             double[] sequenced = SequenceDiskKernel(kernel, size);

             return sequenced;
 }

 private static double[] SequenceDiskKernel(double[,] kernel,int size)
 {
     double[] sequencedKernel = new double[size*size];
     int count = 0;
     for(int i = 0; i < size; i++)
     {
         for(int j = 0; j < size; j++)
         {
             sequencedKernel[count++] = kernel[i, j];
         }
     }

     return sequencedKernel;
 }

        public static void ConvoluteImage(PictureBox pBox, Bitmap image, int radius, double sigma)
        {
            int width = image.Width;
            int height = image.Height;
            Bitmap convolutedImage = new Bitmap(width, height);

            double[,] kernel = GenerateGaussianKernel(radius, sigma);

            BitmapData imageData = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData convolutedImageData = convolutedImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                unsafe
                {
                    byte* imageDataStrPoint = (byte*)imageData.Scan0;
                    byte* convolutedImageStrPoint = (byte*)convolutedImageData.Scan0;

                    int stride = imageData.Stride;
                    int bytesPerPixel = 4;

                    for (int y = 0; y < height-1; y++)
                    {
                        for (int x = 0; x < width-1; x++)
                        {
                            double red = 0, green = 0, blue = 0;

                            for (int dx = -radius; dx <= radius; dx++)
                            {
                                for (int dy = -radius; dy <= radius; dy++)
                                {
                                    int newX = Math.Clamp(x + dx, 0, width-1);
                                    int newY = Math.Clamp(y + dy, 0, height-1);
                                    byte* pixel = imageDataStrPoint + newY * stride + newX * bytesPerPixel;

                                    double weight = kernel[dx + radius, dy + radius];
                                    red += pixel[2] * weight;
                                    green += pixel[1] * weight;
                                    blue += pixel[0] * weight;
                                }
                            }


                            byte newRed = (byte)Math.Max(Math.Min(red, 255), 0);
                            byte newGreen = (byte)Math.Max(Math.Min(green, 255), 0);
                            byte newBlue = (byte)Math.Max(Math.Min(blue, 255), 0);


                            byte* blurredPixel = convolutedImageStrPoint + y * stride + x * bytesPerPixel;
                            blurredPixel[0] = newBlue;
                            blurredPixel[1] = newGreen;
                            blurredPixel[2] = newRed;
                            blurredPixel[3] = 255;
                        }
                    }
                }

                image.UnlockBits(imageData);
                convolutedImage.UnlockBits(convolutedImageData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("'Convolution' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'Convolution' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }


            pBox.Image = convolutedImage;
        }

        //utility function for convolution
        private static double[,] GenerateGaussianKernel(int radius, double sigma)
        {
            int size = 2 * radius + 1;
            double[,] kernel = new double[size, size];
            double sum = 0;

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    double exponent = -(x * x + y * y) / (2 * sigma * sigma);
                    double weight = Math.Exp(exponent) / (2 * Math.PI * sigma * sigma);
                    kernel[x + radius, y + radius] = weight;
                    sum += weight;
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    kernel[i, j] /= sum;
                }
            }

            return kernel;
        }


        public static void EdgeDetection(PictureBox pBox,Bitmap image)
        {

            Bitmap edgeDetectedImage = new Bitmap(image.Width, image.Height);
            BitmapData lockedImage = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData lockedEdgeDetectedImage =
                edgeDetectedImage.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int[,] SobelX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] SobelY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };


            try
            {
                unsafe
                {
                    byte* imageStartingPoint = (byte*)lockedImage.Scan0;
                    byte* edgeDetectedImgStrtPoint = (byte*)lockedEdgeDetectedImage.Scan0;

                    int xByteDistanceRate = 4;

                    for (int y = 1; y < image.Height - 1; y++)
                    {
                        for (int x = 1; x < image.Width - 1; x++)
                        {
                            int gx = 0, gy = 0;

                            for (int sY = -1; sY <= (SobelY.Length / 3) / 2; sY++)
                            {
                                for (int sX = -1; sX <= (SobelX.Length / 3) / 2; sX++)
                                {
                                    int offsetX = x + sX;
                                    int offsetY = y + sY;

                                    byte* neighboorPixel = imageStartingPoint + lockedImage.Stride * offsetY + offsetX * xByteDistanceRate;

                                    int intensity = neighboorPixel[0];

                                    gx += intensity * SobelX[sY + 1, sX + 1];
                                    gy += intensity * SobelY[sY + 1, sX + 1];
                                }
                            }

                            int g = (int)Math.Sqrt(gx * gx + gy * gy);
                            byte edgeValue = (byte)Math.Max(0, Math.Min(255, g));

                            byte* newPixel = edgeDetectedImgStrtPoint + lockedEdgeDetectedImage.Stride * y + x * xByteDistanceRate;

                            newPixel[0] = (byte)edgeValue;
                            newPixel[1] = (byte)edgeValue;
                            newPixel[2] = (byte)edgeValue;
                            newPixel[3] = 255;
                        }
                    }
                }

                image.UnlockBits(lockedImage);
                edgeDetectedImage.UnlockBits(lockedEdgeDetectedImage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("'EdgeDetection' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'EdgeDetection' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
            

            pBox.Image = edgeDetectedImage;
        }

        public static void AddSaltAndPepperNoise(PictureBox pBox,Bitmap originalImage, double noiseDensity)
        {
            Bitmap noisyImage = new Bitmap(originalImage.Width, originalImage.Height);
            Random rand = new Random();
            int threshold = (int)(noiseDensity * 100);

            try
            {
                for (int y = 0; y < originalImage.Height; y++)
                {

                    for (int x = 0; x < originalImage.Width; x++)
                    {

                        Color pixel = originalImage.GetPixel(x, y);

                        int random = rand.Next(100);


                        if (random < threshold)
                        {

                            noisyImage.SetPixel(x, y, Color.White);
                        }
                        else if (random > 100 - threshold)
                        {
                            noisyImage.SetPixel(x, y, Color.Black);
                        }
                        else
                        {
                            noisyImage.SetPixel(x, y, pixel);
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("'AddSaltAndPepperNoise' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'AddSaltAndPepperNoise' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
            
            pBox.Image = noisyImage;
        }


        public static void MeanFilter(PictureBox pBox,Bitmap noisedImage, int radius)
        {
            Bitmap filteredImage = new Bitmap(noisedImage.Width, noisedImage.Height);

            BitmapData lockedNoisedImage = noisedImage.LockBits(
                new Rectangle(0, 0, noisedImage.Width, noisedImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            BitmapData lockedFilteredImg = filteredImage.LockBits(
                new Rectangle(0, 0, filteredImage.Width, filteredImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);


            try
            {
                unsafe
                {
                    byte* noisedImgStrPoint = (byte*)(lockedNoisedImage.Scan0);

                    byte* filteredImgStrPoint = (byte*)(lockedFilteredImg.Scan0);

                    for (int y = 0; y < noisedImage.Height - 1; y++)
                    {
                        for (int x = 0; x < noisedImage.Width - 1; x++)
                        {
                            int sumR = 0, sumG = 0, sumB = 0;
                            int count = 0;

                            for (int kY = -radius; kY <= radius; kY++)
                            {
                                for (int kX = -radius; kX <= radius; kX++)
                                {
                                    int neighboorY = y + kY < 0 ? 0 : (y + kY > noisedImage.Height - 1 ? noisedImage.Height - 1 : y + kY);
                                    int neighboorX = x + kX < 0 ? 0 : (x + kX > noisedImage.Width - 1 ? noisedImage.Width - 1 : x + kX);
                                    byte* currentPixel = noisedImgStrPoint + lockedNoisedImage.Stride * neighboorY+ neighboorX * 3;

                                    sumR += currentPixel[2];
                                    sumG += currentPixel[1];
                                    sumB += currentPixel[0];
                                    count++;

                                }
                            }

                            byte* newPixel = filteredImgStrPoint + y * lockedFilteredImg.Stride + 3 * x;

                            newPixel[0] = (byte)(sumB / count);
                            newPixel[1] = (byte)(sumG / count);
                            newPixel[2] = (byte)(sumR / count);
                        }
                    }
                }
                noisedImage.UnlockBits(lockedNoisedImage);
                filteredImage.UnlockBits(lockedFilteredImg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("'MeanFilter' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'MeanFilter' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
            
            pBox.Image = filteredImage;
        }


        public static void MedianFilter(PictureBox pBox,Bitmap noisedImage, int kernelSize)
        {

            Bitmap filteredImg = new Bitmap(noisedImage.Width, noisedImage.Height);

            BitmapData lockedNoisedImage = noisedImage.LockBits(
               new Rectangle(0, 0, noisedImage.Width, noisedImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            BitmapData lockedFilteredImg = filteredImg.LockBits(
               new Rectangle(0, 0, filteredImg.Width, filteredImg.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int radius = kernelSize / 2;

            int[] red = new int[kernelSize * kernelSize];
            int[] green = new int[kernelSize * kernelSize];
            int[] blue = new int[kernelSize * kernelSize];

            try
            {
                unsafe
                {

                    byte* noisedImgStrPoint = (byte*)(lockedNoisedImage.Scan0);

                    byte* filteredImgStrPoint = (byte*)(lockedFilteredImg.Scan0);

                    for (int y = 0; y < noisedImage.Height ; y++)
                    {
                        for (int x = 0; x < noisedImage.Width ; x++)
                        {
                            int index = 0;
                            for (int kY = -radius; kY <= radius; kY++)
                            {
                                
                                for (int kX = -radius; kX <= radius; kX++)
                                {
                                    
                                    int neighboorY = y + kY < 0 ? 0 : (y + kY > noisedImage.Height - 1 ? noisedImage.Height - 1 : y + kY);
                                    int neighboorX = x + kX < 0 ? 0 : (x + kX > noisedImage.Width - 1 ? noisedImage.Width - 1 : x + kX);
                                    byte* neighboorPixel = noisedImgStrPoint + lockedNoisedImage.Stride * neighboorY+ neighboorX * 3;
                                   

                                    red[index] = neighboorPixel[2];
                                    green[index] = neighboorPixel[1];
                                    blue[index] = neighboorPixel[0];
                                    index++;

                                }
                            }

                            Array.Sort(red);
                            Array.Sort(green);
                            Array.Sort(blue);

                            byte* newPixel = filteredImgStrPoint + y * lockedNoisedImage.Stride + x * 3;

                            newPixel[0] = (byte)blue[(kernelSize * kernelSize) / 2];
                            newPixel[1] = (byte)green[(kernelSize * kernelSize) / 2];
                            newPixel[2] = (byte)red[(kernelSize * kernelSize) / 2];


                        }
                    }
                }

            }catch (Exception ex)
            {
                MessageBox.Show("'MedianFilter' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'MedianFilter' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }

            noisedImage.UnlockBits(lockedNoisedImage);
            filteredImg.UnlockBits(lockedFilteredImg);


            pBox.Image = filteredImg;

        }


        public static unsafe Bitmap Dilate(Bitmap image, int radius)
        {
            Bitmap dilatedImage = new Bitmap(image.Width, image.Height, image.PixelFormat);

            BitmapData originalImageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dilatedImageData = dilatedImage.LockBits(new Rectangle(0, 0, dilatedImage.Width, dilatedImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int heightInPixels = originalImageData.Height;
            int widthInPixels = originalImageData.Width;

            byte* ogImageStrPoint = (byte*)originalImageData.Scan0;
            byte* dilatedImageStrPoint = (byte*)dilatedImageData.Scan0;

            try
            {
                for (int y = 0; y < heightInPixels; y++)
                {
                    for (int x = 0; x < widthInPixels; x++)
                    {
                        byte maxR = 0, maxG = 0, maxB = 0;

                        for (int fy = -radius; fy <= radius; fy++)
                        {
                            for (int fx = -radius; fx <= radius; fx++)
                            {
                                int offsetY = Math.Clamp(y + fy, 0, heightInPixels - 1);
                                int offsetX = Math.Clamp(x + fx, 0, widthInPixels - 1);
                                byte* neighboorPixel = ogImageStrPoint + (offsetY * originalImageData.Stride) + (offsetX * 3);

                                maxB = Math.Max(maxB, neighboorPixel[0]);
                                maxG = Math.Max(maxG, neighboorPixel[1]);
                                maxR = Math.Max(maxR, neighboorPixel[2]);
                            }
                        }

                        byte* newPixel = dilatedImageStrPoint + (y * dilatedImageData.Stride) + (x * 3);
                        newPixel[0] = maxB;
                        newPixel[1] = maxG;
                        newPixel[2] = maxR;

                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("'Dilate' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'Dilate' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
            image.UnlockBits(originalImageData);
            dilatedImage.UnlockBits(dilatedImageData);

            return dilatedImage;
        }


        public unsafe static Bitmap Erode(Bitmap image, int radius)
        {
            Bitmap erodedImage = new Bitmap(image.Width, image.Height);

            BitmapData originalImageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData erodedImageData = erodedImage.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);


            byte* ogImageStrPoint = (byte*)originalImageData.Scan0;
            byte* erodedImageStrPoint = (byte*)erodedImageData.Scan0;


            try
            {
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {

                        int minR = 255; int minG = 255; int minB = 255;


                        for (int kY = -radius; kY < radius; kY++)
                        {
                            for (int kX = -radius; kX < radius; kX++)
                            {
                                int offsetY = Math.Clamp(y + kY, 0, image.Height - 1);
                                int offsetX = Math.Clamp(x + kX, 0, image.Width - 1);

                                byte* neighboorPixel = ogImageStrPoint + originalImageData.Stride * offsetY + offsetX * 3;

                                minB = Math.Min(minB, neighboorPixel[0]);
                                minG = Math.Min(minG, neighboorPixel[1]);
                                minR = Math.Min(minR, neighboorPixel[2]);
                            }
                        }

                        byte* newPixel = erodedImageStrPoint + y * erodedImageData.Stride + x * 3;
                        byte* oldPixel = ogImageStrPoint + y * originalImageData.Stride + x * 3;

                        newPixel[0] = minB == 255 ? oldPixel[0] : (byte)minB;
                        newPixel[1] = minG == 255 ? oldPixel[1] : (byte)minG;
                        newPixel[2] = minR == 255 ? oldPixel[2] : (byte)minR;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("'Erode' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'Erode' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);

            }

            image.UnlockBits(originalImageData);
            erodedImage.UnlockBits(erodedImageData);

            return erodedImage;
        }

        public static void Opening(PictureBox pBox, Bitmap image, int radius)
        {
            Bitmap erodedImage = Erode(image, radius);
            Bitmap openedImage = Dilate(erodedImage, radius);
            erodedImage.Dispose();
            pBox.Image = openedImage;
        }

        public static void Closing(PictureBox pBox, Bitmap image, int radius)
        {
            Bitmap dilatedImage = Dilate(image, radius);
            Bitmap closedImage = Erode(dilatedImage, radius);
            dilatedImage.Dispose();
            pBox.Image = closedImage;
        }


        public unsafe static void AdaptiveThresholding(PictureBox pBox, Bitmap image, double a, double b)
        {

            int w = image.Width;
            int h = image.Height;
            Bitmap thresholdedImage = new Bitmap(w, h);

            BitmapData originalImageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData thresholdedImageData = thresholdedImage.LockBits(new Rectangle(0, 0, thresholdedImage.Width, thresholdedImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int bytes = originalImageData.Stride * originalImageData.Height;
            byte* strPointOfOgImg = (byte*)originalImageData.Scan0;
            byte* strPointOfThresholdedImg = (byte*)thresholdedImageData.Scan0;

            double globalMean = 0.0;
            for (int i = 0; i < bytes; i += 3)
            {
                globalMean += strPointOfOgImg[i];
            }
            globalMean /= h * w;

            try
            {
                for (int y = 1; y < h - 1; y++)
                {
                    for (int x = 1; x < w - 1; x++)
                    {
                        int position = y * originalImageData.Stride + x * 3;
                        double locSum = 0;
                        double locSumSq = 0;
                        int count = 0;

                        for (int kY = -1; kY < 1; kY++)
                        {
                            for (int kX = -1; kX < 1; kX++)
                            {

                                int neighboorPos = position + kY * originalImageData.Stride + kX * 3;
                                byte pixelValue = strPointOfOgImg[neighboorPos];
                                locSum += pixelValue;
                                locSumSq += pixelValue * pixelValue;
                                count++;
                            }
                        }

                        double mean = locSum / count;
                        double variance = (locSumSq / count) - (mean * mean);
                        double stdDv = Math.Sqrt(variance);

                        double threshold = a * stdDv + b * globalMean;

                        for (int i = 0; i < 3; i++)
                        {

                            strPointOfThresholdedImg[position + i] = (byte)((threshold < strPointOfOgImg[position]) ? 255 : 0);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("'AdaptiveThreshold' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'AdaptiveThreshold' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }

            image.UnlockBits(originalImageData);
            thresholdedImage.UnlockBits(thresholdedImageData);
            pBox.Image = thresholdedImage;

        }

        
        public static void ConvertToSelectedColorSpace(ComboBox ColorSpaceComboBox, PictureBox pictureBox)
        {

            string selectedColorSpace = ColorSpaceComboBox.SelectedItem.ToString();

            Bitmap originalImage = (Bitmap)pictureBox.Image;

            MagickImage magickImage = new MagickImage(new MagickColor(0, 0, 0), originalImage.Width, originalImage.Height);

         
            Bitmap convertedImage = new Bitmap(originalImage.Width, originalImage.Height);

            
            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int z = 0; z < originalImage.Height; z++)
                {
                    Color pixelColor = originalImage.GetPixel(x, z);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;

                    switch (selectedColorSpace)
                    {
                        case "CMYK":
                            double cyan, magenta, yellow, black;
                            ColorSpace.RgbToCmyk(red, green, blue, out cyan, out magenta, out yellow, out black);

                            byte nCyan = (byte)(255 * cyan);
                            byte nMagenta = (byte)(255 * magenta);
                            byte nYellow = (byte)(255 * yellow);
                            byte nBlack = (byte)(255 * black);

                            
                            byte[] pixelData = new byte[] { nCyan, nMagenta, nYellow, nBlack };

                         
                            ReadOnlySpan<byte> pixelSpan = new ReadOnlySpan<byte>(pixelData);

                            magickImage.ColorSpace = ImageMagick.ColorSpace.CMYK;

                            magickImage.GetPixels().SetPixel(x, z, pixelSpan);

                            

                            break;
                        case "HSI":

                            double hsiHue, intensity, hsiSaturation;
                            ColorSpace.RgbToHsi(red, green, blue, out hsiHue, out intensity, out hsiSaturation);
                            convertedImage.SetPixel(x, z, ColorSpace.ColorFromHSI(hsiHue, intensity, hsiSaturation));
                            break;
                        case "YCC":
                            double y, cb, cr;
                            ColorSpace.RgbToYcc(red, green, blue, out y, out cb, out cr);
                            convertedImage.SetPixel(x, z, Color.FromArgb((int)y, (int)cb, (int)cr));
                            
                            break;
                        case "YES":
                            double yesY, e, s;
                            ColorSpace.RgbToYes(red, green, blue, out yesY, out e, out s);
                            convertedImage.SetPixel(x, z, Color.FromArgb((int)yesY, (int)e, (int)s));
                            
                            break;
                        default:

                            break;
                    }
                    
                    

                }
            }
            if (selectedColorSpace == "CMYK")
            {
                byte[] imageBytes = magickImage.ToByteArray(MagickFormat.Bmp);

                var ms = new MemoryStream(imageBytes);

                Bitmap newBitmapImage = new Bitmap(ms);
                pictureBox.Image = newBitmapImage;

            }
            else { pictureBox.Image = convertedImage; }

            //pictureBox.Image = convertedImage
            pictureBox.Refresh();
        }



    }
}
