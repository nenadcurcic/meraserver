using System;

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