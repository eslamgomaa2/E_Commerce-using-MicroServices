using AutoMapper;
using Discount.Grpc.Proto;
using DisCount.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisCount.Application.Mapper
{
    public class DiscountProfile:Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon,GetDiscountResponse>().ReverseMap();

        }
    }

}
