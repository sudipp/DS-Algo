using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MicrosoftFolder
{
    //https://leetcode.com/problems/design-search-autocomplete-system/
    public class AutocompleteSystem
    {
        class Trie
        {
            public Dictionary<char, Trie> Children = new Dictionary<char, Trie>();
            public Dictionary<string, int> Hits = new Dictionary<string, int>();
            public Trie()
            { }
        }

        string prefix = "";
        Trie root = null;

        public AutocompleteSystem(string[] sentences, int[] times)
        {
            root = new Trie();

            for (int i = 0; i < sentences.Length; i++)
            {
                Add(sentences[i], times[i]);
            }
        }

        private void Add(string str, int hit)
        {
            Trie node = root;

            foreach (char c in str)
            {
                if (!node.Children.ContainsKey(c))
                    node.Children.Add(c, new Trie());

                node = node.Children[c];

                if (!node.Hits.ContainsKey(str))
                    node.Hits.Add(str, 0);

                node.Hits[str] += hit;
            }
        }

        static int Descending(Tuple<int, int, string> i, Tuple<int, int, string> j)
        {
            if (i.Item1 == j.Item1)
                return 0;
            else
            {
                if (i.Item2 != j.Item2)
                    return j.Item2.CompareTo(i.Item2);
                else
                    return i.Item3.CompareTo(j.Item3);
            }
        }

        public IList<string> Input(char c)
        {
            if (c == '#')
            {
                Add(prefix, 1);
                prefix = "";
                return new List<string>();
            }

            prefix += c;

            Trie node = root;
            foreach (char c1 in prefix)
            {
                if (!node.Children.ContainsKey(c1))
                    return new List<string>();

                node = node.Children[c1];
            }

            SortedSet<Tuple<int, int, string>> maxQ = new SortedSet<Tuple<int, int, string>>(Comparer<Tuple<int, int, string>>.Create(Descending));

            int x = 0;
            foreach (string key in node.Hits.Keys)
            {
                maxQ.Add(new Tuple<int, int, string>(x++, node.Hits[key], key));
            }

            List<string> result = new List<string>();
            for (x = 0; x < 3 && maxQ.Count > 0; x++)
            {
                result.Add(maxQ.First().Item3);
                maxQ.Remove(maxQ.First());
            }
            return result;
        }
    }
}
