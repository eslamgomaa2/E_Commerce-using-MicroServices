using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetProductByNameHandeller : IRequestHandler<GetProductsByNameQuery, IEnumerable<ProductResponseDto>>
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public GetProductByNameHandeller(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponseDto>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepo.GetProductByName(request.Name);
            var response = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return response;
        }
    }
}
