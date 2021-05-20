using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Visitor
{
    public class Client
    {
        static void Main1(string[] args)
        {
            HTMLDocument htmDoc = new HTMLDocument();
            htmDoc.Add(new HeadingNode());
            htmDoc.Add(new AnchorNode());

            htmDoc.Execute(new HighlightOperation());
            htmDoc.Execute(new PlainTextOperation());
        }
    }
}
