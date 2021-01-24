using System;
using System.Threading.Tasks;

namespace GuessWho.SignalR.Contracts
{
    public interface IChatClient
    {
        Task BroadcastMessage(string name, string message);

        Task Echo(string idolName);
    }
}
