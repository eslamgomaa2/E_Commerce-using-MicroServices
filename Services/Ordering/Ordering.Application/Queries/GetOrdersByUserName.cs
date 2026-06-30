using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries
{
    public record GetOrdersByUserName(string username):IRequest<IEnumerable<OrderResponse>>;
    
}
