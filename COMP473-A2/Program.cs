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
            Image result = pp.ApplyPreprocessingXTimes(original, 0, Preprocessor.Type.FillImmidiateNeighbors);
            result = pp.ApplyPreprocessingXTimes(result, 10, Preprocessor.Type.FillOpposites);
            result = pp.ApplyPreprocessingXTimes(result, 10, Preprocessor.Type.FillImmidiateNeighbors);
            result = pp.ApplyPreprocessingXTimes(result, 1, Preprocessor.Type.Thin);

            Console.ReadKey();

        }
    }
}
