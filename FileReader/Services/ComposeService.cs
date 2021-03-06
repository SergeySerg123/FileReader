﻿using FileReader.Repositories;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Services
{
    public class ComposeService
    {
        private readonly LineRepository _lineRepository;
        private readonly WordRepository _wordRepository;

        public ComposeService(LineRepository lineRepository,
            WordRepository wordRepository)
        {
            _lineRepository = lineRepository;
            _wordRepository = wordRepository;
        }

        // Compose words
        public void Compose()
        {
            foreach(var line in _lineRepository.Lines)
            {
                line.Value.Words.ForEach(word =>
                {
                    var IsContains = _wordRepository.IsContainsValue(word);
                    if (IsContains)
                    {
                        _wordRepository.UpdateWord(word, word.Line);
                    } else
                    {
                        _wordRepository.AddWord(word);
                    }
                });
                _lineRepository.Remove(line.Key); // For memory optimization
            }
        }

        public string[] GetComposedStringsList()
        {
            var result = new List<string>();

            foreach (var word in _wordRepository.Words)
            {
                var sb = new StringBuilder();
                var lines = new SortedSet<int>();

                while (word.Value.Count > 0) {
                    lines.Add(word.Value.Dequeue());
                }

                _wordRepository.RemoveWord(word.Key); // For memory optimization

                sb.AppendFormat("{0}: {1}", word.Key, ComposeLines(lines));
                result.Add(sb.ToString());
            }

            result.Sort();
            return result.ToArray();
        }

        private string ComposeLines(SortedSet<int> lines) 
        {
            var sb = new StringBuilder();

            foreach(var lineNumber in lines)
            {
                sb.AppendFormat(" {0},", lineNumber);
            }
            sb[^1] = ' ';
            return sb.ToString();
        }
    }
}
