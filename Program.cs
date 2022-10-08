using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static void Main(string[] args)
    {
        string[] words = { "code", "doce", "ecod", "framer", "frame" };

        List<string> uniqueAnagrams = GetUniqueAnagram(words);
        uniqueAnagrams.Sort();
        string[] result = uniqueAnagrams.ToArray();

        foreach (var word in result) Console.Write(word + " ");
        Console.WriteLine();
    }

    public static List<string> GetUniqueAnagram(string[] words)
    {
        List<string> uniqueAnagrams = new List<string>();

        for (var i = 0; i < words.Length; i++)
        {
            char[] mainLeters = words[i].ToCharArray();
            Array.Sort(mainLeters);

            bool isAnagram = false;

            for (var j = i - 1; j >= 0; j--)
            {
                char[] compareLeters = words[j].ToCharArray();
                Array.Sort(compareLeters);
                isAnagram = Enumerable.SequenceEqual(mainLeters, compareLeters);
                if (isAnagram) break;
            }
            if (isAnagram) continue;
            uniqueAnagrams.Add(words[i]);
        }

        return uniqueAnagrams;
    }
}