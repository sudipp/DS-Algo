using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Stack
{
    public class NearestSmallerLeftRight
    {
        public static int[] NSL(int[] arr)
        {
            int[] NSL = new int[arr.Length];
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                while (stack.Any() && stack.Peek() >= arr[i])
                    stack.Pop();

                NSL[i] = !stack.Any() ? -1 : stack.Peek();
                //Console.Write(NSL[i] + ",");
                stack.Push(arr[i]);
            }
            return NSL;

            //Store NSL of every element in arr
            for (int i = 0; i < arr.Length; i++)
            {
                if (!stack.Any())
                    NSL[i] = -1;
                else if (stack.Any() && stack.Peek() < arr[i])
                    NSL[i] = stack.Peek();
                else if (stack.Any() && stack.Peek() >= arr[i])
                {
                    while (stack.Any() && stack.Peek() >= arr[i])
                        stack.Pop();

                    if (!stack.Any())
                        NSL[i] = -1;
                    else
                        NSL[i] = stack.Peek();
                }
                stack.Push(arr[i]);
            }
            return NSL;
        }

        public static int[] NSR(int[] arr)
        {
            int[] NSR = new int[arr.Length];
            Stack<int> stack = new Stack<int>();

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                while (stack.Any() && stack.Peek() >= arr[i])
                    stack.Pop();

                NSR[i] = !stack.Any() ? arr.Length : stack.Peek();
                //Console.Write(NSR[i] + ",");
                stack.Push(arr[i]);
            }
            //return NSR;

            stack.Clear();
            //Store NSR of every element in arr
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (!stack.Any())
                    NSR[i] = arr.Length;
                else if (stack.Any() && stack.Peek() < arr[i])
                    NSR[i] = stack.Peek();
                else if (stack.Any() && stack.Peek() >= arr[i])
                {
                    while (stack.Any() && stack.Peek() >= arr[i])
                        stack.Pop();

                    if (!stack.Any())
                        NSR[i] = arr.Length;
                    else
                        NSR[i] = stack.Peek();
                }
                stack.Push(arr[i]);
            }
            return NSR;
        }
    }
}
