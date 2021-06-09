using System.ComponentModel.DataAnnotations;

namespace Application.Model
{
    public class ArticleProductDTO
    {
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
