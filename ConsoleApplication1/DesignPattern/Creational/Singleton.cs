using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Creational
{
    public sealed class Singleton3
    {
        private static readonly object lck = new object();
        private static Singleton3 instance = null;

        Singleton3() { }
        
        public static Singleton3 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lck)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton3();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
