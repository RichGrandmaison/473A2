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
        public bool Modified;

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
            copy.Modified = this.Modified;
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

        public void TrimStartingAndEndingZerosInColumns()
        {
            int firstOneColumn = FindFirstColumnWithAOne();
            int lastOneColumn = FindLastColumnWithAOne();
            int firstOneRow = FindFirstRowWithAOne();
            int lastOneRow = FindLastRowWithAOne();
            int columns = lastOneColumn - firstOneColumn + 1;
            int rows = lastOneRow - firstOneRow + 1;
            int[,] tempInt = new int[rows, columns];
            char[,] tempChar = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    tempChar[i, j] = this.pixelChars[i + firstOneRow, j + firstOneColumn];
                    tempInt[i, j] = this.pixelInts[i + firstOneRow, j + firstOneColumn];
                }
            }
            this.pixelInts = tempInt;
            this.pixelChars = tempChar;
        }

        private int FindFirstColumnWithAOne()
        {
            int firstColumnWithAOne = this.pixelChars.GetLength(1);
            for (int i = 0; i < this.pixelChars.GetLength(0); i++)
            {
                for (int j = 0; j < this.pixelChars.GetLength(1); j++)
                {
                    if (this.pixelInts[i, j] == 1)
                    {
                        firstColumnWithAOne = Math.Min(firstColumnWithAOne, j);
                        break;
                    }
                }
            }
            return firstColumnWithAOne;
        }

        private int FindLastColumnWithAOne()
        {
            int lastColumnWithAOne = 0;
            for (int i = 0; i < this.pixelChars.GetLength(0); i++)
            {
                for (int j = this.pixelChars.GetLength(1) - 1; j >= 0; j--)
                {
                    if (this.pixelInts[i, j] == 1)
                    {
                        lastColumnWithAOne = Math.Max(lastColumnWithAOne, j);
                        break;
                    }
                }
            }
            return lastColumnWithAOne;
        }

        private int FindFirstRowWithAOne()
        {
            int firstRowWithAOne = this.pixelChars.GetLength(1);
            for (int j = 0; j < this.pixelChars.GetLength(1); j++)
            {
                for (int i = 0; i < this.pixelChars.GetLength(0); i++)
                {
                    if (this.pixelInts[i, j] == 1)
                    {
                        firstRowWithAOne = Math.Min(firstRowWithAOne, i);
                        break;
                    }
                }
            }
            return firstRowWithAOne;
        }

        private int FindLastRowWithAOne()
        {
            int lastRowWithAOne = 0;
            for (int j = 0; j < this.pixelChars.GetLength(1); j++)
            {
                for (int i = this.pixelChars.GetLength(0) - 1; i >= 0; i--)
                {
                    if (this.pixelInts[i, j] == 1)
                    {
                        lastRowWithAOne = Math.Max(lastRowWithAOne, i);
                        break;
                    }
                }
            }
            return lastRowWithAOne;
        }

        public List<Image> SeperateShapes()
        {
            List<Image> shapes = new List<Image>();
            int rows = this.pixelInts.GetLength(0);
            int firstColums = this.pixelInts.GetLength(1) / 2; ;
            int secondImage = this.pixelInts.GetLength(1) - this.pixelInts.GetLength(1) / 2;
            Image first = new Image(rows, firstColums);
            Image second = new Image(rows, secondImage);
            shapes.Add(first);
            shapes.Add(second);


            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < this.pixelInts.GetLength(1); j++)
                {
                    if(j < firstColums)
                    {
                        first.pixelInts[i, j] = this.pixelInts[i, j];
                        first.pixelChars[i, j] = this.pixelChars[i, j];
                    }
                    else
                    {
                        second.pixelInts[i, j % firstColums] = this.pixelInts[i, j];
                        second.pixelChars[i, j % firstColums] = this.pixelChars[i, j];
                    }
                }
            }
            return shapes;
        }

        public int[] GetCenterOfGravity()
        {
            int[] cog = new int[3];
            int N = 0;
            int sumX = 0;
            int sumY = 0;

            for (int i = 0; i < this.pixelInts.GetLength(0); i++)
            {
                for (int j = 0; j < this.pixelInts.GetLength(1); j++)
                {
                    if(this.pixelInts[i,j] == 1)
                    {
                        N++;
                        sumX += i;
                        sumY += j;
                    }
                }
            }
            cog[0] = sumX / N;
            cog[1] = sumY / N;
            cog[2] = (int)this.pixelChars[cog[0], cog[1]];
            this.pixelChars[cog[0], cog[1]] = 'X';
            return cog;
        }

        public void DisplayCOG(int[] cog)
        {
            System.Console.WriteLine("cog = (" + cog[0] + "," + cog[1] + ")");
            this.Display('c');
            this.pixelChars[cog[0], cog[1]] = (char)cog[2];
        }

        public Image Normalize(Image baseImage)
        {
            int baseX = baseImage.pixelInts.GetLength(0);
            int baseY = baseImage.pixelInts.GetLength(1);
            int originalX = this.pixelInts.GetLength(0);
            int originalY = this.pixelInts.GetLength(1);

            double ratioX = (double)baseX / originalX;
            double ratioY = (double)baseY / originalY;

            Image normalized = new Image((int)Math.Ceiling(originalX * ratioX), (int)Math.Ceiling(originalY * ratioY));
            for(int i = 0; i < originalX; i++)
            {
                for(int j = 0; j < originalY; j++)
                {
                    if(this.pixelInts[i,j] == 1)
                    {
                        int x = (int)Math.Floor(i * ratioX);
                        int y = (int)Math.Floor(j * ratioY);
                        normalized.pixelInts[x,y] = 1;
                    }
                }
            }
            normalized.GenerateCharsFromInts();

            System.Console.WriteLine("Normalization Parameters...");
            System.Console.WriteLine("ratioX: " + ratioX + ", ratioY: " + ratioY);
            return normalized;    
        }

       

    }

 
}
