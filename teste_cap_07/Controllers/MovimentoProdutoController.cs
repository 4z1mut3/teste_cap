using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teste_cap_07.Models;
using teste_cap_07.Data;
using System.Web.Http.ModelBinding;

namespace teste_cap_07.Controllers
{
    [ApiController]
    [Route("v1/")]
    public class MovimentoProdutoController : ControllerBase 
    {

        [HttpGet]
        [Route("/")]
        public async Task<ActionResult<List<MovimentoProduto>>> Get([FromServices] DataContext context)
        {
            var movimentoProdutos = await context.movimentoProdutos.ToListAsync();
            return movimentoProdutos;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<MovimentoProduto>> Post([FromServices] DataContext context, [FromBody] MovimentoProduto model) 
        {
            if (ModelState.IsValid)
            {
                context.movimentoProdutos.Add(model);
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