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
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }

        public async Task Add(ArticleDTO article)
        {
            await articleRepository.AddArticle(mapper.Map<Article>(article));
        }

        public async Task Delete(int id)
        {
            await articleRepository.DeleteArticle(id);
        }

        public async Task<ArticleDTO> Get(int id)
        {
            var article = await articleRepository.GetArticle(id);
            return mapper.Map<ArticleDTO>(article);
        }

        public async Task<ICollection<ArticleDTO>> List()
        {
            var articles = await articleRepository.GetArticles();
            return mapper.Map<List<ArticleDTO>>(articles);
        }

        public async Task<ArticleDTO> Update(ArticleDTO article)
        {
            var updateArticle = mapper.Map<Article>(article);
            var result = await articleRepository.UpdateArticle(updateArticle);
            return mapper.Map<ArticleDTO>(result);
        }

        public async Task Upload(Stream fileStream)
        {
            StreamReader reader = new StreamReader(fileStream);
            string jsonString = reader.ReadToEnd();
            if (jsonString.Length == 0) throw new InvalidDataException("the file is empty");
            InventoryFile result;
            try
            {
                result = JsonSerializer.Deserialize<InventoryFile>(jsonString);
            }
            catch
            {
                throw new InvalidDataException("json file is not valid");
            }
            if (result.inventory == null) throw new InvalidDataException("json file is not valid");
            foreach (var inventory in result.inventory)
            {
                if(inventory.stock == 0 || string.IsNullOrEmpty(inventory.name)) throw new InvalidDataException("stock or name cannot be zero or null");
                await Add(new ArticleDTO()
                {
                    ArticleId = Convert.ToInt32(inventory.articleId),
                    Name = inventory.name,
                    Stock = inventory.stock
                });
            }
        }
    }
}
