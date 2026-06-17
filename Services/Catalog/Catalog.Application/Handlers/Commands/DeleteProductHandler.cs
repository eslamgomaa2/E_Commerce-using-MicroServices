using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public DeleteProductHandler(IProductRepo brandRepo, IMapper mapper)
        {
            _productRepo = brandRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productexist = await _productRepo.GetProductById(request.id);
            if (productexist == null)
            {
                return false;
            }
            return await _productRepo.DeleteProduct(productexist.Id);

        }
    }
}
