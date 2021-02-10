using FileReader.Models;
using System.Collections.Generic;

namespace FileReader.Repositories
{
    public class LineRepository
    {
        //public List<Line> Lines { get; private set; } = new List<Line>();
        public Dictionary<int, Line> Lines { get; private set; } = new Dictionary<int, Line>();

        public void Add(Line line)
        {
            Lines.Add(line.LineNumber, line);
        }

        public Line Get(int lineNumber)
        {
            return Lines.GetValueOrDefault(lineNumber);
        }
    }
}
