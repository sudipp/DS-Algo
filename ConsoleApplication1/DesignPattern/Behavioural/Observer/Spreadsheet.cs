using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Observer
{
    public class Spreadsheet : IObserver
    {
        private DataSource ds;
        public Spreadsheet(DataSource ds)
        {
            this.ds = ds;
        }
        public void Update()
        {
            Console.WriteLine("Spreadsheet got " + ds.Value);
        }
    }
}
