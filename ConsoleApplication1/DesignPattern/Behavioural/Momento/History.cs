using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Momento
{
    public class History
    {
        Stack<EditorState> states = new Stack<EditorState>();
        public void Push(EditorState state)
        {
            states.Push(state);
        }
        public EditorState Pop()
        {
            return states.Pop();
        }
    }
}
