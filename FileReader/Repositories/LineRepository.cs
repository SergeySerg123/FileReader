using FileReader.Models;
using System.Collections.Generic;
using System.Linq;

namespace FileReader.Repositories
{
    public class LineRepository
    {
        private static LineRepository instance = null;

        private LineRepository() { }

        public List<Line> Lines { get; private set; } = new List<Line>();

        public void Add(Line line)
        {
            Lines.Add(line);
        }

        public Line Get(int lineNumber)
        {
            return Lines.FirstOrDefault(line => line.LineNumber == lineNumber);
        }

        // Singleton instance
        public static LineRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new LineRepository();
            }

            return instance;
        }
    }
}
