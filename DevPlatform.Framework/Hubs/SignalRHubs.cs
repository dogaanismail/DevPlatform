using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DevPlatform.Framework.Hubs
{
    public abstract class SignalRHubs
    {
        public abstract class VideoNotificationHub : Hub
        {
            public virtual async Task RoomsUpdated(bool flag)
              => await Clients.Others.SendAsync("RoomsUpdated", flag);
        }
    }
}
