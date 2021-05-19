using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Strategy
{
    public class Calculator
    {
        public int Execute(IOperation operation, int x, int y)
        {
            return operation.doOpeation(x, y);
        }
    }
}
