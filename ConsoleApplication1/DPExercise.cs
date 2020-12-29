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
                //Permutation problem****
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
            
        }

        //64. Minimum Path Sum
        class MinPathSum
        {
            //https://leetcode.com/problems/minimum-path-sum/
            public static int GetMinPathSum(int[][] grid)
            {
                //Permutation problem****

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
                //Permutation problem****

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
                //(2 ^ n) -1 ways to cut the rod, its exponential problem

                //Atleast break into 2 pieces (can break into more number, but atleast into 2 pieces)
                //Page 76.....
                //Almost Same as Cutropes - CutRopes.MaxProduct

                // n=1 = is not a valid case, as the rod must be split ....
                // n=2 = 1 + 1 = 1 X 1
                // n=3 = 2 + 1 = 2 X 1

                /*
                 What could the last piece be?
                The last possible price could be of length 1,2,3...n
                let f(n) = max possible product obtained from 1...n
                In general, f(i) = max possible product obtaineable from rod of length i

                f(i) = max possible product obtaineable from rod of length i

                    cut=n
                f(i) = 	max { max(cut * f(i-cut), cut * (i-cut) }
	                cut=1
                */

                int[] tabulation = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {
                    int max = 0;
                    for (int j = 1; j < i; j++) //you cant make j<=i, as we need atleast one cut
                    {
                        //multiply cut value 'j' with rest (i-j) and tabulation[i - j] and pick the maximum
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

        class RodCutStick
        {
            //https://www.geeksforgeeks.org/cutting-a-rod-dp-13/
            public static int MaxCost(int n, int[] prices)
            {
                //Question :
                //Suppose you have a rod of length n, and you want to cut up rod and sell the pieces in a way that maximizes the total amount of money you get.

                //Combinatorial Optimization problem (we are optimizing sequence)****

                //2 ^ n ways to cut the rod... so exponential problem with overlapping subproblems
                //As a lazy manager, i should only think about the "last" piece of rod. You can cut the last piece into 0,1,2...(n-1),n different size ways.
                //If i cut the last piece for size 0 (don't cut), then i ned to find the max price for rest of rod of size (n)
                //If i cut the last piece for size 1, then i ned to find the max price for rest of rod of size (n-1)
                //If i cut the last piece for size 3, then i ned to find the max price for rest of rod of size (n-3)
                //If i cut the last piece for size 4, then i ned to find the max price for rest of rod of size (n-4)
                //If i cut the last piece for size n, then i ned to find the max price for rest of rod of size (0)

                //So at size ('rodLen' - for me it is the last piece) of rod, I'm looking for a cut (i iterate through all possible cut), which gives me the Maximum value --> Math.Max(max, memo[rodLen - i] + cuts[i]);

                //There are n different subproblems ****

                //when ever subordinates comes back with best price for [i-1], i use my piece price[i] into it and find the best overall best price
                //**** Prefix of Optimal Substructure must be Optimal - Its a optimal Substucture*******
                //https://uplevel.interviewkickstart.com/resource/library-video-878   4:01
                //https://uplevel.interviewkickstart.com/resource/helpful-class-video-4891-6-503-0     5:35:46
                /*
                length | 1   2   3   4   [5]   6   7   [8]
                --------------------------------------------
                Prices | 1   5   8   9   10   17  17   20 */

                //Cutting at index 7 for a length of 8, means NOT cutting at all.
                //cutting at other indices (i) for a length 8, max ( cutval(i) + maxval at (8-i) )

                /*
                 * f(i) = Max amount of money earned from cutting rod of length i
                 *  Size of LAST cut (cut= 1,2,3 ..i), 
	                            cut=i
                    f(i) = 	max { prices(cut) + f(i-cut) }
	                            cut=1
                */

                //Page 74 - Dynamic Programming (Alternative video)

                int[] memo = new int[n + 1];
                memo[0] = 0;                
                for (int rodLen = 1; rodLen <= n; rodLen ++) //[5]/[8]
                {
                    int max = int.MinValue;
                    for(int ctLength = 0; ctLength < rodLen; ctLength++)
                    {
                        max = Math.Max(max, memo[rodLen - ctLength - 1] + prices[ctLength]);
                    }
                    memo[rodLen] = max;
                }

                return memo[n];
            }
        }

        //1547. Minimum Cost to Cut a Stick
        class MinimumCosttoCutaStick
        {
            //https://leetcode.com/problems/minimum-cost-to-cut-a-stick/
            public int MinCost(int n, int[] cuts)
            {
                return 0;
            }
        }

        class Fibonacci
        {
            public static int Calculate(int n)
            {
                //Permutation problem****

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
        class MinEditDistanceOrLevenshteinDistance
        {
            //https://leetcode.com/problems/edit-distance/
            public static int Calculate(string strWord1, string strWord2)
            {
                //Min edit distance to convert word1 to word2
                //https://uplevel.interviewkickstart.com/resource/library-video-879    2:30
                /*
                Combinatorial explosion (sequences of letters) Optimization (Min number of operation) - 
                    (N choose K) = (Nc0) + (Nc1) +... (NcN/2)+..+.(NcN) = 2^N is exponential
                ----------------------------------------------------
                Pairwise alignment - is an alignment of gaps to positions 0,...M in string1 and 0,..N in string2, 
                so as to line up each letter in one sequence with either a letter, or a gap in other sequence.

                To create a alignment, we pick a letter from string1, you have 2 choices (1. pick a letter from string2 2. insert a blank). 
                So for every character from string1 you have 2 choices..   

                choices ->	2 | 2| 2| 2|..|2^N (exponential)
		                    ------------------
                string1		c1|c2|c3|c4|..|cN|

                ------------------------------------------------------------------------
                |h|o|r|s|e|     a	    _	    a	    a
                |r|o|_|s|_|	    a	    a	    _	    b
                -----------    match	insert	delete	edit 
                ========================================================================
                As a lazy manager, we wil look into the 'i'th character of string1 (we have 3 options - Delete a char, 
                insert a new char, keep the char (could be same as string2 or may not be)). 
                What we are looking from prefix( i-1 subordinates) is min edit distance between x0...xi-1 and Y0...Yj-1. 
                So we can add 'i'th cost into it(if last characters are different), to find the overall minimum. 
                Depends on what i choose on 'i'th position, the subproblems are different

                [Overlapping subproblems ***** and optimal substurcture]
                    |
                ----V----------------
                |x0,....xm-1| _  | xm
                |y0.....yn-1| yn | _
                ---------------------
                |x0,....xm-1| xm | _
                |y0.....yn-1| _  | yn
                ---------------------
                
                f(i,j) = edit distance between x0....xi and y0...yj
                f(i,j) = Min 	{ 	f(i-1, j) + 1 <-deletion
			                        f(i, j-1) + 1 <-insertion
			                        f(i-1, j-1) + (xi=yj)? 0: 1
		                        } 	

                //we have 2 parameters ... so we need 2D matrix... There are i * J unique sub problems
                so the 2D array with [J][i] diamemsions. We need extra cell to accommodate, the empty space issue (empty string from string1 or string2).

                //O(M*N)
                //Space - O(M*N)
                //***********************************************************
                //**********Space could be O(1) using memo = int[2][2], as we use only last row and last column data 
                //***********************************************************
                /*     < -- Word1 --->
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
                //prev row is insert char
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
                //https://uplevel.interviewkickstart.com/resource/library-video-879    3:50

                //combinatorial explosion.... Maximization problem - **we want to maximize the number of matches**
                /*
                Substring ---- for string1 of length n = n^2, string2 = n^2... so the total complexity would be n^4
                ex. [1234], substrings are
                -----------------------
                1,12,123,1234
                2,23,234
                3,34
                4

                Subsequence ---for string1 of length n = 2^n(exponential), string2 = 2^n... so the total complexity is 2^n*2^n = 2^2n(still exponential) 
                - so DP could be used
                //https://leetcode.com/problems/subsets/

                [1,2,3] : Subsequence or powerset are [],[1],[2],[1,2],[3],[1,3],[2,3],[1,2,3]
                

                Subsequence of 2 strings could be seen as alignments problems as below
                 ------------------------------------------------------------------------
                |h|o|r|s|e|     a	    _	    a	    a
                |r|o|_|s|_|	    a	    a	    _	    b
                -----------    match	insert	delete	edit 
                ========================================================================
                As a lazy manager, we wil look into the 'i'th character of text1 (we have 3 options - Delete a char, 
                insert a new char, keep the char (could be same as text2 or may not be)). 
                What we are looking from prefix( i-1 subordinates) is Max LCS between x0...xi-1 and Y0...Yj-1. 
                So we can add 'i'th cost into it(if last characters are SAME), to find the overall Maximum. 
                Depends on what i choose on 'i'th position, the subproblems are different

                [Overlapping subproblems ***** and optimal substurcture - Prefix of optimal substructure must be optimal]
                    |       (i-1)th|ith
                ----V------------  ----
                |x0,....xm-1| _    | xm
                |y0.....yn-1| yn   | _
                -----------------  ----
                |x0,....xm-1| xm   | _
                |y0.....yn-1| _    | yn
                -----------------  ----
                *************************************
                Here, we need to find the Max reward when 2 charaters at 'i', 'j' matches in 2 strings
                Reward  for match = 1
	                for mismatch = 0
	                for insertion = 0
	                for deletion = 0

                Base case 
                f(i,0) = 0, for all 'i', 0 for mismatch
                f(0,j) = 0, for all 'j', 0 for mismatch
                
                f(i,j) = Length of LCS between x0....xi and y0...yj
                
                f(i,j) = Max 	{ 	f(i-1, j) <-deletion
			                        f(i, j-1) <-insertion
			                        f(i-1, j-1) + (xi=yj)? 1: 0
		                        }
                     < -- text1 --->
                -----------------------------
                |   |"" | a | b | c | d | e |  
                | ---------------------------
                | ""| 0 | 0 | 0 | 0 | 0 | 0 |   --> delete steps
                | ---------------------------
                | a | 0 | 1 | 1 | 1 | 1 | 1 |   
                | ---------------------------
                | c | 0 | 1 | 1 | 2 | 2 | 2 |
                | ---------------------------
                | e | 0 | 1 | 1 | 2 | 2 | 3 |
                | ---------------------------
                */

                //prev column is delete char
                //prev row is insert char

                int[][] memo = new int[text2.Length + 1][];
                for (int r = 0; r < memo.Length; r++)
                {
                    memo[r] = new int[text1.Length + 1];
                }

                //for text1="", we need to INSERT only to equal string, so reward is 0, for this mismatch
                for (int r = 0; r < memo.Length; r++)
                {
                    memo[r][0] = 0;
                }
                //for text2="", we need to DELETE only to equal string, so reward is 0, for this mismatch
                for (int c = 0; c < memo[0].Length; c++)
                {
                    memo[0][c] = 0;
                }

                //f(i,j) = Max (  f(i-1,j-1) + 1(match reward), f(i-1, j) + 0(mismatch reward), f(i, j-1) + 0(mismatch reward))

                for (int r = 1; r < memo.Length; r++)
                {
                    for (int c = 1; c < memo[0].Length; c++)
                    {
                        char chWrd1 = text1[c - 1];
                        char chWrd2 = text2[r - 1];

                        if (chWrd1 == chWrd2)
                        {
                            //if chacraters are same - NO operation required (get the diagonal value only)
                            //add '1' reward for the match
                            memo[r][c] = memo[r - 1][c - 1] + 1; //1 reward for match;
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
                            memo[r][c] = Math.Max(updateCharVal, Math.Max(deleteCharVal, insertCharVal)) + 0; //0 reward for mismatch;
                        }
                    }
                }

                return memo[memo.Length - 1][memo[0].Length - 1];

                /*int[,] memo1 = new int[text1.Length, text2.Length];
                for (int x = 0; x < text1.Length; x++)
                    for (int y = 0; y < text2.Length; y++)
                        memo1[x, y] = -1;

                //RECURSION (Top down), start from FIRST character matcghing
                return LCS_Recursion_StartFrom1stChar(memo1, text1, text2, 0, 0);
                */
            }

            private int LCS_Recursion_StartFrom1stChar(int[,] memo, string text1, string text2, int text1Index, int text2Index)
            {
                if (text1Index == text1.Length || text2Index == text2.Length)
                    return 0;

                //if already processed, Memoization to save overlapping subproblems
                if (memo[text1Index, text2Index] != -1)
                    return memo[text1Index, text2Index];

                int result = 0;
                if (text1[text1Index] == text2[text2Index]) //if the last character match
                {
                    result = 1 + LCS_Recursion_StartFrom1stChar(memo, text1, text2, text1Index + 1, text2Index + 1);
                }
                else //if the last character doesn't match
                {
                    int resultText1 = LCS_Recursion_StartFrom1stChar(memo, text1, text2, text1Index + 1, text2Index);
                    int resultText2 = LCS_Recursion_StartFrom1stChar(memo, text1, text2, text1Index, text2Index + 1);

                    result = Math.Max(resultText1, resultText2);
                }

                //Memoization to save overlapping subproblems
                memo[text1Index, text2Index] = result;

                return result;
            }
        }

        //583. Delete Operation for Two Strings
        class DeleteOperationForTwoStrings
        {
            //https://leetcode.com/problems/delete-operation-for-two-strings/
            public static int MinDistance(string word1, string word2)
            {
                //https://uplevel.interviewkickstart.com/resource/library-video-879    4:25

                //Minimize deletion opeartion in both strings.... *****************
                //in our case its to minimize both insertion and deletion operation... how ??
                //insertion in one string is same as deletion in other string and deletion in one string is same as insertion in other string.. 
                //so we need to minimize insertion too....

                //In "MinEditDistance" problem, we found the Mininum of insertion/deletion/edition operation....
                //here we want to we found the Mininum of insertion/deletion... how we can stop the edition operation??? 
                //One way - increase Edition operation value to higher (3 or 4... 10), so it wont be picked by the Min function
                //2nd way - to ignore edition values (i.e value of word1[i] != wprd2[j]) in Min function
                /*
                f(i, j) = Min    {
                                    f(i - 1, j) < -deletion
 
                                    f(i, j - 1) < -insertion
 
                                    f(i - 1, j - 1) + 3
 
                                 }
                //prev column is delete char
                //prev row is insert char
                */
                int[][] memo = new int[word2.Length + 1][];
                for (int r = 0; r < memo.Length; r++)
                {
                    memo[r] = new int[word1.Length + 1];
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

                //f(i,j) = Min (  f(i-1,j-1) + 3(update cost), f(i-1, j) + 1(mismatch cost), f(i, j-1) + 1(mismatch cost))
                for (int r = 1; r < memo.Length; r++)
                {
                    for (int c = 1; c < memo[0].Length; c++)
                    {
                        char chWrd1 = word1[c - 1];
                        char chWrd2 = word2[r - 1];

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

                            //*********Way1 - we have 2 choices, pick the Minimum cost of operation.  
                            memo[r][c] = Math.Min(deleteCharVal, insertCharVal) + 1; //1 for insert and delete operation, Ignoring edition opearation.

                            //*********Way2 - we have 3 choices, pick the Minimum cost of operation. 
                            //memo[r][c] = Math.Min(updateCharVal + 3, Math.Min(deleteCharVal, insertCharVal) + 1); //1 for insert and delete operation, 3 for update operation (3 or higher to ensure that this value wont be picked by Min function..

                        }
                    }
                }

                return memo[memo.Length - 1][memo[0].Length - 1];
            }
        }

        //1092. Shortest Common Supersequence
        class ShortestCommonSupersequence
        {
            //https://leetcode.com/problems/shortest-common-supersequence/
            public static string MinCommonSupersequence(string str1, string str2)
            {
                //https://uplevel.interviewkickstart.com/resource/library-video-879   4:55
                /*Given two strings str1 and str2, return the shortest string that has both str1 and str2 as subsequences.  
                If multiple answers exist, you may return any of them.

                (A string S is a subsequence of string T if deleting some number of characters from T (possibly 0, and the 
                characters are chosen anywhere from T) results in the string S.)


                //the idea here is to Get Longest Common Subsequence bewteen 2 strings... then build a trace tree...
                */
                //prev column is delete char
                //prev row is insert char

                int[][] memo = new int[str2.Length + 1][];
                for (int r = 0; r < memo.Length; r++)
                {
                    memo[r] = new int[str1.Length + 1];
                }

                //for text1="", we need to INSERT only to equal string, so reward is 0, for this mismatch
                for (int r = 0; r < memo.Length; r++)
                {
                    memo[r][0] = 0;
                }
                //for text2="", we need to DELETE only to equal string, so reward is 0, for this mismatch
                for (int c = 0; c < memo[0].Length; c++)
                {
                    memo[0][c] = 0;
                }

                //f(i,j) = Max (  f(i-1,j-1) + 1(match reward), f(i-1, j) + 0(mismatch reward), f(i, j-1) + 0(mismatch reward))

                for (int r = 1; r < memo.Length; r++)
                {
                    for (int c = 1; c < memo[0].Length; c++)
                    {
                        char chWrd1 = str1[c - 1];
                        char chWrd2 = str2[r - 1];

                        if (chWrd1 == chWrd2)
                        {
                            //if chacraters are same - NO operation required (get the diagonal value only)
                            //add '1' reward for the match
                            memo[r][c] = memo[r - 1][c - 1] + 1; //1 reward for match;
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
                            memo[r][c] = Math.Max(updateCharVal, Math.Max(deleteCharVal, insertCharVal)) + 0; //0 reward for mismatch;
                        }
                    }
                }

                //https://uplevel.interviewkickstart.com/resource/library-video-879   5:22
                //Now do a trace back
                IList<char> result = new List<char>();
                int row = str2.Length;
                int col = str1.Length;

                while (row != 0 && col != 0)
                {
                    //check where the value in Memo[row][col] came from
                    if (memo[row][col] == memo[row - 1][col])
                    {
                        result.Add(str2[row - 1]);
                        row--;
                    }
                    else if (memo[row][col] == memo[row][col - 1])
                    {
                        result.Add(str1[col - 1]);
                        col--;
                    }
                    else //go diagonal
                    {
                        result.Add(str1[col - 1]);  // or result.Add(str2[row - 1]); 
                        row--;
                        col--;
                    }
                }

                while (row != 0)
                {
                    result.Add(str2[row - 1]);
                    row--;
                }
                while (col != 0)
                {
                    result.Add(str1[col - 1]);
                    col--;
                }

                return new string(result.Reverse().ToArray());
            }
        }

        //91. Decode Ways
        public class DecodeWays
        {
            //https://leetcode.com/problems/decode-ways/
            public static int NumDecodings(string s)
            {
                return 0;
            }
        }

        //516. Longest Palindromic Subsequence
        class LongestPalindromicSubsequence
        {
            //https://leetcode.com/problems/longest-palindromic-subsequence/
            public int GetLongestPalindromicSubsequence(string text1, string text2)
            {
                return 0;
            }
        }

        //935. Knight Dialer
        class KnightDialer
        {
            //https://leetcode.com/problems/knight-dialer/
            public static long numPhoneNumbers(int phonenumberlength)
            {
                int MOD = 1000000000 + 7;

                //https://uplevel.interviewkickstart.com/resource/library-video-878   1:10

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

                //10N unique sub problems
                //So DP table required is N X 10 (0,1...9)

                //Rows - > phone number length
                //Cols -> 0-9 digits

                /* Columns are Digits [0..9], Rows are Phone number length
                 __0__1__2__3__4__5__6__7__8__9_  
                 |1 |1 |1 |1 |1 |1 |1 |1 |1 |1 |     <--- ph num len = 1 (Base case = 1, as ph num len = 1, then its the digit itself, so count is 1) 
                 -------------------------------
                 |  |  |  |  |  |  |  |  |  |  |     <--- ph num len = 2
                 -------------------------------
                 |  |  |  |  |  |  |  |  |  |  |     <--- ph num len = 3
                 -------------------------------
                 */

                int[][] tabulation = new int[phonenumberlength][];
                for (int r = 0; r < tabulation.Length; r++)
                {
                    tabulation[r] = new int[10];
                }

                //for 0 and 1 number length - It is the letter itself
                //if you are at a NUMBER, then its the number itself ****
                for (int r = 0; r < 1; r++)
                {
                    for (int d = 0; d < 10; d++)
                    {
                        tabulation[r][d] = r + 1;
                    }
                }

                //for word length = 1 or higher
                /* Recurrance Relation = 
                    f(phoneLength, start) = phoneLength = n
                                            Sum( f(phoneLength - 1, numberWithNightMove[0,1,2,...8,9]) )
                                            phoneLength = 1   
                
                 As a lazy manager, I will look into the digit that i can put on 'i'th position. 
                 The last digit could be 0,1...9 depends on weather the previous digit (of previous phone_number_length) 
                 was a 4 or 6, a 6 or 8 and so on. **********************                 
                 */

                for (int r = 1; r < tabulation.Length; r++)
                {
                    for (int d = 0; d < 10; d++)
                    {
                        int sum = 0;
                        foreach (int d1 in numberWithNightMove[d])
                            //total numbers for new 'phone length' = sum of all previous digits from 'prev length's (digits those conform to knights move)
                            sum = (sum + tabulation[r - 1][d1]) % MOD; 

                        tabulation[r][d] = sum;
                    }
                }

                //Count total Phone numbers of size N, it is stored in n-1th row
                int total = 0;
                for (int d = 0; d < 10; d++)
                {
                    total = (total + tabulation[phonenumberlength - 1][d]) % MOD;
                }
                return total;


                int[][] memo = new int[phonenumberlength + 1][];
                for (int r = 0; r < memo.Length; r++)
                {
                    memo[r] = new int[10];
                }
                //Recursion - Initially EXPONENTIAL times
                //with Memoization - time complexity is O(phonenumberlength * startdigit)
                for (int i = 0; i < 10; i++)
                {
                    total = Recursion_Helper(phonenumberlength, i, numberWithNightMove, memo) % MOD;
                }
                return total;
            }

            private static int Recursion_Helper(int n, int i, List<List<int>> numberWithNightMove, int[][] memo)
            {
                int MOD = 1000000000 + 7;
                
                //Recursion - Initially EXPONENTIAL times
                //with Memoization - time complexity is O(phonenumberlength * startdigit)

                if (n <= 1)
                    return n;
                if (n == 2)
                    return numberWithNightMove[i].Count;

                if (memo[n][i] != 0)
                    return memo[n][i];

                int totalCount = 0;
                for (int x = 0; x < numberWithNightMove[i].Count; x++)
                {
                    int count = Recursion_Helper(n - 1, numberWithNightMove[i][x], numberWithNightMove, memo);
                    memo[n - 1][numberWithNightMove[i][x]] = count;

                    totalCount = (totalCount + count ) % MOD;
                }

                return totalCount;
            }

        }

        //276. Paint Fence
        class PaintFence
        {
            //https://leetcode.com/problems/paint-fence/
            public static int NumWays(int n, int k)
            {
                if (n == 0)
                    return 0;

                //https://uplevel.interviewkickstart.com/resource/library-video-878   2:39

                //Constraint - "No more than two adjacent fence" posts have the same color.

                //Counting problem... with Recursion its Exponential and also overlapping subporblems...

                //For the lazy manageer, The last color could be any of the k colors (if the previous two colors were different) 
                //or k-1 colors (if the previous two colors were the same)

                //If I need to know not just the total number of ways to color the previous n-1 fence posts, 
                //but also how many of them had the last two fences of the same color (or different color).... 
                //that means every worker needs to calculate the number of ways to color the (prefix) fence posts upto post i, 
                //such that the last fences were of the same color (or different color).
                
                /*
                 *  total(i) = same(i) + different(i);
                    same(i) = different(i - 1);
                    different(i) = total(i - 1) * (k - 1);
                */

                //There are total N sub problems
                int[] same = new int[n];
                int[] diff = new int[n];
                int[] total = new int[n];

                //Base cases
                same[0] = 0;  //(First post), fence no prev post to color with the same color. This is like a domino 
                diff[0] = k;  //(First post), so we can choose from k colors 
                total[0] = same[0] + diff[0];

                for (int i = 1; i < n; i++)
                {
                    same[i] = diff[i - 1]; //we can use prev different colors only
                    diff[i] = total[i - 1] * (k - 1);
                    total[i] = same[i] + diff[i];
                }

                return total[n - 1];
            }
        }

        //256. Paint House
        class PaintHome
        {
            //https://leetcode.com/problems/paint-house/
            public static int MinCost(int[][] costs)
            {
                //https://uplevel.interviewkickstart.com/resource/library-video-878   3:30
                //https://uplevel.interviewkickstart.com/resource/helpful-class-video-4891-6-503-0 [6:02:27]

                //Constraint - no two adjacent houses have the same color.

                //As a manager at nth position (we cant pick any cheap color. We have to a pick color that is NOT picked by the previous neighbour - we cant pick previous neighbour color), 
                //we need the following info from previous ('n-1') neighbour and add 'color cost' for red, green and blue for ith position

                //Min cost of painting n-1 houses ending with 'R' color
                //Min cost of painting n-1 houses ending with 'G' color
                //Min cost of painting n-1 houses ending with 'B' color

                //f(i) = number of way to color (n-1) houses, so that 'i'th house is red
                //f(i) = number of way to color (n-1) houses, so that 'i'th house is blue
                //f(i) = number of way to color (n-1) houses, so that 'i'th house is green

                //https://uplevel.interviewkickstart.com/resource/library-video-879   50:00
                //If I need to know NOT just the best way to color n - 1 houses, 
                //but the best way to color n-1 homes, so that the rightmost house could be green (or red, or blue), 
                //so, every worker must compute these 3 informations and pass it on to the immediate manager. 
                //If i need to color 'i'th house 'red', the predecessor's house color cannt be red.  
                //So this information needs to be split into 3 colors, so i use the previous appropriate color count to color ith house and pass it to my manager.

                if (costs.Length == 0)
                    return 0;

                //int[,] memo = new int[3, costs.Length];   //3 color, number of houses

                int[] red = new int[costs.Length];   //min cost of painting n-1 house with red color
                int[] green = new int[costs.Length];   //min cost of painting n-1 house with green color
                int[] blue = new int[costs.Length];   //min cost of painting n-1 house with blue color
                

                //Base case
                //red
                //memo[0, 0] = costs[0][0];
                red[0] = costs[0][0];

                //blue
                //memo[1, 0] = costs[0][1];
                blue[0]= costs[0][1];

                //green
                //memo[2, 0] = costs[0][2];
                green[0] = costs[0][2];

                for (int i = 1; i < costs.Length; i++)
                {
                    //red
                    red[i] = costs[i][0] + Math.Min(green[i - 1], blue[i - 1]);

                    //blue
                    blue[i] = costs[i][1] + Math.Min(red[i - 1], green[i - 1]);

                    //green
                    green[i] = costs[i][2] + Math.Min(red[i - 1], blue[i - 1]);
                }

                //last house
                return Math.Min(red[costs.Length - 1], Math.Min(green[costs.Length - 1],
                        blue[costs.Length - 1]));
            }
        }

        //265. Paint House II
        class PaintHomeII
        {
            //https://leetcode.com/problems/paint-house-ii/
            public static int MinCostII(int[][] costs)
            {
                if (costs.Length == 0)
                    return 0;

                int[,] memo = new int[costs.Length, costs[0].Length];   //number_of_houses X colors

                //total (n*K) cells
                //complexity - (nk * kLogK) with heap, to find the Minimum amount all columns
                //complexity - (n * K). For Each row once only, traverse all columns and find the min and 2nd min O(k). 
                //So the total runtime for n*K cells is O(n*K), O(1) for finding min of each cell.

                /*
                f(i, c) = min cost to paint houses(0,1...i - 1,i) with the last house colored with 'c'

                f(i, c) =   min over
                            all colors  { f(i - 1, k)}
                            k != c
                */
                //Base case
                //for coloring the first home....
                for (int c = 0; c < memo.GetLength(1); c++)
                {
                    memo[0, c] = costs[0][c];
                }

                //heap - k log k
                //Heap<int> heap = new Heap<int>(memo.GetLength(1), true);

                int minVal = 0;
                int secondMinVal = 0;
                for (int r = 1; r < memo.GetLength(0); r++)
                {
                    //O(K) to find the min and 2nd min
                    minVal = int.MaxValue;
                    secondMinVal = int.MaxValue;
                    for (int c = 0; c < memo.GetLength(1); c++)
                    {
                        if(memo[r - 1, c] < minVal)
                        {
                            secondMinVal = minVal;
                            minVal = memo[r - 1, c];
                        }
                        else if (memo[r - 1, c] < secondMinVal)
                        {
                            secondMinVal = memo[r - 1, c];
                        }
                        //heap.Push(new HeapNode<int>(memo[r - 1, c]));
                    }

                    for (int c = 0; c < memo.GetLength(1); c++)
                    {
                        int min = minVal;
                        if(memo[r - 1, c] == minVal)
                        {
                            min = secondMinVal;
                        }

                        /*
                        //int min = 0;
                        if (heap.Top().val == memo[r - 1, c])
                        {
                            int poppedVal = heap.Pop().val;
                            if (heap.Count > 0)
                                min = heap.Top().val;
                            else
                                min = poppedVal;
                            heap.Push(new HeapNode<int>(poppedVal));
                        }
                        else
                        {
                            min = heap.Top().val;
                        }*/

                        memo[r, c] = costs[r][c] + min;
                    }

                    //heap.Clear();
                }

                int lastRow = memo.GetLength(0) - 1;
                if (lastRow < 0)
                    lastRow = 0;

                minVal = memo[lastRow, 0];
                for (int c = 1; c < memo.GetLength(1); c++)
                {
                    if (memo[lastRow, c] < minVal)
                    {
                        minVal = memo[lastRow, c];
                    }
                }
                return minVal;

                //heap.Clear();
                //for (int c = 0; c < memo.GetLength(1); c++)
                //{
                //    heap.Push(new HeapNode<int>(memo[lastRow, c]));
                //}
                //last row is the last home
                //return heap.Top().val;
            }
        }

        //70. Climbing Stairs
        class WaysToClimbStairs
        {
            //https://leetcode.com/problems/climbing-stairs/
            public static long UniquePathCount(int[] steps, int n)
            {
                //Permutation problem****
                //f(N) = f(N-1) + f(n-2);

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
        }

        //746. Min Cost Climbing Stairs
        class MinCostClimingStairs
        {
            //https://leetcode.com/problems/min-cost-climbing-stairs/
            public static long GetMinCost(int[] steps, int[] cost)
            {
                //Permutation problem****
                //Optimization problem *****
                //f(N) = Min(f(N-1), f(n-2)) + cost[n];

                //sort the steps if not sorted (because f the steps are not sorted, then it would be hard to follow pattern)
                Array.Sort(steps);

                int n = cost.Length;
                long[] memo = new long[n + 1];

                //As are are allowed to start from Floor 0 or 1

                //memo[0] = 0;
                memo[1] = cost[0];
                memo[2] = Math.Min(memo[1] + cost[1], cost[1]);
                
                for (int floor = 3; floor <= n; floor++)
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
                return Math.Min(memo[n], memo[n - 1]);
            }
        }

        //1137. N-th Tribonacci Number
        class NthTribonacciNumber
        {
            //https://leetcode.com/problems/n-th-tribonacci-number/
            public static int Tribonacci(int n)
            {
                int[] memo = new int[4];
                //T0 = 0, T1 = 1, T2 = 1, and Tn+3 = Tn + Tn + 1 + Tn + 2 for n >= 0.
                memo[0] = 0;
                if (n >= 1) memo[1] = 1;
                if (n >= 2) memo[2] = 1;

                for (int i = 3; i <= n; i++)
                {
                    memo[i % 4] = memo[(i - 3) % 4] + memo[(i - 2) % 4] + memo[(i - 1) % 4];
                }

                /*
                int[] memo = new int[n + 1];
                //T0 = 0, T1 = 1, T2 = 1, and Tn+3 = Tn + Tn + 1 + Tn + 2 for n >= 0.
                memo[0] = 0;
                if (n >= 1) memo[1] = 1;
                if (n >= 2) memo[2] = 1;

                for (int i = 3; i < memo.Length; i++)
                {
                    memo[i] = memo[i - 3] + memo[i - 2] + memo[i - 1];
                }*/

                return memo[n % 4];
            }
        }

        //790. Domino and Tromino Tiling
        class DominoTrominoTiling
        {
            //https://leetcode.com/problems/domino-and-tromino-tiling/
            static public int NumTilings(int N)
            {
                //https://uplevel.interviewkickstart.com/resource/library-video-876   2:30:12

                int MOD = 1000000000 + 7;

                /* N=1     N=2
                //  _      _ _ 
                // | |    | | |
                //  -      - - 
                //2 X 1   2 X 2
                */

                int[] Fmemo = new int[N + 1];
                Fmemo[1] = 1;
                if (N >= 2)
                    Fmemo[2] = 2;

                int[] Umemo = new int[N + 1];
                Umemo[1] = 1;
                if (N >= 2)
                    Umemo[2] = 2;

                int[] Lmemo = new int[N + 1];
                Lmemo[1] = 1;
                if (N >= 2)
                    Lmemo[2] = 2;

                for (int i = 3; i <= N; i++)
                {
                    Fmemo[i] = ((Fmemo[i - 1] + Fmemo[i - 2]) % MOD +
                                (Lmemo[i - 2] + Umemo[i - 2]) % MOD) % MOD;

                    Lmemo[i] = (Fmemo[i - 1] + Umemo[i - 1]) % MOD;
                    Umemo[i] = (Fmemo[i - 1] + Lmemo[i - 1]) % MOD;
                }

                return Fmemo[N];
            }
        }


        //198. House Robber
        //213. House Robber II
        class HouseOfRobbers
        {
            //https://leetcode.com/problems/house-robber/
            public static int MaxProfit(int[] values)
            {
                //https://uplevel.interviewkickstart.com/resource/library-video-877    4:05:17

                //Repated Subproblems *****
                //Using nornal recursion it could be Exponential ****
                //we have find max profit with each pairs  
                //Maximization problem - 'COMBINATION' problem - Optimal Substurture

                //At every Index, we calculate Max profit earned, from profits at previous 2 indicies ... How ???
                //(by robbing nth house or by NOT robbing nth house] - 
                //Robbing nth House means, he we have to pick the n-2th house's max profit + nth value
                //Not robbing nth house means, he will have tp pick n-1th house's max profit
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
                // https://uplevel.interviewkickstart.com/resource/library-video-877   4:30

                //Repated Subproblems *****
                //Using nornal recursion it could be Exponential ****
                //we have find max profit with each pairs  
                //Optimal Substurture

                //*************************
                //As you can't select first and last home together, then compare the max values into 2 phase
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

        //983. Minimum Cost For Tickets
        class MinimumCostForTickets
        {
            //https://leetcode.com/problems/minimum-cost-for-tickets/
            public static int MincostTickets(int[] days, int[] costs)
            {
                //https://uplevel.interviewkickstart.com/resource/library-video-877   4:41

                //Look at last day.. how could i travel??? by 1-day pass, 7day pass or 30 days pass
                //It is having optimal sub structure
                int[] memo = new int[days.Length];
                memo[0] = costs.Min();  //for 1 day travel pick the cheapest price


                for (int i = 1; i < days.Length; i++)
                {
                    int case1 = 0;
                    int case2 = 0;
                    int case3 = 0;

                    //if day i is covered by 1 day pass
                    case1 = memo[i - 1] + costs[0]; //if day i is covered by 1 day pass

                    //if day i is covered by 7 day pass- then previous 6 days are also covered by it
                    int j = i - 1;
                    while (j >= 0 && days[j] >= days[i] - 6)
                        j--;
                    if (j >= 0)
                        case2 = memo[j] + costs[1];
                    else
                        case2 = costs[1];

                    //if day i is covered by 30 day pass- then previous 29 days are also covered by it
                    j = i - 1;
                    while (j >= 0 && days[j] >= days[i] - 29)
                        j--;
                    if (j >= 0)
                        case3 = memo[j] + costs[2];
                    else
                        case3 = costs[2];

                    memo[i] = Math.Min(case1, Math.Min(case2, case3));
                }

                return memo[memo.Length - 1];
            }
        }

        //322. Coin Change
        class CoinChange
        {
            //https://leetcode.com/problems/coin-change/
            public static int Min(int[] coins, int amount)
            {
                //Optimization problem - Comination Problems (order doesn't matter) **************
                //As a lazy manager, we need to findf, Minimum coins needs to make Amount[i],
                //Optimal Substructure is maintained - at Amount[i], we rely on previous optimized values 

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

                //F(n) = Min( f(amount - c1), f(amount - c2), f(amount - c3) + f(amount - ck) ) + 1

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
                //https://uplevel.interviewkickstart.com/resource/library-video-877     3:09:17

                //Optimization problem - Comination Problems (order doesn't matter) **************
                //As a lazy manager, we need to find, Minimum coins needs to make Amount[i],
                //Optimal Substructure is maintained - at Amount[i], we rely on previous optimized values 

                // Amt -> 1...... n
                // ----------------------------------
                // |1 |2 |..|..|..|  |  |n-2|n-1|*N*|
                // ----------------------------------

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

        //518. Coin Change 2
        class CoinChangeII
        {
            //https://leetcode.com/problems/coin-change-2/
            public int Change(int amount, int[] coins)
            {
                //Comination Problems (order doesn't matter) **************
                //As a lazy manager, we need to find, total coins needs to make Amount[i],

                // Amt array columns means amount value (1...... n.). This array will hold number of coins to make amount[i]
                // ----------------------------------
                // |1 |2 |..|..|..|  |  |n-2|n-1|*N*|
                // ----------------------------------

                //O(N*M)
                //Space O(N)

                Array.Sort(coins);

                //Basicaly Its a Cominatorial (enumeration) problems repeatATION ALLOWED.... SO its O(m^n) - exponential
                int[] tab = new int[amount + 1];

                //base case - we have only 1 way to make '0' amount - i.e doing nothing
                tab[0] = 1;

                for (int r = 0; r < coins.Length; r++)  //coins
                {
                    for (int amt = 1; amt < tab.Length; amt++) //amount
                    {
                        if (amt - coins[r] >= 0) //this amt count be made (+ve val) with this coin, 
                        {
                            //Sum of exclude and include current coin
                            tab[amt] += tab[amt - coins[r]];
                        }
                    }
                }

                return tab[tab.Length - 1];
            }

            public int CoinChange_DP(int amount, int[] coins)
            {
                /*  Amount -->>
                    0  1  2  3  4  5
                   =================
                0 |[1]  0  0  0  0  0
                1 |[1]  1  1  1  1  1 
                2 |[1]  1  1  2  2  3
                5 |[1]  1  1  2  3 [4]
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
                        dp[i][j] = countWithCoin + countWithOutCoin;
                    }
                }

                return dp[coins.Length][amount];
            }

            public void CoinChange_Recursion(int amount, int[] coins, int index, IList<int> demomination, IList<IList<int>> coinList)
            {
                //Same as find all SubSet of a set  problems (power set) -but only the sum of all should be = amount
                //O(N ^ maxDepth) - N = coin count, maxDepth = number of smallest coin required to reach 'amount'
                //Recursion with backtracking **************
                //https://leetcode.com/problems/subsets/solution/ (find all subsets - powerset)
                //This will do Timeout *****

                if (amount == 0)
                {
                    coinList.Add(new List<int>(demomination));
                    return;
                }
                else if (amount < 0 || index >= coins.Length)
                    return;

                //include a coin
                demomination.Add(coins[index]);
                CoinChange_Recursion(amount - coins[index], coins, index, demomination, coinList);

                //remove the last coin and try next coins on the list
                demomination.RemoveAt(demomination.Count - 1);

                //pick the next coin
                //index++;
                CoinChange_Recursion(amount, coins, index + 1, demomination, coinList);
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
                //Optimization problem - PATH MEANS 'PERMUTATION'

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
                    dp[r, 0] = dp[r - 1, 0] + triangle[r][0]; //--> left extreme - 1 path from Top
                    dp[r, r] = dp[r - 1, r - 1] + triangle[r][r]; //-->right extreme - 1 path from Top
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
        

        public static void runTest()
        {
            DeleteOperationForTwoStrings.MinDistance("sea", "eat");
            //[[20,19,11,13,12,16,16,17,15,9,5,18],[3,8,15,17,19,8,18,3,11,6,7,12],[15,4,11,1,18,2,10,9,3,6,4,15]]

            int[][] arr = new int[2][];
            //arr[0] = new int[12] { 20,19,11,13,12,16,16,17,15,9,5,18 };
            //arr[1] = new int[12] { 3,8,15,17,19,8,18,3,11,6,7,12 };
            //arr[2] = new int[12] { 15,4,11,1,18,2,10,9,3,6,4,15 };

            arr[0] = new int[3] { 1, 5, 3 };
            arr[1] = new int[3] { 2, 9, 4 };

            PaintHomeII.MinCostII(arr);

            NthTribonacciNumber.Tribonacci(4);

            WordBreak.IsValid("aaaaaaa", new List<string>() { "aaaa", "aaa" });


            IntegerBreak.intBreak(2);

            MinCostClimingStairs.GetMinCost(new int[] { 1, 2 }, new int[] { 1, 100, 1, 1, 1, 100, 1, 1, 100, 1 });

            PaintHome.MinCost(new int[][] { new int[] { 17, 2, 17 }, new int[] { 16, 16, 5 }, new int[] { 14, 3, 19 } } );


            WordBreak.CountPossibleWordBreaks("kickstart", new List<string>() { "kick", "start", "kickstart", "is", "awe", "some", "awesome" });// "leet123code", new List<string>() { "leet", "code", "123" });
            
            CutRopes.MaxProduct(4);
            RodCutStick.MaxCost(8, new int[] { 1, 5, 8, 9, 10, 17, 17, 20 });
            
            WaysToClimbStairs.UniquePathCount(new int[] { 1, 2 }, 3);

            int[][] grd = new int[3][];
            grd[0] = new int[] { 1, 3, 1 };
            grd[1] = new int[] { 1, 5, 1 };
            grd[2] = new int[] { 4, 2, 1 };

            MinimumCostForTickets.MincostTickets(new int[] { 1, 4, 6, 7, 8, 20 }, new int[]{ 2, 7, 15 });

            MinPathSum.GetMinPathSum(grd);
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

            //KnightDialer.numPhoneNumbers(6, 30);
            KnightDialer.numPhoneNumbers(20);// (6, 20);

            //Fibonacci.Calculate(6);
            MinEditDistanceOrLevenshteinDistance.Calculate("kitten", "sitting");

            HouseOfRobbers.MaxProfit(new int[] { 2, 1, 3, 5 });
            HouseOfRobbers.MaxProfit_CircularHome(new int[] { 1, 2, 3, 1 });

            
        }
    }
}
