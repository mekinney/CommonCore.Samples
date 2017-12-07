using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace todo.webapi.Hubs
{
    public class DataNotificationHub : Hub
    {
        private readonly IMemoryCache _cache;

        public DataNotificationHub(IMemoryCache cache)
        {
            _cache = cache;
        }
        public Task RegisterId(string userId)
        {
            return Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var id = int.Parse(userId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(3));
                    _cache.Set(id, this.Context.ConnectionId, cacheEntryOptions);
                }
            });

        }

        public override Task OnConnectedAsync()
        {
            this.Groups.AddAsync(this.Context.ConnectionId, "groupName");
            return base.OnConnectedAsync();
         
        }
    }

}
