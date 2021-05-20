using ConsoleApplication1.DesignPattern.Behavioural.Command.fx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command
{
    public class History
    {
        Stack<IUndoableCommand> commands = new Stack<IUndoableCommand>();
        public void Push(IUndoableCommand command)
        {
            commands.Push(command);
        }
        public IUndoableCommand Pop()
        {
            return commands.Pop();
        }
        public int Size()
        {
            return commands.Count;
        }
    }
}
