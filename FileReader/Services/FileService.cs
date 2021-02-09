using FileReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileReader.Services
{
    public class FileService
    {
        private readonly LineService _lineService;
        private readonly ComposeService _composeService;
        private static readonly string _outputPath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Transactions.log";

        public FileService(LineService lineService,
            ComposeService composeService)
        {
            _lineService = lineService;
            _composeService = composeService;
        }

        // TODO: catch exception
        public void Read(string path)
        {
            try
            {
                using var file = new StreamReader(@path);
                var f = file.ReadToEnd();
                var lines = f.Split(new char[] { '\n' });
                var count = lines.Count();
                

                for (var i = 1; i <= lines.Length; i++)
                {
                    string s = lines[i - 1];

                    _lineService.AddWordsToLine(i, SplitString(s));

                }
                _composeService.Compose();
                Write();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException();
            }
        }

        // TODO: not implemented
        public void Write()
        {
            var resultList = _composeService.GetComposedStringsList();
            using StreamWriter sw = new StreamWriter(_outputPath, true);
            foreach(var line in resultList)
            {
                sw.WriteLine(line + '\n');
            }  
        }

        private string[] SplitString(string s)
        {
            char[] separators = new char[] { ' ', '.', '\r' };
            string[] words = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return words;
        }
    }
}
