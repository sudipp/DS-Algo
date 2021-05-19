using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Strategy
{
    public class BWFilter : IFilter
    {
        public void Apply(string filename)
        {
            Console.Write("Apply B&W filter");
        }
    }
}
