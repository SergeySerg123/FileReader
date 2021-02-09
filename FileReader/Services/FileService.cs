using FileReader.Helpers;
using System;
using System.IO;
using System.Linq;

namespace FileReader.Services
{
    public class FileService
    {
        private readonly LineService _lineService;
        private readonly ComposeService _composeService;      

        public FileService(LineService lineService,
            ComposeService composeService)
        {
            _lineService = lineService;
            _composeService = composeService;
        }

        // TODO: catch exception
        public void Read(string path)
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
            Write();
        }

        public void Write()
        {
            var resultList = _composeService.GetComposedStringsList();
            using StreamWriter sw = new StreamWriter(Settings._outputPath, true);
            foreach(var line in resultList)
            {
                sw.WriteLine(line);
            }  
        }
    }
}
