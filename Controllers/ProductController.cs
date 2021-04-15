using desafioStone.Data;
using desafioStone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafioStone.Controllers
{

    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get(
            [FromServices] DataContext context)
        {
            //var products = await context.Products.Include(x => x.Funcionario).ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Product> GetById([FromServices] DataContext context, int id)
        {
            var product = await context.Products.Include(x => x.Funcionario)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        [HttpGet]
        [Route("funcionarios/{id:int}")]
        public async Task<List<Product>> GetByFuncionario([FromServices] DataContext context, int id)
        {
            var products = await context.Products
                .Include(x => x.Funcionario)
                .AsNoTracking()
                .Where(x => x.FuncionarioId == id)
                .ToListAsync();

            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post(
            [FromServices] DataContext context,
            [FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}

