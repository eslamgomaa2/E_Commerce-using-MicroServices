using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IEnumerable<TypesResponseDto>>
    {
        private readonly IProducttypeRepo _producttypeRepo;
        private readonly IMapper _mapper;

        public GetAllTypesHandler(IProducttypeRepo producttypeRepo, IMapper mapper)
        {
            _producttypeRepo = producttypeRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TypesResponseDto>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _producttypeRepo.GetAllProductType();
            var res= _mapper.Map<IEnumerable<TypesResponseDto>>(types);
            return res;
            
        }
    }
}
