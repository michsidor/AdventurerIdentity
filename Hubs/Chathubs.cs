using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine("/////////UZYTKOWNIK " + user);
            Console.WriteLine("//////WIADOOOOOMOSC " + message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
