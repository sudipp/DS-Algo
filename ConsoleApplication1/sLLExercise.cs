using System;
using System.Collections.Generic;
using System.Linq;
//using System.Collections.Generic;

namespace ConsoleApplication1
{
    /*
     * Worst Case
        space	O(n)
        prepend	O(1)
        append	O(1)
        lookup	O(n)
        insert	O(n)
        delete	O(n)
     * 
     * C# its LinkedList<?>, LinkListNode<?>
     * 
     */
    public class sLLExercise
    {
        public class Node
        {
            public int data;
            public Node next;
            public Node(int d)
            {
                data = d;
                next = null;
            }
        }


        public class ListNode {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        public class LinkedList
        {
            public Node head;
            public Node Tail;

            /* Function to reverse the linked list */
            //Node reverse(Node node)
            //{
            //    Node prev = null;
            //    Node current = node;
            //    Node next = null;
            //    while (current != null)
            //    {
            //        next = current.next;
            //        current.next = prev;
            //        prev = current;
            //        current = next;
            //    }
            //    node = prev;
            //    return node;
            //}

            public void reverse()
            {

                Node current = head;
                Node prev = null, next = null;
                while (current != null)
                {
                    next = current.next;
                    current.next = prev;
                    prev = current;
                    current = next;
                }
                Node tailNode = Tail;
                Tail = head;
                head = tailNode;
            }

            public void printList()
            {
                Node node = head;
                while (node != null)
                {
                    Console.WriteLine(node.data + " ");
                    node = node.next;
                }
            }
        }


        private static LinkedList Merge2SortedLinkLists(LinkedList a, LinkedList b) //O(n), Space o(1)
        {
            Node newHead = null;
            Node aCurrent = a.head;
            Node bCurrent = b.head;

            if (aCurrent.data > bCurrent.data)
            {
                newHead = bCurrent;
                bCurrent = bCurrent.next;
            }
            else
            {
                newHead = aCurrent;
                aCurrent = aCurrent.next;
            }

            Node mrgCurrent = newHead;
            while (aCurrent != null && bCurrent != null)
            {
                if (aCurrent.data > bCurrent.data)
                {
                    mrgCurrent.next = bCurrent;
                    mrgCurrent = mrgCurrent.next;
                    bCurrent = bCurrent.next;
                }
                else if (aCurrent.data <= bCurrent.data)
                {
                    mrgCurrent.next = aCurrent;
                    aCurrent = aCurrent.next;
                    mrgCurrent = mrgCurrent.next;
                }
            }
            mrgCurrent.next = bCurrent ?? aCurrent;
            var newlst = new LinkedList {head = newHead};
            return newlst;
        }

        private static LinkedList ReverseLinkList(LinkedList a) //O(n) time and O(1) space. 
        {
            Node current = a.head;
            Node prev=null, next=null;
            while (current != null)
            {
                next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }

            Node tailNode = a.Tail;
            a.Tail = a.head;
            a.head = tailNode;
            
            return a;
        }

        private static int GetMiddleOfLinkList(LinkedList a)
        {
            Node slow = a.head, fast = a.head;
            while (fast != null && fast.next!=null)
            {
                fast = fast.next;
                slow = slow.next;
                fast = fast.next;
            }
            return slow.data;
        }

        private static int getListLength(LinkedList ll)
        {
            Node current = ll.head;
            int len = 0;
            while (current!=null)
            {
                len++;
                current = current.next;
            }
            return len;
        }


        private static int FindIntersectionOf2LinkedLists(LinkedList a, LinkedList b) //O(n), space O(1)
        {
            /*
            A:            a1 -> a2
                                    ->
                                        c1 -> c2 -> c3
                                    ->
            B:      b1 -> b2 -> b3
            */
            int lenA = getListLength(a);
            int lenB = getListLength(b);

            Node headA = a.head;
            Node headB = b.head;

            if (lenA > lenB)
            {
                for (int i = 0; i < lenA - lenB; i++)
                {
                    headA = headA.next;
                }
            }
            else if (lenA < lenB)
            {
                for (int i = 0; i < lenB - lenA; i++)
                {
                    headB = headB.next;
                }
            }

            while (headA != null && headB != null)
            {
                if (headA.data == headB.data)
                {
                    return headA.data;
                }
                headA = headA.next;
                headB = headB.next;
            }
            return -1;
        }

        public static LinkedListNode<int> KthToLastNode(int k, LinkedListNode<int> head) //O(n) time and O(1) space
        {
            if (k < 1)
            {
                throw new ArgumentOutOfRangeException("Impossible to find less than first to last node");
            }

            var leftNode  = head;
            var rightNode = head;

            // Move rightNode to the kth node
            for (int i = 0; i < k - 1; i++)
            {
                // But along the way, if a rightNode doesn't have a next,
                // then k is greater than the length of the list and there
                // can't be a kth-to-last node! we'll raise an error
                if (rightNode.Next == null)
                {
                    throw new ArgumentOutOfRangeException("k is larger than the length of the linked list: {k}");
                }

                rightNode = rightNode.Next;
            }

            // Starting with leftNode on the head,
            // move leftNode and rightNode down the list,
            // maintaining a distance of k between them,
            // until rightNode hits the end of the list
            while (rightNode.Next != null)
            {
                leftNode  = leftNode.Next;
                rightNode = rightNode.Next;
            }

            // Since leftNode is k nodes behind rightNode,
            // leftNode is now the kth to last node!
            return leftNode;
        }

        /*
         * Cannt delete the last node, with this technique          * 
         */
        public static void DeleteNode(Node nodeToDelete) //O(1) time and space
        {
            // Get the input node's next node, the one we want to skip to
            var nextNode = nodeToDelete.next;

            if (nextNode != null)
            {
                // Replace the input node's value and pointer with the next
                // node's value and pointer. the previous node now effectively
                // skips over the input node
                nodeToDelete.data = nextNode.data;
                nodeToDelete.next = nextNode.next;
            }
            else
            {
                // Eep, we're trying to delete the last node!
                throw new InvalidOperationException("Can't delete the last node with this technique!");
            }
        }

        public static bool ContainsCycle(LinkedList a) //O(n) time and O(1) space.
        {
            // Start both runners at the beginning
            Node slow = a.head; 
            Node fast = a.head;

            // Until we hit the end of the list
            while (fast != null && fast.next != null)
            {
                fast = fast.next;
                slow = slow.next;
                fast = fast.next;

                // Case: fastRunner is about to "lap" slowRunner
                if (fast == slow)
                {
                    return true;
                }
            }

            // Case: fastRunner hit the end of the list
            return false;
        }






        //Adi

        //we can iterate with two pointers: current which iterates through the linked list,
        //and runner which checks all subsequent nodes for duplicates.
        void deleteDuplicateNodes(Node head) //O ( 1) space, but O ( N2) time
        {
            Node current = head;
            while (current != null) 
            {
             /* Remove all future nodes that have the same value */
                Node runner = current;
                while (runner.next != null) {
                    if (runner.next.data == current.data) {
                        runner.next = runner.next.next;
                    } else {
                        runner = runner.next;
                    }
                }
                current = current.next;
            }
        }
        void deleteDuplicateNodes_1(Node head) //O(N) time, O(N) space
        { 
            HashSet<int> set = new HashSet<int>();
            Node previous = null;
            Node current = head;
            while (current != null)
            {
                if (set.Contains(current.data))
                {
                    previous.next = current.next;
                } 
                else 
                {
                    set.Add(current.data);
                    previous = current;
                }
                current = current.next;
            }
        }


        //Put half of element in a STACK, so we can pop them reverse order
        //Use fast and slow pointer to go the middle of the link list
        bool isLinkListPalindrome(Node head) {
            Node fast= head;
            Node slow= head;

            Stack<int> stack= new Stack<int>();

            /* Push elements from first half of linked list onto stack. When fast runner
            * (which is moving at 2x speed) reaches the end of the linked list, then we
            * know we're at the middle*/
            while (fast != null && fast.next != null) {
                stack.Push(slow.data);
                slow = slow.next;
                fast= fast.next.next;
            }

            /* Has odd number of elements, so skip the middle element*/
            if (fast!= null) {
                slow= slow.next;
            }

            while (slow != null) {
                int top= stack.Pop();

                /* If values are different, then it's not a palindrome*/
                if (top != slow.data) {
                    return false;
                }
                slow= slow.next;
            }
            return true;
        }

        /*Add 2 link lists 
         * The digits are stored in reverse order, such that the 1 's digit is at the head of the list. 
         * Write a function that adds the two numbers and returns the sum as a linked list.
         * Input: (7-> 1 -> 6) + (5 -> 9 -> 2). That is,617 + 295.
         * Output: 2 -> 1 -> 9. That is, 912.
         */
        static Node Add2LinkLists(Node L1, Node L2, int carry) 
        {
            int sum = 0;
            Node list = new Node(0);
            while (L1 != null || L2 != null)
            {
                if (L1 != null)
                {
                    sum += L1.data;
                    L1 = L1.next;
                }
                if (L2 != null)
                {
                    sum += L2.data;
                    L2 = L2.next;
                }

                //list.val = sum / 10;                        
                list.next = new Node(sum % 10);
                list.next.next = list.next;

                sum /= 10;
            }

            return list.next;


            if (L1 == null && L2 == null && carry== 0) {
                return null;
            }
            
            int value = carry;
            if (L1 != null) {
                value += L1.data;
            }
            if (L2 != null) {
                value += L2.data;
            }

            Node result = new Node(value % 10); /* Second digit of number */

            /*Recurse */
            if (L1 != null || L2 != null) {
                Node more = Add2LinkLists(L1 == null ? null : L1.next,
                                        L2 == null? null : L2. next,
                                        value >= 10 ? 1 : 0);
                 result.next = more;
            }
            return result;
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            Stack<int> s1 = new Stack<int>();
            Stack<int> s2 = new Stack<int>();

            while (l1 != null)
            {
                s1.Push(l1.val);
                l1 = l1.next;
            };
            while (l2 != null)
            {
                s2.Push(l2.val);
                l2 = l2.next;
            }

            int sum = 0;
            ListNode list = new ListNode(0);
            while (s1.Count>0 || s2.Count>0)
            {
                if (s1.Count > 0) sum += s1.Pop();
                if (s2.Count > 0) sum += s2.Pop();
                list.val = sum % 10;

                ListNode head = new ListNode(sum / 10);
                head.next = list;
                list = head;
                sum /= 10;
            }

            return list.val == 0 ? list.next : list;
        }


        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            if (strs.Length == 0) return new List<IList<string>>();

            Dictionary<string, List<string>> ans = new Dictionary<string, List<string>>();
            foreach (string s in strs)
            {
                char[] ca = s.ToCharArray();

                Array.Sort(ca);

                string key = new string(ca);

                if (!ans.ContainsKey(key)) ans.Add(key, new List<string>());

                ans[key].Add(s);
            }
            return new List<IList<string>>(ans.Values);
        }


        public class ListComparer : IComparer<ListNode>
        {
            public int Compare(ListNode left, ListNode right)
            {
                return left.val - right.val; //left.val.CompareTo(right.val);
            }
        }

        public static ListNode MergeKLists(ListNode[] lists)
        {
            var fakehead = new ListNode(-1);
            var start = fakehead;
            var sortedDictionary = new SortedDictionary<int, List<ListNode>>();

            foreach (var list in lists)
            {
                if (list != null)
                {
                    if (sortedDictionary.ContainsKey(list.val))
                    {
                        sortedDictionary[list.val].Add(list);
                    }
                    else
                    {
                        sortedDictionary.Add(list.val, new List<ListNode>(){ list });
                    }
                }
            }

            while (sortedDictionary.Any())
            {
                var top = sortedDictionary.First();
                var minnode = top.Value[0];
                if (top.Value.Count == 1)
                {
                    sortedDictionary.Remove(top.Key);
                }
                else
                {
                    top.Value.RemoveAt(0);
                }

                start.next = minnode;
                start = start.next;

                if (minnode.next != null)
                {
                    if (sortedDictionary.ContainsKey(minnode.next.val))
                    {
                        sortedDictionary[minnode.next.val].Add(minnode.next);
                    }
                    else
                    {
                        sortedDictionary.Add(minnode.next.val, new List<ListNode>(){ minnode.next });
                    }
                }
            }

            return fakehead.next;






            ListNode head = null;
            for (int i = 0; i < lists.Length; i++)
            {
                head = MergeLists(head, lists[i]);
            }

            return head;
        }

        public static ListNode MergeLists(ListNode l1, ListNode l2)
        {

            ListNode newHead = null;
            ListNode aCurrent = l1;
            ListNode bCurrent = l2;

            if (aCurrent != null && bCurrent != null)
            {
                if (aCurrent.val > bCurrent.val)
                {
                    newHead = bCurrent;
                    bCurrent = bCurrent.next;
                }
                else
                {
                    newHead = aCurrent;
                    aCurrent = aCurrent.next;
                }
            }
            else if (aCurrent != null)
            {
                newHead = aCurrent;
                aCurrent = aCurrent.next;
            }
            else if (bCurrent != null)
            {
                newHead = bCurrent;
                bCurrent = bCurrent.next;
            }

            ListNode mrgCurrent = newHead;
            while (aCurrent != null && bCurrent != null)
            {
                if (aCurrent.val > bCurrent.val)
                {
                    mrgCurrent.next = bCurrent;
                    bCurrent = bCurrent.next;
                }
                else if (aCurrent.val <= bCurrent.val)
                {
                    mrgCurrent.next = aCurrent;
                    aCurrent = aCurrent.next;
                }
                mrgCurrent = mrgCurrent.next;
            }
            
            if (mrgCurrent!=null)
                mrgCurrent.next = bCurrent ?? aCurrent;
            //var newlst = new LinkedList {head = newHead};

            return newHead;//newlst;
        }


        

        public static void runTest()
        {
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(4);
            //l1.next.next = new ListNode(5);
            ListNode l2 = new ListNode(1);
            l2.next = new ListNode(3);
            l2.next.next = new ListNode(4);
            ListNode l3 = new ListNode(2);
            l3.next = new ListNode(6);
            ListNode[] llArray=new ListNode[3];
            llArray[0] = l1;
            llArray[1] = l2;
            llArray[2] = l3;
            MergeKLists(llArray);




            Node ll1 = new Node(2);
            ll1.next = new Node(4);
            ll1.next.next = new Node(3);
            Node ll2 = new Node(5);
            ll2.next = new Node(6);
            ll2.next.next = new Node(4);
            Add2LinkLists(ll1, ll2,0);




            ListNode ll11 = new ListNode(7);
            ll11.next = new ListNode(2);
            ll11.next.next = new ListNode(4);
            ll11.next.next.next = new ListNode(6);

            ListNode ll22 = new ListNode(3);
            ll22.next = new ListNode(5);
            ll22.next.next = new ListNode(5);

            AddTwoNumbers(ll11, ll22);

            LinkedList list = new LinkedList();
            //list.head = new Node(2);
            //list.head.next = new Node(4);
            //list.head.next.next = new Node(6);
            //list.head.next.next.next = new Node(8);
            //list.Tail = list.head.next.next.next;


            

            list.head = new Node(2);
            list.head.next = new Node(4);
            list.head.next.next = new Node(6);
            list.head.next.next.next = new Node(8);
            list.head.next.next.next.next = new Node(9);
            list.Tail = list.head.next.next.next.next;
            
            
            LinkedList list2 = new LinkedList();
            list2.head = new Node(1);
            list2.head.next = new Node(6);
            list2.head.next.next = new Node(8);
            list2.head.next.next.next = new Node(9);
            list2.Tail = list.head.next.next.next;

            var mergedLL= Merge2SortedLinkLists(list, list2);
            //mergedLL.printList();

            var reversedLL = ReverseLinkList(list);
            //reversedLL.printList();

            int middle= GetMiddleOfLinkList(list);

            int intersectionVal= FindIntersectionOf2LinkedLists(list, list2);


            list.printList();
            list.reverse();
            list.printList();
        }
    }
}
