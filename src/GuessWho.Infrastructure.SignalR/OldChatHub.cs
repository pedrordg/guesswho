using GuessWho.SignalR.Contracts;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GuessWho.Infrastructure.SignalR
{
    public class OldChatHub : Hub
    {
        //chat
        public async Task SendMessage(string name, string message)
        {
            await Clients.All.SendAsync("SendMessage", name, message);
        }

        //test
        public async Task Echo(string idolName)
        {
            //await Clients.All.SendAsync("Echo", idolName + " card flipped");
            await Clients.All.SendAsync("Echo", idolName);
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
            await Clients.AllExcept(Context.ConnectionId).SendAsync("receiveQuestion", question);
        }

        public async Task AnswerQuestion(AnswerTypes answerTypes)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("receiveAnswer", answerTypes.ToString());
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
