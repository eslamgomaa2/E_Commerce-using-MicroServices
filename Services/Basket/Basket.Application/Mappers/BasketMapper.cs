using AutoMapper;
using Basket.Application.Responses;
using Basket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Mappers
{
    public class BasketMapper:Profile
    {

        public BasketMapper()
        {
            CreateMap<ShopingCartItem, ShopingCartResponse>().ReverseMap();
            CreateMap<ShopingCart, ShopingCartResponse>().ReverseMap();
        }
    }
}
