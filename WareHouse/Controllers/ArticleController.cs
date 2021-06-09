using Application.Interface;
using Application.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WareHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ArticleDTO>>> List()
        {
            return Ok(await articleService.List());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDTO>> Get(int id)
        {
            var article = await articleService.Get(id);
            if (article == null) return NotFound("there is no article with this id");
            return Ok(article);
        }

        [HttpPost]
        public async Task<ActionResult> Add(ArticleDTO article)
        {
            if (article == null) return BadRequest("input is empty");
            try
            {
                await articleService.Add(article);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await articleService.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ArticleDTO>> Update(ArticleDTO article)
        {
            if (article == null) return BadRequest("input is empty");
            try
            {
                var result = await articleService.Update(article);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file == null) return BadRequest("select a file to upload");
            var fileStream = file.OpenReadStream();
            try
            {
                await articleService.Upload(fileStream);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
