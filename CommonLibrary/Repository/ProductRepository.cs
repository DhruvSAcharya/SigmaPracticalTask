using CommonLibrary.Interface;
using CommonLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext;
        public ProductRepository(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            appDbContext = dbContextFactory.CreateDbContext();
        }

        public async Task AddAsync(Product product)
        {
            var category = new Category
            {
                CategoryName = product.Category.CategoryName
            };
            appDbContext.Categories.Add(category);

            await appDbContext.SaveChangesAsync();

            product.CategoryId = category.CategoryId;

            appDbContext.Products.Add(product);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await appDbContext.Products.FindAsync(id);

            if (product != null)
            {
                appDbContext.Products.Remove(product);
                await appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
        }

        public async Task<Product> GetAsync(int id)
        {
            var product = await appDbContext.Products
                .Include(p => p.Category) 
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            List<Product> products = await appDbContext.Products.ToListAsync();
            foreach(Product p in products)
            {
                p.Category = await appDbContext.Categories.FindAsync(p.CategoryId);
            }
            return products;
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await appDbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductID == product.ProductID);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.ProductID} not found.");
            }

            // Update product fields
            existingProduct.Name = product.Name;
            existingProduct.SKU = product.SKU;
            existingProduct.BasePrice = product.BasePrice;
            existingProduct.MRP = product.MRP;
            existingProduct.Description = product.Description;
            existingProduct.Currency = product.Currency;
            existingProduct.ManufacturedDate = product.ManufacturedDate;
            existingProduct.ExpireDate = product.ExpireDate;

            // Update the category if it has changed
            if (existingProduct.CategoryId != product.CategoryId)
            {
                var category = await appDbContext.Categories.FindAsync(product.CategoryId);

                if (category == null)
                {
                    throw new KeyNotFoundException($"Category with ID {product.CategoryId} not found.");
                }
                existingProduct.Category.CategoryName = product.Category.CategoryName;
                existingProduct.CategoryId = category.CategoryId;
            }

            // Save changes
            await appDbContext.SaveChangesAsync();
        }
    }
}
