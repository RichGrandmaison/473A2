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
            Image image = od.GenerateOriginalImage();
            image.TrimStartingAndEndingZerosInColumns();
            List<Image> shapes = image.SeperateShapes();
            Image eight = shapes[0];
            Image nine = shapes[1];

            Preprocess pp = new Preprocess();

            eight = ProcessImage(eight, pp);
            nine = ProcessImage(nine, pp);

            System.Console.WriteLine();
            Image normEight = eight.Normalize(nine);

            normEight.DisplayCOG(normEight.GetCenterOfGravity());
            System.Console.WriteLine();
            nine.DisplayCOG(nine.GetCenterOfGravity());


            System.Console.ReadKey();
        }

        private static Image ProcessImage(Image toProcess, Preprocess pp)
        {
            toProcess = pp.LoopProcess(toProcess, Preprocess.Process.FillOpposites);
            toProcess = pp.LoopProcess(toProcess, Preprocess.Process.Thin);
            toProcess = pp.LoopProcess(toProcess, Preprocess.Process.RemoveLonelyOnes);
            toProcess = pp.LoopProcess(toProcess, Preprocess.Process.RemoveLonelyInColumn);
            toProcess = pp.LoopProcess(toProcess, Preprocess.Process.RemoveLonelyOnes);
            toProcess.TrimStartingAndEndingZerosInColumns();
            toProcess.DisplayCOG(toProcess.GetCenterOfGravity());
            toProcess.Display('c');

            return toProcess;
        }
    }
}
