using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP473_A2
{
    class Preprocess
    {
        int a, b, c, d, e, f, g, h;
        public enum Process { FillOpposites, FillImmediateNeighbors, Thin, RemoveLonelyOnes, RemoveLonelyInRow, ThinEdges, RemoveLonelyInColumn};
        bool isZS1;

        //removes 1s whose neighbors sum up to less than 2
        public Image RemoveLonelyOnes(Image original)
        {
            original.Modified = false;
            Image copy = original.MakeCopy();
            int rows = original.pixelInts.GetLength(0);
            int columns = original.pixelInts.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (original.pixelInts[i, j] == 1)
                    {
                        SetNeighbors(original, i, j);
                        if(a+b+c+d+e+f+g+h < 2)
                        {
                            copy.pixelInts[i, j] = 0;                  
                        }
                    }
                }
            }
            copy.GenerateCharsFromInts();
            return copy;  
        }

        public Image RemoveLonelyInRow(Image original)
        {
            original.Modified = false;
            Image copy = original.MakeCopy();
            int rows = original.pixelInts.GetLength(0);
            int columns = original.pixelInts.GetLength(1);
            int rowCount = 0;
            int[] location = new int[2];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (original.pixelInts[i, j] == 1)
                    {
                        rowCount++;
                        location[0] = i;
                        location[1] = j;
                    }
                }
                if(rowCount == 1) {
                    copy.pixelInts[location[0], location[1]] = 0;
                    copy.Modified = true;
                }
                rowCount = 0;
                location[0] = 0;
                location[1] = 0;
            }
            copy.GenerateCharsFromInts();
            return copy;
        }

        public Image RemoveLonelyInColumn(Image original)
        {
            original.Modified = false;
            Image copy = original.MakeCopy();
            int rows = original.pixelInts.GetLength(0);
            int columns = original.pixelInts.GetLength(1);
            int columnCount = 0;
            int[] location = new int[2];

            for (int j = 0; j < columns; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    if (original.pixelInts[i, j] == 1)
                    {
                        columnCount++;
                        location[0] = i;
                        location[1] = j;
                    }
                }
                if (columnCount == 1)
                {
                    copy.pixelInts[location[0], location[1]] = 0;
                    copy.Modified = true;
                }
                columnCount = 0;
                location[0] = 0;
                location[1] = 0;
            }
            copy.GenerateCharsFromInts();
            return copy;
        }

        // FillImmidiateNeighbors: x1 = x0 + bg(d+e) + de(b+g)
        // FillOpposites: x1 = x0 + ah + bg + cf + de
        public Image Filling(Image original, Process process)
        {
            original.Modified = false;
            Image copy = original.MakeCopy();
            int rows = original.pixelInts.GetLength(0);
            int columns = original.pixelInts.GetLength(1);

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    if(original.pixelInts[i,j] == 0)
                    {
                        if(process == Process.FillOpposites)
                        {
                            SetNeighbors(original, i, j);
                            if((a * h + b * g + c * f + d * e) > 0)
                            {
                                copy.pixelInts[i, j] = 1;
                                copy.Modified = true;
                            }          
                        }
                        else if(process == Process.FillImmediateNeighbors)
                        {
                            SetNeighbors(original, i, j);
                            if((b * g * (d + e)) + (d * e * (b + g)) > 0)
                            {
                                copy.pixelInts[i, j] = 1;
                                copy.Modified = true;
                            }
                        }        
                    }
                   
                }//end columns for loop
            }//end row for loop
            copy.GenerateCharsFromInts();
            return copy;
        }

        public Image ThinEdges(Image original)
        {
            original.Modified = false;
            Image copy = original.MakeCopy();
            int rows = original.pixelInts.GetLength(0);
            int columns = original.pixelInts.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (original.pixelInts[i, j] == 1)
                    {
                        SetNeighbors(original, i, j);
                        if(d+e>=1 && f+g+h >= 3 && a*b*c*d*e*f*g*h == 0)
                        {
                            copy.pixelInts[i, j] = 0;
                        }
                        if(d+1>=1 && a+b+c >= 3 && a* b*c * d * e * f * g * h == 0)
                        {
                            copy.pixelInts[i, j] = 0;
                        }
                    }
                }
            }
            copy.GenerateCharsFromInts();
            return copy;
        }

        public Image ThinZS(Image original)
        {
            original.Modified = false;
            Image copy = original.MakeCopy();
            int rows = original.pixelInts.GetLength(0);
            int columns = original.pixelInts.GetLength(1);

            isZS1 = true;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (original.pixelInts[i, j] == 1)
                    {
                        SetNeighbors(original, i, j);
                        ZhangSuen(copy, i, j, rows, columns);
                    }
                }
            }
            isZS1 = false;
            Image copy2 = copy.MakeCopy();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (copy.pixelInts[i, j] == 1)
                    {
                        SetNeighbors(copy, i, j);
                        ZhangSuen(copy2, i, j, rows, columns);
                    }
                }
            }
            copy2.GenerateCharsFromInts();
            return copy2;
        }

        void ZhangSuen(Image copy, int i, int j, int rows, int columns)
        {
            int A, B;
            bool edgeCase = (i==0) || (j==0) || (i == rows-1) || (j == columns-1);
            
            A = 0;
            B = a + b + c + d + e + f + g + h;
            if (b == 0 && c == 1)
                A++;
            if (c == 0 && e == 1)
                A++;
            if (e == 0 && h == 1)
                A++;
            if (h == 0 && g == 1)
                A++;
            if (g == 0 && f == 1)
                A++;
            if (f == 0 && d == 1)
                A++;
            if (d == 0 && a == 1)
                A++;
            if (a == 0 && b == 1)
                A++;

            if (isZS1)
            {
                if((2 <= B) && (B<=6) && (A==1) && (b*e*g == 0) && (e*g*d == 0))
                {
                    copy.pixelInts[i, j] = 0;
                    copy.Modified = true;
                }
            }
            else
            {
                if ((2 <= B) && (B <= 6) && (A == 1) && (b*e*d == 0) && (b*g*d == 0))
                {
                    copy.pixelInts[i, j] = 0;
                    copy.Modified = true;
                }
            }
        }//end ZhangSuen

        void SetNeighbors(Image original, int i, int j)
        {
            int rows = original.pixelInts.GetLength(0);
            int columns = original.pixelInts.GetLength(1);

            a = (i == 0 || j == 0) ? 0 : original.pixelInts[i-1, j-1]; //top left
            b = (i == 0) ? 0 : original.pixelInts[i - 1,j]; //top middle
            c = (i == 0 || j >= columns - 1) ? 0 : original.pixelInts[i - 1, j + 1]; //top right
            d = (j == 0) ? 0 : original.pixelInts[i, j - 1]; //middle left
            e = (j >= columns - 1) ? 0 : original.pixelInts[i, j + 1]; //middle right
            f = (i >= rows - 1 || j == 0) ? 0 : original.pixelInts[i + 1, j - 1]; //bottom left
            g = (i >= rows - 1) ? 0 : original.pixelInts[i + 1, j]; //bottom middle
            h = (i >= rows - 1 || j >= columns - 1) ? 0 : original.pixelInts[i + 1, j + 1]; //bottom right
        }

        public Image LoopProcess(Image original, Process process)
        {
            Image copy = original.MakeCopy();
            do
            {
                if(process == Process.RemoveLonelyOnes)
                {
                    copy = RemoveLonelyOnes(copy);
                    System.Console.WriteLine();
                    copy.Display('c');
                } else if(process == Process.FillOpposites)
                {
                    copy = Filling(copy, Process.FillOpposites);
                    System.Console.WriteLine();
                    copy.Display('c');
                } else if(process == Process.FillImmediateNeighbors)
                {
                    copy = Filling(copy, Process.FillImmediateNeighbors);
                    System.Console.WriteLine();
                    copy.Display('c');
                } else if(process == Process.Thin)
                {
                    copy = ThinZS(copy);
                    System.Console.WriteLine();
                    copy.Display('c');
                } else if(process == Process.RemoveLonelyInRow)
                {
                    copy = RemoveLonelyInRow(copy);
                    System.Console.WriteLine();
                    copy.Display('c');
                } else if (process == Process.ThinEdges)
                {
                    copy = ThinEdges(copy);
                    System.Console.WriteLine();
                    copy.Display('c');
                }
                else if (process == Process.RemoveLonelyInColumn)
                {
                    copy = RemoveLonelyInColumn(copy);
                    System.Console.WriteLine();
                    copy.Display('c');
                }
                System.Console.ReadKey();
            } while (copy.Modified);
            System.Console.WriteLine();
            System.Console.WriteLine("**** END OF " + process + " ****");
            return copy;
        }
    }
}
