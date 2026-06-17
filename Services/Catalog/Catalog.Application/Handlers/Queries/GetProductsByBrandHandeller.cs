using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetProductsByBrandHandeller : IRequestHandler<GetProductsByBrandQuery, IEnumerable<ProductResponseDto>>
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public GetProductsByBrandHandeller(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponseDto>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var Products = await _productRepo.GetProductsByBrand(request.Brand);
            var Responses = _mapper.Map<IEnumerable<ProductResponseDto>>(Products);
            return Responses;
        }
    }
}
