using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CasaEventos.Data;
using CasaEventos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CasaEventos.Controllers
{
    [Route("api/v1/vendas")]
    [ApiController]
    public class VendasAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public VendasAPIController(ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Listar todas as vendas.
        /// </summary>
        [HttpGet]
        public IActionResult GetCompra()
        {
            if (_context.Compra != null)
            {


                var compras = _context.Compra.Select(
                            x => new CompraTemp
                            {
                                CompraId = x.CompraId,
                                QtdIngressos = x.QtdIngressos,
                                IdentityUser = x.IdentityUser.Email,
                                DataCompra = x.DataCompra,
                                Evento = x.Evento.NomeEvento,
                                Casa = x.Evento.Casa.Nome,
                                Genero = x.Evento.Genero.GeneroNome,
                                TotalCompra = x.TotalCompra

                            });
                _context.Casa.ToList();
                _context.Genero.ToList();
                _context.Evento.ToList();
                return Ok(new { compras });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult("Nenhuma compra foi encontrada.");
            }
        }

        /// <summary>
        /// Buscar compra por Id.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetUser(int? id)
        {
            if (id != null)
            {
                _context.Casa.ToList();
                _context.Genero.ToList();
                try
                {
                    var venda = _context.Compra.Select(
                        x => new CompraTemp
                        {
                            CompraId = x.CompraId,
                            QtdIngressos = x.QtdIngressos,
                            IdentityUser = x.IdentityUser.Email,
                            DataCompra = x.DataCompra,
                            Evento = x.Evento.NomeEvento,
                            Casa = x.Evento.Casa.Nome,
                            Genero = x.Evento.Genero.GeneroNome,
                            TotalCompra = x.TotalCompra

                        }).First(venda => venda.CompraId == id);
                    return Ok(new { venda });

                }
                catch (Exception)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Nenhuma compra foi encontrada, favor inserir um id válido.");
                }
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult("Vendas não encontradas, favor inserir um id válido.");
            }
        }
    }


    public class CompraTemp
    {
        public int CompraId { get; set; }
        public int QtdIngressos { get; set; }
        public DateTime DataCompra { get; set; }
        public float TotalCompra { get; set; }
        public string Evento { get; set; }
        public string Casa { get; set; }
        public string Genero { get; set; }
        public string IdentityUser { get; set; }
    }
}