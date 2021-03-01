using Microsoft.AspNetCore.SignalR;

namespace GuessWho.App.IdProvider
{

    public class NameBasedUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            var userId = connection.User?.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            return userId;
            //return connection.User?.FindFirst(ClaimTypes.objectidentifier)?.Value;
        }
    }
}
