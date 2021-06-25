using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.SlidingWindow
{
    //https://leetcode.com/problems/maximum-number-of-occurrences-of-a-substring/
    public class MaxFreq
    {
        public static int GetMaxFreq(string s, int maxLetters, int minSize, int maxSize)
        {

            Dictionary<char, int> occurances = new Dictionary<char, int>();
            Dictionary<string, int> substringOccuranes = new Dictionary<string, int>();
            int result = 0, left = 0, right = 0;

            for (right = 0; right < s.Length; right++)
            {
                if (!occurances.ContainsKey(s[right]))
                    occurances.Add(s[right], 0);
                occurances[s[right]]++;

                if(right - left >= minSize)
                {
                    char l = s[left++];
                    occurances[l]--;
                    if (occurances[l] == 0)
                        occurances.Remove(l);
                }

                //Rule 2 - Substring/window size can't  
                if (right - left + 1 >= minSize && occurances.Count <= maxLetters)
                {
                    string substr = s.Substring(left, right - left + 1);

                    if (!substringOccuranes.ContainsKey(substr))
                        substringOccuranes.Add(substr, 0);

                    substringOccuranes[substr]++;
                    result = Math.Max(result, substringOccuranes[substr]);

                    Console.WriteLine(substr + ":" + substringOccuranes[substr]);
                }
            }

            return result;
        }
    }
}
