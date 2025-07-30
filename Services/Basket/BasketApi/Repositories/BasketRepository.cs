using BasketApi.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BasketApi.Repositories;

public class BasketRepository : IBasketRepository
{

    #region Ctor

    private readonly IDistributedCache _cache;

    public BasketRepository(IDistributedCache cache)
    {
        _cache = cache;
    }
    #endregion

    

   

    public async Task<ShoppingCart> GetUserBasket(string userName)
    {
        var basket = await _cache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
        {
            return null;

        }
        else
        {
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        await _cache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
        return await GetUserBasket(basket.UserName);
    }

    public async Task DeleteBasket(string userName)
    {
        await _cache.RemoveAsync(userName);
    }
}


