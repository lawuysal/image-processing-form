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
        public static List<Image> processedImages = new List<Image>();
        public static List<string> proceesedNames = new List<string>();

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
                Logger.Log("'Brightness' fonksiyonu çalıştırıldı.");
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
                processedImages.Add(pictureBox.Image);
                proceesedNames.Add("Brightness: " + value.ToString());

                Logger.Log("'Brightness' fonksiyonu tamamlandı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("'Brightness' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'Brightness' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
        }

        /// <summary>
        /// Görseli siyah beyaz hale getirir.
        /// </summary>
        /// <param name="pictureBox">Görselin bulunduğu konteyner.</param>
        /// <param name="image">Üzerinde çalışılan görsel.</param>
        public static void BlackWhite(PictureBox pictureBox, ref Bitmap image)
        {
            try
            {
                Logger.Log("'Black' fonksiyonu çalıştırıldı.");
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

                // History özelliği için gerekli.
                processedImages.Add(pictureBox.Image);
                proceesedNames.Add("Black and White");

                Logger.Log("'BlackWhite' fonksiyonu tamamlandı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("'BlackWhite' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
                Logger.Log("'BlackWhite' fonksiyonu çalışırken hata meydana geldi: " + ex.Message);
            }
        }
    }
}
