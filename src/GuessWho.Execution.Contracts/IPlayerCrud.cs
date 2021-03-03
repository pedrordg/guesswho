using GuessWho.Execution.Dtos;
using GuessWho.Execution.Dtos.Player;
using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IPlayerCrud
    {
        Task<PlayerDto> CreatePlayer(CreatePlayerDto createPlayerDto);

        Task<PlayerDto> UpdatePlayer(UpdatePlayerDto updatePlayerDto);

        Task DeletePlayer(DeletePlayerDto deletePlayerDto);

        Task AddFriend(AddFriendDto addFriendDto);

        Task RemoveFriend(RemoveFriendDto removeFriendDto);
    }
}
