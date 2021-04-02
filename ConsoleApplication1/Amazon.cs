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
        public static string mostCommonWord(string paragraph, string[] banned)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            Regex regex = new Regex("\\w+");
            foreach (Match m in regex.Matches(paragraph))
            {
                string word = m.Groups[0].Value.ToLower();
                if (!banned.Contains(word))
                {
                    if (!map.ContainsKey(word)) map.Add(word, 1);
                    map[word]++;
                }
            }

            //O(N)
            string mxWord = null;
            int count = int.MinValue;
            foreach (string key in map.Keys)
            {
                if (count <= map[key])
                    continue;

                count = map[key];
                mxWord = key;
            }
            return mxWord;
        }

        //https://algo.monster/problems/amazon_oa_turnstile
        //https://leetcode.com/discuss/interview-question/699973/Goldman-Sachs-or-OA-or-Turnstile
        public static List<int> getTimes(int numWorker, List<int> arrTime, List<int> direction)
        {
            //Tuple stores - "inTime" and Job "Index"
            Queue<Tuple<int, int>> load = new Queue<Tuple<int, int>>();
            Queue<Tuple<int, int>> unload = new Queue<Tuple<int, int>>();

            for (int i = 0; i < numWorker; i++)
            {
                if (direction[i] == 0) load.Enqueue(new Tuple<int, int>(arrTime[i], i));
                else unload.Enqueue(new Tuple<int, int>(arrTime[i], i));
            }

            //Tuple stores, Job "Index" and "JobType => 0 = load, 1 = unloading"  who got last accesed to dock
            Tuple<int, int> dockLastUsedbyJob = null;
            int clockTime = 0;
            int[] turnTime = new int[numWorker];
            while (load.Any() || unload.Any())
            {
                //pick jobs who arrived to dock at clockTime or waiting past
                Tuple<int, int> jload = (load.Any() && load.Peek().Item1 <= clockTime) ? load.Peek() : null;
                Tuple<int, int> junload = (unload.Any() && unload.Peek().Item1 <= clockTime) ? unload.Peek() : null;

                //time didnt arrive for any job queue to reach dock, so timelapse
                if (jload == null && junload == null)
                {
                    dockLastUsedbyJob = null;
                    clockTime++;
                    continue;
                }

                //if the loading dock was not in use in the previous minute, then the unloading worker can use the dock.
                //last minute doc was not used
                if (dockLastUsedbyJob == null)
                {
                    if (junload != null) //unload access dock
                    {
                        unload.Dequeue();
                        dockLastUsedbyJob = new Tuple<int, int>(junload.Item2, 1);
                    }
                    else //load access dock
                    {
                        load.Dequeue();
                        dockLastUsedbyJob = new Tuple<int, int>(jload.Item2, 0);
                    }
                }
                else
                {
                    //if the loading dock was just used by another unloading worker, then the unloading worker can use the dock.
                    if (dockLastUsedbyJob.Item2 == 1)
                    {
                        if (junload != null) //unload access dock
                        {
                            unload.Dequeue();
                            dockLastUsedbyJob = new Tuple<int, int>(junload.Item2, 1);
                        }
                        else //load access dock
                        {
                            load.Dequeue();
                            dockLastUsedbyJob = new Tuple<int, int>(jload.Item2, 0);
                        }
                    }
                    //if the loading dock was just used by another loading worker, then the loading worker can use the dock.
                    else if (dockLastUsedbyJob.Item2 == 0)
                    {
                        if (load != null) //load access dock
                        {
                            load.Dequeue();
                            dockLastUsedbyJob = new Tuple<int, int>(jload.Item2, 0);
                        }
                        else //unload access dock
                        {
                            unload.Dequeue();
                            dockLastUsedbyJob = new Tuple<int, int>(junload.Item2, 1);
                        }
                    }
                }
                turnTime[dockLastUsedbyJob.Item1] = clockTime;
                clockTime++;
            }

            return turnTime.ToList();
        }

        class Pair
        {
            public KeyValuePair<int, string> V;
            public int I = 0;

            // Constructor 
            public Pair(int i, KeyValuePair<int, string> v)
            {
                V = v;
                I = i;
            }
        }
        class PairDescOrder : IComparer<Pair>
        {
            public int Compare(Pair x, Pair y)
            {
                if (x.I == y.I)
                    return 0;
                else if (x.V.Key == y.V.Key)
                {
                    return y.I.CompareTo(x.I);
                }
                else
                {
                    return y.V.Key.CompareTo(x.V.Key);
                }
            }
        }
        //https://algo.monster/problems/top_k_frequently_mentioned_keywords
        public static List<string> topMentioned(int k, List<string> keywords, List<string> reviews)
        {
            Regex regex = new Regex("\\b(:?" + string.Join("|", keywords) + ")\\b", RegexOptions.IgnoreCase);
            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (string review in reviews)
            {
                foreach (Match m in regex.Matches(review))
                {
                    string word = m.Groups[0].Value.ToLower();
                    if (!counts.ContainsKey(word))
                        counts.Add(word, 0);
                    counts[word]++;
                }
            }

            SortedSet<Pair> queue = new SortedSet<Pair>(new PairDescOrder());
            int x = 0;
            foreach (string key in counts.Keys)
            {
                queue.Add(new Pair(x++, new KeyValuePair<int, string>(counts[key], key)));
                if (queue.Count > k)
                    queue.Remove(queue.Last());
            }
            List<string> res = new List<string>(queue.Select(p => p.V.Value));
            //res.Reverse();
            return res;
        }

        //https://algo.monster/problems/substrings_of_size_K_with_K_distinct_chars
        public static List<string> substrings(string s, int k)
        {
            List<string> result = new List<string>();
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            for (int i = 0; i < k; i++)
            {
                if (!charCount.ContainsKey(s[i]))
                    charCount.Add(s[i], 0);
                charCount[s[i]]++;
            }
            if (charCount.Keys.Count == k)
                result.Add(s.Substring(0, k));

            for (int i = 1; i < s.Length - k; i++)
            {
                char rc = s[i - 1];
                charCount[rc]--;
                if (charCount[rc] == 0)
                    charCount.Remove(rc);

                rc = s[k + i - 1];
                if(!charCount.ContainsKey(rc))
                    charCount.Add(rc, 0);

                charCount[rc] ++;

                if (charCount.Keys.Count == k)
                    result.Add(s.Substring(i, k));
            }

            return result;
        }

        //algo.monster/problems/number_of_islands

        //https://algo.monster/problems/items_in_containers
        public static List<int> numberOfItems(string s, List<List<int>> ranges)
        {
            List<int> res = new List<int>();
            foreach(List<int> range in ranges)
            {
                int l = range[0], r = range[1];
                while (l + 1 < r && s[l] != '|')
                    l++;
                while (l + 1 < r && s[r] != '|')
                    r --;

                int count = 0;
                while (l + 1 < r)
                {
                    if(s[l + 1] == '*')
                        count++;

                    l ++;
                }
                if (count > 0)
                    res.Add(count);
            }
            return res;
        }
    }
}
