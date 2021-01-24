using GuessWho.SignalR.Contracts;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GuessWho.Infrastructure.SignalR
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task BroadcastMessage(string name, string message)
        {
            await Clients.All.BroadcastMessage( name, message);
        }

        public void Echo(string idolName)
        {
            Clients.AllExcept(Context.ConnectionId).Echo(idolName + " card flipped");
        }
    }
}
