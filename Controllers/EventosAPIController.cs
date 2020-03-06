using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CasaEventos.Data;
using CasaEventos.DTO;
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
        public IActionResult Patch([FromBody] EventoTempo eventoTempo)
        {

            if (_context.Evento.Count() > 0)
            {
                try
                {
                    Evento evento = _context.Evento.First(e => e.EventoId == eventoTempo.EventoId);
                    if (eventoTempo.EventoId > 0)
                    {

                        //Alterar nome do evento
                        if (eventoTempo.NomeEvento != null)
                        {
                            if (eventoTempo.NomeEvento.Length <= 1)
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new { msg = "O nome do evento precisa ter mais do que 1 caracter." });
                            }
                            else
                            {
                                evento.NomeEvento = eventoTempo.NomeEvento;
                                _context.SaveChanges();
                                Response.StatusCode = 200;
                                return new ObjectResult(new { msg = "Nome alterado com sucesso." });
                            }

                        }
                        // Alterar capacidade do evento
                        if (eventoTempo.CapacidadeEvento.ToString() != null)
                        {
                            if (eventoTempo.CapacidadeEvento > 0)
                            {
                                evento.CapacidadeEvento = eventoTempo.CapacidadeEvento;
                                _context.SaveChanges();
                                Response.StatusCode = 200;
                                return new ObjectResult(new { msg = "Capacidade alterada com sucesso." });
                            }
                            else
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new { msg = "A capacidade precisa ser superior a 0." });
                            }

                        }
                        // Alterar quantidade de ingressos
                        if (eventoTempo.ValorIngresso.ToString() != null)
                        {
                            if (eventoTempo.ValorIngresso > 0)
                            {
                                evento.ValorIngresso = eventoTempo.ValorIngresso;
                                _context.SaveChanges();
                                Response.StatusCode = 200;
                                return new ObjectResult(new { msg = "Valor dos ingressos alterados com sucesso." });
                            }
                            else
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new { msg = "A quantidade de ingressos precisa ser superior a 0." });
                            }
                        }
                        //Alterar Data do evento
                        if (eventoTempo.DataEvento != null)
                        {
                            if (eventoTempo.DataEvento == null)
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new { msg = "Coloque uma data válida." });
                            }
                            else
                            {
                                evento.DataEvento = eventoTempo.DataEvento;
                                _context.SaveChanges();
                                Response.StatusCode = 200;
                                return new ObjectResult(new { msg = "Data alterada com sucesso." });
                            }

                        }
                        // Alterar Valor do Ingresso
                        if (eventoTempo.QuantidadeIngressos.ToString() != null)
                        {
                            if (eventoTempo.QuantidadeIngressos > 0)
                            {
                                evento.QuantidadeIngressos = eventoTempo.QuantidadeIngressos;
                                _context.SaveChanges();
                                Response.StatusCode = 200;
                                return new ObjectResult(new { msg = "Quantidade dos ingressos alterado com sucesso." });
                            }
                            else
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new { msg = "A quantia do ingresso precisa ser superior a 0." });
                            }
                        }
                        // Alterar Casa
                        if (eventoTempo.CasaId.ToString() != null)
                        {
                            if (eventoTempo.CasaId <= 0)
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new { msg = "Id de casa inválido." });
                            }
                            else
                            {
                                evento.Casa.CasaId = eventoTempo.CasaId;
                                _context.SaveChanges();
                                Response.StatusCode = 200;
                                return new ObjectResult(new { msg = "Casa de show alterada com sucesso." });
                            }
                        }
                        // Alterar Genero
                        if (eventoTempo.GeneroId.ToString() != null)
                        {
                            if (eventoTempo.GeneroId <= 0)
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new { msg = "Id de genero inválido." });
                            }
                            else
                            {
                                evento.Genero.GeneroId = eventoTempo.GeneroId;
                                _context.SaveChanges();
                                Response.StatusCode = 200;
                                return new ObjectResult(new { msg = "Genero do evento alterado com sucesso." });
                            }
                        }
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Não é possivel alterar um evento vazio." });
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Id de evento invalido." });
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
