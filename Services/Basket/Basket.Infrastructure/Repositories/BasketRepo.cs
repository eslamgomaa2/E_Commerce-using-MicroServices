using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDistributedCache _distributedCache;

        public BasketRepo(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        

        public async Task DeleteBasket(string username )
        {
            var basket = await GetBasketByUserName(username);
            if (basket != null)
            {
                await  _distributedCache.RemoveAsync(username);
            }
        }

        public async Task<ShopingCart> GetBasketByUserName(string userName)
        {
           var Basket= await _distributedCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(Basket))
                return null;
             return JsonSerializer.Deserialize<ShopingCart>(Basket)!;
        }

        public async Task<ShopingCart> UpdateBasket(ShopingCart shopingCart)
        {
            var Basket = await _distributedCache.GetStringAsync(shopingCart.UserName);
            if (Basket is null)
            {
                return await GetBasketByUserName(shopingCart.UserName);
            }
            await _distributedCache.SetStringAsync(shopingCart.UserName, JsonSerializer.Serialize(shopingCart));
            return await GetBasketByUserName(shopingCart.UserName);

        }
    }
}
