using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MicrosoftFolder
{
    //https://leetcode.com/discuss/interview-question/1176191/Microsoft-Online-Onsite-Interview
    public class MaxSum2NumberWhoseDigitSumsAreEqual
    {
        /*
        Write a function given array A consisting of N integers, returns the maximum sum of two numbers whose 
        digits add up to an equal sum. If there are no two numbers whose digits have an equal sum, the function 
        should return -1.

        Examples:

        Given A=[51,71,17,42], the function should return 93. There are two pairs of numbers whose digits add 
            up to an equal sum: (51,42) and (17,71). The first pairs sums up to 93.
        Given A=[42,33,60], the function shpuld return 102. The digits of all numbers in A add up to the same 
            sum, and choosing to add 42 and 60 gives the result 102.
        Given A=[51,32,43], the function should return -1, since all numbers in A have digits that add up to 
            different, unique sums 
        */

        public static int GetMaxSum2NumberWhoseDigitSumsAreEqual(int[] arr)
        {
            //dic with minQ
            Dictionary<int, SortedSet<int>> dict = new Dictionary<int, SortedSet<int>>();
            foreach(int n in arr)
            {
                int n1 = n;
                int digitSum = 0;
                while (n1 != 0)
                {
                    digitSum += n1 % 10;
                    n1 = n1 / 10;
                }

                if (!dict.ContainsKey(digitSum))
                    dict.Add(digitSum, new SortedSet<int>());

                dict[digitSum].Add(n);
                if (dict[digitSum].Count > 2)
                    dict[digitSum].Remove(dict[digitSum].Min);
            }

            int maxSum = -1;
            foreach(int key in dict.Keys)
            {
                if (dict[key].Count > 1)
                    maxSum = Math.Max(maxSum, dict[key].Sum());
            }
            return maxSum;
        }
    }
}
