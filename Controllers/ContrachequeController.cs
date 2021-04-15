using desafioStone.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafioStone.Negocio;

namespace desafioStone.Controllers
{
    [ApiController]
    [Route("v1/contracheques")]
    public class ContrachequeController : ControllerBase
    {
        //[HttpGet]
        //[Route("")]
        //public async Task<ActionResult<List<ContrachequeViewModel>>> Get(
        //    [FromServices] DesafioStoneEntities context)
        //{

        //}

        [HttpGet]
        [Route("{id:int}")]
        public ContrachequeViewModel GetById([FromServices] DesafioStoneEntities context, int id)
        {
            var negocio = new ContrachequeNegocio();
            var contracheque = negocio.GerarContracheque(context, id, "Janeiro");
            return contracheque;
        }
        
    }
}
