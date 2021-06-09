using Application.Interface;
using Application.Model;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task Add(ProductDTO product)
        {
            await productRepository.AddProduct(mapper.Map<Product>(product));
        }

        public async Task Delete(int id)
        {
            await productRepository.DeleteProduct(id);
        }

        public async Task<ProductDTO> Get(int id)
        {
            var product = await productRepository.GetProduct(id);
            return mapper.Map<ProductDTO>(product);
        }

        public async Task<ICollection<ProductDTO>> List()
        {
            var products = await productRepository.GetProducts();
            return mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> Update(ProductDTO product)
        {
            var updateProduct = mapper.Map<Product>(product);
            var result = await productRepository.UpdateProduct(updateProduct);
            return mapper.Map<ProductDTO>(result);
        }

        public async Task Upload(Stream fileStream)
        {
            StreamReader reader = new StreamReader(fileStream);
            string jsonString = reader.ReadToEnd();
            if (jsonString.Length == 0) throw new InvalidDataException("the file is empty");
            ProductFile result;
            try
            {
                result = JsonSerializer.Deserialize<ProductFile>(jsonString);
            }
            catch
            {
                throw new InvalidDataException("json file is not valid");
            }
            if(result.products == null) throw new InvalidDataException("json file is not valid");
            foreach (var product in result.products)
            {
                var listArticleProduct = new List<ArticleProductDTO>();
                foreach(var articleProduct in product.articles)
                {
                    listArticleProduct.Add(new ArticleProductDTO()
                    {
                        ArticleId = Convert.ToInt32(articleProduct.articleId),
                        Amount = articleProduct.amount
                    });
                }
                await Add(new ProductDTO()
                {
                    Name = product.name,
                    ArticleProducts = listArticleProduct,
                });
            }
        }
    }
}
