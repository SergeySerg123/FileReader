using System.Threading.Tasks;

namespace FileReader.Interfaces
{
    public interface IFileService
    {
        Task Read();
        Task Write();
    }
}
