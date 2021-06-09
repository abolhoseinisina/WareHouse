using Application.Model;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IProductService
    {
        public Task<ICollection<ProductDTO>> List();
        public Task<ProductDTO> Get(int id);
        public Task Add(ProductDTO product);
        public Task Delete(int id);
        public Task<ProductDTO> Update(ProductDTO product);
        public Task Upload(Stream fileStream);
    }
}
