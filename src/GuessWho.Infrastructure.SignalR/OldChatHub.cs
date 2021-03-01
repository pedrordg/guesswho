using GuessWho.SignalR.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GuessWho.Infrastructure.SignalR
{
    //[Authorize]
    public class OldChatHub : Hub
    {
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

        //group
        public async Task AddToGame(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the game {groupName}.");
        }

        public async Task RemoveFromGame(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the game {groupName}.");
        }

        public async Task BroadcastConnectionAsync()
        {
            await Clients.All.SendAsync("PlayerConnected", $"{Context.User.Identity.Name} is now online", Context.ConnectionId);

            await base.OnConnectedAsync();
        }
    }
}
