using System.Collections.Generic;

namespace FileReader.Models
{
    public class Line
    {
        public int LineNumber { get; private set; }
        public List<Word> Words { get; private set; }

        private Line(int lineNumber)
        {
            LineNumber = lineNumber;
            Words = new List<Word>();
        }

        // Factory method
        public static Line Create(int lineNumber) => new Line(lineNumber);
    }
}
