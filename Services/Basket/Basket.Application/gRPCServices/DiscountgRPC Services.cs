using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.gRPCServices
{
    public class DiscountgRPC_Services
    {
        private readonly Discount.Grpc.Proto.DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountgRPC_Services(Discount.Grpc.Proto.DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }
        public async Task<Discount.Grpc.Proto.GetDiscountResponse> GetDiscountAsync(string productName)
        {
            var request = new Discount.Grpc.Proto.GetDiscountRequest { ProductName = productName };
            return await _discountProtoServiceClient.GetDiscountAsync(request);
        }
    }
}
