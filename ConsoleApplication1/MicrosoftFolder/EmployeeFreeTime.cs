using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MicrosoftFolder
{
    public class EmployeeFreeTime
    {
        public IList<Interval> GetEmployeeFreeTime(IList<IList<Interval>> schedule)
        {
            Stack<Interval> stack = new Stack<Interval>();

            //merge N sorted list logic
            SortedDictionary<int, IList<IList<Interval>>>
                minQ = new SortedDictionary<int, IList<IList<Interval>>>();

            foreach (IList<Interval> intervals in schedule)
            {
                int start = intervals.First().start;
                if (!minQ.ContainsKey(start))
                    minQ.Add(start, new List<IList<Interval>>());

                minQ[start].Add(intervals);
            }

            while (minQ.Any())
            {
                int minStart = minQ.First().Key;

                IList<IList<Interval>> intervals = minQ.First().Value;

                Interval interval = intervals.First()[0];

                if (stack.Count == 0)
                    stack.Push(interval);
                else
                {
                    Interval siv = stack.Peek();

                    //compare with start and end
                    if (siv.end > interval.start)
                        siv.end = Math.Max(siv.end, interval.end);
                    else
                        stack.Push(interval);
                }

                //removed this processed interval, so we can reach the next interval in list
                intervals.First().Remove(interval);
                //if there are more intervals after it add them into minQ
                if (intervals.First().Count > 0)
                {
                    int start = intervals.First()[0].start;
                    if (!minQ.ContainsKey(start))
                        minQ.Add(start, new List<IList<Interval>>());

                    minQ[start].Add(intervals.First());
                }
                //as we processed this interval, we must delete it from same interval list 
                intervals.Remove(intervals.First());

                if (intervals.Count == 0) // delete q item, if no intervals are exists
                    minQ.Remove(minStart);
            }


            IList<Interval> result = new List<Interval>();
            while (stack.Any())
            {
                Interval isv = stack.Pop();
                if (stack.Any() && stack.Peek().end < isv.start)
                {
                    Interval iv = new Interval(stack.Peek().end, isv.start);
                    result.Insert(0, iv);
                }
            }
            return result;
        }
    }

    public class Interval
    {
        public int start;
        public int end;

        public Interval() { }
        public Interval(int _start, int _end)
        {
            start = _start;
            end = _end;
        }
    }
}
