using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Momento
{
    public class EditorState
    {
        private readonly string content;
        public EditorState(string content)
        {
            this.content = content;
        }
        public string GetContent()
        {
            return this.content;
        }
    }
}
