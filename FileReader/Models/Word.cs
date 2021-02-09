namespace FileReader.Models
{
    public class Word
    {
        public int Line { get; private set; }
        public string Value { get; private set; }
       
        private Word(int lineNumber, string word) 
        {
            Line = lineNumber;
            Value = word;  
        }

        // Factory method
        public static Word Create(int lineNumber, string word) => new Word(lineNumber, word);
    }
}
