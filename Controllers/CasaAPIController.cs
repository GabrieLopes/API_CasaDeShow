using System;
using System.Linq;
using CasaEventos.Data;
using CasaEventos.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetCasa()
        {
            var casas = _context.Casa.ToList();
            return Ok(casas);
        }

        [HttpGet("{id}")]
        public IActionResult GetCasa(int id)
        {
            try
            {
                Casa casa = _context.Casa.First(c => c.CasaId == id);
                return Ok(casa);

            }
            catch (Exception e)
            {
                Response.StatusCode = 404; //Retorna o status Criado
                return new ObjectResult("");
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] CasaTemp casaTemp)
        {
            /* Validação */
            if (casaTemp.Nome == null)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "O nome da casa precisa ter mais do que 1 caracter." });
            }
            if (casaTemp.Endereco == null)
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
            return new ObjectResult(""); // Funciona similar ao OK porem você precisa setar o Status Code e usar um new

        }
        public class CasaTemp
        {
            public string Nome { get; set; }
            public string Endereco { get; set; }
        }

    }
}