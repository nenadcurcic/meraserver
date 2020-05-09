using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeraServer.Services
{
    internal class TextService
    {
        internal int WordCounter(string inputText)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n', ',', '.' };
            return inputText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}