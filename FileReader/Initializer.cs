using FileReader.Helpers;
using FileReader.Interfaces;
using FileReader.Repositories;
using FileReader.Services;
using System;

namespace FileReader
{
    class Initializer
    {
        private IFileService _fileService;
        private DateTime _startTime;

        private Initializer()
        {
            RegisterCustomDependencies();
        }

        private void RegisterCustomDependencies()
        {
            LineRepository lineRepository = new LineRepository();
            WordRepository wordRepository = new WordRepository();
            LineService lineService = new LineService(lineRepository);
            ComposeService composeService = new ComposeService(lineRepository, wordRepository);

            _fileService = new FileService(lineService, composeService);
        }

        public static Initializer Build() => new Initializer();

        public void InitApp()
        {
            try
            {
                Console.WriteLine(Messages.HelloMessage);
                string path = Console.ReadLine();
                _startTime = DateTime.Now;
                var readTask = _fileService.Read(path);
                readTask.Wait();
                var elapsed = DateTime.Now - _startTime;
                Console.WriteLine(Messages.DoneMessage + elapsed.Minutes + "mn " + elapsed.Seconds + "s " + elapsed.Milliseconds + "ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine(Messages.ErrorMessage);
            }
        }
    }
}
