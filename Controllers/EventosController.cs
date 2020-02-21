using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasaEventos.Data;
using CasaEventos.Models;
using CasaEventos.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CasaEventos.Controllers
{
    //Controle de acesso com Identity
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _hostEnvironment;
        // private object _hostEnvironment;

        public EventosController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        [Authorize]

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            if(_context.Casa.Count() == 0)
            {
                return View("EmptyCasa");
            }
            if(_context.Genero.Count() == 0)
            {
                return View("EmptyGenero");
            }
            ViewBag.Casa = _context.Casa.ToList();
            ViewBag.Genero = _context.Genero.ToList();
            return View(await _context.Evento.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .FirstOrDefaultAsync(m => m.EventoId == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Casa = _context.Casa.ToList();
            ViewBag.Genero = _context.Genero.ToList();
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("EventoId,NomeEvento,CapacidadeEvento,QuantidadeIngressos,DataEvento,ValorIngresso,Casa, Genero, Status, Imagem")] EventoDTO eventoTemp, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {//Inserindo maneira de colocar fotos em eventos
                foreach(var formFile in files)
                {
                if(formFile.Length > 0)
                    {
                    var filePath = Path.Combine(_hostEnvironment.WebRootPath, "Img");
                    var fileName = Path.GetRandomFileName();
                    var fileExtension = Path.GetExtension(formFile.FileName);

                    var fullPath = Path.Combine(filePath, fileName + fileExtension);

                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    eventoTemp.Imagem = "/Img/" + fileName + fileExtension;
                    }
                }
                Evento evento = new Evento();
                evento.EventoId = eventoTemp.EventoId;
                evento.NomeEvento = eventoTemp.NomeEvento;
                evento.CapacidadeEvento = System.Convert.ToInt32(eventoTemp.CapacidadeEvento);
                evento.QuantidadeIngressos = System.Convert.ToInt32(eventoTemp.QuantidadeIngressos);
                evento.DataEvento = eventoTemp.DataEvento;
                evento.ValorIngresso = System.Convert.ToSingle(eventoTemp.ValorIngresso);
                evento.Casa = _context.Casa.First(cs => cs.CasaId == eventoTemp.Casa);//Pegando Id de Casa para salvar no banco de dados
                evento.Genero = _context.Genero.First(cs =>cs.GeneroId == eventoTemp.Genero);//Pegando Id do genero para salvar no banco
                evento.Status = true;   
                evento.Imagem = eventoTemp.Imagem;
                _context.Add(evento);
                await  _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }else{
            ViewBag.Casa = _context.Casa.ToList();
            ViewBag.Genero = _context.Genero.ToList();
            return View(eventoTemp);
            }
        }

        // GET: Eventos/Edit/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.Include(e => e.Casa).Include(e => e.Genero).SingleOrDefaultAsync(e => e.EventoId == id);
            if (evento == null)
            {
                return NotFound();
            }
            EventoDTO viewEvento = new EventoDTO();
            viewEvento.EventoId = evento.EventoId;
            viewEvento.NomeEvento = evento.NomeEvento;
            viewEvento.CapacidadeEvento = System.Convert.ToString(evento.CapacidadeEvento);
            viewEvento.QuantidadeIngressos = System.Convert.ToString(evento.QuantidadeIngressos);
            viewEvento.DataEvento = evento.DataEvento;
            viewEvento.Status = true;
            viewEvento.ValorIngresso = System.Convert.ToString(evento.ValorIngresso);
            viewEvento.Casa = evento.Casa.CasaId;
            viewEvento.Imagem = evento.Imagem;
            ViewBag.Casa = _context.Casa.ToList();
            ViewBag.Genero = _context.Genero.ToList();
            return View(viewEvento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("EventoId,NomeEvento,CapacidadeEvento,QuantidadeIngressos,DataEvento,ValorIngresso,GeneroEvento, Casa, Genero, Status, Imagem")] EventoDTO eventoTemp, List<IFormFile> files)
        {
            if (id != eventoTemp.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
            foreach(var formFile in files)
                {
                if(formFile.Length > 0)
                    {
                    var filePath = Path.Combine(_hostEnvironment.WebRootPath, "Img");
                    var fileName = Path.GetRandomFileName();
                    var fileExtension = Path.GetExtension(formFile.FileName);

                    var fullPath = Path.Combine(filePath, fileName + fileExtension);

                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    eventoTemp.Imagem = "/Img/" + fileName + fileExtension;
                    }
                }
                Evento evento = new Evento();
                evento.EventoId = eventoTemp.EventoId;
                evento.NomeEvento = eventoTemp.NomeEvento;
                evento.CapacidadeEvento = System.Convert.ToInt32(eventoTemp.CapacidadeEvento);
                evento.QuantidadeIngressos = System.Convert.ToInt32(eventoTemp.QuantidadeIngressos);
                evento.DataEvento = eventoTemp.DataEvento;
                evento.ValorIngresso = System.Convert.ToSingle(eventoTemp.ValorIngresso);
                evento.Casa = _context.Casa.First(cs => cs.CasaId == eventoTemp.Casa);//Pegando Id de Casa para salvar no banco de dados
                evento.Genero = _context.Genero.First(cs =>cs.GeneroId == eventoTemp.Genero);//Pegando Id do genero para salvar no banco
                evento.Imagem = eventoTemp.Imagem;
                evento.Status = true;
                _context.Update(evento);
                await  _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(eventoTemp.EventoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            ViewBag.Casa = _context.Casa.ToList();
            ViewBag.Genero = _context.Genero.ToList();
            return View(eventoTemp);
        }

        // GET: Eventos/Delete/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FirstOrDefaultAsync(m => m.EventoId == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Evento.FindAsync(id);
            await _context.SaveChangesAsync();
            _context.Evento.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> TodosEventos(){
            ViewBag.Casa = _context.Casa.ToList();
            ViewBag.Genero = _context.Genero.ToList();
            return View(await _context.Evento.ToListAsync());
        }

        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.EventoId == id);
        }
    }
}
