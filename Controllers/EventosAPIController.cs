using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using CasaEventos.Data;
using CasaEventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasaEventos.Controllers
{
    [Route("api/v1/eventos")]
    [ApiController]
    public class EventosAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventosAPIController(ApplicationDbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Listar todas os eventos.
        /// </summary>
        [HttpGet]
        public IActionResult GetEvento()
        {
            var eventos = _context.Evento.ToList();
            return Ok(eventos);
        }
        /// <summary>
        /// Salvar um evento.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] EventoTemp eventoTemp)
        {
            /* Validação */
            if (eventoTemp != null)
            {

                if (eventoTemp.NomeEvento.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "O nome do evento precisa ter mais do que 1 caracter." });
                }
                if (eventoTemp.CapacidadeEvento <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "A capacidade precisa ser superior a 0." });
                }
                if (eventoTemp.QuantidadeIngressos <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "A quantidade de ingressos precisa ser superior a 0." });
                }
                if (eventoTemp.DataEvento == null)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Coloque uma data válida." });
                }
                if (eventoTemp.ValorIngresso <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "O preço do ingresso precisa ser superior a 0." });
                }
                if (eventoTemp.Casa.CasaId <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Id de casa inválido." });
                }
                if (eventoTemp.Genero.GeneroId <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Id de genero inválido." });
                }

                Evento eventoAPI = new Evento();
                eventoAPI.NomeEvento = eventoTemp.NomeEvento;
                eventoAPI.CapacidadeEvento = eventoTemp.CapacidadeEvento;
                eventoAPI.QuantidadeIngressos = eventoTemp.QuantidadeIngressos;
                eventoAPI.DataEvento = eventoTemp.DataEvento;
                eventoAPI.ValorIngresso = eventoTemp.ValorIngresso;
                eventoAPI.Casa = _context.Casa.First(cs => cs.CasaId == eventoTemp.Casa.CasaId);
                eventoAPI.Genero = _context.Genero.First(cs => cs.GeneroId == eventoTemp.Genero.GeneroId);

                _context.Evento.Add(eventoAPI);
                _context.SaveChanges();
                Response.StatusCode = 201;
                return new ObjectResult("Evento criada com sucesso.");
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Requisição inválida, o corpo não pode ser vazio." });
            }
        }

        /// <summary>
        /// Buscar por ID.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetEvento(int id)
        {
            if (id != 0)
            {
                try
                {
                    Evento evento = _context.Evento.First(c => c.EventoId == id);
                    return Ok(evento);

                }
                catch (Exception e)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("");
                }
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não existem casas criadas");
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
        [HttpGet("{nome}")]
        public IActionResult GetCasaByNome(string nome)
        {
            try
            {
                Casa casa = _context.Casa.First(c => c.Nome == nome);
                return Ok(casa);

            }
            catch (Exception e)
            {
                Response.StatusCode = 404; //Retorna o status Criado
                return new ObjectResult("");
            }
        }



        public class EventoTemp
        {
            [Required]
            [StringLength(100, ErrorMessage = "Nome do evento é muito grande, tente um nome menor")]
            [MinLength(2, ErrorMessage = "Nome do evento é muito pequeno, tente um nome com pelo menos 2 caracteres")]
            public string NomeEvento { get; set; }

            [Required(ErrorMessage = "Capacidade é obrigatória!")]
            public int CapacidadeEvento { get; set; }

            [Required(ErrorMessage = "Quantidade de ingressos é obrigatória!")]
            public int QuantidadeIngressos { get; set; }

            [Required(ErrorMessage = "Data do evento é obrigatória!")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
            public DateTime DataEvento { get; set; }

            [Required(ErrorMessage = "Valor do ingresso é obrigatório!")]
            public float ValorIngresso { get; set; }

            [Required]
            public Casa Casa { get; set; }

            public Genero Genero { get; set; }

            public string Imagem { get; set; }

        }


    }
}

