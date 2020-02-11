using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace CasaEventos.DTO
{
    public class EventoDTO
    {

        [Required]
        [Key]
        public int EventoId { get;set; }

        [Required(ErrorMessage="Campo Obrigatório")]
        [StringLength(100, ErrorMessage="Nome do evento é muito grande, tente um nome menor")]
        [MinLength(2, ErrorMessage="Nome do evento é muito pequeno, tente um nome com pelo menos 2 caracteres")]
        public string NomeEvento { get;set; }

        [Required(ErrorMessage="Campo Obrigatório")]
        [StringLength(10000, ErrorMessage="A capacidade não pode ser maior que 10000, tente um numero menor")]
        [MinLength(1, ErrorMessage="A capacidade não pode ser menor que 0, tente um numero maior")]
        public string CapacidadeEvento { get;set; }
      
        [Required(ErrorMessage="Campo Obrigatório")]
        public string QuantidadeIngressos { get;set; }

        [Required(ErrorMessage="Data do evento é obrigatório")]
        [DisplayName("Data do Evento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataEvento { get;set; }

        [Required(ErrorMessage="Campo Obrigatório")]
        public string ValorIngresso { get;set; }
        [Required(ErrorMessage="Casa de Show é obrigatória")]

        public bool Status { get;set; }

        [Required]
        public string Imagem { get; set; }

        [Required(ErrorMessage="Casa de show é obrigatória")]
        public int Casa { get;set; }
        [Required(ErrorMessage="Genero do evento é obrigatório")]
        public int Genero { get;set; }
    }
}