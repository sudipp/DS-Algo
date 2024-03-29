﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class ArrayExercise
    {
        //15. 3Sum
        class ThreeSum
        {
            //https://leetcode.com/problems/3sum/
            public static IList<IList<int>> Get_ThreeSum(int[] nums)
            {
                //O(N^2)

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
        }

        //1. Two Sum
        class TwoSum
        {
            //https://leetcode.com/problems/two-sum/
            public static int[] Get(int[] nums, int target)
            {
                //O(n), Space O(N)
                //neeed has table
                Dictionary<int, int> dict = new Dictionary<int, int>();

                for (int i = 0; i < nums.Length; i++)
                {
                    int remain = target - nums[i];
                    if (dict.ContainsKey(remain))
                        return new int[2] { i, dict[remain] };

                    if (!dict.ContainsKey(nums[i]))
                        dict.Add(nums[i], i);
                }

                return null;
            }
        }

        //1099. Two Sum Less Than K
        class TwoSumLessThanK
        {
            //https://leetcode.com/problems/two-sum-less-than-k/
            public static int Get(int[] nums, int k)
            {
                //O(NLogN), SPace O(1)
                if (nums.Length < 2)
                    return -1;

                //Must Sort the array to use 2 pointer approach
                Array.Sort(nums);  //O(NLogN)

                int MaxSum = -1;

                //sliding window....
                int l = 0;
                int r = nums.Length - 1;

                while (l < r && r < nums.Length)
                {
                    int sum = nums[l] + nums[r];
                    if (sum < k)
                    {
                        MaxSum = Math.Max(MaxSum, nums[l] + nums[r]);
                        l++;
                    }
                    else
                    {
                        r--;
                    }
                }

                return MaxSum;
            }
        }

        //1539. Kth Missing Positive Number
        class KthMissingPositiveNumber
        {
            //https://leetcode.com/problems/kth-missing-positive-number/
            public static int FindKthPositive(int[] arr, int k)
            {
                int prev = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    while (arr[i] - prev > 1 && k > 0)
                    {
                        prev++;
                        k--;

                        if (k == 0)
                            return prev;
                    }
                    //forwarding prev value, to the item
                    prev = arr[i];
                }

                return arr[arr.Length - 1] + k;
            }
        }

        //3. Longest Substring Without Repeating Characters
        class LongestSubstringWithoutRepeatingCharacters
        {
            //https://leetcode.com/problems/longest-substring-without-repeating-characters/
            public static int LengthOfLongestSubstring(string s)
            {
                Dictionary<char, int> d = new Dictionary<char, int>();

                //Sliding window, when there is no duplicate it grows on right side
                //once duplicate found, shink from left, till duplicates are gone.

                int MaxUniqueCharSubString = 0;

                int l = 0;
                int r = 0;
                //abcabcbb
                while (l <= r && l < s.Length && r < s.Length)
                {
                    //remove duplicate - shring the windows
                    while (d.ContainsKey(s[r]))
                    {
                        char c = s[l];
                        d[c]--;

                        if (d[c] == 0)
                            d.Remove(c);

                        l++;
                    }

                    MaxUniqueCharSubString = Math.Max(MaxUniqueCharSubString, r - l + 1);

                    //expand the window
                    d.Add(s[r], 1);
                    r++;
                }

                return MaxUniqueCharSubString;
            }
        }

        //163. Missing Ranges
        class MissingRanges
        {
            //https://leetcode.com/problems/missing-ranges/
            public static IList<string> FindMissingRanges(int[] nums, int lower, int upper)
            {

                IList<string> result = new List<string>();

                if (nums.Length == 0)
                {
                    if (upper - lower > 1)
                        result.Add(lower + "->" + upper);
                    else
                        result.Add(lower + "");
                }

                //check if number missing on left side of array
                if (nums.Length > 0 && lower != nums[0])
                {
                    if (nums[0] - lower > 1)
                        result.Add(lower + "->" + (nums[0] - 1));
                    else
                        result.Add(lower + "");
                }

                //start from lower 
                for (int i = 1; i < nums.Length; i++)
                {
                    if (nums[i] - nums[i - 1] > 1)
                    {
                        if (nums[i - 1] + 1 == nums[i] - 1)
                            result.Add((nums[i - 1] + 1) + "");
                        else
                            result.Add((nums[i - 1] + 1) + "->" + (nums[i] - 1));
                    }
                }

                //check if number missing on right side of array
                if (nums.Length > 0 && upper - nums[nums.Length - 1] > 0)
                {
                    if (upper - nums[nums.Length - 1] > 1)
                        result.Add((nums[nums.Length - 1] + 1) + "->" + +upper);
                    else
                        result.Add(upper + "");
                }

                return result;
            }
        }

        //228. Summary Ranges
        class SummaryRanges
        {
            //https://leetcode.com/problems/summary-ranges/
            public static IList<string> GetSummaryRanges(int[] nums)
            {
                IList<string> result = new List<string>();

                int startRangeIndex = 0;
                for (int r = 0; r < nums.Length; r++)
                {
                    //if the number difference is more than 1, then its a group change, so collect the range
                    if (r + 1 < nums.Length && Math.Abs(nums[r + 1] - nums[r]) > 1)
                    {
                        if (r - startRangeIndex == 0)
                            result.Add(nums[r] + "");
                        else
                            result.Add(nums[startRangeIndex] + "->" + nums[r]);

                        startRangeIndex = r + 1;
                    }
                }

                if (startRangeIndex < nums.Length)
                {
                    if ((nums.Length - 1) - startRangeIndex == 0)
                        result.Add(nums[nums.Length - 1] + "");
                    else
                    {
                        result.Add(nums[startRangeIndex] + "->" + nums[nums.Length - 1]);
                    }
                }

                return result;
            }
        }

        //985. Sum of Even Numbers After Queries
        class SumOfEvenNumbersAfterQueries
        {
            //https://leetcode.com/problems/sum-of-even-numbers-after-queries/
            public static int[] SumEvenAfterQueries(int[] A, int[][] queries)
            {
                int[] result = new int[A.Length];

                int evenSum = 0;
                for (int i = 0; i < A.Length; i++)
                {
                    if (A[i] % 2 == 0)
                        evenSum += A[i];
                }

                for (int i = 0; i < queries.Length; i++)
                {
                    int val = queries[i][0];
                    int index = queries[i][1];

                    if (A[index] % 2 == 0) //even
                    {
                        if (val % 2 == 0) //even
                        {
                            //the addition will also be even
                            evenSum += val;
                            A[index] += val;
                        }
                        else //odd
                        {
                            //sum turning into odd
                            if (A[index] % 2 == 0) //if the previous item was even, deduct it from evenSum
                                evenSum -= A[index];
                            A[index] += val;
                        }
                    }
                    else //odd
                    {
                        if (val % 2 == 0) //even
                        {
                            ////sum turning into odd
                            if (A[index] % 2 == 0) //if the previous item was even, deduct it from evenSum
                                evenSum -= A[index];
                            A[index] += val;
                        }
                        else
                        {
                            //even
                            A[index] += val;
                            evenSum += A[index];
                        }
                    }

                    result[i] = evenSum;
                }

                return result;
            }
        }

        //566. Reshape the Matrix
        class ReshapetheMatrix
        {
            //https://leetcode.com/problems/reshape-the-matrix/
            public static int[][] MatrixReshape(int[][] nums, int r, int c)
            {
                if (nums.Length == 0)
                    return nums;

                if (r * c != nums.Length * nums[0].Length)
                    return nums;

                int[][] result = new int[r][];
                for (int i = 0; i < r; i++)
                {
                    result[i] = new int[c];
                }
                //1,2,  1,2,3
                //3,4   4,5,6
                //5,6

                int dest_r = 0;
                int dest_c = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    for (int j = 0; j < nums[0].Length; j++)
                    {
                        result[dest_r][dest_c] = nums[i][j];

                        dest_c++;
                        if (dest_c == result[0].Length)
                        {
                            dest_c = 0;
                            dest_r++;
                        }
                    }
                }

                return result;
            }
        }

        //26. Remove Duplicates from Sorted Array
        class RemoveDuplicatesfromSortedArray
        {
            //https://leetcode.com/problems/remove-duplicates-from-sorted-array/submissions/
            public static int RemoveDuplicates(int[] nums)
            {
                if (nums.Length == 0)
                    return 0;

                int insertPosition = 1;
                int lastValue = nums[0];

                for (int i = 1; i < nums.Length; i++)
                {
                    if (nums[i] != lastValue)
                    {
                        //bring nums[i] into 'insertPosition' position
                        nums[insertPosition] = nums[i];

                        lastValue = nums[i];
                        insertPosition++;
                    }
                }
                return insertPosition;

            }
        }

        //80. Remove Duplicates from Sorted Array II
        class RemoveDuplicatesfromSortedArrayII
        {
            public static int RemoveDuplicates(int[] nums)
            {
                //0,0,1,1,1,1,2,3,3
                //0,0,1,1,2,3,3
                if (nums.Length == 0)
                    return 0;

                int insertPosition = 1;
                int lastValueIndex = 0;

                for (int i = 1; i < nums.Length; i++)
                {
                    if (nums[i] == nums[lastValueIndex])
                    {
                        //bring nums[i] into 'insertPosition' position
                        nums[insertPosition] = nums[i];

                        if (i - lastValueIndex < 2)
                            insertPosition++;
                    }
                    else
                    {
                        //bring nums[i] into 'insertPosition' position
                        nums[insertPosition] = nums[i];

                        lastValueIndex = i;

                        insertPosition++;
                    }
                }
                return insertPosition;
            }
        }

        //4. Median of Two Sorted Arrays
        class MedianOfTwoSortedArrays
        {
            //https://leetcode.com/problems/median-of-two-sorted-arrays/
            public double FindMedianSortedArrays(int[] nums1, int[] nums2)
            {
                //https://www.youtube.com/watch?v=LPFhl65R7ww

                //idea is to split the arrays such a way where every element in left is less than right side

                //Time (O(Log(Min(m,n))))
                int len1 = nums1.Length;
                int len2 = nums2.Length;

                //make sure that forst array is smaller
                if (len1 > len2)
                    return FindMedianSortedArrays(nums2, nums1);

                int lo = 0;
                int hi = len1;

                while (lo <= hi)
                {
                    //split 2 arrays, such that have have eual numbers on both sides
                    int firstArrSplitIndex = (lo + hi) / 2;
                    int secondArrSplitIndex = (len1 + len2 + 1) / 2 - firstArrSplitIndex;

                    //collect left and right around splits
                    int F1ArrLeft = (firstArrSplitIndex == 0) ? Int32.MinValue : nums1[firstArrSplitIndex - 1];
                    int F1ArrRight = (firstArrSplitIndex == len1) ? Int32.MaxValue : nums1[firstArrSplitIndex];

                    int F2ArrLeft = (secondArrSplitIndex == 0) ? Int32.MinValue : nums2[secondArrSplitIndex - 1];
                    int F2ArrRight = (secondArrSplitIndex == len2) ? Int32.MaxValue : nums2[secondArrSplitIndex];

                    //split where every element in left is less than right side
                    if (F1ArrLeft <= F2ArrRight && F2ArrLeft <= F1ArrRight)
                    {
                        //We have partitioned the array at correct place
                        //now get max of left elements and min of right elements to get the median in case of even length combined array size
                        //or get max of left for odd length combined array size
                        if ((len1 + len2) % 2 == 0)
                        {
                            return (double)(Math.Max(F1ArrLeft, F2ArrLeft) + Math.Min(F1ArrRight, F2ArrRight)) / 2;
                        }
                        else
                        {
                            return (double)Math.Max(F1ArrLeft, F2ArrLeft);
                        }
                    }
                    else if (F1ArrRight < F2ArrLeft)
                    {
                        lo = firstArrSplitIndex + 1;
                    }
                    else
                    {
                        hi = firstArrSplitIndex - 1;
                    }
                }

                // we can come here only if the arrays are not sorted 
                // throw a valid exception
                throw new Exception();
            }
        }

        class KthLargestElementofTwoSortedArrays
        {
            //https://leetcode.com/discuss/interview-question/351782/google-phone-screen-kth-largest-element-of-two-sorted-arrays
            public int FindMedianSortedArrays(int[] nums1, int[] nums2, int k)
            {
                return 0;
            }
        }

        //295. Find Median from Data Stream
        class FindMedianfromDataStream
        {
            Heap<int> leftMaxHeap = null;
            Heap<int> RightMinHeap = null;
            //https://leetcode.com/problems/find-median-from-data-stream/
            /** initialize your data structure here. */
            public FindMedianfromDataStream()
            {
                //The idea is to split the numbers into 2 halfs
                //left half will maintain numbers in Asc order, (Max heap)
                //right side will maintain numbers in Desc order, (MinHeap)

                /*
                 --------------------
                |3|4|5|6|   |8|10|12|
                ---------------------
                 MaxHeap     MinHeap
                */

                leftMaxHeap = new Heap<int>(100, false);
                RightMinHeap = new Heap<int>(100, true);
            }

            public void AddNum(int num)
            {
                //add to max heap
                leftMaxHeap.Push(new HeapNode<int>(num));

                //move it to min heap
                RightMinHeap.Push(leftMaxHeap.Top());
                leftMaxHeap.Pop();

                //if count(minheap) is grear than max heap
                //move the Min value to Max heap
                if (RightMinHeap.Count > leftMaxHeap.Count)
                {
                    leftMaxHeap.Push(RightMinHeap.Top());
                    RightMinHeap.Pop();
                }
            }

            public double FindMedian()
            {
                //if max heap has one more extra value, then return it, 
                if (leftMaxHeap.Count > RightMinHeap.Count)
                {
                    return leftMaxHeap.Top().val;
                }
                else
                {
                    return (RightMinHeap.Top().val + leftMaxHeap.Top().val) * 0.5;
                }
            }
        }

        //480. Sliding Window Median
        class SlidingWindowMedian
        {
            //https://leetcode.com/problems/sliding-window-median/
            public static double[] MedianSlidingWindow(int[] nums, int k)
            {
                //The idea is to split the numbers into 2 halfs
                //left half will maintain numbers in Asc order, (Max heap - SortedSet)
                //right side will maintain numbers in Desc order, (MinHeap - SortedSet)

                /*
                 --------------------
                |3|4|5|6|   |8|10|12|
                ---------------------
                 MaxHeap     MinHeap
                 */

                //https://www.c-sharpcorner.com/UploadFile/0f68f2/comparative-analysis-of-list-hashset-and-sortedset/
                //c# SortedSet - Add/delete operation is O(LogN) - refer to above link

                //O(NLogK), Space(k)

                IList<double> result = new List<double>();

                SortedSet<Pair> minSet = new SortedSet<Pair>();
                SortedSet<Pair> maxSet = new SortedSet<Pair>(new PairDescOrder());

                // To hold the pairs, we will keep renewing 
                // these instead of creating the new pairs 
                Pair[] windowPairs = new Pair[k];

                for (int i = 0; i < k; i++)
                {
                    windowPairs[i] = new Pair(nums[i], i);
                }

                // Add k items 
                for (int i = 0; i < k; i++)
                {
                    maxSet.Add(windowPairs[i]);

                    minSet.Add(maxSet.First());
                    maxSet.Remove(maxSet.First());

                    if (minSet.Count > maxSet.Count)
                    {
                        maxSet.Add(minSet.First());
                        minSet.Remove(minSet.First());
                    }
                }

                printMedian(minSet, maxSet, k, result);

                for (int i = k; i < nums.Length; i++)
                {
                    // Get the item going out of window 
                    Pair temp = windowPairs[i % k];

                    //find which set the older value belong
                    if (temp.value > maxSet.First().value)
                    {
                        //temp was stored on min set, remove it from min set
                        minSet.Remove(temp);
                    }
                    else
                    {
                        //temp was stored on max set, remove it from max set
                        maxSet.Remove(temp);
                    }

                    // Renew window start to new window end 
                    temp.Renew(nums[i], i);

                    maxSet.Add(temp);
                    minSet.Add(maxSet.First());
                    maxSet.Remove(maxSet.First());
                    if (minSet.Count > maxSet.Count)
                    {
                        maxSet.Add(minSet.First());
                        minSet.Remove(minSet.First());
                    }

                    printMedian(minSet, maxSet, k, result);
                }

                return result.ToArray();
            }

            static void printMedian(SortedSet<Pair> minSet, SortedSet<Pair> maxSet, int window, IList<double> result)
            {
                // If the window size is even then the 
                // median will be the average of the 
                // two middle elements 
                if (window % 2 == 0)
                {
                    result.Add(((double)minSet.First().value + (double)maxSet.First().value) / 2.0);
                }
                // Else it will be the middle element 
                else
                {
                    result.Add(maxSet.First().value);
                }
            }

            class Pair : IComparable<Pair>
            {
                public int value = 0, index = 0;

                // Constructor 
                public Pair(int v, int p)
                {
                    value = v;
                    index = p;
                }

                public int CompareTo(Pair o)
                {
                    // Two nodes are equal only when 
                    // their indices are same 
                    if (index == o.index)
                    {
                        return 0;
                    }
                    else if (value == o.value)
                    {
                        return index.CompareTo(o.index);
                    }
                    else
                    {
                        return value.CompareTo(o.value);
                    }
                }

                // Update the value and the position 
                // for the same object to save space 
                public void Renew(int v, int p)
                {
                    value = v;
                    index = p;
                }

                public override string ToString()
                {
                    return string.Format("{0:0.0}, {1:0.0}", value, index);
                }
            }

            class PairDescOrder : IComparer<Pair>
            {
                public int Compare(Pair x, Pair y)
                {
                    return y.CompareTo(x);
                }
            }
        }

        //84. Largest Rectangle in Histogram
        class LargestRectangleinHistogram
        {
            //https://leetcode.com/problems/largest-rectangle-in-histogram/
            public static int LargestRectangleArea(int[] heights)
            {
                //Array and DP

                //generally, its N^3 algo... at every index calculate the Max area going thorugh all heights 
                if (heights.Length == 0)
                    return 0;

                /*      _
                     _ |  |            __
                    |  |  |_        _ |  |
                    |  |  |  |_    |  |  |__
                    |_ |_ |_ |_ |_ |_ | _|__|
                    |4 |5 |3 |2 |1 |3 | 4| 2|
                    -------------------------
            left    -1 |0 |-1|-1|-1|4 | 5| 4|
            right    2 |2 |3 | 4| 8|7 | 7| 8|
                */

                //O(N)

                //To reduce the runtime, at every index i we find the first smaller height on left and right side an store their (height) indicies.

                int[] firstSmallHeightOnLeftSide = new int[heights.Length];
                int[] firstSmallHeightOnRightSide = new int[heights.Length];

                //Base case
                //no further smaller height on left, so the index = -1
                firstSmallHeightOnLeftSide[0] = -1;
                //no smaller height on right, so the index = heights.Length
                firstSmallHeightOnRightSide[heights.Length - 1] = heights.Length;

                //Get first less height element (left side) at i and store on firstSmallHeightOnLeftSide 
                for (int i = 1; i < heights.Length; i++)
                {
                    int minIndex = i - 1;
                    while (minIndex >= 0 && heights[minIndex] >= heights[i])
                        minIndex = firstSmallHeightOnLeftSide[minIndex];

                    firstSmallHeightOnLeftSide[i] = minIndex;
                }

                //Get first less height element (right side) at i and store on firstSmallHeightOnLeftSide 
                for (int i = heights.Length - 2; i >= 0; i--)
                {
                    int minIndex = i + 1;
                    while (minIndex < heights.Length && heights[minIndex] >= heights[i])
                        minIndex = firstSmallHeightOnRightSide[minIndex];

                    firstSmallHeightOnRightSide[i] = minIndex;
                }

                //At every index, calculate the area, (first right side smaller height - first left side smaller height) * height(i)
                int maxRectArea = 0;
                for (int i = 0; i < heights.Length; i++)
                {
                    maxRectArea = Math.Max(maxRectArea, (firstSmallHeightOnRightSide[i] - firstSmallHeightOnLeftSide[i] - 1) * heights[i]);
                }

                return maxRectArea;
            }
        }

        //523. Continuous Subarray Sum
        class ContinuousSubarraySumMultiplesOfK
        {
            //https://leetcode.com/problems/continuous-subarray-sum/
            public static bool CheckSubarraySum(int[] nums, int k)
            {
                //Given a list of non - negative numbers and a target integer k, write a function to check if the array 
                //has a continuous subarray of size at least 2 that sums up to a multiple of k, that is, sums up to n*k where n is also an integer.

                Dictionary<int, int> seen = new Dictionary<int, int>();
                //Base case
                seen.Add(0, -1); //-1 to avoid 

                int cur = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    cur += nums[i];

                    if (k != 0)
                        cur %= k;

                    if (seen.ContainsKey(cur))
                    {
                        if (i - seen[cur] > 1) //atleast size of subarray is 2 
                            return true;
                    }
                    else
                        seen.Add(cur, i);
                }

                return false;
            }
        }

        //560. Subarray Sum Equals K
        class CountSubarraySumEqualsK
        {
            //https://leetcode.com/problems/subarray-sum-equals-k/submissions/
            public static int CountSubarraySum(int[] nums, int k)
            {
                //O(N), Space O(N)
                /*
                Cumulative sum property
                ==========================================
                0-------|i|-------------|j|

                sum(i,j)= sum(0,j) - sum(0,i), where sum(i,j) represents the sum of all the elements from index i to j-1
                => k = sum(0,j) - sum(0,i)
                => sum(0,i) - k = sum(0,i)
                */

                //**********************************************
                //if you need list of subarrays, then store the Indicies in dictionary
                //Else you can put count of indices in a integer.. instead of list         

                Dictionary<int, IList<int>> dp = new Dictionary<int, IList<int>>();
                //Dictionary<int, int> dp1 = new Dictionary<int, int>();
                int cumSum = 0;
                int count = 0;
                //int count1 = 0;

                //Initially Add Sum 0 with 1 occurance
                dp.Add(0, new List<int>() { -1 }); //to handle the case when sub-array with sum S starts from index 0
                //dp1.Add(0, 1);

                
                for (int i = 0; i < nums.Length; i++)
                {
                    cumSum += nums[i];

                    if (dp.ContainsKey(cumSum - k))
                    {
                        IList<int> lstInx = dp[cumSum - k];
                        for (int j = 0; j < lstInx.Count; j++)
                        {
                            count++;
                            Console.WriteLine("[" + (lstInx[j] + 1) + ", " + i + "]" + " - Length : " + (i - lstInx[j]));    
                        }
                    }

                    /*if (dp1.ContainsKey(cumSum - k))
                    {
                        count1 += dp1[cumSum - k];  
                    }*/

                    if (!dp.ContainsKey(cumSum))
                        dp.Add(cumSum, new List<int>());

                    dp[cumSum].Add(i);

                    /*
                    if (!dp1.ContainsKey(cumSum))
                        dp1.Add(cumSum, 0);            
                    dp1[cumSum] ++;
                    */
                }

                return count;
            }
        }

        //974. Subarray Sums Divisible by K
        class SubarraysSumDivByK
        {
            //https://leetcode.com/problems/subarray-sums-divisible-by-k/
            public static int SubarraysDivByK(int[] A, int K)
            {
                /*
                we find subarray sum from from i to j by 
                SUM[i-1] - SUM[j]

                if it has to be div by K then 
                SUM[i-1] - SUM[j] = K*something

                => (SUM[i-1] - SUM[j]) %K  = (K*something) %K = 0
                => SUM[i-1] % k == SUM[j]%k

                So basically for any SUM[i] if its remainder is same as remainder of any other SUM[ some index] then their sum % k == 0
                */

                Dictionary<int, int> dp = new Dictionary<int, int>();

                //setting count for only 0th element to be 1 
                //because we have sub array = [] which has sum of 0

                dp.Add(0, 1);

                int cumSum = 0, count = 0;
                for (int i = 0; i < A.Length; i++)
                {
                    cumSum += A[i];

                    //plus K beacuse there are negative nums too
                    int rem = ((cumSum % K) + K) % K;

                    if (dp.ContainsKey(rem))
                        count += dp[rem];

                    if (!dp.ContainsKey(rem))
                        dp.Add(rem, 0);

                    dp[rem]++;
                }
                return count;
            }
        }

        //1590. Make Sum Divisible by P
        class MakeSumDivisiblebyP
        {
            //https://leetcode.com/problems/make-sum-divisible-by-p/
            public static int MinSubarray(int[] nums, int p)
            {
                /*
                Let pre[] be the prefix sum array,
                then pre[i] is running prefix sum or prefix sum of i elements,
                pre[j] is the prefix sum such that pre[i]-pre[j] is the subarray we need to remove to make pre[n] (sum of all elements) divisible by p

                (pre[n] - (pre[i]-pre[j])) % p = 0 ... (remove a subarray to make pre[n] divisible by p)
                => pre[n] % p = (pre[i]-pre[j]) % p ... ((a-b)%m = a%m - b%m)
                => pre[j]%p = pre[i]%p - pre[n]%p ... (same property used above)
                since RHS can be negative we make it positive modulus by adding p and taking modulus
                => pre[j]%p = (pre[i]%p - pre[n]%p + p) % p
                */

                //****************************************
                //Store The cum Sum % p on DP array ******
                //****************************************

                long cSum = 0;

                //Minimum subarry to remove cannt be bigger than length of array
                int min = nums.Length;

                ////**************************************************************************************************
                //If we dont store cumSum as (cumSum % p), then there would be integer overflow.. so I used 'long' type variable.
                //But both of these approach give same result...
                //for (int i = 0; i < nums.Length; i++)
                //    cSum += nums[i];
                //**************************************************************************************************
                for (int i = 0; i < nums.Length; i++)
                    cSum = (cSum + nums[i]) % p;

                //long modToReduce = cSum % p;
                long modToReduce = cSum;

                //No modToReduce, then exit
                if (modToReduce == 0)
                    return 0;

                Dictionary<int, int> dp = new Dictionary<int, int>();

                //setting count for only 0th element to be -1 
                //because we have sub array = [] which has sum of 0
                dp.Add(0, -1);

                cSum = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    cSum += nums[i];
                    //cSum = (cSum + nums[i]) % p;

                    //per above formula
                    //plus P beacuse there are negative nums too
                    int prev = (int)(((cSum % p) - modToReduce) + p) % p;
                    //int prev = (int)((cSum - modToReduce) + p) % p;

                    if (dp.ContainsKey(prev))
                        min = Math.Min(min, i - dp[prev]);

                    //Store cum sum % p
                    dp[(int)(cSum % p)] = i;
                    //dp[(int)cSum] = i;
                }

                //Minimum subarry to remove cannt be bigger than length of array
                return min >= nums.Length ? -1 : min;
            }
        }

        class NearestNeighbours
        {
            /*
                Given a point p, and other n points in two-dimensional space, find k points out of n points which are nearest to p.
                NOTE: Distance between two points is measured by the standard Euclidean method.

                Input Format:
                There are 4 arguments in input, an integer p_x, which is the x coordinate of point p, integer p_y, 
                which is the y coordinate of point p, an integer k and a 2D integer array of points n_points.
            */
            public static List<List<int>> nearest_neighbours(int p_x, int p_y, int k, List<List<int>> n_points)
            {
                //Euclidean distance between two points in Euclidean space is the length of a line segment between the two points
                //Euclidean Distance - Sqrt((x2-x1)^2 + (y2-y1)^2)

                //O(NLogN), space O(K)
                n_points.Sort(new EuclideanDistanceComparer(p_x, p_y));

                List<List<int>> result = new List<List<int>>();
                for (int i = 0; i < k; i++)
                {
                    result.Add(new List<int>(n_points[i]));
                }
                return result;
            }

            class EuclideanDistanceComparer : IComparer<List<int>>
            {
                int px = 0, py = 0;
                public EuclideanDistanceComparer(int p_x, int p_y)
                {
                    px = p_x;
                    py = p_y;
                }
                public int Compare(List<int> x, List<int> y)
                {
                    //compute Euclidean Distance for x and y corrdinates separately - using Sqrt((x2-x1)^2 + (y2-y1)^2)
                    double eqDistance1 = Math.Sqrt(Math.Pow(Math.Abs(x[0] - px), 2) + Math.Pow(Math.Abs(x[1] - py), 2));
                    double eqDistance2 = Math.Sqrt(Math.Pow(Math.Abs(y[0] - px), 2) + Math.Pow(Math.Abs(y[1] - py), 2));

                    //compare the distance to sort them ASC order
                    return eqDistance1.CompareTo(eqDistance2);
                }
            }
        }

        class SortAllCharacters
        {
            public static List<char> sort_array(List<char> arr)
            {
                //Quick Sort - O(NLogN), Space(1)
                //Count Sort - O(n), space O(n)....

                int[] frequency = new int[128]; /*an array to store the number of 
                                        occurence of each character in the string*/
                foreach (char c in arr)
                {
                    frequency[c]++;
                }
                arr.Clear();
                for (int i = 0; i < 128; i++)
                {
                    /*traversing from charcter having lowest ascii value to that of highest ascii value*/
                    for (int j = 0; j < frequency[i]; j++)
                    {
                        /*appending the result with the 
                        number of occurences of character in the string*/
                        arr.Add((char)i);
                    }
                }
                return arr;
            }
        }

        //266. Palindrome Permutation
        class PalindromePermutation
        {
            //https://leetcode.com/problems/palindrome-permutation/
            public bool CanPermutePalindrome(string s)
            {
                Dictionary<char, int> dict = new Dictionary<char, int>();
                foreach (char c in s)
                {
                    if (!dict.ContainsKey(c))
                        dict.Add(c, 0);

                    dict[c]++;
                }

                int OddOccuranceCount = 0;
                foreach (char c in dict.Keys)
                {
                    if (dict[c] % 2 == 1) //odd occurances
                        OddOccuranceCount++;
                }

                if (s.Length % 2 != 0) //odd length
                {
                    //max of odd occurance char could be 1
                    return (OddOccuranceCount == 1) ? true : false;
                }
                else
                { //even length

                    //max of odd occurance char could be 0
                    return (OddOccuranceCount == 0) ? true : false;
                }
            }
        }

        //1640. Check Array Formation Through Concatenation
        class CheckArrayFormationThroughConcatenation
        {
            //https://leetcode.com/problems/check-array-formation-through-concatenation/
            public bool CanFormArray(int[] arr, int[][] pieces)
            {
                //***********************
                //Note that the distinct part means that every position in the array belongs to only one piece
                //***********************

                Dictionary<int, int> dict = new Dictionary<int, int>();
                for (int i = 0; i < arr.Length; i++)
                {
                    dict.Add(arr[i], i);
                }

                for (int r = 0; r < pieces.Length; r++)
                {
                    int lastItemMatch = -1;
                    for (int c = 0; c < pieces[r].Length; c++)
                    {
                        if (!dict.ContainsKey(pieces[r][c]))
                            return false;

                        if (lastItemMatch > -1)
                        {
                            if (dict[pieces[r][c]] != lastItemMatch + 1)
                                return false;
                        }
                        lastItemMatch = dict[pieces[r][c]];

                        dict.Remove(pieces[r][c]);
                    }
                }

                return (dict.Count == 0);
            }
        }

        //268. Missing Number
        class MissingNumber
        {
            //https://leetcode.com/problems/missing-number/
            public static int Find(int[] nums)
            {
                //O(N), Space O(1)

                //(n * (n + 1)) /2
                int typicalSumOfAllNumbersTillN = (nums.Length * (nums.Length + 1)) / 2;
                int sum = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    sum += nums[i];
                }

                if (sum == typicalSumOfAllNumbersTillN)
                    return 0;
                else
                    return typicalSumOfAllNumbersTillN - sum;
            }
        }

        //41. First Missing Positive
        class FirstSmallestMissingPositive
        {
            //https://leetcode.com/problems/first-missing-positive/
            public static int Find(int[] nums)
            {
                //O(N), Space O(1)
                //*******************************************************
                //WE will do inplace sort of +ve numbers ****************
                //*******************************************************
                //Tricky - Put positive numbers into array at index [(i) + 1]. < 
                //ignore +ve numbers, thats are beyond the array size
                for (int i = 0; i < nums.Length; i++)
                {
                    //we are interested into +ve numbers
                    if (nums[i] < 1)
                        continue;

                    //We will ignore items whose value is greater than array
                    if (nums[i] > nums.Length)
                        continue;

                    //if the values are not placed correctly.. place them to right index
                    if (nums[i] != i + 1)
                    {
                        //if 2 items to be replaved are same, ignore them
                        if (nums[i] == nums[nums[i] - 1])
                            continue;

                        int temp = nums[nums[i] - 1];
                        nums[nums[i] - 1] = nums[i];
                        nums[i] = temp;

                        //We must recheck the replaced value at i, as it might be some thing greater than (i + 1)
                        i--;
                    }
                }

                for (int i = 0; i < nums.Length; i++)
                {
                    //Whenever you find the first get the wrongly placed +ve number, thats our number
                    if (nums[i] != i + 1)
                        return (i + 1);
                }
                //if all +ve numbers are placed correctly, the our answer would be arr length +1
                return nums.Length == 0 ? 1 : nums.Length + 1;



                //O(N), Space O(N)
                HashSet<int> hs = new HashSet<int>();

                //Keep track of max + ve number
                int positiveMax = int.MinValue;

                for (int i = 0; i < nums.Length; i++)
                {
                    //we are interested in only +ve numbers
                    if (nums[i] < 1)
                        continue;

                    //we only need max of +ve numbers
                    positiveMax = Math.Max(positiveMax, nums[i]);
                    if (!hs.Contains(nums[i]))
                        hs.Add(nums[i]);
                }

                //start from smallest +ve number, find the first +ve number is missing
                for (int i = 1; i < positiveMax; i++)
                {
                    if (!hs.Contains(i))
                        return i;
                }
                //if arry is empty, return 1, else return the next max+ ve numer
                return (positiveMax == int.MinValue) ? 1 : positiveMax + 1;
            }
        }

        //76. Minimum Window Substring
        class MinimumWindowSubstring
        {
            public static string MinWindow(string s, string t)
            {
                /************************
                 * Clarifying questions: 
                 *  1) is 't' contains duplicate??? if not, only 1 hashmap will is sufficient to track window's character validity 
                 *  2) if checking presence of character in window, then Dictionary is required...
                 ************************/

                //Sliding window - start at index 0
                //keep growing till we find all characters, take text size
                //once found, shrink window from left side.... if imbalanced, then grow
                //repeat the process till we complete all characters

                Dictionary<char, int> tMap = new Dictionary<char, int>();
                foreach (char c in t)
                {
                    if (!tMap.ContainsKey(c))
                        tMap.Add(c, 0);
                    tMap[c]++;
                }

                Dictionary<char, int> WindowCharsInTMap = new Dictionary<char, int>();

                //default answer is whole 's'
                int resultLen = s.Length;
                string result = "";
                int l = 0, r = 0;
                while (l <= r)   //for (l = 0; l < s.Length - t.Length; l++)
                {
                    //keep expanding window, till we get all characters
                    //check we window contains all items in 't',  
                    while (!IsWinValid(tMap, WindowCharsInTMap) && r < s.Length)
                    {
                        //We track characters, which are there tMap, 
                        if (tMap.ContainsKey(s[r]))
                        {
                            if (!WindowCharsInTMap.ContainsKey(s[r]))
                                WindowCharsInTMap.Add(s[r], 0);
                            WindowCharsInTMap[s[r]]++;
                        }
                        r++;
                    }

                    //here we MUST find all letters... it dont find all letters, we quit
                    if (!IsWinValid(tMap, WindowCharsInTMap))
                        break;

                    if (resultLen >= r - l)
                    {
                        result = s.Substring(l, r - l);
                        resultLen = r - l;
                    }

                    //here all found - shrink from left
                    if (WindowCharsInTMap.ContainsKey(s[l]))
                        WindowCharsInTMap[s[l]]--;

                    l++;
                }

                return result;
            }

            private static bool IsWinValid(Dictionary<char, int> tMap, Dictionary<char, int> WindowCharsInTMap)
            {
                foreach (char c in tMap.Keys)
                {
                    //WindowCharsInTMap can have more occurance of 'c' than tMap, so <=
                    if (!(WindowCharsInTMap.ContainsKey(c) && tMap[c] <= WindowCharsInTMap[c]))
                        return false;
                }

                return true;
            }
        }

        //407 Trapping Rain Water II
        class RainWaterII
        {
            internal class Cell : IComparable<Cell>
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
                    //if (this.height == other.height) return 0;
                    //if (this.height < other.height) return -1;
                    //return 1;
                    return this.height.CompareTo(other.height);
                }
            }

            public static int TrapRainWater(int[][] heightMap)
            {
                int m = heightMap.Length;
                int n = heightMap[0].Length;

                if (m < 3 && n < 3)
                    return 0;

                int[][] directions = new int[4][] {
                    new int[] { 0, 1 },
                    new int[] { 0, -1 },
                    new int[] { 1, 0 },
                    new int[] { -1, 0 }
                };


                bool[,] visited = new bool[m, n];

                Heap<Cell> minHeap = new Heap<Cell>(m * n, true);
                for (int r = 0; r < m; r++)
                {
                    visited[r, 0] = true;
                    visited[r, n - 1] = true;
                    minHeap.Push(new HeapNode<Cell>(new Cell(r, 0, heightMap[r][0])));
                    minHeap.Push(new HeapNode<Cell>(new Cell(r, n - 1, heightMap[r][n - 1])));
                }
                for (int c = 1; c < n - 1; c++)
                {
                    visited[0, c] = true;
                    visited[m - 1, c] = true;
                    minHeap.Push(new HeapNode<Cell>(new Cell(0, c, heightMap[0][c])));
                    minHeap.Push(new HeapNode<Cell>(new Cell(m - 1, c, heightMap[m - 1][c])));
                }

                int res = 0;
                while (minHeap.Count > 0)
                {
                    Cell temp = minHeap.Pop().val;
                    int row = temp.row, col = temp.col, height = temp.height;
                    foreach (int[] dir in directions)
                    {
                        int r = row + dir[0];
                        int c = col + dir[1];

                        if (r > 0 && r < m - 1 && c > 0 && c < n - 1 && !visited[r, c])
                        {
                            visited[r, c] = true;

                            res += Math.Max(0, height - heightMap[r][c]);

                            minHeap.Push(new HeapNode<Cell>(new Cell(r, c, Math.Max(height, heightMap[r][c]))));
                        }
                    }
                }

                return res;
            }
        }


        //Given a list of 4 billion integers, find an integer not in the list using 4MB of memory.
        class FindAnIntegerNotInList
        {
            //https://prismoskills.appspot.com/lessons/Programming_Puzzles/Missing_number_in_billion_integers_.jsp
            //https://uplevel.interviewkickstart.com/resource/editorial/rc-codingproblem-4917-63763-5-91
            public static int Get(uint[] arr)
            {
                // We split the whole range of possible input values [0..2^32) into
                // 2^16 non-overlapping subranges, each subrange is 2^16-long:
                // 0-th subrange: [0 .. 2^16),
                // 1-st subrange: [2^16 .. 2^17),
                // ...,
                // the last, 2^16-1-th subrange: [2^32-2^16 .. 2^32).
                int TWO_POWER_SIXTEEN = (int)(Math.Pow(2, 16));

                // This array will store how many input array elements actually fall
                // into each bucket. Its size is 2^16 * 8 bytes = 2^19 bytes = 1/2 MiB.
                long[] numbersInBucket = new long[TWO_POWER_SIXTEEN];
                foreach (long inputValue in arr)
                {
                    // Dividing the number by 2^16 determines which bucket/subrange it falls into.
                    int bucket = (int)(inputValue >> 16);
                    numbersInBucket[bucket]++;
                }
                // Now let's find a bucket/subrange that has at least one number missing
                // from the input AND then find a missing number in that subrange.
                for (int bucket = 0; bucket < TWO_POWER_SIXTEEN; bucket++)
                {
                    if (numbersInBucket[bucket] >= TWO_POWER_SIXTEEN)
                    {
                        // This bucket _might_ have its entire subrange covered by input values.
                        // Let's skip it. There certainly exists a bucket will fewer input values.
                        continue;
                    }
                    // We found a bucket with fewer than 2^16 input values fallen into it.
                    // This guarantees that at least one value from this bucket's subrange
                    // isn't present in the input.
                    // Such value is a correct answer, let's find and return it.

                    // We will read all the input numbers one more time. This time we will
                    // only care about values from this particular bucket/subrange.
                    // We will use a simple boolean array to store which values
                    // from the current bucket subrange present/absent in the input.
                    bool[] presentInBucket = new bool[TWO_POWER_SIXTEEN]; // Initialized by false by JVM.
                    foreach (long inputValue in arr)
                    {
                        int bucketThisValueFallsInto = (int)(inputValue >> 16);
                        if (bucketThisValueFallsInto == bucket)
                        {
                            int indexWithinCurrentSubrange = (int)(inputValue % TWO_POWER_SIXTEEN);
                            presentInBucket[indexWithinCurrentSubrange] = true;
                        }
                    }
                    // Let us find a falsy value in presentInBucket array. It corresponds to a value
                    // that's missing in the input. Such value is a correct answer and we can return it.
                    for (int i = 0; i < TWO_POWER_SIXTEEN; i++)
                    {
                        if (!presentInBucket[i])
                        {
                            int startOfSubrange = bucket << 16;
                            return startOfSubrange + i;
                        }
                    }
                    throw new Exception("We knew that the subrange of bucket " + bucket +
                            " isn't completely covered by input numbers but we didn't find" +
                            " any value missing in input.");
                }
                throw new Exception("We knew that at least one bucket/subrange" +
                " is bound to have fewer than 2^16 input values in it" +
                " (because four billion is less than 2^16 * 2^16) but didn't find any");
            }
        }

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
                            ending_index = j - 1;
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

        //134	Gas Station
        class Gas
        {
            class StationGasCostPair : IComparable<StationGasCostPair>
            {
                public int Gas;
                public int Cost;
                public int Index;
                public StationGasCostPair(int i, int g, int c)
                {
                    Index = i;
                    Gas = g;
                    Cost = c;
                }

                public int CompareTo(StationGasCostPair other)
                {
                    if (this.Gas == other.Gas)
                    {
                        return other.Cost.CompareTo(this.Cost);
                    }
                    else
                    {
                        int thisDif = this.Gas - this.Cost;
                        int otherDif = other.Gas - other.Cost;
                        return otherDif.CompareTo(thisDif);
                    }
                }
            }

            public static int CanCompleteCircuit(int[] gas, int[] cost)
            {
                StationGasCostPair[] gcpairs = new StationGasCostPair[gas.Length];
                for (int i = 0; i < gas.Length; i++)
                    gcpairs[i] = new StationGasCostPair(i, gas[i], cost[i]);
                Array.Sort(gcpairs);

                //start from gcpairs[0]
                //gcpairs[0].Index
                // 3 4 5 0 1 2
                // 0 1 2 3 4 5
                //------------
                // 5 % 6 = 5

                for (int i = 0; i < gcpairs.Length; i++)
                {
                    int fuelSum = 0;
                    int nextIndex = gcpairs[i].Index;
                    int prevIndex = (gcpairs[i].Index + (gas.Length - 1)) % gas.Length;
                    while (cost[nextIndex] <= (gas[nextIndex] + fuelSum))//  true)
                    {
                        fuelSum += gas[nextIndex];

                        //if accumulated fuel is less than cost..we cant move forward
                        if (fuelSum < cost[nextIndex])
                            break;

                        if (nextIndex == prevIndex)
                            return gcpairs[i].Index;

                        //reduce the trip cost
                        fuelSum -= cost[nextIndex];
                        nextIndex = (nextIndex + 1) % gas.Length;
                    }
                }

                return -1;
            }
        }

        //153. Find Minimum in Rotated Sorted Array
        class FindMinimuminRotatedSortedArray
        {
            public static int FindMin(int[] nums)
            {
                int l = 0;
                int r = nums.Length - 1;
                while (l <= r)
                {
                    int m = l + (r - l) / 2;

                    //1 2 [3] 4 5 6 7
                    //7 1 [2] 3 4 5 6
                    //6 7 [1] 2 3 4 5 *** smallest might on the middle... so we put r = m
                    //5 6 [7] 1 2 3 4
                    //4 5 [6] 7 1 2 3
                    //3 4 [5] 6 7 1 2
                    //2 3 [4] 5 6 7 1

                    //3 [3] 1 3  //middle and right are same, try reduce so the same number gets removed from right
                    //2,2,2,0,1

                    if (nums[m] < nums[r]) //mid < end (right side is sorted) then, lowest element is on left side. smallest might on the middle... so we put r = m
                        r = m;
                    else if (nums[m] > nums[r]) //mid > end (left side is sorted) then, lowest element is on right side
                        l = m + 1;
                    else //both value are same, try reduce so the same number gets removed from right
                        r--;
                }
                return nums[l];
            }
        }

        //1673. Find the Most Competitive Subsequence
        public class FindtheMostCompetitiveSubsequence
        {
            public static int[] MostCompetitive(int[] nums, int k)
            {
                //find subset of size k
                IList<IList<int>> result = new List<IList<int>>();
                DFS(nums, 0, k, result, new List<int>());
                return null;
            }

            private static void DFS(int[] nums, int index, int k, IList<IList<int>> result, IList<int> slate)
            {
                if (index == nums.Length)
                {
                    //result.Add(new List<int>(slate));
                    return;
                }

                if (slate.Count < k)
                {
                    //exclude
                    DFS(nums, index + 1, k, result, slate);

                    //include
                    slate.Add(nums[index]);

                    if (slate.Count == k)
                    {
                        result.Add(new List<int>(slate));
                    }

                    DFS(nums, index + 1, k, result, slate);
                    slate.RemoveAt(slate.Count - 1);
                }
            }
        }

        //159. Longest Substring with At Most Two Distinct Characters
        class LongestSubstringwithAtMostTwoDistinctCharacters
        {
            //https://leetcode.com/problems/longest-substring-with-at-most-two-distinct-characters/submissions/
            public static int LengthOfLongestSubstringTwoDistinct(string s)
            {
                //sliding window .... 
                //l and r start from 0
                //till we have 2 distinct character, keep expanding... we will keep growing till we make the window invalid (more than 2 distinct)
                //once unstable  shrink from left till we make it valid again...

                int maxLength = int.MinValue;
                Dictionary<char, int> dict = new Dictionary<char, int>();
                int l = 0, r = 0;

                while (r < s.Length)
                {
                    if (!dict.ContainsKey(s[r]))
                        dict.Add(s[r], 0);
                    dict[s[r]]++;

                    while (dict.Keys.Count > 2) //invalid
                    {
                        //shrink from left to stanble it

                        if (dict.ContainsKey(s[l]) && dict[s[l]] > 0)
                            dict[s[l]]--;

                        if (dict.ContainsKey(s[l]) && dict[s[l]] == 0)
                            dict.Remove(s[l]);

                        l++;
                    }
                    maxLength = Math.Max(maxLength, r - l + 1);

                    r++;
                }
                return (maxLength == int.MinValue) ? 0 : maxLength;
            }
        }

        //165. Compare Version Numbers
        class CompareVersionNumbers
        {
            public static int CompareVersion(string version1, string version2)
            {

                int l1 = 0, r1 = 0, l2 = 0, r2 = 0;
                int num1, num2;
                while (r1 < version1.Length || r2 < version2.Length)
                {
                    num1 = 0; num2 = 0;
                    if (r1 < version1.Length)
                    {
                        while (r1 < version1.Length && version1[r1] != '.')
                        {
                            r1++;
                        }
                        num1 = int.Parse(version1.Substring(l1, r1 - l1));
                        r1++;
                        l1 = r1;
                    }

                    if (r2 < version2.Length)
                    {
                        while (r2 < version2.Length && version2[r2] != '.')
                        {
                            r2++;
                        }
                        num2 = int.Parse(version2.Substring(l2, r2 - l2));
                        r2++;
                        l2 = r2;
                    }

                    if (num1 != num2)
                        return num1.CompareTo(num2);
                }
                return 0;
            }
        }

        //209. Minimum Size Subarray Sum
        class MinimumSizeSubarraySum
        {
            public static int MinSubArrayLen(int s, int[] nums)
            {
                IList<IList<int>> result = new List<IList<int>>();
                result.Add(new List<int>() { 1, 2 });

                int l = 0, r = 0;
                int sum = 0, min = int.MaxValue;
                while (r < nums.Length)
                {
                    sum += nums[r];
                    if (sum >= s)
                        min = Math.Min(min, r - l + 1);

                    //shrink of the sum is higher
                    while (sum > s)
                    {
                        sum -= nums[l];
                        l++;

                        if (sum >= s)
                            min = Math.Min(min, r - l + 1);

                    }

                    Console.WriteLine(sum + ":" + l + ":" + r);
                    r++;
                }

                while (l < r)
                {
                    sum -= nums[l];
                    l++;
                    if (sum >= s)
                        min = Math.Min(min, r - l + 1);
                }

                return min;
            }
        }

        //218. The Skyline Problem
        class Skyline
        {
            public static IList<IList<int>> GetSkyline(int[][] buildings)
            {
                IList<IList<int>> result = new List<IList<int>>();

                int[][] pillers = new int[buildings.Length * 2][];

                int x = 0;
                for (int i = 0; i < buildings.Length; i++)
                {
                    //    bottom, height, 0/1            
                    pillers[x++] = new int[3] { buildings[i][0], buildings[i][2], 0 }; //start piller
                    pillers[x++] = new int[3] { buildings[i][1], buildings[i][2], 1 }; //end piller
                }
                Array.Sort(pillers, new PillerPosAscending());

                SortedSet<int[]> maxQ = new SortedSet<int[]>(new DescendingSorter());
                int lastTopHeight = 0;
                int newTopHeight = 0;
                for (int i = 0; i < pillers.Length; i++)
                {
                    int[] piller = pillers[i];
                    int posIndex = 0;
                    int typeIndex = 2;
                    int heightIndex = 1;

                    if (piller[typeIndex] == 0) //start piller
                    {
                        maxQ.Add(piller);

                        newTopHeight = maxQ.First()[heightIndex];
                        if (newTopHeight != lastTopHeight)
                        {
                            if (result.Count > 0 && result.Last()[0] == piller[posIndex])
                                result.Remove(result.Last());

                            result.Add(new List<int>(new int[2] { piller[posIndex], newTopHeight }));
                        }
                        lastTopHeight = newTopHeight;
                    }
                    else //end piller, delete it from MaxQ
                    {
                        maxQ.Remove(piller);
                        newTopHeight = (maxQ.Count == 0) ? 0 : maxQ.First()[heightIndex];

                        if (newTopHeight != lastTopHeight)
                        {
                            if (result.Count > 0 && result.Last()[1] == piller[1])
                                result.Remove(result.Last());

                            result.Add(new List<int>(new int[2] { piller[posIndex], newTopHeight }));
                        }
                        lastTopHeight = newTopHeight;
                    }
                }

                return result;
            }

            class DescendingSorter : IComparer<int[]>
            {
                public int Compare(int[] x, int[] y)
                {
                    int heightIndex = 1;
                    return y[heightIndex].CompareTo(x[heightIndex]);
                }
            }

            class PillerPosAscending : IComparer<int[]>
            {
                public int Compare(int[] x, int[] y)
                {
                    int posIndex = 0;
                    return x[posIndex].CompareTo(y[posIndex]);
                }
            }
        }

        public class ShortestPalindrome
        {
            public static string GetShortestPalindrome(string s)
            {
                if (s == null || s.Length == 0)
                {
                    return s;
                }
                StringBuilder sb = new StringBuilder();
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    sb.Append(s[i]);
                }
                String t = sb.ToString();
                for (int i = 0; i <= t.Length; i++)
                {
                    if (s.StartsWith(t.Substring(i)))
                    {
                        return t.Substring(0, i) + s;
                    }
                }
                return t + s;
            }
        }

        //227. Basic Calculator II
        class BasicCalculatorII
        {
            public static int Calculate(string s)
            {
                Stack<string> stack = new Stack<string>();
                int l = 0, r = 0;
                while (r < s.Length)
                {
                    if (s[r] == ' ')
                    {
                        r++;
                        l = r;
                    }
                    else if (s[r] == '+' || s[r] == '-' || s[r] == '*' || s[r] == '/')
                    {
                        stack.Push(s.Substring(r, 1));
                        r++;
                        l = r;
                    }
                    else
                    {
                        while (r < s.Length && char.IsDigit(s[r]))
                            r++;

                        if (l != r)
                        {
                            int cVal = int.Parse(s.Substring(l, r - l));

                            if (stack.Any() && (stack.Peek() == "*" || stack.Peek() == "/"))
                            {
                                string op = stack.Pop(); //remove operator    
                                int poppeedVal = int.Parse(stack.Pop());
                                poppeedVal = (op == "/") ? (poppeedVal / cVal) : (poppeedVal * cVal);
                                stack.Push(poppeedVal.ToString());
                            }
                            else
                            {
                                stack.Push(s.Substring(l, r - l));
                            }
                            l = r;
                        }

                        /*if (r < s.Length && (s[r] == '+' || s[r] == '-' || s[r] == '*' || s[r] == '/'))
                        {
                            stack.Push(s.Substring(r, 1));
                            r++;
                            l = r;
                        }*/
                    }
                }

                Console.WriteLine(stack.Count);

                Stack<string> reverseStack = new Stack<string>();
                while (stack.Any())
                    reverseStack.Push(stack.Pop());

                int result = 0;
                while (reverseStack.Any())
                {
                    if (reverseStack.Peek() == "+" || reverseStack.Peek() == "-")
                    {
                        string op = reverseStack.Pop(); //remove operator
                        int val = int.Parse(reverseStack.Pop());
                        result = (op == "+") ? (result + val) : (result - val);
                    }
                    else
                    {
                        result = int.Parse(reverseStack.Pop());
                    }
                }

                return result;
            }
        }

        //Minimize Deviation in Array
        public class MinimizeDeviationinArray
        {
            class Pair//: IComparable<Pair>
            {
                public int Value;
                public int Index;
                public Pair(int val, int idx)
                {
                    Value = val;
                    Index = idx;
                }
            }
            class AscendingSorter : IComparer<Pair>
            {
                public int Compare(Pair x, Pair y)
                {
                    if (x.Index == y.Index)
                        return 0;
                    else if (x.Value == y.Value)
                        return x.Index.CompareTo(y.Index);
                    else
                        return x.Value.CompareTo(y.Value);
                }
            }
            public static int MinimumDeviation(int[] nums)
            {
                /*SortedSet<int> MaxQ = new SortedSet<int>();
                int min = int.MaxValue, v = 0, result = int.MaxValue;
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] % 2 == 1) //odd
                        v = nums[i] * 2;
                    else
                        v = nums[i];

                    MaxQ.Add(v);// new Pair(v, i));
                    min = Math.Min(min, v);
                }

                while (MaxQ.Max % 2 == 0)
                {
                    int max = MaxQ.Max;
                    result = Math.Min(result, max - min);
                    MaxQ.Remove(max);

                    //dividing even further, till it becomes odd
                    max = max / 2;
                    min = Math.Min(min, max);

                    MaxQ.Add(max);
                }
                result = Math.Min(result, MaxQ.Max - min);

                return result;*/

                SortedSet<Pair> MaxQ = new SortedSet<Pair>(new AscendingSorter());
                int min = int.MaxValue, v = 0, result = int.MaxValue;
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] % 2 == 1) //odd
                        v = nums[i] * 2;
                    else
                        v = nums[i];

                    MaxQ.Add(new Pair(v, i));
                    min = Math.Min(min, v);
                }

                while (MaxQ.Max.Value % 2 == 0)
                {
                    Pair maxPair = MaxQ.Max;
                    result = Math.Min(result, maxPair.Value - min);
                    MaxQ.Remove(maxPair);

                    //dividing even further, till it becomes odd
                    maxPair.Value = maxPair.Value / 2;
                    //updating new Minimum
                    min = Math.Min(min, maxPair.Value);

                    MaxQ.Add(new Pair(maxPair.Value, maxPair.Index));
                }
                result = Math.Min(result, MaxQ.Max.Value - min);

                return result;
            }
        }

        //1745. Palindrome Partitioning IV
        class PalindromePartitioningIV
        {
            public static bool CheckPartitioning(string s)
            {
                //O(n^2)
                for(int i = 2; i < s.Length; i++)
                {
                    if(isPalindrome(s, i, s.Length - 1)) //3rd palindrome check
                    {
                        for(int j = 1; j < i; j++)
                        {
                            if (isPalindrome(s, j, i - 1) && isPalindrome(s, 0, j - 1))
                                return true;
                        }
                    }
                }
                return false;
            }
            static bool isPalindrome(string s, int l, int r)
            {
                while(l < r)
                {
                    if (s[l] != s[r])
                        return false;
                }
                return true;
            }
        }

        //1743. Restore the Array From Adjacent Pairs
        class RestoretheArrayFromAdjacentPairs
        {
            public static int[] RestoreArray(int[][] adjacentPairs)
            {
                Dictionary<int, IList<int>> adjacentEdges = new Dictionary<int, IList<int>>();
                for (int i = 0; i < adjacentPairs.Length; i++)
                {
                    int x = adjacentPairs[i][0];
                    int y = adjacentPairs[i][1];
                    if (!adjacentEdges.ContainsKey(x))
                        adjacentEdges.Add(x, new List<int>());
                    adjacentEdges[x].Add(y);

                    if (!adjacentEdges.ContainsKey(y))
                        adjacentEdges.Add(y, new List<int>());
                    adjacentEdges[y].Add(x);
                }

                IList<int> result = new List<int>();

                //find the first key which have only 1 edge..
                int start = 0;
                foreach (int key in adjacentEdges.Keys)
                {
                    if (adjacentEdges[key].Count == 1)
                    {
                        result.Add(key);
                        result.Add(adjacentEdges[key][0]);

                        start = adjacentEdges[key][0];

                        adjacentEdges.Remove(key);

                        break;
                    }
                }

                while (adjacentEdges.Count > 0 && adjacentEdges.ContainsKey(start))
                {
                    foreach (int v in adjacentEdges[start])
                    {
                        if (adjacentEdges[start].Count > 1 && adjacentEdges.ContainsKey(v))
                        {
                            result.Add(v);
                            adjacentEdges.Remove(start);
                            start = v;

                            break;
                        }
                        else if(adjacentEdges[start].Count == 1)
                            adjacentEdges.Remove(start);
                        
                    }
                }
                return result.ToArray();
            }
        }

        //573 : Squirrel Simulation
        class SquirrelSimulation
        {
            public static int MinDistance(int height, int width, int[] tree, int[] squirrel, int[][] nuts)
            {

                /*
                you can say that we'll achieve the maximum profit by picking up a nut which is farther from the tree but closer to the squirrel, as the first nut. This is because, the only travel distance which we can save is from the tree to the nut, but to achieve this saving, we need to go from the squirrel's start position to the nut's position. 

                Why is the saving d is the difference between distance(tree,nut) and distance(squirrel,nut)? So the difference is actually how farther nut is from tree compared to how farther it is from squirrel. So is the algorithm to pick the nut which is farther from tree but closer to squirrel?

                You can think it like this, we actually swap from one path to the tree to squirrel to that nut. Hence, it means sum => sum - dis(nut, tree) + dis(squirrel, nut)
            => sum - (dis(nut, tree) - dis(squirrel, nut)), therefore, we want to maximum that value inside.

                */

                int totalDistnace = 0, maxDistance = int.MinValue;
                for (int i = 0; i < nuts.Length; i++)
                {
                    //sum  of tree-nut distance 
                    totalDistnace += Distance(nuts[i], tree) * 2;

                    //we need a Nut which is farthest from tree but closest to squirrel
                    maxDistance = Math.Max(maxDistance, Distance(nuts[i], tree) - Distance(nuts[i], squirrel));
                }

                return totalDistnace - maxDistance;
            }

            public static int Distance(int[] a, int[] b)
            {
                /*Manhattan distance - generally works only if the points are arranged in the form of a grid and the problem which we are working on gives more priority to the distance between the points only along with the grids, but not the geometric distance.*/
                return Math.Abs(a[0] - b[0]) + Math.Abs(a[1] - b[1]);
            }
        }

        // 1353. Maximum Number of Events That Can Be Attended
        public class MaximumNumberofEventsThatCanBeAttended
        {
            class Pair : IComparable<Pair>
            {
                public int Index;
                public int Val;

                public Pair(int idx, int val)
                {
                    this.Index = idx;
                    this.Val = val;
                }

                public int CompareTo(Pair other)
                {
                    // Two nodes are equal only when their indices are same 
                    if (this.Index == other.Index)
                    {
                        return 0;
                    }
                    else if (this.Val == other.Val)
                    {
                        return this.Index.CompareTo(other.Index);
                    }
                    else
                    {
                        return this.Val.CompareTo(other.Val);
                    }
                }
            }

            class DuplicateAllowed : IComparer<int>
            {
                public int Compare(int x, int y)
                {
                    if (x == y)
                        return 1;
                    else
                        return x.CompareTo(y);
                }
            }

            public static int MaxEvents(int[][] events)
            {
                SortedSet<Pair> minQ = new SortedSet<Pair>();
                Array.Sort(events,
                    delegate (int[] a, int[] b)
                    {
                        return a[0].CompareTo(b[0]);
                    }
                );
                int result = 0, i = 0;
                for (int d = 1; d <= 100000; d ++)
                {
                    //remove old
                    while (minQ.Any() && minQ.Min.Val < d)
                        minQ.Remove(minQ.Min);

                    while (i < events.Length && events[i][0] == d)
                    {
                        minQ.Add(new Pair(i, events[i][1]));
                        i++;
                    }

                    if (minQ.Any())
                    {
                        minQ.Remove(minQ.Min);
                        result++;
                    }
                }

                return result;
            }
        }

        //1754. Largest Merge Of Two Strings
        public class LargestMergeOfTwoStrings
        {
            public static string LargestMerge(string word1, string word2)
            {
                int w1 = 0, w2 = 0;
                StringBuilder sb = new StringBuilder();
                while (w1 < word1.Length && w2 < word2.Length)
                {
                    if (word1[w1] > word2[w2])
                        sb.Append(word1[w1++]);
                    else if (word1[w1] < word2[w2])
                        sb.Append(word2[w2++]);
                    else
                    {
                        int w11 = w1, w22 = w2;
                        while (w11 < word1.Length && w22 < word2.Length)
                        {
                            if (word1[w11] > word2[w22])
                            {
                                sb.Append(word1[w1++]);
                                break;
                            }
                            else if (word1[w11] < word2[w22])
                            {
                                sb.Append(word2[w2++]);
                                break;
                            }
                            else
                            {
                                w11++;
                                w22++;

                                //once we reach end of a string.
                                if (w11 == word1.Length || w22 == word2.Length)
                                {
                                    int pendingW1 = word1.Length - w11;
                                    int pendingW2 = word2.Length - w22;
                                    //check whose pending length is bigger
                                    if (pendingW1 >= pendingW2)
                                        sb.Append(word1[w1++]);
                                    else if (pendingW2 > pendingW1)
                                        sb.Append(word2[w2++]);

                                    break;
                                }
                            }
                        }
                    }
                }

                while (w1 < word1.Length)
                    sb.Append(word1[w1++]);

                while (w2 < word2.Length)
                    sb.Append(word2[w2++]);

                return sb.ToString();
            }
        }

        //621	Task Scheduler
        class TaskScheduler
        {
            class Task : IComparable<Task>
            {
                public char Id;
                public int Index;
                public int Count;
                public int CoolDown;
                public Task(char id, int idx, int count, int coolDown)
                {
                    Id = id;
                    Index = idx;
                    Count = count;
                    CoolDown = coolDown;
                }

                public int CompareTo(Task x)
                {
                    if (this.Index == x.Index)
                        return 0;
                    else
                    {
                        if(x.Count == Count)
                        {
                            if (CoolDown == x.CoolDown)
                                return Index.CompareTo(x.Index);
                            else
                                return CoolDown.CompareTo(x.CoolDown);
                        }
                        else
                        {
                            return x.Count.CompareTo(Count);
                        }
                    }
                }
            }

            public static int LeastInterval(char[] tasks, int n)
            {
                Dictionary<char, int> TaskFrequency = new Dictionary<char, int>();
                foreach (char c in tasks)
                {
                    if (!TaskFrequency.ContainsKey(c))
                        TaskFrequency.Add(c, 0);
                    TaskFrequency[c]++;
                }

                int index = 0, result = 0;
                SortedSet<Task> maxQ = new SortedSet<Task>();
                foreach (char c in TaskFrequency.Keys)
                {
                    maxQ.Add(new Task(c, index++, TaskFrequency[c], 0));
                }

                IList<Task> tmp = new List<Task>();
                while (maxQ.Any())
                {
                    Task t = maxQ.Min;
                    maxQ.Remove(t);

                    //Inserting cooling time (t.CoolDown)
                    result += (t.CoolDown + 1);

                    tmp.Clear();
                    while (maxQ.Any())
                    {
                        Task minT = maxQ.Min;
                        maxQ.Remove(minT);

                        //reduce any cooldown
                        minT.CoolDown -= (minT.CoolDown >= t.CoolDown + 1) ? t.CoolDown + 1 : 0;
                        
                        tmp.Add(minT);
                    }

                    t.Count--;
                    if (t.Count > 0)
                    {
                        t.CoolDown = n;
                        tmp.Add(t);
                    }

                    foreach (Task t2 in tmp)
                        maxQ.Add(t2);
                }

                return result;
            }
        }

        //https://leetcode.com/problems/split-array-largest-sum/
        public class SplitArrayLargestSum
        {
            //410. Split Array Largest Sum
            /*public static int SplitArray(int[] nums, int m)
            {
                int[] minMaxSum = new int[1];
                minMaxSum[0] = int.MaxValue;

                int sumLeft = 0;
                for (int x = 0; x < nums.Length; x++)
                    sumLeft += nums[x];

                Helper(nums, m, 0, sumLeft, minMaxSum);

                return minMaxSum[0];
            }

            private static int Helper(int[] nums, int m, int idx, int sumLeft, int[] minMaxSum)
            {
                if (m <= 1)
                {
                    return sumLeft;
                }

                int maxSum = 0;
                int sum = 0;
                for (int i = idx; i < nums.Length - m + 1; i++)
                {
                    //if (i + 1 >= nums.Length)
                    //    return sumLeft;

                    sum += nums[i];
                    int subArraySum = Helper(nums, m - 1, i + 1, sumLeft - sum, minMaxSum);
                    
                    maxSum = Math.Max(subArraySum, sum);
                    //minMaxSum[0] = Math.Min(minMaxSum[0], maxSum);
                }
                minMaxSum[0] = Math.Min(minMaxSum[0], maxSum);
                return maxSum;
            }*/

            static int result = int.MaxValue;
            public static int SplitArray(int[] nums, int m)
            {
                //int[] minMaxSum = new int[1];
                //minMaxSum[0] = int.MaxValue;

                int sumLeft = 0;
                for (int x = 0; x < nums.Length; x++)
                    sumLeft += nums[x];

                if (m == 1)
                    return sumLeft;

                int[,] memo = new int[nums.Length, m + 1];
                int v = Helper(nums, m, 0, sumLeft, 0, memo);

                return result;
            }

            private static int Helper(int[] nums, int m, int idx, int sumLeft, int maxSoFarInGroup, int[,] memo)
            {
                if (m <= 1)
                {
                    result = Math.Min(result, Math.Max(sumLeft, maxSoFarInGroup));
                    return sumLeft;
                }
                if(memo[idx, m] != 0)
                {
                    result = Math.Min(result, Math.Max(memo[idx, m], maxSoFarInGroup));
                    return memo[idx, m];
                }    

                int minSum = int.MaxValue;
                int sum = 0;
                for (int i = idx; i < nums.Length - m + 1; i++)
                {
                    sum += nums[i];
                    int subArraySum = Helper(nums, m - 1, i + 1, sumLeft - sum, Math.Max(sum, maxSoFarInGroup), memo);
                    minSum = Math.Min(minSum, Math.Max(subArraySum, sum));
                }
                memo[idx, m] = minSum;

                return minSum;
            }
        }

        public static int  NumberOfArithmeticSlices(int[] A)
        {
            if (A.Length < 3)
                return 0;

            int count = 0, l = 0, r = 1, sequenceCount = 1, arithmDiff = A[l] - A[r];//Math.Abs(A[r] - A[l]);
            while (r < A.Length)
            {
                //Console.WriteLine("r=" + r +":l=" + l + " Diff=" + arithmDiff + ", A[r]=" + A[r] + ", A[r - 1]=" + A[r - 1] + " *** D = " + Math.Abs(A[r] - A[r - 1]));

                //if(Math.Abs(A[r] - A[r - 1]) == arithmDiff)
                if (A[r - 1] - A[r] == arithmDiff)
                {
                    /*if(sequenceCount >= 2)
                    {
                        Console.WriteLine("******** r=" + r +":l=" + l + " Diff=" + arithmDiff + ", A[r]=" + A[r] + ", A[r - 1]=" + A[r - 1] + " *** D = " + Math.Abs(A[r] - A[r - 1]));
                    }*/
                    sequenceCount++;
                    r++;
                }
                else
                {
                    //if(sequenceCount > 2)

                    //if(r - l - 1 >= 3)//sequenceCount >= 3)
                    if (sequenceCount >= 3)
                    {
                        Console.WriteLine("======" + r + ":l=" + l + ":Diff=" + arithmDiff + ":A[r] = " + A[r] + ": A[r - 1] = " + A[r - 1]);
                        count += numberOfSlices(r - l);
                    }

                    l = r - 1;
                    
                    //arithmDiff = (r < A.Length) ? Math.Abs(A[r] - A[l]) : int.MaxValue;
                    arithmDiff = (r < A.Length) ? A[l] - A[r] : 0;
                    //r++;
                    sequenceCount = 1;

                    Console.WriteLine("======" + r + ":l=" + l + ":Diff=" + arithmDiff);

                }
            }

            if(sequenceCount >= 3)
            //if(r - l - 1 >= 3)
                count += numberOfSlices(r - l);
            

            return count;
        }

        private static int numberOfSlices(int n)
        {
            Console.WriteLine(n + ":****");

            if (n < 3) return 0;
            if (n == 3) return 1;
            int innerSlices = numberOfSlices(n - 1);
            return innerSlices + (n - 2);
        }

        public static bool CanPartitionKSubsets(int[] nums, int k1)
        {
            int total = 0;
            IList<int> lst = new List<int>();
            foreach (int item in nums)
            {
                lst.Add(item);
                total += item;
            }

            //Console.WriteLine(total);

            if (total % k1 != 0)
                return false;

            int partSum = total / k1;

            for (int x = 1; x < k1; x++)
            {
                bool[,] dp = new bool[lst.Count + 1, partSum + 1];

                for (int r = 0; r <= lst.Count; r++)
                    dp[r, 0] = true;

                for (int r = 1; r <= lst.Count; r++)
                {
                    for (int c = 1; c <= partSum; c++)
                    {
                        if (c >= nums[r - 1])
                        {
                            dp[r, c] = dp[r - 1, c] //exclude
                                || dp[r - 1, c - nums[r - 1]]; //include
                        }
                        else
                        {
                            dp[r, c] = dp[r - 1, c];
                        }

                        //if (dp[r, c])
                        //{
                        //    lst.RemoveAt(r - 1);
                            //hs.Add(r - 1);
                        //}
                    }
                }

                //Console.WriteLine(dp[nums.Length, partSum]);
                for(int j=0; j < dp.GetLength(0); j++)
                {
                    for (int k = 0; k < dp.GetLength(1); k++)
                    {
                        Console.Write(dp[j, k] + ", ");
                    }
                    Console.WriteLine();
                }

                if (!dp[nums.Length, partSum])
                    return false;

            }
            return true;

        }


        public class ValidateStackSequences
        {
            public static bool Validate(int[] pushed, int[] popped)
            {
                int pushedIndex = 0;
                Stack<int> stack = new Stack<int>();
                for (int i = 0; i < popped.Length; i++)
                {
                    int lastPopped = popped[i];
                    if(stack.Count == 0)
                    {
                        while (pushedIndex < pushed.Length - 1 && pushed[pushedIndex] != lastPopped)
                        {
                            stack.Push(pushed[pushedIndex]);
                            pushedIndex++;
                        }
                        if (pushedIndex <= pushed.Length - 1)
                            stack.Push(pushed[pushedIndex++]);

                        if (stack.Pop() != lastPopped)
                            return false;
                    }
                    else
                    {
                        if (stack.Peek() == lastPopped)
                            stack.Pop();
                        else
                        {
                            while (pushedIndex < pushed.Length - 1 && pushed[pushedIndex] != lastPopped)
                            {
                                stack.Push(pushed[pushedIndex]);
                                pushedIndex++;
                            }
                            if(pushedIndex <= pushed.Length - 1)
                                stack.Push(pushed[pushedIndex++]);

                            if (stack.Pop() != lastPopped)
                                return false;
                        }
                    }
                }
                return true;


                int popStIndex = -1, popEndIndex = -1;
                

                for (int i = 0; i < popped.Length; i++)
                {
                    int lastPopped = popped[i];

                    if(popStIndex == -1)
                    {
                        //start the process. to pop we must push
                        while (pushedIndex < pushed.Length - 1 && pushed[pushedIndex] != lastPopped)
                            pushedIndex++;

                        if (popStIndex == -1)
                            popStIndex = pushedIndex;
                        popEndIndex = pushedIndex;
                    }
                    else if (popStIndex == 0)
                    {
                        //stack is empty... //push to pop
                        while (pushedIndex < pushed.Length - 1 && pushed[pushedIndex] != lastPopped)
                            pushedIndex++;

                        if (pushedIndex == pushed.Length - 1 && pushed[pushedIndex] != lastPopped)
                            return false;
                        else
                            popEndIndex = pushedIndex;
                    }
                    else //stack not empty... we have 2 options
                    {
                        //either pop
                        if (popStIndex - 1 >= 0 && pushed[popStIndex - 1] == lastPopped)
                            popStIndex--;
                        else
                        {
                            //push to pop
                            while (pushedIndex < pushed.Length - 1 && pushed[pushedIndex] != lastPopped)
                                pushedIndex++;

                            if (pushedIndex == pushed.Length - 1 && pushed[pushedIndex - 1] != lastPopped)
                                return false;
                            else
                                popEndIndex = pushedIndex;
                        }
                    }
                    continue;


                    if (popStIndex != -1)
                    {
                        // search for lastPopped in pushed 
                        //if not found, move popEndIndex forward
                        if (popStIndex - 1 >= 0 && pushed[popStIndex - 1] == lastPopped)
                            popStIndex--;
                        else
                        {
                            if (pushedIndex < pushed.Length - 1)
                            {
                                while (pushedIndex < pushed.Length - 1 && pushed[pushedIndex] != lastPopped)
                                    pushedIndex++;
                                
                                popEndIndex = pushedIndex;
                            }
                            else
                                return false;
                        }
                    }
                    else
                    {
                        while (pushed[pushedIndex] != lastPopped)
                            pushedIndex++;

                        if (popStIndex == -1)
                            popStIndex = pushedIndex;
                        popEndIndex = pushedIndex;
                    }
                }
                return (popStIndex == 0);
            }
        }

        public static void runTest()
        {
            ValidateStackSequences.Validate(new int[] { 1,2,3,4,5 }, new int[] { 4, 5, 3, 2, 1 });


            CanPartitionKSubsets(new int[] { 4, 3, 2, 3, 5, 2, 1 }, 4);

            NumberOfArithmeticSlices(new int[] { 27, 2, -32, 38, -21, 27, -44, 34, -31, -5, -2, -20, 7, -31, 14, 5, 30, -12, -23, -10, 13, 28, -19, 21, -45, 15, -33, 16, 47, -23, -25, -22, 45, 43, -4, 14, -46, 36, 11, -21, -19, 35, 47, 39, 29, -41, -9, -34, 29, -11, -43, -18, 45, -5, 12, 0, 14, -41, 48, -11, 10, 22, -15, 12, -35, 42, 5, -2, -8, 31, -23, 45, -17, 25, -21, -12, -6, -2, 0, -21, 33, 7, -35, -6, 43, 10, -38, -12, 40, 10, -9, 18, -31, 18, -1, 31, -9, -17, 2, -12, -38, -16, 40, -46, 26, -44, 38, 39, 10, -4, 10, -17, -24, -21, 28, 40, -5, -33, -23, 8, -3, 8, -9, -25, -38, 23, -50, 15, 25, -30, 46, 12, -6, 39, 28, -1, -48, 38, 28, -44, -9, -22, 13, -21, 3, -10, 24, -33, -13, 35, 18, 3, -49, -45, 23, 19, 15, 0, -9, -1, -17, -39, -50, -3, 33, 32, -32, 41, -44, -23, 42, -5, -42, -10, 46, -28, 8, -23, 38, 13, 30, 6, 46, 7, -45, 21, 5, -28, -10, 36, 6, -36, 33, -31, 15, -41, 15, 11, -14, -27, -27, -22, 5, -12, -34, 27, -6, 39, 36, -13, 2, -2, -41, 39, -29, -1, -29, 5, 22, 12, -16, 22, 13, 16, -10, 6, 41, -29, 12, -24, -3, -29, -7, 31, -8, -36, 2, -49, -6, -2, -48, -11, 5, 43, 49, 48, -30, 18, 34, -27, 6, 22, 32, -42, -36, 18, 42, -33, -44, -18, -43, -12, -11});

            SplitArrayLargestSum.SplitArray(new int[] { 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13 }, 5 );

            CountSubarraySumEqualsK.CountSubarraySum(new int[] { 0, 1, 2, 3, -2, -1, 0 }, 3);

            SplitArrayLargestSum.SplitArray(new int[] { 7, 2, 5, 10, 8}, 2);

            TaskScheduler.LeastInterval(new char[] {'A', 'A', 'A', 'B', 'B', 'B', 'C', 'C', 'C', 'D', 'D', 'E'}, 2);

            LargestMergeOfTwoStrings.LargestMerge("uuurr", "urrrur");

            MaximumNumberofEventsThatCanBeAttended.MaxEvents(new int[][] { new int[]{ 1, 2 }, new int[] { 2,3 }, new int[] { 3,4 }, new int[] { 1,2 }});


            SquirrelSimulation.MinDistance(3, 3, new int[] { 2, 2 }, new int[] { 2, 1 }, new int[][] { new int[] { 0, 0 }, new int[] { 1, 2} });
            //[4,-10],[-1,3],[4,-3],[-3,3]
            RestoretheArrayFromAdjacentPairs.RestoreArray(new int[][] { new int[] { 4, -10 }, new int[] { -1, 3 }, new int[] { 4, -3 }, new int[] { -3, 3 } });

            PalindromePartitioningIV.CheckPartitioning( "aazfcvvvuqhxcsyqvjmewpotbgczdxohxspaxrztiuakpvfmsoztlmyqwlwbmvrtdjjrsgzdzxuuesbiicjwgahwydckxinoncrgzsqahamzykirewnekylyjvnjjvwkejsjrevpdabcqnnxvxsibyftopjipwfgrzzqwnawtbwevvsadcckfixnxifkccdasvvewbtwanwqzzrgfwpijpotfybisxvxnnqcbadpverjsjekwvjjnvjylykenwerikyzmahaqszgrcnonixkcdywhagwjciibseuuxzdzgsrjjdtrvmbwlwqymltzosmfvpkauitzrxapsxhoxdzcgbtopwemjvqyscxhquvvvcfzaarelbkxtlpqdomvpmufxibdbvrskmwwszslvyvimpahgcznvmkwpztiiiiiitzpwkmvnzcghapmivyvlszswwmksrvbdbixfumpvmodqpltxkblercxauwizscedkrdzunjhvwlhkscwnfbfnwcskhlwvhjnuzdrkdecsziwuaxc");

            MinimizeDeviationinArray.MinimumDeviation(new int[] { 2, 10, 8 });

            BasicCalculatorII.Calculate("3-2+1");

            ShortestPalindrome.GetShortestPalindrome("aacecaaa");

            Skyline.GetSkyline(new int[][] { new int[] { 0,2,3 }, new int[] { 2,5,3 }});

            Skyline.GetSkyline(new int[][] { new int[] { 2, 9, 10 }, new int[] { 3, 7, 15 }, new int[] { 5, 12, 12 }, new int[] { 15, 20, 10 }, new int[] { 19, 24, 8 } });

            MinimumSizeSubarraySum.MinSubArrayLen(11, new int[] { 1, 2, 3, 4, 5});
            CompareVersionNumbers.CompareVersion("7.5.2.4", "7.5.3");
            LongestSubstringwithAtMostTwoDistinctCharacters.LengthOfLongestSubstringTwoDistinct("eceba");
            FindtheMostCompetitiveSubsequence.MostCompetitive(new int[] { 1,2,3,4}, 2);
            FindMinimuminRotatedSortedArray.FindMin(new int[] { 3,3,4,4,0,0,1,1,2,2 });
            SlidingWindowMedian.MedianSlidingWindow(new int[] {1, 4, 2, 3}, 4);

            FindMedianfromDataStream fm = new FindMedianfromDataStream();
            fm.AddNum(1);
            fm.AddNum(2);
            fm.AddNum(3);

            LongestSubstringWithoutRepeatingCharacters.LengthOfLongestSubstring("pwwkew");
            KthMissingPositiveNumber.FindKthPositive(new int[] { 1, 2, 3, 4 }, 2);

            //[[1,0],[-3,1],[-4,0],[2,3]]
            int[][] ar = new int[1][];
            ar[0] = new int[2] { 4, 0};
            //ar[1] = new int[2] { 3, 0 };
            //ar[2] = new int[2] { -4, 0 };
            //ar[3] = new int[2] { 2, 3 };
            SumOfEvenNumbersAfterQueries.SumEvenAfterQueries(new int[] { 1 }, ar);

            SummaryRanges.GetSummaryRanges(new int[] { -2147483648, -2147483647, 2147483647 });
            LargestRectangleinHistogram.LargestRectangleArea(new int[] { 4 , 5 , 3 , 2 , 1 , 3 , 4 , 2 });

            FindAnIntegerNotInList.Get(new uint[] { 4294967295, 399999999, 0 });
            FirstSmallestMissingPositive.Find(new int[] { 3, 4, -1, 1 });

            SortAllCharacters.sort_array(new List<char>() { 'a', 'z', 'i', '#', '&', 'l', 'c' });

            List<List<int>> dist = new List<List<int>>();
            dist.Add(new List<int>() { 1, 0 });
            dist.Add(new List<int>() { 2, 1 });
            dist.Add(new List<int>() { 0, 1 });
            NearestNeighbours.nearest_neighbours(1, 1, 2, dist);


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
