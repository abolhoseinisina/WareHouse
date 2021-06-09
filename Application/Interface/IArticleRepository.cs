using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IArticleRepository
    {
        public Task<ICollection<Article>> GetArticles();
        public Task<Article> GetArticle(int id);
        public Task AddArticle(Article article);
        public Task DeleteArticle(int id);
        public Task<Article> UpdateArticle(Article article);
    }
}
