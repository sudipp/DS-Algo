using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.AmazonOA
{
    //Amazon Prime Air Route:
    //https://www.careercup.com/question?id=5750442676453376
    public class AmazonPrimeAirRoute
    {
        private static int RouteAscending(int[] i, int[] j)
        {
            return i[0].CompareTo(j[0]);
        }
        
        public static List<int> getIdPairsForOptimal(List<int[]> forwardList,
            List<int[]> backwardList, int maxDistance)
        {
            //Sort the backwardList
            backwardList.Sort(Comparer<int[]>.Create(RouteAscending));

            int minVal = int.MaxValue;
            int[] result = new int[2];
            for (int i = 0; i < forwardList.Count; i++)
            {
                int x = GetMaxBackwordIndex(backwardList, maxDistance - forwardList[i][1]);
                int diff = maxDistance - (forwardList[i][1] + backwardList[x][1]);

                if (diff >= 0 && minVal >= diff)
                {
                    minVal = maxDistance - (forwardList[i][1] + backwardList[x][1]);

                    result[0] = forwardList[i][0];
                    result[1] = backwardList[x][0];
                }
            }
            return result.ToList();
        }
        private static int GetMaxBackwordIndex(List<int[]> backwardList, int amt)
        {
            int l = 0, r = backwardList.Count - 1;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                if (backwardList[m][1] > amt)
                    r = m;
                else
                    l = m + 1;
            }
            return l;
        }
    }
}
