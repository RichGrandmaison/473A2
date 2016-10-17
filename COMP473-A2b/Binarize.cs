using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP473_A2b
{
    class Binarize
    {
        public Bitmap image;
        int counter = 0;

        public void SetImage(string imagePath)
        {
            Image rawImage = Image.FromFile(imagePath);
            image = new Bitmap(rawImage);
        }

        //First, average out the RGB of the pixel, and set R = B = G = average(R,B,G)
        //Second, set a threshold to decide if a pixel will be black or white
        public void ToBlackAndWhite()
        {
            for(int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color color = image.GetPixel(i, j);
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;
                    int gray = (int)(r * 0.2989 + g*0.5870 + b*0.1140);
                    int THRESHOLD = 128;
                    int blackOrWhite = (gray > THRESHOLD) ? 255 : 0;

                    image.SetPixel(i, j, Color.FromArgb(blackOrWhite, blackOrWhite, blackOrWhite));
                }
            }
            counter++;
            image.Save("blackAndWhite" + counter +".bmp");
        }
 
    }
}
