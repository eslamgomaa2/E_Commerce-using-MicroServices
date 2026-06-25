using Discount.Grpc.Proto;
using DisCount.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisCount.Application._01Commands
{
    public record CreateDiscountCommand(Coupon Coupon):IRequest<CreateDiscountResponse>;
    
}
