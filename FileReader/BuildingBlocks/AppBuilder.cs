using System.IO;

namespace FileReader.BuildingBlocks
{
    public class AppBuilder
    {
        private static AppBuilder instanse = null;
        private readonly string inputPath;
        private readonly string outputPath;

        private AppBuilder(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            inputPath = $@"{currentDirectory}\{args[0].Remove(0, 1)}";
            outputPath = $@"{currentDirectory}\{args[1].Remove(0, 1)}";
        }

        public void Run()
        {
            Initializer.Create(inputPath, outputPath);
        }

        public static AppBuilder CreateDefaultBuilder(string[] args)
        {
            if (instanse == null)
            {
                instanse = new AppBuilder(args);
            }
            return instanse;
        }
    }
}
