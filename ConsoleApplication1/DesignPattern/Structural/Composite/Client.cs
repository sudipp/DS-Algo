using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Composite
{
    public class Client
    {
        public static void main(string[] args)
        {
            var group1 = new Group();
            group1.AddShape(new Shape()); //Square
            group1.AddShape(new Shape()); //Square

            var group2 = new Group();
            group1.AddShape(new Shape()); //circle
            group1.AddShape(new Shape()); //circle

            var group = new Group();
            group.AddShape(group1);
            group.AddShape(group2);
            group.Render();
        }
    }
}
