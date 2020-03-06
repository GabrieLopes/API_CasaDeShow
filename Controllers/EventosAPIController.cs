using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CasaEventos.Data;
using CasaEventos.Models;
using Microsoft.AspNetCore.Mvc;

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
                _context.Casa.ToList();
                _context.Genero.ToList();
                var eventos = _context.Evento.ToList();
                return Ok(eventos);
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult("Nenhum evento foi encontrado.");
            }
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
                try
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
                    if (eventoTemp.CasaId <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Id de casa inválido." });
                    }
                    if (eventoTemp.GeneroId <= 0)
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
                    eventoAPI.Imagem = eventoTemp.Imagem;
                    eventoAPI.Casa = _context.Casa.First(cs => cs.CasaId == eventoTemp.CasaId);
                    eventoAPI.Genero = _context.Genero.First(cs => cs.GeneroId == eventoTemp.GeneroId);
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
        public IActionResult Patch([FromBody] Evento evento)
        {
            if (_context.Evento.Count() > 0)
            {
                try
                {

                    if (evento.NomeEvento.Length <= 1)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "O nome do evento precisa ter mais do que 1 caracter." });
                    }
                    if (evento.CapacidadeEvento <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "A capacidade precisa ser superior a 0." });
                    }
                    if (evento.QuantidadeIngressos <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "A quantidade de ingressos precisa ser superior a 0." });
                    }
                    if (evento.DataEvento == null)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Coloque uma data válida." });
                    }
                    if (evento.ValorIngresso <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "O preço do ingresso precisa ser superior a 0." });
                    }
                    if (evento.Casa.CasaId <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Id de casa inválido." });
                    }
                    if (evento.Genero.GeneroId <= 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Id de genero inválido." });
                    }
                    var ev = _context.Evento.First(eventoTemp => eventoTemp.EventoId == evento.EventoId);
                    if (ev != null)
                    {
                        //Editar
                        ev.NomeEvento = evento.NomeEvento != null ? evento.NomeEvento : ev.NomeEvento;
                        ev.CapacidadeEvento = evento.CapacidadeEvento != 0 ? evento.CapacidadeEvento : ev.CapacidadeEvento;
                        ev.QuantidadeIngressos = evento.QuantidadeIngressos != 0 ? evento.QuantidadeIngressos : ev.QuantidadeIngressos;
                        ev.DataEvento = evento.DataEvento != null ? evento.DataEvento : ev.DataEvento;
                        ev.ValorIngresso = evento.ValorIngresso != 0 ? evento.ValorIngresso : ev.ValorIngresso;
                        ev.Imagem = evento.Imagem != null ? evento.Imagem : ev.Imagem;
                        ev.Casa = evento.Casa != null ? evento.Casa : ev.Casa;
                        ev.Genero = evento.Genero != null ? evento.Genero : ev.Genero;

                        _context.SaveChanges();
                        return Ok(new { msg = "Evento alterado com sucesso." });
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Requsição inválida, o corpo não pode ser vazio." });
                    }
                }
                catch
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Requsição inválida, o corpo não pode ser vazio." });
                }

            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Não existe nenhum evento cadastrado." });
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



public class EventoTemp
{
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
