using FileReader.Models;
using System.Collections.Generic;

namespace FileReader.Repositories
{
    public class WordRepository
    {
        public Dictionary<string, List<int>> Words { get; private set; } = new Dictionary<string, List<int>>();

        public void AddWord(Word word)
        {
            var linesList = new List<int>
            {
                word.Line
            };
            Words.Add(word.Value, linesList);
        }

        public void UpdateWord(Word word, int newLine)
        {
            var lines = Words[word.Value];
            lines.Add(newLine);
            Words[word.Value] = lines;
        }

        public bool IsContainsValue(Word word)
        {
            return Words.ContainsKey(word.Value);
        }
    }
}
