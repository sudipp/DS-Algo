using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.AmazonOA
{
    //https://leetcode.com/discuss/interview-question/1210011/Amazon-or-OA-or-May-or-North-America
    public class DemolitionRobot
    {
        /*
         * Given a matrix with values 0 (trenches) , 1 (flat) , and 9 (obstacle) you have to find minimum distance to reach 9 (obstacle). If not possible then return -1.
            The demolition robot must start at the top left corner of the matrix, which is always flat, and can move on block up, down, right, left.
            The demolition robot cannot enter 0 trenches and cannot leave the matrix.

            Sample Input :

            [1, 0, 0],
            [1, 0, 0],
            [1, 9, 1]]
            Sample Output :

            3
         */
        public static int findMinDiatnaceToObstacle(int[,] matrix)
        {
            int[][] directions = new int[][] { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { 0, 1 } };

            int depth = -1;
            Queue<(int, int)> q = new Queue<(int, int)>();
            q.Enqueue((0, 0));
            while (q.Any())
            {
                depth++;
                int count = q.Count;
                while (count > 0)
                {
                    (int, int) current = q.Dequeue();
                    if (matrix[current.Item1, current.Item2] == 9)
                        return depth;

                    matrix[current.Item1, current.Item2] = 0;

                    //use neigbours
                    foreach (int[] direction in directions)
                    {
                        int x = current.Item1 + direction[0];
                        int y = current.Item2 + direction[1];
                        if(x >= 0 && x < matrix.GetLength(0) &&
                            y >= 0 && y < matrix.GetLength(1) && matrix[x, y] != 0)
                        {   
                            q.Enqueue((x, y));
                        }
                    }
                    count--;
                }
            }
            return -1;
        }
    }
}
