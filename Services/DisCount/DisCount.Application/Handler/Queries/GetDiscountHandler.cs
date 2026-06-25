using Discount.Grpc.Proto;
using DisCount.Application._02Queries;
using DisCount.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisCount.Application.Handler.Queries
{
    public class GetDiscountHandler : IRequestHandler<GetDiscountQuery, GetDiscountResponse>
    {
        private readonly IDiscountRepo _discountRepo;

        public GetDiscountHandler(IDiscountRepo discountRepo)
        {
            _discountRepo = discountRepo;
        }

        public async Task<GetDiscountResponse> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
           var res= await _discountRepo.GetDiscount(request.proudectname);
            if (res == null)
            {
                return new GetDiscountResponse
                {
                    ProductName = request.proudectname,
                    Amount = 0,
                    Description = "No Discount Found"
                };
            }
            return new GetDiscountResponse
            {

                ProductName = res.ProductName,
                Amount = res.Amount,
                Description = res.Description
            };
        }
    }
}
