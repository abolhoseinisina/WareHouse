using Application.Interface;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddProduct(Product product)
        {
            var productInDB = await context.Products.SingleOrDefaultAsync(a => a.Name == product.Name);
            if(productInDB != null) throw new ArgumentException("product is already available in the DB");
            if(string.IsNullOrEmpty(product.Name) || product.ArticleProducts?.Any() != true) throw new ArgumentException("product name or related articles can not be null");
            List<ArticleProduct> articleProducts = new List<ArticleProduct>();
            foreach(var articleProduct in product.ArticleProducts)
            {
                var article = await context.Articles.FindAsync(articleProduct.ArticleId);
                if (article == null)
                    throw new ArgumentException("there is no article with this id number in the DB: " + articleProduct.ArticleId);
                if (articleProduct.Amount == 0) throw new ArgumentException("amount of article can not be zero: " + article.Name);
                int sumOfAmount = articleProduct.Amount + context.ArticleProducts.Where(a => a.ArticleId == article.Id).Sum(a => a.Amount);
                if (sumOfAmount > article.Stock)
                    throw new ArgumentException("sum of 'amount's is more than 'stock' value for this article: " + article.Name);
                articleProducts.Add(new ArticleProduct()
                {
                    ArticleId = article.Id,
                    Amount = articleProduct.Amount
                });
            }
            await context.Products.AddAsync(new Product()
            {
                Name = product.Name,
                Price = product.Price,
                ArticleProducts = articleProducts
            });
            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("could not save changes to the database");
            }
        }

        public async Task DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) throw new ArgumentException("there is no product with this id number in the DB");
            context.Products.Remove(product);
            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("could not save changes to the database");
            }
        }

        public async Task<Product> GetProduct(int id)
        {
            return await context.Products.Include(a => a.ArticleProducts).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<ICollection<Product>> GetProducts()
        {
            return await context.Products.Include(a => a.ArticleProducts).ToListAsync();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var productInDB = await context.Products.Include(a => a.ArticleProducts).FirstOrDefaultAsync(a => a.Name == product.Name);
            if (productInDB == null) throw new ArgumentException("there is no product with this name in the DB");
            productInDB.Name = product.Name ?? productInDB.Name;
            List<ArticleProduct> listArticleProduct = new List<ArticleProduct>();
            foreach(var articleProduct in product.ArticleProducts)
            {
                var article = await context.Articles.FindAsync(articleProduct.ArticleId);
                if (article == null)
                    throw new ArgumentException("there is no article with this id number in the DB: " + articleProduct.ArticleId);
                if (articleProduct.Amount == 0)
                    throw new ArgumentException("amount of article can not be zero: " + article.Name);
                int sumOfAmount = articleProduct.Amount + 
                    (context.ArticleProducts.Any(a => a.ArticleId == articleProduct.ArticleId) ? context.ArticleProducts.Where(a => a.ArticleId == articleProduct.ArticleId).Sum(a => a.Amount) : 0) - 
                    (productInDB.ArticleProducts.Any(a => a.ArticleId == articleProduct.ArticleId) ? productInDB.ArticleProducts.SingleOrDefault(a => a.ArticleId == articleProduct.ArticleId).Amount : 0);
                if (sumOfAmount > article.Stock)
                    throw new ArgumentException("sum of 'amount' is more than 'stock' value for this article: " + article.Name);
                listArticleProduct.Add(articleProduct);
            }
            productInDB.ArticleProducts = listArticleProduct.Count == 0 ? productInDB.ArticleProducts : listArticleProduct;
            try
            {
                await context.SaveChangesAsync();
                return productInDB;
            }
            catch
            {
                throw new InvalidOperationException("could not save changes to the database");
            }
        }
    }
}
