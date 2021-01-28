using GuessWho.SignalR.Contracts;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GuessWho.Infrastructure.SignalR
{
    public class OldChatHub : Hub
    {
        //chat
        public async Task BroadcastMessage(string name, string message)
        {
            await Clients.All.SendAsync("BroadcastMessage", name, message);
        }

        //test
        public async Task Echo(string idolName)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("Echo", idolName + " card flipped");
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

        public async Task PassTurn()
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("PassTurn");
        }

        public async Task AskQuestion(string question)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("AskQuestion", question);
        }

        public async Task AnswerQuestion(AnswerTypes answerTypes)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("AnswerQuestion", answerTypes.ToString());
        }

        //invitation
        public async Task SendGameInvite(string player1Name, int player1Id)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("SendGameInvite", player1Name + " wants to play with you", player1Id);
        }

        public async Task AnswerGameInvite(bool isInvitationAccepted, string player2Name, int player2Id)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("AnswerGameInvite", player2Name, player2Id);
        }
    }
}
