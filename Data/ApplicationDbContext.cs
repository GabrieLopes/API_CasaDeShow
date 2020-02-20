using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CasaEventos.Models;

namespace CasaEventos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<CasaEventos.Models.Casa> Casa { get; set; }
        public DbSet<CasaEventos.Models.Evento> Evento { get; set; }
        public DbSet<CasaEventos.Models.Genero> Genero { get; set; }
        public DbSet<CasaEventos.Models.Compra> Compra { get; set; }

    }
}
