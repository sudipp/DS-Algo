using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Observer
{
    public class DataSource : SubjectObservable
    {
        private int value;
        public int Value
        {   get => value;
            set
            {
                this.value = value;
                this.NotifyObservers(this.value);
            }
        }
    }
}
