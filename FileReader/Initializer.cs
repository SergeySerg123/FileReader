using FileReader.Interfaces;
using FileReader.Repositories;
using FileReader.Services;
using static FileReader.Helpers.Messages;
using System;
using System.Threading.Tasks;

namespace FileReader
{
    class Initializer
    {
        private IFileService _fileService;
        private ILogger _logger;
        private ITimer _timer;

        private Initializer(string inputPath, string outputPath)
        {
            RegisterCustomDependencies(inputPath, outputPath);
            InitApp().Wait();
        }

        private void RegisterCustomDependencies(string inputPath, string outputPath)
        {
            LineRepository lineRepository = new LineRepository();
            WordRepository wordRepository = new WordRepository();
            LineService lineService = new LineService(lineRepository);
            ComposeService composeService = new ComposeService(lineRepository, wordRepository);

            _fileService = new FileService(lineService, composeService, inputPath, outputPath);
            _logger = new LoggerService();
            _timer = new TimerService();
        }

        private void Done()
        {
            _logger.Log(DoneMessage);
            _logger.Log(_timer.GetElapsed());
        }

        public static Initializer Create(string inputPath, string outputPath) => new Initializer(inputPath, outputPath);

        public async Task InitApp()
        {
            try
            {
                _logger.Log(StartProcessMessage);
                _timer.SetStartTime();
                await _fileService.Read();
                await _fileService.Write();
                Done(); 
            }
            catch (Exception ex)
            {
                _logger.LogExceptionMessage(ex);
                _logger.LogErrorMessage();
            }
        }

        
    }
}
