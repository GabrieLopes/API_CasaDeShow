using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasaEventos.Models
{
    public class Evento
    {
        [Required]
        [Key]
        public int EventoId { get;set; }

        [Required]
        [StringLength(100, ErrorMessage="Nome do evento é muito grande, tente um nome menor")]
        [MinLength(2, ErrorMessage="Nome do evento é muito pequeno, tente um nome com pelo menos 2 caracteres")]
        public string NomeEvento { get;set; }

        [Required(ErrorMessage="Capacidade é obrigatória!")]
        public int CapacidadeEvento { get;set; }

        [Required(ErrorMessage="Quantidade de ingressos é obrigatória!")]
        public int QuantidadeIngressos { get;set; }

        [Required(ErrorMessage="Data do evento é obrigatória!")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataEvento { get;set; }

        [Required(ErrorMessage="Valor do ingresso é obrigatório!")]
        public float ValorIngresso { get;set; }
        [Required] 
        public bool Status { get;set; }

        [Required]
        public Casa Casa { get;set; }

        public Genero Genero { get;set; }
   
        public string Imagem { get;set; }

    }
}