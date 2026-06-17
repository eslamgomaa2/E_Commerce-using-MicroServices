using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetAllProductsQuery : IRequest<Pagination<ProductResponseDto>>
    {
        public CatalogSpecParams Spec { get; set; }
        public GetAllProductsQuery(CatalogSpecParams pram)
        {
            Spec=pram;
            
        }
    }
}
