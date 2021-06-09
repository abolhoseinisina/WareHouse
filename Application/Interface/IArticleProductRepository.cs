using Domain.Model;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IArticleProductRepository
    {
        public Task<ArticleProduct> GetArticleProduct(int articleId, int productId);
        public Task<int> GetSumAmount(int id);
    }
}
