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

        //chat
        public async Task SendMessage(string name, string message)
        {
            await Clients.All.SendAsync("SendMessage", name, message);
        }

        //game
        public async Task FlipCard(string cardPosition)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("FlipCard", cardPosition);
        }

        public async Task GuessCard(int cardId)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("GuessCard", cardId);
        }

        public async Task AskQuestion(string question)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("receiveQuestion", question);
        }

        public async Task AnswerQuestion(AnswerTypes answerTypes)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("receiveAnswer", answerTypes.ToString());
        }

        public async Task BroadcastConnectionAsync()
        {
            var playerFriends = (await _playerRelationFetcher.GetPlayerFriendIds(Context.UserIdentifier)).ToList();
            await Clients.Users(playerFriends).SendAsync("PlayerConnected", Context.User.Identity.Name, Context.UserIdentifier);

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
