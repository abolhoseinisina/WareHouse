using System.Collections.Generic;

namespace Domain.Model
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public ICollection<ArticleProduct> ArticleProducts { get; set; }
    }
}
