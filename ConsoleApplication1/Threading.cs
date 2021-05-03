using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Threading
    {
        public static void DemonstrateConcurrency()
        {
            //Concurrency means running on single core
            //Demonstrate Concurrency *** only 1 foreground thread created.....
            Task1();
            Task2();
            Console.WriteLine("give input");
            string str = Console.ReadLine();
            Console.WriteLine("Entered {0} ", str);
        }

        public static async void Task1()
        {
            await Task.Delay(4000);
            Console.WriteLine("Task 1 completed");
        }

        public static async void Task2()
        {
            await Task.Delay(4000);
            Console.WriteLine("Task 2 completed");
        }

        public static void DemonstrateParallelism()
        {
            Parallel.For(0, 100000, x => Task11());
            //Thread t = new Thread(new ThreadStart(Task111));
            //t.Start();
            //Parallelism means running on mutiple core
            //Demonstrate Parallelism *** Multiple threads are created.....
            //Task.Factory.StartNew(Task11);
            //Task.Factory.StartNew(Task22);

            Console.WriteLine("give input");
            string str = Console.ReadLine();
            Console.WriteLine("Entered {0} ", str);
        }

        public static void Task11()
        {
            Task.Delay(4000);
            Console.WriteLine("Task 11 completed");
        }
        public static void Task111()
        {
            for (int i = 0; i < 100000; i++)
            {
                Task.Delay(4000);
                Console.WriteLine("Task 111 completed");
            }
        }

        public static void Task22()
        {
            Task.Delay(4000);
            Console.WriteLine("Task 22 completed");
        }
    }
}
