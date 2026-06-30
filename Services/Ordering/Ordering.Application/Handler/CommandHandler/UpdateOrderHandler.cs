using AutoMapper;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handler.CommandHandler
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IOrderingRepo _orderRepository;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(IOrderingRepo orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            // 1. جلب الطلب الحالي من قاعدة البيانات للتأكد من وجوده
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToUpdate is null)
            {
                // يمكنك هنا رمي NotFoundException مخصص، أو إرجاع null كما تفعل
                return null;
            }

          _mapper.Map<Order>(request);

            // 3. حفظ الكائن المحدث في قاعدة البيانات
            await _orderRepository.UpdateAsync(orderToUpdate);

            // 4. تحويل الكائن المحدث إلى OrderResponse وإرجاعه
            return _mapper.Map<OrderResponse>(orderToUpdate);
        }
    }
}