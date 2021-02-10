using System;

namespace FileReader.Interfaces
{
    public interface ILogger
    {
        void Log(string message);
        void LogErrorMessage();
        void LogExceptionMessage(Exception ex);
    }
}
