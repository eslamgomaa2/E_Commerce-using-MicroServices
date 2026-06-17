using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllBrandHandeller : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandsResponseDto>>
    {
        private readonly IBrandRepo _brandRepo;
        private readonly IMapper _mapper;

        public GetAllBrandHandeller(IBrandRepo brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandsResponseDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepo.GetAllBrands();

            
            var response = _mapper.Map<IEnumerable<BrandsResponseDto>>(brands);

            return response;
        
        }
    }
}
