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
    }
}
