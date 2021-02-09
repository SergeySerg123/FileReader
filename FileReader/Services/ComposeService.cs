using FileReader.Repositories;
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

        public void Compose()
        {
            foreach(var line in _lineRepository.Lines)
            {
                line.Words.ForEach(word =>
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
            }
        }

        public List<string> GetComposedStringsList()
        {
            var result = new List<string>();

            foreach(var word in _wordRepository.Words)
            {
                var sb = new StringBuilder();
                var lines = new SortedSet<int>();
                word.Value.ForEach(line => 
                {
                    lines.Add(line);
                });

                sb.AppendFormat("Word - ' {0} ' contains in the lines: {1}", word.Key, ComposeLines(lines));
                result.Add(sb.ToString());
            }
            result.Sort();
            return result;
        }

        private string ComposeLines(SortedSet<int> lines) 
        {
            var sb = new StringBuilder();

            foreach(var lineNumber in lines)
            {
                sb.AppendFormat(" {0} ", lineNumber);
            }

            return sb.ToString();
        }
    }
}
