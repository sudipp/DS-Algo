using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using ConsoleApplication1.DesignPattern.Behavioural.Visitor;
using ConsoleApplication1.DesignPattern.Behavioural.Command;
using ConsoleApplication1.DesignPattern.Structural.Decorator;
using ConsoleApplication1.DesignPattern.Structural.Proxy;
using ConsoleApplication1.AmazonOA;
using ConsoleApplication1.MicrosoftFolder;
using ConsoleApplication1.Stack;
using ConsoleApplication1.SlidingWindow;

namespace ConsoleApplication1
{
    class Program
    {
        public static int[] LargestSubarray(int[] nums, int k)
        {
            IList<int> result = new List<int>();

            //how many subarray we can build??
            int KSizeSubArrayCount = nums.Length - k + 1;

            //subarray could be overlapping or not (k = 1)
            //if subarray is not over lapping... then we need to loop           till KSizeSubArrayCount

            if (k == 1) //not over lapping, so we need loop
            {
                //how many loop??  KSizeSubArrayCount

            }
            else
            {
                for (int i = 0; i < k; i++)
                {
                    int maxValIndex = i;

                    int count = 0;
                    int j = i;
                    for (; j < i + KSizeSubArrayCount; j++)
                    {
                        if (nums[j] >= nums[maxValIndex])
                        {
                            count++;

                            //if (count > 1)
                            //    break;

                            maxValIndex = i;
                        }
                        j++;
                    }

                    if (count == 1)
                    {
                        //result.Add();
                        return result.ToArray();
                    }
                }
            }
            return null;




            //1,4,5,2,3  k=3
            //1,4,5
            //4,5,2
            //5,2,3

            //1,3,5,2,3  k=3
            //1,3,5
            //3,5,2
            //5,2,3

            //1,4,5,2,3,6  k=2
            //1,4
            //4,5
            //5,2
            //2,3
            //3,6

            //1,4,5,2,3,6  k=5
            //1,4,5,2,3
            //4,5,2,3,6
        }

        public static IList<IList<int>> ThreeSum1(int[] nums)
        {
            //Must sort the array to reduce the runtime...

            Array.Sort(nums);

            IList<IList<int>> result = new List<IList<int>>();
            for (int i = 0; i < nums.Length - 2; i++)
            {
                //duplicate start element, avoid
                if (i > 0 && nums[i] == nums[i - 1])
                    continue;

                int l = i + 1;
                int r = nums.Length - 1;

                //use sliding window... 
                //if sum < 0, shrink it from left(move towards right)
                //if sum > 0, shrink it from right, is ==, shrink it from bothside
                while (l < r)
                {
                    //duplicate high elements
                    if (r + 1 < nums.Length && nums[r] == nums[r + 1])
                    {
                        r--;
                        continue;
                    }

                    if (nums[l] + nums[r] + nums[i] == 0)
                    {
                        result.Add(new List<int>(new int[] { nums[i], nums[l], nums[r] }));

                        r--;
                        l++;
                    }
                    else if (nums[l] + nums[r] + nums[i] < 0)
                    {
                        l++;
                    }
                    else
                    {
                        r--;
                    }
                }
            }

            return result;
        }

        public static int LengthOfLongestSubstring(string s)
        {

            HashSet<char> dict = new HashSet<char>();

            int max = 0;
            int l = 0;
            int r = 0;
            while (l <= r && r < s.Length)
            {
                Console.WriteLine(l + ":" + r + ":" + max);

                if (dict.Contains(s[r]))
                {
                    Console.WriteLine("indise");

                    //shrink window from left till it has all unique elements
                    while (dict.Contains(s[r]))
                    {
                        dict.Remove(s[l]);
                        l++;
                    }

                    dict.Add(s[r]);
                }
                else
                {
                    dict.Add(s[r]);
                    max = Math.Max(max, r - l + 1);
                    r++;
                }
            }

            return max;
        }

        public class Solution
        {

            public static IList<int> FindSubstring(string s, string[] words)
            {
                IList<int> result = new List<int>();
                if (s.Length < words[0].Length)
                    return result;

                int totalChars = words[0].Length * words.Length;

                Dictionary<string, int> tMap = new Dictionary<string, int>();
                foreach (string str in words)
                {
                    if (!tMap.ContainsKey(str))
                        tMap.Add(str, 0);
                    tMap[str]++;
                }

                for (int l = 0; l <= s.Length - totalChars; l++)
                {
                    //O(m - n) time complexity, where m is Length of s, n is the Length of all word
                    Dictionary<string, int> tempMap = new Dictionary<string, int>(tMap);

                    int r = 0;
                    while (!IsWinValid(tempMap) && r < totalChars)
                    {
                        string str = s.Substring(l + r, words[0].Length);
                        if (tempMap.ContainsKey(str))
                        {
                            if (tempMap[str] > 0)
                                tempMap[str]--;
                        }
                        r = r + words[0].Length;
                    }

                    if (!IsWinValid(tempMap))
                        continue;

                    result.Add(l);
                }

                return result;
            }

            private static bool IsWinValid(Dictionary<string, int> tMap)
            {
                foreach (string str in tMap.Keys)
                {
                    if (tMap[str] > 0) return false;
                }
                return true;
            }

        }

        public static string Convert(string s, int numRows)
        {
            IList<IList<char>> result = new List<IList<char>>();
            for (int i = 0; i < numRows; i++)
                result.Add(new List<char>());

            int insertAryIndex = 0;
            bool direction = true; //going down
            for (int i = 0; i < s.Length; i++)
            {
                result[insertAryIndex].Add(s[i]);

                //true is going down

                if (direction)
                {
                    if (insertAryIndex == numRows - 1)
                    {
                        direction = false;
                        if (insertAryIndex > 0)
                            insertAryIndex--;
                    }
                    else
                    {
                        if (insertAryIndex < numRows - 1)
                            insertAryIndex++;
                    }
                }
                else
                {
                    if (insertAryIndex == 0)
                    {
                        direction = true;
                        if (insertAryIndex < numRows - 1)
                            insertAryIndex++;
                    }
                    else
                    {
                        if (insertAryIndex > 0)
                            insertAryIndex--;
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numRows; i++)
                sb.Append(new string(result[i].ToArray()));
            return sb.ToString();
        }

        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            int[] buffer = new int[26];
            Dictionary<string, IList<string>> ans = new Dictionary<string, IList<string>>();

            for (int i = 0; i < strs.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                    buffer[j] = 0;

                foreach (char c in strs[i])
                    buffer[c - 'a']++;

                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < 26; k++)
                {
                    for (int j = 0; j < buffer[k]; j++)
                    {
                        sb.Append((char)(k + 'a'));
                    }
                }

                string key = sb.ToString();
                if (!ans.ContainsKey(key))
                    ans.Add(key, new List<string>());
                ans[key].Add(strs[i]);
            }

            IList<IList<string>> result = new List<IList<string>>();
            foreach (string key in ans.Keys)
                result.Add(ans[key]);
            return result;
        }

        public static int[][] Insert(int[][] intervals, int[] newInterval)
        {
            int s = newInterval[0], e = newInterval[1];
            IList<int[]> left = new List<int[]>();
            IList<int[]> right = new List<int[]>();

            for (int i = 0; i < intervals.Length; i++)
            {
                if (intervals[i][1] < s) //interval end is less than new S
                    left.Add(intervals[i]);
                else if (intervals[i][0] > e) //interval start is greater than new E
                    right.Add(intervals[i]);
                else
                {
                    s = Math.Min(s, intervals[i][0]);
                    e = Math.Max(e, intervals[i][1]);
                }
            }

            left.Add(new int[] { s, e });
            return left.Concat(right).ToArray();
        }

        public static IList<string> FullJustify(string[] words, int maxWidth)
        {

            IList<string> result = new List<string>();
            int start = 0, sum = 0, sumLast = 0;

            for (int i = 0; i < words.Length; i++)
            {
                if (i > 0)
                    sumLast += words[i - 1].Length;
                sum += words[i].Length;

                //Console.WriteLine(sum);

                if (sum + (i - start) > maxWidth)
                {
                    //build till i - 1;
                    result.Add(BuildString(words, start, i - 1, maxWidth - (sumLast)));

                    start = i;
                    sumLast = 0;
                    if (i != words.Length - 1)
                        sum = words[i].Length;
                }
            }

            if (sum > 0)
            {
                result.Add(BuildString(words, start, words.Length - 1, maxWidth - sum));// (sumLast + words[words.Length - 1].Length)));
            }

            return result;
        }

        private static string BuildString(string[] words, int start, int end, int padLeft)
        {
            StringBuilder sb = new StringBuilder();

            int wordCount = end - start + 1;
            int padSplitCount = 0, padSplitCountCarry = 0;
            if (wordCount > 1)
            {
                padSplitCount = padLeft / (wordCount - 1);
                padSplitCountCarry = padLeft % (wordCount - 1);
            }
            else
            {
                padSplitCountCarry = padLeft;
            }

            Console.WriteLine(wordCount + ":" + padSplitCount + ":" + padSplitCountCarry);

            for (int i = start; i <= end; i++)
            {
                sb.Append(words[i]);

                if (i < end)
                    sb.Append(new string(' ', padSplitCount));

                if (padSplitCountCarry > 0)
                {
                    sb.Append(new string(' ', padSplitCountCarry));
                    padSplitCountCarry = 0;
                }
            }

            return sb.ToString();
        }

        public static string SimplifyPath(string path)
        {
            Stack<string> stack = new Stack<string>();

            int start = 1;
            for (int i = 0; i < path.Length; i++)
            {
                if (i > 0 && path[i] == '/')
                {
                    if (i - start > 0) //escaping 2 kore '/' together
                    {
                        string pathSegment = path.Substring(start, i - start);
                        if (pathSegment == "..")
                        {
                            if (stack.Count > 0)
                                stack.Pop();
                        }
                        else if (pathSegment != ".")
                            stack.Push(pathSegment);
                    }
                    start = i + 1;
                }
            }

            if (start != path.Length) //if the last char is not '/'
            {
                string pathSegment = path.Substring(start, path.Length - start);
                if (pathSegment == "..")
                {
                    if (stack.Count > 0)
                        stack.Pop();
                }
                else if (pathSegment != ".")
                    stack.Push(pathSegment);
            }


            if (stack.Count == 0)
                return "/";

            Console.WriteLine(stack.Count);

            StringBuilder sb = new StringBuilder();
            while (stack.Any())
            {
                sb.Insert(0, stack.Pop());
                sb.Insert(0, "/");
            }

            return sb.ToString();
        }

        class DescendingComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y.CompareTo(this);
            }
        }

        public class MinTaps
        {
            public static int MinTaps1(int n, int[] ranges)
            {
                int[] reach = new int[n + 1];
                for (int j = 0; j < ranges.Length; j++)
                {
                    int left = Math.Max(0, j - ranges[j]);
                    int right = Math.Min(n, j + ranges[j]);

                    reach[left] = Math.Max(reach[left], right);
                }

                int count = 1;
                int coverage = reach[0], newCoverage = reach[0];
                int i = 1;
                while (coverage < n) // keep going till n, once we reach n, exit the process
                {
                    bool foundBiggerCoverage = false;
                    for (; i <= coverage; i++)
                    {
                        if (reach[i] > coverage)
                        {
                            //pick the max bigger coverage
                            newCoverage = Math.Max(newCoverage, reach[i]);
                            foundBiggerCoverage = true;
                        }
                    }
                    if (!foundBiggerCoverage)
                        return -1;

                    coverage = newCoverage;
                    count++;
                }
                return count;
            }
        }

        public class GenerateTrees
        {
            public static IList<TreeNode> GenerateTrees1(int n)
            {
                int[] nums = new int[n];
                for (int i = 0; i < n; i++)
                    nums[i] = i + 1;

                IList<TreeNode>[,] memo = new List<TreeNode>[n, n];

                return BuildTree(nums, 0, n - 1, memo);
            }

            private static IList<TreeNode> BuildTree(int[] nums, int l, int r, IList<TreeNode>[,] memo)
            {
                int size = r - l + 1;

                if (size == 0)
                {
                    return new List<TreeNode>();
                }
                else if (size == 1)
                {
                    IList<TreeNode> tempLst = new List<TreeNode>();
                    tempLst.Add(new TreeNode(nums[l]));
                    return tempLst;
                }
                if (size == 2)
                {
                    IList<TreeNode> tempLst = new List<TreeNode>();

                    //2 tree possible - (2)\ 1   or  2 /(1)
                    TreeNode temp = new TreeNode(nums[l]);
                    temp.right = new TreeNode(nums[r]);
                    tempLst.Add(temp);

                    temp = new TreeNode(nums[r]);
                    temp.left = new TreeNode(nums[l]);
                    tempLst.Add(temp);

                    return tempLst;
                }

                if (memo[l, r] != null)
                    return (memo[l, r]);

                List<TreeNode> treeList = new List<TreeNode>();
                for (int i = l; i <= r; i++)
                {
                    //keep "i" at root and get left and right unique BST and multiply them 
                    TreeNode newHead = new TreeNode(i + 1);

                    IList<TreeNode> leftTrees = BuildTree(nums, l, i - 1, memo);
                    IList<TreeNode> rightTrees = BuildTree(nums, i + 1, r, memo);

                    if (leftTrees.Count == 0)
                    {
                        foreach (var right in rightTrees)
                        {
                            newHead.right = right;
                            treeList.Add(Clone(newHead));
                        }
                    }
                    else
                    {
                        foreach (var left in leftTrees)
                        {
                            newHead.left = left;
                            if (rightTrees.Count == 0)
                                treeList.Add(Clone(newHead));
                            else
                            {
                                foreach (var right in rightTrees)
                                {
                                    newHead.right = right;
                                    treeList.Add(Clone(newHead));
                                }
                            }
                        }
                    }
                }

                return memo[l, r] = treeList;
            }

            private static TreeNode Clone(TreeNode head)
            {
                if (head == null)
                    return null;

                TreeNode newHead = new TreeNode(head.val);
                newHead.left = Clone(head.left);
                newHead.right = Clone(head.right);

                return newHead;
            }
        }

        public class ClosestCost1
        {
            public static int ClosestCost(int[] baseCosts, int[] toppingCosts, int target)
            {
                //return MaxToppingPrice(baseCosts, toppingCosts, 0, target, 0);

                int closetDesertPrice = int.MaxValue;
                for (int i = 0; i < baseCosts.Length; i++)
                {
                    int maxPriceSoFar = MaxToppingPrice(toppingCosts, baseCosts[i], target, 0);

                    if (Math.Abs(target - closetDesertPrice) > Math.Abs(target - maxPriceSoFar))
                        closetDesertPrice = maxPriceSoFar;
                    else if (Math.Abs(target - closetDesertPrice) == Math.Abs(target - maxPriceSoFar))
                        closetDesertPrice = Math.Min(closetDesertPrice, maxPriceSoFar);
                }
                return closetDesertPrice;
            }

            private static int MaxToppingPrice(int[] toppingCosts, int sum, int target, int idx)
            {
                if (sum >= target)
                    return sum;

                int maxTopPrice = sum;
                for (int j = idx; j < toppingCosts.Length; j++)
                {
                    int k = 1;
                    while (k <= 2)
                    {
                        int innerMaxToppingPrice = MaxToppingPrice(toppingCosts, sum + (toppingCosts[j] * k), target, j + 1);

                        if (Math.Abs(target - innerMaxToppingPrice) < Math.Abs(target - maxTopPrice))
                            maxTopPrice = innerMaxToppingPrice;
                        else if (Math.Abs(target - innerMaxToppingPrice) == Math.Abs(target - maxTopPrice))
                            maxTopPrice = Math.Min(maxTopPrice, innerMaxToppingPrice);

                        k++;
                    }
                }
                return maxTopPrice;
            }
        }


        public class GetCollisionTimes
        {
            class Pair : IComparable<Pair>
            {
                public int Index;
                public int Pos;
                public int Speed;
                public Pair(int idx, int pos, int speed)
                {
                    Index = idx;
                    Pos = pos;
                    Speed = speed;
                }

                public int CompareTo(Pair o)
                {
                    if (this.Index == o.Index)
                        return 0;
                    else
                    {
                        if (this.Pos == o.Pos)
                            return this.Speed.CompareTo(o.Speed);
                        else
                            return this.Pos.CompareTo(o.Pos);
                    }
                }
            }

            public static double[] GetCollisionTimes1(int[][] cars)
            {
                double[] result = new double[cars.Length];

                SortedSet<Pair> minQ = new SortedSet<Pair>();
                for (int i = 0; i < cars.Length; i++)
                    minQ.Add(new Pair(i, cars[i][0] + cars[i][1], cars[i][1]));

                double clock = 1;
                while (true)
                {
                    Dictionary<int, Pair> collions = new Dictionary<int, Pair>();
                    int minFleetSpeed = int.MaxValue;
                    Pair min = null;
                    foreach (Pair p in minQ)
                    {
                        if (min == null)
                            min = p;
                        else
                        {
                            if (min.Pos == p.Pos) //collision
                            {
                                if (!collions.ContainsKey(p.Index))
                                    collions.Add(p.Index, p);
                                if (!collions.ContainsKey(min.Index))
                                    collions.Add(min.Index, min);

                                minFleetSpeed = Math.Min(minFleetSpeed, p.Speed);
                                minFleetSpeed = Math.Min(minFleetSpeed, min.Speed);
                            }
                        }
                    }

                    if (collions.Count > 0)
                    {
                        //merge
                        //minFleetSpeed
                    }

                    //SortedDictionary<char, int> dict = new SortedDictionary<char, int>(new SortByValue<char, int>());

                }

                return result;
            }

        }

        public static IList<int> FindDuplicates(int[] nums)
        {
            IList<int> duplicates = new List<int>();
            //cycle sort -  O(N)
            int start = 0;
            while (start < nums.Length)
            {
                if (nums[start] != start + 1)
                {
                    int temp = nums[nums[start] - 1];
                    if (temp == nums[start])
                    {
                        start++;
                        continue;
                    }
                    nums[nums[start] - 1] = nums[start];
                    nums[start] = temp;
                }
                else
                    start++;
            }

            for (int i = 0; i < nums.Length; i++)
                if (nums[i] != i + 1)
                    duplicates.Add(nums[i]);

            return duplicates;
        }

        public static IList<IList<string>> PalindromePartition(string s)
        {

            //Understand the matrix
            //https://www.youtube.com/watch?v=XmSOWnL6T_I

            IList<IList<string>> palindromes = new List<IList<string>>();
            int n = s.Length, count = 0;

            int[][] memo1 = new int[n][];
            for (int i = 0; i < n; i++)
            {
                memo1[i] = new int[n];
                memo1[i][i] = 1;
                count++;
                //palindromes.Add(new string(s[i], 1));

                if (palindromes.Count == 0) palindromes.Add(new List<string>());
                palindromes[palindromes.Count - 1].Add(new string(s[i], 1));
            }

            int top = 0, bottom = 0;

            //start from 2 Length palindromes
            for (int len = 1; len < n; len++)
            {
                for (int i = 0; i < n - len; i++)
                {
                    int j = i + len;
                    if (s[i] == s[j] && memo1[i + 1][j - 1] == j - i - 1)
                    {
                        memo1[i][j] = 2 + memo1[i + 1][j - 1];
                        count++;

                        palindromes.Add(new List<string>());

                        top = i - 1;
                        while (top >= 0)
                        {
                            if (memo1[top][top] > 0)
                            {
                                int left = (top - memo1[top][top] + 1);
                                palindromes[palindromes.Count - 1].Add(s.Substring(left, memo1[top][top]));
                            }
                            top--;
                        }

                        palindromes[palindromes.Count - 1].Add(s.Substring(i, j - i + 1));
                        //palindromes.Add(s.Substring(i, j - i + 1));

                        bottom = j + 1;
                        while (bottom < n)
                        {
                            if (memo1[bottom][bottom] > 0)
                            {
                                int left = (bottom - memo1[bottom][bottom] + 1);
                                palindromes[palindromes.Count - 1].Add(s.Substring(left, memo1[bottom][bottom]));
                            }
                            bottom++;
                        }
                    }
                    else
                    {
                        memo1[i][j] = 0;
                    }
                }
            }

            for (int c = 1; c < n; c++)
            {
                //sliding window
                int l = 0, r = n - c - 1, c1 = c, c2 = 1;
                IList<Tuple<int, int>> leftCount = new List<Tuple<int, int>>();
                IList<Tuple<int, int>> rightCount = new List<Tuple<int, int>>();
                while (l <= r)
                {
                    int sizeL = memo1[l][c1];
                    if (sizeL > 0)
                    {
                        leftCount.Add(new Tuple<int, int>(c1 - sizeL + 1, sizeL));
                        if (leftCount.Count > 1)
                        {
                            palindromes.Add(new List<string>());
                            palindromes[palindromes.Count - 1].Add(s.Substring(l, memo1[top][top]));
                        }
                    }

                    int sizeR = memo1[r][n - c2];
                    if (sizeR > 0)
                    {
                        rightCount.Add(new Tuple<int, int>(n - c2 - sizeR + 1, sizeR));
                        if (rightCount.Count > 1)
                        {

                        }
                    }
                    c1++;
                    c2++;
                    l++;
                    r--;
                }
            }

            return palindromes;
        }

        static long DFS(int[] nums, int idx, long[] dp)
        {
            long MOD = 1000000007;
            if (dp[idx] != 0)
                return dp[idx];

            long treeCount = 1; //each elemnet could be tree itself
            for (int j = 0; j < nums.Length; j++)
            {
                if (nums[idx] % nums[j] == 0)
                {
                    int right = nums[idx] / nums[j];
                    for (int k = 0; k < nums.Length; k++)
                    {
                        if (nums[k] == right)
                        {
                            treeCount += (DFS(nums, j, dp) * DFS(nums, k, dp)) % MOD;
                            break;
                        }
                    }
                }
            }
            return dp[idx] = treeCount;
        }


        public class PalindromeTrie
        {
            class TrieNode
            {
                internal TrieNode[] children = new TrieNode[26];
                internal int wordIndex = -1;
                internal List<int> restIsPalindrome;
                internal char C;
                internal TrieNode(char c1)
                {
                    C = c1;
                    restIsPalindrome = new List<int>();
                }
            }

            TrieNode root = new TrieNode(' ');
            int n;
            IList<IList<int>> res = new List<IList<int>>();

            public IList<IList<int>> PalindromePairs(string[] words)
            {
                n = words.Length;

                for (int i = 0; i < n; i++)
                    Add(words[i], i);

                for (int i = 0; i < n; i++)
                    Search(words[i], i);

                return res;
            }

            private void Search(string word, int wordIndex)
            {
                TrieNode cur = root;
                char[] chs = word.ToCharArray();
                for (int i = 0; i < chs.Length; i++)
                {
                    int j = chs[i] - 'a';
                    if (cur.wordIndex != -1 && isPalindrome(chs, i, chs.Length - 1))
                    {
                        res.Add(new int[] { wordIndex, cur.wordIndex }.ToList());
                    }
                    if (cur.children[j] == null)
                        return;
                    cur = cur.children[j];
                }

                // aaaa
                if (cur.wordIndex != -1 && cur.wordIndex != wordIndex)
                {
                    res.Add(new int[] { wordIndex, cur.wordIndex }.ToList());
                }

                foreach (int j in cur.restIsPalindrome)
                {
                    res.Add(new int[] { wordIndex, j }.ToList());
                }
            }

            private void Add(string word, int wordIndex)
            {
                TrieNode cur = root;
                char[] chs = word.ToCharArray();
                for (int i = chs.Length - 1; i >= 0; i--)
                {
                    int j = chs[i] - 'a';
                    if (isPalindrome(chs, 0, i))
                        cur.restIsPalindrome.Add(wordIndex);

                    if (cur.children[j] == null)
                        cur.children[j] = new TrieNode(chs[i]);

                    cur = cur.children[j];
                }

                cur.wordIndex = wordIndex;
            }

            private bool isPalindrome(char[] chs, int i, int j)
            {
                while (i < j)
                {
                    if (chs[i++] != chs[j--]) return false;
                }
                return true;
            }
        }

        private static int Descending(int i, int j)
        {
            return j.CompareTo(i);
        }

        class T : IComparable<int>
        {
            int IComparable<int>.CompareTo(int other)
            {
                throw new NotImplementedException();
            }
        }
        class T1 : IComparer<int>
        {
            int IComparer<int>.Compare(int x, int y)
            {
                throw new NotImplementedException();
            }
        }

        internal class Cell //: IComparable<Cell>
        {
            public int row { get { return _row; } }
            public int col { get { return _col; } }
            public int height { get { return _height; } }

            int _row = 0, _col = 0, _height = 0;
            public Cell(int r, int c, int h)
            {
                _row = r;
                _col = c;
                _height = h;
            }

            public int CompareTo(Cell other)
            {
                return this.height.CompareTo(other.height);
            }
        }

        private static int DescendingCell(Cell i, Cell j)
        {
            return j.height.CompareTo(i.height);
        }


        public static int solveKnapsack(int[] profits, int[] weights, int capacity)
        {
            // basic checks
            if (capacity <= 0 || profits.Length == 0 || weights.Length != profits.Length)
                return 0;

            int n = profits.Length;
            int[,] dp = new int[n + 1, capacity + 1];

            // populate the capacity=0 columns, with '0' capacity we have '0' profit
            for (int i = 0; i <= n; i++)
                dp[i, 0] = 0;

            // process all sub-arrays for all the capacities
            for (int i = 1; i <= n; i++)
            {
                for (int c = 1; c <= capacity; c++)
                {
                    int profit1 = 0, profit2 = 0;
                    // include the item, if it is not more than the capacity
                    if (weights[i - 1] <= c)
                        profit1 = profits[i - 1] + dp[i - 1, c - weights[i - 1]];
                    // exclude the item
                    profit2 = dp[i - 1, c];
                    // take maximum
                    dp[i, c] = Math.Max(profit1, profit2);
                }
            }

            int totalProfit = dp[weights.Length, capacity];
            for (int i = weights.Length; i >= 0; i--)
            {
                if (totalProfit != dp[i, capacity])
                {
                    //System.out.print(" " + weights[i]);
                    capacity -= weights[i];
                    totalProfit -= profits[i];
                }
            }


            // maximum profit will be at the bottom-right corner.
            return dp[n, capacity];
        }

        public static int MaxProfit(int[] prices)
        {

            //Video - https://www.youtube.com/watch?v=MyqDgMy-Kew
            //***************************************************
            // k = 2 transactions allowed... initially we don't hold the stock
            //***************************************************
            //We need to consider, if we hold a stock or not.. 

            //if we hold(1) stack, we can sell or hold/wait it
            //if we dont hold(0) stock, we can buy or wait
            //f(i,0,2) = max profit on ith day with 0 sock on hand with max 2 transactions
            //f(i,1,2) = max profit on ith day with 1 sock on hand with max 2 transactions
            //So....
            //**** To get 0 stock, we dont buy new OR we sold it whatever we had. 1 transaction is reduced *****
            //f(i,0,k) = Max (f(i - 1,0,k), f(i - 1, 1, k-1) + prices[i - 1]) 
            //**** To get 1 stock, we dont sell it OR we buy new *****
            //f(i,1,k) = Max (f(i - 1,1,k), f(i - 1, 0, k) - prices[i - 1])

            int k = 2; //k transactions
            int[,,] dp = new int[prices.Length + 1, 2, k + 1];

            //base case...
            for (int ki = 0; ki <= k; ki++)
                dp[0, 0, ki] = 0;            //First day, profit 0, with holding '0' stock on hand, 

            for (int ki = 0; ki <= k; ki++)
                dp[0, 1, ki] = int.MinValue; //First day, "not possible" to earn profit holding a stock

            for (int i = 1; i < dp.GetLength(0); i++)
            {
                for (int ki = 0; ki <= k; ki++)
                {
                    //sell or dont sell
                    if (ki > 0) //atleast 1 Transaction is allowed
                        dp[i, 0, ki] = Math.Max(dp[i - 1, 0, ki], dp[i - 1, 1, ki - 1] + prices[i - 1]);
                    else
                        dp[i, 0, ki] = Math.Max(dp[i - 1, 0, ki], 0); //no sell allowed with 0 Trans left

                    //buy
                    dp[i, 1, ki] = Math.Max(dp[i - 1, 1, ki], dp[i - 1, 0, ki] - prices[i - 1]);
                }
            }

            //max profit on last day, with 0 stock on hand....
            return dp[dp.GetLength(0) - 1, 0, k];
        }


        public int MaxProfit1(int[] prices)
        {
            //Many transactions allowed, but once we sell we have a cooldown... i.e we cant buy on next day. initially we don't hold the stock
            //We need to consider, if we hold a stock or not.. or we are on coolDown or not
            //cooldown matters once we sell something....

            //if we hold(1) stack, we can sell or hold/wait it. (cooldown doesnt matter)
            //if we dont hold(0) stock, we can buy (if ONLY we are not on COOLDOWN) or wait
            //f(i, !own, cooldown) = max profit on ith day with 0 sock on hand with cooldown
            //f(i, !own, !cooldown) = max profit on ith day with 0 sock on hand without cooldown

            //Cooldown doedn't matter here as we hold stock... cooldown decides when we can buy stock
            //f(i, own, cooldown) = max profit on ith day with 1 sock on hand with cooldown
            //f(i, own, !cooldown) = max profit on ith day with 1 sock on hand without cooldown

            //So....
            //**** To make 0 stock, we dont buy new OR we sold it whatever we had. *****
            //f(i,0, !cooldown) = Max (f(i - 1, 0, cooldown=true), f(i - 1, 1, cooldown=false) + prices[i - 1]) 
            //f(i,0, cooldown)  = Max (f(i - 1, 0, cooldown=true), f(i - 1, 1, cooldown=false) + prices[i - 1]) 


            //**** To make 1 stock, we dont sell it OR we buy new *****
            //f(i,1, !cooldown) = Max (f(i - 1, 1, cooldown=false), f(i - 1 / 2, 0, cooldown=false) - prices[i - 1] )
            //f(i,1, cooldown)  = Max (f(i - 1, 1, cooldown=false), 0) //with cooldown we cant buy stock so '0'

            int cool = 1;
            int noCool = 0;
            int[,,] dp = new int[prices.Length + 1, 2, 2];

            //base case...
            dp[0, 0, noCool] = 0;            //First day, profit 0, with holding '0' stock on hand
            dp[0, 0, cool] = 0;
            dp[0, 1, noCool] = int.MinValue; //First day, "not possible" to earn profit holding a stock 
            dp[0, 1, cool] = int.MinValue;

            for (int i = 1; i < dp.GetLength(0); i++)
            {
                //selling choices
                dp[i, 0, noCool] = Math.Max(dp[i - 1, 0, cool], dp[i - 1, 1, noCool] + prices[i - 1]);
                dp[i, 0, cool] = Math.Max(dp[i - 1, 0, cool], dp[i - 1, 1, noCool] + prices[i - 1]);

                //buy choices
                if (i >= 2) //sell after cooling i.e (i - 2)
                    dp[i, 1, noCool] = Math.Max(dp[i - 1, 1, noCool], dp[i - 2, 0, noCool] - prices[i - 1]);
                else
                    dp[i, 1, noCool] = Math.Max(dp[i - 1, 1, noCool], dp[i - 1, 0, noCool] - prices[i - 1]);

                dp[i, 1, cool] = Math.Max(dp[i - 1, 1, noCool], 0); //no buying possile for cooldown         
            }

            return dp[dp.Length - 1, 0, 0];

        }


        public static int WaterArea(int[] heights)
        {

            if (heights.Length == 0)
                return 0;

            int[] NGL = new int[heights.Length];
            int[] NGR = new int[heights.Length];

            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();

            //find nearest greater on left side
            for (int i = 0; i < heights.Length; i++)
            {
                if (!stack.Any())
                    NGL[i] = 0;
                if (stack.Any() && stack.Peek().Item1 > heights[i])
                {
                    NGL[i] = stack.Peek().Item2;
                }
                else if (stack.Any() && stack.Peek().Item1 <= heights[i])
                {
                    while (stack.Any() && stack.Peek().Item1 <= heights[i])
                        stack.Pop();

                    if (!stack.Any())
                        NGL[i] = 0;
                    else
                        NGL[i] = stack.Peek().Item2;
                }
                stack.Push(new Tuple<int, int>(heights[i], i));
            }

            stack.Clear();

            //find nearest greater on right side
            for (int i = heights.Length - 1; i >= 0; i--)
            {
                if (!stack.Any())
                    NGR[i] = heights.Length - 1;
                if (stack.Any() && stack.Peek().Item1 > heights[i])
                {
                    NGR[i] = stack.Peek().Item2;
                }
                else if (stack.Any() && stack.Peek().Item1 <= heights[i])
                {
                    while (stack.Any() && stack.Peek().Item1 <= heights[i])
                        stack.Pop();

                    if (!stack.Any())
                        NGR[i] = heights.Length - 1;
                    else
                        NGR[i] = stack.Peek().Item2;
                }
                stack.Push(new Tuple<int, int>(heights[i], i));
            }

            int waterCount = 0;
            for (int i = 0; i < NGR.Length; i++)
            {
                int minHeight = Math.Min(heights[NGL[i]], heights[NGR[i]]);
                //Console.WriteLine(minIndex);

                if (minHeight - heights[i] > 0)
                    waterCount += minHeight - heights[i];
            }
            return waterCount;
        }

        private static int AscendingMeetingEndTime(int[] interval1, int[] interval2)
        {
            if (interval1.Equals(interval2))
                return 0;
            else
            {
                if (interval1[1].CompareTo(interval2[1]) == 0)
                    return 1;
                else
                    return interval1[1].CompareTo(interval2[1]);
            }
        }

        public static string Decode(string str)
        {
            //str = "3[a2[c]]";
            Stack<int> repeat = new Stack<int>();
            Stack<string> sub_string = new Stack<string>();

            //bool collectRepeat = true;
            //bool collectSubStr = false;
            int r = 0;
            while (r < str.Length)
            {
                int n = 0;
                while (r < str.Length && char.IsDigit(str[r]))
                {
                    n += (n * 10) + str[r] - '0';
                    r++;
                }
                if (n > 0)
                    repeat.Push(n);

                if (str[r] == '[') //collect caracters to be repeated
                {
                    r++;
                    int l = r;
                    while (r < str.Length && char.IsLetter(str[r]))
                        r++;

                    if (r > l)
                    {
                        string subStr = str.Substring(l, r - l);
                        sub_string.Push(subStr);
                    }
                }
                else if (str[r] == ']' && sub_string.Any() && repeat.Any()) //actual repeat process
                {
                    string subStr = string.Concat(Enumerable.Repeat(sub_string.Pop(), repeat.Pop()));
                    //push the repeated string back to sub_string 'stack'
                    if (sub_string.Any())
                        subStr = sub_string.Pop() + subStr;

                    sub_string.Push(subStr);
                    r++;
                }
                else //any alphabets after ] till next digit
                {
                    int l = r;
                    while (r < str.Length && !char.IsDigit(str[r]))
                        r++;

                    if (r > l)
                    {
                        string subStr = str.Substring(l, r - l);
                        sub_string.Push(sub_string.Pop() + subStr);
                    }
                }
            }
            return sub_string.Pop();
        }


        public class Pair
        {
            public int elIndex = 0, LstIndex = 0;
            public Pair(int elIdx, int aidx)
            {
                elIndex = elIdx;
                LstIndex = aidx;
            }
        }

        static List<int[]> GlobalLists;

        public static int AscendingSorter(Pair x, Pair y)
        {
            // Two nodes are equal only when their index are same 
            if (x.LstIndex == y.LstIndex)
                return 0;
            else
            {
                if (GlobalLists[x.LstIndex][x.elIndex] == GlobalLists[y.LstIndex][y.elIndex])
                    return -1;// x.LstIndex.CompareTo(y.LstIndex);
                else
                    return GlobalLists[x.LstIndex][x.elIndex].CompareTo(GlobalLists[y.LstIndex][y.elIndex]);
            }
        }


        public static IList<string> MostVisitedPattern(string[] username, int[] timestamp, string[] website)
        {
            Dictionary<string, List<(int, string)>> users = new Dictionary<string, List<(int, string)>>();  // Maps a username to a list of accesses (timestamp, website)-pairs.

            for (int i = 0; i < username.Length; i++)
            {
                if (!users.ContainsKey(username[i]))
                    users.Add(username[i], new List<(int, string)>());
                users[username[i]].Add((timestamp[i], website[i]));
            }

            foreach (List<(int, string)> accesses in users.Values)
            {
                accesses.Sort((a, b) => { return a.Item1.CompareTo(b.Item1); });   // Sort accesses of each user by timestamp (ascending order).
            }

            List<string> result = null;
            int resOccurrences = -1;
            string resKey = null;
            Dictionary<string, int> threeSequences = new Dictionary<string, int>();          // Maps a 3-sequence to the number of users that have it.

            foreach (string user in users.Keys)
            {
                List<(int, string)> accesses = users[user];
                HashSet<string> seen = new HashSet<string>();
                for (int i = 0; i < accesses.Count - 2; i++)
                {
                    for (int j = i + 1; j < accesses.Count - 1; j++)
                    {
                        for (int k = j + 1; k < accesses.Count; k++)
                        {
                            string key = accesses[i].Item2 + "-" + accesses[j].Item2 + "-" + accesses[k].Item2;
                            if (seen.Contains(key))
                                continue;

                            if (!threeSequences.ContainsKey(key))
                            {
                                threeSequences.Add(key, 0);
                                seen.Add(key);
                            }
                            threeSequences[key]++;

                            if (resOccurrences <= threeSequences[key] && key.CompareTo(resKey) < 0)//.CompareTo(key) > 0)
                            {
                                result = new string[] { accesses[i].Item2, accesses[j].Item2, accesses[k].Item2 }.ToList();
                                resOccurrences = threeSequences[key];
                                resKey = key;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static int Calculate(string s)
        {

            if (s == null || s.Length == 0)
                return 0;
            Stack<int> nums = new Stack<int>(); // the stack that stores numbers
            Stack<char> ops = new Stack<char>(); // the stack that stores operators (including parentheses)

            //To handle group starts with +/- we will insert 0 in the number 
            bool isNewGorup = true; //expression start is a group start
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == ' ')
                    continue;

                if (char.IsDigit(c))
                {
                    int num = 0;
                    while (i < s.Length && char.IsDigit(s[i]))
                    {
                        num = num * 10 + (s[i] - '0');
                        i++;
                    }
                    i--;

                    nums.Push(num);
                    isNewGorup = false;
                }
                else if (c == '(')
                {
                    ops.Push(c);

                    // ( is also a group start
                    isNewGorup = true;
                }
                else if (c == ')')
                {
                    // do the math when we encounter a ')' until '('
                    while (ops.Peek() != '(')
                        nums.Push(operation(ops.Pop(), nums.Pop(), nums.Pop()));

                    ops.Pop(); // get rid of '(' in the ops stack
                    isNewGorup = false;
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    //if stack has higher or equal priority OP, than the current OP, then do the higher priority OP calculation first and 
                    //store calculation result into stack.
                    while (ops.Any() && Priority(c) <= Priority(ops.Peek()))
                        nums.Push(operation(ops.Pop(), nums.Pop(), nums.Pop()));

                    //To handle group starts with +/- we will insert 0 in the number 
                    if (isNewGorup)
                        nums.Push(0);
                    isNewGorup = false;

                    //push the current OP
                    ops.Push(c);
                }
            }

            while (ops.Any())
            {
                nums.Push(operation(ops.Pop(), nums.Pop(), nums.Pop()));
            }
            return nums.Pop();
        }

        private static int operation(char op, int b, int a)
        {
            switch (op)
            {
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/': return a / b; // assume b is not 0
            }
            return 0;
        }

        private static int Priority(char op)
        {
            if (op == '(' || op == ')')
                return 1;
            else if (op == '+' || op == '-')
                return 2;
            else if (op == '*' || op == '/')
                return 3;
            else
                return -1;
        }

        private static void DFS(string str, int idx, List<char> slate)
        {
            if (idx == str.Length)
            {
                Console.WriteLine(new string(slate.ToArray()));
                return;
            }

            DFS(str, idx + 1, slate);

            slate.Add(str[idx]);
            DFS(str, idx + 1, slate);
            slate.RemoveAt(slate.Count - 1);
        }

        public static int NumTreesBottomUp(int n)
        {
            //dp[i] represents, number of structuraly unique BST for i nodes
            int[] dp = new int[n + 1];
            dp[0] = 1; //number of unique BST with 0 node is 1, i,e empty BST
            dp[1] = 1; //number of unique BST with 1 node is 1, i,e node itself

            int l = 0, r = 0;
            for (int i = 2; i <= n; i++)
            {
                l = 0;
                r = i - 1;
                while (l <= i - 1)
                {
                    dp[i] += dp[l] * dp[r];

                    l++;
                    r--;
                }
            }
            /*for (int i = 2; i <= n; i++) 
            {
                for (int j = 1; j <= i; j++) 
                {
                    dp[i] += dp[j - 1] * dp[i - j];
                }
            }*/

            return dp[n];
        }

        private static int ConvertFromString(string str)
        {
            int result = 0;
            checked
            {
                for (int i = 0; i < str.Length; i++)
                    if (char.IsDigit(str[i]))
                        result = ((result * 10) + (str[i] - '0'));
                    else
                        throw new Exception("Invalid Input");
            }
            return result;
        }
        private static int ConvertFromBinaryString(string str)
        {
            int result = 0;
            int pow = 1;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(str[i]) && str[i] <= '1')
                    result += pow * (str[i] - '0');
                else
                    throw new Exception("Invalid Input");

                pow *= 2;
            }
            return result;
        }
        private static int ConvertFromHexaString(string str)
        {
            int result = 0;
            int pow = 1;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(str[i]))
                    result += pow * (str[i] - '0');
                else if (char.IsLetter(str[i]) && (str[i] <= 'F' || str[i] <= 'f'))
                    result += pow * (str[i] - (char.IsUpper(str[i]) ? 'A' : 'a') + 10);
                else
                    throw new Exception("Invalid Input");

                pow *= 16;
            }
            return result;
        }



        public class Pair1
        {
            public char value;
            public int index;
            public Pair1(int idx, char v)
            {
                index = idx;
                value = v;
            }
        }

        public static int DescendingSorter(Pair1 x, Pair1 y)
        {
            // Two nodes are equal only when their indices are same 
            if (x.Equals(y))
                return 0;
            else if (x.index == y.index)
                return -1; // or x.index.CompareTo(y.index);
            else
                return y.index.CompareTo(x.index);
        }

        public static string ReorganizeString(string str)
        {
            // find the frequency of each number
            Dictionary<char, int> numFrequencyMap = new Dictionary<char, int>();
            for (int i = 0; i < str.Length; i++)
            {
                if (!numFrequencyMap.ContainsKey(str[i]))
                    numFrequencyMap.Add(str[i], 0);

                numFrequencyMap[str[i]]++;
            }

            SortedSet<Pair1> maxHeap = new SortedSet<Pair1>(Comparer<Pair1>.Create(DescendingSorter));

            // add all characters to the max heap
            foreach (char key in numFrequencyMap.Keys)
            {
                maxHeap.Add(new Pair1(numFrequencyMap[key], key));
            }

            Queue<Pair1> queue = new Queue<Pair1>();
            Pair1 previousEntry = null;
            StringBuilder resultString = new StringBuilder();
            while (maxHeap.Any())
            {
                Pair1 currentEntry = maxHeap.First();
                maxHeap.Remove(maxHeap.First());

                // append the current character to the result string and decrement its count
                resultString.Append(currentEntry.value);
                currentEntry.index--;

                queue.Enqueue(currentEntry);

                /*
                // add the previous entry back in the heap if its frequency is greater than zero
                if (previousEntry != null && previousEntry.value > 0)
                    maxHeap.Add(previousEntry);

                previousEntry = currentEntry;*/

                //make sure that we have 2 distance between same letter
                if (queue.Count == 2)
                {
                    Pair1 entry = queue.Dequeue();
                    if (entry.index > 0)
                        maxHeap.Add(entry);
                }
            }

            //Console.WriteLine(resultString.ToString());

            // if we were successful in appending all the characters to the result string, return it
            return resultString.Length == str.Length ? resultString.ToString() : "";
        }

        public int NumberOfSteps(int num)
        {
            if (num < 0)
                return 0;

            int res = 0;
            while (num > 0)
            {
                res += ((num & 1) == 1 ? 2 : 1);
                num = num >> 1;  //divide it
            }
            return res - 1;
        }



        

        public static IList<IList<int>> FindSubsequences(int[] arr)
        {
            if (arr.Length == 0)
                return new List<IList<int>>();

            IList<IList<int>> result = new List<IList<int>>();

            //dp[i] stores the length of LIS ending at 'i'
            int[] dp = new int[arr.Length];

            //Longest LIS ending at 0th index is 1, it’s the number itself
            dp[0] = 1;

            result.Add(new List<int>() { arr[0], arr[0] });
            result.Add(new List<int>() { arr[0], arr[0] });

            int omax = 1;
            for (int i = 1; i < arr.Length; i++)
            {
                //find max LIS among all previous 'i', which are smaller than current value
                int max = 0;
                for (int j = 0; j < i; j++)
                {
                    if (arr[j] <= arr[i])  //must be smaller than current value
                    {
                        max = Math.Max(max, dp[j]); //pick the maximum LIS value
                        var x = result[j];
                        /*foreach (List<int> lst1 in result[j])
                        {
                            IList<int> local = new List<int>(lst1.ToArray());
                            local.Add(arr[i]);
                            result.Add(local);
                        }*/
                    }
                }
                dp[i] = max + 1;

                //omax = Math.Max(omax, dp[i]);
            }

            Console.WriteLine(result.Count);

            return result;
        }

        public static Result MaxSumSubmatrix(int[][] matrix, int k)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            int[] temp = new int[rows];
            Result result = new Result();
            
            for (int left = 0; left < cols; left++)
            {
                //Array.Fill(temp, 0);
                for (int i = 0; i < rows; i++)
                    temp[i] = 0;
                
                for (int right = left; right < cols; right++)
                {
                    for (int i = 0; i < rows; i++)
                        temp[i] += matrix[i][right];

                    KadaneResult kadaneResult = kadane(temp, k);

                    if(kadaneResult.maxSum > result.maxSum)
                    {
                        result.maxSum = kadaneResult.maxSum;
                        result.leftBound = left;
                        result.rightBound = right;
                        result.upBound = kadaneResult.start;
                        result.lowBound = kadaneResult.end;
                    }
                }
            }
            return result;
        }

        private static KadaneResult kadane(int[] arr, int k)
        {
            int max = 0;
            int maxStart = -1;
            int maxEnd = -1;
            int currentStart = 0;
            int maxSoFar = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                maxSoFar += arr[i];
                if (maxSoFar < 0)
                {
                    maxSoFar = 0;
                    currentStart = i + 1;
                }
                if (max < maxSoFar)
                {
                    maxStart = currentStart;
                    maxEnd = i;
                    max = maxSoFar;
                }
            }
            return new KadaneResult(max, maxStart, maxEnd);
        }

        public class Result
        {
            public int maxSum;
            public int leftBound;
            public int rightBound;
            public int upBound;
            public int lowBound;
        }
        public class KadaneResult
        {
            public int maxSum;
            public int start;
            public int end;
            public KadaneResult(int maxSum, int start, int end)
            {
                this.maxSum = maxSum;
                this.start = start;
                this.end = end;
            }
        }

        static void maxProductSubsequence(int[] nums, int n)
        {
            // Your code here
            int[] min = new int[n];
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                while (stack.Any() && stack.Peek() >= nums[i])
                    stack.Pop();

                min[i] = !stack.Any() ? nums[0] : stack.Peek();
                stack.Push(nums[i]);
            }
            stack.Clear();

            int maxProd = nums[0]; 
            for (int i = n - 1; i >= 0; i--)
            {
                //middle is greater than left
                if (nums[i] > min[i])
                {
                    //ensures right/stack has bigger element than left
                    while (stack.Any() && stack.Peek() <= nums[i])
                        stack.Pop();

                    //stack has descendent values, Middle is greater than right
                    if (stack.Any())
                        maxProd = Math.Max(maxProd, nums[i] * min[i] * stack.Peek());

                    stack.Push(nums[i]);
                }
            }
        }

        public static int[] maxOfMin(int[] arr, int n)
        {
            Stack<int> stack = new Stack<int>();
            int[] left = new int[n];
            //find the NSL
            for (int i = 0; i < n; i++)
            {
                while (stack.Any() && arr[stack.Peek()] >= arr[i])
                    stack.Pop();

                left[i] = (!stack.Any()) ? -1 : stack.Peek();
                stack.Push(i);
            }
            //find the NSR
            stack.Clear();
            int[] right = new int[n];
            for (int i = n - 1; i >= 0; i--)
            {
                while (stack.Any() && arr[stack.Peek()] >= arr[i])
                    stack.Pop();

                right[i] = (!stack.Any()) ? n : stack.Peek();
                stack.Push(i);
            }

            int[] ans = new int[n];
            for (int i = 0; i < n; i++)
            {
                // length of the interval
                int len = (right[i] - left[i] - 1) - 1;

                // arr[i] is a possible answer for this length len interval
                ans[len] = Math.Max(ans[len], arr[i]);
            }

            for (int i=n-2; i>=0; i--)
                ans[i] = Math.Max(ans[i], ans[i+1]);

            return ans;
        }

        static int gmin = int.MaxValue;
        public static int MinimumTimeRequired(int[] jobs, int k)
        {
            DFS(jobs, k, new bool[jobs.Length], 0);
            return gmin;
        }
        private static int DFS(int[] jobs, int k, bool[] scheduled, int idx)
        {
            //completed filling up k-1 bucket with targetSubsetSum
            //only 1 bucket left, this buckt will also be filled up by rest of items 
            if (k == 1)
                return Sum(jobs, idx, jobs.Length - 1);

            int lmin = int.MaxValue;
            int sum = 0;
            for (int i = idx; i < (jobs.Length - idx - k + 1); i++)
            {
                if (scheduled[i])
                    continue;

                sum += jobs[i];
                scheduled[i] = true;  //choose

                int tempMin = DFS(jobs, k-1, scheduled, 0);

                lmin = Math.Min(lmin, Math.Max(sum, tempMin));
                gmin = Math.Min(gmin, lmin);

                sum -= jobs[i];
                scheduled[i] = false; //unchoose
            }
            return lmin;
        }

        static int Sum(int[] jobs, int st, int end)
        {
            int sum = 0;
            for (; st <= end; st++)
                sum += jobs[st];
            return sum;
        }

        static void combinationUtil(int[] arr, List<int> data,
        int start, int end,
        int index)
        {
            for (int j = 0; j < data.Count; j++)
                Console.Write(data[j] + " ");
            Console.WriteLine("");

            // replace index with all possible elements.
            for (int i = start; i < end; i++)
            {
                data.Add(arr[i]);

                combinationUtil(arr, data, i + 1, end, index + 1);

                data.RemoveAt(data.Count - 1);
            }
        }

        private static void SubSetSum(int[] nums, int start, int sum)
        {
            if (start == nums.Length)
            {
                Console.WriteLine(sum);
                return;
            }

            sum += nums[start];
            SubSetSum(nums, start + 1, sum);
            sum -= nums[start];

            SubSetSum(nums, start + 1, sum);
        }
        private static void SubSets(int[] nums, int start, IList<int> combination, IList<IList<int>> subsets)
        {
            if (start == nums.Length)
            {
                subsets.Add(new List<int>(combination));
                return;
            }

            // Incldue the current element in subset
            combination.Add(nums[start]);
            SubSets(nums, start + 1, combination, subsets);
            combination.RemoveAt(combination.Count - 1);

            // Don't include the current element to subset
            SubSets(nums, start + 1, combination, subsets);
        }

        private static void jobsdfs(int[] jobs, int pos, int[] sum)
        {
            if (pos == jobs.Length)
            {
                for (int i = 0; i < sum.Length; i++)
                    Console.Write(sum[i] + " ");

                Console.WriteLine();
                //res = Math.min(res, Arrays.stream(sum).max().getAsInt());
                return;
            }
            //if (Arrays.stream(sum).max().getAsInt() >= res) return;           //prune1
            for (int i = 0; i < sum.Length; i++)
            {
                

                //if (i > 0 && sum[i] == sum[i - 1]) continue;                  //prune2
                sum[i] += jobs[pos];
                jobsdfs(jobs, pos + 1, sum);
                sum[i] -= jobs[pos];

                jobsdfs(jobs, pos + 1, sum);

            }
        }


        //int gmin = int.MaxValue;
        public static void PartitionInKSubsets(int[] arr, int k)
        {
            IList<IList<int>> ans = new List<IList<int>>();
            //we need to Split array among K subsets, so we need K subsets, 
            //initially subsets are empty
            for (int i = 0; i < k; i++)
                ans.Add(new List<int>());
            
            DFS11(arr, 0, k, 0, ans);
        }

        private static void DFS11(int[] arr, int idxJ, int kSubSets, int nonEmptySetSoFar, IList<IList<int>> ans)
        {
            if (idxJ == arr.Length)
            {
                if (kSubSets == nonEmptySetSoFar)
                {
                    //Console.Write(sum + "->");
                    foreach (IList<int> set in ans)
                    {
                        Console.Write(string.Join("", set) + " ");
                    }
                    Console.WriteLine();
                }
                return;
            }

            for (int i = 0; i < ans.Count; i++)
            {
                if (ans[i].Count > 0)
                {
                    ans[i].Add(arr[idxJ]);
                    DFS11(arr, idxJ + 1, kSubSets, nonEmptySetSoFar, ans);
                    ans[i].RemoveAt(ans[i].Count - 1);
                }
                else
                {
                    ans[i].Add(arr[idxJ]);
                    DFS11(arr, idxJ + 1, kSubSets, nonEmptySetSoFar + 1, ans);
                    ans[i].RemoveAt(ans[i].Count - 1);
                    break;
                }
            }
        }

        private static void DFS1(int[] jobs, int idxJ, int kSubSets, int setSoFar, IList<int> ans, int sum)
        {
            if (idxJ == jobs.Length)
            {
                if (kSubSets == setSoFar)
                {
                    int max = int.MinValue;
                    Console.Write(sum +"->");
                    foreach (int setSum in ans)
                    {
                        Console.Write(setSum + " ");
                        max = Math.Max(max, setSum);
                    }
                    gmin = Math.Min(gmin, max);
                    Console.WriteLine();
                }
                return;
            }


            for (int i = 0; i < ans.Count; i++)
            {
                if (ans[i] > 0)
                {
                    ans[i] += jobs[idxJ];
                    DFS1(jobs, idxJ+1, kSubSets, setSoFar, ans, sum);
                    ans[i] -= jobs[idxJ];
                }
                else
                {
                    ans[i] += jobs[idxJ];
                    DFS1(jobs, idxJ+1, kSubSets, setSoFar + 1, ans, sum);
                    ans[i] -= jobs[idxJ];
                    break;
                }
            }
        }


        private static int MinWorkerRequired(int[] jobDurations, int maxTimeLimit)
        {
            int l = 1, r = jobDurations[0];
            for(int j=0; j< jobDurations.Length; j++)
            {
                r = Math.Max(r, jobDurations[j]);
            }

            int minWorker = -1;
            while(l <= r)
            {
                int mWrkr = l + (r - l) / 2;
                if(CanSchedule(jobDurations, mWrkr, maxTimeLimit))
                {
                    minWorker = mWrkr;
                    r = mWrkr - 1;
                }
                else
                    l = mWrkr + 1;
            }
            return minWorker;
        }


        public class Pair2
        {
            public int value;
            public int index;
            public Pair2(int idx, int v)
            {
                index = idx;
                value = v;
            }
        }
        private static int AscDurationComparer(Pair2 a, Pair2 b)
        {
            if (a.Equals(b))
                return 0;
            else if (a.value.Equals(b.value))
                return -1;
            else
                return a.value.CompareTo(b.value);
        }
        private static bool CanSchedule(int[] jobDurations, int midWrkr, int maxTimeLimit)
        {
            if (midWrkr == 0 && maxTimeLimit > 0)
                return false;

            SortedSet<Pair2> activeWorkers = new SortedSet<Pair2>(Comparer<Pair2>.Create(AscDurationComparer));
            //Add "midWrkr" workers with 0 time
            for (int i=0; i < midWrkr; i++)
                activeWorkers.Add(new Pair2(i, 0));

            for (int i = 0; i < jobDurations.Length; i++)
            {
                int runningDuration = jobDurations[i] + activeWorkers.First().value;
                if (runningDuration > maxTimeLimit)
                    return false;

                int idx = activeWorkers.First().index;
                activeWorkers.Remove(activeWorkers.First());
                activeWorkers.Add(new Pair2(idx, runningDuration));
            }
            return true;
        }

        public static int minWasteBfs(int cost, int[] bills)
        {
            Array.Sort(bills); //NLogN
            HashSet<int> visited = new HashSet<int>();
            SortedSet<int> queue = new SortedSet<int>(); //NLogN
            visited.Add(0);
            queue.Add(0);

            while (queue.Any())
            {
                int cur = queue.First();
                queue.Remove(queue.First());
                if (cur >= cost)
                {
                    return cur - cost;
                }

                foreach (int bill in bills)
                {
                    int newAmount = bill + cur;
                    if (!visited.Contains(newAmount))
                    {
                        visited.Add(newAmount);
                        queue.Add(newAmount);
                    }
                }
            }

            return int.MaxValue;
        }


        static void Permutation(int[] boxes, int cIndex, int totalIndex)
        {
            if(cIndex > totalIndex)
            {
                for (int b = 0; b < boxes.Length; b++)
                    Console.Write(boxes[b]);
                Console.WriteLine();
                return;
            }

            //Permutation, Items choices Box
            for(int b=0; b<boxes.Length; b++)
            {
                if (boxes[b] == 0) //If box is Empty, It can put itself
                {
                    boxes[b] = cIndex; //putting itself
                    //Every call will have 1 less than the previous choices
                    //Level1 = n, Level2 = n-1 , Level3 = n-2 , Level4 = n-3 , Leveln-r+1 = n-r
                    Permutation(boxes, cIndex + 1, totalIndex);                 
                    boxes[b] = 0;     //removing itself
                }
            }
        }

        static void Combination(int currentBox, int totalBox, int itemSelectionSoFar, int totalItemSelection, string answerSoFar)
        {
            if (currentBox > totalBox)
            {
                if(itemSelectionSoFar == totalItemSelection)
                    Console.WriteLine(answerSoFar);
                return;
            }

            //Combination, Boxes will decide if he wants to be put item or not
            Combination(currentBox + 1, totalBox, itemSelectionSoFar, totalItemSelection, answerSoFar + "-"); //box not selected
            Combination(currentBox + 1, totalBox, itemSelectionSoFar + 1, totalItemSelection, answerSoFar + "i"); //box selected
        }

        static void CombinationToPermutation(int currentBox, int totalBox, int[] itemsUsed, int itemSelectionSoFar, int totalItemSelection, string answerSoFar)
        {
            if (currentBox > totalBox)
            {
                if (itemSelectionSoFar == totalItemSelection)
                    Console.WriteLine(answerSoFar);
                return;
            }

            //Combination, Boxes will decide if he wants to be put item or not
            CombinationToPermutation(currentBox + 1, totalBox, itemsUsed, itemSelectionSoFar, totalItemSelection, answerSoFar + "0"); //box will not hold item

            //box hold all items one by one
            for (int i = 0; i < itemsUsed.Length; i++)
            {
                if (itemsUsed[i] == 0) //item not used
                {
                    itemsUsed[i] = 1; //Use this item
                    CombinationToPermutation(currentBox + 1, totalBox, itemsUsed, itemSelectionSoFar + 1, totalItemSelection, answerSoFar + (i + 1));
                    itemsUsed[i] = 0; //Return it
                }
            }
        }


        static int BrushCount(int[] buildings)
        {
            int brushCount = 0;
            int prevHeight = 0;
            for (int i = 0; i < buildings.Length; i++)
            {
                if (buildings[i] > prevHeight)
                    brushCount += (buildings[i] - prevHeight);

                //Always capture the latest height, this helps to detect if we need to start a new Store in future
                prevHeight = buildings[i];
            }
            return brushCount;
        }


        static int StringToInt(string str)
        {
            int v = 0;
            int m = 10;
            for (int i = 0; i < str.Length; i++)
                v = v * m + str[i] - '0';
            //return v;

            v = 0;
            m = 1;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                v = (str[i] - '0') * m + v;
                m = m * 10;
            }
            return v;
        }

        static void PermutationToCombination(int[] boxes, int itemIdx, int totalItemIdx, int lastBoxIndex)
        {
            if (itemIdx > totalItemIdx)
            {
                for (int b = 0; b < boxes.Length; b++)
                    Console.Write(boxes[b]);
                Console.WriteLine();
                return;
            }

            //An Item choose Box one by one and place itself
            //recersively calls for next Item to choose from n-1 boxes
            for (int b = lastBoxIndex + 1; b < boxes.Length; b++)
            {
                if (boxes[b] == 0) //If box is Empty, It can put itself
                {
                    boxes[b] = itemIdx; //item putting itself

                    //In every level, there will be 1 less boxes than the previous level
                    //Level1 = n, Level2 = n-1 , Level3 = n-2 , Level4 = n-3 , Leveln-r+1 = n-r
                    PermutationToCombination(boxes, itemIdx + 1, totalItemIdx, b);

                    boxes[b] = 0;     //item removing itself
                }
            }
        }


        public static int NextGreaterElement(int n)
        {

            //Count digits in number
            int digits = (int)Math.Floor(Math.Log10(Math.Abs(n))) + 1;
            //convert number to int array
            int[] nums = new int[digits];
            int i = digits - 1;
            while (n > 0)
            {
                nums[i--] = n % 10;
                n = n / 10;
            }

            //From Right side, find the index(l) after which items are in Descending Order
            int l = nums.Length - 2;
            for (; l >= 0; l--)
                if (nums[l] < nums[l + 1])
                    break;

            //if Whole array is descending 5320, next Bigger number is not possible
            if (l == -1)
            {
                return -1;
            }

            //right side after "l", side whole is descending
            //Pick the first item[smlIdx], whose value is bigger than nums[l]
            int smlIdx = nums.Length - 1;
            for (; smlIdx > l; smlIdx--)
                if (nums[l] < nums[smlIdx])
                    break;

            //Swap - the values of l with smlIdx
            int temp = nums[l];
            nums[l] = nums[smlIdx];
            nums[smlIdx] = temp;

            //Reverse the right side of "l"
            l++;
            int r = nums.Length - 1;
            while (l <= r)
            {
                temp = nums[l];
                nums[l] = nums[r];
                nums[r] = temp;
                l++;
                r--;
            }

            //convert array back to integer
            long res = 0;
            long dec = 1;
            for (i = digits - 1; i >= 0; i--)
            {
                res = nums[i] * dec + res;
                dec = dec * 10;
            }
            return res >= int.MaxValue ? -1 : (int)res;
        }

        public static int findNext(int n)
        {
            //Count digits in number
            int digits = (int)Math.Floor(Math.Log10(Math.Abs(n))) + 1;
            //convert number to int array
            int[] nums = new int[digits];
            int i = digits - 1;
            while (n > 0)
            {
                nums[i--] = n % 10;
                n = n / 10;
            }

            //From Right side, find the index(l) after which items are in Descending Order
            int l = nums.Length - 2;
            for (; l >= 0; l--)
                if (nums[l] < nums[l + 1])
                    break;

            //if Whole array is descending 5320, next Bigger number is not possible
            if (l == -1)
            {
                return -1;
            }

            //right side after "l", side whole is descending
            //Pick the first item[smlIdx], whose value is bigger than nums[l]
            int smlIdx = nums.Length - 1;
            for (; smlIdx > l; smlIdx--)
                if (nums[l] < nums[smlIdx])
                    break;

            //Swap - the values of l with smlIdx
            int temp = nums[l];
            nums[l] = nums[smlIdx];
            nums[smlIdx] = temp;

            //Reverse the right side of "l"
            l++;
            int r = nums.Length - 1;
            while (l <= r)
            {
                temp = nums[l];
                nums[l] = nums[r];
                nums[r] = temp;
                l++;
                r--;
            }

            //convert array back to integer
            int res = 0;
            int dec = 1;
            for (i = digits-1; i >=0; i--)
            {
                res = nums[i] * dec + res;
                dec = dec * 10;
            }
            return res;
        }

        public static string NextPalindrome(string num)
        {
            //The String is palindrome, we just need to think about the first half part.
            //The next smallest bigger number is the next permutation of the "first half"
            int len = num.Length;

            int[] arr = new int[len / 2];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = num[i] - '0';

            if (!NextPermutation(arr))
                return "";

            StringBuilder sb = new StringBuilder();
            foreach (int item in arr)
                sb.Append(item);

            if (len % 2 == 1)
                sb.Append(num.Substring(len / 2, 1));

            for (int i = arr.Length - 1; i >= 0; i--)
                sb.Append(arr[i]);

            return sb.ToString();

            /*if(len % 2 == 0) 
                return sb.ToString() + sb.ToString().Reverse();
            else 
                return sb.ToString() + num.Substring(len / 2, len / 2 + 1) + sb.ToString().Reverse();*/
        }

        public static  bool NextPermutation(int[] nums)
        {
            //From Right side, find the index(l) after which items are in Descending Order
            int l = nums.Length - 2;
            for (; l >= 0; l--)
                if (nums[l] < nums[l + 1])
                    break;

            //if Whole array is descending 5320, so l =-1
            //Next permuation would be 0235, so rever the array and return
            if (l == -1)
            {
                Array.Reverse(nums, 0, nums.Length);
                return false;
            }

            //right side after "l", side whole is descending
            //Pick the first item[smlIdx], whose value is bigger than nums[l]
            int smlIdx = nums.Length - 1;
            for (; smlIdx > l; smlIdx--)
                if (nums[l] < nums[smlIdx])
                    break;

            //Swap - the values of l with smlIdx
            int temp = nums[l];
            nums[l] = nums[smlIdx];
            nums[smlIdx] = temp;

            //Reverse the right side of "l"
            l++;
            int r = nums.Length - 1;
            while (l <= r)
            {
                temp = nums[l];
                nums[l] = nums[r];
                nums[r] = temp;
                l++;
                r--;
            }

            return true;
        }

        public static int SuperEggDrop(int e, int f)
        {
            int lo = 0, hi = f;  //max number of throws required is total floor(f)
            int minThrows = 0;
            while (lo <= hi)
            {
                int midThrow = lo + ((hi - lo) / 2);
                if (CanMidThrowsCoverAllFloors(e, f, midThrow))
                {
                    minThrows = midThrow;
                    hi = midThrow - 1;
                }
                else
                    lo = midThrow + 1;
            }
            return minThrows;
        }

//        https://www.youtube.com/watch?v=xsOCvSiSrSs
// Find sum of binomial coefficients
// maxDrops(C)e (where e varies from 1 to e).
//We will check with maxDrops if we can reach "f" floors
private static bool CanMidThrowsCoverAllFloors(int e, int f, int maxDrops)
        {
            //if (((long)1 << maxDrops) > int.MaxValue)
            //    return false;
            long c = (long)1 << maxDrops;
            return (c - 1) >= f;

            //drop is equal to egg count
            int res = 1, sum = 0;
            for (int drop = 1; drop <= e && sum < f; drop++)
            {
                res *= (maxDrops - drop + 1);
                res /= drop;
                sum += res;
            }
            return sum >= f;
        }

    public static int Four_Sum(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
    {
        List<int> ar1 = new List<int>();
        List<int> ar2 = new List<int>();

        for (int i = 0; i < nums1.Length; i++)
            for (int j = 0; j < nums2.Length; j++)
            {
                ar1.Add(nums1[i] + nums2[j]);
                ar2.Add(nums3[i] + nums4[j]);
            }

        //Sort 1 array
        ar2.Sort();

        int count = 0;
        for (int i = 0; i < ar1.Count; i++)
        {
            int findSum = -1 * ar1[i];
            int leftIdx = Search(-1, ar2, findSum);
            int rightIdx = Search(0, ar2, findSum);

            if (leftIdx != -1)
                count += rightIdx - leftIdx + 1;
        }
        return count;
    }

    private static int Search(int direction, List<int> ar1, int findSum)
    {
        int idx = -1;
        int l = 0, r = ar1.Count - 1;
        while (l <= r)
        {
            int m = l + (r - l) / 2;
            if (ar1[m] == findSum)
            {
                idx = m;
                if (direction == -1) //search on left side
                    r = m - 1;
                else              //search on right side
                    l = m + 1;
            }
            else if (ar1[m] < findSum)
            {
                l = m + 1;
            }
            else if (ar1[m] > findSum)
            {
                r = m - 1;
            }
        }
        return idx;
    }

    public static int reverseBits(int input)
    {
        int j = 0;
        bool found = false;
        int rev = 0;
        for (int i = 31; i >= 0; i--)
        {
            if (found) j++;
            int mask = (1 << i);
            if ((input & mask) != 0)
            {
                int rmask = (1 << j);
                rev = rev | rmask;
                found = true;
            }
        }
        return rev;
    }

    public static List<int> getBuildingsWithAView(int[] buildings)
    {
        List<int> result = new List<int>();
        Stack<(int, int)> stack = new Stack<(int, int)>();
        for (int i = 0; i < buildings.Length; i++)
        {
            while (stack.Any() && stack.Peek().Item2 < buildings[i])
                stack.Pop();

            if (!stack.Any())
            {
                result.Add(i);
                stack.Push((i, buildings[i]));
            }
            else if (stack.Any() && result.Last() != stack.Peek().Item1)
            {
                result.Add(stack.Peek().Item1);
                stack.Push((i, buildings[i]));
            }
        }

        return result;
    }
    
    public static string minWindow(string searchString, string t)
    {
        Dictionary<char, int> occ = new Dictionary<char, int>();
        Dictionary<char, int> ocwin = new Dictionary<char, int>();
        foreach (char c in t)
        {
            if (!occ.ContainsKey(c))
            {
                occ.Add(c, 0);
                ocwin.Add(c, 0);
            }
            occ[c]++;
        }

        int minLength = int.MaxValue;
        int lIdx = 0, rIdx = 0, l = 0;
        int found = 0;
        for (int r = 0; r < searchString.Length; r++)
        {
            char c = searchString[r];
            if (occ.ContainsKey(c))
            {
                ocwin[c]++;
                if(ocwin[c] <= occ[c])
                    found++;
            }
            
            while (found == t.Length)
            {
                if (r - l + 1 < minLength)
                {
                    lIdx = l;
                    rIdx = r;
                    minLength = r - l + 1;
                }
                //sqeeze from left side by 1 character
                c = searchString[l++];
                if (occ.ContainsKey(c))
                {
                    if (ocwin[c] <= occ[c])
                        found--;
                    ocwin[c]--;
                }
            }
        }
        return minLength == int.MaxValue ? "" : searchString.Substring(lIdx, rIdx - lIdx + 1);
    }

    public static string getWrongAnswers(int N, string C)
    {
        // Write your code here
        //string choices = "AB";
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < N; i++)
        {
            char correct = C[i];
            sb.Append(correct == 'A' ? 'B' : 'A');
        }
        return sb.ToString();
    }
    static void Main(string[] args)
    {
        try
        {
            getWrongAnswers(3, "ABA");

            minWindow("donutsandwafflemakemehungry", "flea");

            getBuildingsWithAView(new int[] { 9,8,7});
            reverseBits(29);


            Four_Sum(new int[] {}, new int[] {}, new int[] {}, new int[] {});


            NextPalindrome("1");
            NextGreaterElement(1999999999);


            findNext(218765);

            PermutationToCombination(new int[3], 1, 2, -1);

            int digits = (int)(Math.Floor(Math.Log10(Math.Abs(-123)))) + 1;


            int[] carr = new int[26];
            carr['c'-'a'] = 1;

            StringToInt("1234");

            BrushCount(new int[] { 4, 1, 2, 0, 2, 2 });

            CombinationToPermutation(1, 3, new int[2], 0, 2, "");

            Combination(1, 4, 0, 2, "");   // 4 C 2 = 6
            Permutation(new int[4], 1 ,2);   //4 P 2 = 12


            minWasteBfs(9, new int[] { 2, 5, 3 });
            //MinimumTimeRequired1(new int[] { 1, 2, 4, 7, 8 }, 2);

            MinWorkerRequired(new int[] { 1, 2, 3, 4, 10, 9, 8 }, 15);


            jobsdfs(new int[] { 4, 3, 5 }, 0, new int[2]);

            //var all = new List<IList<int>>();
            //SubSets(new int[] { 1, 2, 3 }, 0, new List<int>(), all);

            //IList<IList<int>> subsets = new List<IList<int>>();
            //SubSets(new int[] { 1, 2, 3 }, 0, new List<int>(), subsets);,


            int[] arr121 = { 1, 5, 3};
            //SubSetSum(new int[] { 1, 2, 3 }, 0, 0);
            combinationUtil(arr121, new List<int>(), 0, arr121.Length, 0);


            MinimumTimeRequired(new int[]{ 1, 2, 4, 7, 8 }, 2);


            maxOfMin(new int[] { 10, 20, 30, 50, 10, 70, 30 }, 7);

            maxProductSubsequence(new int[] { 10, 11, 9, 5, 6, 1, 20 }, 7);
            maxProductSubsequence(new int[] { 1, 2, 3, 4 },4);
            //MaxSumSubmatrix(new int[][] { new int[] { 2,2,-1 }}, 2);

            MaxSumSubmatrix(new int[][] { new int[] { 1, 0, 1 }, new int[] { 0, -2, 3 } }, -2);
            //RodCutting(new int[] { 1,   5,   8,   9,  10,  17,  17,  20 });
            //LongestIncreasingSubsequence(new int[] { -1, 2, 1, 2 });

            List<List<int>> res = new List<List<int>>();
            res.Add(new List<int>() { 0, 0, 0, 1 });
            res.Add(new List<int>(){0, 1, 0, 0});
            res.Add(new List<int>() { 0, 1, 0, 0});
            res.Add(new List<int>() { 0, 1, 0, 1});
            //Program1.SquareOfZeroes(res);

            FindSubsequences(new int[] { 4, 6, 7, 7 });

            ReorganizeString("vvvlo");

            ConvertFromString("100000000000000000000");

            ConvertFromBinaryString("10101");
            ConvertFromHexaString("AC3");
            System.Convert.ToInt32("AABCDF991", 16);
            //System.Convert.ToInt32("101", 2);


            int result = 0;
            string wwe = "1234";
            foreach (char c1 in wwe)
                result = (result * 10) + (c1 - '0');




            NumTreesBottomUp(3);

            string str = "abc";
            DFS(str, 0, new List<char>());

            TreeExercise.runTest();
            sLLExercise.runTest();

            Calculate("(1+(4+5+2)-3)");

            SortedSet<int[]> minHeap = new SortedSet<int[]>(Comparer<int[]>.Create(AscendingMeetingEndTime));
                
            int[] testLst = new int[10];

            var sortedDictionary = new SortedDictionary<int, List<int>>(Comparer<int>.Create(Descending));
            Array.Sort(testLst, Comparer<int>.Create(Descending));
            Array.Sort(testLst, (i, j) => { return j.CompareTo(i); });

            Decode("3[a2[c]]");
            Decode("3[a]2[bc]");
            Decode("2[abc]3[cd]ef");

            MaxSlidingWindow.GetMaxSlidingWindow(new int[] { 1, 3, -1, -3, 5, 3, 6, 7}, 3);
            MaxFreq.GetMaxFreq("aababcaab", 2, 3, 4);

            BinarySearchExercise.CutRibbon(new int[] { 1, 2, 3, 4, 9 }, 5);

            NearestSmallerLeftRight.NSL(new int[] { 71, 55, 82, 55});
            NearestSmallerLeftRight.NSR(new int[] { 71, 54, 82, 55});

            JclosestNumbersToK.GetJclosestNumbersToK(new int[] { 2, 3, 5, 11, 27, 34, 55, 92 }, 5, 18);

            int[,] matrix = new int[3, 3];
            matrix[0, 0] = 1;  matrix[0, 1] = 0; matrix[0, 2] = 0;
            matrix[1, 0] = 1; matrix[1, 1] = 0; matrix[1, 2] = 0;
            matrix[2, 0] = 1; matrix[2, 1] = 1; matrix[2, 2] = 9;
            DemolitionRobot.findMinDiatnaceToObstacle(matrix);

            List<int>[] coor = new List<int>[3] { new List<int>(new int[] { 1, 2 }), new List<int>(new int[] { 1, -1 }), new List<int>(new int[] { 3, 4 }) };
            AmazonFreshDeliveries.GetClosestCoordinate(coor.ToList(), 2);

//            eg: max travel distance is : 11000
//forward route list: [1, 3000],[2,5000],[3,4000],[4,10000]
//        backward route list : [1,2000],[2,3000],[3,4000]

            minimum_cost_merging_files.minCostToMerge(new int[] { 14, 25, 5, 8 });
            int[][] forward = new int[4][] { new int[] {1,3000}, new int[] { 2, 5000 }, new int[] { 3, 4000 }, new int[] { 4, 10000 } };
            int[][] backward = new int[3][] { new int[] { 1, 2000 }, new int[] { 2, 3000 }, new int[] { 3, 4000 } };
            AmazonPrimeAirRoute.getIdPairsForOptimal(forward.ToList(), backward.ToList(), 11000);

            int[,] items = new int[4, 2];// { new int[2] { 1, 2 }, new int[2] { 4, 3 }, new int[2] { 5, 6 }, new int[2] { 6, 7 } };
            items[0, 0] = 1;
            items[0, 1] = 2;
            items[1, 0] = 4;
            items[1, 1] = 3;
            items[2, 0] = 5;
            items[2, 1] = 6;
            items[3, 0] = 6;
            items[3, 1] = 7;
            Microsoft.KnapsackProblem(items, 10);

            Microsoft.CountNonInterSectingSegmentsWithEqualSum(new int[] { 10, 1, 3, 1, 2, 2, 1, 0, 4 });
            Microsoft.CountNonInterSectingSegmentsWithEqualSum(new int[] { 10, 5, 1, 3, 1, 2, 2, 1, 6, 0, 4, 2, 3,3,3,3});
            Microsoft.CountNonInterSectingSegmentsWithEqualSum(new int[] { 5, 3, 1, 3, 2, 3 });
            Microsoft.CountNonInterSectingSegmentsWithEqualSum(new int[] { 9, 9, 9, 9, 9,9 });
            Microsoft.CountNonInterSectingSegmentsWithEqualSum(new int[] { 1, 5, 2, 4, 3, 3 });
            Microsoft.CountNonInterSectingSegmentsWithEqualSum(new int[] { 1, 0, 1, 3, 1, 2, 2, 1, 0, 4 });

            MicrosoftFolder.EvaluateBooleanExpressionEvaluator
                .EvaluateBooleanExpression("TRUE OR FALSE OR ( TRUE AND FALSE OR TRUE OR ( TRUE AND FALSE ) ) AND TRUE");

            Microsoft.NumWays("abcde");


            Microsoft.GetShortestBalancedSubstring("azABaabza");
            Microsoft.GetShortestBalancedSubstring("AcZCbaBz");
                
                
                

            Microsoft.MaximumGamesInAGivenPeriodOfTime("01:01", "02:00");
            Microsoft.MaximumGamesInAGivenPeriodOfTime("12:01", "12:44");
            Microsoft.MaximumGamesInAGivenPeriodOfTime("20:00", "06:00");
            Microsoft.MaximumGamesInAGivenPeriodOfTime("00:00", "23:59");
                

            Microsoft.BiggestLetter("aaacbAbCd");
            Microsoft.BiggestLetter("abcdE");
            Microsoft.BiggestLetter("aA");
            Microsoft.ElementsDifferByOne(new int[]{ 11, 1, 8, 12, 14 });
            Microsoft.ElementsDifferByOne(new int[] { 5,5,5,5,5 });
                
            Microsoft.CanCraneMovePackage(new int[] { 1,4,2 }, new int[] { 10,4,7 }, 11,1);

            Microsoft.ReplaceAllToAvoidConsecutiveRepeatingCharacters.ModifyString("ubv?w");
            WaterArea(new int[] { 3, 0, 0, 2, 0, 4 });// 0, 1, 1, 0, 0 });

            //Threading.DemonstrateConcurrency();
            Threading.DemonstrateParallelism();
                



            Microsoft.readStream(@"c:\IK.txt");

            Microsoft.GetShortestBalancedSubstring("azABaabza");
            Microsoft.MaxPossibleByInsertingFive(1234);
                
            solveKnapsack(new int[] { 1,6,10,16}, new int[] { 1,2,3,5}, 7);


                
            testLst.ToList().Sort((i, j) => { return j.CompareTo(i); });

               

            int[][] booked22 = new int[1][] { new int[] { 2,3 }};


            int[][] booked2 = new int[3][] { new int[] { 2,1 },
                new int[] { 1,8}, new int[] {2,6 }};

            int[][] booked1 = new int[4][] { new int[] { 4,3 },
                new int[] { 1,4}, new int[] { 4,6 }, new int[]{ 1,7}};

            int[][] booked = new int[6][] { new int[] { 1, 2 },
                new int[] { 1, 3 }, new int[] { 1, 8 }, new int[]{ 2,6}, new int[]{ 3, 1 }, new int[]{ 3,10} };
                
            Microsoft.MaxNumberOfFamilies(3, booked22);

            Microsoft.maxNetworkRank(new int[] { 1, 2, 3, 3 }, new int[]{ 2,3,1,4}, 4);


            Microsoft.particleVelocity(new int[] { -1, 1, 3, 3, 3, 2, 3, 2, 1, 0 });

            Microsoft.widestPath(new List<int>(new int[] { 5, 5, 5, 7, 7, 7 }), new List<int>(new int[] { 3, 4, 5, 1, 3, 7 }));
            Microsoft.widestPath(new List<int>(new int[] { 6, 10, 1, 4, 3 }), new List<int>(new int[] { 2, 5, 3, 1, 6 }));
            Microsoft.widestPath(new List<int>(new int[] { 4, 1, 5, 4 }), new List<int>(new int[] { 4, 5, 1, 3 }));


            Microsoft.fairIndex(new int[]{4, -1, 0, 3}, new int[]{-2, 5, 0, 3});
            Microsoft.fairIndex(new int[] { 2, -2, -3, 3 }, new int[] { 0, 0, 4, -4});

            Microsoft.minSteps(new int[] { 5, 2, 1 });
            Microsoft.minSteps(new int[] { 5, 5, 2, 2, 1, 1 });
            Microsoft.minSteps(new int[] { 5, 5, 1 });
            Microsoft.minSteps(new int[] { 5, 5, 5, 5, 1 });
            Microsoft.minSteps(new int[] { 3, 2, 2 });

            Microsoft.SmallestSubsequence("cbacdcbc");

            IList<int[]> building = new List<int[]>();
            building.Add(new int[3] { 0, 3, 3 });
            building.Add(new int[3] { 1, 5, 3 });
            building.Add(new int[3] { 2, 4, 3 });
            building.Add(new int[3] { 3, 7, 3 });
            //building.Add(new int[3] { 19, 24, 8 });

            // [[1,38,219],[2,19,228],[2,64,106],[3,80,65],[3,84,8],[4,12,8],[4,25,14],[4,46,225],[4,67,187],[5,36,118],[5,48,211],[5,55,97],[6,42,92],[6,56,188],[7,37,42],[7,49,78],[7,84,163],[8,44,212],[9,42,125],[9,85,200],[9,100,74],[10,13,58],[11,30,179],[12,32,215],[12,33,161],[12,61,198],[13,38,48],[13,65,222],[14,22,1],[15,70,222],[16,19,196],[16,24,142],[16,25,176],[16,57,114],[18,45,1],[19,79,149],[20,33,53],[21,29,41],[23,77,43],[24,41,75],[24,94,20],[27,63,2],[31,69,58],[31,88,123],[31,88,146],[33,61,27],[35,62,190],[35,81,116],[37,97,81],[38,78,99],[39,51,125],[39,98,144],[40,95,4],[45,89,229],[47,49,10],[47,99,152],[48,67,69],[48,72,1],[49,73,204],[49,77,117],[50,61,174],[50,76,147],[52,64,4],[52,89,84],[54,70,201],[57,76,47],[58,61,215],[58,98,57],[61,95,190],[66,71,34],[66,99,53],[67,74,9],[68,97,175],[70,88,131],[74,77,155],[74,99,145],[76,88,26],[82,87,40],[83,84,132],[88,99,99]]


            Microsoft.GetSkyline(building.ToArray());

            Microsoft.maxLength(new string[] { "co", "di", "ity" });
            Microsoft.maxLength(new string[] { "abc", "kkk", "defg", "csv" });

            Microsoft.minAdjSwap("WWRWWWRWRR");
            Microsoft.minAdjSwap("WWRWWWRWR");
            Microsoft.minAdjSwap("WR");
            Microsoft.minAdjSwap("WWW");


            Microsoft.maxInserts("aab");
            Microsoft.maxInserts("aba");
            Microsoft.maxInserts("aabab");
            Microsoft.maxInserts("dog");
            Microsoft.maxInserts("baaaa");
            Microsoft.maxInserts("aababaa");

            Microsoft.SemiAlternateSubstring("baaabbabbb");
            Microsoft.SemiAlternateSubstring("aababababbbabbababb");

            Microsoft.longestValidString("aabbaabbaabbaaa");

                

            Microsoft.minStep("BAAABAB");

            //TitleToNumber("AB");
            //Amazon.getTimes(4, new List<int>(new int[] { 0, 0, 1, 6 }), new List<int>(new int[] { 0, 1, 1, 0 }));
            /*Amazon.topMentioned(2, new List<string>(new string[] { "gatsby", "american", "novel" }),
                new List<string>(new string[] { "The opening of The Great Gatsby -- its first 3-4 pages -- ranks among the best of any novel in the English language." ,
                    "It is masterful, some may say the great American novel.",
                        "The Great Gatsby is a 1925 novel written by American author F. Scott Fitzgerald"
                }));*/
            //Amazon.substrings("aabcdbcd", 3);
            //s = , ranges = [[0, 4], [1, 6]]

            /*var lst23 = new List<List<int>>();
            lst23.Add(new List<int>(new int[] { 0,3 }));
            Amazon.numberOfItems("|**|***|", lst23);
            */


            //Amazon.autoScale(new List<int>(new int[] { 80, 10, 20, 30, 50 }), 100000001);

            var lstA = new List<List<int>>();

            lstA.Add(new List<int>(new int[] { 1, 3 }));
            lstA.Add(new List<int>(new int[] { 2, 5 }));
            lstA.Add(new List<int>(new int[] { 3, 7 }));
            lstA.Add(new List<int>(new int[] { 4, 10 }));
            var lstB = new List<List<int>>();
            lstB.Add(new List<int>(new int[] { 1, 2 }));
            lstB.Add(new List<int>(new int[] { 2, 3 }));
            lstB.Add(new List<int>(new int[] { 3, 4 }));
            lstB.Add(new List<int>(new int[] { 4, 5 }));
            Amazon.getPairs(lstA, lstB, 10);


            List<string> logs = new List<string>();
            logs.Add("345366 899212 45");
            logs.Add("029323 382391 23");
            logs.Add("382391 345366 15");
            logs.Add("029323 382391 77");
            logs.Add("345366 382391 23");
            logs.Add("029323 345366 13");
            logs.Add("382391 382391 23");
            AmazonOA.transactionlogs.getUserWithLogMoreThanThreshold(logs, 3);

            //MincostTickets(new int[] {1,2,3,4,6,8,9,10,13,14,16,17,19,21,24,26,27,28,29 }, new int[] { 3,14,50});
                
            PalindromeTrie pt = new PalindromeTrie();
            pt.PalindromePairs(new string[] { "pidus", "lllsudip" });


            int[] prices11 = new int[10];
            int[][][] dp11 = new int[prices11.Length + 1][][];
            for (int d = 0; d < prices11.Length + 1; d++)
            {
                dp11[d] = new int[2][];
                for (int d11 = 0; d11 < dp11[d].Length; d11++)
                    dp11[d][d11] = new int[2];
            }

            PalindromePartition("madamzmadam");

            FindDuplicates(new int[] { 4, 3, 2, 7, 8, 2, 3, 1 });


            GetCollisionTimes.GetCollisionTimes1(new int[4][]{ new int[2] { 1, 2 }, new int[2] { 2, 1 },new int[2] { 4, 3 },new int[2] { 7, 2 }});


            //1,7], toppingCosts = [3, 4], target = 10
            //[3,10], toppingCosts = [2,5], target = 9
            //[10], toppingCosts = [1], target = 1
            ClosestCost1.ClosestCost(new int[] { 1,7 }, new int[] { 3,4 }, 10);

            ClosestCost1.ClosestCost(new int[] {3,10 }, new int[] {2,5 }, 9);
            ClosestCost1.ClosestCost(new int[] {10 }, new int[] {1 }, 1);

            ClosestCost1.ClosestCost(new int[] {8,4,4,5,8}, new int[] {3,10,9,10,8,10,10,9,3 }, 6);

            GenerateTrees.GenerateTrees1(5);

            MinTaps.MinTaps1(3, new int[] { 0, 0, 0, 0 });


            SortingSearchingLogarithmsExercise.runTest();
            //GraphExercise.runTest();
            ArrayExercise.runTest();
            RecursionExercise.runTest();

                
            FullJustify(new string[] { "What", "must", "be", "acknowledgment", "shall", "be" }, 16);
            //[1,2],[3,5],[6,7],[8,10],[12,16]], newInterval = [4,8]
            Insert(new int[][]{ new int[]{1,2}, new int[]{3,5}, new int[] { 6, 7 }, new int[] { 8, 10 }, new int[] { 12, 16 } }, new int[]{ 4,8});
            //Solution1.CanJump(new int[] { 3, 2, 1, 0, 4 });
            RecursionExercise.runTest();

            Convert("PAYPALISHIRING", 1);


            Solution.FindSubstring("aaaaaaaaaaaaaa", new string[] { "aa", "aa"});

            Solution.FindSubstring("wordgoodgoodgoodbestword", new string[] { "word", "good", "best", "good" });//, "wing", "ding", "wing" });
            Solution.FindSubstring("barfoothefoobarman", new string[] { "foo", "bar" });
            Solution.FindSubstring("barfoofoobarthefoobarman", new string[] { "bar", "foo", "the" });
            Solution.FindSubstring("wordgoodgoodgoodbestword", new string[] { "word", "good", "best", "word" });

            //MinWindow.MinWindow1("ADOBECODEBANC", "ABC");
            //int[][] d = new int[1][] { new int[] { 1} };
            //RainWaterII.TrapRainWater(d);

            LengthOfLongestSubstring("abcabd");

            LargestSubarray(new int[] { 1, 4, 5, 2, 3 }, 3);

            ThreeSum1(new int[] { -1, 0, 1, 2, -1, -4 });

            ArrayExercise.runTest();

            GraphExercise.runTest();
            TreeExercise.runTest();
            //DPExercise.runTest();

            //ArrayExercise.runTest();
            //BinarySearchExercise.runTest();
            //SortingSearchingLogarithmsExercise.runTest();
                
            //DPRecursionBottomUpExercise.runTest();



            RecursionExercise.runTest();

            //GraphExercise.runTest();

            TreeExercise.runTest();

            //
            DPRecursionBottomUpExercise.runTest();


            TreeNode root1 = new TreeNode(1);
            root1.left = new TreeNode(0);
            root1.left.left = new TreeNode(0);
            root1.left.right = new TreeNode(1);

            root1.right = new TreeNode(1);
            root1.right.left = new TreeNode(0);
            root1.right.right = new TreeNode(1);

            SumRootToLeaf(root1);

            WordBreak("leetcode", new string[] { "leet", "code" });


            IList<int[]> vertices = new List<int[]>();
            vertices.Add(new int[] { 1, 2 });
            vertices.Add(new int[] { 3 });
            vertices.Add(new int[] { 3 });
            vertices.Add(new int[] { });

            AllPathsSourceTarget(vertices.ToArray());

            int[] nums12 = new int[] { 1, 2, 1, 3, 2, 5 };
            int xor = 0;
            foreach (int n1 in nums12)
            {
                xor ^= n1;
            }

            //1010", b = "1011
            AddBinary("1010", "1011");
            AngleClock(12, 30);

            TreeNode tn = RecoverFromPreorder("1-401--349---90--88");

            //string wwwww1 = Convert.ToString(67, toBase: 2);



            var bitArray = new BitArray(BitConverter.GetBytes(67));

            var bytes = BitConverter.GetBytes(1);
            BitArray b1 = new BitArray(bytes);

            StringBuilder bitstr = new StringBuilder();
            uint cc = 1;

            while (cc > 0)
            {
                bitstr.Insert(0, cc & 1);
                cc = cc >> 1;
            }


            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(3);
            root.right = new TreeNode(2);
            root.left.left = new TreeNode(5);
            //root.left.right = new TreeNode(3);
            WidthOfBinaryTree(root);

            ThreeSum(new int[] { -1, 0, 1, 2, -1, -4 });

            GetPermutation(4, 3);
            NextPermutation(new int[] { 4, 2, 3, 1 });


            int[] coins = new int[] { 1, 2 };
            int amount1 = 3;
            int[] dp1 = new int[amount1 + 1];
            dp1[0] = 1;
            int[] dp2 = new int[amount1 + 1];
            dp2[0] = 1;

            for (int i = 0; i < coins.Length; i++)
            {
                for (int j = coins[i]; j <= amount1; j++)
                {
                    dp2[j] += dp2[j - coins[i]];
                }
            }

            // give target j, where it can come from
            for (int i = 1; i <= amount1; i++)
            {
                for (int j = 0; j < coins.Length; j++)
                {
                    var number = i - coins[j];
                    if (number >= 0)
                    {
                        //combinations[i] += combinations[number];
                        dp1[i] += dp1[number];
                    }
                }
            }

            CombinationSum4_Bottom_Up(new int[] { 1, 2, 3 }, 4);

            int[] ar1 = new int[] { 1, 1, 2, 5, 6 };
            Array.Sort(ar1);
            result1 = new List<IList<int>>();

            backtrack(ar1, new List<int>(), 0, 8);


            IList<IList<string>> dddd = new List<IList<string>>();

            var v = new List<string>();
            v.Add("EZE");
            v.Add("AXA");
            dddd.Add(v);
            v = new List<string>();
            v.Add("TIA");
            v.Add("ANU");
            dddd.Add(v);
            v = new List<string>();
            v.Add("ANU");
            v.Add("JFK");
            v = new List<string>();
            v.Add("JFK");
            v.Add("ANU");
            dddd.Add(v);


            v = new List<string>();
            v.Add("ANU");
            v.Add("EZE");
            dddd.Add(v);
            v = new List<string>();
            v.Add("TIA");
            v.Add("ANU");
            dddd.Add(v);

            v = new List<string>();
            v.Add("AXA");
            v.Add("TIA");
            dddd.Add(v);
            v = new List<string>();
            v.Add("TIA");
            v.Add("JFK");
            dddd.Add(v);
            v = new List<string>();
            v.Add("ANU");
            v.Add("TIA");
            dddd.Add(v);
            v = new List<string>();
            v.Add("JFK");
            v.Add("TIA");
            dddd.Add(v);

            FindItinerary(dddd);


            coinChange_DP_BottomUp1(new int[] { 2 }, 1);

            CoinChange_DP(5, new int[] { 1, 2, 5 });

            Change(11, new int[] { 1, 2, 5 });

            LargestDivisibleSubset(new int[] { 2, 3, 8, 9, 27 });

            int[][] menNheight = new int[6][];
            menNheight[0] = new int[] { 7, 0 };
            menNheight[1] = new int[] { 4, 4 };
            menNheight[2] = new int[] { 7, 1 };
            menNheight[3] = new int[] { 5, 0 };
            menNheight[4] = new int[] { 6, 1 };
            menNheight[5] = new int[] { 5, 2 };
            ReconstructQueue(menNheight);

            Subsets(new int[] { 1, 2, 3, 4 });

            /*int[] test = new int[] { 1, 3, 4, 5 };

            int search = 1;
            int left1 = 0;
            int right1 = test.Length - 1;
            int mid11 = 0;
            while (left1 <= right1)
            {
                mid11 = left1 + (right1 - left1) / 2;

                if (test[mid11] == search)
                    break;
                else if (test[mid11] < search)
                    left1 = mid11 + 1;
                else
                    right1 = mid11 - 1;
            }
            */


            int[][] costs = new int[4][];
            costs[3] = new int[] { 10, 20 };
            costs[2] = new int[] { 30, 200 };
            costs[1] = new int[] { 400, 500 };
            costs[0] = new int[] { 30, 520 };
            TwoCitySchedCost(costs);



            ListNode ll = new ListNode(4);
            ll.next = new ListNode(5);
            ll.next.next = new ListNode(1);
            ll.next.next.next = new ListNode(9);
            DeleteNode(ref ll.next.next.next);



            int[][] B11 = new int[4][];
            B11[0] = new int[] { 1, 2 };
            B11[1] = new int[] { 1, 3 };
            B11[2] = new int[] { 2, 4 };
            B11[3] = new int[] { 4, 1 };
            PossibleBipartition(4, B11);



            CharacterReplacement("AABABBA", 1);
            int[] A1 = new int[] { 1, 3, 7, 1, 7, 5 };
            int[] B1 = new int[] { 1, 9, 2, 5, 1 };
            MaxUncrossedLines(A1, B1);

            List<List<int>> triangle = new List<List<int>>();
            triangle.Add(new List<int>() { 2 });
            triangle.Add(new List<int>() { 3, 4 });
            triangle.Add(new List<int>() { 6, 5, 7 });
            triangle.Add(new List<int>() { 4, 1, 8, 3 });

            int[] dp = new int[triangle.Count];

            int x11111111111 = min(triangle, 0, 0, dp);

            BuildBSTUsingRange(new int[] { 8, 5, 1, 7, 10, 12 }, int.MinValue, int.MaxValue);

            int[][] A = new int[1][];
            A[0] = new int[] { 8, 15 };

            int[][] B = new int[3][];
            B[0] = new int[] { 2, 6 };
            B[1] = new int[] { 8, 10 };
            B[2] = new int[] { 12, 20 };

            /*int[][] A = new int[4][];
            A[0] = new int[] { 0, 2 };
            A[1] = new int[] { 5, 10 };
            A[2] = new int[] { 13, 23 };
            A[3] = new int[] { 24, 25 };

            int[][] B = new int[4][];
            B[0] = new int[] { 1, 5 };
            B[1] = new int[] { 8,12 };
            B[2] = new int[] { 15,24 };
            B[3] = new int[] { 25,26 };
            */

            IntervalIntersection(A, B);


            FrequencySort("tree");


            Array.Sort(a1111, new SortTest());

            Kadene(new int[] { -1, -2, -4 });


            int testCase = int.Parse(args[0]);
            string[] inputs = args[1].Split(' ');
            string[] prices = args[2].Split(' ');
            int amount = int.Parse(inputs[1]);
            int houseCount = int.Parse(inputs[0]);
            int[] pricesArray = new int[1000];
            for (int x = 0; x < prices.Length; x++)
            {
                pricesArray[int.Parse(prices[x])]++;
            }

            int count = 0;
            for (int x = 0; x < pricesArray.Length; x++)
            {
                while (pricesArray[x] > 0 && amount > 0 && x <= amount)
                //if(pricesArray[x] > 0 && amount > 0)
                {
                    //if(x <= amount)
                    {
                        count++;
                        amount -= x;
                        pricesArray[x]--;
                    }
                }

                if (amount <= 0)
                    break;
            }
            Console.WriteLine("Case #1:" + count);




            RemoveKdigits("1432219", 3);
            //Sqrt(808201);
            /*char[][] memo = new char[9][];

            memo[0] = new char[] { 'X','X','X','X','O','O','X','X','O' };
            memo[1] = new char[] { 'O','O','O','O','X','X','O','O','X'};
            memo[2] = new char[] { 'X','O','X','O','O','X','X','O','X'};
            memo[3] = new char[] { 'O','O','X','X','X','O','O','O','O' };
            memo[4] = new char[] { 'X','O','O','X','X','X','X','X','O' };
            memo[5] = new char[] { 'O','O','X','O','X','O','X','O','X' };
            memo[6] = new char[] { 'O','O','O','X','X','O','X','O','X' };
            memo[7] = new char[] { 'O','O','O','X','O','O','O','X','O' };
            memo[8] = new char[] { 'O','X','O','O','O','X','O','X','O' };

            Solve(memo);
            */
            /*
            //string str1 = "17,51#33,83#53,62#25,34#35,90#29,41#14,53#40,84#41,64#13,68#44,85#57,58#50,74#20,69#15,62#25,88#4,56#37,39#30,62#69,79#33,85#24,83#35,77#2,73#6,28#46,98#11,82#29,72#67,71#12,49#42,56#56,65#40,70#24,64#29,51#20,27#45,88#58,92#60,99#33,46#19,69#33,89#54,82#16,50#35,73#19,45#19,72#1,79#27,80#22,41#52,61#50,85#27,45#4,84#11,96#0,99#29,94#9,19#66,99#20,39#16,85#12,27#16,67#61,80#67,83#16,17#24,27#16,25#41,79#51,95#46,47#27,51#31,44#0,69#61,63#33,95#17,88#70,87#40,42#21,42#67,77#33,65#3,25#39,83#34,40#15,79#30,90#58,95#45,56#37,48#24,91#31,93#83,90#17,86#61,65#15,48#34,56#12,26#39,98#1,48#21,76#72,96#30,69#46,80#6,29#29,81#22,77#85,90#79,83#6,26#33,57#3,65#63,84#77,94#26,90#64,77#0,3#27,97#66,89#18,77#27,43";
            string str1 = "0,1#0,2#1,2";
                
            var ar8 = str1.Split('#');
            int[][] rooms = new int[ar8.Length][];
            int x6 = 0;
            foreach(string s in ar8)
            {
                var ar1 = s.Split(',');
                rooms[x6] = new int[] { int.Parse(ar1[0]), int.Parse(ar1[1]) };
                x6++;
            }
            */
            int[][] rooms = new int[1][];
            rooms[0] = new int[] { 1, 2 };
            /*rooms[1] = new int[] { 1,4 };
            rooms[2] = new int[] { 2,3 };
            rooms[3] = new int[] { 2, 4 };
            rooms[4] = new int[] { 4, 3 };*/
            FindJudge(3, rooms);



            ShortestDistance(rooms);
            //int Length = "12345".Length;
            //int[,] memo = new int[Length, Length];
            //for (int x = 0; x < Length; x++)
            //    for (int y = 0; y < Length; y++)
            //        memo[x, y] = -1;

            //subarraySum(new int[] { 1 }, 0);

            subarraySum(new int[] { 0, 3, 4, 7, 2, -3, 1, 4, 2 }, 7);
            subarraySum(new int[] { 0, 1, 2, -3, 4, 2, -6, 6 }, 6);

            //subarraySum(new int[] { 6 }, 6);


            int[][] shift = new int[7][] {
                        new int[] {0,7},
                        new int[] {1,7},
                        new int[] {1,0},
                        new int[] {1,3},
                        new int[] {0,3},
                        new int[] {0,6},
                        new int[] {1,2},
                    };

            int totalShift = 0;
            for (int x = 0; x < shift.Length; x++)
            {
                if (shift[x][0] == 0) //left shift
                    totalShift -= shift[x][1];
                if (shift[x][0] == 1) //right shift
                    totalShift += shift[x][1];
            }

            Console.WriteLine(totalShift);



            int[] ar = new int[] { 2, 7, 4, 1, 8, 1 };
            Collection<int> c = new Collection<int>();
            SortedSet<int> ss = new SortedSet<int>(ar);

            //SortedDictionary<int, int> sd = new SortedDictionary<int, int>();
            //sd.Add(1, 2);
            //sd.Add(0, 1);
            //sd.Add(3, 2);

            List<int> al = new List<int>(ar);
            al.Sort();

            int h = 0;
            int sh = 0;

            while (al.Count > 0)
            {
                if (al.Count > 1)
                {
                    h = al.Last();
                    al.Remove(h);

                    //h = ss.Last();
                    //ss.Remove(h);
                    //sh = ss.Last();
                    sh = al.Last();
                    al.Remove(sh);

                    if (h - sh > 0)
                    {
                        //ss.Add(h - sh);
                        al.Add(h - sh);
                        al.Sort();
                    }
                }
                else
                {
                    break;
                }
            }




            BackspaceCompare("bxj##tw", "bxj###tw");




            //ArrayExercise.runTest();

            GraphExercise.runTest();
            BinarySearchExercise.runTest();
            sLLExercise.runTest();





            numberToWords(1000);

            var ssss = NumberToWords(999999);


            int[] nums1 = new int[] { 2, 0, 2, 1, 1, 0 };


            int left = 0;
            int right = nums1.Length - 1;

            for (int i = 0; i <= nums1.Length - 1; i++)
            {
                if (nums1[i] == 0)
                {
                    nums1[i] = nums1[left];
                    nums1[left++] = 0;
                }
                else if (nums1[i] == 2)
                {
                    nums1[i] = nums1[right];
                    nums1[right--] = 2;
                }
            }


            BacktrackingMemoizationExercise.runTest();




            sLLExercise.runTest();
            StackQueueExercise.runTest();




            NumberToWords(1000000000);
            int n = 45;
            int answer = 0;

            while (n != 0)
            {
                answer += 1;//n & 1;
                //n = n >>> 1;
                n = n >> 1;
            }

            n = 3;
            answer = 0;
            for (int y = n; y >= 1; y = y / 2)
            {
                answer++;
            }


            int[] a = new int[] { 1, -1, 5, -2, 3 };
            int[] c_sum = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                c_sum[i] = (i == 0) ? a[0] : c_sum[i - 1] + a[i];
                if (c_sum[i] == 4)
                {

                }
                //return new Pair<int>(0, i);
            }

            Dictionary<int, int> d1 = new Dictionary<int, int>();
            for (int i = 0; i < c_sum.Length; i++)
            {
                if (d1.ContainsKey(c_sum[i]))
                {

                }
                //return​​ new​​ Pair<int>(map[c_sum[i]+1,​​i);
                else
                    d1.Add(c_sum[i], i);
            }

            int k = 1;
            int[] nums = new int[] { 2, 5, 9, 6, 9, 3, 8, 9, 7, 1 };
            int sum = 0, max = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();

            int slow = 0, fast = 0;


            while (true)
            {
                fast = nums[nums[fast]];
                slow = nums[slow];
                if (fast == slow) break;
            }

            /*fast = 0;
            while (fast != slow) {
                fast = nums[fast];
                slow = nums[slow];
            }
            return fast;*/

            slow = 0;
            while (fast != slow)
            {
                fast = nums[fast];
                slow = nums[slow];
            }


            utilExercise.runTest();

            BacktrackingMemoizationExercise.runTest();

            DPRecursionBottomUpExercise.runTest();

            GraphExercise.runTest();

            ArrayExercise1.runTest();

            StackQueueExercise.runTest();

            GreedyAlgoExercise.runTest();

            SortingSearchingLogarithmsExercise.runTest();





            ArrayExercise.runTest();



            sLLExercise.runTest();

            ArrayExercise1.runTest();

            AbstractTest.runTest();

            //
            //SortingSearchingLogarithmsExercise.runTest();

            //StackQueueExercise.runTest();

            ListHashDictExercise.runTest();

            //ArrayExercise.runTest();
            //sLLExercise.runTest();
            utilExercise.runTest();
            //BinarySearchExercise.runTest();


            BitArray b111 = new BitArray(new int[] { 3 });


            //palindrome
            //string pstring = "abcdcba";

            //dcbaabcdc b aabcdcba

            string pstring = "abcdcbaabcdcbaabcdcba";
            char[] arr = pstring.ToCharArray();//.Split();
            string[] test11111 = Regex.Split(pstring, string.Empty);
            var anotherArray = pstring.Select(x => x.ToString()).ToArray();
            int mid = (arr.Length + 3) >> 1;
            int mid1 = (arr.Length + 3) / 2;
            int mid2 = arr.Length - ((arr.Length + 3) >> 1);

            string.Join("", arr);
            new string(arr);

            var end = arr.Length - 1;
            for (int x = 0; x <= mid; x++)
            {
                if (!arr[x].Equals(arr[end]))
                {

                }
                end--;
            }

            var l1 = new List<student>();
            for (int x = 0; x <= 50000; x++)
            {
                l1.Add(new student("sudip" + x, x));
            }
            var l2 = new List<student>();
            for (int x = 0; x <= 20000; x++)
            {
                l2.Add(new student("sudip" + x, x));
            }
            l2.Add(new student("sudip0", 0));
            l2.Add(new student("sudip11", 11));

            var xyz = l1.Except(l2, new GenericCompare<student>(s => new { s.Name, s.Age })).ToList();// EqualityProjectionComparer<student>.Create(s => new { s.Name, s.Age }));
            var xyz1 = l1.Except(l2, EqualityProjectionComparer<student>.Create(s => new { s.Name, s.Age })).ToList();

            int count1 = 0;
            while (count <= l1.Count())
            {
                var vins1000Records = l1.Skip(count).Take(10);
                count += 9;
            }





            //{"Unable to find credentials\r\n\r\nException 1 of 4:\r\nSystem.ArgumentException: App.config does not contain credentials information. 
            //Either add the AWSAccessKey and AWSSecretKey or AWSProfileName
            //   .\r\n   at Amazon.Runtime.StoredProfileAWSCredentials..ctor(String profileName, String profilesLocation) in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 318\r\n   at Amazon.Runtime.StoredProfileAWSCredentials..ctor(String profileName) in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 264\r\n   at Amazon.Runtime.StoredProfileAWSCredentials..ctor() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 254\r\n   at Amazon.Runtime.EnvironmentAWSCredentials..ctor() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 590\r\n   at Amazon.Runtime.FallbackCredentialsFactory.<Reset>b__1() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 1117\r\n   at Amazon.Runtime.FallbackCredentialsFactory.GetCredentials(Boolean fallbackToAnonymous) in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 1137\r\n\r\nException 2 of 4:\r\nSystem.ArgumentException: App.config does not contain credentials information. Either add the AWSAccessKey and AWSSecretKey or AWSProfileName.\r\n   at Amazon.Runtime.StoredProfileAWSCredentials..ctor(String profileName, String profilesLocation) in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 318\r\n   at Amazon.Runtime.StoredProfileAWSCredentials..ctor(String profileName) in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 264\r\n   at Amazon.Runtime.StoredProfileAWSCredentials..ctor() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 254\r\n   at Amazon.Runtime.FallbackCredentialsFactory.<Reset>b__2() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 1118\r\n   at Amazon.Runtime.FallbackCredentialsFactory.GetCredentials(Boolean fallbackToAnonymous) in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 1137\r\n\r\nException 3 of 4:\r\nSystem.InvalidOperationException: The environment variables AWS_ACCESS_KEY_ID and AWS_SECRET_ACCESS_KEY were not set with AWS credentials.\r\n   at Amazon.Runtime.EnvironmentVariablesAWSCredentials..ctor() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 535\r\n   at Amazon.Runtime.FallbackCredentialsFactory.<Reset>b__3() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 1119\r\n   at Amazon.Runtime.FallbackCredentialsFactory.GetCredentials(Boolean fallbackToAnonymous) in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 1137\r\n\r\nException 4 of 4:\r\nAmazon.Runtime.AmazonServiceException: Unable to reach credentials server\r\n   at Amazon.Runtime.InstanceProfileAWSCredentials.GetContents(Uri uri) in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 1001\r\n   at Amazon.Runtime.InstanceProfileAWSCredentials.<GetAvailableRoles>d__0.MoveNext() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 880\r\n   at Amazon.Runtime.InstanceProfileAWSCredentials.GetFirstRole() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 1008\r\n   at Amazon.Runtime.InstanceProfileAWSCredentials..ctor() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 866\r\n   at Amazon.Runtime.FallbackCredentialsFactory.<Reset>b__4() in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 1121\r\n   at Amazon.Runtime.FallbackCredentialsFactory.GetCredentials(Boolean fallbackToAnonymous) in d:\\Jenkins\\jobs\\build-sdk-v2\\workspace\\sdk\\src\\AWSSDK_DotNet35\\Amazon.Runtime\\AWSCredentials.cs:line 1137\r\n\r\n"}


            //TransferUtility fileTransferUtility = new
            //    TransferUtility(new AmazonS3Client("AKIAJR5ONMR7TRSCIDNQ", "RiDeSz4fMFD9RjpW6IViKDiocdFuBWROXOiyMPZL",Amazon.RegionEndpoint.SAEast1));

            // 1. Upload a file, file name is used as the object key name.
            //fileTransferUtility.Upload(@"c:\SUService.log", "existingBucketName");
            //Console.WriteLine("Upload 1 completed");



            int[] grades = { 7, 1, 5, 3, 6, 4 };

            int x21 = 1;
            for (int i = 1; i <= 5; i++)
            {
                x21 = 2 * x21;
            }

            int x2 = 2;
            int y2 = x2 * x2 * x2;

            for (int i = 0; i < grades.Length / 2; i++)
            {
                int tmp = grades[i];
                grades[i] = grades[grades.Length - i - 1];
                grades[grades.Length - i - 1] = tmp;
            }



            for (int x1 = 0; x1 < grades.Length; x1++)
            {
                grades[x1] = (grades[x1] % 5);
            }



            Array.Copy(grades, grades, 0);
            return;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    class personHeightComparer : IComparer<int[]>
    {
        public int Compare(int[] x, int[] y)
        {
            if (x[0] != y[0])
                return y[0] - x[0]; //descending by people number

            else return x[1] - y[1]; //ascending by number of people before them
        }
    }
    public static int[][] ReconstructQueue(int[][] people)
    {
        Array.Sort(people, new personHeightComparer());

        int[][] result = new int[people.Length][];
        for (int x = 0; x < result.Length; x++)
            result[x] = new int[2];
            
        for (int x=0; x < people.Length; x++)
        {
            int index = people[x][1];

            //to make space for 'index' 
            //shifting all items 1 position right side
            for(int y = people.Length - 2; y >= index; y -- )
            {
                result[y+1][0] = result[y][0];
                result[y+1][1] = result[y][1];
            }
            //put people[x] at index
            result[index][0] = people[x][0];
            result[index][1] = people[x][1];
        }

        return result;
    }

    public static IList<int> LargestDivisibleSubset(int[] nums)
    {
        //Sort the Array
        //Array.Sort(nums);

        int[] dp = new int[nums.Length];

        //Initiaize the array to 1, because the longest substring for one element is '1'
        for (int i = 0; i < nums.Length; i++)
        {
            dp[i] = 1;
        }

        IList<IList<int>> subsetsAtIndex = new List<IList<int>>();
        int largestSubsetSize = 0;
        int largestSubsetIndex = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            subsetsAtIndex.Add(new List<int>());

            for (int j = 0; j < i; j++)
            {
                if (nums[i] % nums[j] == 0) //if module is 0
                {
                    //dp[i] = Math.Max(dp[i], dp[j] + 1);

                    if (dp[j] + 1 > dp[i])
                    {
                        subsetsAtIndex[i].Clear();
                        subsetsAtIndex[i] = subsetsAtIndex[i].Concat(subsetsAtIndex[j]).ToList();

                        dp[i] = dp[j] + 1;
                    }


                    //update 'largestSubsetSize' and 'largestSubsetIndex' where we got the maximum 'largestSubsetSize'
                    if (dp[i] > largestSubsetSize)
                    {
                            
                        largestSubsetSize = dp[i];
                        largestSubsetIndex = i;
                    }
                }
            }

            subsetsAtIndex[i].Add(nums[i]);
        }

        Console.WriteLine(largestSubsetIndex);

        return subsetsAtIndex[largestSubsetIndex];
    }

    static IList<IList<int>> result = new List<IList<int>>();
    public static int Change(int amount, int[] coins)
    {
        //Array.Sort(coins);

        //CoinChange(amount, coins, 0, new List<int>());
        //return result.Count;

        return CoinChange_DP(amount, coins);
    }

    public static void CoinChange_Recursion(int amount, int[] coins, int index, IList<int> demomination)
    {
        if (amount == 0)
        {
            result.Add(new List<int>(demomination));
            return;
        }
        else if (amount < 0 || index >= coins.Length)
            return;

        demomination.Add(coins[index]);
        CoinChange_Recursion(amount - coins[index], coins, index, demomination);

        demomination.RemoveAt(demomination.Count - 1);

        index++;
        CoinChange_Recursion(amount, coins, index, demomination);
    }

    public static int CoinChange_DP(int amount, int[] coins)
    {
        int[][] dp = new int[coins.Length + 1][];
        for(int x=0; x< dp.Length; x++)
        {
            dp[x] = new int[amount + 1];
        }

        for (int x = 0; x <= coins.Length; x++)
        {
            dp[x][0] = 1;
        }
        for (int x = 0; x <= amount; x++)
        {
            dp[0][x] = (x ==0) ? 1 : 0;
        }

        for (int i = 1; i <= coins.Length; i++)
        {
            for (int j = 1; j <= amount; j++)
            {
                int remainAmt = j - coins[i - 1];
                dp[i][j] = (remainAmt >= 0 ? dp[i][remainAmt] : 0) + dp[i - 1][j];
            }
        }

        return dp[coins.Length][amount];
    }

    public static int coinChange_DP_BottomUp1(int[] coins, int amount)
    {

        int[][] dp = new int[coins.Length + 1][];
        for (int x = 0; x < dp.Length; x++)
        {
            dp[x] = new int[amount + 1];

            //Array.Fill(amount + 1);
            dp[x] = dp[x].Select(s => (amount + 1)).ToArray();
        }

        //we have 0 nimimum way to make '0' amount
        for (int i = 0; i <= coins.Length; i++)
            dp[i][0] = 0;

        for (int i = 1; i <= coins.Length; i++)
        {
            for (int j = 1; j <= amount; j++)
            {
                //exclude current coin
                int MinCountWithOutCoin = dp[i - 1][j]; //exclude current coin, the value is previous coin row

                //include current coin, the total amount is reduced by current coin
                int remainAmt = j - coins[i - 1];
                int MinCountWithCoin = int.MaxValue;
                if(remainAmt >= 0)
                    MinCountWithCoin = dp[i][remainAmt] + 1;

                dp[i][j] = Math.Min(MinCountWithCoin, MinCountWithOutCoin);
            }
        }

        return dp[coins.Length][amount] > (amount) ? -1 : dp[coins.Length][amount];
    }

    public static IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        Dictionary<string, List<string>> d = new Dictionary<string, List<string>>();
        foreach (IList<string> t in tickets)
        {
            string From = t.First();
            string To = t.Last();

            if (!d.ContainsKey(From))
                d.Add(From, new List<string>());

            d[From].Add(To);
        }
        foreach(var t in d)
            t.Value.Sort();
           
        IList<string> r = new List<string>();
           
        Stack<string> s = new Stack<string>();
        s.Push("JFK");

        while (s.Any())
        {
            while(d.ContainsKey(s.Peek()) && d[s.Peek()].Count > 0)
            {
                var temp = d[s.Peek()].First();
                d[s.Peek()].RemoveAt(0);
                s.Push(temp);
            }
            r.Add(s.Pop());
        }

        return r.Reverse().ToList();
    }


    private static void backtrack(int[] candidates, IList<int> combination, int start, int sumLeft)
    {
        if (sumLeft < 0)
            return;

        if (sumLeft == 0)
        {
            result1.Add(new List<int>(combination));
            return;
        }

        for (int i = start; i < candidates.Length; i++)
        {
            if (i > start && candidates[i - 1] == candidates[i])
                continue;

            combination.Add(candidates[i]);

            backtrack(candidates, combination, i + 1, sumLeft - candidates[i]);

            combination.RemoveAt(combination.Count - 1);
        }
    }

    static IList<IList<int>> result1 = null;

    public static int CombinationSum4_Bottom_Up(int[] coins, int amount)
    {
        /*
        int[] dp = new int[amount+1];
        dp[0] = 1;

        // give target j, where it can come from
        for(int i=1; i <= amount; i++) 
        {
            for (int j = 0; j < coins.Length; j++) 
            {
                var number = i - coins[j];
                if (number >= 0)
                {
                    //combinations[i] += combinations[number];
                    dp[i] += dp[number];
                }
            }
        }

        return dp[amount];

        /*          ---------> Amount
                        0  1  2  3  4  5
            |        =================
            |     0 |[1] 0  0  0  0  0
            |     1 |[1] 1  1  1  1  1 
            V     2 |[1] 1  1  2  2  3
            coins   5 |[1] 1  1  2  3 [4]
        */

        //O(N^2)
        int[][] dp = new int[coins.Length + 1][];
        for (int x = 0; x < dp.Length; x++)
        {
            dp[x] = new int[amount + 1];
        }

        //we have only 1 way to make '0' amount - i.e doing nothing
        for (int i = 0; i <= coins.Length; i++)
            dp[i][0] = 1;

        //with 0 coin, we need 1 to make '0' amount, we cant make other amount.
        //for (int j = 0; j <= amount; j++)
        //    dp[0][j] = (j ==0) ? 1 : 0;

        for (int i = 1; i <= coins.Length; i++)
        {
            for (int j = 1; j <= amount; j++)
            {
                //exclude current coin
                int countWithOutCoin = dp[i - 1][j]; //exclude current coin, the value is previous coin row

                //include current coin, the total amount is reduced by current coin
                int remainAmt = j - coins[i - 1];
                int countWithCoin = remainAmt >= 0 ? dp[i][remainAmt] : 0;

                //total
                dp[i][j] += countWithCoin + countWithOutCoin;
            }
        }

        return dp[coins.Length][amount];
    }

    public static void NextPermutation1(int[] nums)
    {
        //Find the *first descending* element from right
        int i = nums.Length - 2;
        while (i >= 0 && nums[i + 1] <= nums[i])
        {
            i--;
        }

        //Find element just bigger than *first descending* element from right, and swap them   
        if (i >= 0)
        {
            int j = nums.Length - 1;
            while (j >= 0 && nums[j] <= nums[i])
            {
                j--;
            }
            swap(nums, i, j);
        }

        //Reverse all elements from the element just bigger than *first descending* to then end
        reverse(nums, i + 1);
    }

    private static void reverse(int[] nums, int start)
    {
        int i = start, j = nums.Length - 1;
        while (i < j)
        {
            swap1(nums, i, j);
            i++;
            j--;
        }
    }

    private static void swap1(int[] nums, int i, int j)
    {
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
    }

    public static string GetPermutation(int n, int k)
    {
        //Permutation of n characters is !n
        //So calclate factorial for n +1 numbers
        int[] fact = new int[n + 1];
        fact[0] = 1;
        List<int> numbers = new List<int>();
        for(int x=1; x <= n; x++)
        {
            fact[x] = fact[x - 1] * x;
            numbers.Add(x);
        }

        k--;

        int numberGroupSize = fact[n] / n;
        int index = k / numberGroupSize;
        k = k % numberGroupSize;

        string result = "";
        while (n > 0)
        {
            result += numbers[index];
            numbers.RemoveAt(index);

            n--;
            if (n > 0)
            {
                numberGroupSize = fact[n] / n;
                index = k / numberGroupSize;
                k = k % numberGroupSize;
            }
        }

        return result;
    }

    public static IList<IList<int>> ThreeSum(int[] nums)
    {
        /*Array.Sort(nums);

        Dictionary<int, IList<int>> dict = new Dictionary<int, IList<int>>();

        for (int x = 0; x < nums.Length; x++)
        {
            if (!dict.ContainsKey(nums[x]))
                dict.Add(nums[x], new List<int>());

            dict[nums[x]].Add(x);
        }

        int total = 0;
        IList<IList<int>> result = new List<IList<int>>();
        for (int x = 0; x < nums.Length; x++)
        {
            int sum = total - nums[x];
                
            for (int y = x+1; y < nums.Length; y++)
            {
                int diff = sum - nums[y];

                if (dict.ContainsKey(diff) && dict[diff].Any(w=> w > y))
                {
                    List<int> temp = new List<int>();
                    temp.Add(nums[x]);
                    temp.Add(nums[y]);
                    temp.Add(diff);
                    result.Add(temp);
                }
            }
        }

        return result;
        */

        HashSet<KeyValuePair<int, int>> found = new HashSet<KeyValuePair<int, int>>();

        int total = 0;
        IList<IList<int>> result = new List<IList<int>>();
        for (int x = 0; x < nums.Length; x++)
        {
            int sum = total - nums[x];

            HashSet<int> dict = new HashSet<int>();
            for (int y = x + 1; y < nums.Length; y++)
            {
                int diff = sum - nums[y];

                if (dict.Contains(diff))
                {
                    int v1 = Math.Min(nums[x], Math.Min(diff, nums[y]));
                    int v2 = Math.Max(nums[x], Math.Max(diff, nums[y]));

                    if (found.Add(new KeyValuePair<int, int>(v1, v2)))
                    {
                        List<int> temp = new List<int>();
                        temp.Add(nums[x]);
                        temp.Add(nums[y]);
                        temp.Add(diff);
                        result.Add(temp);
                    }
                }

                dict.Add(nums[y]);
            }
        }

        return result;
    }

    public static int WidthOfBinaryTree(TreeNode root)
    {
        int maxWidth = 0;
        Queue<KeyValuePair<TreeNode, int>> queue = new Queue<KeyValuePair<TreeNode, int>>();
        queue.Enqueue(new KeyValuePair<TreeNode, int>(root, 0));

        while(queue.Any())
        {
            int count = queue.Count;

            bool first = true;
            int firstVal = 0;
            int lastVal = 0;
            while (count > 0)
            {
                count--;
                KeyValuePair<TreeNode, int> temp = queue.Dequeue();
                    
                if (first)
                {
                    firstVal = temp.Value;
                    first = false;
                }
                if (count == 0) //last value
                    lastVal = temp.Value;
                    
                if (temp.Key.left != null)
                    queue.Enqueue(new KeyValuePair<TreeNode, int>(temp.Key.left, 2 * temp.Value + 1));
                if (temp.Key.right != null)
                    queue.Enqueue(new KeyValuePair<TreeNode, int>(temp.Key.right, 2 * temp.Value + 2));
            }

            maxWidth = Math.Max(maxWidth, lastVal - firstVal + 1);
        }

        return maxWidth;
    }


        public static TreeNode RecoverFromPreorder(string S)
        {
            int n = 0;
            Queue<KeyValuePair<int, int>> queueValueDepths = new Queue<KeyValuePair<int, int>>();

            int dashCount = 0;
            while (n < S.Length)
            {
                if (S[n] != '-')
                {
                    string num = "";
                    while (n < S.Length && S[n] != '-')
                    {
                        num += S[n].ToString();
                        n++;
                    }
                    queueValueDepths.Enqueue(new KeyValuePair<int, int>(dashCount, int.Parse(num)));

                    if (n < S.Length && S[n] == '-')
                        n--;

                    dashCount = 0;
                }
                else
                {
                    dashCount++;
                }
                n++;
            }

            TreeNode root = BuildTree(-1, queueValueDepths);

            return root;
        }

        private static TreeNode BuildTree(int parentDepth, Queue<KeyValuePair<int, int>> queueValueDepths)
        {
            if (!queueValueDepths.Any())
                return null;

            int nodeDepth = queueValueDepths.Peek().Key;
            if (nodeDepth <= parentDepth)
            {
                return null;
            }

            int val = queueValueDepths.Dequeue().Value;
            TreeNode root = new TreeNode(val);

            root.left = BuildTree(nodeDepth, queueValueDepths);
            root.right = BuildTree(nodeDepth, queueValueDepths);
            
            return root;
        }

        public static double AngleClock(int hour, int minutes)
        {
            int totalTick = 60;

            int hourDisplacement = 0;

            //totalTick / (tick/min with in hour - 5) = 12, i.e for every 12 mins changes, hour hand moves by 1 tick

            hourDisplacement = minutes / 12;
            hour += hourDisplacement;



            return (double)0;
        }

        
        public static string AddBinary(string a, string b)
        {

            StringBuilder sb = new StringBuilder();
            int aLen = a.Length - 1;
            int bLen = b.Length - 1;

            //int maxLenString = Math.Max(aLen, bLen);
            //maxLenString--;

            int carry = 0;
            int sum = 0;
            while (aLen >= 0 || bLen >= 0)
            {
                int ae = (aLen >= 0) ? a[aLen --] : 0;
                int be = (bLen >= 0) ? b[bLen --] : 0;

                sum = (ae + be + carry) % 2;
                carry = (ae + be + carry) / 2;

                sb.Insert(0, sum);
            }
            if(carry > 0)
                sb.Insert(0, carry);

            return sb.ToString();
        }

        static IList<IList<int>> resultAllPaths = null;

        public static IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            resultAllPaths = new List<IList<int>>();

            Dictionary<int, IList<int>> abjList = new Dictionary<int, IList<int>>();
            int destVertex = graph.Length - 1;
            for (int r = 0; r < graph.Length; r++)
            {
                abjList.Add(r, new List<int>());
                for (int c = 0; c < graph[r].Length; c++)
                {
                    abjList[r].Add(graph[r][c]);
                }
            }

            DFSGraph(0, destVertex, abjList, new List<int>() { 0 });

            return resultAllPaths;
        }

        private static void DFSGraph(int startVertex, int destVertex, Dictionary<int, IList<int>> abjList, List<int> path)
        {
            if(startVertex == destVertex)
            {
                resultAllPaths.Add(new List<int>(path));
                return;
            }

            foreach(int vertex in abjList[startVertex])
            {
                path.Add(vertex);
                DFSGraph(vertex, destVertex, abjList, path);
                path.RemoveAt(path.Count - 1);
            }
        }


        public static bool WordBreak(string s, IList<string> wordDict)
        {

            HashSet<string> d = new HashSet<string>();
            foreach (string str in wordDict)
            {
                d.Add(str);
            }

            return WordBreak_Recursion(s, d, 0);

        }

        private static bool WordBreak_Memo_Recursion(string s, HashSet<string> wordDict, bool?[] memo, int start)
        {
            if (start == s.Length)
                return true;

            if (memo[start] != null)
                return true;

            for (int end = start + 1; end < s.Length; end++)
            {
                if (wordDict.Contains(s.Substring(start, end - start)) && WordBreak_Memo_Recursion(s, wordDict, memo, end))
                {
                    memo[start] = true;
                    return true;
                }
            }

            memo[start] = false;
            return false;
        }

        private static bool WordBreak_Recursion(string s, HashSet<string> wordDict, int start)
        {
            bool[] memo = new bool[s.Length];


            if (start == s.Length)
                return true;

            for (int end = start + 1; end <= s.Length; end++)
            {
                if (wordDict.Contains(s.Substring(start, end-start)) && WordBreak_Recursion(s, wordDict, end))
                    return true;
            }

            return false;
        }

        static int sum = 0;
        public static int SumRootToLeaf(TreeNode root)
        {

            IList<int> lst = new List<int>();
            Sum(root, lst);
            return sum;
        }

        static void Sum(TreeNode node, IList<int> lst)
        {
            lst.Add(node.val);

            if(node.left == null && node.right ==null)
            {
                int po = 0;
                for (int i = lst.Count - 1; i >= 0; i--)
                {
                    sum += lst[i] * (int)Math.Pow(2, po);
                    po++;
                }
            }

            if (node.left != null) Sum(node.left, lst);

            if (node.right != null) Sum(node.right, lst);

            lst.RemoveAt(lst.Count - 1);
        }
        public static void swap(int[] nums, int s, int t)
        {
            int temp = nums[s];
            nums[s] = nums[t];
            nums[t] = temp;
        }

        public static void QuickSort(int[] nums, int start, int end, int k)
        {
            //if( start <= end && start < k ) {
            if (start < end)
            {
                int pIndex = Partition(nums, start, end);

                QuickSort(nums, start, pIndex - 1, k);
                QuickSort(nums, pIndex + 1, end, k);
            }
        }

        public static int Partition(int[] nums, int start, int end)
        {

            int wall = start;
            int pivot = nums[end];//first item as Pivot

            for (int i = start; i < end; i++)
            {
                if (nums[i] < pivot)
                {
                    swap(nums, wall, i);
                    wall = wall + 1;
                }
            }
            swap(nums, end, wall);
            return wall;
            
        }

       public class Interval {
            public int start;
            public int end;
            public Interval(int s, int e) { start = s; end = e; }
        }

        class comp : IComparer<Interval>
        {
            public int Compare(Interval x, Interval y)
            {
                return x.start-y.start;
            }
        }

        public bool canAttendMeetings(Interval[] intervals) {
            
    // Make a copy so we don't destroy the input, and sort by start time
            var sortedintervals = intervals.Select(m => new Interval(m.start, m.end))
                .OrderBy(m => m.start).ToList(); //O(nLogN)

            for (int i = 0; i < sortedintervals.Count - 1; i++)
            {
                if (sortedintervals[i].end > sortedintervals[i + 1].start)
                {
                    return false;
                }
            }
 
            return true;


        }

        static Dictionary<int, string> map = new Dictionary<int, string>();

        private static void fill()
        {
            map.Add(0, "Zero");
            map.Add(1, "One");
            map.Add(2, "Two");
            map.Add(3, "Three");
            map.Add(4, "Four");
            map.Add(5, "Five");
            map.Add(6, "Six");
            map.Add(7, "Seven");
            map.Add(8, "Eight");
            map.Add(9, "Nine");
            map.Add(10, "Ten");
            map.Add(11, "Eleven");
            map.Add(12, "Twelve");
            map.Add(13, "Thirteen");
            map.Add(14, "Fourteen");
            map.Add(15, "Fifteen");
            map.Add(16, "Sixteen");
            map.Add(17, "Seventeen");
            map.Add(18, "Eighteen");
            map.Add(19, "Nineteen");
            map.Add(20, "Twenty");
            map.Add(30, "Thirty");
            map.Add(40, "Forty");
            map.Add(50, "Fifty");
            map.Add(60, "Sixty");
            map.Add(70, "Seventy");
            map.Add(80, "Eighty");
            map.Add(90, "Ninety");
        }
        

        public static string numberToWords(int num)
        {
            fill();
            StringBuilder sb = new StringBuilder();

            if (num == 0)
            {
                return map[0];
            }

            if (num >= 1000000000) //Billion
            {
                int numBillion = num / 1000000000;
                sb.Append(convert(numBillion) + " Billion");
                num = num % 1000000000;
            }

            if (num >= 1000000) //Million
            {
                int numMillion = num / 1000000;
                sb.Append(convert(numMillion) + " Million");
                num = num % 1000000;
            }

            if (num >= 1000) //Thousand
            {
                int numThousand = num / 1000;
                sb.Append(convert(numThousand) + " Thousand");
                num = num % 1000;
            }
            
            if (num > 0)
            {
                sb.Append(convert(num));
            }

            return sb.ToString().Trim();
        }

        public static string convert(int num)
        {

            StringBuilder sb = new StringBuilder();

            if (num >= 100)
            {
                int numHundred = num / 100;
                sb.Append(" " + map[numHundred] + " Hundred");
                num = num % 100;
            }

            if (num > 0)
            {
                if (num > 0 && num <= 20)
                {
                    sb.Append(" " + map[num]);
                }
                else
                {
                    int numTen = num / 10;
                    sb.Append(" " + map[numTen * 10]);

                    int numOne = num % 10;
                    if (numOne > 0)
                    {
                        sb.Append(" " + map[numOne]);
                    }
                }
            }

            return sb.ToString();
        }


        static string[] LESS_THAN_20 = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static string[] TENS = new string[] { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        static string[] THOUSANDS = new string[] { "", "Thousand", "Million", "Billion" };

        public static string NumberToWords(int num)
        {
            if (num == 0) return "Zero";

            int i = 0;
            string words = "";

            while (num > 0)
            {
                if (num % 1000 != 0)
                    words = helper(num%1000) + THOUSANDS[i] + " " + words;
                num /= 1000;
                i++;
            }

            return words.Trim();
        }

        private static string helper(int num)
        {
            if (num == 0)
                return "";
            else if (num < 20)
                return LESS_THAN_20[num] + " ";
            else if (num < 100)
                return TENS[num / 10] + " " + helper(num % 10);
            else
                return LESS_THAN_20[num / 100] + " Hundred " + helper(num % 100);
        }


        public static bool BackspaceCompare(string S, string T)
        {
            int skipS = 0, skipT = 0;
            int i = S.Length - 1, j = T.Length - 1;

            while (i >= 0 || j >= 0)
            {
                if (i >= 0 && (S[i] == '#' || skipS > 0))
                {
                    if (S[i] == '#') skipS++;
                    else skipS--;
                    i--;
                    continue;
                }
                if (j >= 0 && (T[j] == '#' || skipT > 0))
                {
                    if (T[j] == '#') skipT++;
                    else skipT--;
                    j--;
                    continue;
                }

                if (i < 0 || j < 0 || S[i] != T[j])
                    return false;
                i--;
                j--;
            }

            return true;

        }


        public class DupKey : IComparer<int>
        {
            public int Compare(int left, int right)
            {
                //return left < right ? 1 : -1;
                //return right.CompareTo(left);


                int result = left.CompareTo(right);

                if (result == 0)
                    return 1;   // Handle equality as beeing greater
                else
                    return result;
            }
        }

        public static int subarraySum(int[] nums, int k)
        {
            int count = 0;
            /* 
            //O(N^3), Space O(1)
            for (int start = 0; start < nums.Length; start++)
            {
                for (int end = start; end < nums.Length; end++)
                {
                    int sum = 0;
                    for (int i = start; i <= end; i++)
                        sum += nums[i];
                    if (sum == k)
                        count++;
                }
            }
            return count;
            */

            //O(N^2), Space O(N)
            /*int[] sum = new int[nums.Length];
            sum[0] = 0;
            //store cumulative sum till element into array
            for (int i = 1; i < nums.Length; i++)
                sum[i] = sum[i - 1] + nums[i];

            for (int start = 0; start < nums.Length; start++)
            {
                for (int end = start; end < nums.Length; end++)
                {
                    int prevSumTillStart = 0;
                    //the SUM of all elements from start to end is the diff between, 'end' element and 'start -1' element 
                    if (start - 1 >= 0)
                        prevSumTillStart = sum[start - 1];

                    if (sum[end] - prevSumTillStart == k)
                        count++;
                }
            }
            return count;
            */

            //O(N^2), Space O(N)
            int[] sum = new int[nums.Length];
            sum[0] = nums[0];
            //store cumulative sum including the element into array
            for (int i = 1; i < nums.Length; i++)
                sum[i] = sum[i - 1] + nums[i];

            for (int start = 0; start < nums.Length; start++)
            {
                for (int end = start; end < nums.Length; end++)
                {
                    //**** find cumulative sum between 'start' and 'end' elements ******
                    //Since every element in 'cumsum' array stores, cumulative sum so far + the element value itself
                    //so we need to add the element value back
                    if (sum[end] - sum[start] + nums[start] == k)
                        count++;
                }
            }
            //return count;

            /*
            //O(N^2), Space O(1)
            int count = 0;
            for (int start = 0; start < nums.Length; start++)
            {
                int sum = 0;
                for (int end = start; end < nums.Length; end++)
                {
                    sum += nums[end];
                    if (sum == k)
                        count++;
                }
            }
            return count;*/


            //O(N), Space O(N)
            //We use Dictionary to find the 'PreviousSum'. How?
            //Every CurrentSum we store in Dictionary with number of times it occured in cumulative sum.------------
            //if CurrentSum not present in dictionary, then add it with 1 occurance value
            //if CurrentSum is present in dictionary, then increment the occurance by 1
            //-------------------------------------------
            //CurrentSum - PreviousSum = k, i.e PreviousSum = (CurrentSum - K)
            //if '(CurrentSum - K)' is found on dictionary, that means, 
            //there exist a previousSum, whose difference with currentSum is k

            //    [0,3,4,7 ], Sum [0,3,7,14]
            //     -----
            //       ---
            //Number of occurance(2) of such PreviousSum(0), determines the number of subarrays 
            //with sum k has occured upto the current index.
            Dictionary<int, int> d = new Dictionary<int, int>();
            int cumSum = 0;
            count = 0;

            //Initially Add Sum 0 with 1 occurance
            d.Add(0, 1);
            for (int x=0; x < nums.Length; x++)
            {
                cumSum += nums[x];

                if (d.ContainsKey(cumSum - k))
                    count += d[cumSum - k];

                if (!d.ContainsKey(cumSum))
                    d.Add(cumSum, 1);
                else
                    d[cumSum]++;
            }

            return count;
        }


        public static void Solve(char[][] board)
        {
            //any 'O' on the border doesn't qualify
            //any 'O' adjacent (vertically & horizontally) to other border 'O' doent qualify too.

            IList<KeyValuePair<int, int>> borderCells = new
                List<KeyValuePair<int, int>>();

            for (int r = 0; r < board.Length; r++)
            { 
                if (r == 0 || r == board.Length - 1)
                {
                    for (int c = 0; c < board[0].Length; c++)
                    {
                        if(board[r][c] == 'O')
                            borderCells.Add(new KeyValuePair<int, int>(r, c));
                    }
                }
                else
                {
                    for (int c = 0; c < board[0].Length; c++)
                    {
                        if(c ==0 && board[r][c] == 'O')
                            borderCells.Add(new KeyValuePair<int, int>(r, c));
                        if (c == board[0].Length - 1 && board[r][c] == 'O')
                            borderCells.Add(new KeyValuePair<int, int>(r, c));
                    }
                }
            }

            foreach(KeyValuePair<int, int> brderCell in borderCells)
            {
                int r = brderCell.Key;
                int c = brderCell.Value;
                if (board[r][c] == 'O')
                    Solve_DFS(board, r, c);
            }

            for (int r = 0; r < board.Length; r++)
                for (int c = 0; c < board[0].Length; c++)
                    if (board[r][c] == 'O')
                        board[r][c] = 'X';
                    else if (board[r][c] == 'B')
                        board[r][c] = 'O';
            
        }


        static void Solve_DFS(char[][] board, int r, int c)
        {
            //mark the cell as 'Connect to BORDER'
            board[r][c] = 'B';

            //left
            if(c > 0 && board[r][c - 1] == 'O')
                Solve_DFS(board, r, c - 1);

            //right
            if(c < board[0].Length - 1 && board[r][c + 1] == 'O')
                Solve_DFS(board, r, c + 1);

            //top
            if (r > 0 && board[r - 1][c] == 'O')
                Solve_DFS(board, r - 1, c);

            //bottom
            if(r < board.Length - 1 && board[r + 1][c] == 'O')
                Solve_DFS(board, r + 1, c);
        }

        public static void WallsAndGates(int[][] rooms)
        {
            for (int r = 0; r < rooms.Length; r++)
            {
                for (int c = 0; c < rooms[0].Length; c++)
                {
                    if (rooms[r][c] == 0)
                        WallsAndGates_DFS(rooms, r, c, rooms[r][c]);
                }
            }

            /*
            IList<KeyValuePair<int, int>> borderDoors = new
                    List<KeyValuePair<int, int>>();

            for (int r = 0; r < rooms.Length; r++)
            {
                if (r == 0 || r == rooms.Length - 1)
                {
                    for (int c = 0; c < rooms[0].Length; c++)
                    {
                        if (rooms[r][c] == 0)
                            borderDoors.Add(new KeyValuePair<int, int>(r, c));
                    }
                }
                else
                {
                    for (int c = 0; c < rooms[0].Length; c++)
                    {
                        if ((c == 0 || c == rooms[0].Length - 1) && rooms[r][c] == 0)
                            borderDoors.Add(new KeyValuePair<int, int>(r, c));
                    }
                }
            }

            foreach (KeyValuePair<int, int> brderDoor in borderDoors)
            {
                int r = brderDoor.Key;
                int c = brderDoor.Value;
                WallsAndGates_DFS(rooms, r, c, rooms[r][c]);
            }
            */
        }

        private static void WallsAndGates_DFS(int[][] rooms, int r, int c, int distance)
        {
            if (rooms[r][c] < distance)
                return;

            Console.WriteLine(rooms[r][c] + ":" + distance);

            rooms[r][c] = distance;

            //left
            if (c > 0 && rooms[r][c - 1] != -1 && rooms[r][c - 1] != 0)
                WallsAndGates_DFS(rooms, r, c - 1, distance + 1);

            //right
            if (c < rooms[0].Length - 1 && rooms[r][c + 1] != -1 && rooms[r][c + 1] != 0)
                WallsAndGates_DFS(rooms, r, c + 1, distance + 1);

            //Top
            if (r > 0 && rooms[r - 1][c] != -1 && rooms[r - 1][c] != 0)
                WallsAndGates_DFS(rooms, r - 1, c, distance + 1);

            //Down
            if (r < rooms.Length - 1 && rooms[r + 1][c] != -1 && rooms[r + 1][c] != 0)
                WallsAndGates_DFS(rooms, r + 1, c, distance + 1);
        }


        public static int ShortestDistance(int[][] grid)
        {

            //BFS  - best for finding shortest path

            int MinDistance = int.MaxValue;

            //get count of all Building that we must reach from an empty spot.        
            int buildingCount = 0;
            for (int r = 0; r < grid.Length; r++)
            {
                for (int c = 0; c < grid[0].Length; c++)
                {
                    if (grid[r][c] == 1)
                    {
                        buildingCount++;
                    }
                }
            }

            //If we cant reach all building from an empty spot, then this empty spot wont qualify (return 'MaxValue' as shortest distance)        
            for (int r = 0; r < grid.Length; r++)
            {
                for (int c = 0; c < grid[0].Length; c++)
                {
                    if (grid[r][c] == 0) //empty shot
                    {
                        int distance = ShortestDistance_BFS(grid, r, c, buildingCount);
                        MinDistance = Math.Min(MinDistance, distance);
                    }
                }
            }

            //no empty spot or empty wont qualify (not all buildings cant be reach from it)
            if (MinDistance == int.MaxValue)
                return -1;

            return MinDistance;
        }

        private static int ShortestDistance_BFS(int[][] grid, int r, int c, int buildingCount)
        {
            int[,] directions = new int[,] { { 0, -1 }, { 0, 1 }, { -1, 0 }, { 1, 0 } };

            Queue<KeyValuePair<int, int>> q = new Queue<KeyValuePair<int, int>>();
            bool[,] visited = new bool[grid.Length, grid[0].Length];
            visited[r, c] = true;

            q.Enqueue(new KeyValuePair<int, int>(r, c));
            
            int sumDepthToAllBuilding = 0;
            int buildingReachedSoFar = 0;
            int depthTraversed = 0;

            while (q.Any())
            {
                int qcount = q.Count;
                if(qcount > 0 ) depthTraversed++;
                
                while (qcount > 0)
                {   
                    KeyValuePair<int, int> temp = q.Dequeue();
                    int r1 = temp.Key;
                    int c1 = temp.Value;
                    //left
                    if (c1 > 0 && !visited[r1, c1 - 1])
                    {
                        visited[r1, c1 - 1] = true;
           
                        if (grid[r1][c1 - 1] == 0)
                            q.Enqueue(new KeyValuePair<int, int>(r1, c1 - 1));
                        else if (grid[r1][c1 - 1] == 1)
                        {
                            sumDepthToAllBuilding += depthTraversed;
                            buildingReachedSoFar++;
                        }
                    }

                    //right
                    if (c1 < grid[0].Length - 1 && !visited[r1, c1 + 1])
                    {
                        visited[r1, c1 + 1] = true;
                        if (grid[r1][c1 + 1] == 0)
                            q.Enqueue(new KeyValuePair<int, int>(r1, c1 + 1));
                        else if (grid[r1][c1 + 1] == 1)
                        {
                            sumDepthToAllBuilding += depthTraversed;
                            buildingReachedSoFar++;
                        }
                    }
                    //top
                    if (r1 > 0 && !visited[r1 - 1, c1])
                    {
                        visited[r1 - 1, c1] = true;
                        if (grid[r1 - 1][c1] == 0)
                            q.Enqueue(new KeyValuePair<int, int>(r1 - 1, c1));
                        else if (grid[r1 - 1][c1] == 1)
                        {
                            sumDepthToAllBuilding += depthTraversed;
                            buildingReachedSoFar++;
                        }
                    }

                    //bottom 
                    if (r1 < grid.Length - 1 && !visited[r1 + 1, c1])
                    {
                        visited[r1 + 1, c1] = true;
                        if (grid[r1 + 1][c1] == 0)
                            q.Enqueue(new KeyValuePair<int, int>(r1 + 1, c1));
                        else if (grid[r1 + 1][c1] == 1)
                        {
                            sumDepthToAllBuilding += depthTraversed;
                            buildingReachedSoFar++;
                        }
                    }
                    
                    qcount--;
                }
            }

            return (buildingReachedSoFar == buildingCount) ? sumDepthToAllBuilding : int.MaxValue;
        }

        class UnionFind
        {
            public int[] parent;

            public UnionFind(int n)
            {
                parent = new int[n];
                for (int node = 0; node < n; node++)
                {
                    parent[node] = node;
                }
            }

            public void union(int A, int B)
            {
                int parentA = find(A);
                int parentB = find(B);
                parent[parentA] = parentB;
                //parent[parentB] = parentA;
            }

            private int find(int A)
            {
                while (parent[A] != A)
                {
                    A = parent[A];
                }
                return A;
            }
        }

        public static int MakeConnected(int n, int[][] connections)
        {
            //if there are less than (n-1) connections less than we CANT connect all computers 
            if ((n - 1) > connections.Length)
                return -1;

            UnionFind uf = new UnionFind(n);
            foreach (int[] conn in connections)
            {
                uf.union(conn[0], conn[1]);
            }
            int notConnectedTerminals = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == uf.parent[i])
                    notConnectedTerminals++;
            }
            return notConnectedTerminals - 1;
        }

        static void dfs(int node, bool[] visitedNodes, Dictionary<int, IList<int>> dict)
        {
            if (!dict.ContainsKey(node))// || visitedNodes[node])
                return;

            visitedNodes[node] = (dict[node].Count > 0) ? true : false;
            
            foreach (int neighbourNode in dict[node])
            {
                if (!visitedNodes[neighbourNode])
                    dfs(neighbourNode, visitedNodes, dict);
            }
        }

        private static int Sqrt(int num)
        {
            if (num <= 1)
                return num;

            int lo = 2;
            int hi = num;// / 2;
            while (lo < hi)
            {
                int mid = lo + ((hi - lo) / 2);

                long midmid = (long)mid * mid;
    
                if (midmid < num)
                    lo = mid + 1;
                else if (midmid > num)
                    hi = mid;
                else
                    return mid;
            }

            return lo - 1;


            /*
            if (x < 2)
                return x;

            long num;
            int pivot, left = 2, right = x / 2;
            while (left <= right)
            {
              pivot = left + (right - left) / 2;
              num = (long)pivot * pivot;
              if (num > x)
                    right = pivot - 1;
              else if (num < x)
                    left = pivot + 1;
              else
                    return pivot;
            }

            return right; 
            */
        }

        public static int FindJudge(int N, int[][] trust)
        {
            //only 1 people with 0 trust, i.e 1 is the judge
            if (N == 1 && trust.Length == 0)
            {
                return 1;
            }

            Dictionary<int, int> trusted = new Dictionary<int, int>();
            Dictionary<int, int> trustedBy = new Dictionary<int, int>();
            for (int x = 0; x < trust.Length; x++)
            {
                int tBy = trust[x][0];
                int t = trust[x][1];

                //Maintain all trust by 
                if (!trustedBy.ContainsKey(tBy))
                    trustedBy.Add(tBy, 1);
                else
                    trustedBy[tBy]++;

                if (!trusted.ContainsKey(t))
                    trusted.Add(t, 1);
                else
                    trusted[t]++;

                /*
                //if a 'trusted by', is there in trusted list, then this 'trusted by' is not a judge, so remve him 
                if (trusted.ContainsKey(tBy))
                {
                    trusted.Remove(tBy);
                }
                //if a trusted is not there on 'trusted by' list, then add him a truested list
                if (!trustedBy.ContainsKey(t))
                {
                    if (!trusted.ContainsKey(t))
                        trusted.Add(t, 1);
                    else
                        trusted[t]++;
                }*/
            }

            for (int x = 1; x <= N; x++)
            {
                //must be trusted by N-1 members to qualify
                if (trusted.ContainsKey(x) && trusted[x] == N - 1 && !trustedBy.ContainsKey(N))
                    return x;
            }

            return -1;
        }

        public static string RemoveKdigits(string num, int k)
        {
            char[] stack = new char[num.Length];
            int stackTopIndex = -1;
            foreach (char c in num)
            {
                while (k > 0 && stackTopIndex > -1 && c < stack[stackTopIndex])
                {
                    stackTopIndex --;
                    k--;
                }
                stack[stackTopIndex + 1] = c;
                stackTopIndex ++;
            }

            /* remove the remaining digits from the tail. */
            for (int i = 0; i < k; i++)
            {
                stackTopIndex --;
            }

            StringBuilder ret = new StringBuilder();
            bool leadingZero = true;
            for(int x=0; x <= stackTopIndex; x++)
            {
                char digit = stack[x];
                if (leadingZero && digit == '0')
                    continue;
                leadingZero = false;

                ret.Append(digit);
            }

            /* return the final string  */
            if (ret.Length == 0)
                return "0";
            return ret.ToString();
        }

        static int Kadene(int[] A)
        {
            int maxSumSoFar = A[0];

            //over all max sum value
            int maxSum = A[0];
            //Console.WriteLine(maxSumSoFar);
            for (int x = 1; x < A.Length; x++)
            {
                maxSumSoFar = Math.Max(maxSumSoFar + A[x], A[x]);

                maxSum = Math.Max(maxSum, maxSumSoFar);
            }

            return maxSum;
        }

        class SortTest : IComparer<int>
        {   
            public int Compare(int x, int y)
            {
                //Ascending
                /*if (x == y)
                    return 0;
                else if (x < y)
                    return -1;
                else
                    return 1;*/
                return x - y;

                /*if (x == y)
                    return 0;
                else if (x > y)
                    return -1;
                else
                    return 1;*/
            }
        }

        static int[] a1111 = new int[] { 10, 2, 7, 12,14 };

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        private class DecreasingOrder : IComparer<KeyValuePair<char, int>>
        {
            public int Compare(KeyValuePair<char, int> x, KeyValuePair<char, int> y)
            {
                if (x.Value == y.Value)
                    return -1;
                else if (x.Value > y.Value)
                    return -1;
                else
                    return 1;
            }
        }

        public static string FrequencySort(string s)
        {

            Dictionary<char, int> map = new Dictionary<char, int>();
            

            foreach (char c in s)
            {
                if (map.ContainsKey(c))
                    map[c]++;
                else
                    map.Add(c, 1);
            }

            //SortedSet<KeyValuePair<char, int>> maxHeap = new SortedSet<KeyValuePair<char, int>>(map, new DecreasingOrder());

            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<char, int> pair in map.OrderByDescending(d => d.Value))
            {
                for (int x = 0; x < pair.Value; x++)
                    sb.Append(pair.Key);
            }

            /*foreach (KeyValuePair<char, int> pair in maxHeap)
            {
                Console.WriteLine(pair.Key);
                for (int x = 0; x < pair.Value; x++)
                    sb.Append(pair.Key);
            }*/

            return sb.ToString();
        }


        public static int[][] IntervalIntersection(int[][] A, int[][] B)
        {
            IList<int[]> result = new List<int[]>();
            int sA = 0, sB = 0;
            
            int iStart = 0;
            int iEnd = 0;
            while (sA < A.Length && sB < B.Length)
            {
                //Start is the max of 2 start
                iStart = Math.Max(A[sA][0], B[sB][0]);

                //end is the Min of 2
                iEnd = Math.Min(A[sA][1], B[sB][1]);

                //to qualify for intersection interval Start must be smaller or equal to end
                if (iStart <= iEnd)
                    result.Add(new int[] { iStart, iEnd });

                //A's end is bigger than B, then B should be earlier than A, so sB++
                if (A[sA][1] > B[sB][1])
                    sB++;
                else
                    sA++;
            }
            return result.ToArray();
        }

        static int index = 0;
        public static TreeNode BuildBSTUsingRange(int[] preorder, int lowerRange, int upperRange)
        {
            // if all elements from preorder are used
            // then the tree is constructed
            if (index == preorder.Length)
                return null;

            int val = preorder[index];
            // if the current element 
            // couldn't be placed here to meet BST requirements
            if (val < lowerRange || val > upperRange)
                return null;

            // place the current element
            // and recursively construct subtrees
            index++;

            TreeNode root = new TreeNode(val);

            root.left = BuildBSTUsingRange(preorder, lowerRange, val);
            root.right = BuildBSTUsingRange(preorder, val, upperRange);

            return root;
        }

        public static int min(List<List<int>> tri, int row, int currIndex, int[] dp)
        {
            if (row == tri.Count - 1)
            {
                return tri[row][currIndex];
            }
            int minLeft;
            if (dp[row + 1] != 0)
            {
                minLeft = dp[row + 1];
            }
            else
            {
                minLeft = min(tri, row + 1, currIndex, dp);
            }

            int minRight = min(tri, row + 1, currIndex + 1, dp);

            dp[row + 1] = minRight;

            return tri[row][currIndex] + Math.Min(minLeft, minRight);
        }

        public static int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            Queue<KeyValuePair<string, int>> queue = new Queue<KeyValuePair<string, int>>();
            queue.Enqueue(new KeyValuePair<string, int>(beginWord, 1));

            SortedSet<string> visited = new SortedSet<string>();
            visited.Add(beginWord);

            while (queue.Any())
            {
                KeyValuePair<string, int> temp = queue.Dequeue();

                string word = temp.Key;

                //if we have reached the end word return the ladder Length
                if (word.Equals(endWord))
                {
                    return temp.Value;
                }

                var tempWord = word.ToCharArray(); //convert to char array
                for (int i = 0; i < tempWord.Length; i++)
                {
                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        char cTemp = word[i];
                        if (tempWord[i] != c)
                        {
                            tempWord[i] = c;
                        }

                        string newWord = new string(tempWord);
                        
                        if (!visited.Contains(newWord) && wordList.Contains(newWord))
                        {
                            queue.Enqueue(new KeyValuePair<string, int>(newWord, temp.Value + 1));
                            wordList.Remove(newWord);

                            visited.Add(newWord);
                        }
                        tempWord[i] = cTemp;
                    }
                }
            }
            return 0;
        }

        public static int MaxUncrossedLinesRecursion(int[] A, int[] B, int indexA, int indexB, int[][] dp)
        {
            if (indexA == -1 || indexB == -1)
                return 0;

            if (dp[indexA][indexB] != -1)
                return dp[indexA][indexB];

            if (A[indexA] == B[indexB])
            {
                return dp[indexA][indexB] = 1 + MaxUncrossedLinesRecursion(A, B, indexA - 1, indexB - 1, dp);
            }
            else
            {
                return dp[indexA][indexB] = Math.Max(MaxUncrossedLinesRecursion(A, B, indexA - 1, indexB, dp),
                                MaxUncrossedLinesRecursion(A, B, indexA, indexB - 1, dp));
            }
        }
        public static int MaxUncrossedLines(int[] A, int[] B)
        {
            int[][] dp = new int[A.Length + 1][];
            for (int r = 0; r < A.Length + 1; r++)
            {
                dp[r] = new int[B.Length + 1];
                for (int c = 0; c < B.Length + 1 ; c++)
                {
                    //dp[r][c] = -1;
                    dp[r][c] = 0;
                }
            }

            for (int r = 1; r <= A.Length; r++)
            {
                for (int c = 1; c <= B.Length; c++)
                {
                    if (A[r-1] == B[c-1])
                    {
                        dp[r][c] = 1 + dp[r - 1][c - 1];
                    }
                    else
                    {
                        dp[r][c] = Math.Max(dp[r - 1][c], dp[r][c - 1]);
                    }
                }
            }

            int val00000000000 = dp[A.Length][B.Length];

            int val11111111111111 =  MaxUncrossedLinesRecursion(A, B, A.Length -1, B.Length -1, dp);

            //int lastMatchedRowIndex = A.Length - 1;
            int lastMatchedColIndex = B.Length - 1;
            int count = 0;

            for (int r = A.Length - 1; r >= 0 ; r--)
            {
                for (int c = lastMatchedColIndex; c >= 0; c--)
                {
                    if (A[r] == B[c])
                    {
                        //dp[r][c]++;
                        count++;
                        lastMatchedColIndex --;
                        break;
                    }
                }
            }
            /*
            for (int r = 0; r < A.Length; r++)
            {
                for (int c = lastMatchedColIndex; c < B.Length; c++)
                {
                    if (A[r] == B[c])
                    {
                        //dp[r][c]++;
                        count++;
                        lastMatchedColIndex = c + 1;
                        break;
                    }
                }
            }*/
            return count;
        }

        public static int CharacterReplacement(string s, int k)
        {

            int MaxCount = 0;

            int st = 0;
            int end = 0;

            Dictionary<char, int> winCharCount = new Dictionary<char, int>();

            int mx = 0;

            while (st <= end && end < s.Length)
            {
                char c = s[end];

                if (winCharCount.ContainsKey(c))
                    winCharCount[c]++;
                else
                    winCharCount.Add(c, 1);

                mx = Math.Max(mx, winCharCount[c]);

                while (st < s.Length && end - st - mx + 1 > k)
                {
                //while (!isBalancedWindow(winCharCount, k))
                //{
                    char tc = s[st];
                    st++;

                    //if imbalance - reduce window till it is balanced again
                    winCharCount[tc]--;

                    mx = Math.Max(mx, winCharCount[c]);

                    if (winCharCount[tc] <= 0)
                        winCharCount.Remove(tc);
                }

                MaxCount = Math.Max(MaxCount, end - st + 1);

                //if balance - increase window
                end++;
            }

            return MaxCount;
        }

        private static bool isBalancedWindow(Dictionary<char, int> winCharCount, int k)
        {
            //check for all char count 
            if (winCharCount.Keys.Count == 1)
                return true;
            else if (winCharCount.Keys.Count == 2)
            {
                if (k == 0)
                    return false;

                int count = 0;
                foreach (char key in winCharCount.Keys)
                {
                    if (winCharCount[key] <= k)
                        count++;
                }
                return count > 0;
            }
            else
                return false;
        }

        public static bool PossibleBipartition(int N, int[][] dislikes)
        {
            List<int>[] vertices = new List<int>[N + 1];
            for (int x = 0; x <=N ; x++)
            {
                vertices[x] = new List<int>();
            }

            for (int x=0; x< dislikes.Length; x++) 
            {
                vertices[dislikes[x][0]].Add(dislikes[x][1]);
            }

            int[] colorArr = new int[N + 1];
            for (int i = 1; i <= N; i++)
                colorArr[i] = -1;

            for (int i = 1; i <= N; i++)
                if (colorArr[i] == -1)
                    if (isBipartiteUtil(i, colorArr, vertices) == false)
                        return false;
            return true;
        }

        public static bool isBipartiteUtil(int src, int[] colorArr, List<int>[] vertices)
        {
            colorArr[src] = 1;
            // Create a queue (FIFO) of vertex numbers and enqueue source vertex for BFS traversal
            Queue<int> q = new Queue<int>();
            q.Enqueue(src);
            // Run while there are vertices in queue
            // (Similar to BFS)
            while (q.Any())
            {
                // Dequeue a vertex from queue
                int u = q.Dequeue();

                foreach (var v in vertices[u])
                {
                    // An edge from u to v exists and destination v is not colored 
                    if (colorArr[v] == -1)
                    {
                        // Assign alternate color to this 
                        // adjacent v of u 
                        colorArr[v] = 1 - colorArr[u];
                        q.Enqueue(v);
                    }
                    // An edge from u to v exists and
                    // destination v is colored with same
                    // color as u
                    else if (colorArr[v] == colorArr[u])
                        return false;
                }
            }
            // If we reach here, then all adjacent vertices
            // can be colored with alternate color
            return true;
        }

        public static bool CanFinish(int numCourses, int[][] prerequisites)
        {
            IList<int> indegree = new List<int>();
            IList<int> outdegree = new List<int>();

            for(int x=0; x< prerequisites.Length; x++)
            {
                //indegree[y].
            }

            return false;
        }

        public int[][] KClosest(int[][] points, int K)
        {
            int[] eQdistance = new int[points.Length];

            for (int x = 0; x < points.Length; x++)
            {
                eQdistance[x] = (points[x][0] * points[x][0] + points[x][1] * points[x][1]);
            }

            Array.Sort(eQdistance);
            int distAtK = eQdistance[K - 1];

            int[][] ans = new int[K][];
            int t = 0;
            for (int i = 0; i < points.Length; i++)
                if ((points[i][0] * points[i][0] + points[i][1] * points[i][1]) <= distAtK)
                {
                    ans[t++] = points[i];
                }
            return ans;
        }

        public class ListNode
        {
              public int val;
              public ListNode next;
              public ListNode(int x) { val = x; }
         }

        public static void DeleteNode(ref ListNode node)
        {
            if (node.next != null)
            {
                node.val = node.next.val;
                node.next = node.next.next;
            }
            else
            {
                node = node.next;
            }
        }

        class CityTravelCostDiffComparer : IComparer<int[]>
        {
            public int Compare(int[] x, int[] y)
            {
                //Compare diff between city A and B travel in all elemnets and order them ascending
                int diffX = (x[0] - x[1]);
                int diffY = (y[0] - y[1]);
                return diffX.CompareTo(diffY);
            }
        }

        public static int TwoCitySchedCost(int[][] costs)
        {   
            Array.Sort<int[]>(costs, new CityTravelCostDiffComparer());

            int minAmount = 0;
            for(int i=0; i < costs.Length; i++)
            {
                if (i < costs.Length / 2)
                    minAmount += costs[i][0];
                else 
                    minAmount += costs[i][1];
            }
            return minAmount;
        }

        public static IList<IList<int>> Subsets(int[] nums)
        {
            IList<IList<int>> subsets = new List<IList<int>>();
            subsets.Add(new List<int>());

            for (int x = 0; x < nums.Length; x++)
            {
                List<List<int>> newSubsets = new List<List<int>>();

                //Merge nums[x] into previous 'subsets' and generate new sets
                foreach (List<int> curr in subsets)
                {
                    newSubsets.Add(curr.Concat(new List<int>() { nums[x] }).ToList());
                }

                foreach (List<int> curr in newSubsets)
                {
                    subsets.Add(curr);
                }
            }
            
            return subsets;
        }

        private static void Recursion(int[] nums, int start, IList<int> combination, IList<IList<int>> subsets)
        {
            if (start == nums.Length - 1)
            {
                subsets.Add(new List<int>(combination));
                return;
            }

            Recursion(nums, start + 1, combination, subsets);

            combination.Add(nums[start + 1]);
            Recursion(nums, start + 1, combination, subsets);
            combination.RemoveAt(combination.Count - 1);
        }
        
        class student : IComparer
        {
            public student(string name, int age)
            {
                Age = age;
                Name = name;
            }
            public int Age { get; set; }
            public string Name { get; set; }

            public int Compare(object x, object y)
            {
                student student1 = (student)x;
                student student2 = (student)y;

                return student1.Age - student2.Age;
            }
        }

        class BoxEqualityComparer : IEqualityComparer<student>
        {
            public bool Equals(student b1, student b2)
            {
                if (b2 == null && b1 == null)
                    return true;
                else if (b1 == null | b2 == null)
                    return false;
                else if (b1.Age == b2.Age && b1.Name == b2.Name)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(student bx)
            {
                //return bx.GetHashCode();
                return bx.Age.GetHashCode() ^ bx.Name.GetHashCode();
            }
        }

        public class EqualityProjectionComparer<T>
        {
            public static AutoEqualityComparer<T, K> Create<K>(Func<T, K> projection)
            {
                return new AutoEqualityComparer<T, K>(projection);
            }
        }

        public class AutoEqualityComparer<T, K> : IEqualityComparer<T>
        {
            private readonly Func<T, K> _projection;

            public AutoEqualityComparer(Func<T, K> projection)
            {
                _projection = projection;
            }

            public virtual bool Equals(T x, T y)
            {
                if (x == null && y == null)
                {
                    return true;
                }
                if (x == null)
                {
                    return false;
                }
                if (y == null)
                {
                    return false;
                }

                var xData = _projection(x);
                var yData = _projection(y);

                return EqualityComparer<K>.Default.Equals(xData, yData);
            }

            public virtual int GetHashCode(T obj)
            {
                if (obj == null)
                {
                    return 0;
                }

                var objData = _projection(obj);

                return EqualityComparer<K>.Default.GetHashCode(objData);
            }
        }

        class GenericCompare<T> : IEqualityComparer<T> where T : class
        {
            private Func<T, object> _expr { get; set; }
            public GenericCompare(Func<T, object> expr)
            {
                this._expr = expr;
            }
            public bool Equals(T x, T y)
            {
                var first = _expr.Invoke(x);
                var sec = _expr.Invoke(y);
                if (first != null && first.Equals(sec))
                    return true;
                else
                    return false;
            }
            public int GetHashCode(T obj)
            {
                //return obj.GetHashCode();
                if (obj == null)
                {
                    return 0;
                }
                var objData = _expr(obj);
                return EqualityComparer<object>.Default.GetHashCode(objData);
            }
        }

        enum Animal
        {
            [Description("Giant Panda")]
            NotSet = 0,

            [Description("Giant Panda")]
            GiantPanda = 1,

            [Description("Lesser Spotted Anteater")]
            LesserSpottedAnteater = 2
        }

        public static IList<T> GetValueFromDescription<T>(string description)
        {
            var typeUsage = "";
            var rx = new Regex(@"(?<=Nullable\<)[\w.]*(?=\>)");
            if (rx.IsMatch(typeUsage))
            {
                typeUsage = rx.Match(typeUsage).Value + "?";
            }


            var lst = new List<T>();

            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        lst.Add((T)field.GetValue(null));
                }


                //var attribute = Attribute.GetCustomAttribute(field,
                //    typeof(DescriptionAttribute)) as DescriptionAttribute;
                //if (attribute != null)
                //{
                //    if (attribute.Description == description)
                //        lst.Add((T)field.GetValue(null));
                //        //return (T)field.GetValue(null);
                //}
                //else
                //{
                //    if (field.FieldType == typeof(T))
                //    {
                //        lst.Add((T)field.GetValue(null));
                //    }
                //    //if (field.Name == description)
                //    //    lst.Add((T)field.GetValue(null));
                //        //return (T)field.GetValue(null);
                //}
            }



            throw new ArgumentException("Not found.", "description");
            // or return default(T);




        }


        public class StoredProcedureWriter<T> where T : Dictionary<string, object>
        {

            public bool Write(IEnumerable<T> items)
            {
                var xxx = (Dictionary<string, object>)items.First(); //.ToDictionary(x => (KeyValuePair<string, object>)x).Keys, ((Dictionary<string, object>)x).Values);

                //_storedProcedures.GetHigherThresholdRulesForARole(1);
                return true;
            }
        }
    }

}
