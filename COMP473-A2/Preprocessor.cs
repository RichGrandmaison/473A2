using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP473_A2
{
    class Preprocessor
    {

        public enum Type { FillImmidiateNeighbors, FillOpposites, Thin };

        //if x0 = 0, x1 = bg(d+e) + de(b+g)
        public void FillingImmidiateNeighbors(Image original, Image copy)
        {
            int b, d, e, g;
            int rowSize = original.pixelInts.GetLength(0);
            int columnSize = original.pixelInts.GetLength(1);

            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    if(original.pixelInts[i,j] == 0)
                    {
                        if ((i != 0 && i != rowSize - 1) && (j != 0 && j != columnSize - 1)) //normal non-edge case
                        {
                            copy.pixelInts[i, j] = this.SetMaskValuesFIN(i, j, original);
                        } else if (i == 0)
                        {
                            g = original.pixelInts[i + 1, j];
                            MakeRowEdgeCaseDecisionFIN(original, copy, out d, out e, g, columnSize, i, j);
                        } else if (i == rowSize - 1)
                        {
                            g = original.pixelInts[i - 1, j];
                            MakeRowEdgeCaseDecisionFIN(original, copy, out d, out e, g, columnSize, i, j);
                        } else if (j == 0)
                        {
                            b = original.pixelInts[i - 1, j];
                            g = original.pixelInts[i + 1, j];
                            e = original.pixelInts[i, j + 1];
                            copy.pixelInts[i, j] = (b * g * e) > 0 ? 1 : 0;
                        } else if (j == columnSize - 1)
                        {
                            b = original.pixelInts[i - 1, j];
                            g = original.pixelInts[i + 1, j];
                            d = original.pixelInts[i, j - 1];
                            copy.pixelInts[i, j] = (b * g * d) > 0 ? 1 : 0;
                        }
                    }
                }
            }
            copy.GenerateCharsFromInts();
        }

        private static void MakeRowEdgeCaseDecisionFIN(Image original, Image copy, out int d, out int e, int g, int columnSize, int i, int j)
        {
            e = d = 0;
            if (j == 0)
            {
                d = 0;
            }
            else if (j == columnSize - 1)
            {
                e = 0;
            }
            else
            {
                d = original.pixelInts[i, j - 1];
                e = original.pixelInts[i, j + 1];
            }
            copy.pixelInts[i, j] = (d * e * g) > 0 ? 1 : 0;
        }

        private int SetMaskValuesFIN(int i, int j, Image original)
        {
            int b = original.pixelInts[i - 1, j];
            int g = original.pixelInts[i + 1, j];
            int d = original.pixelInts[i, j - 1];
            int e = original.pixelInts[i, j + 1];

            int decision = (b * g) * (d + e) + (b + g) * (d * e);
            return (decision > 0) ? 1 : 0;
        }

        //if x0 = 0, x1 = ah + fc + gb + de
        public void FillingOpposites(Image original, Image copy)
        {
            int a, b, c, d, e, f, g, h;
            int rowSize = original.pixelInts.GetLength(0);
            int columnSize = original.pixelInts.GetLength(1);

            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    if (original.pixelInts[i, j] == 0) //if current == 1, do nothing
                    {
                        if ((i != 0 && i != rowSize - 1) && (j != 0 && j != columnSize - 1)) //normal non-edge case
                        {
                            copy.pixelInts[i, j] = this.SetMaskValuesFO(i, j, original);
                        }
                        else if (i == 0) //top row
                        {
                            g = original.pixelInts[i + 1, j];
                            a = b = c = d = e = f = h = 0;
                            if(j != 0)
                            {
                                d = original.pixelInts[i, j - 1];
                                f = original.pixelInts[i + 1, j - 1];
                            }
                            if (j != columnSize - 1)
                            {
                                e = original.pixelInts[i, j + 1];
                                h = original.pixelInts[i + 1, j + 1];
                            }
                            copy.pixelInts[i, j] = (d * e) > 0 ? 1 : 0;
                        }
                        else if (i == rowSize - 1) //bottom row
                        {
                            b = original.pixelInts[i - 1, j];
                            a = c = d = e = f = g = h = 0;
                            if (j != 0)
                            {
                                d = original.pixelInts[i, j - 1];
                                a = original.pixelInts[i - 1, j - 1];
                            }
                            if (j != columnSize - 1)
                            {
                                e = original.pixelInts[i, j + 1];
                                c = original.pixelInts[i - 1, j + 1];
                            }
                            copy.pixelInts[i, j] = (d * e) > 0 ? 1 : 0;
                        }
                        else if (j == 0 || j == columnSize - 1) //left column or right column
                        {
                            b = original.pixelInts[i - 1, j];
                            g = original.pixelInts[i + 1, j];

                            copy.pixelInts[i, j] = (b * g) > 0 ? 1 : 0;
                        }
                    }
                }
            }
            copy.GenerateCharsFromInts();
        }

        private int SetMaskValuesFO(int i, int j, Image original)
        {
            int a = original.pixelInts[i - 1, j - 1];
            int b = original.pixelInts[i - 1, j];
            int c = original.pixelInts[i - 1, j + 1];
            int d = original.pixelInts[i, j - 1];
            int e = original.pixelInts[i, j + 1];
            int f = original.pixelInts[i + 1, j - 1];
            int g = original.pixelInts[i + 1, j];
            int h = original.pixelInts[i + 1, j + 1];

            int decision = a * h + f * c + b * g + d * e;
            return (decision > 0) ? 1 : 0;
        }

        // if x0 = 1, then x1 = (a+b+d)*(g+h+e)+(b+c+e)*(d+f+g)
        public void Thinning(Image original, Image copy)
        {
            int a, b, c, d, e, f, g, h;
            int rowSize = original.pixelInts.GetLength(0);
            int columnSize = original.pixelInts.GetLength(1);

            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    if(original.pixelInts[i, j] == 1) //if current == 0, do nothing
                    {
                        if ((i != 0 && i != rowSize - 1) && (j != 0 && j != columnSize - 1)) //normal non-edge case
                        {
                            a = original.pixelInts[i - 1, j - 1];
                            b = original.pixelInts[i - 1, j];
                            c = original.pixelInts[i - 1, j + 1];
                            d = original.pixelInts[i, j - 1];
                            e = original.pixelInts[i, j + 1];
                            f = original.pixelInts[i + 1, j - 1];
                            g = original.pixelInts[i + 1, j];
                            h = original.pixelInts[i + 1, j + 1];

                            copy.pixelInts[i, j] = (a+b+d)*(e+g+h)+(b+c+e)*(d+f+g) > 0 ? 1 : 0;
                        }
                        else if (i == 0) //top row
                        {
                            g = original.pixelInts[i + 1, j];
                            a = b = c = d = e = f = h = 0;
                            if (j != 0)
                            {
                                d = original.pixelInts[i, j - 1];
                                f = original.pixelInts[i + 1, j - 1];
                            }
                            if (j != columnSize - 1)
                            {
                                e = original.pixelInts[i, j + 1];
                                h = original.pixelInts[i + 1, j + 1];
                            }
                            copy.pixelInts[i, j] = (d+f+g)*(e)+(g+h+e)*(d) > 0 ? 1 : 0;
                        }
                        else if (i == rowSize - 1) //bottom row
                        {
                            b = original.pixelInts[i - 1, j];
                            a = c = d = e = f = g = h = 0;
                            if (j != 0)
                            {
                                d = original.pixelInts[i, j - 1];
                                a = original.pixelInts[i - 1, j - 1];
                            }
                            if (j != columnSize - 1)
                            {
                                e = original.pixelInts[i, j + 1];
                                c = original.pixelInts[i - 1, j + 1];
                            }
                            copy.pixelInts[i, j] = (a + b + d) * (e) + (c + b + e) * (d) > 0 ? 1 : 0;
                        }
                        else if (j == 0) //left column
                        {
                            b = original.pixelInts[i - 1, j];
                            g = original.pixelInts[i + 1, j];
                            c = original.pixelInts[i - 1, j + 1];
                            e = original.pixelInts[i, j + 1];
                            h = original.pixelInts[i + 1, j + 1];

                            copy.pixelInts[i, j] = (b+c+e)*(g)+(g+h+e)*(b) > 0 ? 1 : 0;
                        }
                        else if (j == columnSize - 1) //left column
                        {
                            b = original.pixelInts[i - 1, j];
                            g = original.pixelInts[i + 1, j];
                            a = original.pixelInts[i - 1, j - 1];
                            d = original.pixelInts[i, j - 1];
                            f = original.pixelInts[i + 1, j - 1];

                            copy.pixelInts[i, j] = (b + a + d) * (g) + (g + f + g) * (b) > 0 ? 1 : 0;
                        }          
                    }

                }
            }
            copy.GenerateCharsFromInts();
        }

        public Image ApplyPreprocessingXTimes(Image original, int times, Type processType)
        {
            Image copy = original.MakeCopy();
            if (processType == Type.FillImmidiateNeighbors)
                this.FillingImmidiateNeighbors(original, copy);
            else if (processType == Type.FillOpposites)
                this.FillingOpposites(original, copy);
            else
                this.Thinning(original, copy);
            if (times == 0)
            {
                return copy;
            }
            else
            {
                copy.Display('c');
                System.Console.WriteLine();
                return this.ApplyPreprocessingXTimes(copy, times - 1, processType);
            }
        }
    }
     

}
