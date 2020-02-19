using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CasaEventos.Models
{
    public class Compra
    {
        [Key]
        public int CompraId { get;set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Valor inválido")]
        public int QtdIngressos { get;set; }
        public DateTime DataCompra{ get;set; }
        public float TotalCompra { get;set; }
        public Evento Evento { get;set; }
        public IdentityUser IdentityUser { get ;set; }
    }
}