using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP473_A2b
{
    class Program
    {
        static void Main(string[] args)
        {
            string coinPath = "F:\\workspace\\COMP473-A2\\COMP473-A2b\\Resources\\coin2.jpg";
            string stampPath = "F:\\workspace\\COMP473-A2\\COMP473-A2b\\Resources\\stamp2.jpg";

            Binarize b = new Binarize();
            Process p = new Process();
            b.SetImage(coinPath);
            b.ToBlackAndWhite();
            Bitmap coin1 = p.LoopProcess(b.image, Process.Type.FillImmediateNeighbors, 15);
            coin1 = p.LoopProcess(coin1, Process.Type.FillOpposites, 15);
            Bitmap coinContour = p.GetContour(coin1);
            coinContour.Save("contourCoin.bmp");

            b.SetImage(stampPath);
            b.ToBlackAndWhite();
            Bitmap stamp1 = p.LoopProcess(b.image, Process.Type.FillImmediateNeighbors, 15);
            stamp1 = p.LoopProcess(stamp1, Process.Type.FillOpposites, 15);
            Bitmap stampContour = p.GetContour(stamp1);
            stampContour.Save("contourStamp.bmp");
        }
    }
}
