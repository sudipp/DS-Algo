using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.AmazonOA
{
    //https://leetcode.com/discuss/interview-question/1210011/Amazon-or-OA-or-May-or-North-America
    public class AmazonFreshDeliveries
    {
        /*
         Given allLocations list of co-ordinates (x,y) you have to find the X - closest locations from truck's location which is (0,0). Distance is calculated using formula (x^2 + y^2). If the there is tie then choose the co-ordinate with least x value. Output list can be in any order.

Sample Input :

allLocations : 
[ [1, 2] , [1, -1], [3, 4] ]
numOfDeliveries : 
2
Sample Output :

[ [1, -1], [1, 2] ] 
        */

        private static int Ascending((int, int, List<int>) i, (int, int, List<int>) j)
        {
            if (i.Item1 == j.Item1)
                return 0;
            else if (i.Item2 == j.Item2)
                return i.Item1.CompareTo(j.Item1);
            else
                return j.Item2.CompareTo(i.Item2);
        }
        public static List<List<int>> GetClosestCoordinate(List<List<int>> truckLocations, int numOfDeliveries)
        {
            int x = 0;
            SortedSet<(int, int, List<int>)> maxQ = new SortedSet<(int, int, List<int>)>(Comparer<(int, int, List<int>)>.Create(Ascending));
            foreach(List<int> truckLocation in truckLocations)
            {
                //(x ^ 2 + y ^ 2)
                int dist = truckLocation[0] * truckLocation[0] + truckLocation[1] * truckLocation[1];
                maxQ.Add((x++, dist, truckLocation));
                if (maxQ.Count > numOfDeliveries)
                    maxQ.Remove(maxQ.Min);
            }

            return maxQ.Select(s => s.Item3).ToList();
        }
    }
}
