using Microsoft.AspNetCore.SignalR;

namespace GuessWho.Infrastructure.SignalR
{
    public class ChatHub : Hub
    {
        public void BroadcastMessage(string name, string message)
        {
            Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public void Echo(string idolName)
        {
            Clients.AllExcept(Context.ConnectionId).SendAsync("echo", idolName + " card flipped");
        }
    }
}
