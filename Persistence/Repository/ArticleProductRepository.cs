using Application.Interface;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class ArticleProductRepository : IArticleProductRepository
    {
        private readonly DataContext context;

        public ArticleProductRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<ArticleProduct> GetArticleProduct(int articleId, int productId)
        {
            return await context.ArticleProducts.SingleOrDefaultAsync(a => a.ProductId == productId && a.ArticleId == articleId);
        }

        public async Task<int> GetSumAmount(int id)
        {
            return await context.ArticleProducts.Where(a => a.ArticleId == id).SumAsync(a => a.Amount);
        }
    }
}
