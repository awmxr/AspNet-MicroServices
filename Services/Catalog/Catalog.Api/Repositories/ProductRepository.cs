﻿using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext catalog)
        {
            _context = catalog;
        }
        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(c => c.Id, id);

            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;    
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(c => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
