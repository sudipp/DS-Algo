using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MicrosoftFolder
{
    //Replace question mark from a String so that same letter does not occur next to each other?
    public class ReplaceQuestinWithLettersThatAreNotNextToEachOther
    {
        /*
         Write a function that given a string riddle, returns a copy of the string with all of the question 
         marks replaced by lowercase letters(a-z) in such a way that the same letters do not occur next to 
         each other. The result can be any of the possible answers as long as it fulfils the above requirements.
         */
        public static string GetReplaceQuestinMarkWithLettersThatAreNotNextToEachOther(string riddle)
        {
            if (riddle == null || riddle.IndexOf('?') == -1)
            {
                return riddle;
            }
            StringBuilder sb = new StringBuilder("");
            char prev = '\0';
            for (int i = 0; i < riddle.Length; i++)
            {
                char current = riddle[i];
                if (current == '?')
                {
                    char next = '\0';
                    if (i != riddle.Length - 1)
                    {
                        next = riddle[i + 1];
                    }
                    current = prev != 'a' && next != 'a' ? 'a'
                            : prev != 'b' && next != 'b' ? 'b'
                            : 'c';
                }
                sb.Append(current);
                prev = current;
            }
            return sb.ToString();
        }
    }
}
