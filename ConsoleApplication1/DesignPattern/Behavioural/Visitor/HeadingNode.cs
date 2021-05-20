using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Visitor
{
    public class HeadingNode : IHTMLNode
    {
        public void Execute(IOperation op)
        {
            op.Apply(this);
        }
    }
}
