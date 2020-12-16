using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class GraphExercise
    {
        class Graph
        {
            public static List<List<int>> buildGraph(int n, int[][] edges, bool isDirected)
            {
                List<List<int>> adjList = new List<List<int>>();
                // allocate memory for adjacency list
                for (int i = 0; i < n; i++)
                    adjList.Add(new List<int>());

                //setting up edges
                foreach (int[] edge in edges)
                {
                    adjList[edge[0]].Add(edge[1]);
                    if (!isDirected)
                        adjList[edge[1]].Add(edge[0]);
                }

                return adjList;
            }

            public static void FillArray(int[] arr, int val)
            {
                for (int i = 0; i < arr.Length; i++)
                    arr[i] = val;
            }

            public static List<int[]> getNeighbours(int[][] grid, int r, int c)
            {
                List<int[]> neighbours = new List<int[]>();

                if (r + 1 < grid.Length)
                    neighbours.Add(new int[] { r + 1, c });
                if (c + 1 < grid[0].Length)
                    neighbours.Add(new int[] { r, c + 1 });
                if (r - 1 >= 0)
                    neighbours.Add(new int[] { r - 1, c });
                if (c - 1 >= 0)
                    neighbours.Add(new int[] { r, c - 1 });
                return neighbours;
            }

            public static List<int[]> getNeighbours(string[] grid, int r, int c)
            {
                List<int[]> neighbours = new List<int[]>();

                if (r + 1 < grid.Length)
                    neighbours.Add(new int[] { r + 1, c });
                if (c + 1 < grid[0].Length)
                    neighbours.Add(new int[] { r, c + 1 });
                if (r - 1 >= 0)
                    neighbours.Add(new int[] { r - 1, c });
                if (c - 1 >= 0)
                    neighbours.Add(new int[] { r, c - 1 });
                return neighbours;
            }

            public static int[] NumToRowCol(int[][] grid, int num)
            {
                int maxSquare = grid.Length * grid.Length;
                int row = (maxSquare - num) / grid.Length;

                int col = (num - 1) % (2 * grid.Length);
                if (col >= grid.Length)
                    col = (2 * grid.Length - 1) - col;

                return new int[] { row, col };
            }

            public static List<int> getSnakeLadderNeighbours(int[][] grid, int node)
            {
                List<int> neighbours = new List<int>();
                for (int i = 1; i <= 6; i++)
                {
                    var nxt = node + i;

                    if (nxt > grid.Length * grid.Length)
                        continue;

                    neighbours.Add(nxt);
                }
                return neighbours;
            }

            public static List<int[]> getKnightNeighbours(int[][] grid, int r, int c)
            {
                List<int[]> neighbours = new List<int[]>();

                if (r - 2 >= 0 && c - 1 >= 0)
                    neighbours.Add(new int[] { r - 2, c - 1 });
                if (r - 2 >= 0 && c + 1 < grid[0].Length)
                    neighbours.Add(new int[] { r - 2, c + 1 });

                if (r - 1 >= 0 && c + 2 < grid[0].Length)
                    neighbours.Add(new int[] { r - 1, c + 2 });
                if (r + 1 < grid.Length && c + 2 < grid[0].Length)
                    neighbours.Add(new int[] { r + 1, c + 2 });

                if (r + 2 < grid.Length && c + 1 < grid[0].Length)
                    neighbours.Add(new int[] { r + 2, c + 1 });
                if (r + 2 < grid.Length && c - 1 >= 0)
                    neighbours.Add(new int[] { r + 2, c - 1 });

                if (r - 1 >= 0 && c - 2 >= 0)
                    neighbours.Add(new int[] { r - 1, c - 2 });
                if (r + 1 < grid.Length && c - 2 >= 0)
                    neighbours.Add(new int[] { r + 1, c - 2 });

                return neighbours;
            }
        }

        class GraphTraversal
        {
            public static int BFS(int n, int[][] edges, bool isDirected = true)
            {
                List<List<int>> adjList = Graph.buildGraph(n, edges, isDirected);
                int[] visted = new int[n];
                int[] parents = new int[n];
                Graph.FillArray(visted, -1);
                Graph.FillArray(parents, -1);

                int graphComponents = 0;

                for (int v = 0; v < n; v++) {
                    if (visted[v] == -1) //not visited
                    {
                        graphComponents++;
                        BFS_Traversal(n, adjList, visted, parents, v);
                    }
                }

                return graphComponents;
            }

            public static int DFS(int n, int[][] edges, bool isDirected = true)
            {
                List<List<int>> adjList = Graph.buildGraph(n, edges, isDirected);
                int[] visted = new int[n];

                int graphComponents = 0;

                for (int v = 0; v < n; v++)
                {
                    if (visted[v] == -1) //not visited
                    {
                        graphComponents++;
                        DFS_Traversal(n, adjList, visted, v);
                    }
                }

                return graphComponents;
            }

            private static void BFS_Traversal(int n, List<List<int>> adjList, int[] visted, int[] parents, int sourceVertex)
            {
                visted[sourceVertex] = 1;

                Queue<int> queue = new Queue<int>();
                queue.Enqueue(sourceVertex);

                int Level = 0;
                while (queue.Count > 0)
                {
                    int queueCount = queue.Count;
                    while (queueCount-- > 0)
                    {
                        int node = queue.Dequeue();

                        Console.WriteLine("{0} at Level {1}", node, Level);

                        foreach (int neighbour in adjList[node])
                        {
                            if (visted[neighbour] == -1)
                            {
                                queue.Enqueue(neighbour);
                                visted[neighbour] = 1;
                                parents[neighbour] = node;
                            }
                        }
                    }
                    //change level
                    Level++;
                }
            }

            private static void DFS_Traversal(int n, List<List<int>> adjList, int[] visted, int sourceVertex)
            {
                visted[sourceVertex] = 1;

                Console.WriteLine(sourceVertex);

                foreach (int neighbour in adjList[sourceVertex])
                {
                    if (visted[neighbour] == -1)
                    {
                        DFS_Traversal(n, adjList, visted, neighbour);
                    }
                }
            }
        }

        class HasCycle
        {
            //DIRECTED graph
            //backedge makes cycle, but not cross edge (in BFS, its not easy to defect backedge or cross edge), So
            //Wo use only use DFS to defect cycle
            public static bool Check(int n, int[][] edges, bool isDirected = true)
            {
                List<List<int>> adjList = Graph.buildGraph(n, edges, true);
                int[] visted = new int[n];
                int[] parents = new int[n];
                Graph.FillArray(visted, -1);
                Graph.FillArray(parents, -1);

                for (int v = 0; v < n; v++)
                {
                    if (visted[v] == -1) //not visited
                    {
                        //You can use only DFS to check cycles in Directed graph
                        if (DFS_BackEdge_Check(n, adjList, visted, parents, v)) //if BACK Edge found (i.e. its a cycle)
                            return false;
                    }
                }

                return true;
            }

            private static bool DFS_BackEdge_Check(int n, List<List<int>> adjList, int[] visted, int[] parents, int node)
            {
                visted[node] = 1;

                Console.WriteLine(node);

                foreach (int neighbour in adjList[node])
                {
                    if (visted[neighbour] == -1)
                    {
                        parents[neighbour] = node;

                        bool hasBackEdge = DFS_BackEdge_Check(n, adjList, visted, parents, neighbour);
                        if (hasBackEdge) //BACK Edge (DFS) makes a cycle
                            return true;
                    }
                    else
                    {
                        //BACK Edge (DFS) makes a cycle
                        if (parents[node] != neighbour)
                            return true;
                    }
                }

                return false;
            }
        }

        class IsGraphATree
        {
            //Ensure if graph has only 1 component && 
            //Ensure if there is no BACK Edge(DFS) /CROSS Edge (BFS)
            public static bool Check(int n, int[][] edges, bool isDirected = true)
            {
                List<List<int>> adjList = Graph.buildGraph(n, edges, isDirected);
                int[] visted = new int[n];
                int[] parents = new int[n];
                Graph.FillArray(visted, -1);
                Graph.FillArray(parents, -1);

                int graphComponents = 0;

                for (int v = 0; v < n; v++)
                {
                    if (visted[v] == -1) //not visited
                    {
                        graphComponents++;

                        //Ensure if graph has only 1 component && 
                        if (graphComponents > 1)
                            return false;

                        //Check for Cross Edge(BFS)/Backedge(DFS) ---- they makes cycle

                        //if (BFS_CrossEdge_Check(n, adjList, visted, parents, v)) //if CROSS Edge found (i.e. its a cycle)
                        if (DFS_BackEdge_Check(n, adjList, visted, parents, v)) //if BACK Edge found (i.e. its a cycle)
                            return false;
                    }
                }

                return true;
            }

            private static bool BFS_CrossEdge_Check(int n, List<List<int>> adjList, int[] visted, int[] parents, int sourceVertex)
            {
                //2:20:03 - omkar's video June 28, 2020
                visted[sourceVertex] = 1;

                Queue<int> queue = new Queue<int>();
                queue.Enqueue(sourceVertex);

                int Level = 0;
                while (queue.Count > 0)
                {
                    int queueCount = queue.Count;
                    while (queueCount-- > 0)
                    {
                        int node = queue.Dequeue();

                        Console.WriteLine("{0} at Level {1}", node, Level);

                        foreach (int neighbour in adjList[node])
                        {
                            if (visted[neighbour] == -1)
                            {
                                queue.Enqueue(neighbour);
                                visted[neighbour] = 1;
                                parents[neighbour] = node;
                            }
                            else
                            {
                                //Ensure if there is no Cross Edge (BFS)
                                if (parents[node] != neighbour)
                                    return true;
                            }
                        }
                    }
                    //move to next level
                    Level++;
                }
                return false;
            }

            private static bool DFS_BackEdge_Check(int n, List<List<int>> adjList, int[] visted, int[] parents, int node)
            {
                visted[node] = 1;

                Console.WriteLine(node);

                foreach (int neighbour in adjList[node])
                {
                    if (visted[neighbour] == -1)
                    {
                        parents[neighbour] = node;

                        bool hasBackEdge = DFS_BackEdge_Check(n, adjList, visted, parents, neighbour);
                        if (hasBackEdge) //BACK Edge (DFS) makes a cycle
                            return true;
                    }
                    else
                    {
                        //BACK Edge (DFS) makes a cycle
                        if (parents[node] != neighbour)
                            return true;
                    }
                }

                return false;
            }
        }

        class IsGraphBipartite
        {
            public static bool Check(int n, int[][] edges, bool isDirected = true)
            {
                //Undirected graph ******

                //*******************************************************************
                //A graph to be bipartite If all of its components are Bipartite ****
                //*******************************************************************
                //A Tree is Bipartite, as it doesn't have any cycle *******************
                //Grpah with one ODD length cycle is NOT bipartite, even tough it has 
                //EVEN length cycles ***** 
                //Graph with only 1 and 2 nodes is Bipartite
                //Video 3:52:55 Omkar, June 28,2020

                List<List<int>> adjList = Graph.buildGraph(n, edges, isDirected);
                int[] visted = new int[n];
                int[] parents = new int[n];
                int[] distance = new int[n];
                int[] color = new int[n];

                Graph.FillArray(visted, -1);
                Graph.FillArray(parents, -1);
                Graph.FillArray(distance, -1);
                Graph.FillArray(color, -1);

                int graphComponents = 0;

                for (int v = 0; v < n; v++)
                {
                    if (visted[v] == -1) //not visited
                    {
                        graphComponents++;

                        color[v] = -1 * color[v];

                        //Check for Cross Edge(BFS)/Backedge(DFS) ---- they makes cycle

                        //if (!BFS_IsBipartite_Check(n, adjList, visted, parents, distance, v)) //if CROSS Edge found (i.e. its a cycle) in same Level, then it is not Bipartite
                        if (!DFS_IsBipartite_Check(n, adjList, visted, parents, color, v)) //if BACK Edge found (i.e. its a cycle) to the same color of node, then it is not Bipartite
                            return false;
                    }
                }

                return true;
            }

            private static bool BFS_IsBipartite_Check(int n, List<List<int>> adjList, int[] visted, int[] parents, int[] distance, int sourceVertex)
            {
                visted[sourceVertex] = 1;
                distance[sourceVertex] = 0;

                Queue<int> queue = new Queue<int>();
                queue.Enqueue(sourceVertex);

                //int Level = 0;
                while (queue.Count > 0)
                {
                    int queueCount = queue.Count;
                    while (queueCount-- > 0)
                    {
                        int node = queue.Dequeue();

                        Console.WriteLine("{0} at Level {1}", node, distance[node]);

                        foreach (int neighbour in adjList[node])
                        {
                            if (visted[neighbour] == -1)
                            {
                                distance[neighbour] = 1 + distance[node];
                                queue.Enqueue(neighbour);
                                visted[neighbour] = 1;
                                parents[neighbour] = node;
                            }
                            else
                            {
                                if (parents[node] != neighbour) //Cross edge
                                    //Ensure that both start and end vertex not in same LEVEL
                                    if (distance[neighbour] == distance[node])
                                        return false;
                            }
                        }
                    }
                    //move to next level
                    //Level++;
                }
                return true;
            }

            private static bool DFS_IsBipartite_Check(int n, List<List<int>> adjList, int[] visted, int[] parents, int[] color, int node)
            {
                visted[node] = 1;

                Console.WriteLine(node);

                foreach (int neighbour in adjList[node])
                {
                    if (visted[neighbour] == -1)
                    {
                        parents[neighbour] = node;
                        color[neighbour] = -1 * color[node];

                        bool isChildBipartite = DFS_IsBipartite_Check(n, adjList, visted, parents, color, neighbour);
                        if (!isChildBipartite) //BACK Edge (DFS) makes a cycle
                            return false;
                    }
                    else
                    {
                        //BACK Edge (DFS) makes a cycle
                        if (parents[node] != neighbour) //back edge
                            if (color[node] == color[neighbour]) //if connecting to same color ***
                                return false;
                    }
                }

                return true;
            }

        }

        class NumberOfIsland
        {
            public static int GetCount(int[][] grid)
            {
                //Return number of Components in  a graph..

                int graphComponents = 0;
                for (int r = 0; r < grid.Length; r++)
                {
                    for (int c = 0; c < grid[0].Length; c++)
                    {
                        if (grid[r][c] != 0)
                        {
                            graphComponents++;
                            //DFS_Traversal(grid, r, c);
                            BFS_Traversal(grid, r, c);
                        }
                    }
                }

                return graphComponents;
            }

            private static void DFS_Traversal(int[][] grid, int startR, int startC)
            {
                grid[startR][startC] = 0;

                foreach (int[] neighbour in Graph.getNeighbours(grid, startR, startC))
                {
                    int neighbourR = neighbour[0];
                    int neighbourC = neighbour[1];
                    if (grid[neighbourR][neighbourC] == 1)
                    {
                        DFS_Traversal(grid, neighbourR, neighbourC);
                    }
                }
            }

            private static void BFS_Traversal(int[][] grid, int startR, int startC)
            {
                Queue<int[]> queue = new Queue<int[]>();
                queue.Enqueue(new int[] { startR, startC });
                grid[startR][startC] = 0;

                while (queue.Count > 0)
                {
                    int[] node = queue.Dequeue();

                    foreach (int[] neighbour in Graph.getNeighbours(grid, node[0], node[1]))
                    {
                        int neighbourR = neighbour[0];
                        int neighbourC = neighbour[1];
                        if (grid[neighbourR][neighbourC] == 1)
                        {
                            queue.Enqueue(new int[] { neighbourR, neighbourC });
                            grid[neighbourR][neighbourC] = 0;
                        }
                    }
                }
            }

        }

        class MaxAreaOfIsland
        {
            public static int GetMax(int[][] grid)
            {
                int MaxArea = 0;
                for (int r = 0; r < grid.Length; r++)
                {
                    for (int c = 0; c < grid[0].Length; c++)
                    {
                        if (grid[r][c] != 0)
                        {
                            MaxArea = Math.Max(MaxArea, DFS_Traversal(grid, r, c));
                            //MaxArea = Math.Max(MaxArea, BFS_Traversal(grid, r, c));
                        }
                    }
                }

                return MaxArea;
            }

            private static int DFS_Traversal(int[][] grid, int startR, int startC)
            {
                grid[startR][startC] = 0;

                int areaCount = 0;
                areaCount++;

                foreach (int[] neighbour in Graph.getNeighbours(grid, startR, startC))
                {
                    int neighbourR = neighbour[0];
                    int neighbourC = neighbour[1];
                    if (grid[neighbourR][neighbourC] == 1)
                    {
                        areaCount += DFS_Traversal(grid, neighbourR, neighbourC);
                    }
                }

                return areaCount;
            }

            private static int BFS_Traversal(int[][] grid, int startR, int startC)
            {
                int areaCount = 0;
                Queue<int[]> queue = new Queue<int[]>();
                queue.Enqueue(new int[] { startR, startC });
                grid[startR][startC] = 0;

                while (queue.Count > 0)
                {
                    int[] node = queue.Dequeue();
                    areaCount++;

                    foreach (int[] neighbour in Graph.getNeighbours(grid, node[0], node[1]))
                    {
                        int neighbourR = neighbour[0];
                        int neighbourC = neighbour[1];
                        if (grid[neighbourR][neighbourC] == 1)
                        {
                            queue.Enqueue(new int[] { neighbourR, neighbourC });
                            grid[neighbourR][neighbourC] = 0;
                        }
                    }
                }

                return areaCount;
            }

        }

        class FloodFill
        {
            public static int[][] Fill(int[][] grid, int sr, int sc, int newColor)
            {
                //DFS_Traversal(grid, sr, sc, grid[sr][sc], newColor);
                BFS_Traversal(grid, sr, sc, grid[sr][sc], newColor);

                return grid;
            }

            private static void DFS_Traversal(int[][] grid, int startR, int startC, int sourceColor, int newColor)
            {
                grid[startR][startC] = newColor;

                foreach (int[] neighbour in Graph.getNeighbours(grid, startR, startC))
                {
                    int neighbourR = neighbour[0];
                    int neighbourC = neighbour[1];
                    if (grid[neighbourR][neighbourC] == sourceColor && grid[neighbourR][neighbourC] != newColor)
                    {
                        DFS_Traversal(grid, neighbourR, neighbourC, sourceColor, newColor);
                    }
                }
            }

            private static void BFS_Traversal(int[][] grid, int startR, int startC, int sourceColor, int newColor)
            {
                Queue<int[]> queue = new Queue<int[]>();
                queue.Enqueue(new int[] { startR, startC });
                grid[startR][startC] = newColor;

                while (queue.Count > 0)
                {
                    int[] node = queue.Dequeue();

                    foreach (int[] neighbour in Graph.getNeighbours(grid, node[0], node[1]))
                    {
                        int neighbourR = neighbour[0];
                        int neighbourC = neighbour[1];
                        if (grid[neighbourR][neighbourC] == sourceColor && grid[neighbourR][neighbourC] != newColor)
                        {
                            queue.Enqueue(new int[] { neighbourR, neighbourC });
                            grid[neighbourR][neighbourC] = newColor;
                        }
                    }
                }
            }
        }

        class SnakeLadder
        {
            //BFS on Directed graph
            public static int GetMinMove(int[][] grid)
            {
                //MIN move to reach 100 **** we need BFS
                return BFS_Traversal(grid);
            }

            private static int BFS_Traversal(int[][] grid)
            {
                int[] distance = new int[grid.Length * grid.Length + 1];
                Graph.FillArray(distance, -1);

                Queue<int> queue = new Queue<int>();

                //start from 1 position
                queue.Enqueue(1);
                distance[0] = 1;

                while (queue.Count > 0)
                {
                    int node = queue.Dequeue();

                    foreach (int neighbour in Graph.getSnakeLadderNeighbours(grid, node))
                    {
                        int neighbourNode = neighbour;
                        int[] neighbourRowCol = Graph.NumToRowCol(grid, neighbourNode);

                        int neighbourRow = neighbourRowCol[0];
                        int neighbourCol = neighbourRowCol[1];

                        if (grid[neighbourRow][neighbourCol] != -1) //snake or ladder
                        {
                            //got the 
                            neighbourNode = grid[neighbourRow][neighbourCol];
                        }

                        if (distance[neighbourNode] == -1) //distance already calculated, not visted/seen
                        {
                            distance[neighbourNode] = distance[node] + 1;

                            //reached the top/left corner, exit the game
                            if (neighbourNode == grid.Length * grid.Length)
                                break;

                            queue.Enqueue(neighbourNode);
                        }
                    }
                }

                //return level from 
                return distance[grid.Length * grid.Length];
            }
        }

        class KnightTour
        {
            //Its a special case of Bipartite graph*****
            //http://www.cs.kent.edu/~dragan/ST-Spring2016/Knights%20Tour%20Graphs.pdf

            public static int find_minimum_number_of_moves(int rows, int cols, int start_row, int start_col, int end_row, int end_col)
            {
                // Min Move - DFS
                return BFS_Traversal(rows, cols, start_row, start_col, end_row, end_col);
            }

            private static int BFS_Traversal(int rows, int cols, int start_row, int start_col, int end_row, int end_col)
            {
                //maintain the MIN distance in the grid array
                int[][] grid = new int[rows][];
                for (int r = 0; r < rows; r++)
                {
                    grid[r] = new int[cols];
                    Graph.FillArray(grid[r], -1);
                }

                Queue<int[]> queue = new Queue<int[]>();

                //start from 1 position
                queue.Enqueue(new int[] { start_row, start_col });
                grid[start_row][start_col] = 0;

                while (queue.Count > 0)
                {
                    int[] node = queue.Dequeue();

                    foreach (int[] neighbour in Graph.getKnightNeighbours(grid, node[0], node[1]))
                    {
                        int neighbourRow = neighbour[0];
                        int neighbourCol = neighbour[1];

                        if (grid[neighbourRow][neighbourCol] == -1) //distance already calculated, not visted/seen
                        {
                            grid[neighbourRow][neighbourCol] = grid[node[0]][node[1]] + 1;

                            //reached the top/left corner, exit the game
                            if (neighbourRow == end_row && neighbourCol == end_col)
                                break;

                            queue.Enqueue(new int[] { neighbourRow, neighbourCol });
                        }
                    }
                }

                //return level from 
                return grid[end_row][end_col];
            }
        }

        class StringTransformationUsingDictionary
        {
            public static string[] string_transformation(string[] words, string start, string stop)
            {
                //convert to have O(1) search and memory got increased by O(N)
                HashSet<string> wordsHS = new HashSet<string>(words);

                Queue<string> queue = new Queue<string>();
                queue.Enqueue(start);

                //We will maintain the parent information, so we can build the path again
                Dictionary<string, string> parent = new Dictionary<string, string>();
                parent.Add(start, null);

                int distance = 0;

                while (queue.Count > 0)
                {
                    int count = queue.Count;
                    while (count > 0)
                    {
                        string temp = queue.Dequeue();
                        char[] charArr = temp.ToCharArray();
                        for (int i = 0; i < charArr.Length; i++)
                        {
                            char original_char = charArr[i];
                            for (char c = 'a'; c < 'z'; c++)
                            {
                                if (charArr[i] == c) //same characte
                                    continue;

                                charArr[i] = c;
                                string newWord = new string(charArr);


                                //if reached the final word, exit
                                if (newWord == stop)
                                {
                                    //we reached the destination word, inrease the distance before quitting
                                    distance++;
                                    string[] result = new string[distance + 1];
                                    //add the destination word
                                    result[distance--] = newWord;

                                    //start looping from its parent
                                    newWord = temp;
                                    while (parent[newWord] != null)
                                    {
                                        result[distance--] = newWord;
                                        newWord = parent[newWord];
                                    }
                                    result[distance] = newWord;
                                    return result;
                                }


                                //word is NOT visted
                                if (!parent.ContainsKey(newWord))
                                {
                                    if (wordsHS.Contains(newWord))
                                    {
                                        queue.Enqueue(newWord);
                                    }
                                    parent.Add(newWord, temp);
                                }
                                //resetting the changed value
                                charArr[i] = original_char;
                            }
                        }
                        count--;
                    }
                    distance++;
                }

                return new string[] { };
            }
        }

        class ShortestPathIn2DGrid
        {
            public static int[][] find_shortest_path(string[] grid)
            {
                //Shortest - DFS
                return BFS_util(grid);
            }

            private class PathNode
            {
                public int[] Coordinates { get; set; }
                // Key has a bitwise value - Each key between 'a' - 'j' will be assigned integers from 1 to 10
                public int Key { get; set; }
                public PathNode Parent { get; set; }

                public PathNode()
                {}

                public PathNode(int[] coordinates, int key, PathNode parent)
                {
                    Coordinates = coordinates;
                    Key = key;
                    Parent = parent;
                }
            }

            private static int[][] BFS_util(string[] grid)
            {
                var result = new List<int[]>();
                var rows = grid.Length;
                var cols = grid[0].Length;
                // visited should be x, y with the key values. 
                var visited = new HashSet<Tuple<int, int, int>>();
                // Start BFS from '@' 
                var queue = new Queue<PathNode>();

                // Get start x, y
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (grid[i][j] == '@')
                        {
                            queue.Enqueue(new PathNode(new int[] { i, j }, 0, null));
                            // mark '@' position as visited with no keys i.e. 0
                            visited.Add(new Tuple<int, int, int>(i, j, 0));
                            break;
                        }
                    }
                }

                while (queue.Count > 0)
                {
                    int count = queue.Count;

                    while(count > 0 )
                    {
                        var curNode = queue.Dequeue();

                        var curX = curNode.Coordinates[0];
                        var curY = curNode.Coordinates[1];
                        var nextKey = curNode.Key;

                        char neighborVal = grid[curX][curY];
                        if (neighborVal == '+')
                        {
                            result = GetPath(curNode);
                            return result.ToArray();
                        }
                        else if (char.IsLower(neighborVal))
                        {
                            var keyValue = neighborVal - 'a';
                            nextKey |= 1 << keyValue;
                        }

                        foreach (var neighbor in Graph.getNeighbours(grid, curX, curY))
                        {
                            var x = neighbor[0];
                            var y = neighbor[1];

                            neighborVal = grid[x][y];

                            if (neighborVal == '#')
                                continue;                            
                                                     
                            else if (char.IsUpper(neighborVal))
                            {
                                // doorVal - 'A' ==> is the door values starting from 0.
                                var doorVal = neighborVal - 'A';
                                // left shift 1 by doorValue to get key numbers starting from 1 to 26.
                                var doorKey = 1 << doorVal;
                                if ((nextKey & doorKey) == 0)
                                {
                                    continue;
                                }
                            }

                            if (!visited.Contains(new Tuple<int, int, int>(x, y, nextKey)))
                            {
                                var nextNode = new PathNode()
                                {
                                    Coordinates = neighbor,
                                    Key = nextKey,
                                    Parent = curNode
                                };
                                queue.Enqueue(nextNode);
                                visited.Add(new Tuple<int, int, int>(x, y, nextKey));
                            }
                        }
                        count--;
                    }
                }

                return result.ToArray();
            }

            private static List<int[]> GetPath(PathNode nextNode)
            {
                var path = new List<int[]>();

                var cur = nextNode;

                while (cur != null)
                {
                    path.Add(cur.Coordinates);
                    cur = cur.Parent;
                }

                path.Reverse();
                return path;
            }

            private static List<int[]> GetNeighbors(int x, int y, int rows, int cols)
            {
                var result = new List<int[]>();

                if (x + 1 < rows)
                {
                    result.Add(new int[] { x + 1, y });
                }
                if (x - 1 >= 0)
                {
                    result.Add(new int[] { x - 1, y });
                }
                if (y + 1 < cols)
                {
                    result.Add(new int[] { x, y + 1 });
                }
                if (y - 1 >= 0)
                {
                    result.Add(new int[] { x, y - 1 });
                }

                return result;
            }
        }

        class zombieCluster
        {
            public static int Count(List<string> zombies)
            {
                int clusterCount = 0;
                bool[] visited = new bool[zombies.Count];
                //bool[][] visited = new bool[zombies.Count][];
                //for (int x = 0; x < visited.Length; x++)
                //    visited[x] = new bool[zombies[0].Length];

                for (int z = 0; z < zombies.Count; z++)
                {
                    if(!visited[z])
                    {
                        clusterCount++;
                        DFS_Undirected_Util(zombies, visited, z);
                    }

                    /*for (int c = 0; c < zombies[0].Length; c++)
                    {
                        if (!visited[r][c] && zombies[r][c] == '1')
                        {
                            clusterCount++;
                            DFS_Util(zombies, visited, r, c);
                        }
                    }*/
                }

                return clusterCount;
            }

            private static void DFS_Undirected_Util(List<string> zombies, bool[] visited, int startZombie)
            {
                visited[startZombie] = true;

                int zombieCount = zombies.Count;
                for (int z = 0; z < zombieCount; z++)
                {
                    if (!visited[z] && zombies[startZombie][z] == '1')
                    {
                        //zump to the z1 zombie and see its edges
                        DFS_Undirected_Util(zombies, visited, z);
                    }
                }
            }
            /*
            private static void DFS_Util(List<string> zombies, bool[][] visited, int r, int c)
            {
                visited[r][c] = true;

                foreach (int[] neighbour in getNeighbours(zombies.Count, zombies[0].Length, r, c))
                {
                    int neighbourR = neighbour[0];
                    int neighbourC = neighbour[1];
                    if (zombies[neighbourR][neighbourC] == '1' && !visited[neighbourR][neighbourC])
                    {
                        DFS_Util(zombies, visited, neighbourR, neighbourC);
                    }
                }
            }

            public static List<int[]> getNeighbours(int rows, int cols, int r, int c)
            {
                List<int[]> neighbours = new List<int[]>();

                if (r + 1 < rows)
                    neighbours.Add(new int[] { r + 1, c });
                if (c + 1 < cols)
                    neighbours.Add(new int[] { r, c + 1 });
                if (r - 1 >= 0)
                    neighbours.Add(new int[] { r - 1, c });
                if (c - 1 >= 0)
                    neighbours.Add(new int[] { r, c - 1 });
                return neighbours;
            }
            */
        }

        class CourseSchedule
        {
            //static int timestamp = 0;
            //DAG
            public static int[] Get(int n, IList<int[]> prerequisites)
            {
                int[] timestamp = new int[] { 0 };
                int[] arrival = new int[n];
                int[] departure = new int[n];
                bool[] visited = new bool[n];

                //DFS path
                List<int> path = new List<int>();

                Graph.FillArray(arrival, -1);
                Graph.FillArray(departure, -1);

                //Build Graph (Adjacency Matrix)
                List<List<int>> adjMatrix = new List<List<int>>();
                for(int c = 0; c < n; c++)
                {
                    adjMatrix.Add(new List<int>());
                }
                foreach (int[] preReq in prerequisites)
                {
                    adjMatrix[preReq[1]].Add(preReq[0]);
                }
                for(int course =0; course < n; course ++)
                {
                    if(!visited[course])
                    {
                        if (DFS_HasBackedge_util(adjMatrix, course, arrival, departure, visited, path, timestamp))
                            return new int[] { -1 };
                    }
                }
                path.Reverse();

                return path.ToArray();
            }

            private static bool DFS_HasBackedge_util(List<List<int>> adjMatrix, int currentCourse, int[] arrival, int[] departure, bool[] visited, IList<int> path, int[] timestamp)
            {
                visited[currentCourse] = true;
                arrival[currentCourse] = ++ timestamp[0];

                //get neighbours for currentCourse
                foreach(int nextCourse in adjMatrix[currentCourse])
                {
                    if (!visited[nextCourse])
                    {
                        if (DFS_HasBackedge_util(adjMatrix, nextCourse, arrival, departure, visited, path, timestamp))
                            return true;
                    }
                    else
                    {
                        //back edge - No Departure has been set for the node
                        if (departure[nextCourse] == -1)
                            return true;
                    }
                }
                departure[currentCourse] = ++ timestamp[0];
                path.Add(currentCourse);

                return false;
            }
        }

        class CriticalNetworkConnections
        {
            public static List<int[]> Get(int n, List<int[]> connections)
            {
                List<int[]> result = new List<int[]>();
                
                int[] timestamp = new int[] { 0 };
                int[] arrival = new int[n];
                bool[] visited = new bool[n];

                Graph.FillArray(arrival, -1);

                //Build Graph (Adjacency Matrix)
                List<List<int>> adjMatrix = Graph.buildGraph(n, connections.ToArray(), false);
                
                for (int s = 0; s < n; s++)
                {
                    if(!visited[s])
                    {
                        DFS_HasBackedge_util(adjMatrix, s, arrival, visited, -1, result, timestamp);
                    }
                }

                if(result.Count == 0 )
                    result.Add(new int[] { -1, -1 });

                return result;
            }

            private static int DFS_HasBackedge_util(List<List<int>> adjMatrix, int server, int[] arrival, bool[] visited, int parent, 
                IList<int[]> result, int[] timestamp)
            {
                visited[server] = true;
                arrival[server] = ++ timestamp[0];

                int serverArrival = arrival[server];

                //get neighbours for currentCourse
                foreach (int neighbourServer in adjMatrix[server])
                {
                    if (!visited[neighbourServer])
                    { 
                        int min = DFS_HasBackedge_util(adjMatrix, neighbourServer, arrival, visited, server, result, timestamp);
                        if (min > arrival[server])
                        {
                            //add it
                            result.Add(new int[] { server, neighbourServer });
                        }

                        serverArrival = Math.Min(serverArrival, min);
                    }
                    else if (neighbourServer != parent)
                    {
                        serverArrival = Math.Min(serverArrival, arrival[neighbourServer]);
                    }
                }
                
                return serverArrival;
            }
        }

        class ReverseStronglyConnectedEdge
        {
            //Strongly connected graph means - Each vertex is reachable from every other vertices 
            public class Node
            {
                public int val;
                public List<Node> neighbours = new List<Node>();

                public Node(int _val)
                {
                    val = _val;
                    neighbours.Clear();
                }
            };

            public static Node Convert(Node node)
            {
                HashSet<int> visited = new HashSet<int>();

                //create nodes
                Dictionary<int, Node> newVertexList = new Dictionary<int, Node>();
                DFS_CollectVertices(node, visited, newVertexList);
                
                //reverse the edges
                visited.Clear();
                DFS_ReverseEdges(node, visited, newVertexList);

                return newVertexList[node.val];
            }

            private static void DFS_CollectVertices(Node node, HashSet<int> visited, Dictionary<int, Node> newVertexList)
            {
                visited.Add(node.val);                
                foreach (Node neighbour in node.neighbours)
                {
                    if (!visited.Contains(neighbour.val))
                        DFS_CollectVertices(neighbour, visited, newVertexList);
                }
                newVertexList.Add(node.val, new Node(node.val));
            }
            private static void DFS_ReverseEdges(Node node, HashSet<int> visited, Dictionary<int, Node> newVertexList)
            {
                visited.Add(node.val);

                foreach (Node neighbour in node.neighbours)
                {
                    if (!visited.Contains(neighbour.val))
                    {
                        DFS_ReverseEdges(neighbour, visited, newVertexList);
                    }
                    newVertexList[neighbour.val].neighbours.Add(newVertexList[node.val]);
                }
            }
        }

        class AlienDictionary
        {
            public static string GenerateOrder(string[] dictionaryWords)
            {
                Dictionary<char, HashSet<char>> charAdjMap = new Dictionary<char, HashSet<char>>();
                //collect all unique letters (vertex) from disctionary words 
                foreach (string word in dictionaryWords)
                {
                    foreach(char c in word)
                    {
                        if (!charAdjMap.ContainsKey(c))
                        {
                            charAdjMap.Add(c, new HashSet<char>());
                        }   
                    }
                }

                //Check word pairs and find the letter precedence and build Edges
                for (int x = 1; x < dictionaryWords.Length; x++)
                {
                    int w1 = 0; int w2 = 0;
                    string word1 = dictionaryWords[x - 1]; //previous word
                    string word2 = dictionaryWords[x];
                    
                    while (w1 < word1.Length && w2 < word2.Length )
                    {
                        //if the first word is longer than 2nd word (But 2nd word is not BLANK)
                        //if (w1 > 0 && word1.Length > word2.Length)
                        //    return "";


                        if (word1[w1] != word2[w2])
                        {
                            //building adjacency directed edges
                            if (!charAdjMap[word2[w2]].Contains(word1[w1]))
                                charAdjMap[word2[w2]].Add(word1[w1]);
                            
                            break;
                        }

                        w1++;
                        w2++;

                        //if 2 words has some common and the first word is longer than 2nd word (But 2nd word is not BLANK)
                        //Because, blank(or "") MUST be before any letter (Lexographically) - so 2nd word cant be smaller
                        if (w1 < word1.Length && w2 == word2.Length)
                        {
                            return "";
                        }
                    }
                }

                //Do TOPO Sort (KAHN Algo) to generate order of chars in AlienDict
                //find vertex with 0 outdegree
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                while( charAdjMap.Count > 0 )
                {
                    char ZeroOutDegree = ' ';
                    foreach (KeyValuePair<char, HashSet<char>> v in charAdjMap)
                    {
                        if (v.Value.Count == 0)
                        {
                            ZeroOutDegree = v.Key;
                            break;
                        }
                    }

                    //no vertex with 0 InDegree (its not a DAG - cycle is present)
                    if (ZeroOutDegree == ' ')
                        return "";

                    sb.Append(ZeroOutDegree);
                    charAdjMap.Remove(ZeroOutDegree);
                    foreach (KeyValuePair<char, HashSet<char>> v in charAdjMap)
                    {
                        if (v.Value.Contains(ZeroOutDegree))
                            v.Value.Remove(ZeroOutDegree);
                    }
                }

                return sb.ToString();
            }
        }

        public static void runTest()
        {
            //Strongly connected graph


            /*
            List<int[]> edges = new List<int[]>();
            edges.Add(new int[] {0,1 });
            edges.Add(new int[] {0,2 });
            edges.Add(new int[] {0,3 });

            edges.Add(new int[] { 1, 2 });
            edges.Add(new int[] { 2, 3 });
            */

            //[0,1], [0,2], [0,3], [1,4]

            //GraphTraversal.BFS(5, edges.ToArray(), false);
            //IsGraphATree.Check(4, edges.ToArray(), false);
            //IsGraphBipartite.Check(4, edges.ToArray(), false);

            List<int[]> graph = new List<int[]>();

            graph.Add(new int[] { -1,1,2,-1 });
            graph.Add(new int[] { 2,13,15,-1 });
            graph.Add(new int[] { -1,10,-1,-1 });
            graph.Add(new int[] { -1,6,2,8 });
            //graph.Add(new int[] { 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 0, 0 });

            //graph.Add(new int[] { 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0 });
            //graph.Add(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 });
            //graph.Add(new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0 });
            //graph.Add(new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 });

            //MaxAreaOfIsland.GetMax(graph.ToArray());
            //FloodFill.Fill(graph.ToArray(),1,1,2);
            //SnakeLadder.GetMinMove(graph.ToArray());

            KnightTour.find_minimum_number_of_moves(7, 7, 0, 0, 5, 5);
            //KnightTour.find_minimum_number_of_moves(4, 3, 1, 0, 5, 5);

            //StringTransformationUsingDictionary.string_transformation(new string[] { "cccw", "accc", "accw" }, "cccc", "cccc");

            //ShortestPathIn2DGrid.find_shortest_path(new string[] { "+B...", "####.", "##b#.", "a...A", "##@##" });

            //List<string> lst = new List<string>();
            //lst.Add("1100");
            //lst.Add("1110");
            //lst.Add("0110");
            //lst.Add("0001");
            //zombieCluster.Count(lst);

            /*List<int[]> preReq = new List<int[]>();
            preReq.Add(new int[] {0,1});
            preReq.Add(new int[] {1,2 });
            preReq.Add(new int[] {2,0 });
            preReq.Add(new int[] {1,3 });
            CriticalNetworkConnections.Get(4, preReq);
            */

            /*ReverseStronglyConnectedEdge.Node n4 = new ReverseStronglyConnectedEdge.Node(4);
            ReverseStronglyConnectedEdge.Node n3 = new ReverseStronglyConnectedEdge.Node(3);
            ReverseStronglyConnectedEdge.Node n2 = new ReverseStronglyConnectedEdge.Node(2);
            ReverseStronglyConnectedEdge.Node n1 = new ReverseStronglyConnectedEdge.Node(1);
            
            n3.neighbours.Add(n1);
            n2.neighbours.Add(n3);
            n1.neighbours.Add(n2);

            n1.neighbours.Add(n4);
            n4.neighbours.Add(n1);

            ReverseStronglyConnectedEdge.Convert(n1);*/

            AlienDictionary.GenerateOrder(new string[] {"abc", "ab" });

            AlienDictionary.GenerateOrder(new string[] { "baa", "abcd", "abca", "cab", "cad" });
        }
    }

}
