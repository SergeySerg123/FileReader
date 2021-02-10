using FileReader.Helpers;
using FileReader.Interfaces;
using System;
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

        //public async Task Read(string path)
        //{
        //    using var file = new StreamReader(@path);
        //    var f = await file.ReadToEndAsync();
        //    var lines = f.Split(new char[] { '\n' });
        //    var count = lines.Count();


        //    for (var i = 1; i <= lines.Length; i++)
        //    {
        //        string s = lines[i - 1];

        //        _lineService.AddWordsToLine(i, s);

        //    }
        //    _composeService.Compose();
        //    await Write();
        //}

        public async Task Read(string path)
        {
            using (FileStream fs = File.Open(@path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                Console.WriteLine("Start readind");
                var time = DateTime.Now;
                var f = await sr.ReadToEndAsync();
                Console.WriteLine("End read");
                var elapsed = DateTime.Now - time;
                Console.WriteLine(elapsed.Minutes + "mn " + elapsed.Seconds + "s " + elapsed.Milliseconds + "ms");
                var lines = f.Split(new char[] { '\n' });
                var count = lines.Count();

                elapsed = DateTime.Now - time;
                time = DateTime.Now;
                Console.WriteLine("Start add to Lists");
                Console.WriteLine(elapsed.Minutes + "mn " + elapsed.Seconds + "s " + elapsed.Milliseconds + "ms");
                for (var i = 1; i <= lines.Length; i++)
                {
                    string s = lines[i - 1];

                    _lineService.AddWordsToLine(i, s);

                }
                elapsed = DateTime.Now - time;
                Console.WriteLine("End add to Lists");
                Console.WriteLine(elapsed.Minutes + "mn " + elapsed.Seconds + "s " + elapsed.Milliseconds + "ms");
                time = DateTime.Now;
                _composeService.Compose();
                elapsed = DateTime.Now - time;
                Console.WriteLine("End Compose and start write");
                Console.WriteLine(elapsed.Minutes + "mn " + elapsed.Seconds + "s " + elapsed.Milliseconds + "ms");
                await Write();
            }
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
