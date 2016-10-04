using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP473_A2
{
    class Image
    {
        public char[,] pixelChars;
        public int[,] pixelInts;

        public Image(int rows, int columns)
        {
            this.pixelChars = new char[rows, columns];
            this.pixelInts = new int[rows, columns];
        }

        public void Display(char c)
        {
            if(c == 'c')
            {
                for (int i = 0; i < this.pixelChars.GetLength(0); i++)
                {
                    for (int j = 0; j < this.pixelChars.GetLength(1); j++)
                    {
                        Console.Write(pixelChars[i, j]);
                        if (j == this.pixelChars.GetLength(1) - 1)
                            Console.WriteLine();

                    }
                }
            } else if(c == 'i')
            {
                for (int i = 0; i < this.pixelInts.GetLength(0); i++)
                {
                    for (int j = 0; j < this.pixelInts.GetLength(1); j++)
                    {
                        Console.Write(pixelInts[i, j]);
                        if (j == this.pixelInts.GetLength(1) - 1)
                            Console.WriteLine();
                    }
                }
            }
        }

        public void GenerateIntsFromChars()
        {
            for (int i = 0; i < this.pixelChars.GetLength(0); i++)
            {
                for (int j = 0; j < this.pixelChars.GetLength(1); j++)
                {
                    if (pixelChars[i, j] == '1')
                        pixelInts[i, j] = 1;
                    else pixelInts[i, j] = 0;
                }
            }
        }

        public void GenerateCharsFromInts()
        {
            for (int i = 0; i < this.pixelInts.GetLength(0); i++)
            {
                for (int j = 0; j < this.pixelInts.GetLength(1); j++)
                {
                    if (pixelInts[i, j] == 1)
                        pixelChars[i, j] = '1';
                    else pixelChars[i, j] = ' ';
                }
            }
        }

        public Image MakeCopy()
        {
            Image copy = new Image(pixelChars.GetLength(0), pixelChars.GetLength(1));
            for (int i = 0; i < this.pixelChars.GetLength(0); i++)
            {
                for (int j = 0; j < this.pixelChars.GetLength(1); j++)
                {
                    copy.pixelChars[i, j] = this.pixelChars[i, j];
                    copy.pixelInts[i, j] = this.pixelInts[i, j];
                }
            }
            return copy;
        }
    }

 
}
