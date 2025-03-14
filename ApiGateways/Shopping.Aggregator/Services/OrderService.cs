using Shopping.Aggregator.Extentions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName)
        {
            var response = await _client.GetAsync($"/api/v1/Orders/{userName}");
            return await HttpClientExtentions.ReadContentAs<List<OrderResponseModel>>(response);
        }
    }
}
