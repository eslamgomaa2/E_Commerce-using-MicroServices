using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProuductByIdQuery(string id) : IRequest<ProductResponseDto>
    {
    }
}
