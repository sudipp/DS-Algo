using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Strategy
{
    public class PNGCompressor : ICompressor
    {
        public void Compress(string filename)
        {
            Console.Write("Compressing file using PNG");
        }
    }
}
