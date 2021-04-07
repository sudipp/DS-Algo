using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Microsoft
    {
        //1304. Find N Unique Integers Sum up to Zero
        public static int[] SumZero(int n)
        {
            int[] res = new int[n];
            int left = 0, right = n - 1, start = 1;
            while (left < right)
            {
                res[left++] = start;
                res[right--] = -start;
                start++;
            }
            return res;
        }

        //larget alphabetic character
        public static string largestCharacter(string str)
        {
            // Array for keeping track of both uppercase
            // and lowercase english alphabets
            bool[] uppercase = new bool[26];
            bool[] lowercase = new bool[26];

            foreach (char c in str)
            {
                if (char.IsLower(c))
                    lowercase[c - 'a'] = true;

                if (char.IsUpper(c))
                    uppercase[c - 'A'] = true;
            }

            // Iterate from right side of array
            // to get the largest index character
            for (int i = 25; i >= 0; i--)
            {

                // Check for the character if both its
                // uppercase and lowercase exist or not
                if (uppercase[i] && lowercase[i])
                    return (char)(i + 'A') + "";
            }

            // Return -1 if no such character whose
            // uppercase and lowercase present in
            // string str
            return "-1";
        }

        //Visible Nodes in Binary Tree
        //https://leetcode.com/problems/count-good-nodes-in-binary-tree/submissions/
        class GoodNodeBST
        {
            //https://leetcode.com/problems/count-good-nodes-in-binary-tree/submissions/
            int count = 0;
            public int GoodNodes(TreeNode root)
            {
                DFS(root, root.val);
                return count;
            }
            private void DFS(TreeNode root, int maxSoFar)
            {
                if (root == null)
                    return;

                //increase count, if there is no nodes grater than the current
                if (root.val >= maxSoFar)
                {
                    count++;
                }

                //find the current MAX and pass it levels down
                maxSoFar = Math.Max(maxSoFar, root.val);

                if (root.left != null)
                    DFS(root.left, maxSoFar);

                if (root.right != null)
                    DFS(root.right, maxSoFar);
            }
        }
    }
}
