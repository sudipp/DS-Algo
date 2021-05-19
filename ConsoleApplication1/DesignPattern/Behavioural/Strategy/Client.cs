using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Strategy
{
    public class Client
    {
        public static void main(string[] args)
        {
            var imgStore = new ImageStore();
            imgStore.Store(fileName: "a", compressor: new JPEGCompressor(), filter: new BWFilter());
            imgStore.Store(fileName: "a", compressor: new PNGCompressor(), filter: new BWFilter());

            var calc = new Calculator();
            calc.Execute(new Add(), 1, 10);
            calc.Execute(new Subtract(), 1, 10);
        }
    }
}
