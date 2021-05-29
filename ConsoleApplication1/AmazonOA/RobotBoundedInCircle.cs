using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.AmazonOA
{
    //https://leetcode.com/discuss/interview-question/1209982/Amazon-OA-or-May-2021-or-LONDON-UK
    //https://leetcode.com/problems/robot-bounded-in-circle/
    public class RobotBoundedInCircle
    {
        public static bool IsRobotBounded(string instructions)
        {
            int[] cur = new int[] { 0, 0 };

            int[][] dirs = new int[4][] { new int[] { 0, 1 }, new int[] { 1, 0 },
                new int[] { 0, -1 }, new int[] { -1, 0 } };

            int direction = 0; // 0:north(up), 1: right, 2: down, 3: left

            foreach (char c in instructions)
            {
                if (c == 'G')
                {
                    cur[0] += dirs[direction][0];
                    cur[1] += dirs[direction][1];
                }
                else if (c == 'L')
                {
                    direction = (direction + 3) % 4;
                }
                else if (c == 'R')
                {
                    direction = (direction + 1) % 4;
                }
            }

            return (cur[0] == 0 && cur[1] == 0 || direction > 0);
        }
    }
}
