using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    class TowerOfHonoi
    {
        public static List<List<int>> tower_of_hanoi(int n)
        {
            List<List<int>> result = new List<List<int>>();

            towerOfHanoi(n, 1, 3, 2, result);

            return result;
        }
        static void towerOfHanoi(int n, int from_rod, int to_rod, int aux_rod, List<List<int>> result)
        {
            if (n == 1)
            {
                //cout << "Move disk 1 from rod " << from_rod << 
                //                    " to rod " << to_rod<<endl; 
                result.Add(new List<int>(new int[] { from_rod, to_rod }));
                return;
            }

            //move n-1 disk from A to B
            towerOfHanoi(n - 1, from_rod, aux_rod, to_rod, result);
            //cout << "Move disk " << n << " from rod " << from_rod <<
            //                            " to rod " << to_rod << endl; 

            result.Add(new List<int>(new int[] { from_rod, to_rod }));

            //move n-1 disk from B to C
            towerOfHanoi(n - 1, aux_rod, to_rod, from_rod, result);
        }
    }

    class power
    {
        //divide and conquer
        static int power_LogN(int x, int y) //O(LogN), space O(1) - 
        {
            int temp;
            if (y == 0)
                return 1;

            temp = power_LogN(x, y / 2);

            if (y % 2 == 0) //even
                return temp * temp;
            else
                return x * temp * temp;
        }
    }
    
    class StringsFromWildCard
    {
        public static string[] Get(string s)
        {
            IList<string> result = new List<string>();

            MakeString(s, 0, new StringBuilder(), result);

            return result.ToArray();
        }
        private static void MakeString(string s, int index, StringBuilder slate, IList<string> result)
        {
            if (index == s.Length)
            {
                result.Add(slate.ToString());
                return;
            }

            if(s[index] == '?')
            {
                slate.Append('0');
                MakeString(s, index + 1, slate, result);
                slate.Remove(slate.Length - 1, 1);


                slate.Append('1');
                MakeString(s, index + 1, slate, result);
                slate.Remove(slate.Length - 1, 1);
            }
            else
            {
                slate.Append(s[index]);
                MakeString(s, index + 1, slate, result);
                slate.Remove(slate.Length - 1, 1);
            }
        }
    }

    /*
     * Dynamic Programming: USE and (REUSE Subproblems - overlapping subproblem)
     * Two ways of Implementing DP Solutions - (Tabulation - Bottom up) and (recursion with Memoization - top down approach, shows REUSE subproblem)
     */

    class RecursionExercise
    {
        public static int Fibonacci_TopDown_Recursion(int n) //O(2^n) - for recursive Stack
        {

            if (n == 0 || n == 1)
            {
                return n;
            }

            int result = Fibonacci_TopDown_Recursion(n - 1) + Fibonacci_TopDown_Recursion(n - 2);
            return result;
        }


        //Tabulation - Bottom up approach
        public int Product1ToN(int n) //O(n), space O(1)
        {
            // We assume n >= 1
            int result = 1;
            for (int num = 1; num <= n; num++)
            {
                result *= num;
            }

            return result;
        }

        //Bottom up approach
        int power(int x, int y) //O(n), space O(1)
        {
            int power = 1;
            for (int num = 0; num < y; num++)
            {
                power *= x;
            }
            return power;
        }

        

        //https://www.youtube.com/watch?v=k4y5Pr0YVhg ***********
        // It takes O(n*m) time and O(n*m) space - where n is the size of amount and m is the number of items in denominations
        public static int CoinChange_Recursion_Memoization_TopDown(int money, int[] coins, int currentCoinIndex = 0)
        {
            /*
            string memoKey = string.Format("{0}-{1}", money, currentCoinIndex);
            if (_memo1.ContainsKey(memoKey))
            {
                Console.WriteLine(memoKey);
                return _memo1[memoKey];
            }

            // Base cases:
            // We hit the amount spot on. Yes!
            if (money == 0) //we got 1 combination
            {
                return 1;
            }
            // We overshot the amount left (used too many coins), so no combination
            if (money < 0)
            {
                return 0;
            }

            int ways = 0;
            //iterating all coins
            for (int coin = currentCoinIndex; coin < coins.Length; coin++)
            {
                int amtRemaining = money - coins[coin];
                ways += CoinChange_Recursion_Memoization_TopDown(amtRemaining, coins, coin);
            }

            // Save the answer in our memo, so we don't compute it again
            _memo1.Add(memoKey, ways);
            
            return ways;*/
            return 0;
        }

        //https://www.interviewcake.com/question/csharp/coin?course=fc1&section=dynamic-programming-recursion
        //https://www.youtube.com/watch?v=jaNZ83Q3QGc ****************
        //O(n∗m) time and O(n) additional space, where n is the amount of money and mm is the number of potential denominations.
        public static int CoinChange_BottomUp(int amountTotal, int[] coins)
        {
            // Tabulization
            int[] amountCombinations = new int[amountTotal + 1];
                // index of this array represent Amount and value represents the combinations of coins for the amount

            // Base case (If given value is 0)
            amountCombinations[0] = 1;

            // Pick all coins one by one and update the combinations[] values after the index 
            // greater than or equal to the value of the picked coin
            foreach (int coin in coins)
            {
                for (int amt = 1; amt < amountCombinations.Length; amt++)
                {
                    //if(Amount >= coin) then
                    //    combinations[Amount] += combinations[Amount - coin];

                    if (amt - coin >= 0) // >= coin)
                    {
                        amountCombinations[amt] += amountCombinations[amt - coin];

                        Console.WriteLine(string.Join(" ", amountCombinations));
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }
            }

            return amountCombinations[amountTotal];
        }

        public static IList<IList<int>> combinationSum(int[] candidates, int target) {
            IList<IList<int>> combinations = new List<IList<int>>();
            if(candidates.Length == 0)
                return combinations;

            back_track(candidates, combinations, target, new List<int>(), 0);

            return combinations;
        }

        private static void back_track(int[] coins, IList<IList<int>> combinations, int remaining, List<int> tmp, int index)
        {
            if (remaining == 0) //nothing left
            {
                combinations.Add(new List<int>(tmp));
            }
            else
            {
                for (int i = index; i < coins.Length; ++i)
                {
                    if (coins[i] <= remaining)
                    {
                        tmp.Add(coins[i]);

                        back_track(coins, combinations, remaining - coins[i], tmp, i);
                        
                        tmp.Clear();//.Remove(tmp.Count - 1);
                    }
                }
            }
        }


        /*
         Top down Approach using Recursion
         * Let’s say you have to climb N steps. You can jump 1 step, 3 steps or 5 steps at a time. 
         * How many ways are there to get to the top of the steps (i.e at n th step)?
         * ==============
         * Subproblems are 
         *      a[i] = a[i-1] + a[i-3] + a[i-5]
         * =======================
         * https://www.youtube.com/watch?v=5o-kdjv7FD0
        */

        public static int waysToClimb_Recursion(int n, int[] memo)
            //Time - O(3^n) -since each call branches out to three more calls, Space - O(n)
        {
            //base case
            if (n < 0)
                return 0;

            if (n == 0)
                //If we have O steps to go (we're currently standing on the step), are there zero paths to that step or one path?
                return 1;

            if (memo[n] > -1)
                return memo[n];

            memo[n] = waysToClimb_Recursion(n - 1, memo) + waysToClimb_Recursion(n - 3, memo) +
                      waysToClimb_Recursion(n - 5, memo);

            return memo[n];
        }

        //Sudoku Solver
        /*
         * O(4^n) time - recursively branch 4 times for each call to PrintWordFromPhoneDigit
         * Space O(n) - n is total number of characters in phoneNumberArray (on recursion stack)
         *  a with each from {d,e,f}
         *  b with each from {d,e,f}
         *  c with each from {d,e,f}
         */
        static void PrintWordFromPhoneDigit(int[] phoneNumberArray, int index, String prefix, HashSet<String> wordSet,
            List<String> results)
        {
            string[] phNumberLetters = {"", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"};
            
            /* If it's a complete word, print it. */
            if (index == phoneNumberArray.Length) //} && wordSet.Contains(prefix)) {
            {
                results.Add(prefix);
                return;
            }

            /* Get characters that match this digit. */
            int digit = phoneNumberArray[index];
            char[] letters = phNumberLetters[digit].ToCharArray();

            /* Go through all remaining options. */
            if (letters.Length > 0)
            {
                foreach (char letter in letters)
                {
                    PrintWordFromPhoneDigit(phoneNumberArray, index + 1, prefix + letter, wordSet, results);
                }
            }
        }


        //all permutations of size X
        /*
         * Time Complexity: O(n^n) time - n is length of numArray
         * Space Complexity: O(n). We use O(n) space both in the buffer allocation and on recursion stack
         */
        public static void allPermutationsOfSizeX(int[] numArray, int size, int index, String prefix, List<String> results)
        {
            if (index == size)
            {
                results.Add(prefix);
                return;
            }

            for (int x = 0; x < numArray.Length; x++)
            {
                if (prefix.IndexOf(numArray[x].ToString(), StringComparison.Ordinal) < 0)
                {
                    allPermutationsOfSizeX(numArray, size, index + 1, prefix + numArray[x], results);
                }
            }
        }
        public static void allPermutationsOfSizeX_1(int[] numArray, int[] buffer, int bufferIndex, bool[] isInBuffer) 
        {
            // termination case - buffer full    
            if (bufferIndex == buffer.Length)
            {
                Console.WriteLine(string.Concat(buffer.Where(s => (s > 0))));
                return;
            }   
            // find candidates that go into current buffer index    
            for (int i = 0; i < numArray.Length; i++)
            {        
                if (!isInBuffer[i]) 
                {            
                    // place candidate into buffer index            
                    buffer[bufferIndex] = numArray[i];
                    isInBuffer[i] = true;            
                    
                    // recurse to next buffer index            
                    allPermutationsOfSizeX_1(numArray, buffer, bufferIndex + 1, isInBuffer);            
                    isInBuffer[i] = false;        
                }    
            }
        }


        public static void allCombinationOfSizeX_1(int[] numArray, int[] buffer, int bufferIndex, int index)
        {
            // termination case - buffer full    
            if (bufferIndex == buffer.Length)
            {
                Console.WriteLine(string.Concat(buffer.Where(s => (s > 0))));
                return;
            }

            // find candidates that go into current buffer index    
            for (int i = index; i < numArray.Length; i++)
            {
                // place candidate into buffer index            
                buffer[bufferIndex] = numArray[i];

                // recurse to next buffer index            
                allCombinationOfSizeX_1(numArray, buffer, bufferIndex + 1, i + 1);
            }
        }


        public static void printAllSubsetsX_1(int[] numArray, int[] buffer, int bufferIndex, int index)
        {
            Console.WriteLine(string.Concat(buffer.Where(s=>(s>0))));

            // termination case - buffer full    
            if (bufferIndex == buffer.Length)
            {   
                return;
            }
            // find candidates that go into current buffer index    
            for (int i = index; i < numArray.Length; i++)
            {
                // place candidate into buffer index            
                buffer[bufferIndex] = numArray[i];

                // recurse to next buffer index            
                printAllSubsetsX_1(numArray, buffer, bufferIndex + 1, i + 1);
            }
        }
        














        class CountUniqueBST
        {
            public static long No_Of_binary_Tree(int N)
            {
                //List<string> result = new List<string>();
                int[] arr = new int[N];
                for (int x = 1; x <= N; x++)
                {
                    arr[x - 1] = x;
                }
                long result = helper(arr, 0, arr.Length - 1);
                return result;
            }

            private static long helper(int[] arr, int leftIndex, int rightIndex)
            {
                //Keep Left as Root Node, split the array into 2 parts ... left part till root and right part

                //base case (for 1 = we can make 1 unique BST, for 2 we can make 2 unique BST
                if (rightIndex - leftIndex < 2)
                {
                    return rightIndex - leftIndex + 1;
                }
                else
                {
                    long totalUniqueBST = 0;
                    for (int x = leftIndex; x <= rightIndex; x++)
                    {
                        long uniqueBST = 1;
                        //left
                        if (x > leftIndex) //if not the first item, then there is snother split on left side of range
                            uniqueBST *= helper(arr, leftIndex, x - 1);

                        //right
                        if (x + 1 <= rightIndex) //if not the last item on range, then there is another split on right side of range
                            uniqueBST *= helper(arr, x + 1, rightIndex);

                        totalUniqueBST += uniqueBST;
                    }

                    return totalUniqueBST;
                }
            }

        }

        private static void swap(int[] arr, int from , int to)
        {
            int temp = arr[from];
            arr[from] = arr[to];
            arr[to] = temp;
        }

        class expressionEval
        {
            public static string[] expression(string s, int target)
            {
                List<string> result = new List<string>();

                for (int i = 0; i < s.Length; i++)
                {
                    var startNumber = s.Substring(0, i + 1);
                    int startNumberVal = int.Parse(startNumber);
                    helper(s, new char[] { '*', '+' }, target, i + 1, result, startNumber, startNumberVal, startNumberVal);
                }

                return result.ToArray();
            }

            private static void helper(string digits, char[] operators, int target,
                int curDigitIndex,
                List<string> result,
                string slateText,
                int slateSum,
                int lastValue)
            {
                //number of Spaces to fill = digits.Length -1    (1_2_3_4)   
                //options to pick = operators.Length;

                //backtracking
                if (slateSum > target)
                    return;

                if (slateSum == target && curDigitIndex == digits.Length)
                {
                    result.Add(slateText.ToString());
                    return;
                }

                //base case
                if (curDigitIndex == digits.Length)
                    return;
                else
                {
                    for (int j = curDigitIndex; j < digits.Length; j++)
                    {
                        //group remaining characters by size 1,2,3.. (n-1) i.e {1},{12},{123} ... {123n-1} and call the group 'currentDigit'
                        string currentDigit = digits.Substring(curDigitIndex, j - curDigitIndex + 1);
                        int currentDigitVal = int.Parse(currentDigit);

                        for (int i = 0; i < operators.Length; i++)
                        {
                            int cal = 0;
                            int thisValue = 0;
                            if (operators[i] == '*')
                            {
                                cal = slateSum - lastValue + lastValue * currentDigitVal;
                                thisValue = lastValue * currentDigitVal;
                            }
                            else if (operators[i] == '+')
                            {
                                cal = slateSum + currentDigitVal;
                                thisValue = currentDigitVal;
                            }
                            /*else if (operators[i] == '-')
                            {
                                cal = slateSum - val;
                                thisValue = -val;
                            }*/

                            helper(digits, operators, target, j + 1, result, slateText + operators[i] + currentDigitVal, cal, thisValue);
                        }
                    }
                }
            }
        }

        class NQueen
        {
            //O(N^2) ---- N rows to fill for N queens, and scan N columns for validation
            public static string[][] NQueenProblem(int n)
            {
                List<string[]> result = new List<string[]>();
                helper(0, n, result, new List<int>());
                return result.ToArray();
            }

            private static void helper(int index, int n, List<string[]> result, List<int> slate)
            {
                //back track - Validation start from 3rd row onward
                if (index >= 2)
                {
                    int prevIndex = index - 1;
                    for (int x = prevIndex - 1; x >= 0; x--)
                    {
                        //column check -- no 2 queens on same column
                        if (slate[prevIndex] == slate[x])
                            return;

                        //diagonal check
                        int rowDiff = Math.Abs(prevIndex - x);
                        int colDiff = Math.Abs(slate[prevIndex] - slate[x]);
                        if (rowDiff == colDiff)
                            return;
                    }
                }

                //base case
                if (index == n) //all queens are placed
                {
                    StringBuilder sb = new StringBuilder();

                    //build string from slate
                    List<string> lst = new List<string>();
                    for (int x = 0; x < n; x++)
                    {
                        for (int y = 0; y < n; y++)
                        {
                            sb.Append((y == slate[x]) ? "Q" : "-");
                        }
                        lst.Add(sb.ToString());
                        sb.Clear();
                    }
                    result.Add(lst.ToArray());
                }
                else
                {
                    for (int c = 0; c < n; c++)
                    {
                        //set next column for queen
                        slate.Add(c);

                        //moving to next row for queen placement
                        helper(index + 1, n, result, slate);

                        //remove the column for queen
                        slate.RemoveAt(slate.Count - 1);
                    }
                }
            }


        }

        class SebsetSum
        {
            //IK question....

            //Combimation 
            public static bool check_if_sum_possible(long[] arr, long k)
            {
                var result = new List<string>();
                return helper(arr, 0, k, 0, new StringBuilder(), result, 0);
            }

            static bool helper(long[] arr, int index, long k, long slate, StringBuilder soFar, List<string> result, int count)
            {
                //base case
                if (slate == k && count > 0 )
                    return true;

                if (index == arr.Length)
                {
                    result.Add(soFar.ToString());
                }
                else
                {
                    //exclude
                    bool found = helper(arr, index + 1, k, slate, soFar, result, count);
                    if (found) return true;

                    //include
                    soFar.Append(arr[index]);
                    found = helper(arr, index + 1, k, slate + arr[index], soFar, result, count + 1);
                    soFar.Remove(soFar.Length - 1, 1);

                    if (found) return true;
                }

                return false;
            }

        }

        //78. Subsets
        class AllSubsetOrPowerset
        {
            //combination
            //2^0 + 2^1+ 2^2+ ...+ 2^n = 2^n
            public static string[] generate_all_subsets(string s)
            {
                //int noPlaces = s.Length;
                List<string> result = new List<string>();

                if (s == null)
                    return result.ToArray();

                helper(s, 0, new StringBuilder(), result);

                return result.ToArray();
            }

            static void helper(string s, int index, StringBuilder soFar, List<string> result)
            {
                //base case
                if (index == s.Length)
                {
                    result.Add(soFar.ToString());
                }
                else
                {
                    //without /exclude
                    helper(s, index + 1, soFar, result);

                    soFar.Append(s[index]);

                    //with/include
                    helper(s, index + 1, soFar, result);

                    soFar.Remove(soFar.Length - 1, 1);
                }
            }

        }

        //90	Subsets II
        class SubsetsII
        {
            public static IList<IList<int>> SubsetsWithDup(int[] nums)
            {
                //https://uplevel.interviewkickstart.com/resource/helpful-class-video-4869-45-337-0   4:40

                IList<IList<int>> result = new List<IList<int>>();

                Array.Sort(nums);
                DFS(nums, 0, result, new List<int>());

                return result;
            }

            static void DFS(int[] nums, int index, IList<IList<int>> result, List<int> slate)
            {
                //base case
                if (index == nums.Length)
                {
                    result.Add(new List<int>(slate));
                }
                else
                {
                    //since there are multiple copied on s[i], count them
                    int count = 1; //we have atleast 1
                    int j = index + 1;
                    while( j < nums.Length && nums[index] == nums[j])
                    {
                        count++;
                        j ++;
                    }

                    //exclude - jump to next unique number (index +  count) 
                    DFS(nums, index + count, result, slate);
                    
                    //When you make choice fo s[i], it is not about exclude or include
                    //it is about "how many times" to include
                    for (int i = 0; i < count; i++) //for each choice we make about how many "copied" of s[i] to incude
                    {
                        for (j = 0; j <= i; j++) //append those many copied of s[i] to slate
                        {
                            //choose c copied for S[i]
                            slate.Add(nums[index]);
                        }

                        DFS(nums, index + count, result, slate);

                        for (j = 0; j <= i; j++)
                        {
                            slate.RemoveAt(slate.Count - 1);
                        }
                    }
                }
            }
        }

        class PalindromicSubsetOfString
        {
            public static string[] PrintAllSubStrings(string str)
            {
                //n!/(n-k)!  -- > if K ~ 1 then its runtime is n!
                IList<string> result = new List<string>();

                helper(str, 0, result, new StringBuilder());
                
                return result.ToArray();
            }

            private static void helper(string str, int index, IList<string> result, StringBuilder slate)
            {
                if(index == str.Length)
                {
                    return;
                }

                for (int i = index; i < str.Length; i++)
                {
                    string currStr = str.Substring(index, i - index + 1);

                    result.Add(currStr);

                    //slate.Append(currStr);
                    helper(str, i + 1, result, slate);
                    //slate.Remove(slate.Length - 1, 1);
                }
            }
        }


        //46. Permutations
        class Permutation
        {
            public static IList<IList<int>> Permute(int[] nums)
            {
                IList<IList<int>> result = new List<IList<int>>();

                DFS(nums, 0, result, new List<int>());

                return result;
            }

            private static void DFS(int[] nums, int index, IList<IList<int>> result, IList<int> slate)
            {
                if (index == nums.Length)
                {
                    result.Add(new List<int>(slate.ToArray()));
                    return;
                }

                for (int i = index; i < nums.Length; i++)
                {
                    slate.Add(nums[i]);

                    swap(nums, i, index);
                    DFS(nums, index + 1, result, slate);
                    swap(nums, i, index);

                    slate.RemoveAt(slate.Count - 1);
                }
            }

            private static void swap(int[] nums, int i, int j)
            {
                int temp = nums[j];
                nums[j] = nums[i];
                nums[i] = temp;
            }
        }

        //47. Permutations II
        class PermutationII
        {
            //O(N!) -- If repetation is not allwowed
            public static string[] PrintAllPermutation(int[] arr)
            {
                //Repeation is NOT allowed ******
                //https://uplevel.interviewkickstart.com/resource/library-video-221    3:15
                

                IList<string> result = new List<string>();

                Helper(arr, 0, result, new StringBuilder());

                return result.ToArray();
            }

            private static void Helper(int[] arr, int index, IList<string> result, StringBuilder slate)
            {
                if (index == arr.Length)
                {
                    result.Add(slate.ToString());
                    return;
                }

                //hashset is used to remove duplicate set ******
                HashSet<int> hs = new HashSet<int>();
                for (int i = index; i < arr.Length; i++)
                {
                    if (!hs.Contains(arr[i]))
                    {
                        hs.Add(arr[i]);
                        
                        slate.Append(arr[i]);

                        swap(arr, i, index);
                        Helper(arr, index + 1, result, slate);
                        swap(arr, i, index);

                        slate.Remove(slate.Length - 1, 1);
                    }
                }
            }

            static int result;
            public static int PermutateMenAndChairs(int mens, int chairs)
            {
                //REPEATION IS NOT ALLOWED *******************
                //n!/(n-k)!  -- > if K ~ 1 then its runtime is n!

                int[] mensArr = new int[mens];
                for (int x = 0; x < mensArr.Length; x++)
                    mensArr[x] = x + 1;

                PermutateMenAndChairs_helper(mensArr, chairs, 0, 0, new StringBuilder());
                return result;
            }

            private static void PermutateMenAndChairs_helper(int[] mens, int chairs, int menIndex, int chairIndex, StringBuilder slate)
            {
                if (chairIndex == chairs)
                {
                    result++;
                    Console.WriteLine(slate.ToString());
                    return;
                }

                if (menIndex == mens.Length)
                {
                    return;
                }

                for (int x = menIndex; x < mens.Length; x++)
                {
                    slate.Append(mens[x]);

                    swap(mens, x, menIndex);

                    PermutateMenAndChairs_helper(mens, chairs, (menIndex + 1), chairIndex + 1, slate);

                    swap(mens, x, menIndex);

                    slate.Remove(slate.Length - 1, 1);
                }
            }
            
            public static void PrintBinaryString(int n)
            {
                //O(2^n) - repetation is allowed
                PrintBinaryString_helper(n, new StringBuilder());
            }
            private static void PrintBinaryString_helper(int n, StringBuilder slate)
            {
                if(n == 0)
                {
                    Console.WriteLine(slate.ToString());
                    return;
                }

                slate.Append("0");
                PrintBinaryString_helper(n - 1, slate);
                slate.Remove(slate.Length -1,1);

                slate.Append("1");
                PrintBinaryString_helper(n - 1, slate);
                slate.Remove(slate.Length - 1, 1);
            }

            public static void PrintDecimalString(int n)
            {
                //O(10^n) - repetation is allowed
                PrintDecimalString_helper(n, new StringBuilder());
            }
            private static void PrintDecimalString_helper(int n, StringBuilder slate)
            {
                if (n == 0)
                {
                    Console.WriteLine(slate.ToString());
                    return;
                }

                for(int i=0; i < 10; i++)
                {
                    slate.Append(i);
                    PrintDecimalString_helper(n - 1, slate);
                    slate.Remove(slate.Length - 1, 1);
                }
            }
        }

        //784. Letter Case Permutation
        class LetterCasePermutation
        {
            public static IList<string> Generate(string S)
            {
                IList<string> result = new List<string>();
                Helper(S, 0, new StringBuilder(), result);

                return result;
            }

            static void Helper(string s, int idx, StringBuilder sb, IList<string> result)
            {
                if (idx == s.Length)
                {
                    result.Add(sb.ToString());
                    return;
                }

                if (char.IsLetter(s[idx]))
                {
                    if (char.IsLower(s[idx]))
                        sb.Append(char.ToUpper(s[idx]));
                    else
                        sb.Append(char.ToLower(s[idx]));
                    Helper(s, idx + 1, sb, result);
                    sb.Remove(sb.Length - 1, 1);

                    sb.Append(s[idx]);
                    Helper(s, idx + 1, sb, result);
                    sb.Remove(sb.Length - 1, 1);
                }
                else
                {
                    sb.Append(s[idx]);
                    Helper(s, idx + 1, sb, result);
                    sb.Remove(sb.Length - 1, 1);
                }
            }
        }


        public class Solution
        {
            public static IList<string> GenerateParenthesis(int n)
            {
                IList<string> result = new List<string>();

                DFS(n, 0, result, new StringBuilder());
                return result;
            }

            private static void DFS(int n, int currentIndex, IList<string> result, StringBuilder slate)
            {
                if (slate.Length == n * 2)
                {
                    result.Add(slate.ToString());
                    return;
                }

                for (int i = currentIndex; i < n; i++)
                {
                    slate.Append("(");
                    DFS(n, currentIndex + 1, result, slate);
                    slate.Remove(slate.Length - 1, 1);

                    slate.Append(")");
                    DFS(n, currentIndex + 1, result, slate);
                    slate.Remove(slate.Length - 1, 1);
                }
            }
        }


        public static int Search(int[] nums, int target)
        {
            int l = 0, r = nums.Length - 1;
            while (l <= r)
            {
                int m = l + (r - l) / 2;
                if (nums[m] == target)
                    return m;

                else if (nums[m] < nums[r]) //right sorted
                {
                    if (target > nums[m] && target <= nums[r])
                        l = m + 1;
                    else
                        r = m - 1;
                }
                else //left sorted
                {
                    if (target >= nums[l] && target < nums[m])
                        r = m - 1;
                    else
                        l = m + 1;
                }

            }

            return -1;
        }

        //79. Word Search
        class WordSearch
        {
            static int[][] direction = new int[4][] { new int[] { 0, -1 }, new int[] { 0, 1 }, new int[] { -1, 0 }, new int[] { 1, 0 } };
            public static bool Exist(char[][] board, string word)
            {
                bool[][] visited = new bool[board.Length][];
                for (int r = 0; r < board.Length; r++)
                    visited[r] = new bool[board[0].Length];

                for (int r = 0; r < board.Length; r++)
                {
                    for (int c = 0; c < board[0].Length; c++)
                    {
                        if (DFS(board, r, c, visited, word, 0))
                            return true;
                    }
                }
                return false;
            }

            private static bool DFS(char[][] board, int r, int c, bool[][] visited, string word, int index)
            {
                //base case
                if (index == word.Length)
                {
                    return false;
                }

                bool found = false;
                if (board[r][c] == word[index])
                {
                    //If we reached the last character, then we found all characters
                    if (index == word.Length - 1)
                        return true;

                    visited[r][c] = true;
                    foreach (List<int> neighbour in GetNeighbours(board, r, c, visited))
                    {
                        int nr = neighbour[0]; int nc = neighbour[1];
                        found = DFS(board, nr, nc, visited, word, index + 1);
                        if (found)
                            break;
                    }
                    visited[r][c] = false;
                }

                return found;
            }

            private static IList<IList<int>> GetNeighbours(char[][] board, int r, int c, bool[][] visited)
            {
                IList<IList<int>> result = new List<IList<int>>();
                for (int d = 0; d < direction.Length; d++)
                {
                    int r1 = direction[d][0] + r;
                    int c1 = direction[d][1] + c;
                    if (r1 >= 0 && r1 < board.Length && c1 >= 0 && c1 < board[0].Length && !visited[r1][c1])
                        result.Add(new List<int>() { direction[d][0] + r, direction[d][1] + c });
                }
                return result;
            }
        }

        //279. Perfect Squares
        public class PerfectSquares
        {
            /*public static int NumSquares(int n)
            {
                int[] result = new int[1] { int.MaxValue };
                DFS(n, n, 0, result);
                return result[0];
            }

            private static void DFS(int n, int sum, int count, int[] result)
            {
                if (sum == 0)
                {
                    result[0] = Math.Min(result[0], count);
                    return;
                }
                else if (sum == 1)
                {
                    result[0] = Math.Min(result[0], count + 1);
                    return;
                }
                else if (sum < 0)
                    return;

                //we are looking for minimum number of erfect sqaures.. 
                //so lets be greedy and start it from sum /2
                for (int i = sum / 2; i * i >= 1; i--)
                {
                    if (result[0] > count + 1)
                        DFS(n, sum - (i * i), count + 1, result);
                }
            }*/

            public static int NumSquares(int n)
            {
                int[] result = new int[1] { int.MaxValue };
                int[] memo = new int[n + 1];
                for (int i = 0; i < memo.Length; i++)
                    memo[i] = -1;

                DFS(n, n, 0, result, memo);
                return result[0];
            }

            private static int DFS(int n, int sum, int count, int[] result, int[] memo)
            {
                if (sum == 0)
                {
                    result[0] = Math.Min(result[0], count);
                    return count;
                }
                else if (sum == 1)
                {
                    result[0] = Math.Min(result[0], count + 1);
                    return count + 1;
                }
                else if (sum < 0)
                    return 0;

                if (memo[sum] != -1)
                {
                    result[0] = Math.Min(result[0], count + memo[sum]);
                    return count + memo[sum];
                }
                int localMin = int.MaxValue;

                //we are looking for minimum number of erfect sqaures.. 
                //so lets be greedy and start it from sum /2
                for (int i = sum / 2; i * i >= 1; i--)
                {
                    if(result[0] > count + 1)
                    {
                        localMin = Math.Min(localMin, DFS(n, sum - (i * i), count + 1, result, memo));
                    }
                }

                memo[sum] = localMin;
                return localMin;
            }
        }

        public static void runTest()
        {
            PerfectSquares.NumSquares(427);

            char[][] board = new char[3][] { new char[4] { 'A', 'B', 'C', 'E' }, new char[] { 'S', 'F', 'C', 'S' }, new char[] { 'A', 'D', 'E', 'E' }  };
            WordSearch.Exist(board,"ABCCED");

            SubsetsII.SubsetsWithDup(new int[] { 1, 2,2 });

            Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0);

            //Solution.GenerateParenthesis(2);

            PermutationII.PrintAllPermutation(new int[] { 1, 2, 3 });
            Permutation.Permute(new int[] { 1, 2, 3 });

            StringsFromWildCard.Get("111");

            AllSubsetOrPowerset.generate_all_subsets("123");

            PermutationII.PrintDecimalString(2);
            PermutationII.PrintBinaryString(3);

            PermutationII.PermutateMenAndChairs(4, 3);

            PalindromicSubsetOfString.PrintAllSubStrings("abc");

            NQueen.NQueenProblem(4);

            SebsetSum.check_if_sum_possible(new long[] { 1, 2, 3 }, 10);

            expressionEval.expression("222", 24);

            CountUniqueBST.No_Of_binary_Tree(4);

            
        }
    }
}
