using Microsoft.AspNetCore.SignalR;

namespace SignalR
{
    public class TalkHub : Hub
    {
        public async Task SendMessage(string user, string message)

        {

            await Clients.All.SendAsync("NewMessage", user, message);
            

           

        }
    }
}
