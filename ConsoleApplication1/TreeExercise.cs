﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class TreeExercise
    {
        //1379. Find a Corresponding Node of a Binary Tree in a Clone of That Tree
        public class FindCorrespondingNodeOfBinaryTreeInACloneofThatTree
        {
            //https://leetcode.com/problems/find-a-corresponding-node-of-a-binary-tree-in-a-clone-of-that-tree/
            public TreeNode GetTargetCopy(TreeNode original, TreeNode cloned, TreeNode target)
            {
                //O(N), Space O(2*N)

                Stack<TreeNode> stack = new Stack<TreeNode>();
                Stack<TreeNode> stackC = new Stack<TreeNode>();

                stack.Push(original);
                stackC.Push(cloned);

                while (stack.Any())
                {
                    TreeNode temp = stack.Pop();
                    TreeNode tempC = stackC.Pop();

                    if (temp == target)
                        return tempC;

                    if (temp.right_ptr != null)
                    {
                        stack.Push(temp.right_ptr);
                        stackC.Push(tempC.right_ptr);
                    }

                    if (temp.left_ptr != null)
                    {
                        stack.Push(temp.left_ptr);
                        stackC.Push(tempC.left_ptr);
                    }
                }

                return null;
            }
        }


        public class TreeNode
        {
            public int val;
            public TreeNode left_ptr;
            public TreeNode right_ptr;

            public TreeNode(int _val)
            {
                this.val = _val;
            }
        }

        class UniValueTree
        {
            public class TreeNode
            {
                public int val;
                public TreeNode left_ptr;
                public TreeNode right_ptr;

                public TreeNode(int _val)
                {
                    this.val = _val;
                }
            }

            /*
                Complete the funtion below
            */

            //static int Count;

            class SingleNumber
            {
                public int Count;
            }

            public static int findSingleValueTrees(TreeNode root)
            {
                SingleNumber num = new SingleNumber();
                Helper(root, num);

                return num.Count;
            }

            static bool Helper(TreeNode node, SingleNumber num)
            {
                if (node == null)
                {
                    return true;
                }

                bool isLeftSingleVal = Helper(node.left_ptr, num);
                bool isRightSingleVal = Helper(node.right_ptr, num);

                if (!isLeftSingleVal || !isRightSingleVal)
                    return false;

                if (node.left_ptr != null && node.left_ptr.val != node.val)
                    return false;
                if (node.right_ptr != null && node.right_ptr.val != node.val)
                    return false;

                num.Count++;
                return true;
            }
        }

        class BstChecker
        {
            public class TreeNode
            {
                public int val;
                public TreeNode left_ptr;
                public TreeNode right_ptr;

                public TreeNode(int _val)
                {
                    this.val = _val;
                }
            }

            /*
                Complete the function below
            */
            public static bool isBST(TreeNode root)
            {
                bool result = helper(root, int.MinValue, int.MaxValue);
                return result;
            }

            private static bool helper(TreeNode root, int Min, int Max)
            {
                if (root == null)
                    return true;

                //check with Min and Max boundary
                if (root.val < Min || root.val > Max)
                    return false;

                bool isLeftBst = helper(root.left_ptr, Min, root.val);
                bool isRightBst = helper(root.right_ptr, root.val, Max);

                if (!isLeftBst || !isRightBst)
                    return false;

                return true;
            }
        }

        class PrintAllPath
        {
            public class TreeNode
            {
                public int val;
                public TreeNode left_ptr;
                public TreeNode right_ptr;

                public TreeNode(int _val)
                {
                    this.val = _val;
                }
            }

            //[[1,2,3],[2,3,4]]
            public static List<List<int>> allPathsOfABinaryTree(TreeNode root)
            {
                List<List<int>> result = new List<List<int>>();
                helper(root, result, new List<int>());
                return result;
            }

            private static void helper(TreeNode root, List<List<int>> result, List<int> slate)
            {
                if (root == null)
                {
                    return;
                }
                
                slate.Add(root.val);

                //for left copy the path
                if (root.left_ptr == null && root.right_ptr == null)
                    result.Add(new List<int>(slate));

                helper(root.left_ptr, result, slate);
                helper(root.right_ptr, result, slate);

                slate.RemoveAt(slate.Count - 1);
            }

        }

        class TreeTraversal
        {
            public static void InOrder_Iterative(TreeNode root)
            {
                InOrderHelper(root);
            }
            public static void InOrder_Recursive(TreeNode root)
            {
                if (root == null)
                    return;

                InOrder_Recursive(root.left_ptr);
                Console.WriteLine(root.val + " ");
                InOrder_Recursive(root.right_ptr);
            }

            private static void InOrderHelper(TreeNode root)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                TreeNode temp = root;

                while (stack.Any() || temp != null)
                {
                    if (temp != null)
                    {
                        stack.Push(temp);
                        temp = temp.left_ptr;
                    }
                    else
                    {
                        temp = stack.Pop();
                        Console.WriteLine(temp.val + " ");
                        temp = temp.right_ptr;
                    }

                    /*
                    while (temp != null)
                    {
                        stack.Push(temp);
                        temp = temp.left_ptr;
                    }
                    temp = stack.Pop();

                    Console.WriteLine(temp.val + " ");

                    temp = temp.right_ptr;
                    */
                }
            }

            public static void PostOrder_Recursive(TreeNode root)
            {
                if (root == null)
                    return;

                PostOrder_Recursive(root.left_ptr);
                PostOrder_Recursive(root.right_ptr);
                Console.WriteLine(root.val + " ");
            }

            public static void PostOrder_Iterative_usingPreOrder(TreeNode root)
            {
                //O(N), Space O(N)
                Stack<TreeNode> stack = new Stack<TreeNode>();
                Stack<TreeNode> stack2 = new Stack<TreeNode>();
                stack.Push(root);

                //PreOrder (reverse LEFT/RIGHT)
                while(stack.Count > 0 )
                {
                    TreeNode temp = stack.Pop();
                    stack2.Push(temp);
                    
                    if (temp.left_ptr != null)
                        stack.Push(temp.left_ptr);
                    if (temp.right_ptr != null)
                        stack.Push(temp.right_ptr);
                }
               
                while(stack2.Count > 0 )
                {
                    Console.WriteLine(stack2.Pop().val + " ");
                }
            }

            public static void PostOrder_Iterative_OneStack(TreeNode root)
            {
                //O(N), Space O(N)
                Stack<TreeNode> stack = new Stack<TreeNode>();
                
                while (stack.Any() || root != null)
                {
                    if (root != null)
                    {
                        stack.Push(root);
                        root = root.left_ptr;
                    }
                    else
                    {
                        TreeNode temp = stack.Peek().right_ptr;
                        if (temp == null) // no right child
                        {
                            temp = stack.Pop();
                            Console.WriteLine(temp.val + " ");

                            //Check if the POPPED node is RIGHT child of item on STACK.
                            //if YES, then pop the top of the stack, as we have processed both sides...
                            while (stack.Count > 0 && temp == stack.Peek().right_ptr)
                            {
                                temp = stack.Pop();
                                Console.WriteLine(temp.val + " ");
                            }
                        }
                        else 
                        {
                            root = temp;
                        }
                    }
                }
            }

            public static void Boundary_Traversal(TreeNode root)
            {
                void Boundary_Traversal_Helper(TreeNode root1)
                {
                    Console.WriteLine(root1.val + " ");

                    LeftBoundary_Traversal_Helper(root1.left_ptr);

                    BottomBoundary_Traversal_Helper(root1);

                    RightBoundary_Traversal_Helper(root1.right_ptr);
                }
                void LeftBoundary_Traversal_Helper(TreeNode root1)
                {
                    if (root1 == null)
                        return;
                    //excape the Leaf node
                    if (root1.left_ptr == null && root1.right_ptr == null)
                        return;

                    Console.WriteLine(root1.val + " ");

                    if (root1.left_ptr != null)
                        LeftBoundary_Traversal_Helper(root1.left_ptr);
                    else
                    {
                        if (root1.right_ptr != null)
                            LeftBoundary_Traversal_Helper(root1.right_ptr);
                    }
                }
                void RightBoundary_Traversal_Helper(TreeNode root1)
                {
                    if (root1 == null)
                        return;
                    //excape the Leaf node
                    if (root1.left_ptr == null && root1.right_ptr == null)
                        return;

                    if (root1.right_ptr != null)
                        RightBoundary_Traversal_Helper(root1.right_ptr);
                    else
                    {
                        if (root1.left_ptr != null)
                            RightBoundary_Traversal_Helper(root1.left_ptr);
                    }

                    Console.WriteLine(root1.val + " ");
                }
                void BottomBoundary_Traversal_Helper(TreeNode root1)
                {
                    if (root1 == null)
                        return;

                    //excape the Leaf node
                    if (root1.left_ptr == null && root1.right_ptr == null)
                        Console.WriteLine(root1.val + " ");

                    BottomBoundary_Traversal_Helper(root1.left_ptr);
                    BottomBoundary_Traversal_Helper(root1.right_ptr);
                }

                Boundary_Traversal_Helper(root);
            }
        }

        class TreeToDublyCircularLL
        {
            public class TreeNode
            {
                public int val;
                public TreeNode left_ptr;
                public TreeNode right_ptr;

                public TreeNode(int _val)
                {
                    this.val = _val;
                }
            }

            public static TreeNode Convert(TreeNode root)
            {
                TreeNode head = Helper(root);
                return head;
            }

            private static TreeNode Helper(TreeNode root)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                TreeNode temp = root;

                TreeNode newHead = null;
                TreeNode prev = null;
                while (stack.Count > 0 || temp != null)
                {
                    //Inorder traversal *******
                    while (temp != null)
                    {
                        stack.Push(temp);
                        temp = temp.left_ptr;
                    }
                    temp = stack.Pop();

                    //store new head
                    if (newHead == null)
                        newHead = temp;
                    if (prev != null)
                    {
                        temp.left_ptr = prev;
                        prev.right_ptr = temp;
                    }
                    prev = temp; //update previous pointer
                    
                    //Inorder traversal *******
                    //Console.WriteLine(temp.val + " ");
                    temp = temp.right_ptr;
                }

                if (newHead != null)
                {
                    newHead.left_ptr = prev;
                    prev.right_ptr = newHead;
                }

                return newHead;
            }

        }

        class MergeBSTIntoHeightBalancedBST
        {
            public class TreeNode
            {
                public int val;
                public TreeNode left_ptr;
                public TreeNode right_ptr;

                public TreeNode(int _val)
                {
                    this.val = _val;
                }
            }

            private class TreeNodeEnumerator// : IEnumerator<TreeNode>
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                TreeNode root = null;
                public TreeNodeEnumerator(TreeNode _root)
                {
                    root = _root;
                }

                public IEnumerable<TreeNode> Next()
                {
                    while(HasNext())
                    {
                        while(root !=null)
                        {
                            stack.Push(root);
                            root = root.left_ptr;
                        }

                        root = stack.Pop();
                        TreeNode Left = root;

                        root = root.right_ptr;
                        
                        yield return Left;
                    }
                }

                public bool HasNext()
                {
                    return stack.Count > 0 || root != null;
                }
            }

            public static TreeNode Merge(TreeNode t1, TreeNode t2)
            {
                TreeNode root = Helper(t1, t2);
                return root;
            }

            private static TreeNode Helper(TreeNode t1, TreeNode t2)
            {
                TreeNodeEnumerator enumerator1 = new TreeNodeEnumerator(t1);
                TreeNodeEnumerator enumerator2 = new TreeNodeEnumerator(t2);
                IList<int> merged = new List<int>();
                TreeNode t1N = null; TreeNode t2N = null;
                while (enumerator1.HasNext() && enumerator2.HasNext())
                {
                    if (t1N ==null) t1N = enumerator1.Next().First();
                    if (t2N == null) t2N = enumerator2.Next().First();

                    if (t1N.val < t2N.val)
                    {
                        merged.Add(t1N.val);
                        t1N = enumerator1.Next().First();
                    }
                    else if (t1N.val > t2N.val)
                    {
                        merged.Add(t2N.val);
                        t2N = enumerator2.Next().First();
                    }
                    else
                    {
                        merged.Add(t1N.val);
                        merged.Add(t2N.val);
                        t2N = enumerator2.Next().First();
                        t1N = enumerator1.Next().First();
                    }
                }

                while (enumerator1.HasNext())
                {
                    merged.Add(t1N.val);
                    t1N = enumerator1.Next().First();
                }
                while (enumerator2.HasNext())
                {
                    merged.Add(t2N.val);
                    t2N = enumerator2.Next().First();
                }

                //build the tree
                TreeNode newRoot = BuildTree(merged, 0, merged.Count - 1);

                return newRoot;
            }

            private static TreeNode BuildTree(IList<int>  merged, int start, int end)
            {
                TreeNode root = null;
                if(start <= end)
                {
                    int mid = start + (end - start) / 2;
                    root = new TreeNode(merged[mid]);

                    root.left_ptr = BuildTree(merged, start, mid - 1);
                    root.right_ptr = BuildTree(merged, mid + 1, end);
                }
                return root;
            }
        }
        
        class build_balanced_bst
        {
            public class TreeNode
            {
                public int val;
                public TreeNode left_ptr;
                public TreeNode right_ptr;

                public TreeNode(int _val)
                {
                    this.val = _val;
                }
            }

            static TreeNode Build(int[] a)
            {
                return Helper(a, 0, a.Length - 1);
            }

            private static TreeNode Helper(int[] a, int left, int right)
            {
                if (left > right)
                    return null;

                int mid = left + (right - left) / 2;

                TreeNode root = new TreeNode(a[mid]);
                root.left_ptr = Helper(a, left, mid - 1);
                root.right_ptr = Helper(a, mid + 1, right);

                return root;
            }
        }

        class LowestCommonAncestor
        {
            public class Node
            {
                public int val;
                public Node left;
                public Node right;

                public Node(int _val)
                {
                    this.val = _val;
                }
            }

            public static int Get(Node root, Node a, Node b)
            {
                IList<int> pathA = new List<int>();
                IList<int> pathB = new List<int>();

                bool aFound = FindNodeAndBuildPath(root, a, pathA);
                bool bFound = FindNodeAndBuildPath(root, b, pathB);

                if (!aFound || !bFound)
                    return 0;

                int i = 0;
                int j = 0;

                int lastCommon = 0;
                while(i < pathA.Count && j < pathB.Count)
                {
                    if (pathA[i] != pathB[j])
                        break;

                    lastCommon = pathA[i];
                    i++;
                    j++;
                }

                return lastCommon;
            }

            private static bool FindNodeAndBuildPath(Node root, Node target, IList<int> path)
            {
                if (root == null)
                    return false;

                path.Add(root.val);

                if (root.val == target.val)
                    return true;

                bool foundOnRight = false;
                bool foundOnLeft = FindNodeAndBuildPath(root.left, target, path);
                if(!foundOnLeft)
                    foundOnRight = FindNodeAndBuildPath(root.right, target, path);

                if(foundOnLeft || foundOnRight)
                    return true;
                
                path.RemoveAt(path.Count - 1);

                return false;
            }
        }
        
        class UpSideDown
        {
            public class TreeNode
            {
                public int val;
                public TreeNode left_ptr;
                public TreeNode right_ptr;

                public TreeNode(int _val)
                {
                    this.val = _val;
                }
            }

            static TreeNode newRoot = null;

            public static TreeNode Turn(TreeNode  root)
            {   
                Helper(root);
                return newRoot;
            }

            private static TreeNode Helper(TreeNode root)
            {
                if (root == null)
                    return null;
                
                TreeNode left = Helper(root.left_ptr);
                TreeNode right = Helper(root.right_ptr);

                if (newRoot == null)
                    newRoot = root;

                if (left != null)
                {
                    left.left_ptr = right;
                    left.right_ptr = root;

                    root.right_ptr = null;
                    root.left_ptr = null;

                    //string l = ((left.left_ptr != null) ? left.left_ptr.val.ToString() : "NULL") + "<-";
                    //string r = "->" + ((left.right_ptr != null) ? left.right_ptr.val.ToString() : "NULL");
                    //Console.WriteLine("(" + l + left.val + r + ") goes up");
                }

                return root;
            }
        }

        class kth_Maximum_element
        {
            public class TreeNode
            {
                public int val;
                public TreeNode left_ptr;
                public TreeNode right_ptr;

                public TreeNode(int _val)
                {
                    val = _val;
                    left_ptr = null;
                    right_ptr = null;
                }
            };

            public static int Get(TreeNode root, int k)
            {
                return Helper(root, k);
            }

            private static int Helper(TreeNode root, int k)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();

                int x = 0;
                while (stack.Count > 0 || root != null)
                {
                    while (root != null)
                    {
                        stack.Push(root);
                        root = root.right_ptr;
                    }

                    root = stack.Pop();
                    if (--k == 0)
                        return root.val;

                    //Console.WriteLine(root.val + " ");

                    root = root.left_ptr;
                }

                return 0;
            }

        }

        class Height_KAry_tree
        {
            public class TreeNode
            {
                public int val;
                public List<TreeNode> children;

                public TreeNode(int _val)
                {
                    val = _val;
                }
            };

            static int find_height(TreeNode root)
            {
                return Helper(root, 0);
            }

            private static int Helper(TreeNode root, int level)
            {
                int maxAtRoot = level;
                foreach (TreeNode child in root.children)
                {
                    int newLevel = Helper(child, level + 1);
                    maxAtRoot = Math.Max(maxAtRoot, newLevel);
                }

                return maxAtRoot;
            }

        }

        public static void runTest()
        {
            TreeNode root = new TreeNode(20);
            root.left_ptr = new TreeNode(8);
            root.left_ptr.left_ptr= new TreeNode(4);
            root.left_ptr.right_ptr = new TreeNode(12);
            root.left_ptr.right_ptr.left_ptr = new TreeNode(10);
            root.left_ptr.right_ptr.right_ptr = new TreeNode(14);

            root.right_ptr = new TreeNode(22);
            root.right_ptr.right_ptr = new TreeNode(25);
            //root.right_ptr.left_ptr.left_ptr = new TreeNode(10);
            //root.right_ptr.left_ptr.right_ptr = new TreeNode(12);
            //root.right_ptr.left_ptr.right_ptr.left_ptr = new TreeNode(11);

            //kth_Maximum_element.Get(root,3);

            //TreeTraversal.Boundary_Traversal(root);
            //LowestCommonAncestor.Get(root, new LowestCommonAncestor.Node(2), new LowestCommonAncestor.Node(5));
            //MergeBSTIntoHeightBalancedBST.Merge(root, root);
            //TreeToDublyCircularLL.Convert(root);
            //BstChecker.isBST(root);
            //UniValueTree.findSingleValueTrees(root);

        }

    }

}
