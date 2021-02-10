using FileReader.Interfaces;
using System;

namespace FileReader.Services
{
    class TimerService : ITimer
    {
        private DateTime _startTime;

        public void SetStartTime()
        {
            _startTime = DateTime.Now;
        }

        public string GetElapsed()
        {
            var elapsed = DateTime.Now - _startTime;
            return elapsed.Minutes + "mn " + elapsed.Seconds + "s " + elapsed.Milliseconds + "ms";
        }
    }
}
