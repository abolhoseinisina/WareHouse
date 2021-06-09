using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IProductRepository
    {
        public Task<ICollection<Product>> GetProducts();
        public Task<Product> GetProduct(int id);
        public Task AddProduct(Product article);
        public Task DeleteProduct(int id);
        public Task<Product> UpdateProduct(Product product);
    }
}
