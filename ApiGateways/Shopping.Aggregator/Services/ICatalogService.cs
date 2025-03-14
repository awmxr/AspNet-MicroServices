using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatalogs();
        Task<IEnumerable<CatalogModel>> GetCatalogsByCategory(string category);
        Task<CatalogModel> GetCatalog(string id);
    }
}
