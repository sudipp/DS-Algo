using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class ArrayExercise
    {
        

        public static int[] MergeSortedArrays(int[] myArray, int[] alicesArray) //O(n) time and O(n)
        {
            /*
             * O(nlgn) -- We can do better with O(n) time and O(n), see below logic
             * 
             * var mergedArray = new int[myArray.Length + alicesArray.Length];
            myArray.CopyTo(mergedArray, 0);
            alicesArray.CopyTo(mergedArray, myArray.Length);
            Array.Sort(mergedArray); //O(nlgn)
            return mergedArray;
            */

            // Set up our mergedArray
            var mergedArray = new int[myArray.Length + alicesArray.Length];

            int currentIndexAlices = 0;
            int currentIndexMine = 0;
            int currentIndexMerged = 0;

            #region DRY
            while (currentIndexMerged < mergedArray.Length)
            {
                if (currentIndexMine >= myArray.Length)
                {
                    // Case: my array is exhausted
                    mergedArray[currentIndexMerged] = alicesArray[currentIndexAlices];
                    currentIndexAlices++;
                }
                else if (currentIndexAlices >= alicesArray.Length)
                {
                    // Case: Alice's array is exhausted
                    mergedArray[currentIndexMerged] = myArray[currentIndexMine];
                    currentIndexMine++;
                }
                else if (myArray[currentIndexMine] < alicesArray[currentIndexAlices])
                {
                    // Case: my item is next
                    mergedArray[currentIndexMerged] = myArray[currentIndexMine];
                    currentIndexMine++;
                }
                else
                {
                    // Case: Alice's item is next
                    mergedArray[currentIndexMerged] = alicesArray[currentIndexAlices];
                    currentIndexAlices++;
                }

                currentIndexMerged++;
            }
            #endregion


            #region WET
            while (currentIndexMerged < mergedArray.Length)
            {
                bool isMyArrayExhausted = currentIndexMine >= myArray.Length;
                bool isAlicesArrayExhausted = currentIndexAlices >= alicesArray.Length;

                // Case: next comes from my array
                // My array must not be exhausted, and EITHER:
                // 1) Alice's array IS exhausted, or
                // 2) the current element in my array is less
                //    than the current element in Alice's array
                if (!isMyArrayExhausted && (isAlicesArrayExhausted
                        || (myArray[currentIndexMine] < alicesArray[currentIndexAlices])))
                {
                    mergedArray[currentIndexMerged] = myArray[currentIndexMine];
                    currentIndexMine++;
                }
                else
                {
                    // Case: next comes from Alice's array
                    mergedArray[currentIndexMerged] = alicesArray[currentIndexAlices];
                    currentIndexAlices++;
                }

                currentIndexMerged++;
            }
            #endregion

            return mergedArray;
        }

        //Find the smallest missing number from SORTED ARRAY
        private static int findFirstMissingFromSortedArray(int[] array, int start, int end) //O(Logn)
        {
            /*
            …1) If the first element is not same as its index then return first index
            …2) Else get the middle index say mid
            …………a) If arr[mid] greater than mid then the required element lies in left half.
            …………b) Else the required element lies in right half.
            */

            if (start > end)
                return end + 1;

            if (start != array[start])
                return start;

            int mid = (start + end) / 2;

            // Left half has all elements from 0 to mid
            if (array[mid] == mid)
                return findFirstMissingFromSortedArray(array, mid + 1, end);

            return findFirstMissingFromSortedArray(array, start, mid);
        }
        
        private static string RotateArray(string str, int rotFactor) //Space - O(n), O(n) Time
        {
            char[] arr = str.ToCharArray();
            /*This approach is based on the fact that when we rotate the array k times, 
            k elements from the back end of the array come to the front and 
            the rest of the elements from the front shift backwards.

            In this approach, we firstly reverse all the elements of the array. 
            Then, reversing the first k elements followed by reversing the rest n-kn−k elements gives us the required result.

            Original List                   : 1 2 3 4 5 6 7
            After reversing all numbers     : 7 6 5 4 3 2 1
            After reversing first k numbers : 5 6 7 4 3 2 1
            After revering last n-k numbers : 5 6 7 1 2 3 4 --> Result
            */
            //calculate mod
            rotFactor = rotFactor % arr.Length;  //O(n), space O(1)
            ReverseWords(arr, 0, arr.Length - 1);
            ReverseWords(arr, 0, rotFactor - 1);
            ReverseWords(arr, rotFactor, arr.Length - 1);
            return new string(arr);
        }

        private static void ReverseWords(char[] arr, int start, int end) //O(n), Space O(1)
        {
            //int end = arr.Length - 1;
            int mid = start + ((end - start) >> 1);
            for (int i = start; i <= mid; i++, end--) // O(log n)
            {
                char temp = arr[i];
                arr[i] = arr[end];
                arr[end] = temp;
            }
        }

        private static string ReverseWordsInSentence(string sentence) //O(n) , space - O(1)
        {
            //"sudip is a good guy" => "guy good a is sudip"

            char[] arr = sentence.ToCharArray();
            //reverse whole string
            ReverseWords(arr, 0, arr.Length - 1);

            //reverse each words separated by SPACE
            int wordStart = 0;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i + 1] == ' ')
                {
                    ReverseWords(arr, wordStart, i);
                    wordStart = i + 2;
                }
            }
            //rever the last word
            ReverseWords(arr, wordStart, arr.Length - 1);

            return new string(arr);
        }

        private static int[,] rotateArray90(int[,] imgArray)
        {            
            int N = imgArray.GetLength(0);
            // Traverse each cycle 
            for (int level = 0; level < N / 2; level++)
            {
                for (int j = level; j < N - level - 1; j++)
                {
                    // Swap elements of each cycle 
                    // in clockwise direction 
                    int temp = imgArray[level, j];
                    imgArray[level, j] = imgArray[N - 1 - j, level];
                    imgArray[N - 1 - j, level] = imgArray[N - 1 - level, N - 1 - j];
                    imgArray[N - 1 - level, N - 1 - j] = imgArray[j, N - 1 - level];
                    imgArray[j, N - 1 - level] = temp;
                }
            }

            return imgArray;
        }
        
        private static int bestBuySell(int[] ary)
        {
            int d = 0;
            int max = 0;
            for (int i = 1; i < ary.Length; i++)
            {
                d = ary[i] - ary[i - 1] + Math.Max(d, 0);
                if (d > max) max = d;
            }
            return max;
        }

        private static int MaxProfit(int[] prices)
        {
            int maxPro = 0;
            int minPrice = Int16.MaxValue;

            //get minimum prices to buy
            int[] maxProfits_at_i = new int[prices.Length];
            int[] minPrices_at_i = new int[prices.Length];

            for (int i = 0; i < prices.Length; i++)
            {
                minPrice = Math.Min(minPrice, prices[i]); //buyprice = Min(prices)
                maxPro = Math.Max(maxPro, prices[i] - minPrice);  //profit = max(sellprice - buyprice)

                minPrices_at_i[i] = minPrice;
                maxProfits_at_i[i] = maxPro;
            }
            return maxPro;
        }
        
        private static bool StringRotationToOtherString(string stringToCheck, string otherString)
        {
            return (stringToCheck + stringToCheck).Contains(otherString);
        }

        private static string LongestPalindromicSubstringInString(string str)
        {
            //Find​​ odd ​​palindromes:
            //    go ​​through​​ each​ ​character ​
            //        try ​​expanding ​​as ​​much ​​as ​​possible​
            //        compare ​​with ​​largest ​​palindrome​​ found​ ​so​​ far
            //Do​​the​​same​​with​​even​​palindromes

            //"abcababadef" => "ababa" 
            //"ffabbahh" => "abba"
            char[] arr = str.ToCharArray();

            int longest = 1;
            KeyValuePair<int, int> result = new KeyValuePair<int, int>(0, 0);

            //odd
            for (int i = 0; i < arr.Length; i++)
            {
                int offset = 0;
                while (isValidIndex(arr, i - 1 - offset) && isValidIndex(arr, i + 1 + offset)
                && arr[i - 1 - offset] == arr[i + 1 + offset])
                {
                    offset++;
                }
                int longestAtI = offset * 2 + 1;
                if (longestAtI > longest)
                {
                    longest = longestAtI;
                    result = new KeyValuePair<int, int>(i - offset, i + offset);
                }
            }
            //even
            for (int i = 0; i < arr.Length; i++)
            {
                int offset = 0;
                while (isValidIndex(arr, i - offset) && isValidIndex(arr, i + 1 + offset)
                && arr[i - offset] == arr[i + 1 + offset])
                {
                    offset++;
                }
                int longestAtI = offset * 2;
                if (longestAtI > longest)
                {
                    longest = longestAtI;
                    result = new KeyValuePair<int, int>(i + 1 - offset, i + offset);
                }
            }

            return new string(arr, result.Key, result.Value - result.Key + 1);
        }

        private static bool isValidIndex(char[] arr, int index)
        {
            if (arr == null || index < 0 || index >= arr.Length)
                return false;
            else return true;
        }
        
        private static void FindDuplicatesInArray(int[] arr)
        {
            //https://leetcode.com/explore/interview/card/amazon/76/array-and-strings/496/

            /*
            1- Traverse the given array from i= 0 to n-1 elements
                 Go to index arr[i]%n and increment its value by n.
            3- Now traverse the array again and print all those 
               indexes i for which arr[i]/n is greater than 1.

            This approach works because all elements are in range
            from 0 to n-1 and arr[i]/n would be greater than 1
            only if a value "i" has appeared more than once.
            */

            // First check all the values that are present in an array then go to that
            // values as indexes and increment by the size of array
            for (int i = 0; i < arr.Length; i++)
            {
                int index = Math.Abs(arr[i] % arr.Length);
                arr[index] += arr.Length;
            }

            // Now check which value exists more than once by dividing with the size
            // of array
            for (int i = 0; i < arr.Length; i++)
            {
                if ((arr[i] / arr.Length) > 1)
                    Console.Write(i + " ");
            }

        }

        public static int xSumSubarray(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            int i = 0, j = 0, sum = nums[0];
            while (i < nums.Length)
            {
                if (i > j)
                { // i inched forward, bring j back to i
                    j = i;
                    sum = nums[i];
                }
                else if (sum > k)
                {
                    sum = sum - nums[i++];
                }
                else if (sum < k)
                {
                    if ((j + 1) < nums.Length)
                        sum = sum + nums[++j];
                    else
                        break; // reached end, cannot expand further
                }
                else
                {
                    return j - 1;//new Pair<Integer>(i, j);
                }
            }
            return 0;
        }

        public static string LongestPalindrome(string s)
        {
            bool[,] dp = new bool[s.Length, s.Length];
            int maxStart = 0;
            int maxEnd = 0;
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < s.Length - i; j++)
                {
                    if (i == 0 || (s[j] == s[j + i] && (i == 1 || dp[j + 1, j + i - 1])))
                    {
                        dp[j, j + i] = true;
                        maxStart = j;
                        maxEnd = j + i;
                    }
                }
            }
            return s.Substring(maxStart, maxEnd + 1);
        }

        public static int MaxSubArrayLen(int[] nums, int k)
        {
            int runningSum = 0, max = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                runningSum = runningSum + nums[i];

                // when subarray starts from index '0' 
                if (runningSum == k)
                    max = i + 1;

                // check if 'runningSum - k' is present in 'map' or not 
                else if (map.ContainsKey(runningSum - k))
                    max = Math.Max(max, i - map[runningSum - k]);

                // make an entry for 'runningSum' if it is not present in 'map' 
                if (!map.ContainsKey(runningSum))
                    map.Add(runningSum, i);
            }
            return max;
        }

        public static void maxSubArrayLen(int[] nums, int k)
        {
            //Calculative cumulative sum from left to right.
            int[] cumulativeSum = new int[nums.Length + 1];
            for (int i = 0; i < cumulativeSum.Length; i++)
            {
                if (i == 0)
                {
                    cumulativeSum[0] = 0;
                }
                else
                    cumulativeSum[i] = cumulativeSum[i - 1] + nums[i - 1];
            }

            // stores ending index of maximum length sub-array having sum S
            int ending_index = -1;

            int maxLen = 0;
            for (int i = 0; i < cumulativeSum.Length; i++)
            {
                for (int j = i; j < cumulativeSum.Length; j++)
                {
                    /*
                    For each i, check not only the current sum but also (currentSum - previousSum), to see if there is any that equals k, and update max length.
                    ================================================================================================================
                    currentSumAt[j] - PreviousSumBefore[i] = K    
                    i.e PreviousSumBefore[i] = currentSumAt[i] - K

                    So PreviousSumBefore[i] = currentSumAt[i] - K
                    */
                    if ((cumulativeSum[j] - cumulativeSum[i]) == k || cumulativeSum[j] == k)  
                    {
                        if (maxLen < j - i)
                        {
                            maxLen = j - i;
                            ending_index = j-1;
                        }
                    }
                }
            }

            Console.WriteLine("[" + (ending_index - maxLen + 1) + ", " + ending_index + "]");
        }

        public static int maxSubArrayLen1(int[] nums, int k)
        {
            /*if (nums == null || nums.Length == 0)
                return 0;
            int[] store = new int[nums.Length + 1];
            for (int i = 0; i < store.Length; i++)
            {
                if (i == 0)
                {
                    store[0] = 0;
                }
                else
                    store[i] = store[i - 1] + nums[i - 1];
            }

            int max = 0;
            for (int i = 0; i < store.Length; i++)
            {
                for (int j = i; j < store.Length; j++)
                {
                    if ((store[j] - store[i]) == k)  //currentSumAt[i] - PreviousSumAt[i-1] = K     =>      [ PreviousSumAt[i-1] = currentSumAt[i] - K]
                    {
                        if (max < j - i)
                            max = j - i;
                    }
                }
            }
            return max;*/

            // len stores the maximum length of sub-array with sum S
            int len = 0;

            // stores ending index of maximum length sub-array having sum S
            int ending_index = -1;

            // consider all sub-arrays starting from i
            for (int i = 0; i < nums.Length; i++)
            {
                int sum = 0;

                // consider all sub-arrays ending at j
                for (int j = i; j < nums.Length; j++)
                {
                    // sum of elements in current sub-array
                    sum += nums[j];

                    // if we have found a sub-array with sum S
                    if (sum == k)
                    {
                        // update length and ending index of max length sub-array
                        if (len < j - i + 1)
                        {
                            len = j - i + 1;
                            ending_index = j;
                        }
                    }
                }
            }

            return len;
        }

        static int CompareLength(string a, string b)
        {
            // Return result of CompareTo with lengths of both strings.
            return (b + a).CompareTo(a + b);
        }

        class sudip : IComparer<string>
        {
            public int Compare(string a, string b)
            {
                return (b + a).CompareTo(a + b);
            }
        }

        public static void runTest()
        {
            findMinIndex(new int[] { 1, 2, 3 }, new List<int>(), 0, 2);

            findDistinctCount(new int[] { 5, 2, 3, 5, 4, 3 },0);


            quadTuple(new int[] { 10, 2, 3, 4, 5, 9, 7, 8 }, 8, 23);

            calculate(new int[] { 5, 2, 3, 5, 5, 4, 3 });
            

            findMaxSubarrayLength(new int[] { 1, 1, 0,0,0,0 }, new int[] { 1, 0, 1,0,0,1 });

            findPlatform(new int[] { 900, 940, 950, 1100, 1500, 1800 }, new int[]{ 910, 1200, 1120, 1130, 1900, 2000 },6);

            Main123456();


            List<String> numbers = new List<string>() { "10", "68", "97", "9", "21", "12" };
           

            List<String> numbers1 = new List<string>() { "10", "68", "97", "9", "21", "12" };
            // sort using a custom function object
            numbers.Sort((a,b)=> { return (b + a).CompareTo(a + b); });


            numbers1.Sort((a, b) => { return (a+b).CompareTo( b +a); });

            /*

                numbers1.Sort((a, b) => { return (a).CompareTo(b); });
                numbers.Sort((a, b) => { return (a).CompareTo(a); });
                */

            Console.WriteLine(string.Join("", numbers));


            Main123456();

            partition(new int[] { 6,0,8,2,3,0,4,0 });
            /*
            main1233();

            int xor2 = 5 ^ 1;

            int set_bit_no = xor2 & ~(xor2 - 1);
            */

            int[] arr = { 1,2, 3,4 };
            int x=0, y = 0;
            int prodAtI = 1;

            int totalProduct = 1;
            for (int i = 0; i < arr.Length; i++)
            {
                totalProduct *= arr[i];
            }

            for (int i = 0; i< arr.Length; i++)
            {
                Console.Write(totalProduct / arr[i] +",");
            }


            //printCombination(arr, n, r);


            //findLength(new int[] { 1, 56, 58, 57, 90, 91, 93, 92, 6, 45 }, 10);
            maxSubArrayLen(new int[] { 5, 6, -5, 5, 3, 5, 3, -2, 0 }, 8);
            //maxSubArrayLen(new int[] { 2, -5, 6, 3, 4,-2,-5 }, 3);
            //maxSubArrayLen(new int[] { 5, 6, -5, 5, 3, 5, 3, -2, 0 }, 8);

            int[] store = new int[] { 1, 2, 3 }; 
            for (int i = 0; i < store.Length; i++)
            {
                for (int j = i; j < store.Length; j++)
                {
                    Console.WriteLine(j + " " + i);
                }
            }

            //SingleNumber(new int[] {2, 2,1});
            LongestPalindrome("ac");

            int[,] imgArray = new int[4, 4]
                {
                    { 1, 2, 3, 4 }, 
                    { 10, 21, 31, 41 },
                    { 11, 22, 32, 42 },
                    { 12, 23, 33, 43 }
                };
            imgArray = rotateArray90(imgArray);

            bool b1 = StringRotationToOtherString("Sudip", "ps");
            string reverse = ReverseWordsInSentence("sudip is a good guy"); //"guy good a is sudip"
            var lp = LongestPalindromicSubstringInString("abcababadef");//"abcababadef" => "ababa" 
            var lp1 = LongestPalindromicSubstringInString("ffcabbachh");//"ffcabbachh" => "abba"

            var rotated = RotateArray("12345", 2); //45123

            FindDuplicatesInArray(new int[] { 1, 6,3,1,3,6,6 });

            int res = findFirstMissingFromSortedArray(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 10 }, 0, 8); //8

            int[] pricesInts = { 9, 7, 6, 1, 5, 6, 7, 1, 8, 9, 10, -1 };
            int max = MaxProfit(pricesInts);
            max = bestBuySell(pricesInts);
        }



        // Returns length of the longest  
        // contiguous subarray 
        static int findLength(int[] arr, int n)
        {
            int max_len = 1; // Initialize result 
            for (int i = 0; i < n - 1; i++)
            {
                // Initialize min and max for all  
                // subarrays starting with i 
                int mn = arr[i], mx = arr[i];

                // Consider all subarrays starting  
                // with i and ending with j 
                for (int j = i + 1; j < n; j++)
                {
                    // Update min and max in this 
                    // subarray if needed 
                    mn = Math.Min(mn, arr[j]);
                    mx = Math.Max(mx, arr[j]);

                    // If current subarray has all 
                    // contiguous elements 
                    if ((mx - mn) == j - i)
                        max_len = Math.Max(max_len,
                                      mx - mn + 1);
                }
            }
            return max_len; // Return result 
        }
















        // Optimized method to find equilibrium index of an array



















        // Given two boolean arrays X and Y, find the length of longest
        // continuous sequence that starts and ends at same index in both
        // arrays and have same sum
        public static int findMaxSubarrayLength(int[] X, int[] Y)
        {
            // create an empty map
            Dictionary<int, int> map = new Dictionary<int, int>();

            // to handle the case when required sequence starts from index 0
            map.Add(0, -1);

            // stores length of longest continuous sequence
            int res = 0;

            // sum_x, sum_y stores sum of elements of X[] and Y[] respectively
            // till current index
            int sum_x = 0, sum_y = 0;

            // traverse both lists simultaneously
            for (int i = 0; i < X.Length; i++)
            {
                // update sum_x and sum_y
                sum_x += X[i];
                sum_y += Y[i];

                // calculate difference between sum of elements in two lists
                int diff = sum_x - sum_y;

                // if difference is seen for the first time, then store the
                // difference and current index in a map
                if (!map.ContainsKey(diff))
                {
                    map.Add(diff, i);
                }

                // if difference is seen before, then update the result
                else
                {
                    res = Math.Max(res, i - map[diff]);
                }
            }

            return res;
        }










        // Standard partition process of QuickSort(). 
        // It considers the last element as pivot 
        // and moves all smaller element to left of 
        // it and greater elements to right 
        static int partition(int[] arr, int l, int r)
        {
            int x = arr[r], i = l;
            for (int j = l; j <= r - 1; j++)
            {
                if (arr[j] <= x)
                {
                    swap(arr, i, j);
                    i++;
                }
            }
            swap(arr, i, r);
            return i;
        }

        static void swap(int[] A, int i, int j)
        {
            int temp = A[i];
            A[i] = A[j];
            A[j] = temp;
        }
























        // Function to find a duplicate element in a limited range array
        public static int findDuplicate(int[] A)
        {
            int xor = 0;

            // take xor of all array elements
            for (int i = 0; i < A.Length; i++)
            {
                xor ^= A[i];
            }

            // take xor of numbers from 1 to n-1
            for (int i = 1; i < A.Length; i++)
            {
                xor ^= i;
            }

            // same elements will cancel out each other as a ^ a = 0,
            // 0 ^ 0 = 0 and a ^ 0 = a

            // xor will contain the missing number
            return xor;
        }

        private static void FindDuplicatesInArray1(int[] arr)
        {
            //https://leetcode.com/explore/interview/card/amazon/76/array-andstrings/496 /
/*
1- Traverse the given array from i= 0 to n-1 elements
Go to index arr[i]%n and increment its value by n.
3- Now traverse the array again and print all those
indexes i for which arr[i]/n is greater than 1.
This approach works because all elements are in range from 0 to n-1 and
arr[i]/n would be greater than 1
only if a value "i" has appeared more than once.
*/
// First check all the values that are present in an array then go to that
// values as indexes and increment by the size of array
            for (int i = 0; i < arr.Length; i++)
            {
                int index = Math.Abs(arr[i] % arr.Length);
                arr[index] += arr.Length;
            }
            // Now check which value exists more than once by dividing with the size
            // of array
            for (int i = 0; i < arr.Length; i++)
            {
                if ((arr[i] / arr.Length) > 1)
                    Console.Write(i + " ");
            }
        }

        // main function
        public static void main1233()
        {
            
            // input array contains n numbers between [1 to n - 1]
            // with one duplicate, where n = A.length
            int[] A = { 1, 4, 1, 2, 4 };
            FindDuplicatesInArray1(A);
        }


        // Function to move all zeros present in the array to the end
        public static void partition(int[] A)
        {
            int j = 0;

            // each time we encounter a non-zero, j is incremented and
            // the element is placed before the pivot
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] != 0)    // pivot is 0
                {
                    swap(A, i, j);
                    j++;
                }
            }
        }









        // The main function that prints all combinations of size r in arr[] of size n.
        // This function mainly uses combinationUtil()
        static void printCombination(int[] arr,
        int n, int r)
        {
            // A temporary array to store all combination one by one
            //int[] data = new int[r];
            List<int> data = new List<int>();

            // Sort array to handle duplicates
            Array.Sort(arr);
            // Print all combination using temprary array 'data[]'
            //combinationUtil(arr, data, 0,
            //n - 1, 0, r);


            //recurse(arr, data, r, 0, n - 1);
        }

        /* arr[] ---> Input Array
data[] ---> Temporary array to store current combination
start & end ---> Staring and Ending indexes in arr[] index ---> Current index in data[]
r ---> Size of a combination to be printed */
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


        // Driver Code
        static public void Main123456()
        {
            int[] arr = { 1, 2, 3, 4};
            int r = 3;
            int n = arr.Length;

            Array.Sort(arr);
            combinationUtil(arr, new List<int>(), 0, n, 0);
        }


        static int maxPathSum(int[] ar1, int[] ar2, int m, int n)
        {
            // initialize indexes for ar1[]
            // and ar2[]
            int i = 0, j = 0;
            // Initialize result and current
            // sum through ar1[] and ar2[].
            int result = 0, sum1 = 0, sum2 = 0;
            // Below 3 loops are similar to
            // merge in merge sort
            while (i < m && j < n)
            {
                // Add elements of ar1[] to sum1
                if (ar1[i] < ar2[j])
                    sum1 += ar1[i++];
                // Add elements of ar2[] to sum2
                else if (ar1[i] > ar2[j])
                    sum2 += ar2[j++];
                // we reached a common point
                else
                {
                    // Take the maximum of two
                    // sums and add to result
                    result += Math.Max(sum1, sum2);
                    // Update sum1 and sum2 for
                    // elements after this
                    // intersection point
                    sum1 = 0;
                    sum2 = 0;
                    // Keep updating result while
                    // there are more common
                    // elements
                    while (i < m && j < n &&
                    ar1[i] == ar2[j])
                    {
                        result = result + ar1[i++];
                        j++;
                    }
                }
            }
            // Add remaining elements of ar1[]
            while (i < m)
                sum1 += ar1[i++];
            // Add remaining elements of ar2[]
            while (j < n)
                sum2 += ar2[j++];
            // Add maximum of two sums of
            // remaining elements
            result += Math.Max(sum1, sum2);
            return result;
        }



        static int findPlatform(int[] arr, int[] dep, int n)
        {
            // Sort arrival and departure arrays
            Array.Sort(arr);
            Array.Sort(dep);
            // plat_needed indicates number of platforms needed at a time 
            int plat_needed = 0, result = 0; int i = 0, j = 0;

            // Similar to merge in merge sort to process all events in sorted order
            while (i < n && j < n)
            {
                // If next event in sorted order is arrival, increment count of platforms needed
                if (arr[i] <= dep[j])
                {
                    plat_needed++;
                    i++;
                    // Update result if needed
                    if (plat_needed > result)
                        result = plat_needed;
                }
                // Else decrement count of platforms needed
                else
                {
                    plat_needed--;
                    j++;
                }
            }
            return result;
        }






        public static string IntToBinaryString(int number)
        {
            const int mask = 1;
            var binary = string.Empty;
            while (number > 0)
            {
                // Logical AND the number and prepend it to the result string
                binary = (number & mask) + binary;
                number = number >> 1;
            }

            return binary;
        }

        static string GetIntBinaryString(int n)
        {
            char[] b = new char[32];
            int pos = 31;
            int i = 0;

            while (i < 32)
            {
                if ((n & (1 << i)) != 0)
                {
                    b[pos] = '1';
                }
                else
                {
                    b[pos] = '0';
                }
                pos--;
                i++;
            }
            return new string(b);
        }




        public static void calculate(int[] A)
        {
            int n = A.Length;
            // Map to mark elements as visited in the current window
            Dictionary<int, bool> visited = new Dictionary<int, bool>();
            // put all elements in a map
            foreach (int val in A)
            {
                if (!visited.ContainsKey(val))
                    visited.Add(val, false);
            }
            // points to left and right boundary of the current window
            // i.e. current window is formed by A[left, right]
            int right = 0, left = 0;
            // loop until right index of the current window is less
            // than the maximum index

            while (right < n)
            {
                while (right < n && !visited[A[right]])
                {
                    visited[A[right]] = true;
                    right++;
                }
                printSubArray(A, left, right - 1, n);
                // As soon as duplicate is found (A[right]),
                // terminate the above loop and reduce the window's size
                // from its left to remove the duplicate
                while (right < n && visited[(A[right])])
                {
                    visited[A[left]] = false;
                    left++;
                }
            }
        }

        public static void printSubArray(int[] A, int i, int j, int n)
        {
            if (i < 0 || i > j || j >= n)
            { // invalid input
                return;
            }
            for (int index = i; index < j; index++)
            {
                Console.Write(A[index] + ", ");
            }
            Console.WriteLine(A[j]);
        }





        public static bool quadTuple(int[] A, int n, int sum)
        {
            // create an empty map
            // key -> sum of a pair of elements in the array
            // value -> list storing index of every pair having that sum
            Dictionary<int, List<Pair>> map = new Dictionary<int, List<Pair>>();
            // consider each element except last element
            for (int i = 0; i < n - 1; i++)
            {
                // start from i'th element till last element
                for (int j = i + 1; j < n; j++)
                {
                    // calculate remaining sum
                    int val = sum - (A[i] + A[j]);
                    // if remaining sum is found in the map,
                    // we have found a Quadruplet
                    if (map.ContainsKey(val))
                    {
                        // check every pair having sum equal to remaining sum
                        foreach (Pair pair in map[val])
                        {
                            int x = pair.x;
                            int y = pair.y;
                            // if Quadruplet don't overlap, print it and
                            // return true
                            if ((x != i && x != j) && (y != i && y != j))
                            {
                                Console.WriteLine("Quadruplet Found ("
                                + A[i] + ", " + A[j] + ", "
                                + A[x] + ", " + A[y] + ")");
                                return true;
                            }
                        }
                    }
                    // insert current pair into the map
                    if (!map.ContainsKey(val))//A[i] + A[j]))
                    {
                        //map.Add(A[i] + A[j], new List<Pair>());
                        map.Add(val, new List<Pair>());
                    }
                    //map[A[i] + A[j]].Add(new Pair(i, j));
                    map[val].Add(new Pair(i, j));
                }
            }
            // return false if Quadruplet don't exist
            return false;
        }




        public static int trap(int[] heights)
        {
            // maintain two pointers left and right pointing to leftmost and
            // rightmost index of the input array
            int lo = 0, hi = heights.Length - 1, water = 0;
            int maxLeft = 0;
            int maxRight = 0;
            while (lo < hi)
            {
                maxLeft = Math.Max(maxLeft, heights[lo]);
                maxRight = Math.Max(maxRight, heights[hi]);


                if (heights[lo] < heights[hi])
                {
                    water += Math.Min(maxLeft, maxRight) - heights[lo];
                    lo++;
                }
                else
                {
                    water += Math.Min(maxLeft, maxRight) - heights[hi];
                    hi--;
                }
            }
            return water;
        }


        static void findDistinctCount(int[] arr, int k)
        {
            int left=0, right = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            
            while (right < arr.Length)
            {
                while (right < arr.Length && !dict.ContainsKey(arr[right]))
                {
                    dict.Add(arr[right], 1);
                    right++;
                }

                //print the subarray
                for (int x = left; x < right;x++)
                {
                    Console.Write(arr[x] + " ");
                }
                Console.WriteLine();

                while (right < arr.Length && dict[arr[right]] > 0)
                {
                    dict[arr[left]]--;
                    left++;
                }

                if (right < arr.Length && dict[arr[right]] == 0)
                    dict.Remove(arr[right]);
            }
        }




        static void findMinIndex(int[] A, List<int> lst, int start, int K)
        {
            if(lst.Count == K)
            {
                foreach(int i in lst)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }

            for(int x=start; x < A.Length; x++)
            {
                lst.Add(A[x]);

                findMinIndex(A, lst, x + 1, K);

                lst.RemoveAt(lst.Count-1);
            }
        }


    }
}
