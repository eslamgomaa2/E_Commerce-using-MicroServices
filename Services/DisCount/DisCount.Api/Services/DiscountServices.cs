using Discount.Grpc.Proto;
using DisCount.Application._01Commands;
using DisCount.Application._02Queries;
using DisCount.Core.Entities;
using Grpc.Core;
using MediatR;

namespace DisCount.Api.Services
{
        public class DiscountServices: DiscountProtoService.DiscountProtoServiceBase
        {
            private readonly IMediator _mediator;

            public DiscountServices(IMediator mediator)
            {
                _mediator = mediator;
            }

            public override async Task<CreateDiscountResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
            {
                var command = new CreateDiscountCommand(
                    new Coupon
                    {
                        ProductName = request.ProductName,
                        Amount = request.Amount,
                        Description = request.Description
                    }
                );

                var res = await _mediator.Send(command);
                return res;
            }

        public override async Task<UpdateDiscountResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var res= await _mediator.Send(new UpdateDiscountCommand(
                new Coupon
                {
                    Id = request.Id,
                    ProductName = request.ProductName,
                    Amount = request.Amount,
                    Description = request.Description
                    
                }
            ));
            return res;
        }
        public override async Task<GetDiscountResponse> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery(request.ProductName);
            var res = await _mediator.Send(query);
            return res; 
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var command = new DeleteDiscountCommand(request.ProductName);
            var res = await _mediator.Send(command);
            return res;
        }
    }
}
