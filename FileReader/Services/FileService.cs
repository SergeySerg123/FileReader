using FileReader.Helpers;
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

        public FileService(LineService lineService,
            ComposeService composeService)
        {
            _lineService = lineService;
            _composeService = composeService;
        }

        public async Task Read(string path)
        {
            using var file = new StreamReader(@path);
            var f = file.ReadToEnd();
            var lines = f.Split(new char[] { '\n' });
            var count = lines.Count();
                

            for (var i = 1; i <= lines.Length; i++)
            {
                string s = lines[i - 1];

                _lineService.AddWordsToLine(i, s);

            }
            _composeService.Compose();
            await Write();
        }

        public async Task Write()
        {
            var resultList = _composeService.GetComposedStringsList();
            using StreamWriter sw = new StreamWriter(Settings._outputPath, true);
            foreach(var line in resultList)
            {
                await sw.WriteLineAsync(line);
            }  
        }
    }
}
