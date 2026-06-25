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
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CreateDiscountResponse>
    {
        private readonly IDiscountRepo _discountRepo;

        public CreateDiscountHandler(IDiscountRepo discountRepo)
        {
            _discountRepo = discountRepo;
        }

        public async Task<CreateDiscountResponse> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
          var res=await _discountRepo.CreateDiscount(request.Coupon);
            if (res)
            {
                return new CreateDiscountResponse
                {
                    Success = true,
                    Message = "Discount created successfully."
                };
            }
            else
            {
                return new CreateDiscountResponse
                {
                    Success = false,
                    Message = "Failed to create discount."
                };
            }

        }
    }
}
