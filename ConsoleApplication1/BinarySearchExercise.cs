using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class BinarySearchExercise
    {
        private static bool IsMedianFoundInArrayA(int[] a, int[] b, int num_elements_larger_than_median_in_total, ref int median)
        {
            int a_right = a.Length - 1;
            int b_right = b.Length - 1;

            int low = 0;
            int high = a.Length - 1;
            while (low <= high)                 // termination means no overall median within A
            {
                int overall_median = (low + high) / 2;     // guess for the median as middle of A
                long num_larger_elements_in_a = a_right - overall_median;
                long num_larger_elements_in_b = num_elements_larger_than_median_in_total - num_larger_elements_in_a;
                long i_larger_in_b = b_right - num_larger_elements_in_b + 1;    // starting index in array B of elements larger than median of A
                                                                                // Compare with the left element in B if there is one
                if (i_larger_in_b <= 0)                // no elements to the left in B. #2 check can't be performed
                {
                    if (a[overall_median] <= b[0] && i_larger_in_b == 0)      // satisfies being an overall median. check the element to the right (check #1)
                    {
                        median = overall_median;
                        return true;                        // median was found in A
                    }
                    else
                    {   // a[ overall_median ] > b[ i_larger_in_b = 0 ] => guess of overal median is too big to be the overall median. Need to move overall_median within A to become smaller
                        high = overall_median - 1;          // do not include it in our further searches, since it's not the overall median
                    }
                }   // i_larger_in_b > 0  => have a left element to compare with. Compare with the right element in B if there is one
                else if (i_larger_in_b > b_right)           // no elements to the right in B. #1 check can't be performed
                {
                    if (b[b_right] <= a[overall_median] && (i_larger_in_b - 1) == b_right)  // satisfies being an overall median
                    {
                        median = overall_median;
                        return true;                        // median was found in A
                    }
                    else
                    {   // a[ overall_median ] < b[ i_larger_in_b = b_right ] => guess of overall median is too small to be the overall median. Need to move overall_median within A to become smaller
                        low = overall_median + 1;           // do not iclude it in our further searches, since it's not the overall median
                    }
                }   // 0 < i_larger_in_b <= b_right => have two elements within B to compare with a[ overall_median ]
                else if (b[i_larger_in_b - 1] <= a[overall_median]) // check the element to the left  (check #2)
                {
                    if (a[overall_median] <= b[i_larger_in_b])          // check the element to the right (check #1)
                    {
                        median = overall_median;
                        return true;                        // median was found in A
                    }
                    else
                    {
                        high = overall_median - 1;  // do not include it in our further searches, since it's not the overall median
                    }
                }
                else
                {   // a[ overall_median ] < b[ i_larger_in_b - 1 ] => overall median guess is too small and needs to be increased
                    low = overall_median + 1;       // do not iclude it in our further searches, since it's not the overall median
                }
            }
            median = -1;
            return false;	// median was not found in A
        }

        private static double medianOfTwoSortedArrays(int[] a, int[] b)
        {
            int median = -1;
            int which_array = -1;
            int a_length = a.Length;
            int b_length = b.Length;
            int total_length = a_length + b_length;
            int median_in_total = (total_length - 1) / 2;  // works for even or odd number of elements
            int num_elements_larger_than_median_in_total = (total_length - 1) - median_in_total;       // high element in total minus median index
                                                                                                       // There may be a way to eliminate this special case of handling either one or both input arrays of length 0
            if (a_length == 0)
            {
                if (b_length == 0)
                {
                    which_array = 0;           // both arrays are of zero length, return an invalid overall median index
                    median = -1;
                    return median;// a[median];
                }
                else
                {   // b_length != 0
                    which_array = 1;           // median was found in B immediately since A has no elements
                    median = median_in_total;
                    return b[median];// = -1;
                }
                //return median;
            }
            else if (b_length == 0)
            {
                which_array = 0;               // median was found in A immediately since B has no elements
                median = median_in_total;
                return a[median];
            }
            // a_length > 0 and b_length > 0
            if (IsMedianFoundInArrayA(a, b, num_elements_larger_than_median_in_total, ref median))
            {
                which_array = 0;
                return a[median];
            }
            // Search within B, since we didn't find it within A. It has to exist within B, since there is always an overall median.
            if (IsMedianFoundInArrayA(b, a, num_elements_larger_than_median_in_total, ref median))
            {
                which_array = 1;
                return b[median];
            }

            which_array = -1;
            median = -1;   // There has to be an overall median at this point, flag an error condition if it wasn't found.
            return median;
        }


        private static int FindItemInSortedArray(int[] a, int target) //O(log(N))
        {
            if (a == null || a.Length == 0)
                return -1;

            int low = 0;
            int high = a.Length - 1;
            while (low <= high)
            {
                int mid = low + ((high - low) >> 1);
                if (a[mid] == target)
                    return mid;

                if (a[mid] < target)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
            return -1;
        }

        public static int FindElement_which_appears_once_in_sortedArray(int[] arr)
        {
            int start = 0;
            int end = arr.Length - 1;

            while (start <= end)
            {
                int mid = start + (end - start) / 2;

                if (mid > 0 && arr[mid - 1] == arr[mid]) //odd
                {
                    //left 
                    if (mid - 1 % 2 == 0) //even
                    {
                        start = mid + 1;
                    }
                    else
                    {
                        end = mid - 2;
                    }
                }
                else if (mid < arr.Length && arr[mid + 1] == arr[mid])
                {
                    //right
                    if (mid % 2 == 0) //even
                    { //unique on right
                        start = mid + 2;
                    }
                    else
                    {
                        end = mid - 1;
                    }
                }
                else
                {
                    return mid;
                }
            }

            return -1;
        }

        static int findOddOccuring(int[] arr)
        {
            int low = 0;
            int high = arr.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;

                //base case
                if (low == high)
                    return low;

                if (mid % 2 == 0) //mid is even
                {
                    // if element next to mid is same as mid element, the odd element
                    // lies on the right side; otherwise it lies on the left side
                    if (arr[mid] == arr[mid + 1])
                    {
                        low = mid + 2;
                    }
                    else
                    {
                        high = mid;
                    }
                }
                else //mid is odd
                {
                    // if element before mid is same as mid element, the odd element
                    // lies on the right side; otherwise it lies on the left side
                    if (arr[mid] == arr[mid - 1])
                    {
                        low = mid + 1;
                    }
                    else
                    {
                        high = mid - 1;
                    }
                }
            }
            return -1;

        }

        public static int findPeakElement(int[] A)
        {
            int left = 0;
            int right = A.Length - 1;

            while (left <= right)
            {
                // find mid element
                int mid = (left + right) / 2;

                // check if mid element is greater than its neighbors
                if ((mid == 0 || A[mid - 1] <= A[mid]) &&
                        (mid == A.Length - 1 || A[mid + 1] <= A[mid]))
                {
                    return mid;
                }
                // If the left neighbor of mid is greater than the mid element,
                // then find the peak recursively in the left sub-array
                if (mid - 1 >= 0 && A[mid - 1] > A[mid])
                {
                    //return findPeakElement(A, left, mid - 1);
                    right = mid - 1;
                }

                // If the right neighbor of mid is greater than the mid element,
                // then find the peak recursively in the right sub-array
                if (mid + 1 >= A.Length && A[mid + 1] > A[mid])
                {
                    //return findPeakElement(A, left, mid - 1);
                    left = mid + 1;
                }
            }

            return -1;
        }

        private static string printArr(int[] A, int left, int right)
        {
            string str = "";
            for(; left<=right; left++)
            {
                str += A[left] +", ";
            }
            return str;
        }

        public static int MaximumSum(int[] A, int left, int right)
        {
            //ArraySegment<string> a = new ArraySegment<string>(A, left, right - left);
            
            
            // If array contains only one element
            if (right == left)
            {
                Console.WriteLine("Return  = " + A[left] + " for Array =" + printArr(A, left, right));
                return A[left];
            }

            // Find middle element of the array
            int mid = (left + right) / 2;

            // Find maximum subarray sum for the left subarray
            // including the middle element
            int leftMax = int.MinValue;
            int sum = 0;
            for (int i = mid; i >= left; i--)
            {
                sum += A[i];
                if (sum > leftMax)
                {
                    leftMax = sum;
                }
            }

            // Find maximum subarray sum for the right subarray
            // excluding the middle element
            int rightMax = int.MinValue;
            sum = 0;    // reset sum to 0
            for (int i = mid + 1; i <= right; i++)
            {
                sum += A[i];
                if (sum > rightMax)
                {
                    rightMax = sum;
                }
            }

            //Console.WriteLine("leftMax = " + leftMax + " rightMax = " + rightMax + " mid= " + mid);

            int x = MaximumSum(A, left, mid);
            int y = MaximumSum(A, mid + 1, right);
            // Recursively find the maximum subarray sum for left
            // subarray and right subarray and tale maximum
            //int maxLeftRight = Math.Max(x, y);

            //Console.WriteLine("MaximumSum(A, left, mid) = " + x + " MaximumSum(A, mid + 1, right) = " + y + " & *** maxLeftRight = " + maxLeftRight);

            //Console.WriteLine(" maxLeftRight " + maxLeftRight + " Math.Max(maxLeftRight, leftMax + rightMax) = " + Math.Max(maxLeftRight, leftMax + rightMax));
            //Console.WriteLine("Mid crossing SubArray Max = " + (leftMax + rightMax) + " for Array = " + printArr(A, left, right));
            

            //Console.WriteLine(" *** maxLeftRight = " + maxLeftRight + "  leftMax + rightMax = " + (leftMax + rightMax));

            Console.WriteLine("=============Array = " + printArr(A, left, right) + " leftTreeMax=" + x +" RightTreeMax=" + y + " max crossing left = " + leftMax + " max crossing right =" + rightMax + " max crossing" + (leftMax + rightMax) );


            // return maximum of the three
            return Math.Max(Math.Max(x, y), leftMax + rightMax);
        }

        static int smallestMissing(int[] arr)
        {
            //[1] --> 0
            //[0] --> 1
            //[] --> 0
            //[0, 1, 2, 6, 9, 11, 15] --> 3
            //[1, 2, 3, 4, 6, 9, 11, 15] -->0
            //[0, 1, 2, 3, 4, 5, 6] --> 7

            int low = 0;
            int high = arr.Length - 1;
               
            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                // if mid index matches with the mid element, then the mismatch
                // lies on the right half
                if (arr[mid] == mid)
                    low = mid + 1;
                else
                    // mismatch lies on the left half
                    high = mid - 1;

                //base case
                if (low > high)
                    return low;
            }
            return 0;
        }

        private static int GetIndexWhereTargetwouldBePlacedInSortedArray(int[] a, int target) //O(log(N))
        {
            if (a == null || a.Length == 0)
                return -1;

            int low = 0;
            int high = a.Length - 1;
            while (low <= high)
            {
                int mid = low + ((high - low) >> 1);
                if (a[mid] == target)
                    return mid;

                if (a[mid] < target)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
            return target - low;
        }

        /*
         * Using Binary Search****
         * Rotation point is to our left if the mid item is less than the first item. 
         * Else it's to our right.
        */
        private static int FindRotationPoint(string[] a) //O(log(N))
        {
            if (a == null || a.Length == 0)
                return -1;

            int low = 0;
            int high = a.Length - 1;
            string firstWord = a[0];

            while (low <= high)
            {
                int mid = low + ((high - low) >> 1);

                Console.WriteLine(string.Format("Low {0}, High{1}, Mid{2}",low,high,mid));

                // If mid comes after first word or is the first word
                if (string.Compare(a[mid], firstWord, StringComparison.Ordinal) >= 0)
                {
                    // Go right
                    low = mid + 1;
                }
                else
                {
                    // Go left
                    high = mid - 1;
                }
                Console.WriteLine(string.Format("Low {0}, High{1}, Mid{2}",low,high,mid));
            }
            return ++high;
        }

        /*
         * Using Binary Search****
         * Rotation point is to our right if the mid item is > the last item. 
         * Rotation point is to our left if the mid item is < the last item. 
         *  if the mid item == last item. reduce range by high --, as we wont lose any items, because it is present in 'mid'
         */
        private static int FindRotationPoint1(string[] a) //O(log(N))
        {
            if (a == null || a.Length == 0)
                return -1;

            int low = 0;
            int high = a.Length - 1;

            while (low <= high)
            {
                int mid = low + ((high - low) >> 1);

                Console.WriteLine(string.Format("Low {0}, High{1}, Mid{2}", low, high, mid));

                if (string.Compare(a[mid], a[high], StringComparison.Ordinal) > 0)
                    low = mid + 1;
                else if (string.Compare(a[high], a[mid], StringComparison.Ordinal) > 0)
                    high = mid;
                else
                    high--;

                Console.WriteLine(string.Format("Low {0}, High{1}, Mid{2}", low, high, mid));
            }
            return low;
        }

        // Function to find contiguous sub-array with the largest sum
        // in given set of integers (handles negative numbers as well)
        public static int kadaneNeg(int[] A)
        {
            // stores maximum sum sub-array found so far
            int maxGlobal = A[0]; //maxSoFar

            // stores maximum sum of sub-array ending at current position
            // holds, 
            int maxSubArraySumEndingAt = A[0]; //maxEndingHere

            // traverse the given array
            for (int i = 1; i < A.Length; i++)
            {
                /*
                max subarray sum at index = MAX of 
                [
                    [max subarray sum at prev index i-1 + current index value], 
                    [current index value]
                ]
                */
                maxSubArraySumEndingAt = Math.Max(maxSubArraySumEndingAt + A[i], A[i]);

                // update result if current sub-array sum is found to be greater
                maxGlobal = Math.Max(maxGlobal, maxSubArraySumEndingAt);
            }

            return maxGlobal;
        }


        static int power(int x, int n)
        {
            // initialize result by 1
            int pow = 1;

            // do till n is not zero
            while (n>0)
            {
                // if n is odd, multiply result by x
                if ((n & 1) ==1)
                    pow *= x;

                // divide n by 2
                //n = n >> 1;
                n = n / 2;

                // multiply x by itself
                x = x * x;
            }

            // return result
            return pow;
        }


        //35. Search Insert Position
        class SearchInsertPosition
        {
            public static int SearchInsert(int[] nums, int target)
            {
                int lo = 0, hi = nums.Length - 1;

                while (lo <= hi)
                {
                    int mid = lo + (hi - lo) / 2;

                    if (nums[mid] == target)
                        return mid;
                    else if (nums[mid] > target)
                        hi = mid - 1;
                    else
                        lo = mid + 1;
                }
                return hi + 1;
            }
        }


        public static void runTest()
        {
            SearchInsertPosition.SearchInsert(new int[] { 1,3,4,6,7}, 12);

            int ggg= power(2, 5);


            int[] A = { 2, -4, 1, 9, -6, 7, -3 };
            int ttt= MaximumSum(A, 0, A.Length - 1);

            //int ccc = smallestMissing(new int[] { 0,2});

            //int xxx= findOddOccuring(new int[] { 2, 2, 1, 3, 3, 2, 2, 4, 4, 1, 1 });

            double median = medianOfTwoSortedArrays(new int[] { 1, 2 },  new int[] { 3,4, 5 });

            FindElement_which_appears_once_in_sortedArray(new int[] { 1, 1, 2, 2, 4, 4, 5, 6, 6});


            var words = new string[]
            {
                
                "retrograde",
                "supplant",
                "undulate",
                "xenoepist",
                "asymptote",  // <-- rotates here!
                "babka",
                "banoffee",
                "engender",
                "karpatka",
                "othellolagkage",
                "ptolemaic",
            };
            var x= FindRotationPoint(words);
            return;
            var arr= new int[] {1, 1, 1, 3, 3, 4, 5, 6, 7};
            int position = FindItemInSortedArray(arr, 5);
            int position1 = GetIndexWhereTargetwouldBePlacedInSortedArray(arr, 9);
        }
    }
}
