using FileReader.Interfaces;
using System;
using static FileReader.Helpers.Messages;

namespace FileReader.Services
{
    public class LoggerService : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void LogErrorMessage()
        {
            Console.WriteLine(ErrorMessage);
        }

        public void LogExceptionMessage(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
