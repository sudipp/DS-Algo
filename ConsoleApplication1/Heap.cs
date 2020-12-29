using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class HeapNode<T> where T : IComparable<T>
    {
        // Get the default comparer that
        // sorts first by the height.
        IComparer<T> nodeComparer = Comparer<T>.Default;

        public T val; // The element to be stored 

        // index of the array from which the element is taken 
        public int r;

        // index of the next element to be picked from array 
        public int c;

        public HeapNode(T element, int i, int j)
        {
            this.val = element;
            this.r = i;
            this.c = j;
        }

        public HeapNode(T element)
        {
            this.val = element;
        }
        public HeapNode(T element, IComparer<T> comparer) : this(element)
        {
            this.nodeComparer = comparer;
        }

        public int CompareTo(HeapNode<T> obj)
        {
            if (obj == null)
                return 1;

            return nodeComparer.Compare(this.val, obj.val);
            //return this.CompareTo(obj);
        }
    }

    public class Heap<T> where T : IComparable<T>
    {
        HeapNode<T>[] harr; // Array of elements in heap 

        public bool IsMinHeap { get; private set; }

        public int Count { get; private set; }

        public void BuildHeap(HeapNode<T>[] a)
        {
            for (int x = 0; x < a.Length; x++)
                harr[x] = a[x];

            Count = harr.Length;

            // Index of last non-leaf node 
            int startIdx = (Count / 2) - 1;

            // Perform reverse level order traversal 
            // from last non-leaf node and heapify 
            // each node 
            for (int i = startIdx; i >= 0; i--)
            {
                HeapifyDown(i);
            }
        }

        public Heap(int size, bool _isMinHeap)
        {
            //??? is it max heap or Minheap
            this.IsMinHeap = _isMinHeap;

            harr = new HeapNode<T>[size];
        }

        void HeapifyUp(int i)
        {
            // check if node at index i and its parent violates 
            // the heap property
            if (i > 0 && IsMinHeap && harr[parent(i)].CompareTo(harr[i]) > 0) //greter than harr[i].val
            {
                // swap the two if heap property is violated
                swap(i, parent(i));

                // call Heapify-up on the parent
                HeapifyUp(parent(i));
            }
            else if (i > 0 && !IsMinHeap && harr[parent(i)].CompareTo(harr[i]) < 0) //less than harr[i].val
            {
                // swap the two if heap property is violated
                swap(i, parent(i));

                // call Heapify-up on the parent
                HeapifyUp(parent(i));
            }
        }

        void HeapifyDown(int i)
        {
            // get left and right child of node at index i
            int l = left(i);
            int r = right(i);

            if (IsMinHeap)
            {
                int smallest = i;

                // compare A[i] with its left and right child
                // and find smallest value
                if (l < Count && harr[l].CompareTo(harr[i]) < 0) //less than harr[i].val
                    smallest = l;

                if (r < Count && harr[r].CompareTo(harr[smallest]) < 0) //less than harr[smallest].val
                    smallest = r;

                // swap with child having lesser value and 
                // call heapify-down on the child
                if (smallest != i)
                {
                    swap(i, smallest);
                    HeapifyDown(smallest);
                }
            }
            else //max heap
            {
                int hightest = i;

                // compare A[i] with its left and right child
                // and find smallest value
                if (l < Count && harr[l].CompareTo(harr[i]) > 0) //greater than harr[i].val
                    hightest = l;

                if (r < Count && harr[r].CompareTo(harr[hightest]) > 0) //greater than harr[hightest].val
                    hightest = r;

                // swap with child having lesser value and 
                // call heapify-down on the child
                if (hightest != i)
                {
                    swap(i, hightest);
                    HeapifyDown(hightest);
                }
            }
        }

        // to get index of left child of node at index i 
        int left(int i) { return (2 * i + 1); }

        // to get index of right child of node at index i 
        int right(int i) { return (2 * i + 2); }

        int parent(int i) { return (i - 1) / 2; }

        // to get the root 
        public HeapNode<T> Top()
        {
            if (Count == 0)
                throw new IndexOutOfRangeException("index is out of range(Heap underflow)");

            return harr[0];
        }

        // A utility function to swap two min heap nodes 
        //void swap(List<MinHeapNode> arr, int i, int j)
        void swap(int i, int j)
        {
            HeapNode<T> temp = harr[i];
            harr[i] = harr[j];
            harr[j] = temp;
        }

        public HeapNode<T> Pop()
        {
            // if heap has no elements, throw an exception
            if (Count == 0)
                throw new IndexOutOfRangeException("index is out of range(Heap underflow)");

            HeapNode<T> min = harr[0];//.val;

            // replace the root of the heap with the last element of the vector
            swap(0, Count - 1);
            Count--;

            // call heapify-down on root node
            HeapifyDown(0);

            return min;
        }

        public void Push(HeapNode<T> item)
        {
            if (Count >= harr.Length)
                throw new IndexOutOfRangeException("index is out of range(Heap underflow)");

            // insert the new element to the end of the array
            harr[Count] = item;
            Count++;

            // call heapify-up procedure on last element
            HeapifyUp(Count - 1);
        }

        public void Clear()
        {
            Count =0;
        }
    }
}
