using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Microsoft
    {
        //1304. Find N Unique Integers Sum up to Zero
        public static int[] SumZero(int n)
        {
            int[] res = new int[n];
            int left = 0, right = n - 1, start = 1;
            while (left < right)
            {
                res[left++] = start;
                res[right--] = -start;
                start++;
            }
            return res;
        }

        //larget alphabetic character
        public static string largestCharacter(string str)
        {
            // Array for keeping track of both uppercase
            // and lowercase english alphabets
            bool[] uppercase = new bool[26];
            bool[] lowercase = new bool[26];

            foreach (char c in str)
            {
                if (char.IsLower(c))
                    lowercase[c - 'a'] = true;

                if (char.IsUpper(c))
                    uppercase[c - 'A'] = true;
            }

            // Iterate from right side of array
            // to get the largest index character
            for (int i = 25; i >= 0; i--)
            {

                // Check for the character if both its
                // uppercase and lowercase exist or not
                if (uppercase[i] && lowercase[i])
                    return (char)(i + 'A') + "";
            }

            // Return -1 if no such character whose
            // uppercase and lowercase present in
            // string str
            return "-1";
        }

        //Visible Nodes in Binary Tree
        //https://leetcode.com/problems/count-good-nodes-in-binary-tree/submissions/
        class GoodNodeBST
        {
            //https://leetcode.com/problems/count-good-nodes-in-binary-tree/submissions/
            int count = 0;
            public int GoodNodes(TreeNode root)
            {
                DFS(root, root.val);
                return count;
            }
            private void DFS(TreeNode root, int maxSoFar)
            {
                if (root == null)
                    return;

                //increase count, if there is no nodes grater than the current
                if (root.val >= maxSoFar)
                {
                    count++;
                }

                //find the current MAX and pass it levels down
                maxSoFar = Math.Max(maxSoFar, root.val);

                if (root.left_ptr != null)
                    DFS(root.left_ptr, maxSoFar);

                if (root.right_ptr != null)
                    DFS(root.right_ptr, maxSoFar);
            }
        }


        //Min Deletions To Obtain String in Right Format
        //https://algo.monster/problems/min_deletions_to_obtain_string_in_right_format
        public static int minStep(String str)
        {
            // WRITE YOUR BRILLIANT CODE HERE
            int charA = 'A';
            int numB = 0;
            int minDel = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (charA == str[i])
                {
                    minDel = Math.Min(numB, minDel + 1);
                }
                else
                {
                    numB++;
                }
            }
            return minDel;
        }

        //algo.monster/problems/day_of_week
        //Day of week that is K days later
        public static string dayOfWeek(string day, int k)
        {
            // WRITE YOUR BRILLIANT CODE HERE
            List<string> days = new List<string>(new string[] { "Monday",
                "Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"
            });

            int index = days.IndexOf(day);
            return days[(index + k) % 7];
        }

        //https://algo.monster/problems/max_network_rank
        //Max Network Rank


        //https://leetcode.com/problems/minimum-deletions-to-make-character-frequencies-unique/
        //1647. Minimum Deletions to Make Character Frequencies Unique
        public int MinDeletions(string s)
        {
            int maxFrequency = 0;
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (char c in s)
            {
                if (!dict.ContainsKey(c)) dict.Add(c, 0);
                dict[c]++;

                maxFrequency = Math.Max(maxFrequency, dict[c]);
            }

            //create afrequency array.. and store characters under those frequency
            List<char>[] frequencies = new List<char>[maxFrequency + 1];
            for (int i = 0; i < frequencies.Length; i++)
                frequencies[i] = new List<char>();

            foreach (char c in dict.Keys)
                frequencies[dict[c]].Add(c);

            int deletion = 0;
            for (int i = frequencies.Length - 1; i >= 1; i--)
            {
                //more than 1 charcter in same frequency.. delete a cracter of it.. so it will go to next small frequency
                while (frequencies[i].Count > 1)
                {
                    char itemToBeReduced = frequencies[i].First();
                    frequencies[i].Remove(itemToBeReduced);

                    deletion += 1;

                    //delete 1 character and move it to next lower freqency
                    frequencies[i - 1].Add(itemToBeReduced);
                }
            }
            return deletion;
        }

        //Min Steps to Make Piles Equal Height
        //https://algo.monster/problems/min_steps_to_make_piles_equal_height
        public static int minSteps(int[] nums)
        {
            Dictionary<int, int> numFrequency = new Dictionary<int, int>();
            foreach (int i in nums)
            {
                if (!numFrequency.ContainsKey(i)) numFrequency.Add(i, 0);
                numFrequency[i]++;
            }
            //order by the Number descending
            numFrequency.OrderByDescending(o => o.Key);
            int[] keys = new int[numFrequency.Keys.Count];
            numFrequency.Keys.CopyTo(keys, 0);

            int minStep = 0;
            for (int i = 0; i < keys.Length - 1; i++)
            {
                int key = keys[i];

                //making these equal to next higher, so adding those to next higher
                numFrequency[keys[key + 1]] += numFrequency[key];

                minStep += numFrequency[key];
            }
            return minStep;
        }

        //https://leetcode.com/problems/regular-expression-matching/
        public bool IsMatch(string text, string pattern)
        {
            bool[,] dp = new bool[text.Length + 1, pattern.Length + 1];

            //Empty string and Empty pattern, result to true
            dp[0, 0] = true;

            //Deals with patterns like a* or a*b* or a*b*c* 
            for (int i = 1; i < dp.GetLength(1); i++)
            {
                //if there is '*' then get value from left 2 cells (* means 0 or more of Preceding letter)
                if (pattern[i - 1] == '*')
                {
                    dp[0, i] = dp[0, i - 2];
                }
            }

            // build matrix
            for (int i = 1; i < dp.GetLength(0); i++)
            {
                for (int p = 1; p < dp.GetLength(1); p++)
                {
                    if (pattern[p - 1] == '.' || pattern[p - 1] == text[i - 1])
                    {
                        dp[i, p] = dp[i - 1, p - 1]; //look diagonal
                    }
                    else if (pattern[p - 1] == '*')
                    {
                        //get the value from previous cell (* means 0 or more of Preceding letter)
                        dp[i, p] = dp[i, p - 2];

                        if (pattern[p - 2] == '.' || pattern[p - 2] == text[i - 1])
                        {
                            dp[i, p] = dp[i, p] || dp[i - 1, p];
                        }
                    }
                    else
                    {
                        dp[i, p] = false;
                    }
                }
            }
            return dp[text.Length, pattern.Length];
        }


        //https://algo.monster/problems/min_swaps_to_make_palindrome
        //Minimum Adjacent Swaps to Make Palindrome
        public static int minSwap(string str)
        {
            //O(n ^ 2)
            int n = str.Length;
            char[] s = str.ToCharArray();
            int count = 0;
            for (int i = 0; i < n / 2; i++)
            {
                int left = i;
                int right = n - left - 1;
                //keep going to left till we find the same letter on left and right
                while (left < right)
                {
                    if (s[left] == s[right])
                    {
                        break;
                    }
                    else
                    {
                        right--;
                    }
                }

                if (left == right)
                {
                    return -1;
                }
                else
                {
                    //move the pair of item, pounted by 'right' to the mirrored position to right
                    for (int j = right; j < n - left - 1; j++)
                    {
                        char t = s[j];
                        s[j] = s[j + 1];
                        s[j + 1] = t;
                        count++;
                    }
                }
            }
            return count;
        }

        //https://algo.monster/problems/lexicographically_smallest_string
        //Lexicographically Smallest String
        public static string LexicographicallySmallestString(String str)
        {
            // WRITE YOUR BRILLIANT CODE HERE
            int i = 0;
            int s_size = str.Length;
            for (; i < s_size - 1; ++i)
            {
                if (str[i] > str[i + 1])
                {
                    break;
                }
            }
            return str.Substring(0, i) + str.Substring(i + 1, str.Length);
        }

        //Longest Substring Without Two Contiguous Occurrences of Letter
        //https://algo.monster/problems/longest_substring_without_3_contiguous_occurrences_letter
        public static string longestValidString(string str)
        {
            if (str.Length == 0) return "";

            int oA = str[0] == 'a' ? 1 : 0;
            int oB = (oA == 1 ) ? 0 : 1;
            for(int i = 1; i < str.Length; i++)
            {
                if(str[i] == str [i-1])
                {
                    if (str[i] == 'a')
                        oA ++;
                    else if (str[i] == 'b')
                        oB ++;

                    //if any exceeds
                    if (oA > 2 || oB > 2)
                        return str.Substring(0, i);
                }
                else
                {
                    oA = str[i] == 'a' ? 1 : 0;
                    oB = (oA == 1) ? 0 : 1;
                }
            }
            return str;
        }


        //https://algo.monster/problems/longest_semi_alternating_substring
        //Longest Semi-Alternating Substring
        public static int SemiAlternateSubstring(string str)
        {
            //baaabbabbb

            if (str.Length < 3)
                return str.Length;

            int oA = str[0] == 'a' ? 1 : 0;
            int oB = (oA == 1) ? 0 : 1;
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == str[i - 1])
                {
                    if (str[i] == 'a')
                        oA++;
                    else if (str[i] == 'b')
                        oB++;

                    //if any exceeds
                    if (oA > 3 || oB > 3)
                    {
                        //return str.Substring(0, i);
                        maxLength = Math.Max(maxLength, r - l + 1);
                    }
                }
                else
                {
                    oA = str[i] == 'a' ? 1 : 0;
                    oB = (oA == 1) ? 0 : 1;
                }

            }



            //sliding window
            Dictionary<char, int> occurance = new Dictionary<char, int>();
            occurance.Add('a', 0);
            occurance.Add('b', 0);

            int l = 0, r = 0, maxLength = 0;
            while( r < s.Length)
            {
                occurance[s[r]] ++;

                while (occurance['a'] > 2 || occurance['b'] > 2)
                {
                    occurance[s[l]] --;
                    l++;
                }

                maxLength = Math.Max(maxLength, r - l + 1);
                r++;
            }

            return maxLength;
        }
    }
}
