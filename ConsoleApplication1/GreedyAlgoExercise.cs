using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
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

        public override string ToString()
        {
            return StartTime + "," + EndTime;
        }
    }

    public class GreedyAlgoExercise
    {
        public int GetMaxProfit_BruteForce(int[] stockPrices) //O(n^2) time  -- Follow Greeedy Algo
        {
            int maxProfit = 0;

            // Go through every price and time
            for (int earlierTime = 0; earlierTime < stockPrices.Length; earlierTime++)
            {
                int earlierPrice = stockPrices[earlierTime];

                // And go through all the LATER prices
                for (int laterTime = earlierTime + 1; laterTime < stockPrices.Length; laterTime++)
                {
                    int laterPrice = stockPrices[laterTime];

                    // See what our profit would be if we bought at the
                    // min price and sold at the current price
                    int potentialProfit = laterPrice - earlierPrice;

                    // Update maxProfit if we can do better
                    maxProfit = Math.Max(maxProfit, potentialProfit);
                }
            }

            return maxProfit;
        }

        public int GetMaxProfit_GreedyApproach1(int[] stockPrices) //O(n) time and O(1) space, but BUG is there, maxProfit is hard coded to 0
        {
            int minPrice = stockPrices[0];
            int maxProfit = 0;

            /*
             * a greedy approach, where we keep a running maxProfit until we reach the end
             * How? keep track of the lowest price we’ve seen so far, see if we can get a better profit
             */

            foreach (int currentPrice in stockPrices)
            {
                // Ensure minPrice is the lowest price we've seen so far
                minPrice = Math.Min(minPrice, currentPrice);

                // See what our profit would be if we bought at the
                // min price and sold at the current price
                int potentialProfit = currentPrice - minPrice;

                // Update maxProfit if we can do better
                maxProfit = Math.Max(maxProfit, potentialProfit);
            }

            return maxProfit;
        }

        public static int GetMaxProfit_GreedyApproach2(int[] stockPrices) //O(n) time and O(1) space. We only loop through the array once.
        {
            if (stockPrices.Length < 2)
            {
                throw new ArgumentException("Getting a profit requires at least 2 prices", stockPrices.GetType().Name);
            }

            // We'll greedily update minPrice and maxProfit, so we initialize
            // them to the first price and the first possible profit
            int minPrice = stockPrices[0];
            int maxProfit = stockPrices[1] - stockPrices[0];

            // Start at the second (index 1) time.
            // We can't sell at the first time, since we must buy first,
            // and we can't buy and sell at the same time!
            // If we started at index 0, we'd try to buy *and* sell at time 0.
            // This would give a profit of 0, which is a problem if our 
            // maxProfit is supposed to be *negative* --we'd return 0.
            for (int i = 1; i < stockPrices.Length; i++)
            {
                int currentPrice = stockPrices[i];

                // See what our profit would be if we bought at the
                // min price and sold at the current price
                int potentialProfit = currentPrice - minPrice;

                // Update maxProfit if we can do better
                maxProfit = Math.Max(maxProfit, potentialProfit);

                // Update minPrice so it's always
                // the lowest price we've seen so far
                minPrice = Math.Min(minPrice, currentPrice);
            }

            return maxProfit;
        }


        public static int HighestProductOf3_Greedy(int[] arrayOfInts) //O(n) time and O(1) space
        {
            /*
             * Keep track of the highestProductOf2 and lowestProductOf2 (could be a low negative number). 
             * If the current number times one of those is higher than the current highestProductOf3, we have a new highestProductOf3!
             * ======================================================================
             * How do we keep track of the highestProductOf2 and lowestProductOf2 at each iteration? 
             * (Hint: we may need to also keep track of something else.)
             * We also keep track of the lowest number and highest number. ***
             * If the current number times the current highest—or the current lowest, 
             * if current is negative—is greater than the current highestProductOf2, 
             * we have a new highestProductOf2. Same for lowestProductOf2. 
             * =======================================================================
             * So at each iteration we're keeping track of and updating:
                highestProductOf3
                highestProductOf2
                highest
                lowestProductOf2
                lowest
             */


            if (arrayOfInts.Length < 3)
            {
                throw new ArgumentException("Less than 3 items!", arrayOfInts.GetType().Name);
            }

            // We're going to start at the 3rd item (at index 2)
            // so pre-populate highests and lowests based on the first 2 items.
            // We could also start these as null and check below if they're set
            // but this is arguably cleaner
            int highest = Math.Max(arrayOfInts[0], arrayOfInts[1]);
            int lowest = Math.Min(arrayOfInts[0], arrayOfInts[1]);

            int highestProductOf2 = arrayOfInts[0] * arrayOfInts[1];
            int lowestProductOf2 = arrayOfInts[0] * arrayOfInts[1];

            // Except this one--we pre-populate it for the first *3* items.
            // This means in our first pass it'll check against itself, which is fine.
            int highestProductOf3 = arrayOfInts[0] * arrayOfInts[1] * arrayOfInts[2];

            // Walk through items, starting at index 2
            for (int i = 2; i < arrayOfInts.Length; i++)
            {
                int current = arrayOfInts[i];

                // Do we have a new highest product of 3?
                // It's either the current highest,
                // or the current times the highest product of two
                // or the current times the lowest product of two
                highestProductOf3 = Math.Max(Math.Max(
                    highestProductOf3,
                    current * highestProductOf2),
                    current * lowestProductOf2);

                // Do we have a new highest product of two?
                highestProductOf2 = Math.Max(Math.Max(
                    highestProductOf2,
                    current * highest),
                    current * lowest);

                // Do we have a new lowest product of two?
                lowestProductOf2 = Math.Min(Math.Min(
                    lowestProductOf2,
                    current * highest),
                    current * lowest);

                // Do we have a new highest?
                highest = Math.Max(highest, current);

                // Do we have a new lowest?
                lowest = Math.Min(lowest, current);
            }

            return highestProductOf3;
        }


        /*
         * The product of all the integers except the integer at each index can be broken down into two pieces:
            the product of all the integers before each index MULTIPLE
            the product of all the integers after each index.
         * 
         * ========================
         * But what if the input array has fewer than two integers?
         * Well, there won't be any products to return because at any index there are no “other” integers. So let's throw an exception.
         * */
        public static int[] GetProductsOfAllIntsExceptAtIndex_Greedy(int[] intArray) //O(n) time and O(n) space
        {
            if (intArray.Length < 2)
            {
                throw new ArgumentException(
                    "Getting the product of numbers at other indices requires at least 2 numbers",
                    intArray.GetType().Name);
            }

            // We make an array with the length of the input array to
            // hold our products
            int[] productsOfAllIntsExceptAtIndex = new int[intArray.Length];

            // For each integer, we find the product of all the integers
            // before it, storing the total product so far each time
            int productSoFar = 1;
            for (int i = 0; i < intArray.Length; i++)
            {
                productsOfAllIntsExceptAtIndex[i] = productSoFar;
                productSoFar *= intArray[i];
            }

            // For each integer, we find the product of all the integers
            // after it. since each index in products already has the
            // product of all the integers before it, now we're storing
            // the total product of all other integers
            productSoFar = 1;
            for (int i = intArray.Length - 1; i >= 0; i--)
            {
                productsOfAllIntsExceptAtIndex[i] *= productSoFar;
                productSoFar *= intArray[i];
            }

            return productsOfAllIntsExceptAtIndex;
        }




        /*
         * If shuffledDeck is a riffle of half1 and half2, then the first card from shuffledDeck should be either 
         * the same as the first card from half1 or the same as the first card from half2. Let's "throw out" the top card 
         * from shuffledDeck as well as the card it matched with from the top of half1 or half2. 
         * Those cards are now "accounted for."
         */
        public static int[] RemoveTopCard(int[] cards) 
        {
            int[] result = new int[cards.Length - 1];
            if (result.Length > 0)
            {
                Array.Copy(cards, 1, result, 0, result.Length);
            }
            return result;
        }

        /* O(n^2) time and O(n^2) space
        * ========================================
        * In our recursing we'll build up n frames on the call stack. Each of those frames will hold a different slice 
        * of our original shuffledDeck (and half1 and half2, though we only slice one of them in each recursive call).
        * ------------------------------
        * If shuffledDeck has n items to start, taking our first slice takes n-1 time and space 
        * (plus half that, since we're also slicing one of our halves—but that's just a constant multiplier so we can ignore it). 
        * In our second recursive call, slicing takes n-2 time and space. 
        * =======================================================================
        * So our total time and space cost for slicing comes to:
        * (n - 1) + (n - 2) + ... + 2 + 1
        * This is a common series that turns out to be O(n^2).
        */

        public static bool IsSingleRiffle_Recursion1(int[] half1, int[] half2, int[] shuffledDeck) //O(n^2) time and O(n^2) space
        {
            // Base case
            if (shuffledDeck.Length == 0)
            {
                return true;
            }

            // If the top of shuffledDeck is the same as the top of half1
            // (making sure first that we have a top card in half1)
            if (half1.Length > 0 && half1[0] == shuffledDeck[0])
            {
                // Take the top cards off half1 and shuffledDeck and recurse
                return IsSingleRiffle_Recursion1(RemoveTopCard(half1), half2, RemoveTopCard(shuffledDeck));
            }

            // If the top of shuffledDeck is the same as the top of half2
            if (half2.Length > 0 && half2[0] == shuffledDeck[0])
            {
                // Take the top cards off half2 and shuffledDeck and recurse
                return IsSingleRiffle_Recursion1(half1, RemoveTopCard(half2), RemoveTopCard(shuffledDeck));
            }

            // Top of shuffledDeck doesn't match top of half1 or half2,
            // so we know it's not a single riffle
            return false;
        }

        /*
         * Avoid slicing and instead keep track of indices in the array : O(n) time, O(n) space
         */
        public static bool IsSingleRiffle_Recursion2(int[] half1, int[] half2, int[] shuffledDeck, int shuffledDeckIndex = 0,
            int half1Index = 0, int half2Index = 0)  //O(n) time, O(n) space
        {
            // Base case we've hit the end of shuffledDeck
            if (shuffledDeckIndex == shuffledDeck.Length)
            {
                return true;
            }

            if (half1Index < half1.Length
                && half1[half1Index] == shuffledDeck[shuffledDeckIndex])
            {
                // If we still have cards in half1 and the "top" card in half1 is the same
                // as the top card in shuffledDeck
                half1Index++;
            }
            else if (half2Index < half2.Length
                && half2[half2Index] == shuffledDeck[shuffledDeckIndex])
            {
                // If we still have cards in half2 and the "top" card in half2 is the same
                // as the top card in shuffledDeck
                half2Index++;
            }
            else
            {
                // If the top card in shuffledDeck doesn't match the top
                // card in half1 or half2, this isn't a single riffle.
                return false;
            }

            // The current card in shuffledDeck has now been "accounted for",
            // so move on to the next one
            shuffledDeckIndex++;
            return IsSingleRiffle_Recursion2(half1, half2, shuffledDeck,
                shuffledDeckIndex, half1Index, half2Index);
        }

        public static bool IsSingleRiffle_Greedy(int[] half1, int[] half2, int[] shuffledDeck) //O(n) time and O(1) additional space.
        {
            int half1Index = 0;
            int half2Index = 0;

            foreach (var card in shuffledDeck)
            {
                if (half1Index < half1.Length && card == half1[half1Index])
                {
                    // If we still have cards in half1 and the "top" card in half1 is the same
                    // as the top card in shuffledDeck
                    half1Index++;
                }
                else if (half2Index < half2.Length && card == half2[half2Index])
                {
                    // If we still have cards in half2 and the "top" card in half2 is the same
                    // as the top card in shuffledDeck
                    half2Index++;
                }
                else
                {
                    // If the top card in shuffledDeck doesn't match the top
                    // card in half1 or half2, this isn't a single riffle.
                    return false;
                }
            }

            // All cards in shuffledDeck have been accounted for.
            // So this is a single riffle!
            return true;
        }

        
        /*
         * Shuffle a deck of cards
         * Randomize a given array
         * https://www.i-programmer.info/programming/theory/2744-how-not-to-shuffle-the-kunth-fisher-yates-algorithm.html
         */
        public static void ShuffleArray_Greedy(int[] theArray) //O(n) time and O(1) space.
        {
            // If it's 1 or 0 items, just return
            if (theArray.Length <= 1)
            {
                return;
            }
            
            // Creating a object
            // for Random class
            Random r = new Random();

            // Walk through from beginning to end
            for (int indexWeAreChoosingFor = 0;
                    indexWeAreChoosingFor < theArray.Length - 1; indexWeAreChoosingFor++)
            {
                // Choose a random not-yet-placed item to place there
                // (could also be the item currently in that spot).
                // Must be an item AFTER the current item, because the stuff
                // before has all already been placed
                int randomChoiceIndex = r.Next(indexWeAreChoosingFor, theArray.Length - 1);
                //int randomChoiceIndex = GetRandom(indexWeAreChoosingFor, theArray.Length - 1);

                // Place our random choice in the spot by swapping
                if (randomChoiceIndex != indexWeAreChoosingFor)
                {
                    int valueAtIndexWeChoseFor = theArray[indexWeAreChoosingFor];
                    theArray[indexWeAreChoosingFor] = theArray[randomChoiceIndex];
                    theArray[randomChoiceIndex] = valueAtIndexWeChoseFor;
                }
            }
        }

        /*
         * Meetings are random from different team, to use greedy approach, we need look at all other meetings, 
         * So first we them to sort them
         */
        public static List<Meeting> MergeRanges_Greedy(List<Meeting> meetings) //O(nLogN), space O(N)
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

        public static int MinMeetingRooms(Meeting[] intervals)
        {
            var sortedMeetings1112 = intervals.Select(m => new Meeting(m.StartTime, m.EndTime))
                .OrderBy(m => m.StartTime).ToList(); //O(nLogN)

            var mergedMeetings = new List<Meeting> { sortedMeetings1112[0] }; //O(N)
            int z = 1;
            for (int i = 1; i < sortedMeetings1112.Count; i++)
            {
                var lastMergedMeeting = mergedMeetings.Last();
                //var y = mergedMeetings.Count(a => Math.Min(a.EndTime, intervals[i].EndTime) > Math.Max(a.StartTime, intervals[i].StartTime));
                //if (y > 0) //overlapped
                if (Math.Min(lastMergedMeeting.EndTime, sortedMeetings1112[i].EndTime) > Math.Max(lastMergedMeeting.StartTime, sortedMeetings1112[i].StartTime)) //overlapped
                {
                    z++;
                    //time

                    //lastMergedMeeting.EndTime =
                    //    Math.Max(lastMergedMeeting.EndTime, currentMeeting.EndTime);
                    mergedMeetings.Add(sortedMeetings1112[i]);
                }
                else
                {
                    // Add the current meeting since it doesn't overlap
                    //mergedMeetings.Add(intervals[i]);
                }
            }
            
            return -1;


            if (null == intervals) return 0;
            int len = intervals.Length;
            if (len <= 1) return len;

            int[] start = new int[len];
            int[] end = new int[len];
            for (int i = 0; i < len; i++)
            {
                Meeting curr = intervals[i];
                start[i] = curr.StartTime;
                end[i] = curr.EndTime;
            }

            Array.Sort(start);
            Array.Sort(end);
            
            var sortedMeetings111 = intervals.Select(m => new Meeting(m.StartTime, m.EndTime))
                .OrderBy(m => m.StartTime).ThenBy(m => m.EndTime).ToList(); //O(nLogN)

            int min = 0;
            int endTimesIterator = 0;
            for (int i = 0; i < sortedMeetings111.Count; i++)
            {
                //Increment the room for the current meeting that is starting.
                //Check if startTime of current meeting is after endTime of meeting that is suppose to end first.
                min++;
                if (sortedMeetings111[i].StartTime >= sortedMeetings111[endTimesIterator].EndTime)// end[endTimesIterator])
                {
                    min--;   //since one meeting ended, a room got empty.
                    endTimesIterator++;  //move to the next endTime.
                }
            }
            return min;
        }
        
        public static void runTest()
        {
            GetMaxProfit_GreedyApproach2(new int[]{ 7,6,4,3,1 });


            MinMeetingRooms(new Meeting[] {new Meeting(5, 10), new Meeting(15, 20), new Meeting(0, 30)});
            MinMeetingRooms(new Meeting[] {new Meeting(7,10), new Meeting(2,4)});
            MinMeetingRooms(new Meeting[] {new Meeting(9,10), new Meeting(4,9), new Meeting(4,17)});
            MinMeetingRooms(new Meeting[] { new Meeting(5, 8), new Meeting(6, 8)});
            
            MinMeetingRooms(new Meeting[] { new Meeting(0,30), new Meeting(5,10), new Meeting(15,20)});
            

            GetMaxProfit_GreedyApproach2(new[] {1, 2, 3, 4});
            HighestProductOf3_Greedy(new int[] {-7, -1, -4, -2});

            IsSingleRiffle_Greedy(new int[] {5, 6, 7, 8}, new int[] {1, 2, 3, 4}, new int[] {1,2,5,6,3,7,4,8});

            ShuffleArray_Greedy(new int[] {1, 2, 5, 6, 3, 7, 4, 8});
        }

    }
}
