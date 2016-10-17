using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP473_A2b
{
    public class Process
    {
        public enum Type { FillOpposites, FillImmediateNeighbors };
        public int a, b, c, d, e, f, g, h;
        bool Modified = false;
        int count = 0;

        public void SetNeighbors(Bitmap image, int i, int j) {
            a = b = c = d = e = f = g = h = 0;
            int rows = image.Height;
            int columns = image.Width;

            a = (i == 0 || j == 0) ? 0 : (int)image.GetPixel(i - 1, j - 1).R; ; //top left
            b = (j == 0) ? 0 : (int)image.GetPixel(i, j).R; ; //top middle
            c = (j == 0 || i >= columns - 1) ? 0 : (int)image.GetPixel(i + 1, j - 1).R; ; //top right
            d = (i == 0) ? 0 : (int)image.GetPixel(i - 1, j).R; ; //middle left
            e = (i >= columns - 1) ? 0 : (int)image.GetPixel(i + 1 , j).R; ; //middle right
            f = (j >= rows - 1 || i == 0) ? 0 : (int)image.GetPixel(i - 1, j + 1).R; ; //bottom left
            g = (j >= rows - 1) ? 0 : (int)image.GetPixel(i, j + 1).R; ; //bottom middle
            h = (j >= rows - 1 || i >= columns - 1) ? 0 : (int)image.GetPixel(i + 1, j + 1).R; ; //bottom right
        }

    
        public Bitmap FillOpposites(Bitmap image)
        {
            Modified = false;
            Bitmap copy = new Bitmap(image);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    SetNeighbors(image, i, j);
                    if((int)image.GetPixel(i,j).R == 255)
                    {
                        if(a+h == 0 || b+g == 0 || d+e == 0 || c+f == 0)
                        {
                            copy.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                            Modified = true;
                        }
                    }
                }
            }
            return copy;
        }

        public Bitmap FillImmidiateNeighbors(Bitmap image)
        {
            Modified = false;
            Bitmap copy = new Bitmap(image);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    SetNeighbors(image, i, j);
                    if ((int)image.GetPixel(i, j).R == 255)
                    {
                        if (d+b+g+e <= 255) //if 3 neighbors are black
                        {
                            copy.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                            Modified = true;
                        }
                    }
                }
            }
            return copy;
        }

        public Bitmap LoopProcess(Bitmap image, Type type, int times) 
        {
            Bitmap copy = new Bitmap(image);
            do
            {
                System.Console.WriteLine(count++);
                if(type == Type.FillImmediateNeighbors)
                {
                    copy = FillImmidiateNeighbors(copy);
                } else if (type == Type.FillOpposites)
                {
                    copy = FillOpposites(copy);
                }
                times--;
            } while (times > 0);

            copy.Save("processed.bmp");
            return copy;
        }

        public Bitmap GetContour(Bitmap image)
        {
            Bitmap copy = new Bitmap(image);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    SetNeighbors(image, i, j);
                    if ((int)image.GetPixel(i, j).R == 0)
                    {
                        if(a+b+c+d+e+f+g+h == 0)
                        {
                            copy.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                    }
                }
            }

            return copy;
        }

    }
}
