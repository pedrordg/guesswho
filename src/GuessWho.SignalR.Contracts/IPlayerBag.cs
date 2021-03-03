using System.Collections.Generic;

namespace GuessWho.SignalR.Contracts
{
    public interface IPlayerBag
    {
        void AddPlayerToBag(string playerId);

        void RemovePlayerFromBag(string playerId);

        IEnumerable<string> FetchOnlineFriends(IEnumerable<string> friendsToCheckForOnlineStatus);
    }
}
