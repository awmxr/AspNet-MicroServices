using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetSeedData());
            }
        }

        private static IEnumerable<Product> GetSeedData()
        {
            return new List<Product>
                        {
                            new Product
                            {
                                Id = "5f47ac10b5e4c0001c8d4c1a",
                                Name = "iPhone X",
                                Category = "Smartphone",
                                Description = "Apple iPhone X with 64GB memory",
                                Summary = "Apple iPhone X",
                                ImageFile = "iphonex.png",
                                Price = 999.99M
                            },
                            new Product
                            {
                                Id = "5f47ac10b5e4c0001c8d4c1b",
                                Name = "Samsung Galaxy S10",
                                Category = "Smartphone",
                                Description = "Samsung Galaxy S10 with 128GB memory",
                                Summary = "Samsung Galaxy S10",
                                ImageFile = "samsungs10.png",
                                Price = 899.99M
                            },
                            new Product
                            {
                                Id = "5f47ac10b5e4c0001c8d4c1c",
                                Name = "Google Pixel 4",
                                Category = "Smartphone",
                                Description = "Google Pixel 4 with 64GB memory",
                                Summary = "Google Pixel 4",
                                ImageFile = "googlepixel4.png",
                                Price = 799.99M
                            }
                        };
        }
    }
}
