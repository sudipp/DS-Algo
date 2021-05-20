using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Visitor
{
    public class HTMLDocument
    {
        IList<IHTMLNode> nodes = new List<IHTMLNode>();
        public void Add(IHTMLNode node)
        {
            nodes.Add(node);
        }

        public void Execute(IOperation operation)
        {
            foreach(IHTMLNode node in nodes)
            {
                node.Execute(op: operation);
            }
        }
    }
}
