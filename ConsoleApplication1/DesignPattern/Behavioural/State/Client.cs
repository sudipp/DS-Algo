using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.State
{
    public class Client
    {
        public static void main(string[] args)
        {
            var canvas = new Canvas();
            canvas.SetCurrentTools(new BrushTool());
            canvas.MouseDown();
            canvas.MouseUp();

            canvas.SetCurrentTools(new EraserTool());
            canvas.MouseDown();
            canvas.MouseUp();
        }
    }
}
