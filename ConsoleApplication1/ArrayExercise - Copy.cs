using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class ArrayExercise1
    {
        //the big o is n^3 because it's a nested for loop with a helper function
        public static string longestPalindromicSubstringBruteForce(string s) 
        {
            string palindrome = null;
            int longestLength = 0;

            for (int i = 0; i < s.Length-1; i++)
            {
                for (int j = i + 1; j < s.Length; j++)
                {
                    if (longestLength > j - i)
                        continue;

                    try
                    {
                        string sij = s.Substring(i, j);
                        if (isPalindrome(sij))
                        {
                            longestLength = j - i;
                            palindrome = sij; //new String(sij);
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
            return palindrome;

            string longestP = "";
            int maxCount = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                for (int j = i+1; j < s.Length; ++j)
                {
                    if (maxCount > j - i)
                        continue;

                    string sub = s.Substring(i, j);
                    if (isPalindrome(sub) && sub.Length > maxCount)
                    {
                        maxCount = sub.Length;
                        longestP = sub;
                    }
                }
            }
            return longestP;

            //tracker
            string longest = "";
            for (int i = 0; i < s.Length; i++) 
            {
                //this nested for loop checks every letter after the letter you're on
                for (int j = i; j < s.Length; j++) 
                {
                    //the substring starts at the outter loop and continues to where j is
                    string substring = s.Substring(i, j + 1);
                    //if this substring is a palindrome, than it replaces the current longest
                    if (substring.Length > longest.Length && isPalindrome(substring))
                    {
                        longest = substring;
                    }
                }
            }
            return longest;
        }
        //O(n) time, O(1) space
        static bool isPalindrome(string s) 
        {
            /*var arr = s.ToCharArray();
            Array.Reverse(arr);
            return s.Equals(new string(arr));
            */


            if (s == null || s.Length == 0) return true;

            s = s.ToLower();
            
            int left = 0;
            int right = s.Length - 1;
            while(left < right) 
            {
                if (!Char.IsLetterOrDigit(s[left]))
                {
                    left++;
                }
                else if (Char.IsLetterOrDigit(s[right]))
                {
                    right--;
                }
                else
                {
                    if (s[left] != s[right])
                    {
                        return false;
                    }
                    right--;
                    left++;
                }
            }
            return true;
        }


        //https://www.geeksforgeeks.org/longest-palindromic-substring-set-2/
        //Time complexity: O ( n^2 ), Space O(1)
        private static string LongestPalindromicSubstringInString(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }
            if (s.Length == 1)
            {
                return s;
            }

            string longest = "";
            for (int i = 0; i < s.Length; i++)
            {
                // Find the longest even length palindrome with  
                // center points as i-1 and i. 
                string tmp = helper(s, i-1, i);
                if (tmp.Length > longest.Length)
                {
                    longest = tmp;
                }

                // Find the longest odd length palindrome with  
                // center point as i 
                // get longest palindrome with center of i, i+1
                tmp = helper(s, i-1, i + 1);
                if (tmp.Length > longest.Length)
                {
                    longest = tmp;
                }
            }

            return longest;
        }

        // Given a center, either one letter or two letter, 
        // Find longest palindrome
        public static string helper(String s, int begin, int end)
        {
            while (begin >= 0 && end <= s.Length - 1 && s[begin] == s[end])
            {
                begin--;
                end++;
            }
            //return s.Substring(begin + 1, end);
            return s.Substring(begin + 1, end - begin -1);
        }
        
        public static void runTest()
        {
            LongestPalindromicSubstringInString("babad");

            var lp = LongestPalindromicSubstringInString("babad");//abcababadef");//"abcababadef" => "ababa" 
            var lp1 = LongestPalindromicSubstringInString("ffcabbachh");//"ffcabbachh" => "abba"

            var lp2 = LongestPalindromicSubstringInString("ha1bgjd");//"ffcabbachh" => "abba"
            
        }

    }
}
