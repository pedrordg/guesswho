using GuessWho.SignalR.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace GuessWho.Infrastructure.SignalR
{
    [Authorize]
    public class OldChatHub : Hub
    {
        public OldChatHub()
        {

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
            await Clients.All.SendAsync("PlayerConnected", Context.User.Identity.Name, Context.UserIdentifier);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("PlayerDisconnected", Context.UserIdentifier);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
