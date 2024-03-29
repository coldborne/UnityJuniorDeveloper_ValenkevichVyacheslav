﻿using System;

namespace Splitter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string phrase = "За свою карьеру я пропустил более 9000 бросков, проиграл почти 300 игр. " +
                "26 раз мне доверяли сделать финальный победный бросок, и я промахивался. Я терпел поражения снова, и снова, и снова. " +
                "И именно поэтому я добился успеха.";

            char[] separators = { ' ', '.', ',' };
            string[] words = phrase.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}
