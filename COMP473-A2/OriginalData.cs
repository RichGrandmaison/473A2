using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP473_A2
{
    class OriginalData
    {
        public int Rows = 0;
        public int Columns = 0;
        string filePath;


        public OriginalData(string path)
        {
            this.filePath = path;
            this.SetRowsAndColumns();
        }

        public void SetRowsAndColumns()
        {
            string line;
            StreamReader sr = new StreamReader(filePath);
            while ((line = sr.ReadLine()) != null)
            {
                Rows++;
                Columns = (Math.Max(Columns, line.Length));
            }
            sr.Close();
        }

        public Image GenerateOriginalImage()
        {
            Image temp = new Image(this.Rows, this.Columns); //pad
            int row = 0;
            int column = 0;
            StreamReader sr = new StreamReader(filePath);
            while (row < this.Rows)
            {
                char current = (char)sr.Read();

                if (current == 32 || current == 49) //ASCII for 'space' and 1
                {
                    temp.pixelChars[row, column] = (char)current;
                    column++;
                }
                else if (current == 13) //ASCII for '\r'
                {
                    continue;
                }
                else //remaining case is '\n'
                {
                    for (int i = column; i < this.Columns - 2; i++)
                    {
                        temp.pixelChars[row, i] = (char)32;
                    }
                    row++;
                    column = 0;
                }
            }
            sr.Close();
            temp.GenerateIntsFromChars();
            return temp;
        }
    }
}
