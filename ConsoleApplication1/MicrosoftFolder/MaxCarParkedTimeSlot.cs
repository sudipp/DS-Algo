using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MicrosoftFolder
{
    //https://leetcode.com/discuss/interview-question/1105375/Microsoft-Virtual-Onsite

    //There is a parking lot which can park cars. Given the entrance time and the 
    //exit time of a car, find time at which there will be maximum number of cars 
    //in the parking lot. The time is given in 24 hr clock.
    public class MaxCarParkedTimeSlot
    {
        static int Descending(int t1, int t2)
        {
            return t2.CompareTo(t1);
        }

        public static int[] GetMaxCarParkedTimeSlot(int[][] carTickets)
        {
            if (carTickets.Length == 0)
                return new int[] { -1, -1 };

            Array.Sort(carTickets, (int[] o1, int[] o2) => {
                return o1[0].CompareTo(o2[0]); //sort by start time ascending
            });

            Stack<int[]> stack = new Stack<int[]>();
            stack.Push(new int[] { carTickets[0][0], carTickets[0][1] });

            int carCount = 1;
            int maxSt = carTickets[0][0];
            int minEnd = carTickets[0][1];

            SortedDictionary<int, int[]> maxQ = new SortedDictionary<int, int[]>(Comparer<int>.Create(Descending));
            maxQ.Add(carCount, new int[] { maxSt, minEnd });

            for (int i = 1; i < carTickets.Length; i ++)
            {
                int[] lastInterval = stack.Peek();

                int start = carTickets[i][0];
                int end = carTickets[i][1];

                if (lastInterval[1] > start)
                {
                    carCount ++;
                    lastInterval[1] = Math.Max(lastInterval[1], end);

                    maxSt = Math.Max(maxSt, start);
                    minEnd = Math.Min(minEnd, end);
                }
                else
                {
                    maxQ.Add(carCount, new int[] { maxSt, minEnd });

                    carCount = 1;
                    stack.Push(carTickets[i]);

                    maxSt = carTickets[i][0];
                    minEnd = carTickets[i][1];
                }
            }
            maxQ.Add(carCount, new int[] { maxSt, minEnd });

            return maxQ.First().Value;
        }
    }
}
