using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command
{
    public class Client
    {
        public static void main(string[] args)
        {
            CustomerService service = new CustomerService();
            AddCustomerCommand command = new AddCustomerCommand(service);
            fx.Button button = new fx.Button(command);
            button.Click();

            CompositeCommand compCommand = new CompositeCommand();
            compCommand.AddCommand(new ImageResizeCommand());
            compCommand.AddCommand(new BWFilterCommand());
            compCommand.Execute();

            var doc = new Document();
            var history = new History(); 
            doc.SetContent("Hello World");
            BoldCommand boldCommand = new BoldCommand(history, doc);
            boldCommand.Execute();
            Console.WriteLine(doc.GetContent());

            //boldCommand.Unexecute();
            UndoCommand undoCommand = new UndoCommand(history);
            undoCommand.Execute();
            Console.WriteLine(doc.GetContent());
        }
    }
}
