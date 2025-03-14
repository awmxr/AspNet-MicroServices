using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System.Net;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;

        public ShoppingsController(IBasketService basketService, IOrderService orderService, ICatalogService catalogService)
        {
            _basketService = basketService;
            _orderService = orderService;
            _catalogService = catalogService;
        }
        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            var basket = await _basketService.GetBasket(userName);
            if (basket != null && basket.Items != null && basket.Items.Count > 0)
            {

                foreach (var basketItem in basket.Items)
                {
                    var product = await _catalogService.GetCatalog(basketItem.ProductId);
                    basketItem.ProductName = product.Name;
                    basketItem.Summary = product.Summary;
                    basketItem.Description = product.Description;
                    basketItem.Price = product.Price;
                    basketItem.ImageFile = product.ImageFile;
                }
            }

            var orders = await _orderService.GetOrderByUserName(userName);

            var shoppingModel = new ShoppingModel()
            {
                UserName = userName,
                BasketWithProduct = basket,
                Orders = orders
            };

            return Ok(shoppingModel);

        }
    }
}
