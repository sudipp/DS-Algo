using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Visitor
{
    public class PlainTextOperation : IOperation
    {
        public void Apply(HeadingNode headningNode)
        {
            Console.WriteLine("Extract Plain Text from Heading Node");
        }

        public void Apply(AnchorNode anchorNode)
        {
            Console.WriteLine("Extract Plain Text from Anchor Node");
        }
    }
}
