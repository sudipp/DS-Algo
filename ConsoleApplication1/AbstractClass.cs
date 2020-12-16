using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    public interface IAbstract
    {
        int Sum();
    }
    public abstract class AbstractClass : IAbstract
    {
        private int x1 = 0;
        //public AbstractClass()
        //{ }
        public AbstractClass(int x)
        {
            x1 = x;
        }

        public void hello(int x)
        {
            x1 = x;
        }

        public abstract int Sum();
    }



    public class DerivedClass : AbstractClass
    {
        public DerivedClass() : base(10)
        {
            
        }

        public override int Sum()
        {
            return 10;
        }
    }


    public class AbstractTest
    {
        public static void runTest()
        {
            //AbstractClass a1 = new AbstractClass(1);

            DerivedClass a = new DerivedClass();
            a.Sum();
        }
    }
}
