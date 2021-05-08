using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MicrosoftFolder
{
    //https://leetcode.com/problems/find-median-from-data-stream/
    public class StreamMedianFinder
    {
        class Pair
        {
            public int i;
            public int val;
            public Pair(int I, int N)
            {
                i = I;
                val = N;
            }
        }

        int idx = 0;
        SortedSet<Pair> minQ = null;
        SortedSet<Pair> maxQ = null;

        private static int Descending(Pair i, Pair j)
        {
            if (i.i == j.i)
                return 0;
            else
                if (j.val == i.val)
                return j.i.CompareTo(i.i);
            else
                return j.val.CompareTo(i.val);
        }
        private static int Ascending(Pair i, Pair j)
        {
            if (i.i == j.i)
                return 0;
            else
                if (i.val == j.val)
                return j.i.CompareTo(i.i);
            else
                return i.val.CompareTo(j.val);
        }

        public StreamMedianFinder()
        {
            minQ = new SortedSet<Pair>(Comparer<Pair>.Create(Ascending));
            maxQ = new SortedSet<Pair>(Comparer<Pair>.Create(Descending));
        }

        public void AddNum(int num)
        {
            maxQ.Add(new Pair(idx++, num));
            minQ.Add(maxQ.First());
            maxQ.Remove(maxQ.First());

            if (minQ.Count > maxQ.Count)
            {
                maxQ.Add(minQ.First());
                minQ.Remove(minQ.First());
            }
        }

        public double FindMedian()
        {
            if (maxQ.Count > minQ.Count)
                return maxQ.First().val;
            else
                return (minQ.First().val + maxQ.First().val) * 0.5;
        }
    }

}
