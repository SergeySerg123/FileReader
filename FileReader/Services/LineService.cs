using FileReader.Models;
using FileReader.Repositories;

namespace FileReader.Services
{
    public class LineService
    {
        private readonly LineRepository _repository;

        public LineService(LineRepository repository)
        {
            _repository = repository;
        }

        public void AddWordsToLine(int lineNumber, string[] words)
        {
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
    }
}
