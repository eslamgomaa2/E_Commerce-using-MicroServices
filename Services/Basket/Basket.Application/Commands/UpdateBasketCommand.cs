using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Commands
{
    public record UpdateBasketCommand ( string UserName, List<ShopingCartItem> Items ):IRequest<ShopingCartResponse>;
    
}
