using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Amazon
    {
        //https://algo.monster/problems/transaction_logs
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

        //Most Common Word with Exclusion List
        //https://algo.monster/problems/most_common_word
        public static string mostCommonWord(string paragraph, List<string> banned)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            Regex regex = new Regex("\\w+");
            foreach(Match m in regex.Matches(paragraph))
            {
                string word = m.Groups[0].Value.ToLower();
                if (!banned.Contains(word))
                {
                    if (!map.ContainsKey(word)) map.Add(word, 1);
                    map[word] ++;
                }
            }

            string mxWord = null;
            int count = int.MinValue;
            foreach(string key in map.Keys)
            {
                if (count <= map[key])
                    continue;

                count = map[key];
                mxWord = key;
            }
            return mxWord;
        }
    }
}
