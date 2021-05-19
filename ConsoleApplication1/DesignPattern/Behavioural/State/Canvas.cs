using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.State
{
    public class Canvas
    {
        private ITools currentTool; 
        public void MouseDown()
        {
            currentTool.MouseDown();
        }
        public void MouseUp()
        {
            currentTool.MouseUp();
        }
        public ITools GetCurrentTools()
        {
            return currentTool;
        }
        public void SetCurrentTools(ITools tool)
        {
            this.currentTool = tool;
        }
    }
}
