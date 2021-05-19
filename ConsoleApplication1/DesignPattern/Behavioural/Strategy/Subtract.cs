using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Strategy
{
    public class Subtract : IOperation
    {
        public int doOpeation(int i, int j)
        {
            return i - j;
        }
    }
}
