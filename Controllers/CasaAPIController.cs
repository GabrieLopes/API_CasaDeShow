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
                catch (Exception)
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
                try
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
                catch (Exception)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Requisição invalida o corpo não pode ser vazio." });
                }
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
        public IActionResult Patch([FromBody] CasaTemp casaTemp)
        {
            try
            {
                if (_context.Casa.Count() > 0)
                {
                    try
                    {
                        Casa casa = _context.Casa.First(casaTemp => casaTemp.CasaId == casaTemp.CasaId);
                        if (casaTemp.CasaId > 0)
                        {
                            if (casaTemp.Nome != null)
                            {
                                if (casaTemp.Nome.Length <= 1)
                                {
                                    Response.StatusCode = 400;
                                    return new ObjectResult(new { msg = "O nome do evento precisa ter mais do que 1 caracter." });
                                }
                                else
                                {
                                    casa.Nome = casaTemp.Nome;
                                    _context.SaveChanges();
                                    Response.StatusCode = 200;
                                    return new ObjectResult(new { msg = "Nome alterado com sucesso." });
                                }
                            }
                            if (casaTemp.Endereco != null)
                            {
                                if (casaTemp.Endereco.Length <= 1)
                                {
                                    Response.StatusCode = 400;
                                    return new ObjectResult(new { msg = "O endereço do evento precisa ter mais do que 1 caracter." });
                                }
                                else
                                {
                                    casa.Endereco = casaTemp.Endereco;
                                    _context.SaveChanges();
                                    Response.StatusCode = 200;
                                    return new ObjectResult(new { msg = "Endereço alterado com sucesso." });
                                }
                            }
                        }
                        else
                        {
                            Response.StatusCode = 400;
                            return new ObjectResult(new { msg = "Não é possivel alterar uma casa vazia." });
                        }
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Não é possivel alterar uma casa vazia." });

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
            catch (Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Requisição invalida, o corpo não pode ser vazio." });
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
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Id de casa é invalido.");
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
        /// Buscar casa por nome.
        /// </summary>
        [HttpGet("nome")]
        public IActionResult GetCasaByNome(string Casa)
        {
            if (_context.Casa.Count() > 0)
            {
                if (_context.Casa.Where(c => c.Nome == Casa).Count() == 0)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Nome de casa invalido ou não existe.");
                }
                else
                {
                    try
                    {
                        var casaNome = _context.Casa.Where(c => c.Nome == Casa).ToList();
                        return Ok(casaNome);

                    }

                    catch (Exception)
                    {
                        Response.StatusCode = 404;
                        return new ObjectResult("Nome de casa invalido.");
                    }
                }

            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult("Nome de casa invalido ou não existe.");
            }
        }



        public class CasaTemp
        {
            public int CasaId { get; set; }
            public string Nome { get; set; }
            public string Endereco { get; set; }
        }


    }
}