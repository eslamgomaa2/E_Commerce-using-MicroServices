using Basket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Responses
{
    public class ShopingCartResponse
    {
        public string UserName { get; set; }

        public List<ShopingCartItem> Items { get; set; } = new List<ShopingCartItem>();
        public decimal TotalPrice 
        { get
            {
                var totalpeice = 0;
                foreach (var item in Items)
                {
                    totalpeice += item.Price * item.Quantity;

                }
                return totalpeice;
            }
        }
        public ShopingCartResponse()
        {

        }
        public ShopingCartResponse(string UserName)
        {
            this.UserName = UserName;

        }
    }
}
