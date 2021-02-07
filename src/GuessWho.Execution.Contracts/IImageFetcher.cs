using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IImageFetcher
    {
        Task<byte[]> GetImage(string name);
    }
}
