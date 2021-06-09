using Application.Model;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IArticleService
    {
        public Task<ICollection<ArticleDTO>> List();
        public Task<ArticleDTO> Get(int id);
        public Task Add(ArticleDTO product);
        public Task Delete(int id);
        public Task<ArticleDTO> Update(ArticleDTO article);
        public Task Upload(Stream fileStream);
    }
}
