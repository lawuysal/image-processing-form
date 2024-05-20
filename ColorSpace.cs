using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_processing_form
{
    internal class ColorSpace
    {
        public static void RgbToCmyk(int red, int green, int blue, out double cyan, out double magenta, out double yellow, out double black)
        {
            double r = (double)red / 255;
            double g = (double)green / 255;
            double b = (double)blue / 255;

            black = 1 - Math.Max(r, Math.Max(g, b));

            if (black == 1)
            {
                cyan = magenta = yellow = 0;
            }
            else
            {
                cyan = (1 - r - black) / (1 - black);
                magenta = (1 - g - black) / (1 - black);
                yellow = (1 - b - black) / (1 - black);
            }
        }


        
        public static void RgbToHsi(double red, double green, double blue, out double hue, out double saturation, out double intensity)
        {
            
            double r = red / 255.0;
            double g = green / 255.0;
            double b = blue / 255.0;

            
            intensity = (r + g + b) / 3.0;

           
            if (intensity == 0)
            {
                
                saturation = 0;
                hue = 0;
                return;
            }

           
            double min = Math.Min(r, Math.Min(g, b));
            saturation = 1 - (min / intensity);

            
            saturation = Math.Max(0, Math.Min(1, saturation));

            
            double numerator = 0.5 * ((r - g) + (r - b));
            double denominator = Math.Sqrt((r - g) * (r - g) + (r - b) * (g - b));
            double theta = Math.Acos(numerator / denominator);

           
            if (b <= g)
            {
                hue = theta * (180.0 / Math.PI);
            }
            else
            {
                hue = (2 * Math.PI - theta) * (180.0 / Math.PI);
            }
        }




       
        public static void RgbToYcc(double red, double green, double blue, out double y, out double cb, out double cr)
        {
            y = 0.299 * red + 0.587 * green + 0.114 * blue;
            cb = 128 - 0.169 * red - 0.331264 * green + 0.5 * blue;
            cr = 128 + 0.5 * red - 0.419 * green - 0.081 * blue;
        }

        

        public static void RgbToYes(double red, double green, double blue, out double y, out double e, out double s)
        {
            
            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));

           
            y = 0.299 * red + 0.587 * green + 0.114 * blue;
            e = 0.5 * red - 0.5 * green;
            s = -0.25 * red - 0.25 * green + 0.5 * blue;

            
            y = Math.Max(0, Math.Min(255, y));
            e = Math.Max(0, Math.Min(255, e));
            s = Math.Max(0, Math.Min(255, s));
        }


        public static Color ColorFromHSI(double hue, double intensity, double saturation)
        {
            double r = 0, g = 0, b = 0;
            double h = hue * Math.PI / 180.0; 

            if (hue >= 0 && hue < 120)
            {
                b = intensity * (1 - saturation);
                r = intensity * (1 + saturation * Math.Cos(h) / Math.Cos(Math.PI / 3.0 - h));
                g = 3 * intensity * (1 - ((r + b) / (3 * intensity)));

            }
            else if (hue >= 120 && hue < 240)
            {
                h -= 2 * Math.PI / 3;
                r = intensity * (1 - saturation);
                g = intensity * (1 + saturation * Math.Cos(h) / Math.Cos(Math.PI / 3.0 - h));
                b = 3 * intensity * (1 - ((r + g) / (3 * intensity)));
            }
            else if (hue >= 240 && hue < 360)
            {
                h -= 4 * Math.PI / 3;
                g = intensity * (1 - saturation);
                b = intensity * (1 + saturation * Math.Cos(h) / Math.Cos(Math.PI / 3.0 - h));
                r = 3 * intensity * (1 - ((b + g) / (3 * intensity)));
            }

         
            r = Math.Max(0, Math.Min(255, r * 255));
            g = Math.Max(0, Math.Min(255, g * 255));
            b = Math.Max(0, Math.Min(255, b * 255));

           
            return Color.FromArgb((int)r, (int)g, (int)b);
        }



    }
}
