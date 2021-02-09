using FileReader.Models;
using System;
using System.Collections.Generic;
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
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException();
            }
        }

        // TODO: not implemented
        //public void Write(string logInfo)
        //{
        //    using StreamWriter sw = new StreamWriter(OutputPath, true);
        //    sw.WriteLine(logInfo);
        //}

        private string[] SplitString(string s)
        {
            char[] separators = new char[] { ' ', '.', '\r' };
            string[] words = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return words;
        }
    }
}
