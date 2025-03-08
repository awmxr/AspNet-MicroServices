using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        #region Constructor

        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;
        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion
        #region Get Discount

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(statusCode: StatusCode.NotFound, $"the product with name : {request.ProductName} is not exist!"));
            }
            _logger.LogInformation($"Discount is Retrived for Product Name : {request.ProductName}");
            return _mapper.Map<CouponModel>(coupon);



        }
        #endregion

        #region Create Discount
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.CreateDiscount(coupon);
            _logger.LogInformation($"The Product : {coupon.ProductName} successfully created.");

            return _mapper.Map<CouponModel>(coupon);
            //return base.CreateDiscount(request, context);
        }
        #endregion

        #region Update Discount
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var result = await _discountRepository.UpdateDiscount(coupon);
            if (result == true)
            {
                _logger.LogInformation($"The Product {coupon.ProductName} successfully Updated!");
            }
            else
            {
                _logger.LogError($"somthing went worng for Updating {coupon.ProductName}!");
            }

            return _mapper.Map<CouponModel>(coupon);
        }
        #endregion

        #region Delete Discount
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return new DeleteDiscountResponse() { Success = await _discountRepository.DeleteDiscount(request.ProductName) };

        }
        #endregion
    }
}
