using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllProductHandeller : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponseDto>>
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public GetAllProductHandeller(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<Pagination<ProductResponseDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var Products = await _productRepo.GetAllProduct(request.Spec);
            var response = _mapper.Map<Pagination<ProductResponseDto>>(Products);
            return response;
        }
    }
}
