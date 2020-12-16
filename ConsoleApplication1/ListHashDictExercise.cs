using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace ConsoleApplication1
{
    /*
     * TODO: Hashtable: in C# they are Dictionary
     * =============================
     *          Average	Worst Case
        space	O(n)	O(n)
        insert	O(1)	O(n)
        lookup	O(1)	O(n)
        delete	O(1)	O(n)
     */
    public class ListHashDictExercise
    {
        /*
         * 1. If length of string is even, then the frequency of each character in the string must be even.
         * 2. If the length is odd then there should be one character whose frequency is odd and all other chars 
         * must have even frequency and at-least one occurrence of the odd character must be present in the middle of the string.
         */
        public static bool HasPalindromePermutation(string theString) //runtime of O(n), Space of O(n)
        {
            /* we'll need a data structure to keep track of the number of times each character appears. What should we use? 
             * We could use a dictionary. 
             * What if we only track the characters that appear an odd number of times? 
             * Is there a data structure even simpler than a dictionary 
             * we could use? We could use a set, adding and removing characters as we look through the input string, 
             * so the hash set always only holds the characters that appear an odd number of times.
            */
            // Track characters we've seen an odd number of times
            var unpairedCharacters = new HashSet<char>();

            foreach (char c in theString)
            {
                if (unpairedCharacters.Contains(c))
                {
                    unpairedCharacters.Remove(c);
                }
                else
                {
                    unpairedCharacters.Add(c);
                }
            }

            // The string has a palindrome permutation if it
            // has one or zero characters without a pair
            return unpairedCharacters.Count <= 1;
        }

        //Optimize for runtime over memory
        /*
         * We could sort the movieLengths first—then we could use binary search to find secondMovieLength in O(lgn) time 
         * instead of O(n) time. But sorting would cost O(nlg(n)), so we HashSet to get constant lookup for O(1)
         */
        public static bool CanTwoMoviesFillFlight(int[] movieLengths, int flightLength) //O(n) time, and O(n) space
        {
            // Movie lengths we've seen so far
            var movieLengthsSeen = new HashSet<int>(); //Gives O(1) access
            
            foreach (var firstMovieLength in movieLengths)
            {
                int matchingSecondMovieLength = flightLength - firstMovieLength;
                if (movieLengthsSeen.Contains(matchingSecondMovieLength))
                {
                    return true;
                }

                movieLengthsSeen.Add(firstMovieLength);
            }

            // We never found a match, so return false
            return false;
        }

        public class WordCloudData
        {
            /*
             * Runtime and memory cost are both O(n).
             * We optimized to only make one pass over our input and have only one O(n) data structure.
             * ======================================================================================== 
             * Careful— if you thought of building up the word character by character (using +=), you'd 
             * be doing a lot more work than you probably think. Every time we append a character to a string, 
             * C# makes a whole new string. If our input is one long word, then creating all these copies is O(n^2)​​ time.
             * ====================================================
             * Instead, we keep track of the index where our word starts and its current length. 
             * Once we hit a space, we can extract the substring with our word and append it to the array. 
             * That keeps our split method at O(n) time.
             */
            private Dictionary<string, int> _wordsToCounts = new Dictionary<string, int>();

            public IDictionary<string, int> WordsToCounts
            {
                get { return _wordsToCounts; }
            }

            public WordCloudData(string inputString)
            {
                PopulateWordsToCounts(inputString);
            }

            private void PopulateWordsToCounts(string inputString)
            {
                // Iterates over each character in the input string, splitting
                // words and passing them to addWordToDictionary()

                int currentWordStartIndex = 0;
                int currentWordLength = 0;

                for (int i = 0; i < inputString.Length; i++)
                {
                    var character = inputString[i];

                    if (i == inputString.Length - 1)
                    {
                        // If we reached the end of the string we check if the last
                        // character is a letter and add the last word to our dictionary

                        if (char.IsLetter(character))
                        {
                            currentWordLength++;
                        }
                        if (currentWordLength > 0)
                        {
                            var currentWord = inputString.Substring(currentWordStartIndex,
                                currentWordLength);
                            AddWordToDictionary(currentWord);
                        }
                    }
                    else if (character == ' ' || character == '\u2014')
                    {
                        // If we reach a space or emdash we know we're at the end of a word
                        // so we add it to our dictionary and reset our current word

                        if (currentWordLength > 0)
                        {
                            var currentWord = inputString.Substring(currentWordStartIndex,
                                currentWordLength);
                            AddWordToDictionary(currentWord);
                            currentWordLength = 0;
                        }
                    }
                    else if (character == '.')
                    {
                        // We want to make sure we split on ellipses so if we get two periods in
                        // a row we add the current word to our dictionary and reset our current word

                        if (i < inputString.Length - 1 && inputString[i + 1] == '.')
                        {
                            if (currentWordLength > 0)
                            {
                                var currentWord = inputString.Substring(currentWordStartIndex,
                                    currentWordLength);
                                AddWordToDictionary(currentWord);
                                currentWordLength = 0;
                            }
                        }
                    }
                    else if (char.IsLetter(character) || character == '\'')
                    {
                        // If the character is a letter or an apostrophe, we add it to our current word

                        if (currentWordLength == 0)
                        {
                            currentWordStartIndex = i;
                        }
                        currentWordLength++;
                    }
                    else if (character == '-')
                    {
                        // If the character is a hyphen, we want to check if it's surrounded by letters.
                        // If it is, we add it to our current word

                        if (i > 0 && char.IsLetter(inputString[i - 1])
                                && char.IsLetter(inputString[i + 1]))
                        {
                            if (currentWordLength == 0)
                            {
                                currentWordStartIndex = i;
                            }
                            currentWordLength++;
                        }
                        else
                        {
                            if (currentWordLength > 0)
                            {
                                var currentWord = inputString.Substring(currentWordStartIndex,
                                    currentWordLength);
                                AddWordToDictionary(currentWord);
                                currentWordLength = 0;
                            }
                        }
                    }
                }
            }

            private void AddWordToDictionary(string word)
            {
                int currentCount = 0;

                // If the word is already in the dictionary we increment its count
                if (_wordsToCounts.TryGetValue(word, out currentCount))
                {
                    _wordsToCounts[word] = currentCount + 1;
                }
                else if (_wordsToCounts.TryGetValue(word.ToLower(), out currentCount))
                {
                    // If a lowercase version is in the dictionary, we know our input word must be uppercase,
                    // but we only include uppercase words if they're always uppercase.
                    // So we just increment the lowercase version's count

                    _wordsToCounts[word.ToLower()] = currentCount + 1;
                }
                else if (_wordsToCounts.TryGetValue(Capitalize(word), out currentCount))
                {
                    // If an uppercase version is in the dictionary, we know our input word must be lowercase.
                    // Since we only include uppercase words if they're always uppercase, we add the
                    // lowercase version and give it the uppercase version's count

                    _wordsToCounts.Add(word, currentCount + 1);
                    _wordsToCounts.Remove(Capitalize(word));
                }
                else
                {
                    // Otherwise, the word is not in the dictionary at all, lowercase or uppercase.
                    // So we add it to the dictionary

                    _wordsToCounts.Add(word, 1);
                }
            }

            private string Capitalize(string word)
            {
                return word.Substring(0, 1).ToUpper() + word.Substring(1);
            }
        }

        /*Count Sort *****
         * Each round, players receive a score between 0 and 100, which you use to rank them from highest to lowest.  
         * So far you're using an algorithm that sorts in O(nlogN) time, but players are complaining that their rankings 
         * aren't updated fast enough. You need a faster sorting algorithm.
         */
        //https://www.interviewcake.com/question/csharp/top-scores?section=hashing-and-hash-tables&course=fc1
        public static int[] SortScores(int[] theArray, int maxValue) //Time O(n + maxValue), space O(n + maxValue)
        {
            // Array of 0's at indices 0...maxValue
            int[] numCounts = new int[maxValue + 1];

            // Populate numCounts
            foreach (var num in theArray)
            {
                numCounts[num]++;
            }

            // Populate the final sorted array
            int[] sortedArray = new int[theArray.Length];
            int currentSortedIndex = 0;

            // For each num in numCounts
            for (int num = 0; num < numCounts.Length; num++)
            {
                int count = numCounts[num];

                // For the number of times the item occurs
                for (int i = 0; i < count; i++)
                {
                    // Add it to the sorted array
                    sortedArray[currentSortedIndex] = num;
                    currentSortedIndex++;
                }
            }

            return sortedArray;
        }

        public class FilePaths
        {
            public string DuplicatePath { get; private set; }

            public string OriginalPath { get; private set;}

            public FilePaths(string duplicatePath, string originalPath)
            {
                DuplicatePath = duplicatePath;
                OriginalPath = originalPath;
            }

            public override string ToString()
            {
                return "(original: " + OriginalPath + ", duplicate: " + DuplicatePath;
            }
        }

        public static IList<FilePaths> FindDuplicateFiles(string startingDirectory) //O(n), Space o(n) 
        {
            //Dictionary to check if we already visited files
            var filesSeenAlready = new Dictionary<string, FileInfo>();

            //Traverse file system
            var stack = new Stack<FileSystemInfo>();
            stack.Push(new DirectoryInfo(startingDirectory));

            //Duplicate file list
            var duplicates = new List<FilePaths>();

            while (stack.Count > 0)
            {
                var currentInfo = stack.Pop();

                // If it's a directory, put the contents in our stack
                var currentDirectoryInfo = currentInfo as DirectoryInfo;
                if (currentDirectoryInfo != null)
                {
                    foreach (var info in currentDirectoryInfo.GetFileSystemInfos())
                    {
                        stack.Push(info);
                    }
                }

                // If it's a file
                var currentFileInfo = currentInfo as FileInfo;
                if (currentFileInfo != null)
                {
                    // Get its hash
                    var fileHash = SampleHashFile(currentFileInfo);

                    // If we've seen it before
                    if (filesSeenAlready.ContainsKey(fileHash))
                    {
                        var existingFileInfo = filesSeenAlready[fileHash];

                        if (currentFileInfo.LastWriteTime > existingFileInfo.LastWriteTime)
                        {
                            // Current file is the dupe!
                            duplicates.Add(new FilePaths(currentFileInfo.FullName, existingFileInfo.FullName));
                        }
                        else
                        {
                            // Other file is the dupe!
                            duplicates.Add(new FilePaths(existingFileInfo.FullName, currentFileInfo.FullName));

                            // But also update filesSeenAlready to have the new file's info
                            filesSeenAlready[fileHash] = currentFileInfo;
                        }
                    }
                    else
                    {
                        // Throw new files in filesSeenAlready so we can compare it later
                        filesSeenAlready[fileHash] = currentFileInfo;
                    }
                }
            }
            return duplicates;
        }
        private const int SampleSize = 4000;

        private static string SampleHashFile(FileInfo fileInfo) //Time O(1), Space O(1) per file, as sample bytes are constant  
        {
            long totalBytes = fileInfo.Length;

            byte[] digestBytes;
            using (var fileStream = fileInfo.OpenRead())
            {
                using (var fileReader = new BinaryReader(fileStream))
                {
                    // If the file is too short to take 3 samples, hash the entire file
                    if (totalBytes < SampleSize * 3)
                    {
                        digestBytes = fileReader.ReadBytes((int)totalBytes);
                    }
                    else
                    {
                        long numBytesBetweenSamples = (totalBytes - (SampleSize * 3)) / 2;
                        digestBytes = new byte[SampleSize * 3];

                        // Read first, middle and last bytes
                        for (int i = 0; i < 3; i++)
                        {
                            var sectionBytes = fileReader.ReadBytes(SampleSize);
                            sectionBytes.CopyTo(digestBytes, i * SampleSize);
                            fileStream.Seek(numBytesBetweenSamples, SeekOrigin.Current);
                        }
                    }
                }
            }
            
            using (var sha = new SHA256Managed())
            {
                var hash = sha.ComputeHash(digestBytes);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }


        public static void runTest()
        {
            //CanTwoMoviesFillFlight(new int[] { 1, 6, 3, 1, 3, 6, 6 }, 6);

            //WordCloudData v=new WordCloudData("Sudip is a good boy, sudip has a car. Car was red in color.");

            int[] sortedScores = SortScores(new[] { 37, 89, 41, 65, 91, 53 }, 100);
        }

    }
}
