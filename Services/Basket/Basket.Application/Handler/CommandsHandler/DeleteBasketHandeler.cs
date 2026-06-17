using AutoMapper;
using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handler.CommandsHandler
{
    public class DeleteBasketHandeler : IRequestHandler<DeletebasketCommand>
    {
        private readonly IBasketRepo _basketRepo;

       
        public DeleteBasketHandeler(IBasketRepo basketRepo)
        {
            _basketRepo = basketRepo;
        }

        public async Task Handle(DeletebasketCommand request, CancellationToken cancellationToken)
        {
            await _basketRepo.DeleteBasket(request.Username);
        }
    }
}
