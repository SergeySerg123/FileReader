using System;

namespace FileReader.Interfaces
{
    public interface ITimer
    {
        void SetStartTime();
        string GetElapsed();
    }
}
