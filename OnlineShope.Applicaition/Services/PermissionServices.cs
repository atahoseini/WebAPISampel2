using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Applicaition.Services
{
    public class PermissionServices : IPermissionServices
    {
        private readonly OnlineShopDbContext onlineShopDbContext;
        private readonly IMemoryCache memoryCache;

        public PermissionServices(OnlineShopDbContext onlineShopDbContext,IMemoryCache memoryCache)
        {
            this.onlineShopDbContext=onlineShopDbContext;
            this.memoryCache=memoryCache;
        }
        public async Task<bool> CheckPermission(Guid userId,string permissionFlag)
        {
            string permissionCacheKey = $"permission-{userId.ToString()}";
            var permissionFlags = new List<string>();
            //memoryCache.GetOrCreateAsync<List<string>>(permissionCacheKey);
            if (!memoryCache.TryGetValue(permissionCacheKey, out permissionFlags))
            {
                // Key not in cache, so get data.
                var roles = await onlineShopDbContext.UserRoles.Where(q => q.UserId==userId)
               .Select(q => q.RoleId).ToListAsync();

                //GetPersmissions(roles);
                permissionFlags = await onlineShopDbContext.RolePermision
                    .Where(q => roles.Contains(q.RoleId)).Select(q => q.Permission.PermisionFlag).ToListAsync();

                // Set cache options. 
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                // Save data in cache.
                memoryCache.Set(permissionCacheKey, permissionFlags, cacheEntryOptions);
            }

            //how to remove from cache with key
            //memoryCache.Remove(permissionCacheKey);
            return permissionFlags.Contains(permissionFlag);
        }
    }
}
