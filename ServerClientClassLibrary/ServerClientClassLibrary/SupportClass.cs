using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary
{
    public static class SupportClass
    {
        public static string SubEnv(string text, string word, char symbol)
        {
            int indexofword, indexofsymbol, length = word.Length;
            indexofword = text.IndexOf(word);
            indexofsymbol = text.IndexOf(symbol, indexofword);
            return text.Substring(indexofword + length + 1, indexofsymbol - (length + 1) - indexofword);
        }
    }
}
