using GuessWho.Execution.Dtos.Player;
using System.Threading.Tasks;

namespace GuessWho.Execution.Contracts
{
    public interface IPlayerRelationCrud
    {
        Task AddFriend(AddFriendDto addFriendDto);

        Task RemoveFriend(RemoveFriendDto removeFriendDto);
    }
}
