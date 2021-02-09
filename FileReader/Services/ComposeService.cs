using FileReader.Repositories;

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
    }
}
