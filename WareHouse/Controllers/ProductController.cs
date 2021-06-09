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
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProductDTO>>> List()
        {
            return Ok(await productService.List());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await productService.Get(id);
            if(product == null) return NotFound("there is no product with this id");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Add(ProductDTO product)
        {
            if (product == null) return BadRequest("input is empty");
            try 
            {
                await productService.Add(product);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await productService.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>> Update(ProductDTO product)
        {
            if (product == null) return BadRequest("input is empty");
            try
            {
                var result = await productService.Update(product);
                return Ok(result);
            }
            catch (Exception e)
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
                await productService.Upload(fileStream);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
