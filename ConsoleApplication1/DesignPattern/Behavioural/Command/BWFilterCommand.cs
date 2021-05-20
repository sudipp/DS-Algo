using ConsoleApplication1.DesignPattern.Behavioural.Command.fx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command
{
    public class BWFilterCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Applying B&W filter");
        }
    }
}
