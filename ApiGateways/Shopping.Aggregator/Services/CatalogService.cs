using Shopping.Aggregator.Extentions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var response = await _client.GetAsync($"/api/v1/Catalogs/{id}");
            return await HttpClientExtentions.ReadContentAs<CatalogModel>(response);
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogs()
        {
            var response = await _client.GetAsync("/api/v1/Catalogs");
            return await HttpClientExtentions.ReadContentAs<List<CatalogModel>>(response);
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogsByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/Catalogs/GetProductByCategory/{category}");
            return await HttpClientExtentions.ReadContentAs<List<CatalogModel>>(response);
        }
    }
}
