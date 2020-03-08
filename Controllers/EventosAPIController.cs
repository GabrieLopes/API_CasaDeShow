using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CasaEventos.Data;
using CasaEventos.DTO;
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
            if (_context.Evento.Count() <= 0)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Nenhum evento foi encontrado.");
            }
            else
            {
                _context.Casa.ToList();
                _context.Genero.ToList();
                var eventos = _context.Evento.ToList();
                return Ok(eventos);
            }
        }
        /// <summary>
        /// Salvar um evento.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] EventoTempo eventoTempo)
        {
            /* Validação */
            if (eventoTempo != null)
            {
                try
                {

                    if (eventoTempo.NomeEvento.Length <= 1)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "O nome do evento precisa ter mais do que 1 caracter." });
                    }
                    if (eventoTempo.CapacidadeEvento <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "A capacidade precisa ser superior a 0." });
                    }
                    if (eventoTempo.QuantidadeIngressos <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "A quantidade de ingressos precisa ser superior a 0." });
                    }
                    if (eventoTempo.DataEvento == null)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Coloque uma data válida." });
                    }
                    if (eventoTempo.ValorIngresso <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "O preço do ingresso precisa ser superior a 0." });
                    }
                    if (eventoTempo.CasaId <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Id de casa inválido." });
                    }
                    if (eventoTempo.GeneroId <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Id de genero inválido." });
                    }
                    Evento eventoAPI = new Evento();
                    eventoAPI.NomeEvento = eventoTempo.NomeEvento;
                    eventoAPI.CapacidadeEvento = eventoTempo.CapacidadeEvento;
                    eventoAPI.QuantidadeIngressos = eventoTempo.QuantidadeIngressos;
                    eventoAPI.DataEvento = eventoTempo.DataEvento;
                    eventoAPI.ValorIngresso = eventoTempo.ValorIngresso;
                    eventoAPI.Imagem = eventoTempo.Imagem;
                    eventoAPI.Casa = _context.Casa.First(cs => cs.CasaId == eventoTempo.CasaId);
                    eventoAPI.Genero = _context.Genero.First(cs => cs.GeneroId == eventoTempo.GeneroId);
                    eventoAPI.Status = true;

                    _context.Evento.Add(eventoAPI);
                    _context.SaveChanges();
                    Response.StatusCode = 201;
                    return new ObjectResult("Evento criado com sucesso.");
                }
                catch (Exception)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Requisição inválida, o corpo não pode ser vazio." });
                }
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
                    Evento evento = _context.Evento.First(ev => ev.EventoId == id);
                    return Ok(evento);

                }
                catch (Exception)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("");
                }
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não existem eventos criados");
            }
        }


        /// <summary>
        /// Atualizar evento.
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody] Evento eventoPatch)
        {
            try
            {
                if (eventoPatch.EventoId == 0)
                {
                    return NotFound("Id invalido");
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        var evento = _context.Evento.First(ev => ev.EventoId == eventoPatch.EventoId);
                        if (evento != null)
                        {
                            evento.NomeEvento = eventoPatch.NomeEvento != null && eventoPatch.NomeEvento.Length > 1 ? eventoPatch.NomeEvento : evento.NomeEvento;
                            evento.CapacidadeEvento = eventoPatch.CapacidadeEvento > 0 ? eventoPatch.CapacidadeEvento : evento.CapacidadeEvento;
                            evento.QuantidadeIngressos = eventoPatch.QuantidadeIngressos > 0 ? eventoPatch.QuantidadeIngressos : evento.QuantidadeIngressos;
                            evento.DataEvento = eventoPatch.DataEvento != null ? eventoPatch.DataEvento : evento.DataEvento;
                            evento.ValorIngresso = eventoPatch.ValorIngresso > 0 ? eventoPatch.ValorIngresso : evento.ValorIngresso;
                            evento.Casa.CasaId = eventoPatch.Casa.CasaId > 0 ? eventoPatch.Casa.CasaId : evento.Casa.CasaId;
                            evento.Genero.GeneroId = eventoPatch.Genero.GeneroId > 0 ? eventoPatch.Genero.GeneroId : evento.Genero.GeneroId;
                            evento.Imagem = eventoPatch.Imagem != null ? eventoPatch.Imagem : evento.Imagem;
                        }
                        else
                        {
                            return NotFound();
                        }

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return NotFound();
                    }
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception)
            {
                return BadRequest("Evento não encontrado");

            }


        }

        /// <summary>
        /// Deletar um evento.
        /// </summary>
        [HttpDelete("{id}")] //Especificando que irá trabalhar com um Id
        public IActionResult Delete(int id)
        {
            try
            {
                Evento evento = _context.Evento.First(ev => ev.EventoId == id);
                _context.Evento.Remove(evento);
                _context.SaveChanges();
                return Ok(new { msg = "Evento excluido!" });
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "Não foi possivel excluir o evento ou o evento não existe." });
            }
        }
        /// <summary>
        /// Listar os eventos em ordem crescente por capacidade.
        /// </summary>
        [HttpGet("capacidade/asc")]
        public IActionResult GetEventoCapacidadeByAsc()
        {
            var evento = _context.Evento.OrderBy(Asc => Asc.CapacidadeEvento).ToList();
            return Ok(evento);

        }
        /// <summary>
        /// Listar os eventos em ordem decrescente por capacidade.
        /// </summary>
        [HttpGet("capacidade/desc")]
        public IActionResult GetEventoCapacidadeByDesc()
        {
            var evento = _context.Evento.OrderByDescending(Desc => Desc.CapacidadeEvento).ToList();
            return Ok(evento);

        }


        /// <summary>
        /// Listar os eventos em ordem crescente por data.
        /// </summary>
        [HttpGet("data/asc")]
        public IActionResult GetEventoDataByAsc()
        {
            var evento = _context.Evento.OrderBy(Asc => Asc.DataEvento).ToList();
            return Ok(evento);

        }
        /// <summary>
        /// Listar os eventos em ordem decrescente por data.
        /// </summary>
        [HttpGet("data/desc")]
        public IActionResult GetEventoDataByDesc()
        {
            var evento = _context.Evento.OrderByDescending(Desc => Desc.DataEvento).ToList();
            return Ok(evento);

        }


        /// <summary>
        /// Listar os eventos em ordem crescente por nome.
        /// </summary>
        [HttpGet("nome/asc")]
        public IActionResult GetEventoNomeByAsc()
        {
            var evento = _context.Evento.OrderBy(nomeAsc => nomeAsc.NomeEvento).ToList();
            return Ok(evento);

        }
        /// <summary>
        /// Listar os eventos em ordem decrescente por nome.
        /// </summary>
        [HttpGet("nome/desc")]
        public IActionResult GetEventoNomeByDesc()
        {
            var evento = _context.Evento.OrderByDescending(nomeDesc => nomeDesc.NomeEvento).ToList();
            return Ok(evento);

        }
        /// <summary>
        /// Listar os eventos em ordem crescente por preço.
        /// </summary>
        [HttpGet("preco/asc")]
        public IActionResult GetEventoPrecoByAsc()
        {
            var evento = _context.Evento.OrderBy(Asc => Asc.ValorIngresso).ToList();
            return Ok(evento);

        }
        /// <summary>
        /// Listar os eventos em ordem decrescente por preço.
        /// </summary>
        [HttpGet("preco/desc")]
        public IActionResult GetEventoPrecoByDesc()
        {
            var evento = _context.Evento.OrderByDescending(Desc => Desc.ValorIngresso).ToList();
            return Ok(evento);

        }
    }

}


public class EventoTempo
{
    public int EventoId { get; set; }
    public string NomeEvento { get; set; }

    public int CapacidadeEvento { get; set; }

    public int QuantidadeIngressos { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime DataEvento { get; set; }

    public float ValorIngresso { get; set; }
    public bool Status { get; set; }

    public int CasaId { get; set; }

    public int GeneroId { get; set; }

    public string Imagem { get; set; }

}
