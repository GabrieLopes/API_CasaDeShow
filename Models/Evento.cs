using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasaEventos.Models
{
    public class Evento
    {
 
        [Key]
        public int EventoId { get;set; }
        public string NomeEvento { get;set; }

        public int CapacidadeEvento { get;set; }
        public int QuantidadeIngressos { get;set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataEvento { get;set; }

        public float ValorIngresso { get;set; }

        public bool Status { get;set; }
        
        public Casa Casa { get;set; }

        public Genero Genero { get;set; }
   
        public string Imagem { get;set; }

    }
}