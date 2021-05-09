using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MicrosoftFolder
{
    public class EvaluateBooleanExpressionEvaluator
    {
        public static string EvaluateBooleanExpression(string str)
        {
            // you can write to stdout for debugging purposes, e.g.
            //Console.WriteLine("This is a debug message");

            str = "TRUE OR FALSE OR ( TRUE AND FALSE OR TRUE OR ( TRUE AND FALSE ) ) AND TRUE";
            str = "TRUE OR FALSE OR ( TRUE AND FALSE OR TRUE ) AND TRUE";
            Stack<string> stack = new Stack<string>();
            string[] arr = str.Split(' ');

            string op = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == "TRUE" || arr[i] == "FALSE")
                {
                    if (!stack.Any() || stack.Peek() == "(")
                        stack.Push(arr[i]);
                    else
                    {
                        bool oernd1 = bool.Parse(stack.Pop());
                        bool oernd2 = bool.Parse(arr[i]);
                        if (op == "AND")
                            stack.Push((oernd1 && oernd2).ToString().ToUpper());
                        else if (op == "OR")
                            stack.Push((oernd1 || oernd2).ToString().ToUpper());
                    }
                }
                else if (arr[i] == "AND" || arr[i] == "OR")
                    op = arr[i];
                else if (arr[i] == "(")
                {
                    stack.Push(op);
                    stack.Push(arr[i]);
                }
                else if (arr[i] == ")")
                {
                    while (stack.Any() && stack.Count >= 2)
                    {
                        bool oernd1 = bool.Parse(stack.Pop());
                        stack.Pop(); //remove '('

                        op = stack.Pop();
                        bool oernd2 = bool.Parse(stack.Pop());

                        if (op == "AND")
                            stack.Push((oernd1 && oernd2).ToString().ToUpper());
                        else if (op == "OR")
                            stack.Push((oernd1 || oernd2).ToString().ToUpper());
                    }
                }
            }
            return stack.Pop();
        }

    }
}
