using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP473_A2
{
    class Image
    {
        public char[,] pixels;

        public Image(int rows, int columns)
        {
            this.pixels = new char[rows, columns];
        }

        public void Display()
        {
            for(int i = 0; i < this.pixels.GetLength(0); i++)
            {
                for(int j = 0; j < this.pixels.GetLength(1); j++)
                {
                    Console.Write(pixels[i, j]);
                    if (j == this.pixels.GetLength(1) - 1)
                        Console.WriteLine();

                }
            }
        }
    }

 
}
