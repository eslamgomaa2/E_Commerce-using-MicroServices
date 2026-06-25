using Discount.Grpc.Proto;
using DisCount.Application._01Commands;
using DisCount.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisCount.Application.Handler.Commands
{
    public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, UpdateDiscountResponse>
    {
        private readonly IDiscountRepo _discountRepo;

        public UpdateDiscountHandler(IDiscountRepo discountRepo)
        {
            _discountRepo = discountRepo;
        }

        public async Task<UpdateDiscountResponse> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
           var res= await _discountRepo.UpdateDiscount(request.coupon);
            if (res)
            {
                return new UpdateDiscountResponse
                {
                    Success = true,
                    Message = "Discount updated successfully."
                };
            }
            else
            {
                return new UpdateDiscountResponse
                {
                    Success = false,
                    Message = "Failed to update discount."
                };
            }
        }
    }
}
