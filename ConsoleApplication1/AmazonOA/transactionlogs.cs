using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.AmazonOA
{
    //https://leetcode.com/discuss/interview-question/989768/Amazon-or-OA-2020-or-Transaction-logs
    //https://algo.monster/problems/transaction_logs
    public class transactionlogs
    {
        public static string[] getUserWithLogMoreThanThreshold(List<string> logData, int threshold)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            foreach (string logs in logData)
            {
                string[] log = logs.Split(' ');
                if (!map.ContainsKey(log[0])) map.Add(log[0], 0);
                map[log[0]] += 1;

                if (log[0] != log[1])
                {
                    if (!map.ContainsKey(log[1])) map.Add(log[1], 0);
                    map[log[1]] += 1;
                }
            }

            List<string> userId = new List<string>();
            foreach (string user in map.Keys)
            {
                if (map[user] >= threshold)
                {
                    userId.Add(user);
                }
            }
            string[] users = userId.ToArray();
            Array.Sort(users);
            return users;
        }
    }
}
