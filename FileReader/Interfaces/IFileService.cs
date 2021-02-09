using System.Threading.Tasks;

namespace FileReader.Interfaces
{
    public interface IFileService
    {
        Task Read(string path);
        Task Write();
    }
}
