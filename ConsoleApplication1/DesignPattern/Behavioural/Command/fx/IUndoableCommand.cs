using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command.fx
{
    public interface IUndoableCommand : ICommand
    {
        void Unexecute();
    }
}
