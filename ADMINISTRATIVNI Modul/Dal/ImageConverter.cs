using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADMINISTRATIVNI_Modul.Dal
{
    public class ImageConverter
    {
        public static string ImageToBase64(System.Drawing.Image image)
        {
            string base64String = string.Empty;
            try
            {
                Bitmap bitmap = new Bitmap(image);
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    // Convert Image to byte array
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    imageBytes = ms.ToArray();                                     
                }
                // Convert byte array to Base64 string
                base64String = Convert.ToBase64String(imageBytes);
            }
            catch (Exception)
            {
                // oh well
            }
            return base64String;
        }

        public static Bitmap Base64ToImage(string imagestring) 
        {
            Bitmap bitmap = null;
            try
            {
                byte[] imageBytes = Convert.FromBase64String(imagestring);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    bitmap = new Bitmap(ms);
                }
            }
            catch (Exception)
            {
                //eh
            }
            return bitmap;
        }

    }
}
