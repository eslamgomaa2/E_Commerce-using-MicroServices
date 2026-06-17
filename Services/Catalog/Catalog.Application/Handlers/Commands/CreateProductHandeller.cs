using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands
{
    public class CreateProductHandeller : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepo _brandRepo;
        private readonly IMapper _mapper;

        public CreateProductHandeller(IProductRepo brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            var result = await _brandRepo.CreateProduct(product);
            return result;

        }
    }
}
