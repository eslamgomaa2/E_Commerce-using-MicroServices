using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProductsByBrandQuery(string Brand) : IRequest<IEnumerable<ProductResponseDto>>
    {
    }
}
