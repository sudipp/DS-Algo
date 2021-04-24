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
        //Unique Integers That Sum Up To 0
        //https://algo.monster/problems/unique_integers_that_sum_up_to_0
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

        //Min Deletions to Make Frequency of Each Letter Unique
        //https://molchevskyi.medium.com/best-solutions-for-microsoft-interview-tasks-min-deletions-to-make-frequency-of-each-letter-unique-16adb3ec694e

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
        public static int maxNetworkRank(int[] A, int[] B, int N)
        {
            int[] cities = new int[N + 1];
            //calculate in and out degree by cities
            for(int i = 0; i < A.Length; i++)
            {
                cities[A[i]] ++;
                cities[B[i]] ++;
            }

            int maxRank = 0;
            for (int i = 0; i < A.Length; i++)
            {
                // - 1, to count direct link between cities only once.
                maxRank = Math.Max(maxRank, cities[A[i]] + cities[B[i]] - 1);
            }
            return maxRank;
        }


        //https://molchevskyi.medium.com/best-solutions-for-microsoft-interview-tasks-min-deletions-to-make-frequency-of-each-letter-unique-16adb3ec694e
        //https://leetcode.com/problems/minimum-deletions-to-make-character-frequencies-unique/
        //1647. Minimum Deletions to Make Character Frequencies Unique
        //Min Deletions to Make Frequency of Each Letter Unique
        public int MinDeletions(string s)
        {
            //Step1 : get char occurance
            int maxFrequency = 0;
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (char c in s)
            {
                if (!dict.ContainsKey(c)) dict.Add(c, 0);
                dict[c]++;

                maxFrequency = Math.Max(maxFrequency, dict[c]);
            }

            //Step2 : store character by frequencies.. so char with same freqeuncty will be togther,
            List<char>[] frequencies = new List<char>[maxFrequency + 1];
            for (int i = 0; i < frequencies.Length; i++)
                frequencies[i] = new List<char>();

            foreach (char c in dict.Keys)
                frequencies[dict[c]].Add(c);

            //Step 3: From highest frequencies onwards check if there are more than 1 char with same frequency, then 
            //remove 1 frequency of first character.. this char 'c' will be moved to next lower frequency. 
            //We will keep on checking all frequencies for char occurance. we will terminate at index 1.
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
            //https://leetcode.com/discuss/interview-question/364618/

            //Step1 : get char frequency
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
        public static string LexicographicallySmallestString(string str)
        {
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
            int oB = str[0] == 'b' ? 1 : 0;
            for (int i = 1; i < str.Length; i++)
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
                    oB = str[i] == 'b' ? 1 : 0;
                }
            }
            return str;
        }


        //https://algo.monster/problems/longest_semi_alternating_substring
        //Longest Semi-Alternating Substring
        public static int SemiAlternateSubstring(string str)
        {
            if (str.Length < 3)
                return str.Length;

            int oA = str[0] == 'a' ? 1 : 0;
            int oB = str[0] == 'b' ? 1 : 0;
            int l = 0, maxLength = 0;
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == str[i - 1])
                {
                    if (str[i] == 'a')
                        oA++;
                    else if (str[i] == 'b')
                        oB++;

                    //if any exceeds 2, caclulate max length
                    if (oA == 3 || oB == 3)
                    {
                        if(str[i] == 'a')
                            oA --;
                        else if(str[i] == 'b')
                            oB--;

                        maxLength = Math.Max(maxLength, i - l);

                        //set l to prior 'index
                        l = i - 1;
                    }
                }
                else
                {
                    oA = str[i] == 'a' ? 1 : 0;
                    oB = str[i] == 'b' ? 1 : 0;
                }
            }
            return (l == 0) ? str.Length : maxLength;
        }


        //https://algo.monster/problems/max_inserts_to_obtain_string_without_3_consecutive_a
        //Max Inserts to Obtain String Without 3 Consecutive 'a'
        public static int maxInserts(string str)
        {
            int maxInsert = 0;
            int oA = str[0] == 'a' ? 1 : 0;

            //start has possible 2 padding, if first char is not 'a'
            if (str[0] != 'a') 
                maxInsert += 2;
            
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == str[i - 1])
                {
                    if (str[i] == 'a')
                        oA++;

                    //detect invalid string, return -1
                    if (oA == 3)
                        return -1;
                }
                else
                {
                    //except cuurent character != 'a', it will check for previous possible insert of character 'a'
                    if (str[i] != 'a') //current is not 'a'
                        maxInsert += (2 - oA);
                    
                    oA = str[i] == 'a' ? 1 : 0;
                }
            }

            //if (oA < 2) 
            //end has possible padding
            maxInsert += (2 - oA);

            return maxInsert;
        }


        //https://algo.monster/problems/concatenated_string_length_with_unique_characters
        //Concatenated String Length with unique Characters
        public static int maxLength(string[] args)
        {
            // "co", "di", "ity"
            byte[] char_bits_vector = new byte[26];
            int result = 0;

            foreach (string str in args)
            {
                byte[] char_bits = new byte[26];

                // set bits corresponding to chars in the string.
                foreach (char c in str) 
                    char_bits[c - 'a'] = 1;
                
                // How many bits were set.
                int bit_num = char_bits.Count(c=> c == 1);
                // the string contains duplicate characters so 
                // don't process it
                if (bit_num < str.Length)
                    continue;
                
                // Check if current word has common letters with 
                // already processed strings
                if (char_bits_vector.Intersect(char_bits).Count() > 0)
                    continue;

                char_bits_vector = char_bits_vector.Union(char_bits).ToArray();

                // add length of the current string to the result
                result = Math.Max(result, char_bits_vector.Count(c => c == 1));

                /*
                for (int i = char_bits_vector.Length - 1; i >= 0; i--)
                {
                    var c_b = char_bits_vector[i];
                    // if two bitsets have common 1 bits i.e. 
                    // if two strings have common letters don't 
                    // process current string

                    //if ((char_bits_vector[i] == char_bits[i]))
                    //    continue;
                    if ((c_b & char_bits).any()) continue;

                    // if current string has unique letters add 
                    // to the vector a bitset where 
                    // all bits corresponding to letters of the current 
                    // string are set to 1.
                    char_bits_vector.push_back(c_b | char_bits);
                    
                    // add length of the current string to the result
                    result = max<int>(result, c_b.count() + bit_num);
                }*/
            }



            //Recursion .... 
            int maxLen = 0;
            StringBuilder sb = new StringBuilder();
            maxLen = DFS(args, 0, 0, sb);
            return maxLen;
        }

        private static int DFS(string[] args, int idx, int prevLen, StringBuilder path)//string path)
        {
            //will the new path turn duplicate?? return 0 length
            if (!isUnique(path.ToString()))
                return 0;
            if (idx == args.Length)
                return prevLen;

            int localMax = prevLen;
            for(int i = idx; i < args.Length; i++)
            {
                if (!isUnique(args[i]))
                    continue;

                int id = path.Length;
                path.Append(args[i]);

                localMax = Math.Max(localMax, DFS(args, i + 1, prevLen + args[i].Length, path));

                path.Remove(id, args[i].Length);
            }
            return localMax;
        }

        private static bool isUnique(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Sort(chars);
            for (int i = 1; i < chars.Length; i++)
            {
                if (chars[i] == chars[i - 1])
                    return false;
            }
            return true;
        }


        //https://algo.monster/problems/largest_k_positive_and_negative
        //Largest K such that both K and -K exist in array
        public static int largestK(int[] nums)
        {
            int largest = 0;
            HashSet<int> hs = new HashSet<int>();
            for(int i = 0; i < nums.Length; i++)
            {
                if (hs.Contains(nums[i] * -1))
                    largest = Math.Max(largest, Math.Abs(nums[i]));
                else
                    hs.Add(nums[i]);
            }
            return largest;
        }

        //Min Adj Swaps to Group Red Balls
        //https://algo.monster/problems/min_adj_swaps_to_group_red_balls
        public static int minAdjSwap(string str)
        {
            //https://molchevskyi.medium.com/best-solutions-for-microsoft-interview-tasks-min-swaps-to-group-red-balls-aedd07dc5cd2
            //using 2 pointers - idea : We can get minimum swap when we will shrink towards middle from both side 

            int countR = 0, totalR = 0;
            foreach (char c in str)
                if(c == 'R') countR++;
            totalR = countR;

            int min = 0;
            int l = 0, r = str.Length - 1;
            while( l < r)
            {
                //find first 'R' from left
                while (l < str.Length && str[l] != 'R')
                    l++;
            
                //find first 'R' from right
                while (r > 0 && str[r] != 'R')
                    r--;
                
                //count number of 'W' between l and r.
                countR = countR - 2;
                int countW = r - l - 1 - countR;
                /*
                for(int l1 = l + 1; l1 < r; l1++)
                {
                    if(str[l1] == 'W')
                        countW++;
                }*/

                min += countW;
                l++;
                r--;
            }

            if (totalR == 0) return 0; //no 'R' available
            else if (totalR == 1) return -1; //with 1 'R'

            return min;
        }

        //Minimum Swaps to Group All 1's Together
        //https://leetcode.com/problems/minimum-swaps-to-group-all-1s-together/
        public static int MinSwaps(int[] data)
        {
            //leetcode.com/problems/minimum-swaps-to-group-all-1s-together/discuss/355506/JavaSliding-window-O(n)-with-detailed-explanation-very-easy-to-understand

            //sliding window **** make a window of size (number of 1 in array)
            int windowSize = 0; //number of 1 is widnwo size
            foreach (int i in data)
                if (i == 1) windowSize++;

            int max1InWindow = int.MinValue;

            int count1 = 0;
            //count number of 1 in window while moving towards rights
            for (int i = 0; i < windowSize; i++)
                if (data[i] == 1) count1++;

            int j = 0;
            while (j < data.Length - windowSize)
            {
                //Store the maximum number of 1 in a window
                max1InWindow = Math.Max(max1InWindow, count1);

                //slide foward
                if (data[j] == 1)
                    count1--;

                j++;
                if (data[j + windowSize - 1] == 1)
                    count1++;

                //the window with max numebr of 1 will give smallest swap
                max1InWindow = Math.Max(max1InWindow, count1);
            }

            //this is min swap
            return windowSize - max1InWindow;
        }


        //Particle Velocity
        //https://algo.monster/problems/particle_velocity
        public static int particleVelocity(int[] particles)
        {
            // [-1, 1, 3, 3, 3, 2, 3, 2, 1, 0]
            if (particles.Length < 3) return -1;
            int numStable = 0;
            for (int i = 0; i < particles.Length - 2; i ++)
            {
                for(int j = i + 1; j < particles.Length - 1; j++)
                {
                    if (particles[i + 1] - particles[i] == particles[j + 1] - particles[j])
                        numStable += 1;
                    else
                        break;
                }
            }

            return numStable;
        }
        

        private static int CompareHeights(Tuple<int, int> h1, Tuple<int, int> h2)
        {
            if (h1.Item1 == h2.Item1) //item1 is building indicies are same
                return 0;
            else
            {
                if (h1.Item2 == h2.Item2) //height same, sort by x or indicies
                    return h1.Item1.CompareTo(h2.Item1);
                else
                    return h2.Item2.CompareTo(h1.Item2); //higher height comes first
            }
        }
        public static IList<IList<int>> GetSkyline(int[][] buildings)
        {
            //https://www.youtube.com/watch?v=W2afZs9DYYA
            Tuple<int, int, int, int>[] events = new Tuple<int, int, int, int>[buildings.Length * 2];
            int j = 0;
            for (int i = 0; i < buildings.Length; i++)
            {
                //tuple stores - x-start/x-end, height, event type(0/1), building Index
                events[j++] = new Tuple<int, int, int, int>(buildings[i][0], buildings[i][2], 0, i); //0 - start event
                events[j++] = new Tuple<int, int, int, int>(buildings[i][1], buildings[i][2], 1, i); //1 - end event
            }
            Array.Sort(events, (t1, t2) => {
                //sort by x - coordinate (start/end)
                return t1.Item1.CompareTo(t2.Item1);
            });

            //Comparison<Tuple<int, int>> heightComparison = new Comparison<Tuple<int, int>>(CompareHeights);

            int n = events.Length;
            SortedSet<Tuple<int, int>> active_heights = new SortedSet<Tuple<int, int>>(Comparer<Tuple<int, int>>
                .Create(CompareHeights)
            );
            //add 0 as valid height
            active_heights.Add(new Tuple<int, int>(-1, 0));

            j = 0;
            IList<IList<int>> ans = new List<IList<int>>();
            while (j < n)
            {
                int cur_x = events[j].Item1;

                //process all events with same x together
                while (j < n && events[j].Item1 == cur_x)
                {
                    var evnt = events[j];
                    int height = evnt.Item2;
                    int evnt_type = evnt.Item3;
                    int index = evnt.Item4;

                    if (evnt_type == 0) //start event
                        active_heights.Add(new Tuple<int, int>(index, height)); //we will add this height into pool
                    else //end event
                        active_heights.Remove(new Tuple<int, int>(index, height)); //remove the height from pool

                    j++;
                }

                //higest height
                int higestHeight = active_heights.First().Item2;

                //check if the height has changed
                if (ans.Count == 0 || ans.Last()[1] != higestHeight)
                    ans.Add(new List<int>(new int[] { cur_x, higestHeight }));
            }
            return ans;
        }

        //https://leetcode.com/problems/smallest-subsequence-of-distinct-characters
        public static string SmallestSubsequence(string s)
        {
            //find character fequencies
            int[] freq = new int[26];
            foreach (char c in s)
                freq[c - 'a']++;

            //is the char used already.. to ensure no duplicate
            int[] usedChar = new int[26];

            StringBuilder sb = new StringBuilder();
            foreach (char c in s)
            {
                if (usedChar[c - 'a'] == 1) //already used it
                    continue;

                //reduce frequency
                freq[c - 'a']--;

                if (sb.Length > 0)
                {
                    char topC = sb[sb.Length - 1];
                    while (sb.Length > 0 && freq[topC - 'a'] > 0 && topC > c)
                    {
                        usedChar[topC - 'a'] = 0;
                        freq[topC - 'a']++;
                        sb.Remove(sb.Length - 1, 1);

                        if(sb.Length > 0)
                            topC = sb[sb.Length - 1];
                    }
                }
                sb.Append(c);
                usedChar[c - 'a'] = 1;
            }
            return sb.ToString();
        }


        //Jump Game
        //https://algo.monster/problems/jump_game
        public static bool jumpGame(List<int> arr, int start)
        {
            if (start >= 0 && start < arr.Count && arr[start] != -1)
            {
                int jump = arr[start];
                arr[start] = -1; //mark index 'start' as Visited.. so we dont use DFS on this.
                return jump == 0 || jumpGame(arr, start + jump) || jumpGame(arr, start - jump);
            }
            return false;
        }


        //https://leetcode.com/problems/string-to-integer-atoi/submissions/
        public int MyAtoi(string str)
        {
            long res = 0;

            var sign = 1;
            str = str.Trim();
            if (string.IsNullOrEmpty(str)) return 0;

            int index = 0;

            if (str[0] == '+' || str[0] == '-')
            {
                sign = (str[0] == '+') ? 1 : -1;
                index++;
            }

            while (index < str.Length)
            {
                if (char.IsNumber(str[index]))
                {
                    res = res * 10 + str[index] - '0';
                    if (res * sign > int.MaxValue) return int.MaxValue;
                    if (res * sign < int.MinValue) return int.MinValue;
                }
                else
                {
                    break;
                }
                index++;
            }
            return (int)res * sign;
        }

        //https://leetcode.com/problems/excel-sheet-column-number/
        //171. Excel Sheet Column Number
        public int TitleToNumber(string s)
        {
            //https://www.youtube.com/watch?v=g-l4UpF62x0&list=RDCMUCMn3_305DqmTylxJPFA8OJA&start_radio=1&t=6
            int result = 0;
            int mul = 1;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                result += mul * (s[i] - 'A' + 1);
                mul *= 26;
            }
            /*foreach(char c in s)
            {
                int d = c - 'A' + 1;
                result = result * 26 + d;
            }*/
            return result;
        }

        //https://leetcode.com/problems/excel-sheet-column-title/
        public string ConvertToTitle(int n)
        {
            StringBuilder result = new StringBuilder();
            while (n > 0)
            {
                n--;
                result.Insert(0, (char)('A' + (n % 26)));
                n /= 26;
            }

            return result.ToString();
        }

        //Widest Path Without Trees
        //https://algo.monster/problems/widest_path_without_trees
        public static int widestPath(List<int> x, List<int> y)
        {
            if (x.Count == 1 || y.Count == 1) return 0;

            SortedSet<int> xDistance = new SortedSet<int>();
            foreach (int xc in x)
                xDistance.Add(xc);
            
            int maxWidth = 0;
            int xl = xDistance.Min, xr = 0;
            xDistance.Remove(xDistance.Min);
            while (xDistance.Count > 0)
            {
                xr = xDistance.Min;
                maxWidth = Math.Max(maxWidth, xr - xl);

                xl = xr;
                xDistance.Remove(xDistance.Min);
            }

            // WRITE YOUR BRILLIANT CODE HERE
            return maxWidth;
        }

        //Fair Indexes
        //https://algo.monster/problems/fair_indexes
        public static int fairIndex(int[] a, int[] b)
        {
            if (a.Length > b.Length)
                fairIndex(b, a);

            int sumA = 0, sumB = 0;
            for (int i = 0; i < a.Length; i ++) sumA += a[i];
            for (int i = 0; i < b.Length; i ++) sumB += b[i];

            if (sumA != sumB)
                return 0;
            
            int count = 0;
            int sumAL = a[0], sumAR = sumA - a[0], sumBL = b[0], sumBR = sumB - b[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (sumAL == sumAR && sumAR == sumBL && sumBL == sumBR)
                    count++;

                if (i < a.Length)
                {
                    sumAL += a[i];
                    sumAR -= a[i];
                    sumBL += b[i];
                    sumBR -= b[i];
                }
            }

            return count;
        }

        //Plane Seat Reservation
        //1386. Cinema Seat Allocation
        //https://leetcode.com/problems/cinema-seat-allocation/submissions/
        public static int MaxNumberOfFamilies(int n, int[][] reservedSeats)
        {
            Array.Sort(reservedSeats, (i1, i2) => {
                return i1[0].CompareTo(i2[0]);
            });

            int lastRowBooked = reservedSeats[0][0];
            int maxTicket = 0;
            int r = 0;

            //for rows which are not booked at start
            if (lastRowBooked > 1)
                maxTicket += (lastRowBooked - 1) * 2;

            while (r < reservedSeats.Length)
            {
                bool asile1 = true, asile21 = true, asile22 = true, asile3 = true;

                while (lastRowBooked == -1 || r < reservedSeats.Length && lastRowBooked == reservedSeats[r][0])
                {
                    lastRowBooked = reservedSeats[r][0];
                    int seat = reservedSeats[r][1];

                    if (seat >= 2 && seat <= 3)
                        asile1 = false;
                    if (seat >= 8 && seat <= 9)
                        asile3 = false;
                    if (seat >= 4 && seat <= 5)
                        asile21 = false;
                    if (seat >= 6 && seat <= 7)
                        asile22 = false;

                    r++;
                }

                if (asile1 && asile21 && asile22 && asile3)
                    maxTicket += 2;
                else if (asile1 && asile21 || asile3 && asile22)
                    maxTicket += 1;
                else if (asile21 && asile22)
                    maxTicket += 1;

                //for rows which are not booked in between 
                if (r < reservedSeats.Length && reservedSeats[r][0] - lastRowBooked > 1)
                    maxTicket += (reservedSeats[r][0] - lastRowBooked - 1) * 2;

                //update lastRowBooked
                if (r < reservedSeats.Length)
                    lastRowBooked = reservedSeats[r][0];
            }

            //for rows which are not booked 
            if (n > lastRowBooked)
                maxTicket += (n - lastRowBooked) * 2;

            return maxTicket;
        }

        //Maximum possible value by inserting '5'
        public static int MaxPossibleByInsertingFive(int number)
        {
            string n1 = Math.Abs(number).ToString();
            int i = 0;
            while(i < n1.Length)
            {
                if (5.CompareTo(n1[i] - '0') >= 0 && number > -1) //positive
                    return int.Parse(n1.Substring(0, i) + '5' + n1.Substring(i, n1.Length - i)) * (number < 0 ? -1 : 1);
                else if (5.CompareTo(n1[i] - '0') <= 0 && number < 0) //negative
                    return int.Parse('5' + n1.Substring(i, n1.Length - i)) * (number < 0 ? -1 : 1);
                i++;
            }
            return int.Parse(n1 + '5') * (number < 0 ? -1 : 1);
        }

        //https://leetcode.com/discuss/interview-question/963586/Microsoft-or-OA-or-Codility
        /*
         * A string is considered balanced when every letter in the string appears both in uppercase and lowercase
For example, CATattac is balanced (a, c, t occur in both cases). Madam is not (a, d only appear in lowercase).
Write a function that given a string returns the shortest balanced substring of that string.
Can this be solved with a sliding window approach?
Update:
More examples
“azABaabza” returns “ABaab”
“TacoCat” returns -1 (not balanced)
“AcZCbaBz” returns the entire string
         */

        public static string GetShortestBalancedSubstring(string str)
        {
            int l = 0, r = 0;
            Dictionary<char, int> cap = new Dictionary<char,int>();
            Dictionary<char, int> low = new Dictionary<char, int>();
            while (l <= r && r < str.Length)
            {
                char c = str[r];
                //once immbalanced keep growing the window till we get shortest balanced..
                //once we balanced, try to shink from left.. to find the shorest
                if (char.IsUpper(c))
                {
                    if (!cap.ContainsKey(c)) cap.Add(c, 0);
                    cap[c]++;
                }
                else
                {
                    if (!low.ContainsKey(c)) low.Add(c, 0);
                    low[c]++;
                }
                
                //balanced ?? shink from left
                while(isDictionaryBalanced(cap, low))
                {
                    c = str[l];
                    if (char.IsUpper(c))
                    {
                        cap[c] --;
                        if (cap[c] == 0) cap.Remove(c);
                    }
                    else
                    {
                        low[c]--;
                        if (low[c] == 0) low.Remove(c);
                    }
                    l++;
                }
                r++;
            }

            if (l == r)
                return "-1";
            return str.Substring(l, r - l);
        }
        private static bool isDictionaryBalanced(Dictionary<char, int> cap, Dictionary<char, int> low)
        {
//            “azABaabza” returns “ABaab”
//“TacoCat” returns - 1(not balanced)
//“AcZCbaBz” returns the entire string

            //if (cap.Keys.Count != low.Keys.Count)
            //    return false;
            foreach (char key in cap.Keys)
            {
                if (!low.ContainsKey(key))// || cap[key] != low[key])
                    return false;
            }
            return true;
        }

    }
}
