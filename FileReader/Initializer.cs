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

        private Initializer()
        {
            RegisterCustomDependencies();
        }

        private void RegisterCustomDependencies()
        {
            LineRepository lineRepository = LineRepository.GetInstance();
            WordRepository wordRepository = WordRepository.GetInstance();
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
                var readTask = _fileService.Read(path);
                readTask.Wait();
                Console.WriteLine(Messages.DoneMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Messages.ErrorMessage);
            }
        }
    }
}
