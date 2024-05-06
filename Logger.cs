using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_processing_form
{
    public class Logger
    {
        private static string logFolderPath;

        public Logger()
        {
            // Klasör yolunu belirtin
            logFolderPath = Path.Combine(Environment.CurrentDirectory, "ImgProcessLogs");
            // Eğer klasör yoksa oluşturun
            Directory.CreateDirectory(logFolderPath);
        }

        public static void Log(string message)
        {
            // Şu anki tarihi alın ve formatlayın
            string currentDate = DateTime.Now.ToString("dd_MM_yyyy");
            // Log dosyasının tam yolu
            string logFilePath = Path.Combine(logFolderPath, currentDate + ".log");

            
                // Log dosyasına mesajı ekle
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {message}");
                }
            
            
        }
    }
}
