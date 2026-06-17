using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetProductByIdHandeller : IRequestHandler<GetProuductByIdQuery, ProductResponseDto>
    {

        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public GetProductByIdHandeller(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> Handle(GetProuductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetProductById(request.id);
            var response = _mapper.Map<ProductResponseDto>(product);
            return response;
        }
    }
}
