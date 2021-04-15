using desafioStone.Data;
using desafioStone.Models;
using desafioStone.Negocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafioStone.Controllers
{

    [ApiController]
    [Route("v1/funcionarios")]
    public class FuncionarioController: ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<FuncionarioViewModel>>> Get(
            [FromServices] DesafioStoneEntities context)
        {
            var funcionarios = await context.Funcionarios.ToListAsync();
            return funcionarios;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<FuncionarioViewModel> GetById([FromServices] DesafioStoneEntities context, int id)
        {
            var funcionario = await context.Funcionarios.FindAsync(id);
            return funcionario;
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<FuncionarioViewModel>> Post(
            [FromServices] DesafioStoneEntities context,
            [FromBody]FuncionarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.Funcionarios.Add(model);

                if (!Utils.RetornarDocumentoValido(model.Documento))
                {
                    Console.Error.WriteLine("Documento inválido.");
                    return null;
                }

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
