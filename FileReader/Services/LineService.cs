using FileReader.Models;
using FileReader.Repositories;
using System;
using System.Text.RegularExpressions;

namespace FileReader.Services
{
    public class LineService
    {
        private readonly LineRepository _repository;

        public LineService(LineRepository repository)
        {
            _repository = repository;
        }

        public void AddWordsToLine(int lineNumber, string s)
        {
            var words = SplitString(s);
            if (words.Length > 0)
            {
                var line = _repository.Get(lineNumber);

                if (line == null)
                {
                    line = Line.Create(lineNumber);
                    _repository.Add(line);
                }

                foreach (var w in SplitString(s))
                {
                    if (IsMatched(w))
                    {
                        var word = Word.Create(lineNumber, w.ToUpper());
                        line.Words.Add(word);
                    } 
                }
            }
        }

        private string[] SplitString(string s)
        {
            char[] separators = new char[] { ' ', '.', '!', '?', ';', ':', ')', '(', '/', '\r' };
            string[] words = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return words;
        }

        // Words validation rules
        private bool IsMatched(string str)
        {
            var regex = new Regex("[a-zA-Z0-9]");
            return regex.IsMatch(str);
        }
    }
}
