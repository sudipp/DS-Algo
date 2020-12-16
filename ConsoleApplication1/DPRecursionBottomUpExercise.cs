using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Pair
    {
        public int x;
        public int y;

        public Pair(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }


    /*
     * Dynamic Programming: USE and (REUSE Subproblems - overlapping subproblem)
     * Two ways of Implementing DP Solutions - (Tabulation - Bottom up) and (recursion with Memoization - top down approach, shows REUSE subproblem)
     */

    class DPRecursionBottomUpExercise
    {
        // Create an array for memoization
        private static Dictionary<int, int> _memo = new Dictionary<int, int>();
        static int[] fMemo = new int[1000];

        public static int Fibonacci_TopDown_Recursion(int n) //O(2^n) - for recursive Stack
        {

            if (n == 0 || n == 1)
            {
                return n;
            }

            int result = Fibonacci_TopDown_Recursion(n - 1) + Fibonacci_TopDown_Recursion(n - 2);
            return result;
        }

        /*DP - Recursion with Memoization - Top Down
         *    * Subproblems is a[i] = a[i-1] + a[i-2]
        */

        public static int Fibonacci_TopDown_RecursionMemoization(int n) //O(n) and Space O(n) - for recursive Stack
        {
            // Edge case
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(n.GetType().Name,
                    "Index was negative. No such thing as a negative index in a series.");
            }

            // Base cases
            if (n == 0 || n == 1)
            {
                return n;
            }

            // See if we've already calculated this
            if (_memo.ContainsKey(n))
            {
                Console.WriteLine("Grabbing _memo [{0}]", n);
                return _memo[n];
            }

            Console.WriteLine(string.Format("Computing Fib({0})", n));
            int result = Fibonacci_TopDown_RecursionMemoization(n - 1) + Fibonacci_TopDown_RecursionMemoization(n - 2);

            // Memoize
            _memo[n] = result;

            return result;
        }

        /*Bottom Up - DP ****
         * Subproblem : a[i] = a[i-1] + a[i-2]
        */

        public static int Fibonacci_DPBottomUp(int n) //O(n) time and O(1) space.
        {
            // Edge cases:
            if (n < 0)
            {
                throw new ArgumentException("Index was negative. No such thing as a negative index in a series.");
            }

            if (n == 0 || n == 1)
            {
                return n;
            }

            // We'll be building the fibonacci series from the bottom up.
            // So we'll need to track the previous 2 numbers at each step.
            int prevPrev = 0; // 0th fibonacci
            int prev = 1; // 1st fibonacci
            int current = 0; // Declare and initialize current

            for (int i = 1; i < n; i++)
            {
                // Iteration 1: current = 2nd fibonacci
                // Iteration 2: current = 3rd fibonacci
                // Iteration 3: current = 4th fibonacci
                // To get nth fibonacci ... do n-1 iterations.
                current = prev + prevPrev;
                prevPrev = prev;
                prev = current;
            }

            return current;
        }

        /*Bottom Up - DP ****
         * Subproblem : a[i] = a[i-1] + a[i-2]
        */

        public static int Fibonacci_DPBottomUp_Tabulization(int n) //O(n) time and O(n+1) space.
        {
            //tabulation
            int[] a = new int[n + 1];
            a[0] = 0;
            a[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                a[i] = a[i - 1] + a[i - 2];
            }

            return a[n];
        }

        //DP using Recursion (Top Down)
        public static int Fibonacci_LogN_DP_RecursionMemoization(int n) //Time O(Log n) Time
        {
            // Base cases
            if (n == 0)
                return 0;
            if (n == 1 || n == 2)
            {
                if (!_memo.ContainsKey(n))
                    _memo.Add(n, 1);
                return (_memo[n]);
            }

            // If fib(n) is already computed, return it
            if (_memo.ContainsKey(n) && _memo[n] != 0)
                return _memo[n];

            //If n is even, we can put k = n/2
            //If n is odd, we can put k = (n+1)/2
            int k = (n%2) == 1 ? (n + 1)/2 : n/2;

            _memo.Add(n, (n%2) == 1 //for odd
                ? (Fibonacci_LogN_DP_RecursionMemoization(k)*Fibonacci_LogN_DP_RecursionMemoization(k) +
                   Fibonacci_LogN_DP_RecursionMemoization(k - 1)*Fibonacci_LogN_DP_RecursionMemoization(k - 1))
                : (2*Fibonacci_LogN_DP_RecursionMemoization(k - 1) + Fibonacci_LogN_DP_RecursionMemoization(k))*
                  Fibonacci_LogN_DP_RecursionMemoization(k)
                );

            return _memo[n];
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

        /*
         * Let's make our subproblem be getting all permutations for all characters except the last one.
         * If we had all permutations for "cat," how could we use that to generate all permutations for "cats"? 
         * We could put the "s" in each possible position for each possible permutation of "cat"!
         */
        public static ISet<string> GetPermutations(string inputString)
        {
            // Base case
            if (inputString.Length <= 1)
            {
                return new HashSet<string>(inputString.Select(c => new string(c, 1)));
            }

            var allCharsExceptLast = inputString.Substring(0, inputString.Length - 1);
            char lastChar = inputString[inputString.Length - 1];

            // Recursive call: get all possible permutations for all chars except last
            var permutationsOfAllCharsExceptLast = GetPermutations(allCharsExceptLast);

            // Put the last char in all possible positions for each of the above permutations
            var permutations = new HashSet<string>();
            foreach (var permutationOfAllCharsExceptLast in permutationsOfAllCharsExceptLast)
            {
                for (int position = 0; position <= allCharsExceptLast.Length; position++)
                {
                    var permutation = permutationOfAllCharsExceptLast.Substring(0, position)
                                      + lastChar
                                      + permutationOfAllCharsExceptLast.Substring(position);
                    permutations.Add(permutation);
                }
            }

            return permutations;
        }


        private static Dictionary<string, int> _memo1 = new Dictionary<string, int>();

        //https://www.youtube.com/watch?v=k4y5Pr0YVhg ***********
        // It takes O(n*m) time and O(n*m) space - where n is the size of amount and m is the number of items in denominations
        public static int CoinChange_Recursion_Memoization_TopDown(int money, int[] coins, int currentCoinIndex = 0)
        {
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

            return ways;
        }

        // With Auxiliary buffer
        // It takes O(n*m) time and O(n) + O(M) space - where n is the size of amount and m is the number of items in denominations
        // recursive it will build up a large call stack of size O(m)
        public static void CoinChange_Recursion_TopDown_AuxiliaryBuffer(int[] coins, int money, Stack<int> buffer, IList<IList<int>> combinations,
            int index = 0, int amountWithCoin = 0)
        {
            // termination cases    
            if (amountWithCoin > money)
            {
                return;
            }

            //If sum has reached target, exit program
            if (amountWithCoin == money)
            {
                combinations.Add(new List<int>());

                //priting the buffer
                foreach (var comb in buffer)
                {
                    Console.Write(comb);
                    combinations.Last().Add(comb);
                }
                Console.WriteLine();

                return;
            }

            // find candidates that go into buffer    
            for (int i = index; i < coins.Length; i++)
            {
                // place candidate into buffer and recurse        
                buffer.Push(coins[i]);
                CoinChange_Recursion_TopDown_AuxiliaryBuffer(coins, money, buffer, combinations, i, amountWithCoin + coins[i]);
                buffer.Pop();
            }
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
         Bottom Up Approach using Tabulation
         * Let’s say you have to climb N steps. You can jump 1 step, 3 steps or 5 steps at a time. 
         * How many ways are there to get to the top of the steps (i.e at n th step)?
         * ==============
         * Subproblems are 
         *      a[i+1] = a[i+1] + a[i], 
         *      a[i+3] = a[i+3] + a[i], 
         *      a[i+5] = a[i+5] + a[i]
        */

        public static int waysToClimb_DP_BottomUp_Tabulization(int n)
        {
            //tabulation
            int[] a = new int[n + 1];
            a[0] = 1;

            for (int i = 0; i < a.Length; i++) //Start calculation from 0 th position
            {
                if (i + 1 < a.Length)
                    a[i + 1] = a[i + 1] + a[i];

                if (i + 3 < a.Length)
                    a[i + 3] = a[i + 3] + a[i];

                if (i + 5 < a.Length)
                    a[i + 5] = a[i + 5] + a[i];
            }
            return a[n]; //n th step will hold the result
        }

        /*
         Top down Approach using Tabulation
         * Let’s say you have to climb N steps. You can jump 1 step, 3 steps or 5 steps at a time. 
         * How many ways are there to get to the top of the steps (i.e at n th step)?
         * ==============
         * Subproblems are 
         *      a[i] = a[i-1] + a[i-3] + a[i-5]
        */

        public static int waysToClimb_DP_TopDown_Tabulization(int n)
        {
            //tabulation
            int[] a = new int[n + 1];
            a[0] = 1;

            for (int i = 1; i < a.Length; i++)
            {
                int nMinus1 = i - 1 < 0 ? 0 : a[i - 1];
                int nMinus3 = i - 3 < 0 ? 0 : a[i - 3];
                int nMinus5 = i - 5 < 0 ? 0 : a[i - 5];

                a[i] = nMinus1 + nMinus3 + nMinus5;
            }
            return a[n];
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

        public static int waysToClimb_DP_TopDown_Recursion(int n, int[] memo)
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

            memo[n] = waysToClimb_DP_TopDown_Recursion(n - 1, memo) + waysToClimb_DP_TopDown_Recursion(n - 3, memo) +
                      waysToClimb_DP_TopDown_Recursion(n - 5, memo);

            return memo[n];
        }

        void TowerOfHonoi_Recursion_DP(String[] args) //Time O(n) - space O(n)
        {
            int n = 3;
            Tower[] towers = new Tower[n];

            //set 3 towers
            for (int i = 0; i < 3; i++)
            {
                towers[i] = new Tower(i);
            }
            //Put disks in first towers
            for (int i = n - 1; i >= 0; i--)
            {
                towers[0].add(i);
            }

            //move disk to 3 tower
            towers[0].moveDisks(n, towers[2], towers[1]);
        }

        class Tower
        {
            private Stack<int> disks;
            private int index;

            public Tower(int i)
            {
                disks = new Stack<int>();
                index = i;
            }

            public int Index()
            {
                return index;
            }

            public void add(int d)
            {
                if (disks.Count != 0 && disks.Peek() <= d)
                {
                    Console.WriteLine("Error placing disk" + d);
                }
                else
                {
                    disks.Push(d);
                }
            }

            public void moveTopTo(Tower t)
            {
                int top = disks.Pop();
                t.add(top);
            }

            public void moveDisks(int n, Tower destination, Tower buffer)
            {
                if (n > 0)
                {
                    //base case

                    //move n-1 disks to tower2
                    moveDisks(n - 1, buffer, destination); //recursion
                    //move nth disk to tower 3
                    moveTopTo(destination);
                    //move n-1 disk to tower 3 from tower 2
                    buffer.moveDisks(n - 1, destination, this);
                }
            }
        }

        static int combo(int amt, int[] coins)
        {
            if (amt == 0)
                return 1;
            if (amt < 0)
                return 0;

            int ways = 0;
            for (int x = 0; x < coins.Length; x++)
            {
                ways += combo(amt - coins[x], coins);
            }

            return ways;
        }

        /*
         * Using binary search pattern ****,
         * if a[mid]> mid then magic index on left side, or right side
         * ====================================
         * A magic index in an array A[ 1 .. n-1] is defined to be an index such that A[i] = i. 
         * Given a sorted array of distinct integers, write a method to find a magic index, if one exists, in array A.
         */

        int MagicIndex_from_SortedArray_DP_Recursion(int[] array, int start, int end)
        {

            //base case
            if (end < start)
            {
                return -1;
            }

            int mid = (start + end)/2;
            if (array[mid] == mid)
            {
                return mid;
            }
            else if (array[mid] > mid)
            {
                return MagicIndex_from_SortedArray_DP_Recursion(array, start, mid - 1);
            }
            else
            {
                return MagicIndex_from_SortedArray_DP_Recursion(array, mid + 1, end);
            }
        }


        /*
         * Print all Combinations (or Permutations) of an array.
         * https://www.geeksforgeeks.org/write-a-c-program-to-print-all-permutations-of-a-given-string/
         * Time: O(n * n!)
         */
        private static void PrintAllPermutationsOfStringOrLexicographic(string str, int start, int end)
        {
            //Lexicographic 
            if (start == end)
                Console.WriteLine(str);
            else
            {
                for (int i = start; i <= end; i++)
                {
                    Console.WriteLine("Swapping {0} with {1}", start, i);
                    str = swap(str, start, i);
                    PrintAllPermutationsOfStringOrLexicographic(str, start + 1, end);
                    str = swap(str, start, i); //backtrack
                }
            }
        }

        private static string swap(string str, int start, int end)
        {
            char[] arr = str.ToCharArray();
            char temp = arr[start];
            arr[start] = arr[end];
            arr[end] = temp;
            return new string(arr);
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

        /*
         * O(4^n) time - recursively branch 4 times for each call to PrintWordFromPhoneDigit
         * Space Complexity: O(n). We use O(n) space both in the buffer allocation and on recursion stack
         */
        static void PrintWordFromPhoneDigit_1(int[] phoneNumberArray, int index, char[] buffer, HashSet<String> wordSet)
        {
            string[] phNumberLetters = { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };


            /* If it's a complete word, print it. */
            if (index == phoneNumberArray.Length) //} && wordSet.Contains(prefix)) {
            {
                Console.WriteLine(string.Concat(buffer.Where(s => (s > 0))));
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
                    buffer[index] = letter;
                    PrintWordFromPhoneDigit_1(phoneNumberArray, index + 1, buffer, wordSet);
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
         
        // A recursive function to replace previous color 'prevC' at  '(x, y)' 
        // and all surrounding pixels of (x, y) with new color 'newC' and
        static void floodFillUtil(int[][] screen, int x, int y, int prevColor, int newColor)
        {
            // Base cases
            if (x < 0 || x >= 8 || y < 0 || y >= 8)
                return;
            if (screen[x][y] != prevColor)
                return;
 
            // Replace the color at (x, y)
            screen[x][y] = newColor;
 
            // Recur for north, east, south and west
            floodFillUtil(screen, x + 1, y, prevColor, newColor);
            floodFillUtil(screen, x - 1, y, prevColor, newColor);
            floodFillUtil(screen, x, y + 1, prevColor, newColor);
            floodFillUtil(screen, x, y - 1, prevColor, newColor);
        }
        
        // A recursive function to replace previous color 'prevC' at  '(x, y)' 
        // and all surrounding pixels of (x, y) with new color 'newC' and
        static bool MazeAllDirection(int[][] screen, int x, int y, int prevC, int newC, Hashtable hs)
        {
            // Base cases
            if (x < 0 || x >= 8 || y < 0 || y >= 8 || screen[x][y] == 0)
            {
                Console.WriteLine("Exiting =>" + "(" + x + "," + y + ")");
                return false;
            }
            if (hs.ContainsKey(x + "-" + y))
            {
                //Console.WriteLine("Found in Memo =>" + "(" + x + "," + y + ")");
                return false;
            }

            //if (screen[x][y] != prevC)
            //    return false;

            if (x == 7 && y == 7)
            {
                Console.WriteLine("Reached =>" + "(" + x + "," + x + ")");
                return true;
            }

            // Replace the color at (x, y)
            //screen[x][y] = newC;
            hs.Add(x + "-" + y, false);

            
            // Recur for north, east, south and west
            if (MazeAllDirection(screen, x + 1, y, prevC, newC, hs))
                return true;
            if(MazeAllDirection(screen, x - 1, y, prevC, newC, hs))
                return true;
            if(MazeAllDirection(screen, x, y + 1, prevC, newC, hs))
                return true;
            if(MazeAllDirection(screen, x, y - 1, prevC, newC, hs))
                return true;


            Console.WriteLine( "x:{0},y:{1}",x,y);
            return false;
        }

        

        static int No_of_island_BFS(char[,] grid)
        {
            if (grid == null || grid.GetLength(0) == 0)
            {
                return 0;
            }

            int nr = grid.GetLength(0);
            int nc = grid.GetLength(1);
            
            // Below arrays details all 8 possible movements from a cell
            // (top, right, bottom, left and 4 diagonal moves)
            int[] row = { -1, 0, 0, 1 };
            int[] col = { 0, -1, 1, 0 };

            // stores if cell is processed or not
            bool[,] processed = new bool[nr, nc];

            int island = 0;
            for (int i = 0; i < nr; i++)
            {
                for (int j = 0; j < nc; j++)
                {
                    // start BFS from each unprocessed node and
                    // increment island count
                    if (grid[i, j] == '1' && !processed[i, j])
                    {
                        island++;

                        //create an empty queue and enqueue source node
                        Queue<Pair> q = new Queue<Pair>();
                        q.Enqueue(new Pair(i, j));

                        // mark source node as processed
                        processed[i, j] = true;

                        // run till queue is not empty
                        while (q.Count > 0)
                        {
                            Pair p = q.Dequeue(); 
                        
                            // check for all 8 possible movements from current cell
                            // and enqueue each valid movement
                            for (int k = 0; k < row.Length; k++)
                            {
                                int x = p.x + row[k];
                                int y = p.y + col[k]; 
                            
                                // Skip if location is invalid or already processed or has water
                                if (isSafe(grid, x, y, processed))// && !processed[x, y] && grid[x, y] == '1')
                                {
                                    // skip if location is invalid or it is already
                                    // processed or consists of water
                                    processed[x, y] = true;
                                
                                    q.Enqueue(new Pair(x, y));
                                }
                            }
                        }
                    }
                }
            }

            return island;
        }

        // Function to check if it is safe to go to position (x, y)
        // from current position. The function returns false if (x, y)
        // is not valid matrix cordinates or (x, y) represents water or
        // position (x, y) is already processed
         static bool isSafe(char[,] mat, int x, int y, bool[,] processed)
        {


            int processedR = processed.GetLength(0);
            int processedC = processed.GetLength(1);

            return (x >= 0) && (x < processedR) &&
                    (y >= 0) && (y < processedC) &&
                    (mat[x, y] == '1' && !processed[x, y]);
        }

        public class NQueenProblem
        {
            static int N = 4;

            /* This function solves the N Queen problem using Backtracking.  
             * It returns false if queens cannot be placed, otherwise return true and prints placement of queens in the form of 1s.
             * Please note that there may be more than one solutions, this function prints one of the feasible solutions.*/
            public static bool solveNQueen()
            {
                //no queen should share the same row, column or diagonal.
                int[,] board = new int[4, 4]
                {
                    {0, 0, 0, 0},
                    {0, 0, 0, 0},
                    {0, 0, 0, 0},
                    {0, 0, 0, 0}
                };

                if (solveNQUtil(board, 0) == false)
                {
                    Console.Write("Solution does not exist");
                    return false;
                }

                printSolution(board);
                return true;
            }

            /* A utility function to print solution */
            static void printSolution(int[,] board)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                        Console.Write(" " + board[i, j] + " ");
                    Console.WriteLine();
                }
            }

            /* A utility function to check if a queen can be placed on board[row][col]. 
             * Note that this function is called when "col" queens are already placed in columns from 0 to col -1. 
             * So we need to check only left side for attacking queens */
            static bool isSafe(int[,] board, int row, int col)
            {
                int r, c;

                /* Check this row on left side */
                for (r = 0; r < col; r++)
                    if (board[row, r] == 1)
                        return false;

                /* Check upper diagonal on left side */
                for (r = row, c = col; r >= 0 && c >= 0; r--, c--)
                    if (board[r, c] == 1)
                        return false;

                /* Check lower diagonal on left side */
                for (r = row, c = col; c >= 0 && r < N; r++, c--)
                    if (board[r, c] == 1)
                        return false;

                return true;
            }

            /* A recursive utility function to solve N Queen problem */
            static bool solveNQUtil(int[,] board, int col)
            {
                /* base case: If all queens are placed then return true */
                if (col >= N)
                    return true;

                /* Consider this column and try placing this queen in all rows one by one */
                for (int row = 0; row < N; row++)
                {
                    /* Check if queen can be placed on board[row][col] */
                    if (isSafe(board, row, col))
                    {
                        /* Place this queen in board[row][col] */
                        board[row, col] = 1;

                        /* recur to place rest of the queens */
                        if (solveNQUtil(board, col + 1) == true)
                            return true;

                        /* If placing queen in board[row][col] doesn't lead to a solution then remove queen from board[row][col] */
                        board[row, col] = 0; // BACKTRACK
                    }
                }

                /* If queen can not be place in any row in
                   this colum col, then return false */
                return false;
            }
        }

        
        
        public class KnightTour
        {
            /*
             * Check current cell is not already used if not then mark that cell (start with 0 and keep incrementing it, it will show us the path for the knight).
             * Check if index = N*N-1, means Knight has covered all the cells. return true and print the solution matrix.
             * Now try to solve rest of the problem recursively by making index +1. Check all 8 directions. (Knight can move to 8 cells from its current position.) 
             * Check the boundary conditions as well
             * If none of the 8 recursive calls return true, BACKTRACK and undo the changes ( put 0 to corresponding cell in solution matrix) and return false.
             */

            int[,] solution;
            int path = 0;

            public KnightTour(int N) {
		        solution = new int[N,N];
	        }

            public void solve() {
		        if (findPath(0, 0, 0, solution.Length)) {
			        print();
		        } else {
			        Console.WriteLine("NO PATH FOUND");
		        }
	        }

            public bool findPath(int row, int column, int index, int N)
            {
                // check if current is not used already
                if (solution[row,column] != 0)
                {
                    return false;
                }
                // mark the current cell is as used
                solution[row, column] = path++;
                
                // if (index == 50) {
                if (index == N * N - 1)
                {
                    // if we are here means we have solved the problem
                    return true;
                }
                // try to solve the rest of the problem recursively

                // go down and right
                if (canMove(row + 2, column + 1, N)
                        && findPath(row + 2, column + 1, index + 1, N))
                {
                    return true;
                }
                // go right and down
                if (canMove(row + 1, column + 2, N)
                        && findPath(row + 1, column + 2, index + 1, N))
                {
                    return true;
                }
                // go right and up
                if (canMove(row - 1, column + 2, N)
                        && findPath(row - 1, column + 2, index + 1, N))
                {
                    return true;
                }
                // go up and right
                if (canMove(row - 2, column + 1, N)
                        && findPath(row - 2, column + 1, index + 1, N))
                {
                    return true;
                }
                // go up and left
                if (canMove(row - 2, column - 1, N)
                        && findPath(row - 2, column - 1, index + 1, N))
                {
                    return true;
                }
                // go left and up
                if (canMove(row - 1, column - 2, N)
                        && findPath(row - 1, column - 2, index + 1, N))
                {
                    return true;
                }
                // go left and down
                if (canMove(row + 1, column - 2, N)
                        && findPath(row + 1, column - 2, index + 1, N))
                {
                    return true;
                }
                // go down and left
                if (canMove(row + 2, column - 1, N)
                        && findPath(row + 2, column - 1, index + 1, N))
                {
                    return true;
                }
                // if we are here means nothing has worked , backtrack
                solution[row, column] = 0;
                path--;
                return false;
            }

            public bool canMove(int row, int col, int N)
            {
                if (row >= 0 && col >= 0 && row < N && col < N)
                {
                    return true;
                }
                return false;
            }

            public void print() {
		        for (int i = 0; i < solution.Length; i++) {
			        for (int j = 0; j < solution.Length; j++) {
				        Console.Write("   " + string.Format("D2", solution[i,j]));
			        }
                    Console.WriteLine();
		        }
	        }

            public static void main(String[] args)
            {
                int N = 8;
                KnightTour i = new KnightTour(N);
                i.solve();
            }

        }


        public static int NumIslands(char[,] grid)
        {
            if (grid == null || grid.GetLength(0) == 0)
            {
                return 0;
            }

            int nr = grid.GetLength(0);
            int nc = grid.GetLength(1);
            int num_islands = 0;
            for (int r = 0; r < nr; ++r)
            {
                for (int c = 0; c < nc; ++c)
                {
                    if (grid[r,c] == '1')
                    {
                        ++num_islands;
                        dfs(grid, r, c);
                    }
                }
            }

            return num_islands;
        }

        static void dfs(char[,] grid, int r, int c)
        {
            int nr = grid.GetLength(0);
            int nc = grid.GetLength(1);

            if (r < 0 || c < 0 || r >= nr || c >= nc || grid[r,c] == '0')
            {
                return;
            }

            grid[r,c] = '0';
            dfs(grid, r - 1, c);
            dfs(grid, r + 1, c);
            dfs(grid, r, c - 1);
            dfs(grid, r, c + 1);
        }
        
        public static void runTest()
        {
            combinationSum(new int[] { 2, 3, 6, 7 }, 7);


            Console.WriteLine(CoinChange_BottomUp(5, new int[] { 1, 2, 3, 4 }));


            char[,] island = new char[4, 5]
                     {
                            {'1', '1', '0', '0', '0'},
                            {'1', '1', '0', '0', '0'},
                            {'0', '0', '1', '0', '0'},
                            {'0', '0', '0', '1', '1'},
                     };
            NumIslands(island);

            int[][] screen = new int[8][] {
                            new int[] {1, 1, 1, 1, 1, 1, 1, 1},
                            new int[] {1, 1, 1, 1, 1, 1, 0, 0},
                            new int[] {1, 0, 0, 1, 1, 0, 1, 1},
                            new int[] {1, 2, 2, 2, 2, 0, 1, 0},
                            new int[] {1, 1, 1, 2, 2, 0, 1, 0},
                            new int[] {1, 1, 1, 2, 2, 2, 2, 0},
                            new int[] {1, 1, 1, 1, 1, 2, 1, 1},
                            new int[] {1, 1, 1, 1, 1, 2, 2, 1},
                     };

            int[][] screen1 = new int[8][] {
                            new int[] {1, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 1, 0, 0, 0, 0, 0, 0},
                            new int[] {0, 1, 1, 0, 0, 0, 0, 0},
                            new int[] {0, 0, 1, 1, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 1, 0, 0, 0},
                            new int[] {0, 0, 0, 0, 1, 1, 0, 0},
                            new int[] {0, 0, 0, 0, 0, 1, 1, 1},
                            new int[] {0, 0, 0, 0, 0, 0, 0, 1},
                     };

            bool b = MazeAllDirection(screen1, 0, 0, 1, 2, new Hashtable());
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                    Console.Write(screen1[i][j] + " ");
                Console.WriteLine();
            }





            int x = 4, y = 4, newC = 3;
            floodFillUtil(screen, 4, 4, 2, 3);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                    Console.Write(screen[i][j] + " ");
                Console.WriteLine();
            }

            //PrintWordFromPhoneDigit_1(new int[] { 2, 3, 4 }, 0, new char[3], null);         
            //var results= new List<string>();
            //allPermutationsOfSizeX_1(new int[] {1, 2, 3, 4, 5}, new int[3] , 0, new bool[5]);
            Console.WriteLine("-----------");
            printAllSubsetsX_1(new int[] { 1, 2, 3 }, new int[3], 0, 0);

            allCombinationOfSizeX_1(new int[] { 1, 2, 3 }, new int[3], 0, 0);


            int[] buffer = new int[3];
            bool[] isInBuffer = new bool[3];
            //printPermsHelper(new int[] { 1, 2, 3 }, buffer, 0, isInBuffer);

            PrintWordFromPhoneDigit_1(new int[] { 2, 3 }, 0, new char[2], null);


            PrintAllPermutationsOfStringOrLexicographic("123456", 0, 2);
            //Console.WriteLine(CoinChange_Recursion_Memoization_TopDown(5, new int[] { 1, 2, 3, 4}));//, new Stack<int>()));
            //Console.WriteLine("==================");

            Console.WriteLine("Fibonacci " + Fibonacci_TopDown_RecursionMemoization(9));

            //GetPermutations("abcde");
        }
    }
}
