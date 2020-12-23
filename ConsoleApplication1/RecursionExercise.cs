﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

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

        //divide and conquer
        int power_LogN(int x, int y) //O(LogN), space O(1) - 
        {
            int temp;
            if (y == 0)
                return 1;

            temp = power_LogN(x, y/2);

            if (y%2 == 0) //even
                return temp*temp;
            else
                return x*temp*temp;
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
            //Combimation 
            public static bool check_if_sum_possible(long[] arr, long k)
            {
                var result = new List<string>();
                return helper(arr, 0, k, 0, new StringBuilder(), result);
            }

            static bool helper(long[] arr, int index, long k, long slate, StringBuilder soFar, List<string> result)
            {
                //base case
                if (slate > k)
                    return false;
                if (slate == k)
                    return true;

                if (index == arr.Length)
                {
                    result.Add(soFar.ToString());
                }
                else
                {

                    /*if (index > 0 && slate == k)
                        return true;
                        */

                    bool found = helper(arr, index + 1, k, slate, soFar, result);
                    if (found)
                        return true;

                    soFar.Append(arr[index]);

                    //swap(arr, index, x);
                    found = helper(arr, index + 1, k, slate + arr[index], soFar, result);
                    //swap(arr, index, x);

                    soFar.Remove(soFar.Length - 1, 1);

                    if (found)
                        return true;
                }

                return false;
            }

        }

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

        class Permutation
        {
            //O(N!) -- If repetation is not allwowed
            public static string[] PrintAllPermutation(int[] arr)
            {
                //Repeation is NOT allowed ******
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
        
        public static void runTest()
        {
            Permutation.PrintAllPermutation(new int[] { 1, 2, 3 });


            AllSubsetOrPowerset.generate_all_subsets("123");

            Permutation.PrintDecimalString(2);
            Permutation.PrintBinaryString(3);

            Permutation.PermutateMenAndChairs(4, 3);

            PalindromicSubsetOfString.PrintAllSubStrings("abc");

            NQueen.NQueenProblem(4);

            SebsetSum.check_if_sum_possible(new long[] { 1, 2, 3 }, 10);

            expressionEval.expression("222", 24);

            CountUniqueBST.No_Of_binary_Tree(4);

            
        }
    }
}