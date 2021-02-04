using GuessWho.Execution.Dtos;
using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IDeckFetcher
    {
        Task<DeckDto> GetDeckById(string deckId);
    }
}
