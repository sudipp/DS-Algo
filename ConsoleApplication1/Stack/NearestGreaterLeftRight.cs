using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Stack
{
    public class NearestGreaterLeftRight
    {
        public static int[] NGL(int[] arr)
        {
            int[] NGL = new int[arr.Length];
            Stack<int> stack = new Stack<int>();
            
            //Store NGL of every element in arr
            for (int i = 0; i < arr.Length; i++)
            {
                if (!stack.Any())
                    NGL[i] = -1;
                else if (stack.Any() && stack.Peek() > arr[i])
                    NGL[i] = stack.Peek();
                else if (stack.Any() && stack.Peek() <= arr[i])
                {
                    while (stack.Any() && stack.Peek() <= arr[i])
                        stack.Pop();

                    if (!stack.Any())
                        NGL[i] = -1;
                    else
                        NGL[i] = stack.Peek();
                }
                stack.Push(arr[i]);
            }
            return NGL;
        }

        public static int[] NGR(int[] arr)
        {
            int[] NGR = new int[arr.Length];
            Stack<int> stack = new Stack<int>();

            //Store NGR of every element in arr
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (!stack.Any())
                    NGR[i] = arr.Length;
                else if (stack.Any() && stack.Peek() > arr[i])
                    NGR[i] = stack.Peek();
                else if (stack.Any() && stack.Peek() <= arr[i])
                {
                    while (stack.Any() && stack.Peek() <= arr[i])
                        stack.Pop();

                    if (!stack.Any())
                        NGR[i] = arr.Length;
                    else
                        NGR[i] = stack.Peek();
                }
                stack.Push(arr[i]);
            }
            return NGR;
        }
    }
}
