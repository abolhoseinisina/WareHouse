
namespace Domain.Model
{
    public class ArticleProduct
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
