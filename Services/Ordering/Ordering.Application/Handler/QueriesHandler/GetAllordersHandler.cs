using AutoMapper;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handler.QueriesHandler
{
    public class GetAllOrdersHandler : IRequestHandler<GetOrdersByUserName, IEnumerable<OrderResponse>>
    {
        private readonly IOrderingRepo _orderingRepo;
        private readonly IMapper _mapper;

        public GetAllOrdersHandler(IMapper mapper, IOrderingRepo orderingRepo)
        {
            _mapper = mapper;
            _orderingRepo = orderingRepo;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByUserName request, CancellationToken cancellationToken)
        {
            var orders = await _orderingRepo.GetOrdersByUserNameAsync(request.username);
            var res =_mapper.Map<IEnumerable<OrderResponse>>(orders);
            return res;

        }
    }
}
