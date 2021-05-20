using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Observer
{
    public class Chart : IObserver
    {
        private DataSource ds;
        public Chart(DataSource ds)
        {
            this.ds = ds;
        }
        public void Update()
        {
            Console.WriteLine("Chart got value = " + ds.Value);
        }
    }
}
