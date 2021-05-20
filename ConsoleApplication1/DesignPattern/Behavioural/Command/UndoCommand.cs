using ConsoleApplication1.DesignPattern.Behavioural.Command.fx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command
{
    public class UndoCommand : ICommand
    {
        private History history;
        public UndoCommand(History history)
        {
            this.history = history;
        }
        public void Execute()
        {
            if(this.history.Size() > 0)
                this.history.Pop().Unexecute();
        }
    }
}
