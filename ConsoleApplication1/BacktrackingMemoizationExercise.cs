using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class BacktrackingMemoizationExercise
    {
        class KnightTour {
            static int N = 8;
 
            /* A utility function to check if i,j are 
               valid indexes for N*N chessboard */
            static bool isSafe(int x, int y, int[,] sol) {
                return (x >= 0 && x < N && y >= 0 &&
                        y < N && sol[x,y] == -1);
            }
 
            /* A utility function to print solution matrix sol[N][N] */
            static void printSolution(int[,] sol) {
                for (int x = 0; x < N; x++) {
                    for (int y = 0; y < N; y++)
                        Console.Write(sol[x,y] + " ");
                    Console.WriteLine();
                }
            }
 
            /* This function solves the Knight Tour problem
               using Backtracking.  This  function mainly
               uses solveKTUtil() to solve the problem. It
               returns false if no complete tour is possible,
               otherwise return true and prints the tour.
               Please note that there may be more than one
               solutions, this function prints one of the
               feasible solutions.  */
            public static bool solveKnightTour() {

                /*The knight is placed on the first block of an empty board and, 
                moving according to the rules of chess, 
                must visit each square exactly once. */

                int[,] sol = new int[8,8];
 
                /* Initialization of solution matrix */
                for (int x = 0; x < N; x++)
                    for (int y = 0; y < N; y++)
                        sol[x,y] = -1;
 
               /* xMove[] and yMove[] define next move of Knight.
                  xMove[] is for next value of x coordinate
                  yMove[] is for next value of y coordinate */
                int[] xMove = {2, 1, -1, -2, -2, -1, 1, 2};
                int[] yMove = {1, 2, 2, 1, -1, -2, -2, -1};
 
                // Since the Knight is initially at the first block
                sol[0,0] = 0;
 
                /* Start from 0,0 and explore all tours using
                   solveKTUtil() */
                if (!solveKTUtil(0, 0, 1, sol, xMove, yMove)) {
                    Console.WriteLine("Solution does not exist");
                    return false;
                } else
                    printSolution(sol);
 
                return true;
            }
 
            /* A recursive utility function to solve Knight Tour problem */
            static bool solveKTUtil(int x, int y, int movei,
                                       int[,] sol, int[] xMove,
                                       int[] yMove) {
                int k, next_x, next_y;
                if (movei == N * N)
                    return true;
 
                /* Try all next moves from the current coordinate x, y */
                for (k = 0; k < 8; k++) {
                    next_x = x + xMove[k];
                    next_y = y + yMove[k];
                    if (isSafe(next_x, next_y, sol)) {
                        sol[next_x,next_y] = movei;
                        if (solveKTUtil(next_x, next_y, movei + 1,
                                        sol, xMove, yMove))
                            return true;
                        else
                            sol[next_x,next_y] = -1;// backtracking
                    }
                }
 
                return false;
            }
   
        }

        public class NQueenProblem
        {
            static int N = 4;
 
            /* A utility function to print solution */
            static void printSolution(int[,] board)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                        Console.Write(" " + board[i,j] + " ");
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
                    if (board[row,r] == 1)
                        return false;
 
                /* Check upper diagonal on left side */
                for (r=row, c=col; r>=0 && c>=0; r--, c--)
                    if (board[r,c] == 1)
                        return false;
 
                /* Check lower diagonal on left side */
                for (r=row, c=col; c>=0 && r<N; r++, c--)
                    if (board[r,c] == 1)
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
                        board[row,col] = 1;
 
                        /* recur to place rest of the queens */
                        if (solveNQUtil(board, col + 1) == true)
                            return true;
 
                        /* If placing queen in board[row][col] doesn't lead to a solution then remove queen from board[row][col] */
                        board[row,col] = 0; // BACKTRACK
                    }
                }
 
                /* If queen can not be place in any row in
                   this colum col, then return false */
                return false;
            }
 
            /* This function solves the N Queen problem using Backtracking.  
             * It returns false if queens cannot be placed, otherwise return true and
               prints placement of queens in the form of 1s.
               Please note that there may be more than one solutions, this function prints one of the feasible solutions.*/
            public static bool solveNQueen()
            {
                //no queen should share the same row, column or diagonal.
                int[,] board = new int[4,4]
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
 
        }
        
        class Pair
        {
            public Pair(int _x, int _y)
            {
                x = _x;
                y = _y;
            }
            public int x { get; set; }
            public int y { get; set; }

            public override bool Equals(Object obj)
            {
                //Check for null and compare run-time types.
                if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                {
                    return false;
                }
                else
                {
                    Pair p = (Pair)obj;
                    return (x == p.x) && (y == p.y);
                }
            }

            public override int GetHashCode()
            {
                return (x << 2) ^ y;
            }

            public override string ToString()
            {
                return String.Format("Point({0}, {1})", x, y);
            }
        }


        private static bool PathExistsWithMemoizationWithRightBotttomMove(int i, int j, int xLen, int yLen, int[,] arr, Hashtable hs)
        {
            Console.WriteLine("(" + i + "," + j + ")");

            // if (x,y outside maze) return false
            if (i < 0 || i >= xLen || j < 0 || j >= yLen || arr[i, j] == 1) //for value 1 means BLOCKED, move not allowed
            {
                Console.WriteLine("Exiting =>" + "(" + i + "," + j + ")");
                return false;
            }

            // if (x,y is goal) return true
            if (i == xLen - 1 && j == yLen - 1)
            {
                Console.WriteLine("Reached =>" + "(" + i + "," + j + ")");
                return true;
            }

            //check for already visited points, then return the value
            if (hs.ContainsKey(new Pair(i, j)))
            {
                Console.WriteLine("Found in Memo =>" + "(" + i + "," + j + ")");
                return (bool)hs[new Pair(i, j)];
            }


            //Move - Bottom and Right is allowed
            /* Move forward in x direction */
            /* If moving in x direction doesn't give
               solution then  Move down in y direction */
            if (PathExistsWithMemoizationWithRightBotttomMove(i + 1, j, xLen, yLen, arr, hs) ||
                PathExistsWithMemoizationWithRightBotttomMove(i, j + 1, xLen, yLen, arr, hs))
            {
                Console.WriteLine("Reached =>" + "(" + i + "," + j + ")");
                return true;
            }

            Console.WriteLine("Didn't find, Adding to Memo " + i + ":" + j);
            //mark point visited
            hs.Add(new Pair(i, j), false);
            return false;
        }

        private static bool PathExistsWithMemoizationWithAllDirectionMove(int i, int j, int xLen, int yLen, int[,] arr,
            Hashtable hs)
        {
            Console.WriteLine("(" + i + "," + j + ")");

            // if (x,y outside maze) return false
            if (i < 0 || i >= xLen || j < 0 || j >= yLen || arr[i, j] == 1 || hs.ContainsKey(new Pair(i, j)))
                //for value 1 means BLOCKED, move not allowed
            {
                Console.WriteLine("Existing =>" + "(" + i + "," + j + ")");
                return false;
            }

            // if (x,y is goal) return true
            if (i == xLen - 1 && j == yLen - 1)
            {
                Console.WriteLine("Reached =>" + "(" + i + "," + j + ")");
                return true;
            }

            //check for already visited points, then return the value
            if (hs.ContainsKey(new Pair(i, j)))
            {
                Console.WriteLine("Found in Memo =>" + "(" + i + "," + j + ")");
                return (bool) hs[new Pair(i, j)];
            }


            //Move - Bottom and Right is allowed
            /* Move forward in x direction */
            /* If moving in x direction doesn't give
               solution then  Move down in y direction */
            if (PathExistsWithMemoizationWithAllDirectionMove(i + 1, j, xLen, yLen, arr, hs))
            {
                Console.WriteLine("Reached on Down=>" + "(" + i + "," + j + ")");
                return true;
            }

            if(PathExistsWithMemoizationWithAllDirectionMove(i, j + 1, xLen, yLen, arr, hs))
            {
                Console.WriteLine("Reached on Right=>" + "(" + i + "," + j + ")");
                return true;
            }
            if (PathExistsWithMemoizationWithAllDirectionMove(i-1, j, xLen, yLen, arr, hs))
            {
                Console.WriteLine("Reached on Top=>" + "(" + i + "," + j + ")");
                return true;
            }
            if (PathExistsWithMemoizationWithAllDirectionMove(i, j-1, xLen, yLen, arr, hs))
            {
                Console.WriteLine("Reached on Left=>" + "(" + i + "," + j + ")");
                return true;
            }

            Console.WriteLine("Didn't find, Adding to Memo " + i + ":" + j);
            //mark point visited
            hs.Add(new Pair(i, j), false);
            return false;
        }

        private static void PrintWordFromPhoneDigit(string[] arr, int[] numbersToPrint, int currentNo, StringBuilder result)
        {
            if (currentNo >= numbersToPrint.Length)
            {
                Console.WriteLine(result);
                return;
            }

            int digit = numbersToPrint[currentNo];
            if (digit == 0 || digit == 1)
                return;

            for (int z = 0; z < arr[digit].Length; z++)
            {
                result.Append(arr[digit].ToCharArray()[z].ToString());
                PrintWordFromPhoneDigit(arr, numbersToPrint, currentNo + 1, result);
                
                result.Remove(result.Length - 1, 1);    
                
            }
        }

        public List<string> letterCombinations(string digits)
        {
            List<string> result = new List<string>();
            if (digits.Length == 0)
            {
                return result;
            }

            string[] digits2Letters = new string[] { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };

            result.Add("");//this line is important, otherwise result is empty and Java will default it to an empty String
            for (int i = 0; i < digits.Length; i++)
            {
                result = combine(digits2Letters[digits[i]], result);
            }

            return result;
        }

        List<string> combine(string letters, List<string> result)
        {
            List<string> newResult = new List<string>();

            for (int i = 0; i < letters.Length; i++)
            {
                //the order of the two for loops doesn't matter, you could swap them and it still works.
                foreach (string str in result)
                {
                    newResult.Add(str + letters[i]);
                }
            }
            return newResult;
        }


        public static IList<string> LetterCombinations(string digits)
        {
            Dictionary<char, string> map = new Dictionary<char, string>();
            map.Add('2', "abc");
            map.Add('3', "def");
            map.Add('4', "ghi");
            map.Add('5', "jkl");
            map.Add('6', "mno");
            map.Add('7', "pqrs");
            map.Add('8', "tuv");
            map.Add('9', "wxyz");

            int x = 1;
            List<string> res = new List<string>();
            res.Add("");
            foreach (char dig in digits)
            {
                List<string> track = new List<string>();
                foreach (char letter in map[dig])
                {
                    foreach (string prefix in res)
                    {
                        Console.WriteLine(x);
                        x++;
                        track.Add(prefix + letter);
                    }
                }
                res = track;
            }
            return res;

            
        }


        public static List<string> letterCombinations1(string digits) {

            Dictionary<char, string> map = new Dictionary<char, string>();
            map.Add('2', "abc");
            map.Add('3', "def");
            map.Add('4', "ghi");
            map.Add('5', "jkl");
            map.Add('6', "mno");
            map.Add('7', "pqrs");
            map.Add('8', "tuv");
            map.Add('9', "wxyz");

            List<string> res = new List<string>();
            int x = 0;
            combination("", digits, 0, res, map, ref x);
            return res;
        }

        private static void combination(string prefix, string digits, int idx, List<string> res, Dictionary<char, string> map, ref int x)
        {
            if (idx < digits.Length) {
                foreach (char letter in map[digits[idx]])
                {
                    x++;
                    Console.WriteLine(x);

                    combination(prefix + letter, digits, idx+1, res, map, ref x);
                }
            } else {
                res.Add(prefix);
            }
        }


        private static void PrintAllPermutationsOfStringOrLexicographic(string str, int start, int end)
        {
            //Lexicographic 
            if (start == end)
                Console.WriteLine(str);
            else
            {
                for (int i = start; i <= end; i++)
                {
                    str = swap(str,start,i);
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

        private static Dictionary<string, int> _memo = new Dictionary<string, int>();

        public static int ChangePossibilitiesTopDown(int amountLeft, int[] denominations, int currentIndex = 0)
        {
            string memoKey = string.Format("{0}, {1}", amountLeft,currentIndex );
            if (_memo.ContainsKey(memoKey))
            {
                Console.WriteLine(string.Format("grabbing memo [{0}]", memoKey));
                return _memo[memoKey];
            }

            // Base cases:
            // We hit the amount spot on. Yes!
            if (amountLeft == 0) {
                return 1;
            }

            // We overshot the amount left (used too many coins)
            if (amountLeft < 0)
            {
                return 0;
            }

            // We're out of denominations
            if (currentIndex == denominations.Length)
            {
                return 0;
            }

            // Choose a current coin
            int currentCoin = denominations[currentIndex];

            // Print out actual part of array
            Console.Write(new String('-', currentCoin) + string.Format("checking ways to make {0} with ", amountLeft));
            Console.WriteLine(string.Join(", ", denominations.Skip(currentIndex).Take(denominations.Length - currentIndex)));

            

            // See how many possibilities we can get
            // for each number of times to use currentCoin
            int numPossibilities = 0;

            int x = 0;
            while (amountLeft >= 0)
            {
                x++;
                Console.WriteLine(new String('-', currentCoin-1 + x) + "Before Recursion currentCoin {0}, amountLeft {1}", currentCoin, amountLeft);
                numPossibilities += ChangePossibilitiesTopDown(amountLeft, denominations, currentIndex + 1);
                amountLeft -= currentCoin;
                Console.WriteLine(new String('-', currentCoin - 1 + x) + "After Recursion currentCoin{0}, amountLeft {1}", currentCoin, amountLeft);
            }

            // Save the answer in our memo, so we don't compute it again
            _memo.Add(memoKey, numPossibilities);
            return numPossibilities;
        }

        public static void runTest()
        {
            LetterCombinations("234");
            //letterCombinations1("23");
            letterCombinations1("23456789");
            


            int x= ChangePossibilitiesTopDown(4, new int[]{1, 2, 3});
            return;

            int[,] arr = new int[5, 5]
                {
                    {0,0,0,0,0},
	                {1,1,1,1,0},
                    {0,0,0,0,0},
	                {0,1,1,1,1},
	                {0,0,0,0,0}
                };
            Hashtable memoization=new Hashtable();

            //Rat in a Maze
            bool canReach = PathExistsWithMemoizationWithRightBotttomMove(0, 0, arr.GetLength(0),
                arr.GetLength(1), arr, memoization);

            string[] phNumberLetters = new string[] {"", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"};
            int[] numbersToPrint = new int[]{2, 3};
            StringBuilder str = new StringBuilder();
            PrintWordFromPhoneDigit(phNumberLetters, numbersToPrint, 0, str);

            PrintAllPermutationsOfStringOrLexicographic("ABC", 0, 2);

            KnightTour.solveKnightTour();

            NQueenProblem.solveNQueen();
        }

    }
}
