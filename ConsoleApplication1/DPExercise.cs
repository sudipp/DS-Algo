using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class DPExercise
    {
        //62. Unique Paths
        class UniquePath
        {
            //https://leetcode.com/problems/unique-paths/
            public static int Count(int m, int n)
            {
                //only can traverse down and right direction

                int[][] grid = new int[m][];
                for (int r = 0; r < grid.Length; r++)
                    grid[r] = new int[n];
                
                for (int r = 0; r < grid.Length; r++)
                    grid[r][0] = 1;
                for (int c = 0; c < grid[0].Length; c++)
                    grid[0][c] = 1;

                for (int r = 1; r < grid.Length; r++)
                    for (int c = 1; c < grid[0].Length; c++)
                        grid[r][c] = grid[r][c - 1] + grid[r - 1][c];
                
                return grid[grid.Length - 1][grid[0].Length - 1];
            }
            
            //https://leetcode.com/problems/minimum-path-sum/
            public static int MinPathSum(int[][] grid)
            {
                int[][] dp = new int[grid.Length][];
                for (int r = 0; r < dp.Length; r++)
                    dp[r] = new int[grid[0].Length];

                dp[0][0] = grid[0][0];

                for (int r = 1; r < dp.Length; r++)
                    dp[r][0] = dp[r - 1][0] + grid[r][0];
                for (int c = 1; c < dp[0].Length; c++)
                    dp[0][c] = dp[0][c - 1] + grid[0][c];

                for (int r = 1; r < grid.Length; r++)
                    for (int c = 1; c < grid[0].Length; c++)
                        dp[r][c] = Math.Min(dp[r][c - 1], dp[r - 1][c]) + grid[r][c];

                return dp[dp.Length - 1][dp[0].Length - 1];
            }
        }

        //63. Unique Paths II
        class UniquePathII
        {
            public static int CountWithObstacle(int[][] grid)
            {
                //Memory Optimization **************************
                //https://uplevel.interviewkickstart.com/resource/helpful-class-video-4891-6-503-0
                //1:36:44

                if (grid[0][0] == 1) //Start is blocked
                    return 0;

                //only 2 rows are
                int[,] memo = new int[Math.Min(2, grid.Length), grid[0].Length];

                //how many ways, we can reach first cell (top-left)
                memo[0, 0] = 1;

                for (int c = 0; c < grid[0].Length; c++)
                {
                    if (grid[0][c] == 1)
                        break;

                    //if current column is NOT blocked (dp[0][c] == 0), then we CAN reach this cell
                    memo[0, c] = 1;
                }

                //f(r,c) = f(r - 1,c) + f(r, c-1)
                for (int r = 1; r < grid.Length; r++)
                {
                    if (grid[r][0] == 1) //if there is an obstacle
                        memo[r % 2, 0] = 0;
                    else                 //no obstacle was there
                        memo[r % 2, 0] = memo[(r - 1) % 2, 0];

                    for (int c = 1; c < grid[0].Length; c++)
                    {
                        if (grid[r][c] == 1) //escape the blocked cell
                            memo[r % 2, c] = 0;
                        else
                            memo[r % 2, c] = memo[(r - 1) % 2, c] + memo[r % 2, c - 1];
                    }
                }

                return memo[(grid.Length - 1) % 2, grid[0].Length - 1];


                /* Space - O(M*N)
                int[,] memo = new int[grid.Length, grid[0].Length];

                //how many ways, we can reach first cell (top-left)
                memo[0, 0] = 1;

                for (int r = 0; r < grid.Length; r++)
                {
                    if (grid[r][0] == 1)
                        break;

                    //if current row is NOT blocked (dp[r][0] == 0), then we CAN reach this cell
                    memo[r, 0] = 1;
                }

                for (int c = 0; c < grid[0].Length; c++)
                {
                    if (grid[0][c] == 1)
                        break;

                    //if current column is NOT blocked (dp[0][c] == 0), then we CAN reach this cell
                    memo[0, c] = 1;
                }

                //f(r,c) = f(r - 1,c) + f(r, c-1)
                for (int r = 1; r < grid.Length; r++)
                {
                    for (int c = 1; c < grid[0].Length; c++)
                    {
                        if (grid[r][c] == 1) //escape the blocked cell
                            continue;
                        memo[r, c] = memo[r - 1, c] + memo[r, c - 1];
                    }
                }
                return memo[grid.Length - 1, grid[0].Length - 1];
                */
            }

            public static int CountWithoutObstacle(int[][] grid)
            {
                int[,] memo = new int[grid.Length, grid[0].Length];

                //how many ways, we can reach first cell (top-left)
                memo[0, 0] = 1;

                for (int r = 0; r < grid.Length; r++)
                {
                    //if current row is NOT blocked (dp[r][0] == 0), then we CAN reach this cell
                    memo[r, 0] = 1;
                }

                for (int c = 0; c < grid[0].Length; c++)
                {
                    //if current column is NOT blocked (dp[0][c] == 0), then we CAN reach this cell
                    memo[0, c] = 1;
                }

                //f(r,c) = f(r - 1,c) + f(r, c-1)
                for (int r = 1; r < grid.Length; r++)
                {
                    for (int c = 1; c < grid[0].Length; c++)
                    {
                        memo[r, c] = memo[r - 1, c] + memo[r, c - 1];
                    }
                }

                return memo[memo.GetLength(0) - 1, memo.GetLength(1) - 1];

            }

            public static int CountWithObstacle_TopDown(int[][] grid, int r, int c, int[][] dp)
            {
                if (c == 0 && r == 0)
                {
                    //first row/column
                    return (grid[r][c] == 0) ? 1 : 0;
                }

                if (r < 0 || c < 0 || grid[r][c] == 1)
                {
                    return 0;
                }

                if (dp[r][c] != -1)
                    return dp[r][c];

                if (grid[r][c] == 0)
                {
                    dp[r][c] = CountWithObstacle_TopDown(grid, r - 1, c, dp) + CountWithObstacle_TopDown(grid, r, c - 1, dp);
                    return dp[r][c];
                }
                else
                {
                    return 0;
                }
            }            
        }

        //343. Integer Break
        class IntegerBreak
        {
            //https://leetcode.com/problems/integer-break/
            public static int intBreak(int n)
            {
                //Page 76.....
                //Almost Same as Cutropes - CutRopes.MaxProduct
                
                int[] tabulation = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {
                    int max = 0;
                    for (int j = 1; j < i; j++) //you cant make j<=i, as we need atleast one cut
                    {
                        //max = Math.Max(max, Math.Max(i, tabulation[i - j] * j));
                        //max = Math.Max(max, Math.Max((i > 3 ? (i-1) : i), tabulation[i - j] * j));

                        //multiply cut value 'j' with rest (i-j)
                        max = Math.Max(max, Math.Max((i - j) * j, tabulation[i - j] * j));
                    }
                    tabulation[i] = max;
                }
                return tabulation[n];
            }
        }

        class CutRopes
        {
            //https://www.geeksforgeeks.org/maximum-product-cutting-dp-36/
            public static int MaxProduct(int n)
            {
                //O(N^2)
                int[] memo = new int[n + 1];
                for (int r = 0; r < memo.Length; r++)
                {
                    memo[r] = -1;
                }
                int ret = recursion(n, memo);


                //O(N^2)
                int[] tabulation = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {
                    int max = 0;
                    for (int j = 1; j <= i ; j++)
                    {
                        //max = Math.Max(max, Math.Max(i, tabulation[i - j] * j));
                        //max = Math.Max(max, Math.Max((i > 3 ? (i-1) : i), tabulation[i - j] * j));
                        max = Math.Max(max, Math.Max((i - j) * j, tabulation[i - j] * j));
                    }
                    tabulation[i] = max;
                }
                return tabulation[n];

                /* O(N) ***********************
                // n equals to 2 or 3 must be handled explicitly 
                if (n == 2 || n == 3) return (n - 1);

                // Keep removing parts of size 3 while n is greater than 4 
                int res = 1;
                while (n > 4)
                {
                    n -= 3;
                    res *= 3; // Keep multiplying 3 to res 
                }
                return (n * res); // The last part multiplied by previous parts
                */

                /*
                Dictionary<string, bool> memo = new Dictionary<string, bool>();
                int[] maxProduct = new int[1];
                DFS(n, 0, 1, new List<int>(), memo, maxProduct);
                return maxProduct[0];*/
            }

            //private static int recursion(int n, int prod, int[] memo)
            private static int recursion(int n, int[] memo)
            {
                if (n == 0 || n == 1)
                {
                    return 0;
                }   

                int max = 0;
                int maxValAt = 0;
                int maxProdAt = 0;
                for (int i = 1; i <= n; i++)
                {
                    //*******************************************
                    //if we cut the rope at 'i'... (n -i) is left
                    //*******************************************
                    if (memo[n - i] != -1)
                    {
                        //find the max value at (n-i)
                        maxValAt = Math.Max((n - i), memo[n - i]);
                        //calculate max product at (n-i)
                        maxProdAt = maxValAt * i;

                        memo[n] = Math.Max(max, maxProdAt);
                        return memo[n];
                    }

                    //max = Math.Max(max, Math.Max((n - i) * i, recursion(n - i, prod * i, memo) * i));

                    //find the max value at (n-i)
                    maxValAt = Math.Max((n - i), recursion(n-i, memo));
                    //calculate max product at (n-i)
                    maxProdAt = maxValAt * i;
                    max = Math.Max(max, maxProdAt);
                }
                memo[n] = max;

                return max;


                /*
                 if (n == 0 || n == 1)
                    return 0;

                int max = 0;
                for (int i = 1; i < n; i++)
                {
                    if (memo[n - i] != -1)
                        return Math.Max(max, Math.Max(i * (n - i), memo[n - i] * i));

                    max = Math.Max(max, Math.Max(i * (n - i), DFS1(n - i, memo) * i));

                    //max = Math.Max(max, Math.Max(i * (n - i), DFS(n - i) * i));
                }
                memo[n] = max;

                return max;
                 */
            }
                        
            private static bool DFS(int n, int sum, int product, IList<int> slate, Dictionary<string, bool> memo, int[] maxProduct)
            {
                //O(N^N)
                if(sum > n )
                {
                    return false;
                }
                if (sum == n)
                {
                    //calculate max product
                    maxProduct[0] = Math.Max(maxProduct[0], product);

                    foreach (int s in slate) 
                        Console.Write(s + ",");
                    Console.WriteLine();

                    return  true;
                }
                else
                {
                    bool canDFSPathMakeToSum = false;
                    for (int i = 1; i < n; i++ )
                    {
                        //string key = "[" + string.Join(",", slate) + "]" + "_Sum=" + sum + "_Prod=" + (cProd) + "_Next=" + i;

                        //you need atleast sum,cprod and i into key
                        string key = "Sum=" + sum + "_Prod=" + (product) + "_Next=" + i;
                        if (memo.ContainsKey(key))
                        {
                            //if adding 'i' to curent sum == n ???? then update the max product
                            if (memo[key]) 
                                maxProduct[0] = Math.Max(maxProduct[0], product * i);

                            continue;
                        }

                        slate.Add(i);
                        var val = DFS(n, sum + i, product * i, slate, memo, maxProduct);
                        slate.RemoveAt(slate.Count - 1);

                        //update memo
                        memo[key] = val;

                        //if doent make the sum, then further loop
                        if (!val)
                            continue;

                        //If sum of ANY dfs path originated at 'i' is n, then we must return TRUE
                        canDFSPathMakeToSum = canDFSPathMakeToSum || val;
                    }
                    return canDFSPathMakeToSum;
                }
            }

        }

        //1547. Minimum Cost to Cut a Stick
        class RodCutStick
        {
            //https://leetcode.com/problems/minimum-cost-to-cut-a-stick/
            //https://www.geeksforgeeks.org/cutting-a-rod-dp-13/
            public static int MaxCost(int n, int[] cuts)
            {
                //as a lazy manager, i should only think about what the last piece [i], 
                //when ever subordinates comes back with best price for [i-1], i will use my piece price[i] into it and find the best overall best price
                //**** Prefix of Optimal Substructure must be Optimal - Its a optimal Substucture*******
                //https://uplevel.interviewkickstart.com/resource/helpful-class-video-4891-6-503-0     5:35:46
                /*
                length | 1   2   3   4   [5]   6   7   [8]
                --------------------------------------------
                price  | 1   5   8   9   10  17  17   20 */

                //Cutting at index 7 for a length of 8, means NOT cutting at all.
                //cutting at other indices (i) for a length 8, max ( cutval(i) + maxval at (8-i) )

                int[] memo = new int[n + 1];
                for (int i = 1; i <= n; i++) //[5]/[8]
                {
                    int max = int.MinValue;
                    for(int j = 1; j <= i; j++)
                    {
                        max = Math.Max(max, memo[i-j] + cuts[j - 1]);
                    }
                    memo[i] = max;
                }
                return memo[n - 1];
            }
        }

        class Fibonacci
        {
            public static int Calculate(int n)
            {
                //Space(O(1)
                int[] tabulation = new int[3];
                tabulation[0] = 0;
                tabulation[1] = 1;
                for (int x = 2; x <= n; x++)
                {
                    tabulation[x % 3] = tabulation[(x - 1) % 3] + tabulation[(x - 2) % 3];
                }
                return tabulation[n % 3];

                /*
                //Space O(N)
                int[] tabulation = new int[n + 1];
                tabulation[0] = 0;
                tabulation[1] = 1;
                for (int x = 2; x <= n; x++)
                {
                    tabulation[x] = tabulation[(x - 1)] + tabulation[(x - 2)];
                }
                return tabulation[n];
                */
            }
        }

        //72. Edit Distance
        class LevenshteinDistanceMinEditDistance
        {
            //https://leetcode.com/problems/edit-distance/
            public static int Calculate(string strWord1, string strWord2)
            {
                //Min edit distance to convert word1 to word2

                //O(M*N)
                //Space - O(M*N)
                //Space could be O(1) using memo = int[2][2], as we use only last row and last column data

                /*     < -- strWord1 --->
                ---------------------------------
                |   |"" | k | i | t | t | e | n |  
                | -------------------------------
                | ""| 0 | 1 | 2 | 3 | 4 | 5 | 6 |   --> delete steps
                | -------------------------------
                | s | 1 | 1 | 2 | 3 | 4 | 5 | 6 |   
                | -------------------------------
                | i | 2 | 2 | 1 | 2 | 3 | 4 | 5 |
                | -------------------------------
                | t | 3 | 3 | 2 | 1 | 2 | 3 | 4 |
                | -------------------------------
                | t | 4 | 4 | 3 | 2 | 1 | 2 | 3 |
                | -------------------------------
                | i | 5 | 5 | 4 | 3 | 2 | 2 | 3 |
                | -------------------------------
                | n | 6 | 6 | 5 | 4 | 3 | 3 | 2 |
                | -------------------------------
                | g | 7 | 7 | 6 | 5 | 4 | 4 | 3 |
                --------------------------------

                //prev column is delete char
                //previous row is insert char
                */
                int[][] memo = new int[strWord2.Length + 1][];
                for (int r = 0; r < memo.Length; r++)
                {
                    memo[r] = new int[strWord1.Length + 1];
                }

                //for str1="", we need to INSERT only to equal string
                for (int r = 0; r < memo.Length; r++)
                {
                    memo[r][0] = r;
                }
                //for str2="", we need to DELETE only to equal string
                for (int c = 0; c < memo[0].Length; c++)
                {
                    memo[0][c] = c;
                }

                //f(i,j) = Min (  f(i-1,j-1) + 0(match cost), f(i-1, j) + 1(mismatch cost), f(i, j-1) + 1(mismatch cost))

                for (int r = 1; r < memo.Length; r++)
                {
                    for (int c = 1; c < memo[0].Length; c++)
                    {
                        char chWrd1 = strWord1[c - 1];
                        char chWrd2 = strWord2[r - 1];

                        if (chWrd1 == chWrd2)
                        {
                            //if chacraters are same - NO operation required (get the diagonal value only)
                            memo[r][c] = memo[r - 1][c - 1];
                        }
                        else
                        {
                            //if we update to eqalize, we came from previous diagonal character
                            int updateCharVal = memo[r - 1][c - 1]; //get the diagonal value

                            //if we delete 'chWrd1' to eqalize, we land up on previous character column 
                            int deleteCharVal = memo[r][c - 1]; //get the previous column value

                            //if we insert 'chWrd1' to eqalize, we move from previous character row
                            int insertCharVal = memo[r - 1][c]; //get the previous row value

                            //we have 3 choices, pick the Minimum cost of operation. 
                            memo[r][c] = Math.Min(updateCharVal, Math.Min(deleteCharVal, insertCharVal)) + 1; //1 is added to count a opration;
                        }
                    }
                }

                return memo[memo.Length - 1][memo[0].Length - 1];
            }
        }

        //1143. Longest Common Subsequence
        class LongestCommonSubsequence
        {
            //https://leetcode.com/problems/longest-common-subsequence/
            public int LongestCommonSubsequence1(string text1, string text2)
            {
                return 0;
            }
        }

        //1092. Shortest Common Supersequence
        class ShortestCommonSupersequence
        {
            //https://leetcode.com/problems/shortest-common-supersequence/
        }

        //516. Longest Palindromic Subsequence
        class LongestPalindromicSubsequence
        {
            //https://leetcode.com/problems/longest-palindromic-subsequence/
            public int LongestCommonSubsequence1(string text1, string text2)
            {
                return 0;
            }
        }

        class PhoneNumberNightMove
        {
            public static long numPhoneNumbers(int startdigit, int phonenumberlength)
            {
                //Build the Possible Next numbers selection on a digit
                List<List<int>> numberWithNightMove = new List<List<int>>();
                numberWithNightMove.Add(new List<int>(new int[] { 4, 6 }));     //digit 0
                numberWithNightMove.Add(new List<int>(new int[] { 6, 8 }));     //digit 1
                numberWithNightMove.Add(new List<int>(new int[] { 7, 9 }));     //digit 2
                numberWithNightMove.Add(new List<int>(new int[] { 4, 8 }));     //digit 3
                numberWithNightMove.Add(new List<int>(new int[] { 0, 3, 9 }));  //digit 4
                numberWithNightMove.Add(new List<int>()); //digit 5 --- cant get any digit per Knight rule
                numberWithNightMove.Add(new List<int>(new int[] { 0, 1, 7 }));  //digit 6
                numberWithNightMove.Add(new List<int>(new int[] { 2, 6 }));     //digit 7
                numberWithNightMove.Add(new List<int>(new int[] { 1, 3 }));     //digit 8
                numberWithNightMove.Add(new List<int>(new int[] { 2, 4 }));     //digit 9

                long[][] tabulation = new long[phonenumberlength + 1][];
                for (int r = 0; r < tabulation.Length; r++)
                {
                    tabulation[r] = new long[10];
                }

                //for 0 and 1 number length - It is the letter itself
                //if you are at a NUMBER, then its the number itself ****
                for (int r = 0; r < 2; r++)
                {
                    for (int c = 0; c < tabulation[0].Length; c++)
                    {
                        tabulation[r][c] = r;
                    }
                }

                //for word length = 2 or higher
                /* Recurrance Relation = 
                    f(phoneLength, start) = start = n
                                            Sum( f(phoneLength - 1 , numberWithNightMove[i....n]) )
                                            start = i   
                */
                for (int r = 2; r < tabulation.Length; r++)
                {
                    for (int c = 0; c < tabulation[0].Length; c++)
                    {
                        long sum = 0;
                        foreach (int c1 in numberWithNightMove[c])
                            sum += tabulation[r - 1][c1];

                        tabulation[r][c] = sum;
                    }
                }

                return tabulation[phonenumberlength][startdigit];


                long[][] memo = new long[phonenumberlength + 1][];
                for (int r = 0; r < memo.Length; r++)
                {
                    memo[r] = new long[10];
                }
                //Recursion - Initially EXPONENTIAL times
                //with Memoization - time complexity is O(phonenumberlength * startdigit)
                return Recursion_Helper(phonenumberlength, startdigit, numberWithNightMove, memo);
            }

            private static long Recursion_Helper(int n, int i, List<List<int>> numberWithNightMove, long[][] memo)
            {
                //Recursion - Initially EXPONENTIAL times
                //with Memoization - time complexity is O(phonenumberlength * startdigit)

                if (n <= 1)
                    return n;
                if (n == 2)
                    return numberWithNightMove[i].Count;

                if (memo[n][i] != 0)
                    return memo[n][i];

                long totalCount = 0;
                for (int x = 0; x < numberWithNightMove[i].Count; x++)
                {
                    long count = Recursion_Helper(n - 1, numberWithNightMove[i][x], numberWithNightMove, memo);
                    memo[n - 1][numberWithNightMove[i][x]] = count;

                    totalCount += count;
                }

                return totalCount;
            }

        }

        //746. Min Cost Climbing Stairs
        //70. Climbing Stairs
        class WaysToClimbStairs
        {
            //https://leetcode.com/problems/climbing-stairs/
            public static long UniquePathCount(int[] steps, int n)
            {
                //sort the steps if not sorted (because f the steps are not sorted, then it would be hard to follow pattern)
                Array.Sort(steps);

                long[] memo = new long[n + 1];
                memo[0] = 1;

                for (int floor = 1; floor <= n; floor++)
                {
                    for (int s = 0; s < steps.Length; s++)
                    {
                        if (floor < steps[s])
                            break;
                        if (floor - steps[s] > -1)
                        {
                            memo[floor] += memo[floor - steps[s]];
                        }
                    }
                }

                return memo[n];
            }

            //https://leetcode.com/problems/min-cost-climbing-stairs/
            public static long GetMinCost(int[] steps, int[] cost)
            {
                //Optimization problem *****

                //sort the steps if not sorted (because f the steps are not sorted, then it would be hard to follow pattern)
                Array.Sort(steps);

                int n = cost.Length;
                long[] memo = new long[n + 1];
                memo[0] = 0;

                for (int floor = 1; floor <= n; floor++)
                {
                    long min = long.MaxValue;
                    for (int s = 0; s < steps.Length; s++)
                    {
                        if (floor < steps[s])
                            break;
                        if (floor - steps[s] > -1)
                        {
                            min = Math.Min(min, memo[floor - steps[s]] + cost[floor - 1]);
                        }
                    }

                    memo[floor] = min;
                }

                //to reach the top, pay Min(last or last - 1) cost
                return Math.Min(memo[n] , memo[n - 1]);
            }
        }

        //198. House Robber
        //213. House Robber II
        class HouseOfRobbers
        {
            //https://leetcode.com/problems/house-robber/
            public static int MaxProfit(int[] values)
            {
                //Repated Subproblems *****
                //Using nornal recursion it could be Exponential ****
                //we have find max profit with each pairs  
                //Optimal Substurture

                //here at every Index, we calculate Max profit earned, from profits at previous 2 indicies
                int[] memo = new int[values.Length + 2];

                //f(n) = Max(f(n-1), f(n-2) + values[n])
                for (int x = 2; x < memo.Length; x++)
                {
                    //max of previous Index (x-1) and (sum of currentValue and previous max at x-2)
                    memo[x] = Math.Max(memo[x - 1], memo[x - 2] + values[x - 2]);
                }

                return memo[memo.Length - 1];
            }

            //https://leetcode.com/problems/house-robber-ii/
            public static int MaxProfit_CircularHome(int[] values)
            {
                //Repated Subproblems *****
                //Using nornal recursion it could be Exponential ****
                //we have find max profit with each pairs  
                //Optimal Substurture

                //*************************
                //As you can't select first and last together, then compare the max values into 2 phase
                //Exclude FIRST item and calculate the max profit
                //Exclude LAST item and calculate the max profit
                //return Max between 2 profits

                if (values.Length == 0)
                    return 0;

                if (values.Length == 1)
                    return values[0];

                //here at every Index, we calculate Max profit earned, from profits at previous 2 indicies
                int[] memo = new int[values.Length + 2];

                //f(n) = Max(f(n-1), f(n-2) + values[n])

                //Exclude the LAST item
                for (int x = 2; x < memo.Length - 1; x++)
                {
                    memo[x] = Math.Max(memo[x - 1], memo[x - 2] + values[x - 2]);
                }
                int valExcludeLast = memo[memo.Length - 2];

                //resetting the memo
                for (int x = 0; x < memo.Length; x++)
                    memo[x] = 0;

                //exclude the FIRST element
                for (int x = 3; x < memo.Length; x++)
                {
                    memo[x] = Math.Max(memo[x - 1], memo[x - 2] + values[x - 2]);
                }
                int valExcludeFirst = memo[memo.Length - 1];

                return Math.Max(valExcludeLast, valExcludeFirst);
            }
        }

        //322. Coin Change
        class CoinChange
        {
            //https://leetcode.com/problems/coin-change/
            public static int Min(int[] coins, int amount)
            {
                //O(N*M)
                //Space O(N*M)

                /*  amount --->
                    0  1  2  3  4  5
                   =================
                0 |[0]  0  0  0  0  0
                1 |[0]  1  1  1  1  1 
                2 |[0]  1  1  2  2  3
                5 |[0]  1  1  2  3 [4]
                */


                Array.Sort(coins);

                //Basicaly Its a permutation (enumeration) problems repeatATION ALLOWED.... SO its O(m^n) - exponential
                int[,] tab = new int[coins.Length + 1, amount + 1];
                for (int r = 0; r < tab.GetLength(0); r++)
                    for (int c = 0; c < tab.GetLength(1); c++)
                        tab[r, c] = int.MaxValue;

                for (int r = 0; r < tab.GetLength(0); r++)
                    tab[r, 0] = 0; //we can make zero amount from {0},{0,1},{0,1,5},{0,1,5,7} coins

                for (int r = 1; r < tab.GetLength(0); r++)  //coins
                {
                    for (int c = 1; c < tab.GetLength(1); c++) //amount
                    {
                        if (c - coins[r - 1] < 0) //this amt cant make (-ve val) with this coin, so copy previous values
                            tab[r, c] = tab[r - 1, c];
                        else
                        {
                            //Minimum between exclude and include current coin
                            tab[r, c] = Math.Min(tab[r - 1, c], tab[r, c - coins[r - 1]] + 1);
                        }
                    }
                }

                return tab[tab.GetLength(0) - 1, tab.GetLength(1) - 1];
            }

            public static int Min_1DArray(int[] coins, int amount)
            {
                //O(N*M)
                //Space O(N)

                Array.Sort(coins);

                //Basicaly Its a permutation (enumeration) problems repeatATION ALLOWED.... SO its O(m^n) - exponential
                int[] tab = new int[amount + 1];
                for (int r = 0; r < tab.GetLength(0); r++)
                    tab[r] = int.MaxValue;

                tab[0] = 0; //we can make zero amount from {0},{0,1},{0,1,5},{0,1,5,7} coins

                for (int r = 0; r < coins.Length; r++)  //coins
                {
                    for (int amt = 1; amt < tab.Length; amt++) //amount
                    {
                        if (amt - coins[r] >= 0) //this amt count be made (+ve val) with this coin, 
                        {
                            //Minimum between exclude and include current coin
                            tab[amt] = Math.Min(tab[amt], tab[amt - coins[r]] + 1);
                        }
                    }
                }

                return tab[tab.Length - 1];
            }
        }

        //118. Pascal's Triangle
        class SubsetSizeK_PascalTriangle
        {
            //https://uplevel.interviewkickstart.com/resource/rc-video-4890-63739-98-496-436191
            public static int Count(int n, int k)
            {
                //N choose K - Combination
                //only can traverse down and right direction
                int[,] tab = new int[n + 1, k + 1];

                //base case - for n == 0 and k = 0 or N=K
                for (int r = 0; r < tab.GetLength(0); r++)
                    tab[r, 0] = 1;

                //f(n C k) = f(n-1 C k) + f(n-1 C k-1)
                for (int r = 1; r < tab.GetLength(0); r++)
                {
                    for (int c = 1; c < tab.GetLength(1); c++)// Math.Min(r, k); c++)
                    {
                        //          exclude + include
                        tab[r, c] = tab[r - 1, c] + tab[r - 1, c - 1];
                    }
                }

                return tab[tab.GetLength(0) - 1, tab.GetLength(1) - 1];
            }

            //https://leetcode.com/problems/pascals-triangle/
            public static IList<IList<int>> GeneratePascalTraingle(int rows)
            {
                if (rows == 0)
                    return new List<IList<int>>();

                List<IList<int>> result = new List<IList<int>>();
                result.Add(new List<int>(new int[] { 1 })); //base case - n = 0 and n = k (i.e first row)

                for (int r = 1; r < rows; r++)
                {
                    IList<int> prevList = result[r - 1];
                    List<int> rowNewList = new List<int>();

                    rowNewList.Add(1);
                    for (int c = 1; c < prevList.Count; c++)
                    {
                        rowNewList.Add(prevList[c - 1] + prevList[c]);
                    }
                    rowNewList.Add(1);

                    result.Add(new List<int>(rowNewList));
                }

                return result;
            }
        }

        //120. Triangle
        class Traingle
        {
            //https://leetcode.com/problems/triangle/
            public static int MinimumTotal(IList<IList<int>> triangle)
            {
                //https://uplevel.interviewkickstart.com/resource/helpful-class-video-4891-6-503-0    2:54:00

                //Based on Pascal Triangle ***
                //O(r*c)
                //Space (r*c) -- But we can optimize it further to O(r), keep only 2 rows at a time

                int[,] dp = new int[triangle.Count, triangle[triangle.Count - 1].Count];

                dp[0, 0] = triangle[0][0];
                
                /* Pascal Traingle
                | -----------------------
                | 0 |   |   |   |   |   |
                | -----------------------
                | s | 1 |   |   |   |   |
                | -----------------------
                | i | 2 | 2 |   |   |   |
                | -----------------------
                | t | 3 | 3 | 2 |   |   |
                | -----------------------
                | t | 4 | 4 | 3 | 2 |   |
                | -----------------------
                | i | 5 | 5 | 4 | 3 | 2 |
                -------------------------
                */

                //set up base cases -- 2 extreme sides
                for (int r = 1; r < triangle.Count; r++)
                {
                    dp[r, 0] = dp[r - 1, 0] + triangle[r][0];
                    dp[r, r] = dp[r - 1, r - 1] + triangle[r][r];
                }

                //f(r,c) = Min(f(r-1,c), f(r-1, c-1)) + triangle[r][c]
                for (int r = 2; r < triangle.Count; r++)
                {
                    for (int c = 1; c < r; c++)
                    {
                        dp[r, c] = Math.Min(dp[r - 1, c - 1], dp[r - 1, c]) + triangle[r][c];
                    }
                }

                int min = int.MaxValue;
                for (int c = 0; c < triangle[triangle.Count - 1].Count; c++)
                {
                    min = Math.Min(min, dp[triangle.Count - 1, c]);
                }

                return min;
            }
        }

        //139. Word Break
        public class WordBreak
        {
            //https://leetcode.com/problems/word-break/
            public static bool IsValid(string str, IList<string> wordDict)
            {
                //manager picks  a word that is in hashmap, and delegate rest of string to subordinate to validate, 
                //if Subordinate comes back with TRUE, then Manager responses back with TRUE else FALSE

                HashSet<string> dict = new HashSet<string>(wordDict);

                /*
                bool?[] memo1 = new bool?[str.Length + 1];
                bool b =  DFS(str, dict, memo1, 0);
                */

                bool[] memo = new bool[str.Length + 1];

                //base case 
                memo[0] = true;

                for (int i = 1; i <= str.Length; i++)
                {
                    for (int j = 1; j <= i; j++)
                    {
                        //string lastWord = 0;
                        if (dict.Contains(str.Substring(j - 1, i - j + 1)))
                        {
                            if (memo[j - 1])
                            {
                                memo[i] = true;
                                break;
                            }
                        }
                        /*
                        if (dict.Contains(str.Substring(i - j - 1, i - j + 1)))
                        {
                            if (memo[i - j])
                            {
                                memo[i] = true;
                                break;
                            }
                        }*/
                    }
                }
                return memo[memo.Length - 1];
            }

            public static int CountPossibleWordBreaks(string str, IList<string> wordDict)
            {
                //its a same as IsValid, only you stire is count instead of boolean
                
                HashSet<string> dict = new HashSet<string>(wordDict);
                int[] memo = new int[str.Length + 1];

                for (int i = 1; i <= str.Length; i++)
                {
                    //if the 
                    if (dict.Contains(str.Substring(0, i)))
                    {
                        memo[i] += 1;
                    }

                    for (int j = 1; j <= i; j++)
                    {
                        if (dict.Contains(str.Substring(j - 1, i - j + 1)))
                        {
                            memo[i] += memo[j - 1];
                        }
                    }
                }
                return memo[memo.Length - 1];
            }

            //O(2^N), Space - O(N)
            private static bool DFS(string str, HashSet<string> dict, bool?[] memo, int index)
            {
                if (index == str.Length)
                    return true;

                if (memo[index].HasValue)
                        return memo[index].Value;

                for (int i = index; i < str.Length; i++ )
                {
                    if (dict.Contains(str.Substring(index, i - index + 1)))
                        if (DFS(str, dict, memo, i + 1))
                        {
                            memo[index] = true;
                            return true;
                        }
                }

                memo[index] = false;
                return false;
            }
        }

        class test
        {
            public static List<bool> equalSubSetSumPartition(List<int> s)
            {
                int total = 0;
                int minRange = 0;
                int maxRange = 0;
                foreach (int item in s)
                {
                    total += item;
                    if (item < 0) minRange += item;
                    if (item >= 0) maxRange += item;
                }

                if (total % 2 != 0)
                    return new List<bool>();

                int half = total / 2;
                int sumRange = maxRange - minRange + 1;
                bool[] result = new bool[s.Count];
                bool[][] memo = new bool[s.Count + 1][];
                for (int r = 0; r < memo.Length; r++)
                    memo[r] = new bool[sumRange + 1];

                //// if 0 items in the list and sum is non-zero
                //for (int c = 1; c <= half; c++)
                //    memo[0][c] = false;

                // if sum is zero
                //for (int r = 0; r <= s.Count; r++)
                //    memo[r][0] = true;


                //for (int c = 0; c < sumRange; c++)
                //{
                //    if(minRange + c == 0) //for SUM == 0
                //        memo[0][c] = true;
                //}

                /*
                bool[,] dp = new bool[s.Count, maxRange - minRange + 1];
                for (int i = 0; i < dp.GetLength(0); i++)
                {
                    for (int j = 0; j < dp.GetLength(1); j++)
                    {
                        //Console.Error.WriteLine($"{i}, {j}");
                        int sum = minRange + j;

                        if (s[i] == sum)
                        {
                            dp[i, j] = true;
                        }
                        else if (i > 0)
                        {
                            if (dp[i - 1, j])
                            {
                                dp[i, j] = true;
                            }
                            else if ((sum - s[i]) >= minRange && (sum - s[i]) <= maxRange)
                            {
                                if (dp[i - 1, sum - s[i] - minRange])
                                {
                                    dp[i, j] = true;
                                }
                            }
                        }
                    }
                }


                int halfColIndex = Math.Abs(minRange) + half;
                for (int r = 0; r < dp.GetLength(0); r++)
                {
                    string str = "";
                    for (int c = 0; c < dp.GetLength(1); c++)
                    {
                        str += (dp[r, c] ? "T" : "F") + ",";
                    }

                    Console.WriteLine(s[r] + ":" + str);// + dp[r,halfColIndex]);
                }*/


                int zezoSumColumnIndex = Math.Abs(minRange);
                memo[0][zezoSumColumnIndex] = true;  //for empty set{}, we can make 0 sum

                // if sum is zero
                for (int r = 1; r <= s.Count; r++)
                    memo[r][zezoSumColumnIndex] = true;

                // do for ith item
                for (int r = 1; r < s.Count; r++)
                {
                    // consider all sum from 1 to sum
                    for (int c = 0; c < sumRange; c++)
                    {
                        int sumColumn = minRange + c;

                        /*if (s[r] == sum)
                        {
                            //exclude the item (get data from previous row)
                            memo[r][c] = true;
                        }
                        else */

                        if (s[r - 1] > sumColumn)
                        {
                            //exclude the item (get data from previous row)
                            memo[r][c] = memo[r - 1][c];
                        }
                        else
                        {
                            // find subset with sum j by excluding or including the ith item
                            memo[r][c] = memo[r - 1][c] || (memo[r - 1][sumColumn - s[r - 1]]);// [c - s[r - 1]]);
                        }



                        /*
                        // don't include ith element if j-arr[i-1] is negative
                        if (s[r - 1] > c)
                            memo[r][c] = memo[r - 1][c];
                        else
                        {
                            // find subset with sum j by excluding or including the ith item
                            memo[r][c] = memo[r - 1][c] || (c - s[r - 1] >= 0 && c - s[r - 1] < half && memo[r - 1][c - s[r - 1]]);
                        }*/
                    }
                }

                int halfColIndex = Math.Abs(minRange) + half;
                for (int r = 0; r < memo.Length; r++)
                {
                    string str = "";
                    for (int c = 0; c < memo[0].Length; c++)
                    {
                        str += (memo[r][c] ? "T" : "F") + ",";
                    }

                    Console.WriteLine(":" + str);// + dp[r,halfColIndex]);
                }

                return result.ToList();
            }

            public static List<bool> equalSubSetSumPartition_Recursion(List<int> s)
            {
                int total = 0;
                foreach (int item in s)
                    total += item;

                if (total % 2 != 0)
                    return new List<bool>();

                bool[] result = new bool[s.Count];
                List<List<int>> resultItems = new List<List<int>>();
                List<int> slate = new List<int>();

                Dictionary<string, List<List<int>>> memo = new Dictionary<string, List<List<int>>>();

                if (DFS(s, 0, total / 2, 0, slate, result, resultItems, memo))
                    return result.ToList();
                else
                    return new List<bool>();
            }

            private static bool DFS(List<int> s, int index, int target, int amt, List<int> slate, bool[] result, List<List<int>> resultItems, Dictionary<string, List<List<int>>> memo)
            {
                string key = "Sum:" + amt + "_Index:" + index;

                //atleast you need 1 item in each groups (after splitting into 2 groups)
                if (amt == target && slate.Count > 0 && slate.Count < s.Count)
                {
                    List<int> items = new List<int>();
                    foreach (int sIndex in slate)
                    {
                        items.Add(s[sIndex]);
                        result[sIndex] = true;
                    }
                    resultItems.Add(items);
                    return true;
                }

                if (index == s.Count)// || amt > target)
                    return false;
                else
                {
                    //if key found, update amt and check with target
                    if (memo.ContainsKey(key))
                    {
                        int tempAmt = amt;
                        foreach (var exIncludeIndicies in memo[key])
                        {
                            //increase amount
                            foreach (var idx in exIncludeIndicies)
                                tempAmt += s[idx];

                            //if calculated amout is equal to target
                            if (tempAmt == target && exIncludeIndicies.Count > 0 && exIncludeIndicies.Count < s.Count)
                            {
                                List<int> items = new List<int>();
                                foreach (int sIndex in exIncludeIndicies)
                                {
                                    items.Add(s[sIndex]);
                                    result[sIndex] = true;
                                }
                                foreach (int sIndex in slate)
                                {
                                    items.Add(s[sIndex]);
                                    result[sIndex] = true;
                                }

                                resultItems.Add(items);
                                return true;
                            }
                            tempAmt = amt;
                        }
                        return false;
                    }


                    //exclude
                    if (DFS(s, index + 1, target, amt, slate, result, resultItems, memo))
                        return true;

                    if (!memo.ContainsKey(key))
                        memo.Add(key, new List<List<int>>());
                    memo[key].Add(new List<int>(slate));

                    //include
                    //slate.Add(s[index]);
                    slate.Add(index);
                    if (DFS(s, index + 1, target, amt + s[index], slate, result, resultItems, memo))
                        return true;

                    if (!memo.ContainsKey(key))
                        memo.Add(key, new List<List<int>>());
                    memo[key].Add(new List<int>(slate));

                    slate.RemoveAt(slate.Count - 1);
                }

                return false;
            }
        }


        class KnapSack
        {
            public static int Solve()
            {
                return 0;
            }
        }

        //256. Paint House
        class PaintHome
        {
            //https://uplevel.interviewkickstart.com/resource/helpful-class-video-4891-6-503-0 [6:02:27]
            public static int MinCost(int[][] costs)
            {
                if (costs.Length == 0)
                    return 0;

                int[,] memo = new int[3,costs.Length];   //3 color, number of houses

                //Base case
                //red
                memo[0, 0] = costs[0][0];

                //blue
                memo[1, 0] = costs[0][1];

                //green
                memo[2, 0] = costs[0][2];

                for (int i = 1; i < memo.GetLength(1); i++)
                {
                    //red
                    memo[0, i] = costs[i][0] + Math.Min(memo[1, i - 1], memo[2, i - 1]);
                    
                    //blue
                    memo[1, i] = costs[i][1] + Math.Min(memo[0, i - 1], memo[2, i - 1]);

                    //green
                    memo[2, i] = costs[i][2] + Math.Min(memo[0, i - 1], memo[1, i - 1]);
                }

                //last house
                return Math.Min(memo[0, memo.GetLength(1) - 1], Math.Min(memo[1, memo.GetLength(1) - 1], 
                        memo[2, memo.GetLength(1) - 1]));
            }
        }


        public static void runTest()
        {
            WordBreak.IsValid("aaaaaaa", new List<string>() { "aaaa", "aaa" });


            IntegerBreak.intBreak(2);

            WaysToClimbStairs.GetMinCost(new int[] { 1, 2 }, new int[] { 1, 100, 1, 1, 1, 100, 1, 1, 100, 1 });

            PaintHome.MinCost(new int[][] { new int[] { 17, 2, 17 }, new int[] { 16, 16, 5 }, new int[] { 14, 3, 19 } } );


            WordBreak.CountPossibleWordBreaks("kickstart", new List<string>() { "kick", "start", "kickstart", "is", "awe", "some", "awesome" });// "leet123code", new List<string>() { "leet", "code", "123" });
            
            CutRopes.MaxProduct(4);
            RodCutStick.MaxCost(8, new int[] { 1, 5, 8, 9, 10, 17, 17, 20 });
            
            WaysToClimbStairs.UniquePathCount(new int[] { 1, 2 }, 3);

            int[][] grd = new int[3][];
            grd[0] = new int[] { 1, 3, 1 };
            grd[1] = new int[] { 1, 5, 1 };
            grd[2] = new int[] { 4, 2, 1 };

            

            UniquePath.MinPathSum(grd);
            UniquePath.Count(3, 7);
            
            IList<IList<int>> triangle = new List<IList<int>>();
            triangle.Add(new List<int>(new int[]{2}));
            triangle.Add(new List<int>(new int[] { 3, 4 }));
            triangle.Add(new List<int>(new int[] { 6, 5, 7 }));
            triangle.Add(new List<int>(new int[] { 4, 1, 8, 3 }));
            Traingle.MinimumTotal(triangle);

            //int[][] grd = new int[3][];
            //grd[0] = new int[3] { 0, 0, 0 };
            //grd[1] = new int[3] { 0, 1, 0 };
            //grd[2] = new int[3] { 0, 0, 0 };

            UniquePathII.CountWithObstacle(grd);

            SubsetSizeK_PascalTriangle.GeneratePascalTraingle(1);
            SubsetSizeK_PascalTriangle.Count(3, 3);

            CoinChange.Min_1DArray(new int[] { 1, 5, 7 }, 9);

            test.equalSubSetSumPartition(new List<int>(new int[] { 12, 8, 0, -2, -3, 3, 2 })); // 7, 3,2,5,1 }));

            test.equalSubSetSumPartition_Recursion(new List<int>(new int[] { 26,22,27,22,1,-4,18,0,3,2,9,14,-16,29,1,-4,0,29,32,8,-29,-29,3,-30,32,17,-20,-31,25,0,16,0,4,16,30,20,15,22,-6,10,-9,12,31,-16,-2,-29,13,-9,-5,6,21,92,9,-32,-21,19,-3,24,0,-3,0,0,-13,-14,-5,0,6,28,6,-17,-2,3,22,3,23,-5,-16,24,0,12,22,3,0,2,0,-26,21,0,48,17,13,32,29,-30,3,5,-14,-16,13,57 }));


            test.equalSubSetSumPartition_Recursion(new List<int>(new int[] { 12, 8,0,-2,-3,3,2 } ));


            WaysToClimbStairs.UniquePathCount(new int[] { 32, 28, 2, 14, 21, 35, 10, 29, 39, 15, 8 }, 117);

            //PhoneNumberNightMove.numPhoneNumbers(6, 30);
            PhoneNumberNightMove.numPhoneNumbers(6, 20);

            //Fibonacci.Calculate(6);
            LevenshteinDistanceMinEditDistance.Calculate("kitten", "sitting");

            HouseOfRobbers.MaxProfit(new int[] { 2, 1, 3, 5 });
            HouseOfRobbers.MaxProfit_CircularHome(new int[] { 1, 2, 3, 1 });
        }
    }
}
