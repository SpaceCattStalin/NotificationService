using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            foreach (var claim in Context.User.Claims)
            {
                Console.WriteLine($"🔍 Claim: {claim.Type} = {claim.Value}");
            }
            Console.WriteLine($"✅ Connected: {Context.UserIdentifier}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"🔌 Disconnected: {Context.UserIdentifier}");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
