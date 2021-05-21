using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Composite
{
    public class Shape : IComponent
    {
        public void Render()
        {
            Console.WriteLine("Render shape");
        }
        public void Resize()
        {
            Console.WriteLine("Resize shape");
        }
    }
}
