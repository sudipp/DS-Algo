using ConsoleApplication1.DesignPattern.Behavioural.Command.fx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command
{
    public class ImageResizeCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Resizing Image");
        }
    }
}
