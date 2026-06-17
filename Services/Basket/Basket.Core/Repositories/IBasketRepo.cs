using Basket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Repositories
{
    public interface IBasketRepo
    {
        Task<ShopingCart> GetBasketByUserName(string userName);          
        Task<ShopingCart> UpdateBasket(ShopingCart shopingCart);    
        Task DeleteBasket(string username );    
        
    }
}
