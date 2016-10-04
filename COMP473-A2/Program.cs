using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP473_A2
{
    class Program
    {

        static void Main(string[] args)
        {
            string filePath = "F:\\workspace\\COMP473-A2\\data.txt";
            OriginalData od = new OriginalData(filePath);
            Image original = od.GenerateOriginalImage();
            Preprocessor pp = new Preprocessor();
            Image result = pp.ApplyPreprocessingXTimes(original, 1, Preprocessor.Type.FillOpposites);
            Image result2 = pp.ApplyPreprocessingXTimes(result, 1, Preprocessor.Type.FillImmidiateNeighbors);
            Image result3 = pp.ApplyPreprocessingXTimes(result2, 15, Preprocessor.Type.Thin);

            Console.ReadKey();

        }
    }
}
