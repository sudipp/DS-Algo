using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    class SortingSearchingLogarithmsExercise
    {
        class Ladder
        {

            private List<string> path;  //For storing path
            private string lastWord;  //For storing last word of path
            private int length;   //Length of the path.

            public Ladder(List<string> path)
            {
                this.path = path;
            }

            public Ladder(List<string> path, int length, string lastWord)
            {
                this.path = path;
                this.length = length;
                this.lastWord = lastWord;
            }
            public List<string> getPath()
            {
                return path;
            }
            public int getLength()
            {
                return length;
            }
            public string getLastWord()
            {
                return lastWord;
            }

            public void setPath(List<string> path)
            {
                this.path = path;
            }

            public void setLength(int length)
            {
                this.length = length;
            }
        }

        private static Ladder getShortestTransformationIterative(string startWord, string endWord, HashSet<string> dictionary)
        {
            //if(dictionary.Contains(startWord) && dictionary.Contains(endWord))
            if (dictionary.Contains(endWord))
            {
                List<string> path = new List<string>();
                path.Add(startWord);

                //All intermediate paths are stored in queue.
                Queue<Ladder> queue = new Queue<Ladder>();
                queue.Enqueue(new Ladder(path, 1, startWord));

                //We took the startWord in consideration, So remove it from dictionary, otherwise we might pick it again.
                dictionary.Remove(startWord);

                //Iterate till queue is not empty or endWord is found in Path.
                while (queue.Count != 0 && !queue.Peek().Equals(endWord))
                {
                    Ladder ladder = queue.Dequeue();

                    if (endWord.Equals(ladder.getLastWord())) {
                        return ladder;
                    }

                    IEnumerator<string> i = dictionary.GetEnumerator();
                    while (i.MoveNext()) {
                        string str = i.Current;

                        if (differByOne(str, ladder.getLastWord()))
                        {
                            List<string> list = new List<string>(ladder.getPath());
                            list.Add(str);

                            //If the words differ by one then dump it in Queue for later processsing.
                            queue.Enqueue(new Ladder(list, ladder.getLength() + 1, str));

                            //Once the word is picked in path, we don't need that word again, So remove it from dictionary.
                            //i.remove();
                            //dictionary.Remove(str);
                        }
                    }
                }

                //Check is done to see, on what condition above loop break, 
                //if break because of Queue is empty then we didn't got any path till endWord.
                //If break because of endWord matched, then we got the Path and return the path from head of Queue.
                if (queue.Count != 0)
                {
                    return queue.Dequeue();
                }
            }
            return null;
        }
        private static bool differByOne(string word1, string word2)
        {
            if (word1.Length != word2.Length)
            {
                return false;
            }

            int diffCount = 0;
            for (int i = 0; i < word1.Length; i++)
            {
                if (word1[i] != word2[i])
                {
                    diffCount++;
                }
            }
            return (diffCount == 1);
        }

        private static int BinarySearch(int[] a, int target) //O(log(N))
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

        /*Count Sort *****
         * =================================================================
         * Top Scores
         * Efficiently sort numbers in an array, where each number is below a certain maximum.
         * =================================================================
         * Each round, players receive a score between 0 and 100, which you use to rank them from highest to lowest.  
         * So far you're using an algorithm that sorts in O(nlogN) time, but players are complaining that their rankings 
         * aren't updated fast enough. You need a faster sorting algorithm.
         */
        //https://www.interviewcake.com/question/csharp/top-scores?section=hashing-and-hash-tables&course=fc1
        public static int[] SortScores(int[] theArray, int maxValue) //Time O(n + maxValue), space O(n + maxValue)
        {
            // Array of 0's at indices 0...maxValue
            int[] numCounts = new int[maxValue + 1];

            // Populate numCounts
            foreach (var num in theArray)
            {
                numCounts[num]++;
            }

            // Populate the final sorted array
            int[] sortedArray = new int[theArray.Length];
            int currentSortedIndex = 0;

            // For each num in numCounts
            for (int num = 0; num < numCounts.Length; num++)
            {
                int count = numCounts[num];

                // For the number of times the item occurs
                for (int i = 0; i < count; i++)
                {
                    // Add it to the sorted array
                    sortedArray[currentSortedIndex] = num;
                    currentSortedIndex++;
                }
            }

            return sortedArray;
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

                Console.WriteLine(string.Format("Low {0}, High{1}, Mid{2}", low, high, mid));

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
                Console.WriteLine(string.Format("Low {0}, High{1}, Mid{2}", low, high, mid));
            }
            return ++high;
        }


        public class Meeting
        {
            public int StartTime { get; set; }

            public int EndTime { get; set; }

            public Meeting(int startTime, int endTime)
            {
                // Number of 30 min blocks past 9:00 am
                StartTime = startTime;
                EndTime = endTime;
            }

            /*public override string Tostring()
            {
                return StartTime + "," + EndTime;
            }*/
        }

        /*
         * Meetings are random from different team, to use greedy approach, we need look at all other meetings, 
         * So first we them to sort them
         */
        public static List<Meeting> MergeMeetingRanges_Greedy(List<Meeting> meetings) //O(nLogN), space O(N)
        {
            // Make a copy so we don't destroy the input, and sort by start time
            var sortedMeetings = meetings.Select(m => new Meeting(m.StartTime, m.EndTime))
                .OrderBy(m => m.StartTime).ToList(); //O(nLogN)

            // Initialize mergedMeetings with the earliest meeting
            var mergedMeetings = new List<Meeting> { sortedMeetings[0] }; //O(N)

            foreach (var currentMeeting in sortedMeetings)
            {
                var lastMergedMeeting = mergedMeetings.Last();

                if (currentMeeting.StartTime <= lastMergedMeeting.EndTime)
                {
                    // If the current meeting overlaps with the last merged meeting, use the
                    // later end time of the two
                    lastMergedMeeting.EndTime =
                        Math.Max(lastMergedMeeting.EndTime, currentMeeting.EndTime);
                }
                else
                {
                    // Add the current meeting since it doesn't overlap
                    mergedMeetings.Add(currentMeeting);
                }
            }

            return mergedMeetings;
        }


        /*  Which Appears Twice. (Duplicate)
         *  Find the repeat number in an array of numbers. Optimize for runtime.
         *  ======================================
         *  I have an array of n + 1 numbers. Every number in the range 1..n appears once 
         *  except for one number that appears twice. All values are >0
         *  **** n=5, array has (n+1 = 6) elements, start from 1...5
         *  https://www.interviewcake.com/question/csharp/which-appears-twice?section=combinatorics-probability-math&course=fc1
         */
        public static int FindDuplicate_OptimizeRuntime(int[] numbers) //O(N) and O(1) memory.
        {
            if (numbers.Length < 2)
            {
                throw new ArgumentException("Finding duplicate requires at least two numbers",
                                            numbers.GetType().Name);
            }

            int n = numbers.Length - 1; // reducing 1 as there is 1 duplicate
            int sumWithoutDuplicate = (n * n + n) / 2; //Triangular series sum
            int actualSum = numbers.Sum(x => x); //O(N) to canculate sum

            return actualSum - sumWithoutDuplicate;
        }

        /*
         * Our approach is similar to a binary search, except we divide the range of possible answers in half at each step, 
         * rather than dividing the array in half.
         * =============
         * Find the number of integers in our input array which lie within the range 1.. n/2
         * ===========================
         * Compare that to the number of possible unique integers in the same range.
         * ============================
         * If the number of actual integers is greater than the number of possible integers, 
         * we know there’s a duplicate in the range 1..n/2, so we iteratively use the same approach on that range.
         * ================================
         * If the number of actual integers is not greater than the number of possible integers, 
         * we know there must be duplicate in the range n/2+1..n, so we iteratively use the same approach on that range.
         * =================================
         * At some point our range will contain just 1 integer, which will be our answer.
         */
        public static int FindDuplicate_OptimizeSpace(int[] numbers) //O(1) space and O(nlg{n}) time
        {
            int low = 1;
            int high = numbers.Length - 1;

            while (low < high)
            {
                // Divide our range 1..n into an upper range and lower range
                // (such that they don't overlap)
                // Lower range is low..mid
                // Upper range is mid+1..high
                int mid = low + (high - low) / 2;
                int lowerRangeFloor = low;
                int lowerRangeCeiling = mid;
                int upperRangeFloor = mid + 1;
                int upperRangeCeiling = high;

                // Count number of items in lower range
                int itemsInLowerRange = numbers.Count(item => item >= lowerRangeFloor && item <= lowerRangeCeiling);

                int distinctPossibleIntegersInLowerRange = lowerRangeCeiling - lowerRangeFloor + 1;

                if (itemsInLowerRange > distinctPossibleIntegersInLowerRange)
                {
                    // There must be a duplicate in the lower range
                    // so use the same approach iteratively on that range
                    low = lowerRangeFloor;
                    high = lowerRangeCeiling;
                }
                else
                {
                    // There must be a duplicate in the upper range
                    // so use the same approach iteratively on that range
                    low = upperRangeFloor;
                    high = upperRangeCeiling;
                }
            }

            // Floor and ceiling have converged
            // We found a number that repeats!
            return low;
        }

        //347. Top K Frequent Elements
        class TopKFrequentElements
        {
            //347. Top K Frequent Elements
            public static int[] Get(int[] nums, int k)
            {
                //if N == K
                // O(1) time -- this ensures that below logic runs less than O(n Log n)
                if (k == nums.Length)
                    return nums;


                //Build a Min Heap....
                Heap<int> heap = new Heap<int>(k + 1, true);

                // 1. build hash map : character and how often it appears
                // O(N) time
                Dictionary<int, int> count = new Dictionary<int, int>();
                for (int i = 0; i < nums.Length; i++)
                {
                    if (!count.ContainsKey(nums[i]))
                        count.Add(nums[i], 0);

                    count[nums[i]]++;
                }

                //O(n Log K) - for N items, with K push/pop operation 
                foreach (int key in count.Keys)
                {
                    heap.Push(new HeapNode<int>(count[key], key, 0));
                    if (heap.Count > k)
                    {
                        heap.Pop();
                    }
                }

                //O(K Log K) - popping k items
                IList<int> result = new List<int>();
                while (heap.Count > 0)
                {
                    result.Add(heap.Pop().r);
                }

                return result.ToArray();
            }
        }

        //215. Kth Largest Element in an Array
        class KthLargestElementInArray
        {
            //215. Kth Largest Element in an Array
            static public int FindKthLrgest(int[] nums, int k)
            {
                //Create a Min Heap *****
                Heap<int> heap = new Heap<int>(k + 1, true);
                for (int i = 0; i < nums.Length; i++)
                {
                    heap.Push(new HeapNode<int>(nums[i], 0, 0));
                    if (heap.Count > k)
                    {
                        heap.Pop();
                    }
                }

                return heap.Pop().val;
            }
        }

        //692. Top K Frequent Words
        class TopKFrequentWords
        {
            public static IList<string> TopKFrequent(string[] words, int k)
            {
                return null;
                /*
                //if N == K
                // O(1) time -- this ensures that below logic runs less than O(n Log n)
                if (k == words.Length)
                    return words;


                //Build a Min Heap....
                Heap heap = new Heap(k + 1, true);

                // 1. build hash map : character and how often it appears
                // O(N) time
                Dictionary<string, int> count = new Dictionary<string, int>();
                for (int i = 0; i < words.Length; i++)
                {
                    if (!count.ContainsKey(words[i]))
                        count.Add(words[i], 0);

                    count[words[i]]++;
                }

                //O(n Log K) - for N items, with K push/pop operation 
                foreach (string key in count.Keys)
                {
                    heap.Push(new HeapNode(count[key], key, 0));
                    if (heap.Count > k)
                    {
                        heap.Pop();
                    }
                }

                //O(K Log K) - popping k items
                IList<int> result = new List<int>();
                while (heap.Count > 0)
                {
                    result.Add(heap.Pop().r);
                }

                return result.ToArray();*/
            }
        }


        class MergeSort
        {
            public static void Sort()
            {
                int[] arr = new int[] { 12, 11, 13, 5, 6, 7 };
                MergeSortUtil(arr, 0, arr.Length - 1);
            }

            private static void MergeSortUtil(int[] arr, int l, int r)
            {
                if (r - l == 0) //only 1 element left
                    return;

                int m = l + (r - l) / 2;

                MergeSortUtil(arr, l, m);
                MergeSortUtil(arr, m + 1, r);

                //return Merge(Left, Right);
                int[] temp = new int[r - l + 1];
                int lp = l;
                int rp = m + 1;
                int k = 0;
                while (lp <= m && rp <= r)
                {
                    if (arr[lp] <= arr[rp])
                    {
                        temp[k++] = arr[lp++];
                    }
                    else
                    {
                        temp[k++] = arr[rp++];
                    }
                }

                while (lp <= m)
                {
                    temp[k++] = arr[lp++];
                }
                while (rp <= r)
                {
                    temp[k++] = arr[rp++];
                }

                for (int i = 0; i < temp.Length; i++)
                    arr[l++] = temp[i];
            }

        }

        //1588. Sum of All Odd Length Subarrays
        class SumofAllOddLengthSubarrays
        {
            public static int SumOddLengthSubarrays(int[] arr)
            {
                int l = 0; int r = 0;
                int total = 0;
                while (true)
                {
                    int sum = 0;
                    while (l < arr.Length && r < arr.Length)
                    {
                        sum += arr[r];
                        if ((r - l + 1) % 2 != 0)
                        {
                            total += sum;
                            Console.WriteLine(sum);
                        }

                        r++;
                    }

                    l++;
                    r = l;

                    if (l >= arr.Length)
                        break;
                }

                return total;
            }
        }

        //905. Sort Array By Parity
        class SortArrayByParity
        {
            //905. Sort Array By Parity
            static bool isEven(int val)
            {
                return val % 2 == 0;
            }

            public static int[] EvenOddGroup(int[] arr)
            {
                if (arr.Length < 2)
                    return arr;

                int oddP = 0;
                int evenP = arr.Length - 1;

                while (oddP < evenP)
                {
                    //We will SWAP values, when we find a ODD value on left side and EVEN value on right side


                    //from left - go to first ODD item
                    while (oddP < arr.Length && isEven(arr[oddP]))
                        oddP++;

                    //from right - go to first EVEN item
                    while (evenP >= 0 && !isEven(arr[evenP]))
                        evenP--;

                    if (oddP > evenP)
                        break;

                    //swap
                    int temp = arr[oddP];
                    arr[oddP] = arr[evenP];
                    arr[evenP] = temp;

                    //reduce the window
                    oddP++;
                    evenP--;
                }

                return arr;
            }

        }

        //922. Sort Array By Parity II
        class SortArrayByParityII
        {
            public static int[] SortArrayByParity2(int[] arr)
            {
                if (arr.Length < 2)
                    return arr;

                int winLeft = 0;
                int winRight = 0;

                while (winLeft < arr.Length && winRight < arr.Length)
                {
                    //Sliding window -
                    //if we the find disprecency (Type 1. 'even value' in odd index / Type 2. 'odd value' in even index)? 
                    //then grow the window till we get the first fix 
                    //  for Type '1' - find Odd value at even index, 
                    //  for Type '2' - find even value at odd index
                    //Swap the value

                    if (winLeft % 2 == 0) //even
                    {
                        if (arr[winLeft] % 2 != 0) //found odd value, on Even Index
                        {
                            //expand window to find next EVEN Value at Odd index, so we can swap
                            while (winRight < arr.Length && !(arr[winRight] % 2 == 0 && winRight % 2 != 0))
                                winRight++;

                            //swap
                            int temp = arr[winRight];
                            arr[winRight] = arr[winLeft];
                            arr[winLeft] = temp;
                        }
                    }
                    else //odd
                    {
                        if (arr[winLeft] % 2 == 0) //found even value
                        {
                            //expand window to find next ODD value in EVEN index
                            while (winRight < arr.Length && !(arr[winRight] % 2 != 0 && winRight % 2 == 0))
                                winRight++;

                            //swap
                            int temp = arr[winRight];
                            arr[winRight] = arr[winLeft];
                            arr[winLeft] = temp;
                        }
                    }

                    winLeft++;
                }

                return arr;
            }
        }

        //Leetcode – 23. Merge k Sorted Lists
        class MergeKSortedArray
        {
            //Leetcode – 23. Merge k Sorted Lists
            public static int[] MergeKSrtedArray(int[][] arr)
            {
                bool descending = false;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i][0] > arr[i][arr[i].Length - 1])
                    {
                        descending = true;
                        break;
                    }
                }

                int[] result = new int[arr.Length * arr[0].Length];

                HeapNode<int>[] hArr = new HeapNode<int>[arr.Length];
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    hArr[i] = new HeapNode<int>(arr[i][0], i, 0);
                }
                //build the min heap
                Heap<int> heap = new Heap<int>(arr.Length, !descending); //true - MinHeap, False - MaxHeap
                heap.BuildHeap(hArr);

                int r = 0;
                while (heap.Count > 0)
                {
                    HeapNode<int> root = heap.Top();
                    result[r++] = root.val;

                    heap.Pop();

                    //insert next item from same row
                    if ((root.c + 1) < arr[root.r].Length)
                    {
                        root.val = arr[root.r][root.c + 1];
                        root.c = root.c + 1;

                        heap.Push(root);
                    }
                }

                return result;
            }
        }

        //670. Maximum Swap
        class MaximumValueAfterSwap
        {
            public int MaximumSwap(int num)
            {
                return 0;
            }
        }

        //280. Wiggle Sort
        class WiggleSort
        {
            public void Sort(int[] nums)
            {

            }
        }

        class WiggleSortII
        {
            public void Sort(int[] nums)
            {

            }
        }

        //1326. Minimum Number of Taps to Open to Water a Garden
        class MinTapsToOpenToWaterAGarden
        {
            public static int MinTaps(int n, int[] ranges)
            {
                //O(N^2)

                int[] min = new int[n + 1];
                int[] max = new int[n + 1];

                for (int i = 0; i <= n; i++)
                {
                    int l = (i - ranges[i]) < 0 ? 0 : i - ranges[i];
                    int r = i + ranges[i];

                    if (l <= r)
                    {
                        min[i] = l;
                        max[i] = r;
                    }
                    else
                    {
                        min[i] = r;
                        max[i] = l;
                    }
                }

                int maxValIndex = GetMaxWithMaxMinIndex(max, min, n);

                if (maxValIndex == -1)
                    return -1;

                int minRequired = 1;
                int maxMinVal = min[maxValIndex];
                while (maxMinVal > 0)
                {
                    minRequired++;

                    int originalmaxMinVal = maxMinVal;
                    int maxVal = min[maxValIndex];
                    for (int i = maxValIndex - 1; i >= 0; i--)
                    {
                        if (max[i] >= maxVal && min[i] <= maxMinVal)
                        {
                            maxValIndex = i;
                            maxMinVal = min[i];
                        }
                    }

                    if (originalmaxMinVal == maxMinVal)
                        return -1;
                }

                return minRequired;
            }

            private static int GetMaxWithMaxMinIndex(int[] max, int[] min, int n)
            {
                int maxValIndex = -1;
                int minMost = int.MaxValue;
                for (int i = 0; i < max.Length; i++)
                {
                    if (max[i] >= (n) && min[i] <= minMost)
                    {
                        maxValIndex = i;
                        minMost = min[i];
                    }
                }
                return maxValIndex;
            }
        }

        class DutchNationalFlag
        {
            public static void dutch_flag_sort(List<char> balls)
            {
                int Rp = 0;
                int Bp = balls.Count - 1;

                int i = 0;
                while (i <= Bp)
                {
                    if (balls[i] == 'R')
                    {
                        char temp = balls[Rp];
                        balls[Rp] = balls[i];
                        balls[i] = temp;
                        Rp++;

                        i++;
                    }
                    else if (balls[i] == 'B')
                    {
                        char temp = balls[Bp];
                        balls[Bp] = balls[i];
                        balls[i] = temp;
                        Bp--;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        //121. Best Time to Buy and Sell Stock
        class BestTimetoBuyandSellStock
        {
            public int MaxProfit(int[] prices)
            {
                //buy and sell once
                //Start with Buy at 0...
                //Traverse list- if you find a NEW ***cheaper*** buy price then buy before at that price, and calcluate profit

                int bestBuyAt = 0; //Assume lowest price to buy
                int maxProfit = 0;
                for (int i = 1; i < prices.Length; i++)
                {
                    //New Cheaper price to buy available
                    if (prices[bestBuyAt] > prices[i])
                        bestBuyAt = i; //buy it
                    else //prices are going up... calculate profit
                        maxProfit = Math.Max(maxProfit, prices[i] - prices[bestBuyAt]);
                }

                return maxProfit;
            }
        }

        //122. Best Time to Buy and Sell Stock II
        class BestTimetoBuyandSellStockII
        {
            public int MaxProfit(int[] prices)
            {
                //buy and sell MULTIPLE times
                //Start with Buy at 0...
                //Traverse list- whenever you see, price went down, then Buy at that price... sell when the price goes up

                int bestBuyAt = 0; //Assume lowest price to buy
                int totalProfit = 0;
                int profit = 0;
                for (int i = 1; i < prices.Length; i++)
                {
                    //buy when price goes down
                    if (prices[i - 1] > prices[i])
                    {
                        //merge previous profit into total profit
                        totalProfit += profit;
                        //reset previous profit as we find next position to buy
                        profit = 0;
                        //new buy 
                        bestBuyAt = i;
                    }
                    else //sell when price goes up
                    {
                        //calculate best profit, from the buy position
                        profit = Math.Max(profit, prices[i] - prices[bestBuyAt]);
                    }
                }

                //For any profit we didnt merge into total profit
                if (profit > 0)
                    totalProfit += profit;

                return totalProfit;
            }
        }

        //123. Best Time to Buy and Sell Stock III
        class BestTimetoBuyandSellStockIII
        {
            public static int MaxProfit(int[] prices)
            {
                //Buy/sell ATMOST 2 times
                //DP ???... 

                //Our problem is to split the array at an Index, where left and right side of array gives us max profit
                int[] left = new int[prices.Length];
                int[] right = new int[prices.Length];

                //Calculating profit from LeftSide
                //Code from BestTimetoBuyandSellStock
                int bestBuyAt = 0; //Assume lowest price to buy
                int maxProfit = 0;
                for (int i = 1; i < prices.Length; i++)
                {
                    //New Cheaper price to buy available
                    if (prices[bestBuyAt] > prices[i])
                        bestBuyAt = i; //buy it
                    else //prices are going up... calculate profit
                        maxProfit = Math.Max(maxProfit, prices[i] - prices[bestBuyAt]);

                    left[i] = maxProfit;
                }

                //Calculating profit from BackSide
                int bestSellAt = prices.Length - 1; //Assume highest price to sell
                maxProfit = 0;
                for (int i = prices.Length - 2; i >= 0; i--)
                {
                    //New Cheaper price to buy available, calculate the profit
                    if (prices[bestSellAt] > prices[i])
                        maxProfit = Math.Max(maxProfit, prices[bestSellAt] - prices[i]);
                    else //best higher price to sell is available
                        bestSellAt = i; //update sell pointer

                    right[i] = maxProfit;
                }

                int totalProfit = 0;
                for (int i = 0; i < prices.Length; i++)
                    totalProfit = Math.Max(totalProfit, left[i] + right[i]);

                return totalProfit;
            }
        }

        //188. Best Time to Buy and Sell Stock IV
        class BestTimetoBuyandSellStockIV
        {
            public int MaxProfit(int k, int[] prices)
            {
                //Buy/sell MANY times
                return 0;
            }
        }

        //309. Best Time to Buy and Sell Stock with Cooldown
        class BestTimetoBuyandSellStockwithCooldown
        {
            public static int MaxProfit(int[] prices)
            {
                //buy and sell MULTIPLE times, with COOLDown
                //Start with Buy at 0...
                //Traverse list- whenever you see, price went down, then Buy at that price... sell when the price goes up


                return 0;
            }
        }

        //48. Rotate Image
        class RotateImage
        {
            //Rotate by 90
            public static void Rotate(int[][] matrix)
            {
                //phase 1 - Swap columns
                for (int r = 0; r < matrix.Length; r++)
                {
                    for (int c = 0; c < matrix[0].Length / 2; c++)
                    {
                        int temp = matrix[r][matrix[0].Length - c - 1];
                        matrix[r][matrix[0].Length - c - 1] = matrix[r][c];
                        matrix[r][c] = temp;
                    }
                }

                //phase 2 - SWAP top row and last column
                for (int r = 0; r < matrix.Length; r++)
                {
                    for (int c = 0; c < matrix[0].Length - r; c++)
                    {
                        int temp = matrix[r][c];
                        matrix[r][c] = matrix[matrix.Length - 1 - c][matrix[0].Length - 1 - r];
                        matrix[matrix.Length - 1 - c][matrix[0].Length - 1 - r] = temp;
                    }
                }
            }
        }

        //81. Search in Rotated Sorted Array II
        class SearchInRotatedArray {
            public static bool Search(int[] nums, int target)
            {
                int l = 0;
                int r = nums.Length - 1;
                while (l <= r)
                {
                    int mid = l + (r - l) / 2;

                    if (nums[mid] == target)
                        return true;

                    if (nums[l] == nums[mid])
                    {
                        l++;
                        continue;
                    }
                    if (nums[r] == nums[mid])
                    {
                        r--;
                        continue;
                    }

                    //right side sorted
                    if (nums[mid] <= nums[r])
                    {
                        //right side is sorted 
                        if (target > nums[mid] && target <= nums[r])
                            l = mid + 1;
                        else
                            r = mid - 1;
                    }
                    else //if (nums[l] <= nums[mid]) //left side is sorted
                    {
                        if (target >= nums[l] && target < nums[mid])
                            r = mid - 1;
                        else
                            l = mid + 1;
                    }

                }
                return false;
            }
        }

        //1385. Find the Distance Value Between Two Arrays
        class FindtheDistanceValueBetweenTwoArrays
        {
            public static int FindTheDistanceValue(int[] arr1, int[] arr2, int d)
            {
                return 0;
            }
        }

        //564. Find the Closest Palindrome
        class FindtheClosestPalindrome
        {
            public static string NearestPalindromic(string n)
            {
                if (n.Equals("1"))
                {
                    return "0";
                }

                //just mirror the left side string
                string a = mirroring(n);
                long diff1 = long.MaxValue;
                diff1 = Math.Abs(long.Parse(n) - long.Parse(a));
                if (diff1 == 0)
                {
                    diff1 = long.MaxValue;
                }

                //if middle index number is 0, keep replace the it with 9 -- trying to reduce the number
                //n=89->79,n=99->89,n=20->10, n=19,18...10 -> 9*, n=9....2->(i-1)
                StringBuilder s = new StringBuilder(n);
                int mid = (s.Length - 1) / 2;
                while (mid >= 0 && s[mid] == '0')
                {
                    s.Replace(s[mid], '9', mid, 1);
                    mid--;
                }
                //if the first value is '1', remove it, and put '9' on next 
                if (mid == 0 && s[mid] == '1')
                {
                    s.Remove(0, 1);
                    mid = (s.Length - 1) / 2;
                    s.Replace(s[mid], '9', mid, 1);
                }
                else
                { 
                    //reduce the value by 1
                    s.Replace(s[mid], (char)(s[mid] - 1), mid, 1);
                }
                string b = mirroring(s.ToString());
                long diff2 = Math.Abs(long.Parse(n) - long.Parse(b));

                //if middle index number is 9, keep replace the it with 0-- trying to increase the number
                //n=89->99,n=99->109,n=20->30, n=19,18...10 -> 2*, n=9....2->(i+1)
                s = new StringBuilder(n);
                mid = (s.Length - 1) / 2;
                while (mid >= 0 && s[mid] == '9')
                {
                    s.Replace(s[mid], '0', mid, 1);
                    mid--;
                }

                if (mid < 0)
                {
                    s.Insert(0, "1");
                }
                else
                {
                    s.Replace(s[mid], (char)(s[mid] + 1), mid, 1);
                }
                string c = mirroring(s.ToString());
                long diff3 = Math.Abs(long.Parse(n) - long.Parse(c));

                if (diff2 <= diff1 && diff2 <= diff3)
                {
                    return b;
                }
                if (diff1 <= diff3 && diff1 <= diff2)
                {
                    return a;
                }
                else
                {
                    return c;
                }
            }

            public static string mirroring(string s)
            {
                string x = s.Substring(0, (s.Length) / 2);
                return x + (s.Length % 2 == 1 ? s[s.Length / 2].ToString() : "") + Reverse(x);
            }

            private static string Reverse(string s)
            {
                char[] chars = s.ToCharArray();
                Array.Reverse(chars);
                return new string(chars);
            }
        }

        //239. Sliding Window Maximum
        class MaxValueInSlidingWindow
        {
            //https://leetcode.com/problems/sliding-window-maximum/
            public static int[] GetMax(int[] nums, int winSize)
            {
                //O(N) - space O(winSize)

                //https://leetcode.com/problems/sliding-window-maximum/discuss/435402/C-mock-interview-performance-with-code-review-in-2019

                //list will maintain, Descending order value indicies
                //Always add on the last side...
                //if the last value is bigger than current, as it direct
                //if the last value is small than current, keep on removing from last till list is empty or we find a bigger one

                if (nums == null || nums.Length == 0)
                {
                    return new int[0];
                }

                var deque = new LinkedList<int>();

                for (int i = 0; i < winSize; i++)
                {
                    var current = nums[i];
                    //if the last value is bigger than current, as it direct
                    if (deque.Count == 0 || nums[deque.Last.Value] > current)
                    {
                        deque.AddLast(i);
                    }
                    else
                    {
                        //if the last value is small than current, keep on removing from last till list is empty 
                        //or we find a bigger one
                        while (deque.Count > 0 && nums[deque.Last.Value] < current)
                        {
                            deque.RemoveLast();
                        }

                        deque.AddLast(i);
                    }
                }

                IList<int> max = new List<int>();
                max.Add(nums[deque.First.Value]);

                //if Max goes out of window, remove it from first/left
                if (deque.First.Value == 0)
                {
                    deque.RemoveFirst();
                }

                for (int i = 1; i < nums.Length - winSize + 1; i++)
                {
                    int winRightBoundaryIdx = i + winSize - 1;

                    var current = nums[winRightBoundaryIdx];

                    //if the last value is bigger than current, as it direct on the right/last
                    if (deque.Count == 0 || nums[deque.Last.Value] > current)
                    {
                        deque.AddLast(winRightBoundaryIdx);
                    }
                    else
                    {
                        //if the last value is small than current, keep on removing from last till list is empty 
                        //or we find a bigger one 
                        //and add it on the right/last
                        while (deque.Count > 0 && nums[deque.Last.Value] < current)
                        {
                            deque.RemoveLast();
                        }

                        deque.AddLast(winRightBoundaryIdx);
                    }

                    max.Add(nums[deque.First.Value]);

                    //if Max goes out of window, remove it from first/left
                    if (deque.First.Value == i)
                    {
                        deque.RemoveFirst();
                    }
                }

                return max.ToArray();
            }

        }

        //857. Minimum Cost to Hire K Workers
        class MincostToHireWorkers
        {
            //https://leetcode.com/problems/minimum-cost-to-hire-k-workers/
            public static double GetMincost(int[] quality, int[] wage, int k)
            {
                //https://leetcode.com/problems/minimum-cost-to-hire-k-workers/discuss/141768/Detailed-explanation-O(NlogN)

                // The key to the solution is that Kth ratio (wage / quality) can only satisfy workers with a smaller ratio. 
                // Otherwise, it will break the minimum expectation constraint, since every worker will be sharing the same ratio. 
                // However, you cannot simply pick the first Kth workers. Since if the sum of the quality is super high, 
                // then we would rather have a higher ratio but smaller sum of quality. 
                // Therefore, we are looking for the minimum sum of quality * ratio. Following is how the algorithm works:

                // 1. Create an array that stores the ratio (worker[0]) and quality (worker[1]). 
                //     We don't care about the min wage expectation anymore, 
                //     since we are only looking at the workers with a ratio that is smaller than any current ratio, 
                //     and will always satisfy the minimum constraint.
                // 2. Sort the array and start from Kth smallest ratio. 
                //     Otherwise, there won't be enough workers that satisfy with the current ratio (minimum constraint breaks)
                // 3. Keep increase the current ratio with the next worker and find the smallest sum, since both the sum of quality and ratio impacts the result
                // 4. Keep removing the largest quality, since we want the smallest sum of quality and the smallest ratio possible

                long MOD = (long)(1000000000 + 7);

                double[][] workers = new double[wage.Length][];
                for (int i = 0; i < wage.Length; i++)
                {
                    workers[i] = new double[2];
                    workers[i][0] = (double) wage[i] / quality[i];   //ratio
                    workers[i][1] = (double) quality[i];                     //quality
                }

                //sort by ratio (ascending)
                Array.Sort(workers, new AscendingComparer());

                //Create a Max Heap for large quality, Keep removing the largest quality, 
                //since we want the smallest sum of quality and the smallest ratio possible
                Heap<double> pq = new Heap<double>(k + 1, false);

                double result = double.MaxValue, qSum = 0;
                for (int i = 0; i < workers.Length; i++)
                {                    
                    qSum += workers[i][1];
                    pq.Push(new HeapNode<double>(workers[i][1], 0, 0));
                 
                    // if we have more than k workers, remove the one with the largest quality
                    if (pq.Count > k)
                    {
                        qSum -= pq.Pop().val;
                    }
                    if (pq.Count == k)
                    {
                        result = Math.Min(result, qSum * workers[i][0]);
                    }                    
                }

                return (int)(result % MOD);
            }

            class AscendingComparer : IComparer<double[]>
            {
                public int Compare(double[] x, double[] y)
                {
                    return x[0].CompareTo(y[0]);
                }
            }
        }
        
        //1383. Maximum Performance of a Team
        class MaximumPerformanceOfATeam
        {
            //https://leetcode.com/problems/maximum-performance-of-a-team/

            //https://leetcode.com/problems/maximum-performance-of-a-team/discuss/942044/JAVA-PriorityQueue-O(N-log-N)-lots-of-comments
            //https://leetcode.com/problems/maximum-performance-of-a-team/discuss/595185/Faster-than-88-using-PQ-JAVA
            public static int MaxPerfAtmostKEngineers(int n, int[] speed, int[] efficiency, int k)
            {
                //Time O(NlogN) for sorting
                //Time O(NlogK) for priority queue
                //Space O(N)


                //The main idea is to sort in descending order of efficiency, and fix an efficiency 
                //from top to bottom at a time, so that the sum of the corresponding speed is as 
                //large as possible (that is, the number of people is selected as much as possible), 
                //if the number of people exceeds k, the engineer with smallest speed is deleted ( Use the smallest heap to process), 
                //and constantly update the performance to get the answer.

                //*************************************************
                //Sort efficiency with descending order. Because, afterwards, when we iterate whole engineers, every round, 
                //when calculating the current performance, minimum efficiency is the effiency of the new incoming engineer.
                //*************************************************

                long MOD = (long)(1000000000 + 7);

                int[][] es = new int[speed.Length][];
                for (int i = 0; i < es.Length; i++)
                {
                    es[i] = new int[2];
                    es[i][0] = efficiency[i];
                    es[i][1] = speed[i];
                }

                //highest to lowest efficiency, since we need instant access to current lowest efficiency while processing performance
                Array.Sort(es, new DescendingComparer());

                //Create a Min Heap for slow speed engineers, so we can remove slow speed engineers
                Heap<int> pq = new Heap<int>(k, true);

                long perf = 0, speedsum = 0;
                for (int i = 0; i < es.Length; i++)
                {
                    if (pq.Count == k)
                    {
                        //remove min speed (efficiency won't be affected)
                        //Evict the slowest engineer from out current team
                        speedsum -= pq.Pop().val;
                    }

                    //we are hiring a new engineer, either he is replacing someone as in condition above ^ or he's just a newcomer.
                    //out team is still small (< k)

                    //Add new/replace existing engineer and new calculate the new speed
                    pq.Push(new HeapNode<int>(es[i][1], 0, 0));

                    speedsum += es[i][1];

                    perf = Math.Max(perf, speedsum * es[i][0]);
                }

                return (int)(perf % MOD);
            }

            class DescendingComparer : IComparer<int[]>
            {
                public int Compare(int[] x, int[] y)
                {
                    return y[0].CompareTo(x[0]);
                }
            }
        }

        //https://www.geeksforgeeks.org/minimum-number-platforms-required-railwaybus-station/
        class CountMinimumPlatformRequired
        {
            class AscendingComparer : IComparer<int[]>
            {
                public int Compare(int[] x, int[] y)
                {
                    return x[0].CompareTo(y[0]);
                }
            }
            public static int Count(int[] arr, int[] dep, int n)
            {
                //Time Complexity: O(nlogn).

                // Sort arrival and departure arrays
                Array.Sort(arr);
                Array.Sort(dep);

                // plat_needed indicates number of platforms needed at a time 
                int plat_needed = 1, result = 1;
                int i = 1, j = 0;

                // Similar to merge in merge sort to process all events in sorted order 
                while (i < n && j < n)
                {
                    // If next event in sorted order is arrival, increment count of platforms needed 
                    if (arr[i] <= dep[j])
                    {
                        plat_needed++;
                        i++;
                    }

                    // Else decrement count of platforms needed 
                    else if (arr[i] > dep[j])
                    {
                        plat_needed--;
                        j++;
                    }

                    // Update result if needed 
                    if (plat_needed > result)
                        result = plat_needed;
                }

                return result;

                /*
                int[][] schedule = new int[n][];
                for(int k = 0; k <n; k ++)
                {
                    schedule[k] = new int[2];
                    schedule[k][0] = arr[k];
                    schedule[k][1] = dep[k];
                }

                Array.Sort(schedule, new AscendingComparer());

                int count_platfrm = 0;
                int Min_platfrm_required = 0;
                for (int k = 0; k < n; k++)
                {
                    if (k < n - 1)
                    {
                        if (schedule[k][1] < schedule[k + 1][0])
                        {
                            count_platfrm++;
                            Min_platfrm_required = Math.Max(Min_platfrm_required, count_platfrm);

                            //train departed... platform free, reduce count
                            if (count_platfrm > 0)
                                count_platfrm--;
                        }
                        else
                        {
                            schedule[k + 1][1] = Math.Max(schedule[k + 1][1], schedule[k][1]);
                            count_platfrm++;
                            Min_platfrm_required = Math.Max(Min_platfrm_required, count_platfrm);
                        }
                    }
                    else //for last schedule, reduce platform count
                    {
                        if (count_platfrm > 0)
                            count_platfrm--;
                    }
                }
                
                return Min_platfrm_required;*/

            }
        }

        public static void runTest()
        {
            //FindtheClosestPalindrome.NearestPalindromic("14");

            MincostToHireWorkers.GetMincost(new int[] { 10, 20, 5}, new int[]{70,50,30},2);

            //CountMinimumPlatformRequired.Count(new int[] { 900, 940, 950, 1100, 1500, 1800 }, new int[] { 910, 1200, 1120, 1130, 1900, 2000 }, 6);

            MaximumPerformanceOfATeam.MaxPerfAtmostKEngineers(6, new int[] { 2, 10, 3, 1, 5, 8 }, new int[] { 5, 4, 3, 9, 7, 2 }, 2);
            
           // MaxValueInSlidingWindow.GetMax(new int[] { 1, 3, -1, -3, 5, 3, 6, 7 }, 3);
            MaxValueInSlidingWindow.GetMax(new int[] { 7157,9172,7262,-9146,3087,5117,4046,7726,-1071,6011,5444,-48,-1385,-7328,3255,1600,586,-5160,-371,-5978,9837,3255,-6137,8587,-3403,9775,260,6016,9797,3371,2395}, 5);

            

            SearchInRotatedArray.Search(new int[] { 1, 1, 2, 1, 1,1,1,1,1,1,1,1 }, 2);

            BestTimetoBuyandSellStockwithCooldown.MaxProfit(new int[] { 1, 2, 3, 0, 2 });

            SortArrayByParity.EvenOddGroup(new int[] { 2, 2, 4, 2, 8, 4 });

            //KthLargestElementInArray.FindKthLrgest(new int[] { 3, 2, 1, 5, 6, 4 }, 2);
            TopKFrequentElements.Get(new int[] { 1,2 }, 2);
            MinTapsToOpenToWaterAGarden.MinTaps(3, new int[] { 0, 0, 0, 0 });

            //dutch_flag_sort(new List<char>() { 'G', 'B', 'G', 'G', 'R', 'B', 'R', 'G' });

            int[][] arr = new int[9][] {
                new int[] {43,43,37,34,25,25,23,21,12,5},
                new int[] {53,45,44,37,35,29,25,19,15,9},
                new int[] {43,41,36,28,24,18,15,13,6,2},
                new int[] {46,37,36,28,23,17,15,15,8,6},
                new int[] {49,43,38,37,33,29,20,11,10,2},
                new int[] {53,49,44,36,33,25,24,19,13,4},
                new int[] {43,39,31,29,26,26,22,18,13,8},
                new int[] {52,47,42,40,36,36,27,22,13,4},
                new int[] {57,48,43,36,36,36,28,20,14,5},
            };
            MergeKSortedArray.MergeKSrtedArray(arr);

            SortArrayByParity.EvenOddGroup(new int[] { 1,1 });
            //TopK( new int[] { 1, 5, 1, 5, 1 }, 3);

            var l= getShortestTransformationIterative("hit", "cog", new HashSet<string>()
            {
                "hot","dot","dog","lot","log","cog"
            });

            //CanTwoMoviesFillFlight(new int[] { 1, 6, 3, 1, 3, 6, 6 }, 6);

            //WordCloudData v=new WordCloudData("Sudip is a good boy, sudip has a car. Car was red in color.");

            int sortedScores = FindDuplicate_OptimizeSpace(new[] { 1, 2, 3, 4, 5, 5 });
        }
    }

}
