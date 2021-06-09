using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MicrosoftFolder
{
    public class JclosestNumbersToK
    {
        public static void GetJclosestNumbersToK(int[] A, int j, int k)
        {
            //Input : 2, 3, 5, 11, 27, 34, 55, 92
            //Answer : 3,5,11,27,34

            int idx = -1;
            int l = 0, r = A.Length - 1;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                if (A[m] == k)
                {
                    idx = m;
                    break;
                }
                else if (A[m] < k)
                    l = m + 1;
                else
                    r = m - 1;
            }
            if (idx == -1)
                idx = l;

            if (idx == 0) //on left boundary
            {
                for (int i = idx; i < j; i++)
                    Console.Write(A[i]);
                Console.WriteLine();
            }
            else if (idx == A.Length - 1) //on rigt boundary
            {
                for (int i = idx; i > 0; i--)
                    Console.Write(A[i]);
                Console.WriteLine();
            }
            else
            {
                Console.Write(A[idx]);
                int idxL = idx;
                int idxR = idx;
                //j--;
                //2,[],3,5,11,27,34,55,92
                while (j > 0)
                {
                    if (j == 1)
                    {
                        Console.Write((k - A[idxL] <= A[idxR] - k) ? A[idxL] : A[idxR]);
                        break;
                    }
                    else
                    {
                        Console.Write(A[idxL]);
                        Console.Write(A[idxR]);
                        idxL--;
                        idxR++;
                        j--;
                        j--;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
