using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handler.CommandsHandler
{
    public class UpdateBasketHandler : IRequestHandler<UpdateBasketCommand, ShopingCartResponse>
    {
        private readonly IBasketRepo _repo;
        private readonly IMapper _mapper;

        public UpdateBasketHandler(IMapper mapper, IBasketRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ShopingCartResponse> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var mapping = _mapper.Map<ShopingCart>(request);
            var basket = await _repo.UpdateBasket(mapping);
            var res= _mapper.Map<ShopingCartResponse>(basket);
            return res;
        }
    }
}
