using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command
{
    public class BoldCommand : fx.IUndoableCommand
    {
        History History;
        Document document;
        private string previousContent;

        public BoldCommand(History history, Document document)
        {
            this.History = history;
            this.document = document;
        }

        public void Execute()
        {
            this.History.Push(this);
            previousContent = this.document.GetContent();
            this.document.MakeBold();
        }
        public void Unexecute()
        {
            this.document.SetContent(previousContent);
        }
    }
}
