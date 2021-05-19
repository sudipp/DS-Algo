using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Momento
{
    public class Editor
    {
        private string content;
        public EditorState CreateState()
        {
            return new EditorState(this.content);
        }
        public void Restore(EditorState state)
        {
            this.content = state.GetContent();
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
