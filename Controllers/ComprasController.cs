using System;
using System.Linq;
using System.Threading.Tasks;
using CasaEventos.Data;
using CasaEventos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CasaEventos.Controllers
{
    public class ComprasController : Controller
    {
          private readonly ApplicationDbContext _context;

        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Compra(int? id){
             if (id == null)
            {
                return NotFound();
            }
            var evento = _context.Evento.ToList();
            Compra compra = new Compra();
            compra.Evento = _context.Evento.First(c => c.EventoId == id);
            return View(compra);
        }

        public IActionResult ConfirmarCompra(Compra compra)
        {
            compra.Evento = _context.Evento.First(c => c.EventoId == compra.Evento.EventoId);
            compra.DataCompra = DateTime.Now;
            compra.TotalCompra = compra.QtdIngressos * compra.Evento.ValorIngresso;

            //Salvando Id do usuário na compra
            compra.IdentityUser.Id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            compra.IdentityUser = _context.Users.First(c => c.Id == compra.IdentityUser.Id);

            //Decrementando a quantia de ingressos
            var ingresso = _context.Evento.First(c => c.EventoId == compra.Evento.EventoId);
            ingresso.QuantidadeIngressos -= compra.QtdIngressos;
            
            _context.Update(ingresso);
            _context.Add(compra);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        [Authorize]
        public async Task<IActionResult> HistoricoCompra(Compra compra){
            ViewBag.Casa = _context.Casa.ToList();
            ViewBag.Genero = _context.Genero.ToList();
            return View( await _context.Compra.Include(x =>x.Evento).Where(x => x.IdentityUser.Id == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync());
        }
    }
}