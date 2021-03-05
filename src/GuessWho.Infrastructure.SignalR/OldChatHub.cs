using GuessWho.Execution.Contracts;
using GuessWho.SignalR.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.Infrastructure.SignalR
{
    [Authorize]
    public class OldChatHub : Hub
    {
        private readonly IPlayerRelationFetcher _playerRelationFetcher;
        private readonly IPlayerBag _playerBag;

        public OldChatHub(IPlayerRelationFetcher playerRelationFetcher, IPlayerBag playerBag)
        {
            _playerRelationFetcher = playerRelationFetcher;
            _playerBag = playerBag;
        }

        //game
        public async Task FlipCard(string cardPosition, string userId)
        {
            await Clients.User(userId).SendAsync("FlipCard", cardPosition);
        }

        public async Task GuessCard(int cardId, string userId)
        {
            await Clients.User(userId).SendAsync("GuessCard", cardId);
        }

        public async Task AskQuestion(string question, string userId)
        {
            await Clients.User(userId).SendAsync("receiveQuestion", question);
        }

        public async Task AnswerQuestion(AnswerTypes answerTypes, string userId)
        {
            await Clients.User(userId).SendAsync("receiveAnswer", answerTypes.ToString());
        }

        public async Task RequestGame(string friendId)
        {
            await Clients.User(friendId).SendAsync("GameRequested", Context.UserIdentifier);
        }

        public async Task AnswerGameRequest(string gamehostId, bool isGameAccepted)
        {
            await Clients.User(gamehostId).SendAsync("GameRequestAnswer", isGameAccepted, Context.UserIdentifier);
        }

        //login
        public async Task BroadcastConnectionAsync()
        {
            var playerFriends = (await _playerRelationFetcher.GetPlayerFriendIds(Context.UserIdentifier)).ToList();
            await Clients.Users(playerFriends).SendAsync("PlayerConnected", Context.UserIdentifier);

            _playerBag.AddPlayerToBag(Context.UserIdentifier);

            var playersOnlineFriends = _playerBag.FetchOnlineFriends(playerFriends);
            if (playersOnlineFriends.Any())
            {
                playersOnlineFriends.ToList().ForEach(friendId => Clients.User(Context.UserIdentifier).SendAsync("PlayerConnected", friendId));
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var playerFriends = (await _playerRelationFetcher.GetPlayerFriendIds(Context.UserIdentifier)).ToList();
            await Clients.Users(playerFriends).SendAsync("PlayerDisconnected", Context.UserIdentifier);

            _playerBag.RemovePlayerFromBag(Context.UserIdentifier);
        }
    }
}
