﻿using FileReader.Models;
using FileReader.Repositories;
using System.Linq;
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

                foreach (var w in words)
                {
                    var word = Word.Create(lineNumber, w.ToUpper());
                    line.Words.Add(word);
                }
            }
        }

        // Exclude inappropriate words 
        private string[] SplitString(string s)
        {

            var pattern = @"[\s\W]";
            string[] words = Regex.Split(s, pattern)
                .Where(str => str != string.Empty && IsMatched(str))
                .ToArray();

            return words;
        }

        private bool IsMatched(string str)
        {
            var regex = new Regex("[a-zA-Z0-9]");
            return regex.IsMatch(str);
        }
    }
}
