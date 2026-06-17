using AutoMapper;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handler.QueriesHandler
{
    public class GetBasketByUserNameHandeler : IRequestHandler<GetBasketByUserNameQuery, ShopingCartResponse>
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public GetBasketByUserNameHandeler(IBasketRepo basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        public async Task<ShopingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepo.GetBasketByUserName(request.userName);
            
                var res=  _mapper.Map<ShopingCartResponse>(basket);
                return res;
            
        }
    }
}
