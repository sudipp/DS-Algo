using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.State
{
    public class EraserTool : ITools
    {
        public void MouseDown()
        {
            Console.WriteLine("Eraser icon");
        }
        public void MouseUp()
        {
            Console.WriteLine("Erase content");
        }
    }
}
