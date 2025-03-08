using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcService
    {
        #region Constructor
        private readonly DiscountProtoService.DiscountProtoServiceClient _client;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient client)
        {
            _client = client;
        }
        #endregion


        #region Get Discount
        public async Task<CouponModel> GetDicsout(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };

            return await _client.GetDiscountAsync(discountRequest);
        }
        #endregion

    }
}
