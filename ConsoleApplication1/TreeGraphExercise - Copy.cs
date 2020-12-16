using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Amazon.CloudFront.Model;

namespace ConsoleApplication1
{
    public class Node
    {
        public Node pre;
        public Node next;
        public int val;
        public int key;

        public Node(int val)
        {
            this.val = val;
        }

    }


    public class BSTIterator
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }

        Stack<TreeNode> stack;

        public BSTIterator(TreeNode root)
        {
            stack = new Stack<TreeNode>();
            TreeNode ptr = root;
            while (ptr != null)
            {
                stack.Push(ptr);
                ptr = ptr.left;
            }
        }

        /**
        * @return whether we have a next smallest number
        */
        public bool hasNext()
        {
            return stack.Count > 0;
        }

        /**
         * @return the next smallest number
         */
        public int next()
        {
            TreeNode ptr = stack.Pop();
            int val = ptr.val;
            if (ptr.right != null)
            {
                ptr = ptr.right;
                stack.Push(ptr);
                ptr = ptr.left;
                while (ptr != null)
                {
                    stack.Push(ptr);
                    ptr = ptr.left;
                }
            }
            return val;
        }
    }

    public class TreeGraphExercise1
    {
        public static bool HasPathSum(TreeNode root, int sum)
        {
            if (root == null) return false;

            Stack<TreeNode> nodeStack = new Stack<TreeNode>();
            Stack<int> sumStack = new Stack<int>();

            nodeStack.Push(root);
            sumStack.Push(sum);

            //DFS
            while (nodeStack.Count != 0)
            {
                TreeNode cur = nodeStack.Pop();

                int rem = sumStack.Pop();

                if (cur.val == rem && cur.left == null && cur.right == null)
                    return true;

                if (cur.left != null)
                {
                    nodeStack.Push(cur.left);
                    sumStack.Push(rem - cur.val);
                }
                if (cur.right != null)
                {
                    nodeStack.Push(cur.right);
                    sumStack.Push(rem - cur.val);
                }
            }
            return false;
        }


        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public int diameter;
            public int maxSumTillLeaf;
            public int sumTillNode;
            public TreeNode(int x) { val = x; }
        }
        public class TreeNodeDepth
        {
            public int depth;
            public TreeNode node;
            public TreeNodeDepth(TreeNode _node, int _depth)
            {
                node = _node;
                depth = _depth;
            }
        }


        /*
         * https://www.interviewcake.com/concept/csharp/binary-tree?section=trees-graphs&course=fc1
         * Total nodes [n = Pow(2, h)​​ − 1] where h is the height/depth of the tree
         * log2(n+1) = h
         * ====================================
         * In a balanced tree, d is O(log​2(n)), so space is O(log​2(n)). And the more unbalanced the tree gets, the closer d gets to n.
         * ====================================
         * BST uses a queue and DST uses a stack 
         * ====================================
         * Tree's breadth can as much as double each time it gets one level deeper, 
         * depth-first traversal is likely to be more space-efficient than breadth-first traversal.
         * ======================================
         * Depth-first reaches leaves faster.
         * ======================================
         */
        public class BinaryTreeNode
        {

            public int Value { get; private set; }

            public BinaryTreeNode Left { get; set; }

            public BinaryTreeNode Right { get; set; }

            public BinaryTreeNode next { get; set; }

            public BinaryTreeNode Parent { get; private set; }

            public BinaryTreeNode(int value)
            {
                Value = value;
            }

            public BinaryTreeNode InsertLeft(int leftValue)
            {
                Left = new BinaryTreeNode(leftValue);
                Left.Parent = this;
                return Left;
            }

            public BinaryTreeNode InsertRight(int rightValue)
            {
                Right = new BinaryTreeNode(rightValue);
                Right.Parent = this;
                return Right;
            }
        }

        public class NodeDepthPair
        {
            public BinaryTreeNode Node { get; private set; }

            public int Depth { get; private set; }

            public NodeDepthPair(BinaryTreeNode node, int depth)
            {
                Node = node;
                Depth = depth;
            }
        }

        public class NodeBounds
        {
            public BinaryTreeNode Node { get; private set; }

            public double LowerBound { get; private set; }

            public double UpperBound { get; private set; }

            public NodeBounds(BinaryTreeNode node, double lowerBound, double upperBound)
            {
                Node = node;
                LowerBound = lowerBound;
                UpperBound = upperBound;

                Console.WriteLine(string.Format("Value {0} - Lower {1}, Upper {2}", node.Value, lowerBound, upperBound));
            }
        }

        public class GraphNode
        {
            public string Label { get; private set; }
            public ISet<GraphNode> Neighbors { get; private set; }
            public string Color { get; set; }

            public GraphNode(string label)
            {
                Label = label;
                Neighbors = new HashSet<GraphNode>();
            }

            public void AddNeighbor(GraphNode neighbor)
            {
                Neighbors.Add(neighbor);
            }
        }


        /*
         * Because we’re doing a depth first search, nodes will hold at most d nodes where d is the depth of the tree 
         * (the number of levels in the tree from the root node down to the lowest node). So we could say our space cost is O(d).
         * But we can also relate d to n. In a balanced tree, d is O(log​2(n)). And the more unbalanced the tree gets, the closer d gets to n.
         * 
         * All Right nodes will have, UpperBound = int.MaxValue 
         * All Left nodes will have, LowerBound = int.MinValue
         */
        static bool IsBinarySearchTree(BinaryTreeNode root) //O(n) time and O(n) space 
        {
            if (root == null)
            {
                return true;
            }

            // Start at the root, with an arbitrarily low lower bound
            // and an arbitrarily high upper bound
            var nodeAndBoundsStack = new Stack<NodeBounds>();
            nodeAndBoundsStack.Push(new NodeBounds(root, double.MinValue, double.MaxValue));

            // Depth-first traversal
            while (nodeAndBoundsStack.Count > 0)
            {
                var nb = nodeAndBoundsStack.Pop();
                var node = nb.Node;
                var lowerBound = nb.LowerBound;
                var upperBound = nb.UpperBound;

                // If this node is invalid, we return false right away
                if (node.Value <= lowerBound || node.Value >= upperBound)
                {
                    return false;
                }

                if (node.Left != null)
                {
                    // This node must be less than the current node
                    nodeAndBoundsStack.Push(new NodeBounds(node.Left, node.Value, root.Value));
                }

                if (node.Right != null)
                {
                    // This node must be greater than the current node
                    nodeAndBoundsStack.Push(new NodeBounds(node.Right, root.Value, node.Value));
                }
            }

            // If none of the nodes were invalid, return true
            // (at this point we have checked all nodes)
            return true;
        }

        /* 
         * BST NOT Balanced if 2 unique leaf depth difference >1
         * More than 2 unique leaf deapth, so the difference is >1 ex (2,3,4)
         * =======================================
         * Why are we doing a depth-first, because it reaches leaves faster. ****
         * ========================================
         * BST uses a queue and DFS uses a stack 
         * ========================================
         * depths will never hold more than three elements, so we can write that off as O(1).
         * Because we’re doing a depth first search, nodes will hold at most d nodes where d is the depth of the tree 
         * (the number of levels in the tree from the root node down to the lowest node). So we could say our space cost is O(d).
         * But we can also relate d to n. In a balanced tree, d is O(log​2(n)). And the more unbalanced the tree gets, the closer d gets to n.
         * ===============================================
         * In the worst case, the tree is a straight line of right children from the root where every node in that line also has a left child. 
         * The traversal will walk down the line of right children, adding a new left child to nodes at each step. When the traversal 
         * hits the rightmost node, nodes will hold half of the n total nodes in the tree. Half n is O(n), 
         * so our worst case space cost is O(n).
        */
        public static bool IsBalanced(BinaryTreeNode treeRoot) //O(n) time and O(n) space
        {
            //using DFS *** 

            // a tree with no nodes is superbalanced, since there are no leaves!
            if (treeRoot == null)
            {
                return true;
            }

            var depths = new List<int>(3);  // We short-circuit as soon as we find more than 2

            // Nodes will store pairs of a node and the node's depth
            var nodes = new Stack<NodeDepthPair>();
            nodes.Push(new NodeDepthPair(treeRoot, 0));

            while (nodes.Count > 0)
            {
                // Pop a node and its depth from the top of our stack
                var nodeDepthPair = nodes.Pop();
                var node = nodeDepthPair.Node;
                var depth = nodeDepthPair.Depth;

                // Case: we found a leaf
                if (node.Left == null && node.Right == null)
                {
                    // We only care if it's a new depth
                    if (!depths.Contains(depth))
                    {
                        depths.Add(depth);

                        // Two ways we might now have an unbalanced tree:
                        //   1) more than 2 different leaf depths
                        //   2) 2 leaf depths that are more than 1 apart
                        if (depths.Count > 2
                            || (depths.Count == 2 && Math.Abs(depths[0] - depths[1]) > 1))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    // Case: this isn't a leaf - keep stepping down

                    if (node.Left != null)
                    {
                        nodes.Push(new NodeDepthPair(node.Left, depth + 1));
                    }

                    if (node.Right != null)
                    {
                        nodes.Push(new NodeDepthPair(node.Right, depth + 1));
                    }
                }
            }

            return true;
        }



        /*
         * Our first thought might be to do an in-order traversal of the BST and return the second-to-last item. 
         * This means looking at every node in the BST. That would take O(n) time and O(h) space, 
         * where h is the max height of the tree (which is lg(n) if the tree is balanced, but could be as much as n if not).
         * =====================
         * We can do better than O(n) time and O(h) space.
         * We can do this in one walk from top to bottom of our BST. This means O(h) time (again, that's O(lg{n}) if the tree is balanced, 
         * O(n) otherwise).
         * =============
         * A clean recursive implementation will take O(h) space in the call stack, but we can bring our algorithm down to O(1) space overall.
         */
        public static int FindLargest_Recursion(BinaryTreeNode rootNode) //O(h) , O(h) - time and space
        {
            if (rootNode == null)
            {
                throw new ArgumentNullException(rootNode.GetType().Name, "Tree must have at least 1 node");
            }

            if (rootNode.Right != null)
            {
                return FindLargest_Recursion(rootNode.Right);
            }

            return rootNode.Value;
        }

        public static int FindSecondLargest_Recursion(BinaryTreeNode rootNode)
        {
            if (rootNode == null
                || (rootNode.Left == null && rootNode.Right == null))
            {
                throw new ArgumentException("Tree must have at least 2 nodes", rootNode.GetType().Name);
            }

            // Case: we're currently at largest, and largest has a left subtree,
            // so 2nd largest is largest in said subtree
            if (rootNode.Left != null && rootNode.Right == null)
            {
                return FindLargest_Recursion(rootNode.Left);
            }

            // Case: we're at parent of largest, and largest has no left subtree,
            // so 2nd largest must be current node
            if (rootNode.Right != null
                && rootNode.Right.Left == null
                && rootNode.Right.Right == null)
            {
                return rootNode.Value;
            }

            // Otherwise: step right
            return FindSecondLargest_NoRecursion(rootNode.Right);
        }

        public static int FindLargest_NoRecursion(BinaryTreeNode rootNode) //O(h) - time and O(1) space
        {
            var current = rootNode;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current.Value;
        }

        /* todo: largest element are on the right side of the tree, so traverse right
         * Todo Case1 : If we have a left subtree but not a right subtree, then the current node is the largest overall (the "rightmost") node. 
         * The second largest element must be the largest element in the left subtree. We use our FindLargest() method above to
         * find the largest in that left subtree!
         * ===================================================
         * todo Case2 : If we have a right child, but that right child node doesn't have any children, then the right child must be the
         * largest element and our current node must be the second largest element!
         * ================================================
         * Else, we have a right subtree with more than one element, so the largest and second largest are somewhere in that subtree. So we step right.
         */
        public static int FindSecondLargest_NoRecursion(BinaryTreeNode rootNode) //O(h) - time and O(1) space
        {
            if (rootNode == null
                || (rootNode.Left == null && rootNode.Right == null))
            {
                throw new ArgumentException("Tree must have at least 2 nodes",
                    rootNode.GetType().Name);
            }

            var current = rootNode;

            while (true)
            {
                // Case: current is largest and has a left subtree
                // 2nd largest is the largest in that subtree
                if (current.Left != null && current.Right == null)
                {
                    return FindLargest_NoRecursion(current.Left);
                }

                // Case: current is parent of largest, and largest has no children,
                // so current is 2nd largest
                if (current.Right != null
                    && current.Right.Left == null
                    && current.Right.Right == null)
                {
                    return current.Value;
                }

                current = current.Right;
            }
        }

        /*
         * A brute force approach would be to try every possible combination of colors until we find a legal coloring. 
         *  Our steps would be:
            1. For each possible graph coloring,
            2. If the coloring is legal, then return it
            3. Otherwise, move on to the next coloring
         * Ex. D is 3, so we can use 4 colors. 12 Nodes
         * ==============================================
         * 4^​12 combinations (every combination of 4 colors for 12 nodes). In general, we'll have to check O(D^N) colorings. 
         * And that's not all—each time we try a coloring, we have to check all M edges to see if the vertices at both ends have different colors. 
         * So, our runtime is O(M*D^N). That's exponential time since N is in an exponent.
         * ==============================================
         * Todo : SOLUTION -- 
         * Todo : We go through the nodes in one pass, assigning each node the first legal color we find.
         * Todo : In a graph with maximum degree D, each node has at most D neighbors. 
         * Todo : That means there are at most D colors taken by a node's neighbors. And we have D+1 colors, so there's always 
         * Todo : at least one color left to use.
         * Todo : When we color each node, we're careful to stop iterating over colors as soon as we find a legal color.
         * ============================
         */
        public static void ColorGraph(GraphNode[] graph, string[] colors) //O(N+M) time where N = number of nodes and M = number of edges, O(D) space, D= max Degree
        {
            foreach (var node in graph)
            {
                //Avoid looping - A loop in a graph is an edge where both ends connect to the same node:
                if (node.Neighbors.Contains(node))
                {
                    throw new ArgumentException(
                        "Legal coloring impossible for node with loop: ${node.Label}",
                        graph.GetType().Name);
                }

                // Get the node's neighbors' colors, as a set so we
                // can check if a color is illegal in constant time
                var illegalColors = new HashSet<string>(
                    from neighbor in node.Neighbors
                    where neighbor.Color != null
                    select neighbor.Color);

                // Assign the first legal color
                node.Color = colors.First(c => !illegalColors.Contains(c));
            }
        }

        /*
         * If we didn't have nodesAlreadySeen hash, our algorithm would be slower (since we'd be revisiting tons of nodes) 
         * and it might never finish (if there's no path to the end node).
         * =======================
         * We're using a Queue instead of a List because we want an efficient first-in-first-out (FIFO) 
         * structure with O(1) inserts and removes. If we used a List, appending would be O(1), 
         * but removing elements from the front would be O(n).
         */
        public static void Bfs(Dictionary<string, string[]> graph, string startNode, string endNode)
        {
            var nodesToVisit = new Queue<string>();
            nodesToVisit.Enqueue(startNode);

            // Keep track of what nodes we've already seen so we don't process them twice
            var nodesAlreadySeen = new HashSet<string>();
            nodesAlreadySeen.Add(startNode);

            while (nodesToVisit.Count > 0)
            {
                var currentNode = nodesToVisit.Dequeue();

                // Stop when we reach the end node
                if (currentNode == endNode)
                {
                    // Found it!
                    break;
                }

                foreach (var neighbor in graph[currentNode])
                {
                    if (!nodesAlreadySeen.Contains(neighbor))
                    {
                        nodesAlreadySeen.Add(neighbor);
                        nodesToVisit.Enqueue(neighbor);
                    }
                }
            }

        }

        public static string serialize(TreeNode root)
        {
            if (root == null)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            Queue<TreeNode> queue = new Queue<TreeNode>();

            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                TreeNode t = queue.Dequeue();
                if (t != null)
                {
                    sb.Append(t.val.ToString() + ",");
                    queue.Enqueue(t.left);
                    queue.Enqueue(t.right);
                }
                else
                {
                    sb.Append("#,");
                }
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static TreeNode deserialize(String data)
        {
            if (data == null || data.Length == 0)
                return null;

            String[] arr = data.Split(',');
            TreeNode root = new TreeNode(int.Parse(arr[0]));


            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            int i = 1;
            while (queue.Count != 0)
            {
                TreeNode t = queue.Dequeue();

                if (t == null)
                    continue;

                if (!arr[i].Equals("#"))
                {
                    t.left = new TreeNode(int.Parse(arr[i]));
                    queue.Enqueue(t.left);
                }
                else
                {
                    t.left = null;
                    queue.Enqueue(null);
                }
                i++;

                if (!arr[i].Equals("#"))
                {
                    t.right = new TreeNode(int.Parse(arr[i]));
                    queue.Enqueue(t.right);
                }
                else
                {
                    t.right = null;
                    queue.Enqueue(null);
                }
                i++;
            }

            return root;
        }

        private static string[] ReconstructPathFromDestination(Dictionary<string, string> howWeReachedNodes, string startNode, string endNode)
        {
            var reversedShortestPath = new List<string>();

            // Start from the end of the path and work backwards
            var currentNode = endNode;

            while (currentNode != null)
            {
                reversedShortestPath.Add(currentNode);
                currentNode = howWeReachedNodes[currentNode];
            }

            // Reverse our path to get the right order
            // by flipping it around, in place
            reversedShortestPath.Reverse();
            return reversedShortestPath.ToArray();
        }

        /*
         * Input user network as a graph in adjacency list format. 
         * Then we do a breadth-first search from the sender, stopping once we reach the recipient.
         * ====================
         * In order to recover the actual shortest path from the sender to the recipient, we do two things:
            1) during our breadth-first search, we keep track of how we reached each node, and
            2) after our breadth-first search reaches the end node, we use our dictionary to backtrack from the recipient to the sender.
         * =======
         * To make sure our breadth-first search terminates, we're careful to avoid visiting any node twice.
         * =============
         * TODO: Space complexity - The queue of nodes to visit, the mapping of nodes to previous nodes, 
         * TODO: and the final path ... they all store a constant amount of information per node. 
         * TODO: So, each data structure could take up to O(N) space if it stored information about all of our nodes. 
         * TODO: That means our overall space complexity is O(N).
         */
        public static string[] BfsGetPath(Dictionary<string, string[]> graph, string startNode, string endNode) //time O(N+M), N=number of nodes, M= number of edges. Space O(N)
        {
            if (!graph.ContainsKey(startNode))
            {
                throw new ArgumentException("Start node not in graph", startNode.GetType().Name);
            }
            if (!graph.ContainsKey(endNode))
            {
                throw new ArgumentException("End node not in graph", endNode.GetType().Name);
            }

            /*
             * We're using a Queue instead of a List because we want an efficient first-in-first-out (FIFO) 
             * structure with O(1) inserts and removes. If we used a List, appending would be O(1), 
             * but removing elements from the front would be O(n).
             */
            var nodesToVisit = new Queue<string>();
            nodesToVisit.Enqueue(startNode);

            // Keep track of how we got to each node.
            // We'll use this to reconstruct the shortest path at the end.
            // We'll ALSO use this to keep track of which nodes we've already visited (ensure BFS terminates,  avoid loop)
            var howWeReachedNodes = new Dictionary<string, string>();
            howWeReachedNodes.Add(startNode, null);

            while (nodesToVisit.Count > 0)
            {
                var currentNode = nodesToVisit.Dequeue();

                // Stop when we reach the end node
                if (currentNode == endNode)
                {
                    //reconstruct the path from endNode to startNode
                    return ReconstructPathFromDestination(howWeReachedNodes, startNode, endNode);
                }

                foreach (var neighbor in graph[currentNode])
                {
                    if (!howWeReachedNodes.ContainsKey(neighbor))
                    {
                        nodesToVisit.Enqueue(neighbor);
                        howWeReachedNodes.Add(neighbor, currentNode);
                    }
                }
            }

            // If we get here, then we never found the end node
            // so there's NO path from startNode to endNode
            return null;
        }



        //Adi

        //Route Between Nodes - BfsGetPath



        /*
         * Minimal Tree: Given a sorted (increasing order) array with unique integer elements, 
         * write an algorithm to create a binary search tree with minimal height
         * ============================
         * 1. Insert into the tree the middle element of the array.
         * 2. Insert (into the left subtree) the left subarray elements.
         * 3. Insert (into the right subtree) the right subarray elements.
         * 4. Recurse.
         */
        BinaryTreeNode createMinimalBST(int[] array)
        {
            return createMinimalBST(array, 0, array.Length - 1);
        }
        BinaryTreeNode createMinimalBST(int[] arr, int start, int end)
        {

            if (end < start)
            {
                return null;
            }
            int mid = (start + end) / 2;
            BinaryTreeNode n = new BinaryTreeNode(arr[mid]);
            n.Left = createMinimalBST(arr, start, mid - 1);
            n.Right = createMinimalBST(arr, mid + 1, end);
            return n;
        }

        /*
         * TODO: Given a binary tree, design an algorithm which creates a linked list of all the nodes 
         * at each depth (e.g., if you have a tree with depth D, you'll have D linked lists).
         * ===================
         * Though we might think at first glance that this problem requires a level-by-level traversal, this isn't actually 
         * necessary. We can traverse the graph any way that we'd like, provided we know which level we're on as we do so.
         * =====================================
         * We can implement a simple modification of the pre-order traversal algorithm, where we pass in level + 1 to the next recursive call. 
         * The code below provides an implementation using depth-first search.
         */
        void createLevelLinkedList(BinaryTreeNode root, List<LinkedList<BinaryTreeNode>> lists, int level) //O ( N) time, uses 0( log N) recursive calls
        {
            if (root == null) return; // base case

            LinkedList<BinaryTreeNode> list = null;
            if (lists.Count == level)
            { // Level not contained in list
                list = new LinkedList<BinaryTreeNode>();
                /* Levels are always traversed in order. So, if this is the first time we've
                * visited level i, we must have seen levels 0 through i - 1. We can
                 * therefore safely add the level at the end. */
                lists.Add(list);
            }
            else
            {
                list = lists[level];
            }
            list.AddFirst(root);
            createLevelLinkedList(root.Left, lists, level + 1);
            createLevelLinkedList(root.Right, lists, level + 1);
        }
        /*
         * Alternatively, we can also implement a modification of TODO : breadth-first search. 
         * With this implementation, we want to iterate through the root first, then level 2, then level 3, and so on.
         */
        List<LinkedList<BinaryTreeNode>> createLevelLinkedlist_1(BinaryTreeNode root)//O ( N) time
        {
            List<LinkedList<BinaryTreeNode>> result = new List<LinkedList<BinaryTreeNode>>();
            /* "Visit" the root */
            LinkedList<BinaryTreeNode> current = new LinkedList<BinaryTreeNode>();
            if (root != null)
            {
                current.AddFirst(root);
            }
            while (current.Count > 0)
            {
                result.Add(current); // Add previous level
                LinkedList<BinaryTreeNode> parents = current; //Go to next level
                current = new LinkedList<BinaryTreeNode>();
                foreach (BinaryTreeNode parent in parents)
                {
                    /* Visit the children*/
                    if (parent.Left != null)
                    {
                        current.AddFirst(parent.Left);
                    }
                    if (parent.Right != null)
                    {
                        current.AddFirst(parent.Right);
                    }
                }
            }
            return result;
        }

        public static IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> result = new List<IList<int>>();

            if (root == null) return result;

            Queue<TreeNodeDepth> queue = new Queue<TreeNodeDepth>();
            queue.Enqueue(new TreeNodeDepth(root, 0));

            while (queue.Count != 0)
            {
                TreeNodeDepth queueHead = queue.Dequeue();

                int level = queueHead.depth;
                if (result.Count < level + 1)
                    result.Add(new List<int>());
                result[level].Add(queueHead.node.val);

                if (queueHead.node.left != null)
                    queue.Enqueue(new TreeNodeDepth(queueHead.node.left, level + 1));
                if (queueHead.node.right != null)
                    queue.Enqueue(new TreeNodeDepth(queueHead.node.right, level + 1));
            }
            return result;
        }


        /*
         * Successor: Write an algorithm to find the "next" node (i.e., in-order successor) of a given node in a BST. 
         * You may assume that each node has a link to its parent.
         * ====================
         * TODO: We know that the order goes left subtree, then current side, then right subtree. So, the next node we visit should be on the right side.
         * ======================
         * 1) If right subtree of node is not NULL, then successor lies in right subtree. 
         *      -> Go to right subtree and return the node with minimum key value in right subtree.
         * 2) If right sbtree of node is NULL, then succ is one of the ancestors. 
         *      -> Travel up using the parent pointer until you see a node which is left child of it’s parent. The parent of such a node is the successor
         * */
        static BinaryTreeNode InOrderSuccessor(BinaryTreeNode n)
        {

            if (n == null) return null;

            /* Found right children -> return leftmost node of right subtree. */
            if (n.Right != null)
            {
                return leftMostChild(n.Right);
            }
            else
            {
                BinaryTreeNode q = n;
                BinaryTreeNode x = q.Parent;
                // Go up until we're on left instead of right
                while (x != null && x.Left != q)
                {
                    q = x;
                    x = x.Parent;
                }
                return x;
            }
        }

        static TreeNode InOrderSuccessor1(TreeNode root, TreeNode p)
        {
            TreeNode succ = null;
            while (root != null)
            {
                if (p.val < root.val)
                {
                    succ = root;
                    root = root.left;
                }
                else
                    root = root.right;
            }
            return succ;
        }

        static BinaryTreeNode leftMostChild(BinaryTreeNode n)
        {
            if (n == null) return null;
            while (n.Left != null)
                n = n.Left;

            return n;
        }





        public static String alienOrder(String[] words)
        {
            Dictionary<char, HashSet<char>> map = new Dictionary<char, HashSet<char>>();

            for (int i = 0; i < words.Length; i++)
            {
                foreach (char c in words[i])
                {
                    if (!map.ContainsKey(c))
                    {
                        map.Add(c, new HashSet<char>());

                    }
                }
            }

            for (int i = 0; i < words.Length - 1; i++)
            {
                String curr = words[i];
                String next = words[i + 1];

                int length = Math.Min(curr.Length, next.Length);
                for (int j = 0; j < length; j++)
                {
                    char c1 = curr[j];
                    char c2 = next[j];
                    if (c1 != c2)
                    {
                        //c1 comes before c2
                        map[c1].Add(c2);
                        break;
                    }
                }
            }

            //now map is constructed. key is the node and values are all its neighbors. now simply perform topo on the map

            Stack<char> topoStack = new Stack<char>();
            int[] visited = new int[26];

            foreach (char key in map.Keys)
            {
                if (containsCycle(map, visited, topoStack, key))
                    return "";
            }

            StringBuilder result = new StringBuilder();
            while (topoStack.Count > 0)
            {
                result.Append(topoStack.Pop());
            }
            return result.ToString();
        }

        public static bool containsCycle(Dictionary<char, HashSet<char>> map, int[] visited, Stack<char> topoStack, char node)
        {

            //already visited in current cycle
            if (visited[node - 'a'] == 1)
                return true;

            if (visited[node - 'a'] == 2)
                return false;

            visited[node - 'a'] = 1;

            foreach (char v in map[node])
                if (containsCycle(map, visited, topoStack, v))
                    return true;

            visited[node - 'a'] = 2;

            topoStack.Push(node);
            return false;
        }

        public TreeNode ConstructMaximumBinaryTree(int[] nums)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            for (int i = 0; i < nums.Length; i++)
            {
                TreeNode curr = new TreeNode(nums[i]);
                while (stack.Count != 0 && stack.Peek().val < nums[i])
                {
                    curr.left = stack.Pop();
                }
                if (stack.Count != 0)
                {
                    stack.Peek().right = curr;
                }
                stack.Push(curr);
            }

            return stack.Count == 0 ? null : stack.Reverse().First();
        }

        public bool FindTarget(TreeNode root, int k)
        {
            HashSet<int> map = new HashSet<int>();
            return find(root, k, map);
        }


        public bool find(TreeNode node, int k, HashSet<int> hashSet)
        {
            if (node == null)
            {
                return false;
            }
            if (hashSet.Contains(k - node.val))
            {
                return true;
            }
            hashSet.Add(node.val);
            return find(node.left, k, hashSet) || find(node.right, k, hashSet);
        }

        public static int sumNumbers(TreeNode root)
        {
            if (root == null)
                return 0;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            int total = 0;//root.val;
            while (stack.Count != 0)
            {
                TreeNode node = stack.Pop();

                if (node.left == null && node.right == null)
                    total += node.val;

                if (node.right != null)
                {
                    stack.Push(node.right);
                    node.right.val = (node.val * 10) + node.right.val;
                }
                if (node.left != null)
                {
                    stack.Push(node.left);
                    //total = (node.val *10) + node.left.val;
                    node.left.val = (node.val * 10) + node.left.val;
                }
                //total = (total * 10);
            }
            return total;

            /*
            if (root == null) return 0;

            int sum = 0;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count!=0)
            {
                TreeNode temp = q.Dequeue();

                if (temp.left == null && temp.right == null)
                    sum += temp.val;

                int num = (temp.val * 10);

                if (temp.left != null)
                {
                    temp.left.val = temp.left.val + num;
                    q.Enqueue(temp.left);
                }

                if (temp.right != null)
                {
                    temp.right.val = temp.right.val + num;
                    q.Enqueue(temp.right);
                }
            }

            return sum;
            */
        }


        public bool FindTarget1(TreeNode root, int k)
        {
            List<int> nums = new List<int>();
            inorder(root, nums);
            for (int i = 0, j = nums.Count - 1; i < j;)
            {
                if (nums[i] + nums[j] == k)
                    return true;
                if (nums[i] + nums[j] < k)
                    i++;
                else j--;
            }
            return false;
        }
        public void inorder(TreeNode root, List<int> nums)
        {
            if (root == null)
                return;
            inorder(root.left, nums);
            nums.Add(root.val);
            inorder(root.right, nums);


        }
        List<int> inorderTraversal(TreeNode root)
        {
            List<int> inorder = new List<int>();
            Stack<TreeNode> nodes = new Stack<TreeNode>();

            if (root == null)
                return inorder;

            while (root != null || nodes.Any())
            {
                //push left children if available
                while (root != null)
                {
                    nodes.Push(root);
                    root = root.left;
                }

                //retrieve top node and store its right child if exists
                root = nodes.Pop();

                inorder.Add(root.val);

                root = root.right;
            }
            return inorder;
        }

        List<int> postorderTraversal(TreeNode root)
        {
            List<int> inorder = new List<int>();
            Stack<TreeNode> nodes = new Stack<TreeNode>();

            if (root == null)
                return inorder;

            while (root != null || nodes.Any())
            {
                //push left children if available
                while (root != null)
                {
                    nodes.Push(root);
                    root = root.left;
                }

                //retrieve top node and store its right child if exists
                root = nodes.Pop();

                inorder.Add(root.val);

                root = root.right;
            }
            return inorder;
        }

        public bool validTree(int n, int[,] edges)
        {
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                List<int> list = new List<int>();
                map.Add(i, list);
            }

            for (int i = 0; i <= edges.GetLength(0) - 1; i++)
            {
                map[edges[i, 0]].Add(edges[i, 1]);
                map[edges[i, 1]].Add(edges[i, 0]);
            }


            bool[] visited = new bool[n];

            if (!helper(0, -1, map, visited))
                return false;

            foreach (bool b in visited)
            {
                if (!b)
                    return false;
            }

            return true;
        }

        public bool helper(int curr, int parent, Dictionary<int, List<int>> map, bool[] visited)
        {
            if (visited[curr])
                return false;

            visited[curr] = true;

            foreach (int i in map[curr])
            {
                if (i != parent && !helper(i, curr, map, visited))
                {
                    return false;
                }
            }

            return true;
        }


        public TreeNode lowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            /*
             To find the lowest common ancestor, we need to find where is p and q and a way to track their ancestors. 
             A parent pointer for each node found is good for the job. After we found both p and q, we create a set 
             of p's ancestors. Then we travel through q's ancestors, the first one appears in p's is our answer.
             */
            Dictionary<TreeNode, TreeNode> parent = new Dictionary<TreeNode, TreeNode>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            parent.Add(root, null);
            stack.Push(root);

            while (!parent.ContainsKey(p) || !parent.ContainsKey(q))
            {
                TreeNode node = stack.Pop();
                if (node.left != null)
                {
                    parent.Add(node.left, node);
                    stack.Push(node.left);
                }
                if (node.right != null)
                {
                    parent.Add(node.right, node);
                    stack.Push(node.right);
                }
            }
            List<TreeNode> ancestors = new List<TreeNode>();
            while (p != null)
            {
                ancestors.Add(p);
                p = parent[p];
            }
            while (!ancestors.Contains(q))
                q = parent[q];
            return q;
        }

        public class UndirectedGraphNode
        {
            public int label;
            public IList<UndirectedGraphNode> neighbors;
            public UndirectedGraphNode(int x) { label = x; neighbors = new List<UndirectedGraphNode>(); }
        };
        public static UndirectedGraphNode CloneGraph(UndirectedGraphNode node)
        {

            Queue<UndirectedGraphNode> queue = new Queue<UndirectedGraphNode>();
            Dictionary<int, UndirectedGraphNode> map = new Dictionary<int, UndirectedGraphNode>();


            if (node == null)
            {
                return null;
            }
            queue.Enqueue(node);

            UndirectedGraphNode nodeToReturn = new UndirectedGraphNode(node.label);
            map.Add(nodeToReturn.label, nodeToReturn);

            while (queue.Count > 0)
            {
                UndirectedGraphNode head = queue.Dequeue();
                foreach (UndirectedGraphNode neighbor in head.neighbors)
                {
                    if (map.ContainsKey(neighbor.label))
                    {
                        map[head.label].neighbors.Add(map[neighbor.label]);
                    }
                    else
                    {
                        queue.Enqueue(neighbor);
                        UndirectedGraphNode neighborNodeCopy = new UndirectedGraphNode(neighbor.label);
                        map.Add(neighbor.label, neighborNodeCopy);
                        map[head.label].neighbors.Add(neighborNodeCopy);

                    }
                }
            }
            return nodeToReturn;

        }


        public class TreeLinkNode
        {
            int val;
            public TreeLinkNode left;
            public TreeLinkNode right;
            public TreeLinkNode next;

            public TreeLinkNode(int _val)
            {
                val = _val;
            }
        }

        public static void connect(TreeLinkNode root)
        {
            TreeLinkNode dummy = new TreeLinkNode(0); //points to the left most node
            TreeLinkNode t = dummy;
            while (root != null)
            {
                if (root.left != null)
                {
                    t.next = root.left; //point to left node
                    t = t.next;
                }
                if (root.right != null)
                {
                    t.next = root.right; //left point to right node
                    t = t.next;
                }

                root = root.next;

                if (root == null) //for right most node
                {
                    root = dummy.next; //point to Root's left child

                    //dummy.next = null;
                    dummy = new TreeLinkNode(0);

                    t = dummy;
                }
            }
        }

        bool IsSameTreeR(TreeNode x, TreeNode y)
        {
            // if both trees are empty, return true
            if (x == null && y == null)
                return true;

            // if both trees are non-empty and value of their root node matches,
            // recurse for their left and right sub-tree
            return (x != null && y != null) && (x.val == y.val) &&
                 IsSameTreeR(x.left, y.left) &&
                 IsSameTreeR(x.right, y.right);
        }

        //O(Log(N)), space O(Log(N))
        public static bool IsSameTree(TreeNode p, TreeNode q)
        {
            // if both trees are empty, return true
            if (p == null && q == null)
                return true;
            // if first tree is empty (& second tree is non-empty), return false
            if (p == null)
                return false;
            // if second tree is empty (& first tree is non-empty), return false
            if (q == null)
                return false;

            Stack<KeyValuePair<TreeNode, TreeNode>> stack = new Stack<KeyValuePair<TreeNode, TreeNode>>();
            stack.Push(new KeyValuePair<TreeNode, TreeNode>(p, q));

            while (stack.Any())
            {
                var data = stack.Pop();
                // pop top pair from the stack and process it
                TreeNode x = data.Key; TreeNode y = data.Value;

                // if value of their root node don't match, return false
                if (x.val != y.val)
                    return false;

                // if left subtree of both x and y exists, push their addresses
                // to stack else return false if only one left child exists
                if (x.left != null && y.left != null)
                    stack.Push(new KeyValuePair<TreeNode, TreeNode>(x.left, y.left));

                else if (x.left != null || y.left != null)
                    return false;

                // if right subtree of both x and y exists, push their addresses
                // to stack else return false if only one right child exists

                if (x.right != null && y.right != null)
                    stack.Push(new KeyValuePair<TreeNode, TreeNode>(x.right, y.right));

                else if (x.right != null || y.right != null)
                    return false;
            }
            return false;
        }

        public IList<int> PostorderTraversal(TreeNode root)
        {
            IList<int> result = new List<int>();

            if (root == null)
                return result;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                TreeNode temp = stack.Peek();

                if (temp.left == null && temp.right == null)  //for leaf node
                {
                    TreeNode pop = stack.Pop();
                    result.Add(pop.val);
                }
                else
                {
                    if (temp.right != null) //if right is present
                    {
                        stack.Push(temp.right);
                        temp.right = null;
                    }
                    if (temp.left != null) //got to left
                    {
                        stack.Push(temp.left);
                        temp.left = null;
                    }
                }
            }
            return result;
        }

        void levelOrderTraversal(TreeNode root)
        {
            if (root == null)
                return;

            // create an empty queue and enqueue root node
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            // run till queue is not empty
            while (queue.Any())
            {
                // process each node in queue and enqueue their
                // non-empty left and right child to queue
                TreeNode curr = queue.Dequeue();

                Console.WriteLine(curr.val);

                if (curr.left != null)
                    queue.Enqueue(curr.left);

                if (curr.right != null)
                    queue.Enqueue(curr.right);
            }
        }

        static void rightView(TreeNode root)
        {
            if (root == null)
                return;

            // create an empty queue and enqueue root node
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            // run till queue is not empty
            while (queue.Any())
            {
                // calculate number of nodes in current level
                int size = queue.Count;
                int i = 0;
                // process every node of current level and enqueue their
                // non-empty left and right child to queue
                while (i++ < size)
                {
                    // process each node in queue and enqueue their
                    // non-empty left and right child to queue
                    TreeNode curr = queue.Dequeue();

                    // if this is first node of current level, print it
                    if (i == size)
                        Console.WriteLine(curr.val);

                    if (curr.left != null)
                        queue.Enqueue(curr.left);

                    if (curr.right != null)
                        queue.Enqueue(curr.right);
                }
            }
        }



        public IList<int> InorderTraversal(TreeNode root)
        {

            IList<int> result = new List<int>();
            if (root == null)
                return result;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            //Left Root Right               
            while (stack.Count > 0)
            {
                TreeNode top = stack.Peek();

                if (top.left != null) //go to left node
                {
                    stack.Push(top.left);
                    top.left = null;
                }
                else
                {
                    result.Add(top.val); //Add left node

                    stack.Pop();

                    if (top.right != null) //Go to right
                    {
                        stack.Push(top.right);
                    }
                }
            }

            return result;
        }

        public IList<int> PreorderTraversal(TreeNode root)
        {

            IList<int> result = new List<int>();
            if (root == null)
                return result;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            //ROOT > LEFT > RIGHT
            while (stack.Count > 0)
            {

                TreeNode temp = stack.Pop();
                result.Add(temp.val);

                //Do Right first then Left, as we are keeping on Stack (LIFO)
                if (temp.right != null)
                {
                    stack.Push(temp.right);
                }

                if (temp.left != null)
                {
                    stack.Push(temp.left);
                }
            }

            return result;
        }

        void preorderIterative(TreeNode root)
        {
            // return if tree is empty
            if (root == null)
                return;

            // create an empty stack and push root node
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            // start from root node (set current node to root node)
            TreeNode curr = root;

            // run till stack is not empty
            while (stack.Any())
            {
                // if current node is not null, print it and push its right child
                // to the stack and move to its left child
                if (curr != null)
                {
                    Console.WriteLine(curr.val);

                    if (curr.right != null)
                        stack.Push(curr.right);

                    curr = curr.left;
                }
                // else if current node is null, we pop a node from the stack
                // set current node to the popped node
                else
                {
                    curr = stack.Pop();
                }
            }
        }

        // Function to print spiral order traversal of given binary tree
        void spiralOrderTraversal1(TreeNode root)
        {
            if (root == null)
                return;

            // create an empty double ended queue and push root node
            Queue<TreeNode> deque = new Queue<TreeNode>();      // or use deque
            deque.Enqueue(root);

            // flag used to differentiate between odd or even level
            bool flag = false;

            // run till deque is not empty
            while (deque.Any())
            {
                // calculate number of nodes in current level
                int nodeCount = deque.Count;

                // print left to right
                if (flag)
                {
                    // process each node of current level and push their
                    // non-empty left and right child to deque
                    while (nodeCount > 0)
                    {
                        // pop from front if flag is true
                        TreeNode curr = deque.Dequeue();

                        Console.Write(curr.val + " ");

                        // push left child to end followed by right child 
                        // if flag is true

                        if (curr.left != null)
                            deque.Enqueue(curr.left);

                        if (curr.right != null)
                            deque.Enqueue(curr.right);

                        nodeCount--;
                    }
                }

                // print right to left
                else
                {
                    // process each node of current level and push their
                    // non-empty right and left child to queue
                    while (nodeCount > 0)
                    {
                        // Important - pop from back if flag is false
                        TreeNode curr = deque.Dequeue();

                        Console.Write(curr.val + " ");   // print front node

                        // Important - push right child to front followed by left
                        // child if flag is false

                        if (curr.right != null)
                            deque.Enqueue(curr.right);

                        if (curr.left != null)
                            deque.Enqueue(curr.left);

                        nodeCount--;
                    }
                }

                // flip the flag for next level
                flag = !flag;
                Console.WriteLine();
            }
        }


        static void PrintLevelOrder(TreeNode root)
        {
            if (root == null) return;

            // create an empty queue and enqueue root node
            LinkedList<TreeNode> queue = new LinkedList<TreeNode>();
            queue.AddFirst(root);

            int depth = 0;
            // run till queue is not empty
            while (true)
            {
                int nodeCount = queue.Count;
                if (nodeCount == 0)
                    return;

                // process every node of current level and enqueue their
                // non-empty left and right child to queue
                while (nodeCount -- > 0)
                {
                    TreeNode temp = queue.First.Value;
                    queue.Remove(temp);
                    
                    Console.Write(temp.val);

                    if (depth % 2 == 0) {
                        
                        if (temp.left != null)
                            queue.AddFirst(temp.left);

                        if (temp.right != null)
                            queue.AddFirst(temp.right);
                    }
                    else
                    {
                        if (temp.right != null)
                            queue.AddLast(temp.right);

                        if (temp.left != null)
                            queue.AddLast(temp.left);
                    }
                }
                depth++;
                Console.WriteLine();
            }
        }

        static void deleteTree(TreeNode root)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Any())
            {
                TreeNode temp= queue.Dequeue();

                Console.Write(temp.val + " ");
                if (temp.left != null)
                    queue.Enqueue(temp.left);
                if (temp.right != null)
                    queue.Enqueue(temp.right);

                temp = null;
            }
        }

        private static List<int> MyPostOrder(TreeNode root)
        {
            List<int> lst = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            while (stack.Any())
            {
                TreeNode temp = stack.Peek();
                if(temp.left != null)
                {
                    stack.Push(temp.left);
                    temp.left = null;
                }
                else
                {
                    Console.Write(temp.val + " ");
                    stack.Pop();
                    if (temp.right != null)
                    {
                        stack.Push(temp.right);
                    }
                }
            }

            return lst;

        }


        static TreeNode findRightNode(TreeNode root, int val)
        {
            // return null if tree is empty
            if (root == null)
                return null;
            // create an empty queue and enqueue root node
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            // pointer to store current node
            TreeNode front = null;
            // run till queue is not empty
            while (queue.Any())
            {
                front = queue.Dequeue();

                // if desired node is found, return its next right node
                if (front.val == val)
                {
                    // if next right node doesn't exists, return null
                    if (!queue.Any())
                        return null;
                    return queue.Peek();
                }
                if (front.left != null)
                    queue.Enqueue(front.left);
                if (front.right != null)
                    queue.Enqueue(front.right);
            }
            return null;
        }




        public static void printZigZagTraversal(TreeNode rootNode)
        {
            // declare two stacks  
            Stack<TreeNode> currentLevel = new Stack<TreeNode>();
            Stack<TreeNode> nextLevel = new Stack<TreeNode>();

            // push the root  
            currentLevel.Push(rootNode);
            bool leftToRight = true;

            // check if stack is empty  
            while (currentLevel.Count > 0)
            {
                // pop out of stack  
                TreeNode node = currentLevel.Pop();

                // print the data in it  
                Console.Write(node.val + " ");

                // store data according to current  
                // order.  
                if (leftToRight)
                {
                    if (node.right != null)
                    {
                        nextLevel.Push(node.right);
                    }
                    if (node.left != null)
                    {
                        nextLevel.Push(node.left);
                    }                    
                }
                else
                {
                    if (node.left != null)
                    {
                        nextLevel.Push(node.left);
                    }
                    if (node.right != null)
                    {
                        nextLevel.Push(node.right);
                    }
                }
                //change levels
                if (currentLevel.Count == 0)
                {
                    leftToRight = !leftToRight;
                    Stack<TreeNode> temp = currentLevel;
                    currentLevel = nextLevel;
                    nextLevel = temp;
                }
            }
        }

        static void PostOrder(TreeNode root, TreeNode parentNode, Queue<KeyValuePair<TreeNode, TreeNode>> queue)
        {
            if (root == null)
                return;

            PostOrder(root.left, root, queue);
            PostOrder(root.right, root, queue);

            queue.Enqueue(new KeyValuePair<TreeNode, TreeNode>(root, parentNode));
        }

        public static TreeNode pruneUtil(TreeNode root, int k, INT sum)
        {
            // Base Case
            if (root == null)
            {
                return null;
            }
            // Initialize left and right sums as sum from root to
            // this node (including this node)
            INT lsum = new INT(sum.v + root.val);
            INT rsum = new INT(lsum.v);
            // Recursively prune left
            // and right subtrees
            root.left = pruneUtil(root.left, k, lsum);
            root.right = pruneUtil(root.right, k, rsum);
            // Get the maximum of left and right sums
            sum.v = Math.Max(lsum.v, rsum.v);
            // If maximum is smaller than k, then this node must be deleted
            if (sum.v < k)
            {
                root = null;
            }
            return root;
        }
        public class INT
        {
            public int v;
            public INT(int a)
            {
                v = a;
            }
        }

        public static void PostOrder_sudip(TreeNode root, int K)
        {
            Stack<KeyValuePair<TreeNode, TreeNode>> stack = new Stack<KeyValuePair<TreeNode, TreeNode>>();
            Stack<KeyValuePair<TreeNode, TreeNode>> destStack = new Stack<KeyValuePair<TreeNode, TreeNode>>();
            //Queue<TreeNode> targetQueue = new Queue<TreeNode>();

            root.sumTillNode = root.val;

            stack.Push(new KeyValuePair<TreeNode, TreeNode>(root, null));

            while (stack.Any())
            {
                var temp = stack.Pop();
                destStack.Push(new KeyValuePair<TreeNode, TreeNode>(temp.Key, temp.Value));

                if(temp.Key.left != null)
                {
                    stack.Push(new KeyValuePair<TreeNode, TreeNode>(temp.Key.left, temp.Key));
                }
                if (temp.Key.right != null)
                {
                    stack.Push(new KeyValuePair<TreeNode, TreeNode>(temp.Key.right, temp.Key));
                }

                /*
                if (temp.left ==null && temp.right == null)
                {
                    temp = stack.Pop();
                    /*if (temp.sumTillNode > K)// >= 0)
                    {
                        targetQueue.Enqueue(temp);
                    }
                }
                else
                {
                    if(temp.right != null)
                    {
                        //temp.right.sumTillNode = temp.sumTillNode + temp.right.val;
                        stack.Push(temp.right);
                    }
                    if (temp.left != null)
                    {
                        //temp.left.sumTillNode = temp.sumTillNode + temp.left.val;
                        stack.Push(temp.left);
                    }
                }*/
            }
            /*
            PostOrder(root, null, queue);

            while (queue.Any())
            {
                KeyValuePair<TreeNode, TreeNode> temp = queue.Dequeue();
                TreeNode parentNode = temp.Value;
                TreeNode node = temp.Key;

                //we will drop node which has only 1 child
                if (node.left != null && node.right == null) //has only left child
                {
                    //if the node is left child of its parent
                    if (parentNode.left == node)
                    {
                        //point parent's left child to node left child
                        parentNode.left = node.left;
                        node.left = null;
                    }
                    else //if the node is right child of its parent
                    {
                        //point parent's right child to node left child
                        parentNode.right = node.left;
                        node.right = null;
                    }
                }
                if (node.left == null && node.right != null) //has only right child
                {
                    //if the node is left child of its parent
                    if (parentNode.left == node)
                    {
                        //point parent's left child to node right child
                        parentNode.left = node.right;
                        node.left = null;
                    }
                    else //if the node is right child of its parent
                    {
                        //point parent's right child to node right child
                        parentNode.right = node.right;
                        node.right = null;
                    }                        
                }
            }*/
        }

        public static TreeNode BuildTreeFromParentArray(int[] parentArr)
        {
            //build TreeNode array with value as array index
            TreeNode[] tree = new TreeNode[parentArr.Length];
            for(int x=0;x< parentArr.Length;x++)
            {
                tree[x] = new TreeNode(x);
            }

            TreeNode root = null;
            for (int x = 0; x < parentArr.Length; x++)
            {
                //mark root if array value = -1
                if (parentArr[x] == -1)
                {
                    root = tree[x];
                }
                else
                {
                    //Access parent tree node by array value

                    //if parent node's left child is empty, then add current Treenode as left child, else right
                    if (tree[parentArr[x]].left == null)
                    {
                        tree[parentArr[x]].left = tree[x];
                    }
                    else
                    {
                        tree[parentArr[x]].right = tree[x];
                    }
                }
            }
            return root;
        }



        class A
        {
            public int ans = int.MinValue;
        }

        /* Function to find height of a tree */
        static int height(TreeNode root, A a)
        {
            if (root == null)
                return 0;

            int left_height = height(root.left, a);

            int right_height = height(root.right, a);

            // update the answer, because diameter of a  
            // tree is nothing but maximum value of  
            // (left_height + right_height + 1) for each node  
            a.ans = Math.Max(a.ans, 1 + left_height +
                            right_height);

            return 1 + Math.Max(left_height, right_height);
        }


        static void printDiagonal(TreeNode root, int diagonal, SortedDictionary<int,
List<int>> map)
        {
            // Base case
            if (root == null)
                return;
            // get the list at the particular d value
            List<int> k = map.ContainsKey(diagonal)? map[diagonal]:null;
            // k is null then create a vector and store the data
            if (k == null)
            {
                k = new List<int>();
                k.Add(root.val);
            }
            // k is not null then update the list
            else
            {
                k.Add(root.val);
            }
            // Store all nodes of same line together as a vector
            if(map.ContainsKey(diagonal))
                map[diagonal] = k;
            else
                map.Add(diagonal, k);
            
            
            // Increase the vertical distance if left child
            printDiagonal(root.left, diagonal + 1, map);
            // Vertical distance remains same for right child
            printDiagonal(root.right, diagonal, map);
        }


        // Function to print the diagonal elements of given binary tree
        static void printDiagonalSum(TreeNode root)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            TreeNode dummy = new TreeNode(-1);

            //store whole right tree on queue
            queue.Enqueue(root);
            while (root.right != null)
            {
                queue.Enqueue(root.right);
                root = root.right;
            }
            //add a dummy node as a marker
            queue.Enqueue(dummy);

            int diagonalSum = 0;

            while (queue.Any())
            {
                TreeNode temp = queue.Dequeue();
                //not a dummy node
                if(temp.val != dummy.val)
                {
                    diagonalSum += temp.val;
                    //go to the left node, and traverse right most node  of the left node
                    TreeNode leftNode = temp.left;
                    if (leftNode != null)
                    {
                        //store left node
                        queue.Enqueue(leftNode);

                        //store all right nodes into queue
                        while (leftNode.right != null)
                        {
                            queue.Enqueue(leftNode.right);
                            leftNode = leftNode.right;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(diagonalSum);
                    diagonalSum = 0;

                    //if there are node lest in queue, then add a dummy node as line delimiter
                    if (queue.Count >0)
                        queue.Enqueue(dummy);
                }
            }
        }


        static int printMaxSumPath_InOrder(TreeNode root)
        {
            //store node and level
            Stack<KeyValuePair<TreeNode, int>> stack = new Stack<KeyValuePair<TreeNode, int>>();
            stack.Push(new KeyValuePair<TreeNode, int>(root, 1));
            List<int> lst = new List<int>();

            int pathSumMax = int.MinValue;

            while (stack.Any())
            {
                KeyValuePair<TreeNode, int> temp = stack.Pop();
                int level = temp.Value;
                if (level > lst.Count)
                    lst.Add(temp.Key.val);
                else
                    lst[level - 1] = temp.Key.val;
            
                //on leaf node, print the list till Node Level
                if (temp.Key.left == null && temp.Key.right == null)
                {
                    int pathSum = 0;
                    for (int x = 0; x < level; x++)
                    {
                        pathSum += lst[x];
                        Console.Write(lst[x] + " ");
                    }
                    Console.WriteLine();
                    pathSumMax = Math.Max(pathSumMax, pathSum);
                }
                else
                {
                    if (temp.Key.right != null)
                    {
                        stack.Push(new KeyValuePair<TreeNode, int>(temp.Key.right, level + 1));
                    }
                    if (temp.Key.left != null)
                    {
                        stack.Push(new KeyValuePair<TreeNode, int>(temp.Key.left, level + 1));
                    }
                }
            }

            return pathSumMax;
        }



        public static void LCA(TreeNode root, int n1, int n2)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            // Creata a map to store ancestors of n1 
            HashSet<int> ancestors = new HashSet<int>();
            //ancestors.Add(root.val);

            while (stack.Any())
            {
                TreeNode temp = stack.Peek();

                if (temp.left == null && temp.right == null)
                {
                    stack.Pop();
                }
                else
                {
                    ancestors.Add(temp.val);
                    if (temp.val == n1)
                    {
                        break;
                    }

                    if (temp.right != null)
                    {
                        stack.Push(temp.right);
                    }
                    if (temp.left != null)
                    {
                        stack.Push(temp.left);
                    }
                }
            }

        }


        public static int InOrder_BST_Predecessor(TreeNode root, int K)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode prev = null;

            while(root !=null || stack.Any())
            {
                while(root != null)
                {
                    stack.Push(root);

                    root = root.left;
                }

                root = stack.Pop();

                if (prev != null && root.val == K)
                    return prev.val;

                prev = root;
                root = root.right;
            }
            return -1;
        }

        public static TreeNode findPredecessor(TreeNode root, int key)
        {
            TreeNode prec = null;
            while (root != null)
            {
                // if given key is less than the root node, visit left subtree
                if (key < root.val)
                {
                    root = root.left;
                }
                // if given key is more than the root node, visit right subtree
                else if (key > root.val)
                {
                    // update predecessor to current node before visiting
                    // right subtree
                    prec = root;
                    root = root.right;
                }
                // if node with key's value is found, predecessor is maximum
                // value node in its left subtree (if any)
                else
                {
                    if (root.left != null)
                    {
                        prec = findMaximum(root.left);
                    }
                    break;
                }
            }
            // return predecessor if any
            return prec;
        }

        public static TreeNode findMaximum(TreeNode root)
        {
            while (root.right != null)
            {
                root = root.right;
            }
            return root;
        }


        public static void Reverse_InOrder_Update(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            int sum = 0;
            while (root != null || stack.Any())
            {
                while (root != null)
                {
                    stack.Push(root);
                    root = root.right;
                }
                
                root = stack.Pop();
                sum += root.val;
                root.val = sum;

                Console.Write(root.val + " ");
                root = root.left;
            }
        }


        // Function to print the  nodes of a  
        // binary tree in boundary traversal order 
        static void boundaryTraversal(TreeNode root)
        {
            if (root == null)
                return;
            

                // If there is only 1 node print it 
                // and return 
                if (root.left == null && root.right ==null)
                {
                    Console.WriteLine(root.val);
                    return;
                }

            // List to store order of traversed 
            // nodes 
            //vector<Node*> list;
            //list.push_back(root);
            //Console.Write(root.val + " "); 

            // Traverse left boundary without root 
            // and last node 
            TreeNode temp = root;
            while (temp !=null)
            {
                Console.Write(temp.val + " ");
                temp = temp.left;
            }

            Console.WriteLine();

            // BFS designed to only include leaf nodes 
            temp = root;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(temp);
            while (q.Any())
            {
                int depthCount = q.Count;
                while(depthCount -- > 0)
                {
                    temp = q.Dequeue();
                    if (temp.left == null && temp.right == null)
                    {
                        Console.Write(temp.val + " ");
                    }
                    if (temp.left !=null)
                    {
                        q.Enqueue(temp.left);
                    }
                    if (temp.right !=null)
                    {
                        q.Enqueue(temp.right);
                    }
                }                
            }

            Console.WriteLine();

            // Traverse right boundary without root 
            // and last node 
            Stack<TreeNode> stack = new Stack<TreeNode>();
            temp = root.right;
            while (temp !=null)
            {
                stack.Push(temp);
                temp = temp.right;
            }

            while (stack.Any())
            {
                Console.Write(stack.Pop().val + " ");
            }
            return;
            
        }









        public static void runTest()
        {
            
                

            int[] a1 = new int[] { 8, 3, 6, 1, 4, 7, 10, 14, 13 };
            int[] b1 = new int[] { 8, 10, 14, 3, 6, 4, 1, 7, 13 };

            

            TreeNode t11 = new TreeNode(5);
            t11.left = new TreeNode(3);
            t11.right = new TreeNode(8);
            t11.left.left = new TreeNode(2);
            t11.left.right = new TreeNode(4);            

            t11.right.left = new TreeNode(6);
            t11.right.right= new TreeNode(10);


            TreeNode t1111 = new TreeNode(16);

            TreeNode t111 = new TreeNode(15);

            t1111.left = t111;

            t111.left = new TreeNode(10);
            t111.right = new TreeNode(20);
            
            t111.left.left = new TreeNode(8);
            t111.left.right = new TreeNode(12);

            t111.right.left = new TreeNode(18);
            t111.right.right = new TreeNode(25);

            //t111.left.right.left = new TreeNode(10);
            //t111.left.right.right = new TreeNode(14);

            //t111.right.right.right = new TreeNode(3);

            //NewInOrder(t11);//, 18);
            truncate(t1111, 9, 12);

            //PostOrder_sudip(t111, 20);
            pruneUtil(t111, 9, new INT(0));


            TreeNode t13 = new TreeNode(8);
            t13.left = new TreeNode(3);
            t13.right = new TreeNode(5);

            t13.left.left = new TreeNode(10);
            t13.left.right = new TreeNode(2);

            t13.right.left = new TreeNode(4);
            t13.right.right = new TreeNode(6);

            //t13.right.left.left = new TreeNode(7);
            //t13.right.left.right = new TreeNode(8);

            //t13.right.right.left = new TreeNode(10);
            //t13.right.right.right = new TreeNode(0);

            

            binarytree_count_recursive(t13);





            t13.left.left.left = new TreeNode(211);
            t13.left.left.right = new TreeNode(212);
            t13.left.right.left = new TreeNode(221);
            t13.left.right.right = new TreeNode(222);

            t13.right.left.left = new TreeNode(311);
            t13.right.left.right = new TreeNode(312);
            t13.right.right.left = new TreeNode(321);
            t13.right.right.right = new TreeNode(322);

            //height(t13, 1);
            //spiralOrderTraversal(t13);
            printSpecificLevelOrder(t13);


            UndirectedGraphNode n = new UndirectedGraphNode(1);
            UndirectedGraphNode zeron = new UndirectedGraphNode(0);
            UndirectedGraphNode twon = new UndirectedGraphNode(2);

            n.neighbors.Add(zeron);
            n.neighbors.Add(twon);

            zeron.neighbors.Add(n);
            zeron.neighbors.Add(twon);

            twon.neighbors.Add(twon);
            twon.neighbors.Add(n);
            twon.neighbors.Add(zeron);

            CloneGraph(n);


            TreeLinkNode t1 = new TreeLinkNode(1);
            t1.left = new TreeLinkNode(2);
            t1.right = new TreeLinkNode(3);
            /*TreeLinkNode tr1 = t1.right = new TreeLinkNode(3);
            tl1.left = new TreeLinkNode(4);
            tl1.right = new TreeLinkNode(5);
            tr1.left = new TreeLinkNode(6);
            tr1.right = new TreeLinkNode(7);
            */

            connect(t1);

            /*

            //[-2147483648,null,2147483647]
            var root = new BinaryTreeNode(2);
            var left = root.InsertLeft(1);
            var right = root.InsertRight(3);
            /*var left1 = left.InsertLeft(20);
            var right1 = left.InsertRight(40);
            var left2 = left1.InsertLeft(10);
            var left3 = right.InsertLeft(70);
            var right3 = right.InsertRight(90);
            var left4 = left3.InsertLeft(60);

            right3.InsertLeft(85);
            right3.InsertRight(100);
            */

            //bool b1 = IsBinarySearchTree(root);

            var left11 = new TreeNode(1);
            left11.left = new TreeNode(2);
            left11.right = new TreeNode(3);

            left11.right.left = new TreeNode(4);
            left11.right.right = new TreeNode(5);

            string sssss = serialize(left11);
            deserialize(sssss);

            //sumNumbers(left11);
            //LevelOrder(left11);

            TreeGraphExercise1.HasPathSum(left11, 22);


            var strArr = new string[]
            {
                "wrt",
                "wrf",
                "er",
                "ett",
                "rftt"
            };
            alienOrder(strArr);


            /*
            b1= IsBalanced(root);
            FindSecondLargest_NoRecursion(root);
            FindSecondLargest_Recursion(root);
            */
            //----------------------------------
            var a = new GraphNode("a");
            var b = new GraphNode("b");
            var c = new GraphNode("c");

            a.AddNeighbor(b);
            b.AddNeighbor(a);
            b.AddNeighbor(c);
            c.AddNeighbor(b);

            var graph = new GraphNode[] { a, b, c };



            /*var root = new BinaryTreeNode(20);
            var left = root.InsertLeft(8);
            var right1 = left.InsertRight(12);
            var right3 = right1.InsertRight(14);
            */

            //var y = InOrderSuccessor(right3);
        }


        // Function to print all nodes of a given level from left to right
        static bool printLevelLeftToRight(TreeNode root, int level)
        {
            if (root == null)
            {
                Console.WriteLine("Leaf Node - printLevelLeftToRight");
                return false;
            }


            if (level == 1)
            {
                Console.WriteLine(root.val + " - printLevelLeftToRight");
                return true;
            }

            //Console.WriteLine(("Call printLevelLeftToRight ->" + root.val).PadLeft(level * 40)+" ");
            printme(("Call printLevelLeftToRight ->" + root.val), level);

            // process left child before right child
            bool left = printLevelLeftToRight(root.left, level - 1);
            bool right = printLevelLeftToRight(root.right, level - 1);

            //Console.WriteLine(("Call End printLevelLeftToRight ->" + root.val).PadLeft(level * 40) + " ");

            printme(("Call printLevelLeftToRight ->" + root.val + "*"), level);

            return left || right;
        }

        // Function to print all nodes of a given level from right to left
        static bool printLevelRightToLeft(TreeNode root, int level)
        {
            if (root == null)
            {
                Console.WriteLine("Leaf Node - printLevelRightToLeft");
                return false;
            }
            if (level == 1)
            {
                Console.WriteLine(root.val + " -printLevelRightToLeft");
                return true;
            }

            //Console.WriteLine(("Call printLevelRightToLeft ->" + root.val).PadLeft(level * 40) + " ");
            printme(("Call printLevelRightToLeft ->" + root.val), level);


            // process right child before left child
            bool right = printLevelRightToLeft(root.right, level - 1);
            bool left = printLevelRightToLeft(root.left, level - 1);

            printme(("Call printLevelRightToLeft ->" + root.val + "*"), level);

            return right || left;
        }



        // Function to print level order traversal of given binary tree
        static void LevelOrderTraversal(TreeNode root)
        {
            /*if (root == null)
                return;

            int _height = height(root);
            for (int i = 1; i <= _height; i++)
            {
                printGivenLevel(root, i);
            }*/
        }

        public static void printSpiral(TreeNode node)
        {
            int h = height(node);

            /* ltr -> left to right. If this  
            variable is set then the given 
            label is transversed from left to right */
            bool ltr = false;
            for (int i = 1; i <= h; i++)
            {
                printGivenLevel(node, i, ltr);

                /*Revert ltr to traverse next  
                  level in opposite order*/
                ltr = !ltr;
            }
        }

        /* Print nodes at the given level */
        public static void printGivenLevel(TreeNode root, int level, bool ltr)
        {
            if (root == null)
            {
                return;
            }
            if (level == 1)
            {
                Console.Write(root.val + " ");
            }
            else if (level > 1)
            {
                if (ltr != false)
                {
                    printGivenLevel(root.left, level - 1, ltr);
                    printGivenLevel(root.right, level - 1, ltr);
                }
                else
                {
                    printGivenLevel(root.right, level - 1, ltr);
                    printGivenLevel(root.left, level - 1, ltr);
                }
            }
        }

        public static int height(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                /* compute height of each subtree */
                int lheight = height(root.left);
                int rheight = height(root.right);

                return Math.Max(lheight, rheight) + 1;
            }
        }

        static void printme(string str, int level)
        {
            string str1 = String.Concat(Enumerable.Repeat("-", level));

            Console.WriteLine(str1 + str);// str.PadLeft(level * 30, '-') + " ");
        }


        /* Given a perfect binary tree, print its nodes in specific 
      level order */
        static void printSpecificLevelOrder1(TreeNode node)
        {
            if (node == null)
                return;

            // Let us print root and next level first 
            Console.Write(node.val);

            //  Since it is perfect Binary Tree, right is not checked 
            if (node.left != null)
                Console.Write(" " + node.left.val + " " + node.right.val);

            // Do anything more if there are nodes at next level in 
            // given perfect Binary Tree 
            if (node.left.left == null)
                return;

            // Create a queue and enqueue left and right children of root 
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(node.left);
            q.Enqueue(node.right);

            // We process two nodes at a time, so we need two variables 
            // to store two front items of queue 
            TreeNode first = null, second = null;

            // traversal loop 
            while (q.Any())
            {
                // Pop two items from queue 
                first = q.Dequeue();
                second = q.Dequeue();

                // Print children of first and second in reverse order 
                Console.Write(" " + first.left.val + " " + second.right.val);
                Console.Write(" " + first.right.val + " " + second.left.val);

                // If first and second have grandchildren, enqueue them 
                // in reverse order 
                if (first.left.left != null)
                {
                    q.Enqueue(first.left);
                    q.Enqueue(second.right);

                    q.Enqueue(first.right);
                    q.Enqueue(second.left);
                }
            }
        }



        static void printSpecificLevelOrderUtil(TreeNode root, Stack<TreeNode> s)
        {
            if (root == null)
                return;

            // Create a queue and enqueue left and right 
            // children of root 
            Queue<TreeNode> q = new Queue<TreeNode>();

            q.Enqueue(root.left);
            q.Enqueue(root.right);

            // We process two nodes at a time, so we 
            // need two variables to store two front 
            // items of queue 
            TreeNode first = null, second = null;

            // traversal loop 
            while (q.Any())
            {
                // Pop two items from queue 
                first = q.Dequeue();
                second = q.Dequeue();

                // Push first and second node's chilren 
                // in reverse order 
                s.Push(second.left);
                s.Push(first.right);
                s.Push(second.right);
                s.Push(first.left);

                // If first and second have grandchildren, 
                // enqueue them in specific order 
                if (first.left.left != null)
                {
                    q.Enqueue(first.right);
                    q.Enqueue(second.left);
                    q.Enqueue(first.left);
                    q.Enqueue(second.right);
                }
            }
        }

        /* Given a perfect binary tree, print its nodes in 
           specific level order */
        static void printSpecificLevelOrder(TreeNode root)
        {
            //create a stack and push root 
            Stack<TreeNode> s = new Stack<TreeNode>();

            //Push level 1 and level 2 nodes in stack 
            s.Push(root);

            // Since it is perfect Binary Tree, right is 
            // not checked 
            if (root.left != null)
            {
                s.Push(root.right);
                s.Push(root.left);
            }

            // Do anything more if there are nodes at next 
            // level in given perfect Binary Tree 
            if (root.left.left != null)
                printSpecificLevelOrderUtil(root, s);

            // Finally pop all Nodes from stack and prints 
            // them. 
            while (s.Any())
            {
                Console.Write(s.Pop().val + " ");
            }
        }



        /* Given a binary tree, return true if the tree is complete 
       else false */
        static bool isCompleteBT(TreeNode root)
        {
            // Base Case: An empty tree is complete Binary Tree 
            if (root == null)
                return true;

            // Create an empty queue 
            Queue<TreeNode> queue = new Queue<TreeNode>();

            // Create a flag variable which will be set true 
            // when a non full node is seen 
            bool flag = false;

            // Do level order traversal using queue. 
            queue.Enqueue(root);
            while (queue.Any())
            {
                TreeNode temp_node = queue.Dequeue();

                /* Check if left child is present*/
                if (temp_node.left != null)
                {
                    // If we have seen a non full node, and we see a node 
                    // with non-empty left child, then the given tree is not 
                    // a complete Binary Tree 
                    if (flag == true)
                        return false;

                    // Enqueue Left Child 
                    queue.Enqueue(temp_node.left);
                }
                // If this a non-full node, set the flag as true 
                else
                    flag = true;

                /* Check if right child is present*/
                if (temp_node.right != null)
                {
                    // If we have seen a non full node, and we see a node 
                    // with non-empty right child, then the given tree is not 
                    // a complete Binary Tree 
                    if (flag == true)
                        return false;

                    // Enqueue Right Child 
                    queue.Enqueue(temp_node.right);
                }
                // If this a non-full node, set the flag as true 
                else
                    flag = true;
            }
            // If we reach here, then the tree is complete Bianry Tree 
            return true;
        }




        static int binarytree_count_recursive(TreeNode root)
        {
            int count = 1;
            if (root.left != null)
            {
                count += binarytree_count_recursive(root.left);
            }
            if (root.right != null)
            {
                count += binarytree_count_recursive(root.right);
            }
            return count;
        }
        // Perform in-order traversal of the array and fill array A[]
        static void inorder(TreeNode root, bool[] A, int i)
        {
            if (root == null)
                return;

            // recurse with index 2i+1 for left node
            inorder(root.left, A, 2 * i + 1);

            // mark index i as visited
            A[i] = true;

            // recurse with index 2i+2 for right node
            inorder(root.right, A, 2 * i + 2);
        }

        // Function to check if given binary tree is complete binary tree or not
        // call as isComplete(root, n) where n is number of nodes in the tree
        bool isComplete(TreeNode root)
        {
            // return if tree is empty
            if (root == null)
                return true;

            //get node count
            int node_count = binarytree_count_recursive(root);

            // construct an auxiliary boolean array of size n
            bool[] A = new bool[node_count];

            // fill array A[]
            inorder(root, A, 0);

            // check if all positions in the array are filled or not
            for (int i = 0; i < node_count; i++)
                if (!A[i])
                    return false;

            return true;
        }


        // Recursive function to do a pre-order traversal of the tree and fill map Here node has
        // 'dist' horizontal distance from the root of the tree
        static void printBottom(TreeNode node, int dist, SortedDictionary<int, int> map)
        {
            // base case: empty tree
            if (node == null)
                return;

            // keep updating the Map so that it will have the last entry from
            // each level(vertically) and that will the bottom view of the tree
            if (map.ContainsKey(dist))
            {
                map[dist] = node.val;
            }
            else
            {
                map.Add(dist, node.val);
            }

            // recurse for left subtree by decreasing horizontal distance by 1
            printBottom(node.left, dist - 1, map);
            // recurse for right subtree by increasing horizontal distance by 1
            printBottom(node.right, dist + 1, map);
        }

        // Function to perform vertical traversal of a given binary tree
        static void printBottom(TreeNode root)
        {
            // create an empty map where
            // key -> relative horizontal distance of the node from root node and
            // value -> nodes present at same horizontal distance
            //We need the key to be sorted in ASC order, we are using SortedDictionary, in Java, its TreeMap
            SortedDictionary<int, int> map = new SortedDictionary<int, int>();

            // do pre-order traversal of the tree and fill the map
            printBottom(root, 0, map);

            // traverse the map and print vertical nodes
            foreach (var item in map)
            {
                Console.WriteLine(item.Value + " ");
            }
        }


        // Recursive function to invert binary Tree using preorder traversal
        static void invertBinaryTree(TreeNode root)
        {
            // base case: if tree is empty
            if (root == null)
                return;

            // swap left subtree with right subtree
            TreeNode temp = root.left;
            root.left = root.right;
            root.right = temp;

            // invert left subtree
            invertBinaryTree(root.left);

            // invert right subtree
            invertBinaryTree(root.right);
        }






        // Function to check if given node is a leaf node or not
        static bool isLeaf(TreeNode node)
        {
            return (node.left == null && node.right == null);
        }


        public static List<string> binaryTreePaths(TreeNode root)
        {
            List<string> paths = new List<string>();
            if (root == null)
                return paths;

            Stack<TreeNode> node_stack = new Stack<TreeNode>();
            Stack<string> path_stack = new Stack<string>();
            node_stack.Push(root);
            path_stack.Push(root.val.ToString());

            TreeNode node;
            string path;
            while (node_stack.Any())
            {
                node = node_stack.Pop();
                path = path_stack.Pop();

                if ((node.left == null) && (node.right == null))
                {
                    //paths.Add(path);

                    for (int i = paths.Count - 1; i >= 0; i--)
                        Console.Write(paths[i] + " -> ");

                    Console.WriteLine();


                }

                if (node.right != null)
                {
                    node_stack.Push(node.right);
                    path_stack.Push(path + "->" + node.right.val);
                }
                if (node.left != null)
                {
                    node_stack.Push(node.left);
                    path_stack.Push(path + "->" + node.left.val);
                }

            }
            return paths;
        }




        /* Utility function that prints out an array on a line. */
        public static void printArray(List<int> ints, int len)
        {
            int i;
            for (i = 0; i <= len; i++)
            {
                Console.Write(ints[i] + " ");
            }
            Console.WriteLine("");
        }


        public static void printPaths_Iterative(TreeNode root)
        {
            if (root == null)
            {
                return;
            }

            Stack<KeyValuePair<TreeNode, int>> stack = new Stack<KeyValuePair<TreeNode, int>>();
            stack.Push(new KeyValuePair<TreeNode, int>(root, 0));
            List<int> path = new List<int>();
            while (stack.Any())
            {
                KeyValuePair<TreeNode, int> temp = stack.Pop();
                TreeNode node = temp.Key;

                /* append this node to the path array */
                if (path.Count == temp.Value)
                    path.Add(node.val);
                else
                {
                    path[temp.Value] = node.val;
                }
                //pathLen++;

                /* it's a leaf, so print the path that led to here  */
                if (node.left == null && node.right == null)
                {
                    printArray(path, temp.Value);
                }
                else
                {
                    //Preorder, using Stack, LIFO, so calling right first
                    if (node.right != null)
                        stack.Push(new KeyValuePair<TreeNode, int>(node.right, temp.Value + 1));

                    if (node.left != null)
                        stack.Push(new KeyValuePair<TreeNode, int>(node.left, temp.Value + 1));
                }
            }
        }




        // Function to find maximum width of the tree using level order
        // traversal of given binary tree
        void maxWidth(TreeNode root)
        {
            // return if tree is empty
            if (root == null)
                return;

            // create an empty queue and enqueue root node
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            // pointer to store current node
            TreeNode curr = null;

            // stores maximum width
            int max = 0;

            // run till queue is not empty
            while (queue.Any())
            {
                // calculate number of nodes in current level
                // This is equal to width of current level
                int width = queue.Count;

                // update maximum width if number of nodes in current level
                // is more than maximum width found so far
                if (max < width)
                    max = width;

                // process every node of current level and enqueue their
                // non-empty left and right child to queue
                while (width-- > 0)
                {
                    curr = queue.Dequeue();

                    if (curr.left != null)
                        queue.Enqueue(curr.left);

                    if (curr.right != null)
                        queue.Enqueue(curr.right);
                }
            }

            Console.WriteLine("Maximum width is " + max);
        }




        // Function to convert a normal binary tree to Left-child
        // right-sibling (LC-RS) binary tree
        static void convertLCRS(TreeNode root)
        {
            // base case: empty tree
            if (root == null)
                return;

            // recursively convert left and right subtree first
            convertLCRS(root.left);
            convertLCRS(root.right);

            // if left child is empty, then make right child as left's
            // and set right to null
            if (root.left == null)
            {
                root.left = root.right;
                root.right = null;
            }

            // if left child already exists, then make right child of the
            // left child to point to right child of current node and
            // set current right child as null
            else
            {
                root.left.right = root.right;
                root.right = null;
            }
        }



        // Recursive function to insert an key into BST
        public static TreeNode insert(TreeNode root, int key)
        {
            // if the root is null, create a new node an return it
            if (root == null)
            {
                return new TreeNode(key);
            }

            // if given key is less than the root node, recurse for left subtree
            if (key < root.val)
            {
                root.left = insert(root.left, key);
            }
            // if given key is more than the root node, recurse for right subtree
            else
            {
                root.right = insert(root.right, key);
            }

            return root;
        }


        // main function
        public static void main1()
        {
            TreeNode root = null;
            int[] keys = { 15, 10, 20, 8, 12, 16, 25 };

            foreach (int key in keys)
            {
                root = insert(root, key);
            }
        }
























        public static TreeNode truncate(TreeNode root, int min, int max)
        {
            // base case
            if (root == null)
            {
                return root;
            }

            // recursively truncate left and right subtree first
            root.left = truncate(root.left, min, max);
            root.right = truncate(root.right, min, max);

            //if (root.val < min || root.val > max)
            //    return null;

            if (root.val < min)// || root.val > max)
                root = root.right;
            else if (root.val > max)// || root.val > max)
                root = root.left;

            return root;
        }
    }
}
