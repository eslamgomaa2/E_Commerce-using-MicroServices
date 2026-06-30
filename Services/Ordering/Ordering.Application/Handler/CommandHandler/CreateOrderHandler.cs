using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handler.CommandHandler
{
    public class CreateOrderHandler : IRequestHandler<AddOrderCommand, OrderResponse>
    {
        private readonly IOrderingRepo _orderingRepo;
        private readonly IMapper _mapper;

        public CreateOrderHandler(IOrderingRepo orderingRepo, IMapper mapper)
        {
            _orderingRepo = orderingRepo;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var order = await  _orderingRepo.AddAsync(orderEntity);
            return _mapper.Map<OrderResponse>(order);
        }
    }
}
