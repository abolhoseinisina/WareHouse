using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Model
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public ICollection<ArticleProductDTO> ArticleProducts { get; set; }
    }
}
