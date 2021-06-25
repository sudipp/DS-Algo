using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.SlidingWindow
{
    //https://leetcode.com/problems/sliding-window-maximum
    public class MaxSlidingWindow
    {
        public static int[] GetMaxSlidingWindow(int[] nums, int winSize)
        {
            //O(N), Space O(N)


            //we will maintain (Mono Descending Queue), Descending order value indicies 
            //Always add on the last side...
            //if the last value is bigger than current, as it direct
            //if the last value is small than current, keep on removing from last till list is empty or we find a bigger one

            if (nums == null || nums.Length == 0)
                return new int[0];

            var deque = new LinkedList<int>();
            IList<int> max = new List<int>();

            for (int i = 0; i < nums.Length; i++) // - winSize + 1; i++)
            {
                //if Max goes out of window, remove it from first/left
                if (deque.Count > 0 && i - winSize + 1 > deque.First.Value)
                    deque.RemoveFirst();

                //if the last value is small than current, keep on removing from last till list is empty 
                //or we find a bigger one and add it on the right/last
                while (deque.Count > 0 && nums[deque.Last.Value] <= nums[i])
                    deque.RemoveLast();

                deque.AddLast(i);

                if (i + 1 >= winSize)
                {
                    max.Add(nums[deque.First.Value]);
                }
            }

            return max.ToArray();
        }
    }
}
