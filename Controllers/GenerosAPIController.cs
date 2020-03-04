using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CasaEventos.Data;
using CasaEventos.Models;
using Microsoft.AspNetCore.Mvc;

namespace CasaEventos.Controllers
{
    [Route("api/v1/generos")]
    [ApiController]
    public class GenerosAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GenerosAPIController(ApplicationDbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Listar todos os generos.
        /// </summary>
        [HttpGet]
        public IActionResult GetGenero()
        {
            var generos = _context.Genero.ToList();
            return Ok(generos);
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
                    Genero genero = _context.Genero.First(g => g.GeneroId == id);
                    return Ok(genero);

                }
                catch (Exception e)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Genero não encontrado.");
                }
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não existem generos criados");
            }
        }
        /// <summary>
        /// Inserir genero.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] GeneroTemp generoTemp)
        {
            /* Validação */
            if (generoTemp != null)
            {
                if (generoTemp.GeneroNome.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "O nome do genero precisa ter mais do que 1 caracter." });
                }

                Genero generoAPI = new Genero();
                generoAPI.GeneroNome = generoTemp.GeneroNome;

                _context.Genero.Add(generoAPI);
                _context.SaveChanges();
                Response.StatusCode = 201; //Retorna o status Criado
                return new ObjectResult("Genero criado com sucesso."); // Funciona similar ao OK porem você precisa setar o Status Code e usar um new
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Requisição invalida o corpo não pode ser vazio." });
            }
        }


        /// <summary>
        /// Atualizar Genero.
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody] Genero genero)
        {
            if (genero.GeneroId > 0)
            {
                try
                {
                    var g = _context.Genero.First(generoTemp => generoTemp.GeneroId == genero.GeneroId);
                    if (g != null)
                    {
                        g.GeneroNome = genero.GeneroNome != null ? genero.GeneroNome : g.GeneroNome;
                        _context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Genero não encontrado." });
                    }
                }
                catch
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Genero não encontrado." });
                }

            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Id do genero é invalido." });
            }
        }

        /// <summary>
        /// Deletar um Genero.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Genero genero = _context.Genero.First(g => g.GeneroId == id);
                _context.Genero.Remove(genero);
                _context.SaveChanges();
                return Ok(new { msg = "Genero excluido." });
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Genero não encontrado.");
            }
        }
        public class GeneroTemp
        {
            [Required(ErrorMessage = "Nome do genero é obrigatório!")]
            [StringLength(100, ErrorMessage = "Nome do genero é muito grande, tente um nome menor")]
            [MinLength(2, ErrorMessage = "Nome do genero é muito pequeno, tente um nome com pelo menos 2 caracteres")]
            public string GeneroNome { get; set; }
        }
    }
}