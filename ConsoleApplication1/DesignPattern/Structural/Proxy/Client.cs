using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Proxy
{
    public class Client
    {
        public static void main(string[] args)
        {
            Library lib = new Library();
            string[] fileNames = new string[] { "a", "b", "c" };
            foreach (var filename in fileNames)
                lib.Add(new RealEBook(fileName: filename));
                //lib.Add(new eBookProxy(fileName: filename));

            lib.OpenEBook("a");
        }
    }
}
