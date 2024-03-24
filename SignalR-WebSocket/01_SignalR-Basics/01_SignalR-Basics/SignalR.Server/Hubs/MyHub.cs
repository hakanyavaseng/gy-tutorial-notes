using Microsoft.AspNetCore.SignalR;

namespace SignalR.Server.Hubs
{
    public class MyHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("recevieMessage", message);
        }
    }
}
