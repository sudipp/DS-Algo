using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ConsoleApplication1
{
    /*Queue 
        space	O(n)
        enqueue	O(1)
        dequeue	O(1)
        peek	O(1)
     * TODO: Queues are easy to implement with linked lists:
     * Breadth-first search uses a queue to keep track of the nodes to visit next.
    */

    /*Stack
        space	O(n)
        push	O(1)
        pop	    O(1)
        peek	O(1)
     * TODO: Tree/Graph Traversal - Depth-first search uses a stack (sometimes the call stack) to keep track of which nodes to visit next.
     * TODO: expression parsing 
     * TOdO: You can implement a stack with either a linked list or a dynamic array—they both work pretty well:
         *                  Stack Push	        Stack Pop
            Linked Lists	insert at head	    remove at head
            Dynamic Arrays	append	            remove last element
    */

    class BuildingPoint : IComparable<BuildingPoint>
    {
        public int x;
        public bool isStart;
        public int h;

        public BuildingPoint(int x, bool isStart, int h)
        {
            this.x = x;
            this.h = h;
            this.isStart = isStart;
        }

        public int CompareTo(BuildingPoint r)
        {
            if (this.x != r.x)
            {
                return this.x.CompareTo(r.x);
            }
            else
            {
                if (this.isStart && r.isStart)
                {
                    return r.h.CompareTo(this.h);
                }
                else if (this.isStart && !r.isStart)
                {
                    return -this.h - r.h;
                }
                else if (! this.isStart && !r.isStart)
                {
                    return this.h.CompareTo(r.h);
                }
                else
                {
                    return this.h + r.h;
                }
            }
        }
    }


    class HitCounter
    {
        LinkedList<int> deque = new LinkedList<int>();

        /** Initialize your data structure here. */
        public HitCounter() {
            deque = new LinkedList<int>();
        }

        /** Record a hit.
            @param timestamp - The current timestamp (in seconds granularity). */
        public void hit(int timestamp)
        {
            deque.AddLast(timestamp);
        }

        /** Return the number of hits in the past 5 minutes.
            @param timestamp - The current timestamp (in seconds granularity). */
        public int getHits(int timestamp)
        {
            while (deque.Count != 0 && deque.First.Value <= timestamp - 300) 
                deque.RemoveFirst();//.poll();

            return deque.Count;
        }
    }

    class MovingAverage
    {
        /** Initialize your data structure here. */
        LinkedList<int> que;
        int SIZE; // size limit
        double sum;

        public MovingAverage(int size) {
            que = new LinkedList<int>();
            SIZE = size;
            sum = 0;
        }

        public double next(int val)
        {
            que.AddLast(val);
            sum += (double)val;
            if (que.Count > SIZE)
            {
                sum -= (double)que.First.Value;
                que.RemoveFirst();
            }
            return sum / (double)que.Count;
        }
    }

    public class StackQueueExercise
    {
        public static IList<int[]> GetSkyline(int[,] buildings)
        {
            BuildingPoint[] bps = new BuildingPoint[buildings.GetLength(0) * 2];
            int index = 0;
            for (int i = 0; i < buildings.GetLength(0); i++)
            {
                BuildingPoint bp1 = new BuildingPoint(buildings[i, 0], true, buildings[i, 2]);
                BuildingPoint bp2 = new BuildingPoint(buildings[i, 1], false, buildings[i, 2]);
                bps[index++] = bp1;
                bps[index++] = bp2;
            }

            //this is one key step:
            Array.Sort(bps);

            List<int[]> result = new List<int[]>();

            //SortedList<>
            var treeMap = new SortedList<int, int>(new DupKey());// <int, int>();
            //TreeMap<Integer, Integer> treeMap = new TreeMap();
            treeMap.Add(0, 1);
            int prevMaxH = 0;
            foreach (BuildingPoint bp in bps)
            {
                //if it's a starting point, we'll add it into the final result
                if (bp.isStart)
                {
                    if (treeMap.ContainsKey(bp.h))
                    {
                        treeMap[bp.h]=treeMap[bp.h] + 1;
                    }
                    else
                    {
                        treeMap.Add(bp.h, 1);
                    }
                }
                else if (!bp.isStart)
                {
                    //if it's an ending point, we'll decrement/remove this entry
                    if (treeMap.ContainsKey(bp.h) && treeMap[bp.h] > 1)
                    {
                        treeMap[bp.h]= treeMap[bp.h] - 1;
                    }
                    else
                    {
                        treeMap.Remove(bp.h);
                    }
                }



                int currMaxH = treeMap.Max().Key;//.Keys.Max();//.Max().Key;//.Last().Key;//.lastKey();
                if (currMaxH != prevMaxH)
                {
                    result.Add(new int[] { bp.x, currMaxH });
                    prevMaxH = currMaxH;
                }
            }


            return result;
        }


        //O(1) time for Push(), Pop(), and GetMax(). 
        //O(m) additional space, where m is the number of operations performed on the stack.
        public class MaxStack
        {
            Stack<int> _stack = new Stack<int>();
            Stack<int> _maxesStack = new Stack<int>();

            // Add a new item to the top of our stack. If the item is greater
            // than or equal to the last item in _maxesStack, it's
            // the new max! So we'll add it to _maxesStack.
            public void Push(int item)
            {
                _stack.Push(item);
                if (_maxesStack.Count == 0 || item >= _maxesStack.Peek())
                {
                    _maxesStack.Push(item);
                }
            }

            // Remove and return the top item from our stack. If it equals
            // the top item in _maxesStack, they must have been pushed in together.
            // So we'll pop it out of _maxesStack too.
            public int Pop()
            {
                int item = _stack.Pop();
                if (item == _maxesStack.Peek())
                {
                    _maxesStack.Pop();
                }
                return item;
            }

            // The last item in _maxesStack is the max item in our stack.
            public int GetMax()
            {
                return _maxesStack.Peek();
            }
        }

        public class QueueTwoStacks //O(1) - Enqueue, O(N) - Dequeue 
        {
            private Stack<int> _inStack = new Stack<int>();
            private Stack<int> _outStack = new Stack<int>();

            public bool IsEmpty()
            {
                return !_inStack.Any() && !_outStack.Any();
            }

            public int Count()
            {
                return _inStack.Count + _outStack.Count;
            }

            //For enqueue, we simply push the enqueued item onto inStack.
            public void Enqueue(int item)
            {
                _inStack.Push(item);
            }

            public int Peek()
            {
                if (_outStack.Count == 0)
                {
                    // Move items from inStack to outStack, reversing order
                    while (_inStack.Count > 0)
                    {
                        int newestInStackItem = _inStack.Pop();
                        _outStack.Push(newestInStackItem);
                    }

                    // If outStack is still empty, raise an error
                    if (_outStack.Count == 0)
                    {
                        throw new InvalidOperationException("Can't dequeue from empty queue!");
                    }
                }

                //dequeue on a non-empty outStack, we simply return the top item from outStack.
                return _outStack.Peek();
            }

            public int Dequeue()
            {
                /*For dequeue on an empty outStack, the oldest item is at the bottom of inStack. So we dig to the 
                 * bottom of inStack by pushing each item one-by-one onto outStack until we reach the bottom item, which we return.
                 */
                if (_outStack.Count == 0)
                {
                    // Move items from inStack to outStack, reversing order
                    while (_inStack.Count > 0)
                    {
                        int newestInStackItem = _inStack.Pop();
                        _outStack.Push(newestInStackItem);
                    }

                    // If outStack is still empty, raise an error
                    if (_outStack.Count == 0)
                    {
                        throw new InvalidOperationException("Can't dequeue from empty queue!");
                    }
                }

                //dequeue on a non-empty outStack, we simply return the top item from outStack.
                return _outStack.Pop();
            }
        }

        public static int GetClosingParen_Stack(string sentence, int openingParenIndex)
            //O(n) time, where n is the number of chars in the string. O(1) space.
        {
            /*
             * In this problem, we can realize our stack would only hold '(' characters. 
             * So instead of storing each of those characters in a stack, we can store the number of items our stack would be holding.
             * That gets us from O(n) space to O(1) space.
             */
            int openNestedParens = 0;

            for (int position = openingParenIndex + 1; position < sentence.Length; position++)
            {
                char c = sentence[position];

                if (c == '(')
                {
                    openNestedParens++;
                }
                else if (c == ')')
                {
                    if (openNestedParens == 0)
                    {
                        return position;
                    }
                    else
                    {
                        openNestedParens--;
                    }
                }
            }

            throw new ArgumentException("No closing parenthesis :(", sentence.GetType().Name);
        }

        public static bool IsBracesBracketsParenthesesValid_Stack(String code)
            //O(n) time (one iteration through the string), and O(n) space
        {
            var openersToClosers = new Dictionary<char, char>
            {
                {'(', ')'},
                {'[', ']'},
                {'{', '}'}
            };

            var openers = new HashSet<char>(openersToClosers.Keys);
            var closers = new HashSet<char>(openersToClosers.Values);

            var openersStack = new Stack<char>();

            /*
             *  If we see an opener, we push it onto the stack.
                If we see a closer, we check to see if it is the closer for the opener at the top of the stack. 
             *      If it is, we pop from the stack. If it isn't, or if the stack is empty, we return false.
            */
            foreach (char c in code)
            {
                if (openers.Contains(c))
                {
                    openersStack.Push(c);
                }
                else if (closers.Contains(c))
                {
                    if (openersStack.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        char lastUnclosedOpener = openersStack.Pop();

                        // If this closer doesn't correspond to the most recently
                        // seen unclosed opener, short-circuit, returning false
                        if (openersToClosers[lastUnclosedOpener] != c)
                        {
                            return false;
                        }
                    }
                }
            }
            return openersStack.Count == 0;
        }

        public static int evaluateExpression_Stack(char[] expression)
        {
            //O(n) time and O(n) space
            if (expression == null || expression.Length == 0)
                return 0;

            Stack<char> operand = new Stack<char>();
            Stack<char> Operator = new Stack<char>();
            foreach (char ch in expression)
            {
                if (isOperand(ch))
                    operand.Push(ch);
                else if (isOperator(ch))
                {
                    while (Operator.Count != 0 && precedence(Operator.Peek()) >= precedence(ch))
                    {
                        process(Operator, operand);
                    }
                    Operator.Push(ch);
                }
            }
            while (Operator.Count != 0)
            {
                process(Operator, operand);
            }
            return operand.Pop();
        }

        #region helper functions

        /*
        * Helper functions. Ask interviewer if they want you to implement.
        */

        private static bool isOperand(char ch)
        {
            return (ch >= '0') && (ch <= '9');
        }

        private static bool isOperator(char ch)
        {
            return ch == '+' || ch == '-' || ch == '*' || ch == '/';
        }

        private static int precedence(char ch)
        {
            switch (ch)
            {
                case '/':
                case '*':
                    return 2;
                case '+':
                case '-':
                    return 1;
                default:
                    throw new ArgumentException("Invalid operator: " + ch);
            }
        }

        private static void process(Stack<char> operatorStack, Stack<char> operand)
        {
            int num2 = (int) Char.GetNumericValue(operand.Pop());
            int num1 = (int) Char.GetNumericValue(operand.Pop());
            char op = operatorStack.Pop();
            int result = 0;
            switch (op)
            {
                case '/':
                    result = num1/num2;
                    break;
                case '*':
                    result = num1*num2;
                    break;
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
            }
            operand.Push((char) ('0' + result)); // '0' is 48  //Convert.ToChar(result);
        }

        #endregion

        //using Doubly ended Queue
        private static void slidingWindowSum_usingQueue(int[] a, int windowSize) //Time O(n), Space O(k)
        {
            if (windowSize == 0 || a.Length == 0)
                return;

            //Whenever you see a sliding window problem, you should think of using a
            //doubly ended queue (Deque).
            LinkedList<int> ll = new LinkedList<int>();

            int sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                /*
                 * We keep adding elements to the Deque until it is of size K. To add another element, we
                    remove the element from the back(last) and add the new element to the front(first), maintaining the size
                    K of the queue.
                 * ============================
                 * Every time we add an element, we add its value to the sum. When we remove an element, we
                    subtract its value from the sum. That way, the sum always contains the sum of the sliding
                    window of size K.
                 */
                if (ll.Count == windowSize)
                {
                    int last = ll.Last.Value;
                    ll.RemoveLast();
                    sum -= last;
                }

                ll.AddFirst(a[i]);
                sum += a[i];

                if (ll.Count == windowSize)
                {
                    Console.WriteLine(sum);
                }
            }
        }

        //using Doubly ended Queue - LinkedList
        public class MaxStockPriceinWindowTime_Queue
        {
            internal class Price
            {
                int price;
                int day;

                public Price(int price, int day)
                {
                    this.price = price;
                    this.day = day;
                }

                public int getPrice()
                {
                    return price;
                }

                public int getDay()
                {
                    return day;
                }
            }

            LinkedList<Price> dq;
            int window;

            public MaxStockPriceinWindowTime_Queue(int windowDays)
            {
                dq = new LinkedList<Price>();
                window = windowDays;
            }

            public void addPrice(int price, int day) //Time Worst O(n), Space O(n)
            {
                // Every time a new stock price comes in, we add it to the sliding window 
                // and remove all elements that exceed 3 days from the back.
                while (dq.Count == 0 && dq.Last.Value.getDay() < (day - window + 1))
                    dq.RemoveLast();

                dq.AddFirst(new Price(price, day));
            }

            // Returns max price in last 3 days
            public int getMax() //Time O(n), Space O(1), Use MaxStack class (above), to use the runtime O(1)
            {
                int maxPrice = 0;

                IEnumerator<Price> iter = dq.GetEnumerator();
                while (iter.MoveNext())
                {
                    int price = (iter.Current).getPrice();
                    if (price > maxPrice)
                        maxPrice = price;
                }
                return maxPrice;
            }
        }



        //Adi

        //3StackInArray
        public class ThreeStackInArray
        {
            int stackSize;
            private int[] buffer;
            private int[] stackPointer;

            public ThreeStackInArray(int stackSize)
            {
                stackSize = this.stackSize;
                buffer = new int[stackSize*3];
                stackPointer = new int[] {0, 0, 0}; // stack pointers to track top elem
            }

            public void push(int stackNum, int value)
            {
                /* Check that we have space for the next element */
                if (isFull(stackNum))
                    throw new Exception("Stack Full");

                /* Increment stack pointer and then update top value. */
                stackPointer[stackNum]++;
                buffer[indexOfTop(stackNum)] = value;
            }

            public int pop(int stackNum)
            {
                if (isEmpty(stackNum))
                {
                    throw new Exception("Empty Stack");
                }

                int topindex = indexOfTop(stackNum);
                int value = buffer[topindex]; // Get top
                buffer[topindex] = 0; // Clear
                stackPointer[stackNum]--; // Shrink

                return value;
            }

            public int peek(int stackNum)
            {
                if (isEmpty(stackNum))
                    throw new Exception("Empty Stack");

                return buffer[indexOfTop(stackNum)];
            }

            bool isEmpty(int stackNum)
            {
                return stackPointer[stackNum] == 0;
            }

            bool isFull(int stackNum)
            {
                return stackPointer[stackNum] == stackSize;
            }

            private int indexOfTop(int stackNum)
            {
                int offset = stackNum*stackSize;
                int size = stackPointer[stackNum];
                return offset + size - 1;
            }
        }

        

        //O(1) time for Push(), Pop(), and GetMin(). 
        //O(m) additional space, where m is the number of operations performed on the stack.
        public class MinStack
        {
            Stack<int> _stack = new Stack<int>();
            Stack<int> _minStack = new Stack<int>();

            // Add a new item to the top of our stack. If the item is lesser
            // than or equal to the last item in _minStack, it's
            // the new min! So we'll add it to _minStack.
            public void Push(int item)
            {
                _stack.Push(item);
                if (_minStack.Count == 0 || item <= _minStack.Peek())
                {
                    _minStack.Push(item);
                }
            }

            // Remove and return the top item from our stack. If it equals
            // the top item in _maxesStack, they must have been pushed in together.
            // So we'll pop it out of _maxesStack too.
            public int Pop()
            {
                int item = _stack.Pop();
                if (item == _minStack.Peek())
                {
                    _minStack.Pop();
                }
                return item;
            }

            // The last item in _maxesStack is the max item in our stack.
            public int GetMin()
            {
                return _minStack.Peek();
            }
        }

        /*
         * Write a program to sort a stack such that the smallest items are on the top
         * When we pop 5 from s1, we need to find the right place in s2 to insert this number. 
         * In this case, the correct place is on s2 just above 3. How do we get it there? We can do this by popping 5 from s1 and holding it 
         * in a temporary variable. Then, we move 12 and 8 over to s1 (by popping them from s2 and pushing them onto s1) and then push 5 onto s2.
        */

        void SortStack(Stack<int> s) //O ( N2 ) time and O ( N) space
        {
            Stack<int> r = new Stack<int>();

            while (s.Count > 0)
            {
                /* Insert each element in s in sorted order into r. */
                int tmp = s.Pop();
                while (r.Count > 0 && r.Peek() > tmp)
                {
                    s.Push(r.Pop());
                }
                r.Push(tmp);
            }

            /* Copy the elements from r back into s. */
            while (r.Count > 0)
            {
                s.Push(r.Pop());
            }
        }



        public IList<IList<int>> zigzagLevelOrder(Node root)
        {
            Queue<Node> q = new Queue<Node>();
            List<IList<int>> levels = new List<IList<int>>();
            if (root == null)
            {
                return levels;
            }
            q.Enqueue(root);

            bool forward = true;
            while (q.Any())
            {
                int size = q.Count;
                List<int> level = new List<int>();
                for (int i = 0; i < size; i++)
                {
                    Node curr = q.Dequeue();
                    level.Add(curr.val);
                    if (curr.pre != null)
                    {
                        q.Enqueue(curr.pre);
                    }
                    if (curr.next != null)
                    {
                        q.Enqueue(curr.next);
                    }
                }
                if (forward)
                {
                    forward = false;
                    levels.Add(level);
                }
                else
                {
                    level.Reverse();
                    levels.Add(level);
                    forward = true;
                }
            }
            return levels;
        }

        public static void runTest()
        {
            //[[2,9,10],[3,7,15],[5,12,12],[15,20,10],[19,24,8]]
                       
            GetSkyline(new int[,] { { 2, 9, 10 }, { 3, 7, 15 }, { 5, 12, 12 }, { 15, 20, 10 }, { 19, 24, 8 } });


            SingleNumber(new int[] {2, 2, 1});


            maxSlidingWindow(new int[] {1, -1}, 1);

            maxSlidingWindow(new int[] {1, 3, 1, 2, 0, 5}, 3);

            maxSlidingWindow(new int[] {-7, -8, 7, 5, 7, 1, 6, 0}, 4);

            maxSlidingWindow(new int[] {1, 3, -1, -3, 5, 3, 6, 7}, 3);
            slidingWindowSum_usingQueue(new int[] {-2, 1, -3, 4, -1, 2, 1, -5, 4}, 2);


            ThreeStackInArray test = new ThreeStackInArray(2);
            test.push(0, 1);
            test.push(1, 2);
            test.push(2, 3);

            test.push(0, 11);
            test.push(1, 22);

            test.push(2, 33);

            test.pop(1);
            test.pop(1);
            test.pop(1);

            int[,] imgArray = new int[4, 4]
            {
                {1, 2, 3, 4},
                {10, 21, 31, 41},
                {11, 22, 32, 42},
                {12, 23, 33, 43}
            };
            //(new int[] { 1, 2, 3, 4 }, 3);

            evaluateExpression_Stack(new char[] {'1', '+', '2', '/', '1', '+', '3', '*', '2'});
        }

        public class DupKey : IComparer<int>
        {
            public int Compare(int left, int right)
            {
                //return left < right ? 1 : -1;
                return right.CompareTo(left);
            }
        }

        public class DupKey1 : IComparable<int>//, IComparer<int>
        {
            public int CompareTo(int right)
            {
                //return left < right ? 1 : -1;
                return right.CompareTo(this);
            }

            public int Compare(int left, int right)
            {
                //return left < right ? 1 : -1;
                return right.CompareTo(left);
            }
        }


        public static int[] maxSlidingWindow(int[] nums, int windowSize)
        {
            if (nums.Length == 0)
                return new int[] {};
            LinkedList<int> window = new LinkedList<int>();
            int[] ans = new int[nums.Length - windowSize + 1];

            for (int i = 0; i < nums.Length; ++i)
            {
                if (window.Count > 0 && (window.First.Value <= i - windowSize))
                {
                    window.RemoveFirst();
                }
                while (window.Count > 0 && (nums[window.Last.Value] <= nums[i]))
                {
                    window.RemoveLast();
                }
                window.AddLast(i);
                if (i >= windowSize - 1)
                {
                    ans[i - windowSize + 1] = nums[window.First.Value];
                }
            }
            return ans;


            if (nums == null || nums.Length == 0) return new int[0];
            int[] result1 = new int[nums.Length - windowSize + 1];
            int index = 0;

            //PriorityQueue<Integer> queue = new PriorityQueue<Integer>(Collections.reverseOrder());
            SortedSet<int> queue2 = new SortedSet<int>(new DupKey());
            for (int i = 0; i < nums.Length; i++)
            {
                queue2.Add(nums[i]);
                if (queue2.Count == windowSize)
                {
                    result1[index++] = queue2.First();
                    queue2.Remove(nums[i + 1 - windowSize]);
                }
            }
            return result1;

            int len = nums.Length;
            int[] result = new int[len - windowSize + 1];
            if (nums.Length == 0)
                return new int[0];
            //SortedList<int, object> queue = new SortedList<int, object>(new DupKey());
            SortedSet<int> queue1 = new SortedSet<int>(new DupKey());

            for (int i = 0; i < windowSize; i++)
            {
                //if (!queue1.ContainsKey(nums[i]))
                queue1.Add(nums[i]); //,null);
            }
            result[0] = queue1.First(); //.Key;//.peek();
            for (int i = windowSize; i < len; i++)
            {
                queue1.Remove(nums[i - windowSize]);

                //if (!queue.ContainsKey(nums[i]))
                queue1.Add(nums[i]); //,null);
                result[i - windowSize + 1] = queue1.First(); //.Key;
            }
            return result;


            if (nums == null || nums.Length <= 1)
            {
                return nums;
            }

            /** initialize your data structure here. */
            SortedList<int, object> maxHeap = new SortedList<int, object>(new DupKey());
            //PriorityQueue maxHeap = new PriorityQueue(Collections.reverseOrder());

            int[] maxElements = new int[nums.Length - windowSize + 1];

            for (int i = 0, l = 0; l + windowSize <= nums.Length; l++)
            {
                for (int j = i; j < l + windowSize; j++)
                {
                    maxHeap.Add(nums[j], null);
                }
                maxElements[l] = maxHeap.First().Key;
                maxHeap.Remove(nums[l]);
                i = l + windowSize;
            }
            return maxElements;
        }

        
        public class MedianFinder
        {
            public class DupKey : IComparer<int>
            {
                public int Compare(int left, int right)
                {
                    return left > right ? 1 : -1;
                }
            }

            /** initialize your data structure here. */
            SortedList<int, object> data;

            SortedSet<int> data1;

            public MedianFinder()
            {
                data = new SortedList<int, object>(new DupKey());

                data1 = new SortedSet<int>(new DupKey());
            }

            public void AddNum(int num)
            {
                data.Add(num, null);
                data1.Add(num);
            }

            public double FindMedian()
            {
                int mid = data1.Count/2;

                if (data1.Count%2 == 1)
                {
                    return data1.ElementAt(mid); //.Key;
                }
                else
                {
                    //double med = (data1.ElementAt(mid - 1).Key + data1.ElementAt(mid).Key) / 2.0;
                    double med = (data1.ElementAt(mid - 1) + data1.ElementAt(mid))/2.0;
                    return med;
                }
            }
        }



        public String minWindow(string str, string pat)
        {
            int len1 = str.Length;
            int len2 = pat.Length;

            // check if string's length is less than pattern's
            // length. If yes then no such window can exist
            if (len1 < len2)
            {
                //cout << "No such window exists";
                return "";
            }

            int no_of_chars = 256;

            int[] hash_pat = new int[no_of_chars];
            int[] hash_str = new int[no_of_chars];

            // store occurrence ofs characters of pattern
            for (int i = 0; i < len2; i++)
                hash_pat[pat[i]]++;

            int start = 0;
            int start_index = -1;
            int min_len = int.MaxValue;

            // start traversing the string
            int count = 0; // count of characters
            for (int j = 0; j < len1; j++)
            {
                // count occurrence of characters of string
                hash_str[str[j]]++;

                // If string's char matches with pattern's char
                // then increment count
                if (hash_pat[str[j]] != 0 &&
                    hash_str[str[j]] <= hash_pat[str[j]])
                    count++;

                // if all the characters are matched
                if (count == len2)
                {
                    // Try to minimize the window i.e., check if
                    // any character is occurring more no. of times
                    // than its occurrence in pattern, if yes
                    // then remove it from starting and also remove
                    // the useless characters.
                    while (hash_str[str[start]] > hash_pat[str[start]]
                           || hash_pat[str[start]] == 0)
                    {

                        if (hash_str[str[start]] > hash_pat[str[start]])
                            hash_str[str[start]]--;
                        start++;
                    }

                    // update window size
                    int len_window = j - start + 1;
                    if (min_len > len_window)
                    {
                        min_len = len_window;
                        start_index = start;
                    }
                }
            }

            // If no window found
            if (start_index == -1)
            {
                //cout << "No such window exists";
                return "";
            }

            // Return substring starting from start_index
            // and length min_len
            return str.Substring(start_index, min_len);
        }


        public static int SingleNumber(int[] arr)
        {
            int x = 0;
            foreach (int num in arr)
            {
                x = x ^ num;
            }
            return x;
        }

        public IList<int> NumIslands2(int m, int n, int[,] positions)
        {

            int[,] dirs = {{0, 1}, {1, 0}, {-1, 0}, {0, -1}};

            List<int> result = new List<int>();
            if (m <= 0 || n <= 0) return result;

            int count = 0; // number of islands
            int[] roots = new int[m*n];
            // one island = one tree
            for (int k = 0; k < roots.Length; k++)
                roots[k] = -1;

            for (int k = 0; k < positions.GetLength(0); k++)
            {
                int root = n*positions[k, 0] + positions[k, 1]; // assume new point is isolated island
                roots[root] = root; // add new island
                count++;

                for (int d = 0; d < dirs.GetLength(0); d++)
                {
                    int x = positions[k, 0] + dirs[d, 0];
                    int y = positions[k, 1] + dirs[d, 1];

                    int nb = n*x + y;
                    if (x < 0 || x >= m || y < 0 || y >= n || roots[nb] == -1)
                        continue;

                    int rootNb = findIsland(roots, nb);
                    if (root != rootNb)
                    {
                        // if neighbor is in another island
                        roots[root] = rootNb; // union two islands 
                        root = rootNb; // current tree root = joined tree root
                        count--;
                    }
                }

                result.Add(count);
            }
            return result;
        }

        public int findIsland(int[] roots, int id)
        {
            while (id != roots[id])
            {
                roots[id] = roots[roots[id]]; // only one line added
                id = roots[id];
            }
            return id;
        }
    }
}
