using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Visitor
{
    public interface IOperation
    {
        void Apply(HeadingNode headningNode);
        void Apply(AnchorNode anchorNode);
    }
}
