using GuessWho.SignalR.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace GuessWho.SignalR.Hubs
{
    public class PlayerBag : IPlayerBag
    {
        private List<string> _onlinePlayers = new List<string>();

        public void AddPlayerToBag(string playerId)
        {
            if (!_onlinePlayers.Contains(playerId))
            {
                _onlinePlayers.Add(playerId);
            }
        }

        public void RemovePlayerFromBag(string playerId)
        {
            if (_onlinePlayers.Contains(playerId))
            {
                _onlinePlayers.Remove(playerId);
            }
        }

        public IEnumerable<string> FetchOnlineFriends(IEnumerable<string> friendsToCheckForOnlineStatus)
        {
            return _onlinePlayers.Intersect(friendsToCheckForOnlineStatus);
        }
    }
}
