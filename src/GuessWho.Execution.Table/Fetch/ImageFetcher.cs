using GuessWho.Execution.Contracts;
using GuessWho.Infra.Blob.Contracts;
using System.Threading.Tasks;

namespace GuessWho.Execution.Table
{
    public class ImageFetcher : IImageFetcher
    {
        private readonly IBlobReader _blobReader;

        public ImageFetcher(IBlobReader blobReader)
        {
            _blobReader = blobReader;
        }

        public async Task<byte[]> GetImage(string name)
        {
            return await _blobReader.DownloadContent(name);
        }
    }
}
