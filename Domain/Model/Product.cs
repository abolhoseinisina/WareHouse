using System.Collections.Generic;

namespace Domain.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ICollection<ArticleProduct> ArticleProducts { get; set; }
    }
}
