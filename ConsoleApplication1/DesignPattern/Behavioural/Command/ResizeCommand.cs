using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command.fx
{
    public class ResizeCommand : IUndoableCommand
    {
        string previousImageSize;
        public void Execute()
        {
            previousImageSize = "Current image Size";
            Console.WriteLine("Resizing Image");
        }
        public void Unexecute()
        {
            Console.WriteLine("Undoing Resize");
        }
    }
}
