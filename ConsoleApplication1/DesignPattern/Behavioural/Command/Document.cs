using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command
{
    public class Document
    {
        private string content;
        public void MakeBold()
        {
            content = "<b>" + content +"</b>";
        }
        public string GetContent()
        {
            return this.content;
        }
        public void SetContent(string content)
        {
            this.content = content;
        }
    }
}
