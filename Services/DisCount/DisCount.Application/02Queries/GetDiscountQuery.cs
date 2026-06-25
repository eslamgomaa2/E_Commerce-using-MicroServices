using Discount.Grpc.Proto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisCount.Application._02Queries
{
    public record GetDiscountQuery(string proudectname):IRequest<GetDiscountResponse>;
   
}
