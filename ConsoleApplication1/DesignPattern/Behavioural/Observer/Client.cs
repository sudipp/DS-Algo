using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Observer
{
    public class Client
    {
        public static void main(string[] args)
        {
            var ds = new DataSource();
            var sheet1 = new Spreadsheet(ds);
            var chart1 = new Chart(ds);

            ds.AddObserver(sheet1);
            ds.AddObserver(chart1);

            ds.Value = 1;
        }
    }
}
