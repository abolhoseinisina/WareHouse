using Application.Interface;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext context;

        public ArticleRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddArticle(Article article)
        {
            var articleInDB = await context.Articles.SingleOrDefaultAsync(a => a.Name == article.Name);
            if (articleInDB != null) throw new ArgumentException("this article is already available in the DB");
            if(string.IsNullOrEmpty(article.Name) || article.Stock == 0) throw new ArgumentException("stock or name cannot be zero or null"); 
            await context.Articles.AddAsync(article);
            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("could not save changes to the database");
            }
        }

        public async Task DeleteArticle(int id)
        {
            var article = await context.Articles.FindAsync(id);
            if (article == null) throw new ArgumentException("there is no article with this id number in the DB");
            context.Articles.Remove(article);
            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("can not save changes to the database");
            }
        }

        public async Task<Article> GetArticle(int id)
        {
            return await context.Articles.FindAsync(id);
        }

        public async Task<ICollection<Article>> GetArticles()
        {
            return await context.Articles.ToListAsync();
        }

        public async Task<Article> UpdateArticle(Article article)
        {
            var articleInDB = await context.Articles.FindAsync(article.Id);
            if (articleInDB == null) throw new ArgumentException("there is no article with this id number in the DB");
            if (articleInDB.Stock == 0) throw new ArgumentException("stock can not be zero");
            articleInDB.Name = article.Name ?? articleInDB.Name;
            articleInDB.Stock = article.Stock;
            try
            {
                await context.SaveChangesAsync();
                return articleInDB;
            }
            catch
            {
                throw new InvalidOperationException("could not save changes to the database");
            }
        }
    }
}
