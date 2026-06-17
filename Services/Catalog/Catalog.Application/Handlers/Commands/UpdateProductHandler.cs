using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetProductById(request.id);
            if (product is null)
                return false;
            var mapedproduct = _mapper.Map<Product>(product);
            return await _productRepo.UpdateProduct(mapedproduct);
        }
    }
}
