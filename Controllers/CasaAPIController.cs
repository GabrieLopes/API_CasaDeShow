using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CasaEventos.Data;
using CasaEventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasaEventos.Controllers
{

    [Route("api/v1/casas")]
    [ApiController]
    public class CasaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CasaAPIController(ApplicationDbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Listar todas as casas de show.
        /// </summary>
        [HttpGet]
        public IActionResult GetCasa()
        {
            var casas = _context.Casa.ToList();
            return Ok(casas);
        }

        /// <summary>
        /// Buscar por ID.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetCasa(int id)
        {
            if (id != 0)
            {
                try
                {
                    Casa casa = _context.Casa.First(c => c.CasaId == id);
                    return Ok(casa);

                }
                catch (Exception e)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existem casas criadas com o respectivo Id");
                }
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não existem casas criadas com o respectivo Id");
            }
        }
        /// <summary>
        /// Inserir casa de show.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] CasaTemp casaTemp)
        {
            /* Validação */
            if (casaTemp != null)
            {
                if (casaTemp.Nome.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "O nome da casa precisa ter mais do que 1 caracter." });
                }
                if (casaTemp.Endereco.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "O endereço da casa precisa ter mais do que 1 caracter." });
                }

                Casa casaAPI = new Casa();
                casaAPI.Nome = casaTemp.Nome;
                casaAPI.Endereco = casaTemp.Endereco;

                _context.Casa.Add(casaAPI);
                _context.SaveChanges();
                Response.StatusCode = 201; //Retorna o status Criado
                return new ObjectResult("Casa criada com sucesso."); // Funciona similar ao OK porem você precisa setar o Status Code e usar um new
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Requisição invalida o corpo não pode ser vazio." });
            }
        }


        /// <summary>
        /// Atualizar Casa de Show.
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody] Casa casa)
        {
            if (casa.CasaId > 0)
            {
                try
                {
                    var c = _context.Casa.First(casaTemp => casaTemp.CasaId == casa.CasaId);
                    if (c != null)
                    {
                        //Editar

                        //"IF ELSE reduzido"  
                        /*Condição ? faz algo : faz outra coisa, ou seja, se o dado nome for diferente de nulo que vem da minha requisição eu altero o nome do produto pelo nome
                        que vem na minha requisição, senão o nome que veio na requisição é nulo ele mantem o nome do produto*/
                        c.Nome = casa.Nome != null ? casa.Nome : c.Nome;
                        c.Endereco = casa.Endereco != null ? casa.Endereco : c.Endereco;

                        _context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Casa não encontrada." });
                    }
                }
                catch
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Casa não encontrada." });
                }

            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Id da casa é invalido." });
            }
        }

        /// <summary>
        /// Deletar uma Casa de Show.
        /// </summary>
        [HttpDelete("{id}")] //Especificando que irá trabalhar com um Id
        public IActionResult Delete(int id)
        {
            try
            {
                Casa casa = _context.Casa.First(c => c.CasaId == id);
                _context.Casa.Remove(casa);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }

        /// <summary>
        /// Listar as casas em odem alfabética crescente por nome.
        /// </summary>
        [HttpGet("asc")]
        public IActionResult GetCasaByAsc()
        {
            var casa = _context.Casa.OrderBy(nomeAsc => nomeAsc.Nome).ToList();
            return Ok(casa);

        }
        /// <summary>
        /// Listar as casas em odem alfabética decrescente por nome.
        /// </summary>
        [HttpGet("desc")]
        public IActionResult GetCasaByDesc()
        {
            var casa = _context.Casa.OrderByDescending(nomeDesc => nomeDesc.Nome).ToList();
            return Ok(casa);

        }
        /// <summary>
        /// Buscar por casa por nome.
        /// </summary>
        [HttpGet("nome")]
        public IActionResult GetCasaByNome(string Casa)
        {
            try
            {
                var casaNome = _context.Casa.Where(c => c.Nome == Casa).ToList();
                return Ok(casaNome);

            }
            catch (Exception e)
            {
                Response.StatusCode = 404; //Retorna o status Criado
                return new ObjectResult("Nome de casa invalido.");
            }
        }



        public class CasaTemp
        {
            public string Nome { get; set; }
            public string Endereco { get; set; }
        }


    }
}