using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.AmazonOA
{
    public class minimum_cost_merging_files
    {
        private static int Ascending((int, int) i, (int, int) j)
        {
            if (i.Item1 == j.Item1)
                return 0;
            else if (i.Item2 == j.Item2)
                return i.Item1.CompareTo(j.Item1);
            else
                return i.Item2.CompareTo(j.Item2);
        }

        //https://www.csestack.org/minimum-cost-merging-files/
        //https://leetcode.com/discuss/interview-question/1213316/Amazon-OA-SDE-I-(2-coding-questions)
        public static int minCostToMerge(int[] files)
        {
            if (files.Length == 0)
                return 0;

            SortedSet<(int, int)> minQ = new SortedSet<(int, int)>(Comparer<(int, int)>.Create(Ascending));
            int i = 0;
            for (; i < files.Length; i++)
                minQ.Add((i, files[i]));// new  Pair());

            int cost = 0;
            while (minQ.Count > 1)
            {
                int x = minQ.Min.Item2;//.V;
                minQ.Remove(minQ.Min);
                int y = minQ.Min.Item2;//.V;
                minQ.Remove(minQ.Min);

                minQ.Add((i++, x + y));// new Pair(i++, x + y));
                cost += (x + y);
            }
            return cost;
        }
    }
}
