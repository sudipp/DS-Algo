using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Visitor
{
    public class HighlightOperation : IOperation
    {
        public void Apply(HeadingNode headningNode)
        {
            Console.WriteLine("Apply highlight text on Heading Node");
        }

        public void Apply(AnchorNode anchorNode)
        {
            Console.WriteLine("Apply highlight text on Anchor Node");
        }
    }
}
