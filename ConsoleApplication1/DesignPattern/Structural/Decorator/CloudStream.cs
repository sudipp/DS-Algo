using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Decorator
{
    public class CloudStream : IStream
    {
        public void Write(string data)
        {
            Console.WriteLine("Writing " + data);
        }
    }
}
