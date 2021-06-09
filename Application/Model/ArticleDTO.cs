using System.ComponentModel.DataAnnotations;

namespace Application.Model
{
    public class ArticleDTO
    {
        public int ArticleId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
