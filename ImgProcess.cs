
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image_processing_form
{
    public class ImgProcess
    {
        public static List<Bitmap> processedImages = new List<Bitmap>();
        public static List<string> proceesedNames = new List<string>();

        // Brightness fonksiyonu
        /// <summary>
        /// Görselin parlaklığını verilen değere göre ayarlar.
        /// </summary>
        /// <param name="pictureBox">Görselin bulunduğu konteyner.</param>
        /// <param name="image">Üzerinde çalışılan görsel.</param>
        /// <param name="value">Parlaklığın değişme değeri.</param>
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

        //BlackWhite fonksiyonu
        /// <summary>
        /// Görseli siyah beyaz hale getirir.
        /// </summary>
        /// <param name="pictureBox">Görselin bulunduğu konteyner.</param>
        /// <param name="image">Üzerinde çalışılan görsel.</param>
        public static void BlackWhite(PictureBox pictureBox, ref Bitmap image)
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
                        int cR = c.R;
                        int cG = c.G;
                        int cB = c.B;
                        int avg = (cR + cG + cB) / 3;
                        bmap.SetPixel(i, j, Color.FromArgb(avg, avg, avg));
                    }
                }
                pictureBox.Image = bmap;

                //// History özelliği için gerekli.
                //processedImages.Add(pictureBox.Image);
                //proceesedNames.Add("Black and White");
            }
            catch (Exception ex)
            {
                MessageBox.Show("'BlackWhite' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'BlackWhite' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
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
        /// <param name="treshold">Sınır değeri.</param>
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
                            bmap.SetPixel(i, j, Color.White);
                        else
                            bmap.SetPixel(i, j, Color.Black);
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

                //// History özelliği için gerekli.
                //processedImages.Add(pictureBox.Image);
                //proceesedNames.Add("Shifter: " + offsetX.ToString() + "; " + offsetY.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("'Shifter' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'Shifter' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
        }

        public static void  CalculteHistogram(FormsPlot histGraph, ref Bitmap image)
        {
            // Histogram dizisi oluştur
            int[] histogram = new int[256];

            // Her piksel için histogramu hesapla
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    System.Drawing.Color pixelColor = image.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11); // Renkleri gri tonlamaya dönüştür
                    histogram[grayValue]++; // Histogram dizisinde ilgili gri ton değerini artır
                }
            }

            //ScottPlot.Bar[] bars = 

            //histGraph.Plot.Add.Bars(histogram);
            histGraph.Plot.Clear();
            histGraph.Plot.Axes.SetLimits(0, 256, 0, 10);
            histGraph.Plot.Axes.AutoScaleExpand();

            for (int i = 0; i < histogram.Length; i++)
            {
                histGraph.Plot.Add.Bar(position: i, value: histogram[i]);
                histGraph.Plot.Axes.AutoScaleExpand();
                
                histGraph.Refresh();
            }

            histGraph.Refresh();

        }
    }
}
