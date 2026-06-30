using MediatR;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handler.CommandHandler
{
    internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderingRepo _orderingRepo;

        public DeleteOrderHandler(IOrderingRepo orderingRepo)
        {
            _orderingRepo = orderingRepo;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderingRepo.GetByIdAsync(request.Id);  
            if (order is null) return false;

            var res = _orderingRepo.DeleteAsync(order);
            if (res)
            {

                return true;
            }
            return false;
        }


        
    }
}
    
    

