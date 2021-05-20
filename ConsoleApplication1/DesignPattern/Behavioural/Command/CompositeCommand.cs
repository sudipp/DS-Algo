using ConsoleApplication1.DesignPattern.Behavioural.Command.fx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command
{
    public class CompositeCommand : ICommand
    {
        IList<ICommand> commands = new List<ICommand>();
        public void AddCommand(ICommand command)
        {
            this.commands.Add(command);
        }
        public void Execute()
        {
            foreach (ICommand command in commands)
                command.Execute();
        }
    }
}
