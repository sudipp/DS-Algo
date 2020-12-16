using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Amazon.CloudSearch;
using System.Drawing;
using System.IO;

namespace ConsoleApplication1
{
    
    public class TrieNode
    {
        public TrieNode[] link;
        public string word;

        public TrieNode()
        {
            this.link = new TrieNode[26];
            this.word = null;
        }
    }


    public class LRUCache
    {
        Dictionary<int, Node> cache = new Dictionary<int, Node>();

        Node head, tail;
        int capacity = 0;

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
        }

        public int Get(int key)
        {
            if (cache.ContainsKey(key)) // Key Already Exist, just update the
            {
                Node entry = cache[key];

                removeNode(entry);

                addAtTop(entry);

                return entry.val;
            }
            return -1;
        }

        public void Put(int key, int value)
        {

            if (cache.ContainsKey(key)) // Key Already Exist, just update the value and move it to top
            {
                Node entry = cache[key];
                entry.val = value;

                removeNode(entry);

                addAtTop(entry);
            }
            else
            {
                Node newnode = new Node(-1);
                newnode.val = value;
                newnode.key = key;

                if (cache.Count == capacity) // We have reached maxium size so need to make room for new element.
                {
                    cache.Remove(tail.key);
                    removeNode(tail);
                    addAtTop(newnode);
                }
                else
                {
                    addAtTop(newnode);
                }

                cache.Add(key, newnode);
            }
        }

        //Adding to Start/Head of LL
        public void addAtTop(Node node)
        {
            node.next = head;
            node.pre = null;

            if (head != null)
                head.pre = node;

            head = node;

            if (tail == null)
                tail = head;
        }

        public void removeNode(Node node)
        {

            if (node.pre != null)
            {
                node.pre.next = node.next;
            }
            else
            {
                head = node.next;
            }

            if (node.next != null)
            {
                node.next.pre = node.pre;
            }
            else
            {
                tail = node.pre;
            }
        }
    }


    public class PointComparer : IComparer<KeyValuePair<string, int>>
    {
        public int Compare(KeyValuePair<string, int> x, KeyValuePair<string, int> y)
        {
            return x.Value.CompareTo(y.Value);
        }
    }

    public class LexComparer : IComparer<string>
    {

        public int Compare(string x, string y)
        {
            return x.CompareTo(y);
        }
    }

    public class utilExercise
    {
        private static int ConvertStringTpoInt(string number)
        {
            int response = 0;
            foreach (char c in number)
            {
                response *= 10;
                response += c - 48;//'0';
            }

            return response;
        }

        class point
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        

        // Returns minimum difference between any pair
        private static int findMinDiff(int[] arr, int n)
        {
            // Sort array in non-decreasing order
            Array.Sort(arr);

            // Initialize difference as infinite
            int diff = int.MaxValue;

            // Find the min diff by comparing adjacent
            // pairs in sorted array
            for (int i = 0; i < n - 1; i++)
                if (arr[i + 1] - arr[i] < diff)
                    diff = arr[i + 1] - arr[i];

            // Return min diff
            return diff;
        }

        public class MixedNumbersAndStringsComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                int xVal, yVal;
                var xIsVal = int.TryParse(x, out xVal);
                var yIsVal = int.TryParse(y, out yVal);

                if (xIsVal && yIsVal)   // both are numbers...
                    return xVal.CompareTo(yVal);
                if (!xIsVal && !yIsVal) // both are strings...
                    return x.CompareTo(y);
                if (xIsVal)             // x is a number, sort first
                    return -1;
                return 1;               // x is a string, sort last
            }
        }

        public static List<string> reorderLines(int logFileSize, string[] logLines)
        {
            List<string> alphas = new List<string>();
            for (int x = 0; x < logLines.Length; x++)
            {
                alphas.Add(logLines[x]);
            }
            alphas = alphas.OrderBy(s => s, new LexComparer()).ToList();
            return alphas;
        }

        public static List<string> findMostFrequentlyUsedWordsExcludeCommonUsedWords(string literatureText, List<String> wordsToExclude)
        {
            Dictionary<string, int> wordCountMap = new Dictionary<string, int>();
            string[] words = literatureText.ToLower().Split(' ');
            foreach (string word in words)
            {
                if (!wordsToExclude.Exists(w => w.Equals(word)))
                {
                    if (wordCountMap.ContainsKey(word))
                    {
                        wordCountMap[word] = wordCountMap[word] + 1;
                    }
                    else
                    {
                        wordCountMap.Add(word, 1);
                    }
                }
            }
            var lst = wordCountMap.ToList().OrderByDescending(i=>i.Value);
            //most frequently used words, are count > 1
            return lst.Where(w => w.Value > 1).Select(s => s.Key).ToList();
        }


        class ExternalMergeSort
        {
            public static void start()
            {
                // This does a external merge sort on a big file
                // http://en.wikipedia.org/wiki/External_sorting
                // The idea is to keep the memory usage below 50megs.

                Split("c:\\BigFile.txt");

                MemoryUsage();

                SortTheChunks();

                MemoryUsage();

                MergeTheChunks();

                MemoryUsage();
            }

            /// <summary>
            /// Merge all the "sorted00058.dat" chunks together 
            /// Uses 45MB of ram, for 100 chunks
            /// Takes 5 minutes, for 100 chunks of 10 megs each ie 1 gig total
            /// </summary>
            static void MergeTheChunks()
            {
                W("Merging");

                string[] paths = Directory.GetFiles("C:\\", "sorted*.dat");
                int chunks = paths.Length; // Number of chunks
                int recordsize = 100; // estimated record size
                int records = 10000000; // estimated total # records
                int maxusage = 500000000; // max memory usage
                int buffersize = maxusage / chunks; // size in bytes of each buffer
                double recordoverhead = 7.5; // The overhead of using Queue<>
                int bufferlen = (int)(buffersize / recordsize / recordoverhead); // number of records in each buffer

                // Open the files
                StreamReader[] readers = new StreamReader[chunks];
                for (int i = 0; i < chunks; i++)
                    readers[i] = new StreamReader(paths[i]);

                // Make the queues
                Queue<string>[] queues = new Queue<string>[chunks];
                for (int i = 0; i < chunks; i++)
                    queues[i] = new Queue<string>(bufferlen);

                // Load the queues
                W("Priming the queues");
                for (int i = 0; i < chunks; i++)
                    LoadQueue(queues[i], readers[i], bufferlen);
                W("Priming the queues complete");

                // Merge!
                StreamWriter sw = new StreamWriter("C:\\BigFileSorted.txt");
                bool done = false;
                int lowest_index, j, progress = 0;
                string lowest_value;
                while (!done)
                {
                    // Report the progress
                    if (++progress % 5000 == 0)
                        Console.Write("{0:f2}%   \r",
                            100.0 * progress / records);

                    // Find the chunk with the lowest value
                    lowest_index = -1;
                    lowest_value = "";
                    for (j = 0; j < chunks; j++)
                    {
                        if (queues[j] != null)
                        {
                            if (lowest_index < 0 || String.CompareOrdinal(queues[j].Peek(), lowest_value) < 0)
                            {
                                lowest_index = j;
                                lowest_value = queues[j].Peek();
                            }
                        }
                    }

                    // Was nothing found in any queue? We must be done then.
                    if (lowest_index == -1) { done = true; break; }

                    // Output it
                    sw.WriteLine(lowest_value);

                    // Remove from queue
                    queues[lowest_index].Dequeue();
                    // Have we emptied the queue? Top it up
                    if (queues[lowest_index].Count == 0)
                    {
                        LoadQueue(queues[lowest_index], readers[lowest_index], bufferlen);
                        // Was there nothing left to read?
                        if (queues[lowest_index].Count == 0)
                        {
                            queues[lowest_index] = null;
                        }
                    }
                }
                sw.Close();

                // Close and delete the files
                for (int i = 0; i < chunks; i++)
                {
                    readers[i].Close();
                    File.Delete(paths[i]);
                }

                W("Merging complete");
            }

            /// <summary>
            /// Loads up to a number of records into a queue
            /// </summary>
            static void LoadQueue(Queue<string> queue, StreamReader file, int records)
            {
                for (int i = 0; i < records; i++)
                {
                    if (file.Peek() < 0) break;
                    queue.Enqueue(file.ReadLine());
                }
            }

            /// <summary>
            /// Go through all the "split00058.dat" files, and sort them
            /// into "sorted00058.dat" files, removing the original
            /// This should use 37megs of memory, for chunks of 10megs
            /// Takes about 2 minutes.
            /// </summary>
            static void SortTheChunks()
            {
                W("Sorting chunks");
                foreach (string path in Directory.GetFiles("C:\\", "split*.dat"))
                {
                    Console.Write("{0}     \r", path);

                    // Read all lines into an array
                    string[] contents = File.ReadAllLines(path);
                    // Sort the in-memory array
                    Array.Sort(contents);
                    // Create the 'sorted' filename
                    string newpath = path.Replace("split", "sorted");
                    // Write it
                    File.WriteAllLines(newpath, contents);
                    // Delete the unsorted chunk
                    File.Delete(path);
                    // Free the in-memory sorted array
                    contents = null;
                    GC.Collect();
                }
                W("Sorting chunks completed");
            }

            /// <summary>
            /// Split the big file into chunks
            /// This kept memory usage to 8mb, with 10mb chunks
            /// It took 4 minutes for a 1gig source file
            /// </summary>
            static void Split(string file)
            {
                W("Splitting");
                int split_num = 1;
                StreamWriter sw = new StreamWriter(string.Format("c:\\split{0:d5}.dat", split_num));
                long read_line = 0;
                using (StreamReader sr = new StreamReader(file))
                {
                    while (sr.Peek() >= 0)
                    {
                        // Progress reporting
                        if (++read_line % 5000 == 0)
                            Console.Write("{0:f2}%   \r",
                                100.0 * sr.BaseStream.Position / sr.BaseStream.Length);

                        // Copy a line
                        sw.WriteLine(sr.ReadLine());

                        // If the file is big, then make a new split,
                        // however if this was the last line then don't bother
                        if (sw.BaseStream.Length > 100000000 && sr.Peek() >= 0)
                        {
                            sw.Close();
                            split_num++;
                            sw = new StreamWriter(string.Format("c:\\split{0:d5}.dat", split_num));
                        }
                    }
                }
                sw.Close();
                W("Splitting complete");
            }

            /// <summary>
            /// Write to console, with the time
            /// </summary>
            static void W(string s)
            {
                Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), s);
            }

            /// <summary>
            /// Print memory usage
            /// </summary>
            static void MemoryUsage()
            {
                W(String.Format("{0} MB peak working set | {1} MB private bytes",
                    Process.GetCurrentProcess().PeakWorkingSet64 / 1024 / 1024,
                    Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024
                    ));
            }
        }

        public static void runTest()
        {
            ExternalMergeSort.start();

            string[] fileLines= new string[]
            {
                "12",
                "2434",
                "23",
                "1",
                "654",
                "222",
                "56",
                "100000"
            };
            var str111 = reorderLines(fileLines.Length, fileLines);
            string literatureText = "I am www www Sudip I am a good boy. Sudip";
            List<String> wordsToExclude=new List<string>();
            wordsToExclude.Add("I");
            wordsToExclude.Add("am");
            wordsToExclude.Add("boy");
            var x1111 = findMostFrequentlyUsedWordsExcludeCommonUsedWords(literatureText, wordsToExclude);

            int x = ConvertStringTpoInt("1234");
            
            int[,] imgArray = new int[5, 4]
                {
                    { 1, 2, 3, 4 }, 
                    { 10, 21, 31, 41 },
                    { 11, 22, 32, 42 },
                    { 12, 23, 33, 43 },
                    { 12, 23, 33, 43 }
                };
            imgArray.GetLength(0);
            imgArray.GetLength(1);
            imgArray.GetUpperBound(0);
            imgArray.GetUpperBound(1);

            int[] a= new int[]{1,2,3,4,0,5,6,7,8};
            int mid = 0 + ((a.Length - 0) >> 1);  //start + ((end - start) >> 1)

            int uniqueDeliveryId = 50000;
            for (int x1 = 0; x1 < imgArray.GetLength(0); x1++)
            {
                for (int y = 0; y < imgArray.GetLength(1); y++)
                {
                    uniqueDeliveryId ^= imgArray[x1,y];
                }
            }
            /*
            // Multiplication
            i * 8; // normal
            i << 3; // bitwise [8 = 2^3, so use 3]

            // Division
            i / 16; // normal
            i >> 4; // bitwise [16 = 2^4, so use 4]

            // Modulus
            i % 4; // normal
            i & 3; // bitwise [4 = 1 << 2, apply ((1 << 2) - 1), so use 3]
            */
            int half = 10 >> 1; //10/2 - SAME AS 10 >> 1;
            int Double = 10 << 1; //10*2 - SAME AS 10 << 1;

            //int tesstString = 1;
            byte[] bytes = BitConverter.GetBytes(1);
            byte[] bytes1 = BitConverter.GetBytes(2);
            byte[] bytes2 = BitConverter.GetBytes(3);
            byte[] bytes3 = BitConverter.GetBytes(4);
            byte[] bytes4 = BitConverter.GetBytes(5);
            byte[] bytes5 = BitConverter.GetBytes(1048576);

            BitArray b = new BitArray(new int[] { 3 });
            BitArray b1 = new BitArray(bytes);
            
        }
    }
}
