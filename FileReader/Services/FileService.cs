using FileReader.Interfaces;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileReader.Services
{
    public class FileService : IFileService
    {
        private readonly LineService _lineService;
        private readonly ComposeService _composeService;
        private readonly string _inputPath;
        private readonly string _outputPath;

        public FileService(LineService lineService,
            ComposeService composeService,
            string inputPath, 
            string outputPath)
        {
            _lineService = lineService;
            _composeService = composeService;
            _inputPath = inputPath;
            _outputPath = outputPath;
        }

        public async Task Read()
        {
            using var file = new StreamReader(_inputPath);
            var f = await file.ReadToEndAsync();
            var lines = f.Split(new char[] { '\n' });
            var count = lines.Count();


            for (var line = 1; line <= lines.Length; line++)
            {
                string s = lines[line - 1].Trim();
                if (s.Length > 0)
                    _lineService.AddWordsToLine(line, s);

            }
            _composeService.Compose();
        }

        public async Task Write()
        {
            var resultList = _composeService.GetComposedStringsList();
            using StreamWriter sw = new StreamWriter(_outputPath, true);
            foreach(var line in resultList)
            {
                await sw.WriteLineAsync(line);
            }  
        }
    }
}
