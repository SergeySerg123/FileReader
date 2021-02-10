using FileReader.Models;
using System.Collections.Generic;

namespace FileReader.Repositories
{
    public class WordRepository
    {
        public Dictionary<string, Queue<int>> Words { get; private set; } = new Dictionary<string, Queue<int>>();

        public void AddWord(Word word)
        {
            var linesList = new Queue<int>();
            linesList.Enqueue(word.Line);
            Words.Add(word.Value, linesList);
        }

        public void UpdateWord(Word word, int newLine)
        {
            var lines = Words[word.Value];
            lines.Enqueue(newLine);
            Words[word.Value] = lines;
        }

        public void RemoveWord(string key)
        {
            Words.Remove(key);
        }

        public bool IsContainsValue(Word word)
        {
            return Words.ContainsKey(word.Value);
        }
    }
}
